# GenericRepository
Generic Repository through Unit Of Work pattern for ASP.NET Core 3.1

Provide abstraction based on the Repository and Repository&UnitOfWork patterns and support common CRUD operations
 
 In the solution you will find simple project **DemoApp** with few examples:
  - **UserRepoContoller** implements a simple UserRepository based on the Generic Repository pattern.
  - **GenericRepoController** implements Generic Repository through Unit Of Work pattern.

## Usage
To use GenericRepository add the following code snippet to ConfigureServices method in **Startup.cs** just after registering DbContext

 ```csharp
public void ConfigureServices(IServiceCollection services)
{
  ...
  //Register you DbContext
  
  services.AddGenericRepository<__DbContext__>();
  
  ...
}
```

### Generic Repository through Unit Of Work pattern
The complete example you may find in **GenericRepoController** in **DemoApp**, here I will paste small snipped 

 ```csharp
public class GenericRepoController
{
  private readonly IUnitOfWork unitOfWork;
  private readonly IRepository<User> userRepository;

  public GenericRepoController(IUnitOfWork unitOfWork)
  {
      this.unitOfWork = unitOfWork;
      this.userRepository = unitOfWork.Repository<User>();
  }
  
  public void Add(User newUser)
  {
      this.userRepository.Add(newUser);

      await unitOfWork.SaveChanges();
  }
  
  ...
  
}
```

### Generic Repository
The library provides **RepositoryBase<TEntity>** class for inheritance if you want to create directly your own generic repositories and extend the provided functionality. You may find a complete example is in **UserRepoController** which uses **UserReposity**.

 ```csharp
public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(DemoDbContext dbContext)
        : base(dbContext)
    { }
    ...
    //Bellow you may add your own methods, example:
    public User ChangeEmail(int id, string email)
    {
        var user = this.GetById(id);
        user.Email = email;

        this.Update(user);

        this.SaveChanges();

        return user;
    }
}
```


## Methods

### Add new entity
 ```csharp
  Add(TEntity entity);
  AddAsync(TEntity entity);
```

### Update Entity
 ```csharp
  Update(TEntity entity);
```

### Remove Entity
 ```csharp
  Remove(TEntity entity);
```

### Get all entities
 ```csharp
  IEnumerable<TEntity> GetAll();
  Task<IEnumerable<TEntity>> GetAllAsync();
```

### Get entity by **id** - get the entity by primary key
 ```csharp
  TEntity GetById(object id);
  Task<TEntity> GetByIdAsync(object id);
```

### Get filtered entities
 ```csharp
  IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
  Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

Example: userRepository.Get(u => u.Email.EndsWith("@domain.com"))
```




