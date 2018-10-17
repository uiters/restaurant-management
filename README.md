# Restaurant Mangement

> 🍔🍟🍕🍺Restaurant Management App using C# and SQL Server.

![ndc](https://user-images.githubusercontent.com/34389409/44380263-84d16d00-a534-11e8-9b20-6a95daca5440.png)

## Features

* Order foods by table
* Checkout & print invoice
* Manage table
* Manage food & category
* Manage account
* Sales report

## Installation

**1. Create a C# Windows Forms Application**

* Create a C# Windows Forms Project.
* Add New Item and Add SQL Server Database to your application.
* Add a table to your application and fill some data in it.
* Show the data in your main form.

**2. Create a Setup Project**

* Add new project => setup and deployment => setup project.
* Right Click on Setup project and Add project Output and select primary output from your main project.
* Right Click on Setup project and Add project Output and select content files from your main project.
* Right CLick on setup project and Click Properties and click Prerequisites and select SQL Server Express.
* Select .Net Framework.
* Select Windows Installer.
* Select radio button Download prerequisites from the same location as my application.
* Right Click on Users Desktop at left pane and add new Shortcut and select application folder, primary output from SampleApplication, and click ok and the rename the short cut to what you need.
* Rebuild solution.
* Rebuild Setup Project.
* Go to Output directory of setup project and run setup.exe.

Enjoy 😍

## Build with

* [Visual Studio 2017](https://visualstudio.microsoft.com/fr/downloads/?rr=https%3A%2F%2Fwww.google.com.vn%2F)
* [SQL Server 2017](https://www.microsoft.com/en-us/sql-server/sql-server-2017)

## Documents

For help getting started with C#, view our online [documentation](https://docs.microsoft.com/en-us/dotnet/csharp/).

## License

MIT
