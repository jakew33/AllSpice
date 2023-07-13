<template>
  
  <!-- <header class="card elevation-5 align-content-center w-100% header-photo object-fit-cover">
          <div class="position-absolute bottom-50 start-50 translate-middle fs-5">
          <h1 style="font-size:5em; color: white;" class="">Allspice</h1>
      </div>
  </header> -->
  
  <router-view />
  <main>
    <!-- <Navbar /> -->
  </main>

  <Modal id="modal">
    <CreateRecipeForm />
  </Modal>

  <Modal id="activeRecipe">
    <ActiveRecipe />
  </Modal>

  

</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
// import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  // components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}


</style>
