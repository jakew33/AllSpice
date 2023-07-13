<template>
    <form @submit.prevent="searchRecipes()" action="">
        <input class="w-100 bg-none mdi mdi-magnify" type="text" v-model="search" placeholder="Search Categories..." />
    </form>
</template>

<script>
import { ref } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
export default {
    setup() {
        const search = ref('')
        return {
            search,

            async searchRecipes() {
                try {
                    const searchTerm = search.value
                    logger.log('searching recipes', searchTerm)
                    await recipesService.searchRecipes(searchTerm)
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            }
        };
    },
};
</script>

<style></style>