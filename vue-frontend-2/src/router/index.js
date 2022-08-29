import Vue from 'vue'
import Router from 'vue-router'
import auth from '@/services/auth.service'

Vue.use(Router)

export const routes = [{
  path: '/',
  redirect: '/home'
}, {
  path: '/home',
  name: 'home',
  component: () => import(/* webpackChunkName: "home" */ '@/views/Home.vue')
}, {
  path: '/secure',
  name: 'secure',
  component: () => import(/* webpackChunkName: "secure" */ '@/views/Secure.vue'),
  meta: {
    authName: auth.authName
  }
}, {
  path: '*',
  alias: '/home'
}]

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL || '/',
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) return savedPosition

    return { x: 0, y: 0 }
  },
  routes
})

/**
 * Before each route update
 */
// router.beforeEach((to, from, next) => {
//   // If auth required, check login. If login fails redirect to login page
//   if (to.meta.authRequired) {
//     if (!auth.isAuthenticated()) {
//       router.push({ name: 'login', query: { to: to.path } })
//     }
//   }

//   // If route name is login & auth authenticated, redirect to dashboard page
//   if (to.name === 'login') {
//     if (auth.isAuthenticated()) {
//       router.push('/').catch(() => {})
//     }
//   }

//   return next()
// })

/**
 * After each route update
 */
// router.afterEach(() => {
// })

auth.useRouter(router)

export default router
