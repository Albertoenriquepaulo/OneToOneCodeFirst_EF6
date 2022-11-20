# One To One Example Using EF 6

Clone the project

```shell
git clone https://github.com/Albertoenriquepaulo/OneToOneCodeFirst_EF6.git
```

#### Ensure that the solutions have all the packages installed:

1. Microsoft.EntityFrameworkCore.SqlServer (6.0.10)
2. Microsoft.EntityFrameworkCore.Tools (6.0.10)
3. Microsoft.VisualStudio.Web.CodeGeneration.Design (6.0.10)
4. Swashbuckle.AspNetCore (6.2.3)

#### Run the Migrations command

I use the Package Manager Console

Add the migrations

```shell
Add-Migration "First Migration"
```

Update the database

```shell
Update-Database
```

*If you have problems updating the database, please check the `ConnectionStrings-OneToOneDbContext` section in the appsettings.json file and update accordingly your database connection*

You will see two class in the folder models, once you execute the migrations and the database update, two tables will be created in the database with a One to One relationship between them: User and UserActivation tables.

`User` doesn't depend on `UserActivation` , but `UserActivation` does, so an `User` can be saved without `UserActivation` but the `UserActivation` entity cannot be saved without the `User` entity. EF will throw an exception if you try to save the `StudentAddress` entity without the `Student` entity.

Reference:
https://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspx

