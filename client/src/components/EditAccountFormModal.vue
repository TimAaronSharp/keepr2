<script setup>
import { AppState } from '@/AppState.js';
import { accountService } from '@/services/AccountService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref, watch } from 'vue';


const account = computed(() => AppState.account)

const editableAccountData = ref({
  name: account.value?.name,
  picture: account.value?.picture,
  coverImg: account.value?.coverImg
})

watch(account, () => {
  editableAccountData.value = {
    name: account.value?.name,
    picture: account.value?.picture,
    coverImg: account.value?.coverImg
  }
})

async function editAccount() {
  try {
    const confirmed = await Pop.confirm("Are you sure you want to save these changes to your account?")
    if (!confirmed) return

    await accountService.edit(editableAccountData.value)
  }
  catch (error) {
    Pop.error(error, "Could not edit account");
    logger.error("Could not edit account".toUpperCase(), error)
  }
}

</script>


<template>
  <section class="modal fade" id="edit-account-modal" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content bg-color">
        <div class="d-flex justify-content-between">
          <h1 class="modal-title bg-color fs-2 my-4 ms-4">Edit your account</h1>
          <button type="button" class="btn-close mt-3 me-3 close-button" data-bs-dismiss="modal"
            aria-label="Close"></button>
        </div>
        <div class="modal-body bg-color">
          <form @submit.prevent="editAccount()">
            <div class="mb-3">
              <label for="edit-account-name" class="form-label fw-bold">Account Name</label>
              <input v-model="editableAccountData.name" class="w-100" type="text" id="edit-account-name" maxlength="30"
                required>
            </div>
            <div class="mb-3">
              <label for="edit-account-picture" class="form-label fw-bold">Account Picture</label>
              <input v-model="editableAccountData.picture" class="w-100" type="url" id="edit-account-picture" required>
            </div>
            <div class="mb-3">
              <label for="edit-account-coverImg" class="form-label fw-bold">Account Cover image</label>
              <input v-model="editableAccountData.coverImg" class="w-100" type="url" id="edit-account-coverImg"
                required>
            </div>
            <div class="d-flex justify-content-end">
              <button type="submit" class="btn fw-bold create-button text-light" data-bs-dismiss="modal">Save</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped></style>