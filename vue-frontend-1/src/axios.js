import axios from 'axios'
import auth from '@/services/auth.service'
// import router from './router'

// Set config defaults when creating the instance
const instance = axios.create({
  baseURL: process.env.VUE_APP_API_BASE_URL || '',
  headers: {
    'Accept': 'application/json',
    'Content-Type': 'application/json' 
  }
})
console.log(auth)

// request interceptor
instance.interceptors.request.use((config) => {
  console.log(auth)
  const accessToken = auth.accessToken
  if (accessToken) {
    config.headers['Authorization'] = `Bearer ${accessToken}`
  }
  return config
}, (error) => {
  return Promise.reject(error)
})

// response interceptor
instance.interceptors.response.use((response) => {
  return response
}, (error) => {
  if (error.response) {
    if (error.response.status === 401 || error.response.status === 403) {
      auth.signOut()
      // // Remove localStorage
      // localStorage.removeItem('accessToken')
      // localStorage.removeItem('userInfo')

      // // Navigate to login page
      // router.push({ name: 'login' })
    } else {
      let errMessage = error.response?.data?.title ?? ''
      if (!errMessage) {
        errMessage = `${error.response.statusText}.`
      }
      console.log('app/showError', `${error.response.status} ${errMessage}`)
    }
  }
  return Promise.reject(error)
})

export default instance
