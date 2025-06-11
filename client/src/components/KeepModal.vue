<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';



const keep = computed(() => AppState.activeKeep)




</script>


<template>
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
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>