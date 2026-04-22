[CmdletBinding()]
param(
    [switch]$Watch,
    [switch]$StartIisExpress,
    [string]$SmokeUrl,
    [string]$IisUrl = "https://localhost:44357/",
    [string]$IisSiteName = "MWM-Assignment-New",
    [string]$Configuration = "Debug",
    [string]$Platform = "Any CPU",
    [string]$Solution = "MWM-Assignment-New.sln"
)

$ErrorActionPreference = "Stop"

function Find-MSBuild {
    $fromPath = Get-Command MSBuild.exe -ErrorAction SilentlyContinue
    if ($fromPath) {
        return $fromPath.Source
    }

    $candidates = @(
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe",
        "${env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
    )

    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath $candidate) {
            return $candidate
        }
    }

    throw "MSBuild.exe was not found. Install Visual Studio Build Tools or add MSBuild to PATH."
}

function Invoke-SmokeCheck {
    param([string]$Url)

    if ([string]::IsNullOrWhiteSpace($Url)) {
        return
    }

    Write-Host "Smoke checking $Url"
    try {
        $response = Invoke-WebRequest -Uri $Url -UseBasicParsing -TimeoutSec 15
        if ($response.StatusCode -lt 200 -or $response.StatusCode -ge 400) {
            throw "Unexpected HTTP status $($response.StatusCode)"
        }
        Write-Host "Smoke check passed: HTTP $($response.StatusCode)"
    }
    catch {
        Write-Warning "Smoke check failed: $($_.Exception.Message)"
        throw
    }
}

function Find-IISExpress {
    $fromPath = Get-Command iisexpress.exe -ErrorAction SilentlyContinue
    if ($fromPath) {
        return $fromPath.Source
    }

    $candidates = @(
        "${env:ProgramFiles}\IIS Express\iisexpress.exe",
        "${env:ProgramFiles(x86)}\IIS Express\iisexpress.exe"
    )

    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath $candidate) {
            return $candidate
        }
    }

    throw "IIS Express was not found. Install IIS Express or start the site from Visual Studio."
}

function Test-UrlReady {
    param([string]$Url)

    try {
        $response = Invoke-WebRequest -Uri $Url -UseBasicParsing -TimeoutSec 3
        return ($response.StatusCode -ge 200 -and $response.StatusCode -lt 500)
    }
    catch {
        return $false
    }
}

function Start-ProjectIISExpress {
    param(
        [string]$Url,
        [string]$SiteName
    )

    if (Test-UrlReady -Url $Url) {
        Write-Host "IIS Express already responds at $Url"
        return
    }

    $applicationHostConfig = Join-Path (Get-Location) ".vs\MWM-Assignment-New\config\applicationhost.config"
    if (-not (Test-Path -LiteralPath $applicationHostConfig)) {
        throw "Missing IIS Express config: $applicationHostConfig"
    }

    $iisExpress = Find-IISExpress
    Write-Host "Starting IIS Express site '$SiteName'"
    Start-Process -FilePath $iisExpress -ArgumentList "/config:$applicationHostConfig", "/site:$SiteName", "/systray:false" -WindowStyle Hidden | Out-Null

    $deadline = (Get-Date).AddSeconds(20)
    while ((Get-Date) -lt $deadline) {
        if (Test-UrlReady -Url $Url) {
            Write-Host "IIS Express is ready at $Url"
            return
        }
        Start-Sleep -Milliseconds 500
    }

    throw "IIS Express did not respond at $Url within 20 seconds."
}

function Invoke-WebsiteChecks {
    $msbuild = Find-MSBuild
    Write-Host "Using MSBuild: $msbuild"
    Write-Host "Building $Solution [$Configuration|$Platform]"

    & $msbuild $Solution `
        /t:Restore,Build `
        "/p:Configuration=$Configuration" `
        "/p:Platform=$Platform" `
        /m `
        /v:minimal

    if ($LASTEXITCODE -ne 0) {
        throw "MSBuild failed with exit code $LASTEXITCODE."
    }

    if ($StartIisExpress) {
        Start-ProjectIISExpress -Url $IisUrl -SiteName $IisSiteName
        if ([string]::IsNullOrWhiteSpace($SmokeUrl)) {
            $SmokeUrl = $IisUrl
        }
    }

    Invoke-SmokeCheck -Url $SmokeUrl
}

function Start-Watch {
    Invoke-WebsiteChecks

    $watcher = New-Object System.IO.FileSystemWatcher
    $watcher.Path = (Get-Location).Path
    $watcher.IncludeSubdirectories = $true
    $watcher.EnableRaisingEvents = $true

    $script:watchExtensions = @(".aspx", ".ascx", ".master", ".cs", ".config", ".css", ".js")
    $script:watchIgnoredParts = @("\bin\", "\obj\", "\.git\", "\.vs\", "\packages\")
    $script:pending = $false
    $lastRun = Get-Date

    $action = {
        $path = $Event.SourceEventArgs.FullPath
        $extension = [System.IO.Path]::GetExtension($path)

        foreach ($part in $script:watchIgnoredParts) {
            if ($path -like "*$part*") {
                return
            }
        }

        if ($script:watchExtensions -notcontains $extension) {
            return
        }

        $script:pending = $true
    }

    $subscriptions = @(
        Register-ObjectEvent $watcher Changed -Action $action,
        Register-ObjectEvent $watcher Created -Action $action,
        Register-ObjectEvent $watcher Deleted -Action $action,
        Register-ObjectEvent $watcher Renamed -Action $action
    )

    Write-Host "Watching for Web Forms changes. Press Ctrl+C to stop."

    try {
        while ($true) {
            Start-Sleep -Milliseconds 500
            if (-not $script:pending) {
                continue
            }

            if (((Get-Date) - $lastRun).TotalSeconds -lt 2) {
                continue
            }

            $script:pending = $false
            $lastRun = Get-Date

            try {
                Invoke-WebsiteChecks
                Write-Host "Checks passed at $(Get-Date -Format 'HH:mm:ss')"
            }
            catch {
                Write-Warning "Checks failed at $(Get-Date -Format 'HH:mm:ss'): $($_.Exception.Message)"
            }
        }
    }
    finally {
        foreach ($subscription in $subscriptions) {
            Unregister-Event -SubscriptionId $subscription.Id -ErrorAction SilentlyContinue
        }
        $watcher.Dispose()
    }
}

if ($Watch) {
    Start-Watch
}
else {
    Invoke-WebsiteChecks
}
