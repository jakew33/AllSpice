import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class favoritesService{

  async createFavorite(recipeId) {
    const res = await api.post('api/favorites', { recipeId})
    logger.log('creating favorite', res.data)
    AppState.favorites.push(new Favorite(res.data))
  }

  async getMyFavs(){
    const res = await api.get('account/favorites')
    logger.log('getting favs', res.data)
    AppState.myFavorites = res.data.map(c => new Favorite(c))
    logger.log(AppState.myFavorites)
  }
  async getFavsByRecipeId(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/favorites`)
    logger.log('getting recipe favs', res.data)
    AppState.favorites = res.data.map(f => new Favorite(f))
  }
  async removeFav(favoriteId) {
    const res = await api.delete(`api/favorites/${favoriteId}`)
    logger.log('deleting fav', res.data)
    AppState.favorites = AppState.favorites.filter(f => f.id != favoriteId)
  }
}


export const FavoritesService = new favoritesService