<script setup>
import EditKeepFormModal from './components/EditKeepFormModal.vue';
import EditVaultFormModal from './components/EditVaultFormModal.vue';
import KeepModal from './components/KeepModal.vue';
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer class=" text-center">
    Made with <i class="mdi mdi-heart text-pink"></i> by CodeWorks
  </footer>
  <KeepModal />
  <EditKeepFormModal />
  <EditVaultFormModal />
</template>

<style lang="scss"></style>
