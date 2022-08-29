<template>
  <div class="hello">
    <h1>This page is secured by login page</h1>
    <div v-for="c in claims" :key="c.key">
      <strong>{{ c.key }}</strong>: {{ c.value }}
    </div>
    <br />
    <div>{{ weathers }} </div>
    <p>
      Link to
      <router-link :to="{ name: 'home' }">Home Page</router-link>
    </p>
    <p>
      <a href="#" @click.prevent="logout">Logout</a>
    </p>
  </div>
</template>

<script>
import api from '@/services/axios.service'

export default {
  data: () => ({
    weathers: []
  }),

  created: function () {
    this.getWeathers()
  },

  computed: {
    user() {
      return { ...this.$oidc.userProfile, accessToken: this.$oidc.accessToken }
    },
    claims() {
      if (this.user) {
        return Object.keys(this.user).map(key => ({
          key,
          value: this.user[key]
        }))
      }
      return []
    }
  },

  methods: {
    getWeathers() {
      api.getAll('weatherforecast')
        .then(response => {
          this.weathers = response.data
        })
    },
    logout() {
      this.$oidc.signOut()
    }
  }
}
</script>
