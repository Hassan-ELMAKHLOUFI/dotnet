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