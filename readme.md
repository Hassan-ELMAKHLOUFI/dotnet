## 
Partie 1 : Créer une application DotNet Core de type console qui permet gérer des comptes (id, curency, balance)

 Créer la classe Account
Créer l'interface AccountService avec les opérations : . AddNewAccount . GetAllAccounts . GetAccountById . GetDebitedAccounts . GetBalanceAVG()
Créer une implémentation de cette interface utilisant une collection de type Dictionary
Tester l'application
##
Partie 2 :Créer une application DotNet Core de type WebAPI qui permet gérer des produits appartenant à des catégories

## Partie 1 
# class Account
```
namespace Service{ 
public class Account
{
    public double balance ;
    public  int id ;
    public string currency;
    public Account()
    {
    
    }
    public Account(int id ,string currency,double balance)
    {
        this.id = id;
        this.currency = currency ;
        
        this.balance = balance ;
    }
    public override string ToString()
    {
        return  "id "+id+ " balance "+balance ; 
    }
    
}
}
```
# interface Service
```shell
namespace Service
{
    public interface AccountService{
       public void AddNewAccount(Account ac);
        public List<Account> GetAllAccounts() ; 
        public Account  GetAccountById(int id);
        public List<Account>  GetDebitedAccounts();
        public double GetBalanceAVG() ;
   
    }
}
```
# impl de service 
```shell


namespace Service

  {
    public class ServiceAccountImp : Service.AccountService
    {   


        Dictionary<int , Service.Account> dics = new Dictionary<int, Account> ();

       public  ServiceAccountImp (){

        }
        void AccountService.AddNewAccount(Account ac)
        {
            dics.Add(ac.id,ac);
        }

        Account AccountService.GetAccountById(int id)
        {
            return dics[id];
        }

        List<Account> AccountService.GetAllAccounts()
        {
           return dics.Values.ToList();
        }

        double AccountService.GetBalanceAVG()
        {
            return dics.Values.Average(acc=> acc.balance);
        }

       List<Account> AccountService.GetDebitedAccounts()
        {
            return (List<Account>) dics.Values.Where(acc => acc.balance> 0);
        }
    }

}
```

# Partie 2

# Category Model
```shell
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogueApp.Models
{
    [Table("CATEGORIES")]
    public class Category {
        [Key]
        public int CategoryID { get;set;}
        public string?  Name {get; set;}

        [JsonIgnore]
        public virtual ICollection <Product> Products {get;set;}
    }
}
```

# Product Model 
```shell
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogueApp.Models
{
    public class Product{
            public int ProductID {get; set;}
            public string Name{get; set;}
            public double Price { get; set;}
          
            public int CategoryID {get; set;}
            [ForeignKey("CategoryID")]
            public virtual Category Category{get; set;}
    }
}
```

# DB Init 
```shell
using CatalogueApp.Models ;
namespace CatalogueApp.Service
{
    public static class DbInit{
        public static void initData(CatalogueDbRepository catalogueDb){
            catalogueDb.Categories.Add(new Category{Name ="Ordinateurs"} );
            catalogueDb.Categories.Add(new Category{Name ="Imprimantes"} );
            catalogueDb.SaveChanges();
        }
    }
}
```
# Category Rest Controller
```shell
using CatalogueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections ;
using System.Linq.Expressions ;

namespace CatalogueApp.Controllers
{
    [Route("/api/categories")]
    public class CategoryRestController : Controller
    {
        public CatalogueDbRepository catalogueRepository{get;set;}

        public CategoryRestController(CatalogueDbRepository repository)
        {
            this.catalogueRepository = repository;
           
        }
        [HttpGet]
        public IEnumerable<Category> listCats(){
            return catalogueRepository.Categories; 
        }

        [HttpGet("{Id}")]
        public Category getCat(int Id){
            return catalogueRepository.Categories.FirstOrDefault(c=>c.CategoryID==Id); 
        }

        [HttpGet("{Id}/products")]
        public IEnumerable<Product>  getProducts(int Id){
            Category category = catalogueRepository.Categories.Include(c=>c.Products).FirstOrDefault(c=>c.CategoryID==Id); 
            return category.Products ; 
        }


        [HttpPut("{Id}")]
        public Category update([FromBody] Category category,int Id){

            category.CategoryID= Id ;
            catalogueRepository.Categories.Update(category);
            catalogueRepository.SaveChanges();
            return category ;
        }

        [HttpPost]
        public Category Save ([FromBody] Category category){
            catalogueRepository.Categories.Add(category);
            catalogueRepository.SaveChanges();
            return category ;
        }

        [HttpDelete("{Id}")]
        public void Delete (int Id){
           Category category = catalogueRepository.Categories.FirstOrDefault(c=>c.CategoryID==Id); 
           catalogueRepository.Categories.Remove(category);
           catalogueRepository.SaveChanges();

            
        }
    }
}
```

# Product Rest Controller 
````shell
using CatalogueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections ;
using System.Linq.Expressions ;

namespace CatalogueApp.Controllers
{
    [Route("/api/products")]
    public class ProductRestController : Controller
    {
        public CatalogueDbRepository catalogueRepository{get;set;}

        public ProductRestController(CatalogueDbRepository repository)
        {
            this.catalogueRepository = repository;
           
        }
        [HttpGet]
        public IEnumerable<Product> listCats(){
            return catalogueRepository.Products.Include(p=>p.Category); 
        }

        [HttpGet("{Id}")]
        public Product getCat(int Id){
            return catalogueRepository.Products.Include(p=>p.Category).FirstOrDefault(c=>c.ProductID==Id); 
        }

        [HttpPut("{Id}")]
        public Product update([FromBody] Product product,int Id){

            product.ProductID= Id ;
            catalogueRepository.Products.Update(product);
            catalogueRepository.SaveChanges();
            return product ;
        }

        [HttpPost]
        public Product Save ([FromBody] Product product){
            catalogueRepository.Products.Add(product);
            catalogueRepository.SaveChanges();
            return product ;
        }

        [HttpDelete("{Id}")]
        public void Delete (int Id){
           Product product= catalogueRepository.Products.FirstOrDefault(c=>c.ProductID==Id); 
           catalogueRepository.Products.Remove(product);
           catalogueRepository.SaveChanges();

            
        }
    }
}
````