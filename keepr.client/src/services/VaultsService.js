import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { vaultKeepsService } from "./VaultKeepsService"

class VaultsService {
  async getMyVaults() {
    const res = await api.get('account/vaults')
    AppState.myVaults = res.data
    logger.log(AppState.myVaults)
  }

  async getById(id) {
    const res = await api.get('api/vaults/' + id)
    // logger.log('get vaults by id', res.data)
    if (res.data.isPrivate && res.data.creatorId !== AppState.account.id) {
      return false
    } else {
      AppState.activeVault = res.data
      vaultKeepsService.getVaultKeeps(id)
      return true
    }
  }

  async deleteVault(id) {
    const res = await api.delete('api/vaults/' + id)
    // logger.log(res.data)
  }


}
export const vaultsService = new VaultsService()