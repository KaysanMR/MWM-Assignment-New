# MWM Assignment New

ASP.NET Web Forms keyboard shop application using .NET Framework, Bootstrap, jQuery, Forms Authentication, and a LocalDB database stored in `App_Data/`.

## Project Layout

- `MWM-Assignment-New.sln` - Visual Studio solution.
- `MWM-Assignment-New.csproj` - Web Forms project.
- `Site.Master` and `Site.Mobile.Master` - shared layouts.
- `Content/Site.css` - project styling.
- `Admin/` - admin dashboard and management pages.
- `Customer/` - customer profile, wishlist, and order pages.
- `Images/Products/` - product images.
- `App_Data/myData.mdf` - LocalDB database file.

## Requirements

- Visual Studio 2019 or newer, with ASP.NET and web development tools.
- .NET Framework 4.8 targeting pack.
- IIS Express.
- SQL Server LocalDB.
- ripgrep is optional, but recommended for fast repo searches.

## Build

```powershell
.\tools\Invoke-WebsiteChecks.ps1
```

Watch Web Forms files and rebuild after changes:

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -Watch
```

## Run And Smoke Test

Start IIS Express from the repo and smoke-test the site:

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -StartIisExpress
```

If the site is already running in Visual Studio or IIS Express:

```powershell
.\tools\Invoke-WebsiteChecks.ps1 -SmokeUrl https://localhost:44357/
```

## Search

The repo has a `.ignore` file so `rg` skips generated and vendored folders.

```powershell
rg --files
rg -n 'Session\[|SqlConnection|Response.Redirect' .
```

When searching file contents from PowerShell, include `.` as the path to avoid shell/glob surprises.

## Notes

- The main catalog page is `Products.aspx`.
- Authentication uses Forms Authentication and session values such as `UserID`, `UserRole`, and `Cart`.
- Avoid hand-editing `.designer.cs` files unless there is no better option.
- Keep SQL parameterized for new data access code.
