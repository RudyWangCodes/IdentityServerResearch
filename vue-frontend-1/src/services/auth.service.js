// import axios from '../axios'
import Vue from 'vue'
import { createOidcAuth, SignInType, LogLevel } from 'vue-oidc-client/vue2'

const loc = window.location
const appRootUrl = `${loc.protocol}//${loc.host}${process.env.BASE_URL}`

// SignInType could be Window or Popup
const mainOidc = createOidcAuth(
  'main',
  SignInType.Window,
  appRootUrl,
  {
    authority: process.env.VUE_APP_AUTH_BASE_URL || '',
    client_id: 'frontend_1',
    response_type: 'code',
    scope: 'openid profile api',
    redirect_uri: `${appRootUrl}auth/signin`,
    post_logout_redirect_uri: `${appRootUrl}home`
  },
  console,
  LogLevel.Debug
)

// handle events
mainOidc.events.addAccessTokenExpiring(function () {
  // eslint-disable-next-line no-console
  console.log('access token expiring')
})

mainOidc.events.addAccessTokenExpired(function () {
  // eslint-disable-next-line no-console
  console.log('access token expired')
})

mainOidc.events.addSilentRenewError(function (err) {
  // eslint-disable-next-line no-console
  console.error('silent renew error', err)
})

mainOidc.events.addUserLoaded(function (user) {
  // eslint-disable-next-line no-console
  console.log('user loaded', user)
})

mainOidc.events.addUserUnloaded(function () {
  // eslint-disable-next-line no-console
  console.log('user unloaded')
})

mainOidc.events.addUserSignedOut(function () {
  // eslint-disable-next-line no-console
  console.log('user signed out')
})

mainOidc.events.addUserSessionChanged(function () {
  // eslint-disable-next-line no-console
  console.log('user session changed')
})

// a little something extra
Vue.prototype.$oidc = mainOidc

export default mainOidc
// export default {
//   login(username, password) {
//     return axios.post('/auth/login', {
//       username,
//       password
//     })
//   },

//   logout() {
//     return axios.post('/auth/logout')
//   },

//   isAuthenticated() {
//     return new Date(Date.now()) < new Date(localStorage.getItem('expToken') * 1000) &&
//       localStorage.getItem('accessToken')
//   }
// }
