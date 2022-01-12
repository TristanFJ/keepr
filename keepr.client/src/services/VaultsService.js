import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { vaultKeepsService } from "./VaultKeepsService"

class VaultsService {
  async getMyVaults() {
    const res = await api.get('account/vaults')
    AppState.myVaults = res.data
    // logger.log(AppState.myVaults)
  }

  async getById(id) {
    const res = await api.get('api/vaults/' + id)
    logger.log('get vaults by id', res.data)
    AppState.activeVault = res.data
    await vaultKeepsService.getVaultKeeps(id)
  }

  async deleteVault(id) {
    const res = await api.delete('api/vaults/' + id)
    // logger.log(res.data)
  }

  async addToVault(vaultId, keepId) {
    let data = { vaultId: vaultId, keepId: keepId }
    const res = await api.post('api/vaultkeeps', data)

  }

  async createVault(data) {
    const res = await api.post('api/vaults', data)
    // logger.log(res.data)
    AppState.myVaults.push(res.data)
  }

}
export const vaultsService = new VaultsService()