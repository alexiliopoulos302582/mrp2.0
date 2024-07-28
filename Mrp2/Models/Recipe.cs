namespace Mrp2.Models
{
    public class Recipe
    {


        public int Id { get; set; }
        public string RecipeName { get; set; } = "";



        public decimal TotalCost { get; set; } = 0;
         
        public decimal Quantity { get; set; } = 0;

        public ICollection<RecipeRawMaterial> recipeRawMaterials { get; set; }



    }
}
