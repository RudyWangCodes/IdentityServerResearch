<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p>
      Link to
      <router-link :to="{ name: 'secure' }">Secure Page</router-link>
    </p>
  </div>
</template>

<script>
export default {
  props: {
    msg: String
  }
}
</script>
