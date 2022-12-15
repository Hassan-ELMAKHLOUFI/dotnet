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