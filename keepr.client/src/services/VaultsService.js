import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async getMyVaults() {
    const res = await api.get('account/vaults')
    AppState.myVaults = res.data
    logger.log(AppState.myVaults)
  }



  async getById(id) {
    const res = await api.get('api/vaults/' + id)
    logger.log('get vaults by id', res.data)
  }


}
export const vaultsService = new VaultsService()