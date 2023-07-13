<template>
  <div v-for="r in recipes" :key="r.id">
    <div class="card">
      <div class="row">
        <div class="col-4">
          <!-- <img :src="recipe.img" :alt="recipe.name"> -->
        </div>
        <div class="col-4">
          <div class="card">
            <!-- <h1>{{recipe.title}}</h1> -->
            {{ 'HELLO ABDUL' }}
          </div>gjhj
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";

export default {
  props: {
    // recipe: {type: Object, required:true}
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      recipe: computed(() => AppState.activeRecipe)
    }
  }
};
</script>


<style lang="scss" scoped></style>

