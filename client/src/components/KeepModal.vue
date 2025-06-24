<script setup>
import { AppState } from '@/AppState.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, ref } from 'vue';

const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const vaults = computed(() => AppState.vaults)


const selectedVault = ref({
  vaultId: 0
})

async function saveKeepToVault() {
  try {
    const vaultKeepData = {
      keepId: keep.value.id,
      vaultId: selectedVault.value.vaultId,
      creatorId: account.value.id
    }
    await vaultKeepsService.create(vaultKeepData)
    selectedVault.value.vaultId = 0
  }
  catch (error) {
    Pop.error(error, `Could not save keep: ${keep.value.name}, id: ${keep.value.id} to vault`);
    logger.error(`Could not save keep: ${keep.value.name}, id: ${keep.value.id} to vault`, error)
  }
}

</script>

<!-- NOTE CHANGE MOBILE FORMAT TO HAVE PIC ON TOP. COL-12'S OR MEDIA QUERIES. ALSO, MOBILE MODAL SHOULD BE FULL SCREEN (MODAL SIZE EXTRA LARGE OR SOMETHING?) -->
<template>
  <!-- Modal -->
  <div class="modal fade" id="keeps-modal" data-bs-keyboard="false" tabindex="-1" aria-labelledby="keep-modal-label"
    aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content">
        <div class="container">
          <div class="row">
            <div class="col-12 col-md-6 p-0">
              <img class="img-fluid w-100 modal-img modal-img-corners" :src="keep?.img"
                :alt="`A picture for the ${keep?.name} keep by ${keep?.creator.name}`"
                :title="`A picture for the keep titled ${keep?.name} created by ${keep?.creator.name}`">
            </div>
            <div class="col-12 col-md-6">
              <div class="d-flex h-100 flex-column justify-content-between">
                <div class="text-center mt-3">
                  <span class="mdi mdi-eye-outline">{{ keep?.views }}</span>
                  <span class="mdi mdi-alpha-k-box-outline">{{ keep?.kept }}</span>
                </div>
                <div class="text-center">
                  <p class="modal-title fs-5 fw-bold mb-4" id="keeps-modal-label">{{ keep?.name }}</p>
                  <p>{{ keep?.description }}</p>
                </div>
                <div class="d-flex justify-content-between mb-2 w-100">
                  <div v-if="account">
                    <form @submit.prevent="saveKeepToVault()">
                      <select v-model="selectedVault.vaultId" class="fw-bold" name="vault-select" id="vault-select">
                        <option disabled value="0">Select a Vault</option>
                        <option v-for="vault in vaults" :key="`vault option ` + vault.id" :value="vault.id">{{
                          vault.name }}</option>
                      </select>
                      <button type="submit" class="btn save-button fw-bold ms-3 pt-0 px-2 pb-1">Save</button>
                    </form>
                  </div>
                  <RouterLink v-if="keep" :to="{ name: 'Profile Page', params: { profileId: keep?.creatorId } }">
                    <img data-bs-dismiss="modal" class="profile-thumbnail" :src="keep?.creator.picture"
                      :alt="`Picture of ${keep?.creator.name}, creator of this keep`"
                      :title="`Picture of ${keep?.creator.name}, creator of this keep`">
                  </RouterLink>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.save-button {
  outline: 1px solid black;
}
</style>