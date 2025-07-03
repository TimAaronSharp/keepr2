<script setup>
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';


const editableKeepData = ref({
  name: "",
  img: "",
  description: ""
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

</script>


<template>
  <section class="modal fade" id="create-keep-modal" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="d-flex justify-content-between">
          <h1 class="modal-title fs-2 my-4 ms-4" id="create-keep-backdrop-label">Create Your Keep</h1>
          <button type="button" class="btn-close mt-3 me-3 close-button" data-bs-dismiss="modal"
            aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep()">
            <div class="mb-3">
              <label for="keep-name" class="form-label fw-bold">Keep Name</label>
              <input v-model="editableKeepData.name" class="w-100" id="keep-name" type="text" placeholder="Name...">
            </div>
            <div class="mb-3">
              <label for="keep-img" class="form-label fw-bold">Keep Image URL</label>
              <input v-model="editableKeepData.img" class="w-100" id="keep-img" type="text" placeholder="Img URL...">
            </div>
            <div class="mb-3">
              <label for="keep-description" class="form-label fw-bold">Keep Description</label>
              <textarea v-model="editableKeepData.description" class="w-100" id="keep-description" type="text"
                placeholder="Description..."></textarea>
            </div>
            <button type="submit" class="btn btn-primary text-light fw-bold create-button"
              data-bs-dismiss="modal">Create Keep</button>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
textarea {
  height: 5rem;
}
</style>