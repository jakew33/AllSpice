<template>
  <div class="bg-color">
    
    <ul class="my-3">
    <li>
      <button class="btn btn-secondary rounded-circle text-white create" data-bs-toggle="modal" data-bs-target="#modal">+</button>
    </li>
  </ul>
  
<section style="position: relative;">
  <div class="elevation-5 login bg-none">
    <Login />
  </div>

  <div class="searchbar">
    <SearchBar />
  </div>

  <div style="display: flex; justify-content: center;">
    <img class="img-fluid elevation-5 mx-5 my-5 shadow" alt="logo" src="../assets/img/Header.png" style="width: 100%;"/>
    <h1 class="headerText text-white">All-Spice</h1>
    <p class="headerText2 text-white">Cherish Your Family</p>
    <p class="headerText3 text-white">And Their Cooking</p>
  </div>

  <section class="row justify-content-center ">
    <div class="col-8">
  <div class=" d-flex justify-content-around align-items-center my-3 rounded card elevation-5 col-4 filter bg-color ">
  <h1 @click="filterBy = ''" class=" btn w-15 mx-1 bg-green ">Home</h1>
    <h1 @click="filterBy = 'myRecipes'" class=" btn w-15 mx-1 bg-green">My Recipes</h1>
    <h1 @click="filterBy = 'favorites'" class=" btn w-15 mx-1 bg-green">Favorites</h1>
    </div>
    </div>
  </section>
</section>

  <div class="container-fluid justify-content-start ">
    <section class="row">
      <div class="col-md-3 my-3 p-4 " v-for="r in recipes" :key="r.id">
        <RecipeCard :recipe="r" />
      </div>
    </section>
  </div>
  </div>
  <ActiveRecipe />
</template>

<script>
import { computed, onMounted, ref } from "vue"
import { recipesService } from "../services/RecipesService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { AppState } from "../AppState.js"
import SearchBar from '../components/SearchBar.vue'
import Login from '../components/Login.vue'
import { useRoute } from "vue-router"
import { FavoritesService } from "../services/FavoritesService.js"
export default {
  components: { Login, SearchBar },

  setup() {
    const route = useRoute()
    const filterBy = ref('')

    async function getAllRecipes() {
      try {
        logger.log('gettin recipes')
        await recipesService.getAllRecipes()
      } catch (error) {
        Pop.error(error.message)
        logger.log(error)
      }
    }

    async function getFavsByRecipeId() {
      try {
        const recipeId = route.params.id
        await FavoritesService.getFavsByRecipeId(recipeId)
      } catch (error) {
        logger.error(error)
        Pop.toast(error.messgae, 'error')
      }
    }

    onMounted(() => {
      getAllRecipes()
      getFavsByRecipeId()
    })

    return {

      filterBy,
      appState: computed(() => AppState),
      myFavorites: computed(() => AppState.myFavorites),
      recipes: computed(() => {

        if (filterBy.value == "") {
          return AppState.recipes
        } else{
          return AppState.recipes.filter(r => r.category == AppState.account.id)
        }
      })
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
.login{
  position: absolute;
  top: 28%; 
  right: 6%; 
  transform: translate(100%, -150%); 
  z-index: 1;
}

.searchbar{
  position: absolute; 
  top: 23%; 
  right: 19%; 
  transform: translate(100%, -150%); 
  z-index: 1
}

.filter{
  position: absolute; 
  top: 115%; 
  right: 66%; 
  transform: translate(100%, -150%); 
  z-index: 1;
  color:#219653;
  background-color: rgba(64, 63, 63, 0.358);
    backdrop-filter: blur(10px);
}

.headerText{
  position: absolute; 
  top: 45%; 
  right: 59%; 
  transform: translate(100%, -150%); 
  z-index: 1;
  font-size: 80px;
  text-shadow: 100px;
}

.headerText2{
  position: absolute; 
  top: 48%; 
  right: 57%; 
  transform: translate(100%, -150%); 
  z-index: 1;
  font-size: 30px
}

.headerText3{
  position: absolute; 
  top: 55%; 
  right: 57%; 
  transform: translate(100%, -150%); 
  z-index: 1;
  font-size: 30px
}

.bg-color{
  background-color: rgb(255, 255, 255);
}

.bg-green{
  background-color: #fffdfd ;
  color:  #527360;
  font-size: 20px
}

.btn-secondary{
  background-color: #527360;
}

// .create{
//   font-size: 50px
// }

//  .shadow{
//   box-shadow: 50px 500px;
// }
</style>
