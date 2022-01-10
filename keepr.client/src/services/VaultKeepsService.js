import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {

  async getVaultKeeps(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    // logger.log('vaultkeeps', res.data)
    AppState.keeps = res.data
  }

}

export const vaultKeepsService = new VaultKeepsService()