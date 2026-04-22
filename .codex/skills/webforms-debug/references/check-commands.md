# Check Commands

## Build

```powershell
.\tools\Invoke-WebsiteChecks.ps1
```

## Watch

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -Watch
```

## IIS Express

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -StartIisExpress
```

## Smoke Test

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -SmokeUrl https://localhost:44357/
```

## Notes

- Treat a successful compile as the baseline verification because the solution currently has no dedicated test project.
- Use smoke testing for routing, master page, Forms Authentication, ScriptManager, and runtime database issues.
