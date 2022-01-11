import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {

  async getVaultKeeps(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    // logger.log('vaultkeeps', res.data)
    AppState.keeps = res.data
  }

  async deleteVaultKeep(keepId) {
    const keep = AppState.keeps.find(k => k.id === keepId)
    let vaultKeepId = keep.vaultKeepId
    // logger.log(vaultKeepId)
    const res = await api.delete('api/vaultkeeps/' + vaultKeepId)
    logger.log(res.data)
    AppState.keeps = AppState.keeps.filter(k => k.vaultKeepId !== vaultKeepId)
  }

}

export const vaultKeepsService = new VaultKeepsService()