<script setup>
    import HomeView from './views/HomeView.vue'
</script>

<template>
  <div>
    <div class="wrapper">
      <HomeView/>
    </div>
  </div>
</template>

<style scoped>
* {
  margin: 0;
  padding: 0;
  width: 100%;
}
header {
  line-height: 1.5;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
