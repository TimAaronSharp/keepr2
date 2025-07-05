<script setup>
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { ref } from 'vue';


const editableVaultData = ref({
  name: "",
  img: "",
  description: "",
  isPrivate: false
})

async function createVault() {
  try {
    await vaultsService.create(editableVaultData.value)
    editableVaultData.value = {
      name: "",
      img: "",
      description: "",
      isPrivate: false
    }
  }
  catch (error) {
    Pop.error(error, "Could not create vault");
    logger.error("Could not create vault".toUpperCase(), error)
  }
}

</script>


<template>
  <section class="modal fade" id="create-vault-modal" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content bg-color">
        <div class="d-flex justify-content-between">
          <h1 class="modal-title bg-color fs-2 my-4 ms-4" id="create-vault-backdrop-label">Add your vault</h1>
          <button type="button" class="btn-close mt-3 me-3 close-button" data-bs-dismiss="modal"
            aria-label="Close"></button>
        </div>
        <div class="modal-body bg-color">
          <form @submit.prevent="createVault()">
            <div class="mb-3">
              <label for="vault-name" class="form-label"></label>
              <input v-model="editableVaultData.name" id="vault-name" type="text" placeholder="Name...">
            </div>
            <div class="mb-3">
              <label for="vault-img" class="form-label"></label>
              <input v-model="editableVaultData.img" id="vault-img" type="url" placeholder="Img URL...">
            </div>
            <div class="mb-3">
              <label for="vault-description" class="form-label"></label>
              <textarea v-model="editableVaultData.description" id="vault-description" type="text"
                placeholder="Description..."></textarea>
            </div>
            <div class="d-flex flex-column align-items-end mb-3">
              <div>
                <label>Private vaults can only be seen by you</label>
              </div>
              <div class="d-flex align-items-center">
                <input v-model="editableVaultData.isPrivate" type="checkbox" class="form-check-input"
                  id="is-private-checkbox">
                <label class="form-check-label fs-5 ms-1" for="is-private-checkbox">Make vault private?</label>
              </div>
            </div>
            <div class="d-flex justify-content-end">
              <button type="submit" class="btn btn-primary text-light fw-bold create-button" data-bs-dismiss="modal"
                aria-label="Create Vault">Create Vault</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped></style>