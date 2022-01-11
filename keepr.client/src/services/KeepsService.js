import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService {

  async getAll() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
    // logger.log(AppState.keeps)
  }

  async getByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    // logger.log(res.data)
    AppState.keeps = res.data
  }

  async getById(id) {
    const res = await api.get('api/keeps/' + id)
    // logger.log(res.data)
    AppState.activeKeep = res.data
  }

  async createKeep(data) {
    const res = await api.post('api/keeps', data)
    AppState.keeps.push(res.data)
    // logger.log(res.data)
  }

  async deleteKeep(id) {
    const res = await api.delete('api/keeps/' + id)
    // logger.log(res.data)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  }

}

export const keepsService = new KeepsService();