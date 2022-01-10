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

}

export const keepsService = new KeepsService();