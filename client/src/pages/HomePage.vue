<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted, ref } from 'vue';


const keeps = computed(() => AppState.keeps)

const editableKeepData = ref({
  name: "",
  img: "",
  description: ""
})

onMounted(() => {
  getAllKeeps()
})

async function createKeep() {
  try {
    await keepsService.create(editableKeepData.value)
  }
  catch (error) {
    Pop.error(error, "Could not create keep");
    logger.error("Could not create keep".toUpperCase(), error)
  }
}

// NOTE 🧺 Get all keeps request to the server
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
  <div>
    <form @submit.prevent="createKeep()">
      <div class="mb-3">
        <label for="keep-name" class="form-label"></label>
        <input v-model="editableKeepData.name" id="keep-name" type="text" placeholder="Name...">
      </div>
      <div class="mb-3">
        <label for="keep-img" class="form-label"></label>
        <input v-model="editableKeepData.img" id="keep-img" type="text" placeholder="Img URL...">
      </div>
      <div class="mb-3">
        <label for="keep-description" class="form-label"></label>
        <textarea v-model="editableKeepData.description" id="keep-description" type="text"
          placeholder="Description..."></textarea>
      </div>
      <button type="submit">Create</button>
    </form>
  </div>
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
