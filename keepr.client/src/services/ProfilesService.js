import { AppState } from "../AppState"
import { api } from "../services/AxiosService"
import { logger } from "../utils/Logger"

class ProfilesService {
  async getProfile(id) {
    const res = await api.get('api/profiles/' + id)
    AppState.profile = res.data
  }

  async getVaults(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.profileVaults = res.data
  }
}
export const profilesService = new ProfilesService()