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
    <div class="modal-dialog modal-dialog-scrollable modal-xl modal-fullscreen-md-down modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-body container-fluid">
          <div class="row">
            <div class="col-12 col-md-6 p-0">
              <img class="img-fluid w-100 modal-img modal-img-corners h-100" :src="keep?.img"
                :alt="`A picture for the ${keep?.name} keep by ${keep?.creator.name}`"
                :title="`A picture for the keep titled ${keep?.name} created by ${keep?.creator.name}`">
            </div>
            <div class="col-12 col-md-6 pe-0">
              <div class="d-flex h-100 flex-column justify-content-between">
                <div class="mt-1 d-flex justify-content-center">
                  <span class="mdi mdi-eye-outline">{{ keep?.views }}</span>
                  <span class="mdi mdi-alpha-k-box-outline">{{ keep?.kept }}</span>
                  <button type="button" class="btn-close close-button position-absolute pt-0" data-bs-dismiss="modal"
                    aria-label="Close"></button>
                </div>
                <!-- NOTE Decide whether you still need to adjust keep modal stylings (view/keep count and close button, spacing/sizing at different screen sizes (maybe move that to polish?)) -->
                <div class="text-center">
                  <p class="modal-title fs-5 fw-bold mb-4 keep-mobile-space-between" id="keeps-modal-label">{{
                    keep?.name }}</p>
                  <p class="keep-mobile-space-between">{{ keep?.description }}</p>
                </div>
                <div class="d-flex justify-content-between mb-2 w-100 keep-mobile-space-between">
                  <div v-if="account">
                    <form @submit.prevent="saveKeepToVault()">
                      <select v-model="selectedVault.vaultId" class="fw-bold select-size" name="vault-select"
                        id="vault-select">
                        <option class="first-option" disabled value="0">Select a Vault</option>
                        <option v-for="vault in vaults" :key="`vault option ` + vault.id" :value="vault.id">{{
                          vault.name }}</option>
                      </select>
                      <button type="submit" class="btn save-button fw-bold ms-3 pt-0 px-2 pb-1">Save</button>
                    </form>
                  </div>
                  <RouterLink v-if="keep" :to="{ name: 'Profile Page', params: { profileId: keep?.creatorId } }"
                    class="margin-left-auto">
                    <img data-bs-dismiss="modal" class="profile-thumbnail modal-profile-thumbnail"
                      :src="keep?.creator.picture" :alt="`Picture of ${keep?.creator.name}, creator of this keep`"
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
.close-button {
  right: .2%;
}

.save-button {
  color: white;
  background-color: #877a8f;
  outline: 1px solid black;
}

.save-button:hover {
  // color: black;
  background-color: #2d2d2d;
}

.margin-left-auto {
  margin-left: auto;
}

.margin-left-counts {
  margin-left: 45%;
}

.modal-body {
  padding: 0 12px 0 12px;

  // NOTE Come back to this after figuring out the vault save select list width being too large (likely due to the length of the vault names. Might need to go with formatting the modal differently if ~30 char is still too long.)
  // @media screen AND (max-width: 992px) {
  //   .img-fluid {
  //     height: 100%;
  //     aspect-ratio: 1/1;
  //   }
  // }

  .test-img {
    @media screen AND (min-width: 767.99px) AND (max-width: 991.97px) {
      aspect-ratio: 1/1;
    }
  }

  @media screen AND (max-width: 767.98px) {
    padding-left: 12px;
    padding-right: 12px;
    padding-top: 0px;
  }
}

.keep-mobile-space-between {
  @media screen AND (max-width: 767.98px) {
    margin-top: 50px;
  }
}

select:focus {
  outline: 2px solid blue;
  /* Add a custom outline */
  box-shadow: 0 0 5px rgba(0, 0, 255, 0.5);
  /* Add a subtle shadow */
}

button:focus {
  outline: 2px solid blue;
  /* Add a custom outline */
  box-shadow: 0 0 5px rgba(0, 0, 255, 0.5);
  /* Add a subtle shadow */
}

input {
  outline: none;
}

.first-option {
  outline: none;
}

select:not(:placeholder-shown):valid {
  outline: none;
}

.modal-profile-thumbnail {
  margin-right: 8px;
}

select {
  outline: none;
  border: none;
  border-bottom: 1px solid #877a8f30;

  @media screen AND (min-width: 991.98px) AND (max-width: 1199.98px) {
    width: 75%;

  }

  @media screen AND (min-width: 767.99px) AND (max-width: 991.97px) {
    width: 50%;
  }
}

option {
  outline: none;
}
</style>