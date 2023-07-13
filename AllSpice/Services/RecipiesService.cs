namespace AllSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repo.CreateRecipe(recipeData);
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }

  internal Recipe GetById(int recipeId)
  {
    Recipe recipe = _repo.GetById(recipeId);
    if (recipe == null) throw new Exception($"{recipeId}: It's a no from me, dawg");
    return recipe;
  }

  internal Recipe UpdateRecipe(Recipe updateData)
  {
    Recipe original = GetById(updateData.Id);

    original.Title = updateData.Title != null ? updateData.Title : original.Title;
    original.Instructions = updateData.Instructions != null ? updateData.Instructions : original.Instructions;
    original.Img = updateData.Img != null ? updateData.Img : original.Img;
    original.Category = updateData.Category != null ? updateData.Category : original.Category;


    _repo.UpdateRecipe(original);
    return original;
  }

  internal void DeleteRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetById(recipeId);
    if (recipe.CreatorId != userId) new Exception("Bruh");
    int rows = _repo.DeleteRecipe(recipeId);
    if (rows > 1) new Exception("Uuuuuh...");

  }

  internal List<Recipe> SearchRecipes(string search)
  {
    List<Recipe> recipes = _repo.SearchRecipes(search);
    return recipes;
  }
}