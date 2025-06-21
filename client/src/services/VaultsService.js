import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Vault } from "@/models/Vault.js"

class VaultsService{
  // NOTE Look into making IsPrivateCheck() on the server versatile to use amongst all methods, like your make/unMake methods. (Find a way to check if someone is a List of something)
  async getByProfileId(profileId) {
    AppState.vaults = []
    logger.log("AppState.vaults starts as ", AppState.vaults)
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log("getByProfileId returned ", res.data)
    this.makeVaults(res.data)
  }
  makeVaults(vaults){
    if(Array.isArray(vaults)){
      const createdVaults = vaults.map(pojo => new Vault(pojo))
      logger.log("AppState.vaults is now ", AppState.vaults)
      return AppState.vaults = createdVaults
    }
    const createdVault = new Vault(vaults)
    logger.log("AppState.activeVault is now ", AppState.activeVault)
    return AppState.activeVault = createdVault
  }
}

export const vaultsService = new VaultsService()