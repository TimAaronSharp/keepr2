<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';

const keep = computed(() => AppState.activeKeep)

const editableKeepData = ref({
  id: 0,
  name: "",
  description: ""
})

// NOTE 🧵 Edit keep request to the server.
// NOTE For some reason if I assign id the editableKeepData ref above it is undefined when I use editableKeepData.value.id in editKeep(), so I just assigned it in the function. It works as is, but will investigate why it wouldn't work before as I am unsure why that would be.
async function editKeep() {
  try {
    editableKeepData.value.id = keep.value.id
    await keepsService.edit(editableKeepData.value)
    editableKeepData.value = {
      id: 0,
      name: "",
      description: ""
    }


  }
  catch (error) {
    Pop.error(error, `Could not edit keep "${keep.value.name}"`);
    logger.error(`Could not edit keep "${keep.value.name}"`.toUpperCase(), error)
  }
}

</script>


<template>
  <div class="modal fade" id="edit-keep-modal" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="d-flex justify-content-between">
          <h1 class="modal-title fs-2 my-4 ms-4">Add your keep</h1>
          <button type="button" class="btn-close mt-3 me-3" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="editKeep()">
            <div class="mb-3">
              <label for="keep-name" class="form-label"></label>
              <input v-model="editableKeepData.name" type="text" id="keep-name" maxlength="255" required
                placeholder="Name..." name="keep-name-field">
            </div>
            <div class="mb-3">
              <label for="keep-description"></label>
              <textarea v-model="editableKeepData.description" name="keep-description-field" id="keep-description"
                placeholder="Description..." required></textarea>
            </div>
            <div class="d-flex justify-content-end">
              <button type="submit" class="btn btn-primary text-light fw-bold" data-bs-dismiss="modal">Save</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>