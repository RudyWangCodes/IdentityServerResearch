import axios from '@/axios'

class AxiosService {
  // constructor() {
  //   this.service = axios.create({
  //     baseURL: process.env.VUE_APP_API_BASE_URL || '',
  //     headers: {
  //       'Accept': 'application/json',
  //       'Content-Type': 'application/json' 
  //     }
  //   })
  // }

  getAll(url, params) {
    return axios.get(`/${url}`, params)
  }

  getOne(url, id, config) {
    return axios.get(`/${url}/${id}`, config)
  }

  create(url, data, config) {
    return axios.post(`/${url}`, data, config)
  }

  update(url, id, data, config) {
    return axios.put(`/${url}/${id}`, data, config)
  }

  delete(endpoint, id, config) {
    return axios.delete(`/${endpoint}/${id}`, config)
  }
}

export default new AxiosService
