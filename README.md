# Avalonbuild.com

Website for Avalon Development & Construction Inc.

## To set up a development environment:

1.  Install the dotnet core sdk
2.  Clone this github repo
3.  Navigate to the avalonbuild.com\src\avalonbuild.com folder
4.  Run `dotnet restore` to install dependencies
5.  Run `dotnet tool install --global dotnet-ef`
6.  Run `dotnet ef database update -c ApplicationDbContext` to create the application db tables
7.  Run `dotnet ef database update -c FilebContext` to create the file db tables
8.  Run `dotnet ef database update -c ImageDbContext` to create the image db tables
9.  Run `dotnet run environment=development` to run the application in development mode
10. Navigate to `localhost:5000` in your browser to view the app
11. Navigate to `localhost:5000/register` to register the admin account (after initial create this url will redirect to the homepage)
12. To build the static content install the following
    - node js
    - bower
    - gulp
13. Run `bower install`
14. Run `gulp build`