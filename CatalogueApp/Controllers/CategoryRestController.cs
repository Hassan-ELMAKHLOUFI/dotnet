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