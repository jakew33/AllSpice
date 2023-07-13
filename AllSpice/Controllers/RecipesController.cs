namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RecipesController : ControllerBase
{
  private readonly RecipesService _recipesService;
  private readonly IngredientsService _ingredientsService;
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth;

  public RecipesController(IngredientsService ingredientsService, RecipesService recipesService, FavoritesService favoritesService, Auth0Provider auth)
  {
    _recipesService = recipesService;
    _ingredientsService = ingredientsService;
    _favoritesService = favoritesService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;

      Recipe recipe = _recipesService.CreateRecipe(recipeData);
      return new ActionResult<Recipe>(Ok(recipe));
    }
    catch (Exception e)
    {
      return new ActionResult<Recipe>(BadRequest(e.Message));
    }
  }

  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes(string search)
  {
    try
    {
      List<Recipe> recipes;
      if (search == null)
      {
        recipes = _recipesService.GetAllRecipes();
      }
      else
      {
        recipes = _recipesService.SearchRecipes(search);
      }
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetById(recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{recipeId}")]
  [Authorize]
  public ActionResult<Recipe> UpdateRecipe(int recipeId, [FromBody] Recipe updateData)
  {
    try
    {
      updateData.Id = recipeId;
      Recipe recipe = _recipesService.UpdateRecipe(updateData);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientByRecipeId(recipeId);
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpDelete("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<Recipe>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      _recipesService.DeleteRecipe(recipeId, userInfo.Id);
      return Ok("delorted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // [HttpGet("{recipeId}/favorites")]
  // public ActionResult<List<FavoriteAccount>> GetFavoritesByRecipeId(int recipeId)
  // {
  //   try
  //   {
  //     List<FavoriteAccount> favorites = _favoritesService.GetFavoritesByRecipeId(recipeId);
  //     return Ok(favorites);
  //   }
  //   catch (Exception e)
  //   {
  //     return BadRequest(e.Message);
  //   }
  // }

}