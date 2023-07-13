<template>
  <div class="card text-white recipe-card my3 bg rounded">
    <img class="rounded elevation-5" :src="recipe.img" :alt="recipe.name">
    <div class="card-img-overlay">
      <div class="d-flex justify-content-around p-1">
        <p class="label label-default favorite elevation-5 p-1 rounded">{{ recipe.category }}</p>
        <i class="mdi mdi-heart-outline text-end p-1 elevation-5 label label-default rounded favorite"></i>
      </div>
      <p class="text-center p-2 label label-default favorite elevation-5 rounded">{{ recipe.title }}</p>
    </div>
  </div>
  <div v-if="recipe.creatorId == account.id" class="d-flex justify-content-around">
    <button class="btn btn-dark bg-red elevation-5 text-white" @click="deleteRecipe()">Remove Recipe</button>
    <button data-bs-toggle="modal" data-bs-target="#modal" class="btn btn-dark bg-blue elevation-5">Edit Recipe</button>
    <button class="btn btn-dark bg-green elevation-5" type="button" @click="setActiveRecipe(recipe.id)">View
      Recipe</button>
  </div>
</template>

<script>
import { computed } from "vue";
// import { Recipe } from "../models/Recipe.js";
import { recipesService } from "../services/RecipesService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js";
import { Modal } from "bootstrap";
export default {
  props: {
    recipe: { type: Object, required: true }
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),

      async setActiveRecipe(recipeId) {
        try {
          await recipesService.getRecipeById(recipeId)
          Modal.getOrCreateInstance("#activeRecipe").show();
        } catch (error) {
          Pop.error(error)
        }
      },

      async deleteRecipe() {
        try {
          if (await Pop.confirm())
            await recipesService.deleteRecipe(props.recipe.id)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message);
        }
      },

    }
  }

}
</script>



<style lang="scss" scoped>
// img {
//     height: 25vh;
//     width: 100%;
//     object-fit: cover;
// }

.recipe-card {

  img {
    height: 35vh;
    width: 100%;
    object-fit: cover;
  }

  .mdi {
    color: rgb(255, 255, 255);
  }
}

.favorite {
  background-color: rgba(64, 63, 63, 0.358);
  backdrop-filter: blur(10px);
}

.bg-green {
  background-color: #527360;
  color: #ffffff;
}

.bg-red {
  background-color: #735252
}

.bg-blue {
  background-color: #525b73;
  color: #ffffff;
}
</style>