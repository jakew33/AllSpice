import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {
  async getAllRecipes() {
    const res = await api.get('api/recipes')
    logger.log('getting recipes', res.data)
    AppState.recipes = res.data.map(r => new Recipe(r))
    // logger.log("appstate recipes", AppState.recipes)
  }

  async getRecipeById(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}`)
    logger.log('getting recipe by id', res.data)
    AppState.activeRecipe = new Recipe(res.data)
  }

  async createRecipe(recipeData){
    const res = await api.post('api/recipes', recipeData)
    logger.log('creating recipe', res.data)
    AppState.recipes.unshift(new Recipe(res.data))
    return res.data
  }

  async deleteRecipe(recipeId) {
    const res = await api.delete(`api/recipes/${recipeId}`)
    AppState.recipes = AppState.recipes.filter(r => r.id != recipeId)
    logger.log('Delete Recipe', res.data)
  }

    async searchRecipes(searchTerm) {
    const res = await api.get('api/recipes', {
      params:{
        search: searchTerm
      }
    })
    AppState.query = searchTerm
    AppState.recipes = res.data.map(r => new Recipe(r));
    logger.log(AppState.recipes)
  }

  async setActiveRecipe(recipeId){
    AppState.activeRecipe = AppState.recipes.find(r=> r.id == recipeId)
    logger.log('Getting Active Recipe', AppState.activeRecipe)
  }
}

export const recipesService = new RecipesService()