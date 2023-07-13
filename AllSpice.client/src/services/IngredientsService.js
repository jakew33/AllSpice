import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService{
  async addIngredient(formData){
    const res = await api.post(`api/recipes`, formData)
    logger.log('adding ingredient', res.data)
    AppState.ingredients.push(new Ingredient(res.data))
}

async getIngredientsByRecipeId(recipeId){
  logger.log(recipeId)
    const res = await api.get(`api/recipes${recipeId}/ingredients`)
    logger.log('getting ingredients', res.data)
    const ingredients = res.data.map(i=> new Ingredient(i))
    AppState.ingredients.push(...ingredients)
  }
}

export const ingredientsService = new IngredientsService()