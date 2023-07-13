namespace AllSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;
  private readonly RecipesService _recipesService;

  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient newIngredient = _repo.CreateIngredient(ingredientData);
    return newIngredient;
  }

  internal List<Ingredient> GetIngredientByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  internal void DeleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetById(ingredientId);
    Recipe recipe = _recipesService.GetById(ingredient.recipeId);
    if (userId != recipe.CreatorId) throw new Exception("You shall not pass");
    int rows = _repo.DeleteIngredient(ingredientId);
    if (rows > 1) throw new Exception("That Wasn't Supposed To Happen");
  }

  // internal List<Ingeredient> GetAllIngredients()
  // {
  //   List<Ingredient> ingredients = _repo.GetAllIngredients();
  //   return ingredients;
  // }

  internal Ingredient GetById(int ingredientId)
  {
    Ingredient ingredient = _repo.GetById(ingredientId);
    if (ingredient == null) new Exception("Invalid Id");
    return ingredient;
  }
}