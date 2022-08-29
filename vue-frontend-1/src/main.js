import Vue from 'vue'
import App from './App.vue'

// Vue Router
import router from './router'

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
