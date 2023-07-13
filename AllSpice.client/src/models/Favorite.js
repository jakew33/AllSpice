export class Favorite {
  constructor(data){
    this.id = data.id
    this.accountId = data.accountId
    this.recipeId = data.recipeId
    this.recipe = data.recipe
  }
}
  export class MyFavorites extends Favorite {
    constructor(data) {
      super(data)
      this.title = data.recipe.title
      this.img = data.recipe.img
    }
  }
