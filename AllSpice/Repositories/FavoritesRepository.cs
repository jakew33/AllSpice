namespace AllSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites
    (accountId, recipeId)
    VALUES
    (@accountId, @recipeId);
    SELECT 
    *
    FROM favorites
    WHERE id = LAST_INSERT_ID();
    ";
    Favorite favorite = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();
    return favoriteData;
  }

  internal Favorite GetById(int favoriteId)
  {
    string sql = @"
    SELECT
    favs.*
    FROM favorites favs
    WHERE favs.id = @favoriteId;
    ";
    Favorite fav = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return fav;
  }

  internal int DeleteFavorite(int favoriteId)
  {
    string sql = @"
    DELETE
    FROM favorites
    WHERE id = @favoriteId
    LIMIT 1
    ;";
    int rows = _db.Execute(sql, new { favoriteId });
    return rows;
  }

  // internal List<FavoriteAccount> GetFavoritesByRecipeId(int recipeId)
  // {
  //   string sql = @"
  //   SELECT
  //   favs.*
  //   act.*
  //   FROM favorites favs
  //   JOIN accounts act ON act.id = favs.accountId
  //   WHERE favs.recipeId = @recipeId;
  //   ";
  //   List<FavoriteAccount> favorite = _db.Query<FavoriteAccount, Account, FavoriteAccount>(sql, (favorite, account) =>
  //   {
  //     favorite.Id = account.Id;
  //     return favorite;
  //   }, new { recipeId }).ToList();
  //   return favorite;
  // }

  internal List<FavoriteRecipe> GetMyFavoriteRecipes(string accountId)
  {
    string sql = @"
    SELECT
    favs.*,
    rec.*,
    act.*
    FROM favorites favs
    JOIN recipes rec ON rec.id = favs.recipeId
    JOIN accounts act ON act.id = rec.creatorId
    WHERE favs.accountId = @accountId;
    ";
    List<FavoriteRecipe> myFavorites = _db.Query<Favorite, FavoriteRecipe, Account, FavoriteRecipe>(sql, (favs, recipe, account) =>
    {
      recipe.FavoriteId = favs.Id;
      recipe.Creator = account;
      return recipe;
    }, new { accountId }).ToList();
    return myFavorites;
  }
}