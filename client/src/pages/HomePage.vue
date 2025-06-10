<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { computed } from 'vue';


const keeps = computed(() => AppState.keeps)

</script>

<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <div>
          <div v-for="keep in keeps" :key="'keep ' + keep.id">
            <KeepCard :keepProp="keep" />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss"></style>
