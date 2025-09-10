# Employee Management System
In Visual Studio Installer, click modify/download Visual Stuido 2022 and download/add the following packages:
-ASP.NET and web development
- .NET desktop development
Once you clone the repo, run:
```bash
git config core.hooksPath .githooks  
```
Install entity framework packages in Visual Studio:  
Right click on project name, open "Manage NuGet packages", download the following:  
- Microsoft.EntityFrameworkCore  
- Microsoft.EntityFrameworkCore.Tools  
- Microsoft.EntityFrameworkCore.Design  
- Microsoft.EntityFrameworkCore.SqlServer
- Newtonsoft.Json


On the terminal run these commands (note : change the path to the directory where EmployeeManagementSystem.API.csproj is stored )
cd "C:\Users\hp\Desktop\Tarsh\EmployeeManagementSystem.API"
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.*
