<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


const keeps = computed(() => AppState.keeps)

onMounted(() => {
  getAllKeeps()
})

// NOTE 🧺 Gets all keeps from server
async function getAllKeeps() {
  try {
    await keepsService.getAll()
  }
  catch (error) {
    Pop.error(error, "Could not get all keeps");
    logger.error("Could not get all keeps".toUpperCase(), error)
  }
}

</script>

<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <div class="masonry-container mt-4">
          <div v-for="keep in keeps" :key="'keep ' + keep.id">
            <KeepCard :keepProp="keep" />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss"></style>
