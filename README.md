## Technologies 
- **.NET 7.0**: using for create API at backend and ASP.NET Core MVC for front-end
- **JWT**: used for authentication using token
- **Microsoft Sql Server**: Relational database
- **Entity Framework**: ORM(Object Relational Mapper) interact with the database

## Main structure
### Backend 
- Implementing the Repository and Unit of Work patterns
- Contains the APIs and logic handles
- Using Entity Framework Core - Code First
- Using JWT to config and handle login authentication
### Frontend 
(Not built yet)

## Functionality
- User: login, logout
- Daster data management: Brand, Category, Item, Customer, Supplier, User
- Order management system
  
## Guidance
- Software requirements: Visual Studio, SQL server, SSMS (optional).
- After cloning the project, go to the **src** folder, run the **.sln** file, visual studio will run up.
- On the navigation bar of Visual Studio select the following: **Tools -> NuGet Package Manager -> Package Manager Console**.
- Select default project as **SalesManagementWebsite.Infrastructure** and run command: **update-database** to create your local database.
- Run the **SalesManagementWebsite.API** project you will see the homepage is **Swagger** displaying the API library.
  
## Contact
- **Author**: Vo Tuan Phuong
- **Email**: tuanphuong4139@gmail.com

