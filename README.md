# Intoduction
Before reading and understanding this content you should already know [PetaPoco](https://github.com/CollaboratingPlatypus/PetaPoco "PetaPoco")

PetaPoco Repository is a library of classes and interfaces created to jumpstart using PetaPoco with the Repository Pattern.

## Read Repositories
By deriving from ` ReadRepositoryBase<T, TPrimaryKeyType>` you will get a concrete implementations of `FindById`, `FindAllByIds` and `GetAll`, each of which take in strongly typed in put params and return strongly type IEnumerable


## Crud Repositories
Creating Repos that support the basic Create-Read-Update-Delete functions is as simple as derriving from `CrudRepositoryBase`.  Doing so provides you an implementation of `ICrudRepository` which gets you `Add`, `Update`, and `Remove` along with everything from the `IReadRepository` 


## Implementing a Simple Crud Repository
### Define your interface (optional but a good practice)
Declare that it derrives from `ICrudRepository` and define any extra methods that you want to provide.
```csharp
  public interface ISalesOrderRepository 
  		: PetaPoco.Repository.Abstractions.ICrudRepository<SalesOrder, int>
    {
        IEnumerable<SalesOrder> FindByMinSalesAmount(decimal minSalesAmount);
    }
```
### Create your implementation
By deriving from `CrudRepositoryBase` all your CRUD functions are implemented, you are left with only implementing your custom functionality.  

```csharp
public class SalesOrderRepository 
   : PetaPoco.Repository.CrudRepositoryBase<SalesOrder, int>, Abstractions.ISalesOrderRepository
    {
        public IEnumerable<SalesOrder> FindByMinSalesAmount(decimal minSalesAmount)
        {
            return this.Query($"WHERE {nameof(EntityType.Amount)} >= @0", minSalesAmount)
                .ToList();
        }
    }
```
*Of course you can always override the base CRUD methods for full control*
```csharp
        public override SalesOrder Add(SalesOrder entity)
        {
            if (entity.Amount < 0)
                throw new ApplicationException("You are a terrible salesman, you're losing us money!");

            return base.Add(entity);
        }
```

## Startup Configuration
Your repositories will need to know how to connect to the database, so somewhere in your application start up you'll need to specify how your repos get an concrete instance of `IDatabaseFactory`.  You can do that by using dependency injection or by setting up a configuration object.

### Using Dependency Injection
Register your own `IDatabaseFactory` implementation, or just register the one for Sql Server included in the package 

```csharp
services.AddSingleton<PetaPoco.Repository.Abstractions.IDatabaseFactory>(new PetaPoco.Repository.DefaultSqlDatabaseFactory(conString));
```
### Old-school, No DI
```csharp

            //get the connection string for our database
            string conString = "your connction string, prolly from appsettings.json or floppy disk";

            PetaPoco.Repository.Configuration.InitDefaultWithSqlServer(conString);
```

## Middleware/Service
First register the built-in Service collection, or implement your own
```csharp
services.AddSingleton<PetaPoco.Repository.Abstractions.ICrudRepositoryServiceCollection, PetaPoco.Repository.CrudRepositoryServiceCollection>();
```
then add services

## Extensions/Addons
[PetaPoco.Repository.CreateUpdateStamper](https://github.com/TimBurris/PetaPoco.Repository.CreateUpdateStamper// "PetaPoco.Repository.CreateUpdateStamper") is a Middleware Service Implementation allowing you automatically stamp your Models with Created By, Updated By, Created On Date and Updated On Date values at the time that it's written to the repository.

[PetaPoco.Repository.Logging](https://github.com/TimBurris/PetaPoco.Repository.Logging "PetaPoco.Repository.Logging") is a Middleware Service Implementation that includes Logging capability for the repositories
