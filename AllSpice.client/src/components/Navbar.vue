<template>
  <!-- <Login /> -->
  <div></div>
      <!-- <ul class="navbar-nav me-auto">
        <li>
          <button class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#modal">Add Recipe</button>
        </li>
      </ul> -->
    
</template>

<script>
import { computed } from "vue";
// import Login from './Login.vue';
import { AppState } from "../AppState.js";
export default {
  setup() {
    return {
      user: computed(() => AppState.user)
    }
  },
  // components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
