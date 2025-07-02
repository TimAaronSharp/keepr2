<script setup>
import { AppState } from '@/AppState.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref, watch } from 'vue';


const vault = computed(() => AppState.activeVault)

const editableVaultData = ref({
  name: "",
  description: "",
  img: "",
  isPrivate: false
})

watch(vault, () => {
  editableVaultData.value = {
    name: vault.value?.name,
    description: vault.value?.description,
    img: vault.value?.img,
    isPrivate: vault.value?.isPrivate
  }
})

async function editVault() {
  try {
    await vaultsService.edit(editableVaultData.value, vault.value.id)
  }
  catch (error) {
    Pop.error(error, `Could not edit vault: ${vault.value.name}`);
    logger.error(`Could not edit vault: ${vault.value.name}`.toUpperCase(), error)
  }
}
</script>


<template>
  <div class="modal fade" id="edit-vault-modal" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="d-flex justify-content-between">
          <h1 class="modal-title fs-2 my-4 ms-4"></h1>
          <button class="btn-close mt-3 me-3" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="editVault()">
            <div class="mb-3 d-flex flex-column fw-bold">
              <label for="edit-vault-name" class="form-label">Vault Name</label>
              <input v-model="editableVaultData.name" type="text" id="edit-vault-name" maxlength="40" required
                name="edit-vault-name-field">
            </div>
            <div class="mb-3 d-flex flex-column fw-bold">
              <label for="edit-vault-img" class="form-label">Vault Image URL</label>
              <input v-model="editableVaultData.img" type="url" id="edit-vault-img" name="edit-vault-img-field">
            </div>
            <div class="mb-3 d-flex flex-column fw-bold">
              <label for="edit-vault-description" class="form-label">Vault Description</label>
              <textarea v-model="editableVaultData.description" name="edit-vault-description-field"
                id="edit-vault-description" maxlength="1000" required></textarea>
            </div>
            <div class="mb-3 d-flex flex-column align-items-end">
              <label>Private vaults can only be seen by you.</label>
              <div class="d-flex align-items-center">
                <input v-model="editableVaultData.isPrivate" type="checkbox" class="form-check-input"
                  id="edit-vault-is-private">
                <label for="edit-vault-is-private">Make vault private?</label>
              </div>
            </div>
            <div class="d-flex justify-content-end">
              <button type="submit" class="btn btn-primary text-light fw-bold create-button" data-bs-dismiss="modal"
                aria-label="Save vault edit changes.">Save Changes</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>