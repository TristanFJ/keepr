import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService {

  async getAll() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
    logger.log(AppState.keeps)
  }

}

export const keepsService = new KeepsService();