import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Profile } from "@/models/Account.js"

class ProfilesService{
  async getProfile(profileId) {
    AppState.profile = null
    // logger.log("AppState.profile starts out ", AppState.profile)
    const res = await api.get(`api/profiles/${profileId}`)
    // logger.log("get returned ", res.data)
    const profile = new Profile(res.data)
    // logger.log("res.data is now a Profile ", profile)
    AppState.profile = profile
    // logger.log("AppState.profile is now ", AppState.profile)
  }
}

export const profilesService = new ProfilesService()