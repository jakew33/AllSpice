<template>
    <div class="modal-content">
        <div class="modal-header">
            <h1 class="col-12 p-1 modal-title bg-green text-start text-white">New Recipe</h1>
            
        </div>
        <form @submit.prevent="createRecipe()">

    <div class="modal-body">
      <div class="form-floating mb-3">
        <input type="text" class="form-control" id="floatingInput" placeholder="Event Image" v-model="editable.img">
        <label for="floatingPassword">Recipe Image</label>
      </div>
      <div class="form-floating mb-3">
        <input type="text" class="form-control" id="floatingInput" placeholder="Title" v-model="editable.title">
        <label for="floatingPassword">Recipe Title</label>
      </div>
      <!-- <div class="form-floating mb-3">
        <input type="text" class="form-control" id="floatingInput" placeholder="Ingredients" v-model="editable.ingredients">
        <label for="floatingPassword">Ingredients</label>
      </div> -->
      <div class="form-floating mb-3">
        <input type="text" class="form-control" id="floatingInput" placeholder="Instructions" v-model="editable.instructions">
        <label for="floatingPassword">Instructions</label>
      </div>
      <div>
        <select name="type" id="type" v-model="editable.category">
        <option value="mexican">Mexican</option>
        <option value="italian">Italian</option>
        <option value="Asian">Asian</option>
        <option value="cheese">Cheese</option>
        <option value="Soup">Soup</option>
        <option value="specialty coffee">Specialty Coffee</option>
        <option value="Burgers">Burgers</option>
        <option value="Fish">Fish</option>
        <option value="Pasta">Pasta</option>
        </select>
      </div>
    </div>
    <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="submit" class="btn btn-secondary" data-bs-dismiss="modal">
          {{ recipe.id ? 'Edit Recipe' : 'Create Recipe'}}</button>
    </div>
  </form>
    </div>
</template>


<script>
import { recipesService } from "../services/RecipesService.js";
import Pop from "../utils/Pop.js";
import { ref } from "vue";
import { Modal } from 'bootstrap';
import { logger } from "../utils/Logger.js";
import { Recipe } from "../models/Recipe.js";

export default {
  props: {
    recipe: {
      type: Recipe,
      default: {}
    }
  },

  setup(props){
    const editable = ref({ ...props.recipe })
    
  return { 
    editable,

    async createRecipe() {
      try {
      const recipeData = editable.value
      await recipesService.createRecipe(recipeData)
      editable.value = {}
      Modal.getOrCreateInstance('#modal').hide()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    },

    async editRecipe() {
      try {
        const recipeData = editable.value
        await recipesService.editRecipe(recipeData)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    },

    handleSubmit(){
      if (props.recipe.id) {
        this.editRecipe
      }
      else {
        this.editRecipe()
      }
    }
    };
  },
};
</script>



<style>

.bg-green{
  background-color: #527360 ;
}

.btn-secondary{
  background-color: #527360;
}

</style>