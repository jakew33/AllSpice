namespace AllSpice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql =@"
    INSERT INTO ingredients
    (id, name, quantity, recipeId)
    VALUES
    (@id, @name, @quantity, @recipeId);

    SELECT
    *
    FROM ingredients
    WHERE id = LAST_INSERT_ID() 
    ;";

    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
    return ingredient;
  }

  // internal List<Ingredient> GetAllIngredients()
  // {
  //   string sql = @"SELECT
  //   *
  //   FROM ingredients;
  //   ";
  //   List<Ingredient> ingredients = _db.Query<Ingredient>(sql).ToList();
  //   return ingredients;
  // }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    *
    FROM ingredients
    WHERE recipeId = @recipeId
    ;";
    List<Ingredient> recipeIngredients = _db.Query<Ingredient>(sql,new {recipeId}).ToList(); 
    return new List<Ingredient>(recipeIngredients);
  }

  internal int DeleteIngredient(int ingredientId)
  {
    string sql = @"
    DELETE 
    FROM ingredients
    WHERE id = @ingredientId 
    LIMIT 1;";
    int rows = _db.Execute(sql, new {ingredientId});
    return rows;
  }

  internal Ingredient GetById(int ingredientId)
  {
  string sql = @"
  SELECT 
  *
  FROM ingredients
  WHERE id = @ingredientId;
    ";
    Ingredient ingredient = _db.Query<Ingredient>(sql, new {ingredientId}).FirstOrDefault();
    return ingredient;
  }
}