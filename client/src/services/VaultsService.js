import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Vault } from "@/models/Vault.js"

class VaultsService{
  // NOTE Look over service and create whatever missing methods you need to (create, etc.)
  async create(vaultData){
    const res = await api.post('api/vaults', vaultData)
    logger.log("VaultsService.create() returned ", res.data)
  }

  async getById(vaultId) {
    AppState.activeVault = null
    const res = await api.get(`api/vaults/${vaultId}`)
    // logger.log("VaultsService.getById returned ", res.data)
    this.makeVaults(res.data)
  }
  // NOTE Look into making IsPrivateCheck() on the server versatile to use amongst all methods, like your make/unMake methods. (Find a way to check if someone is a List of something). |server| - VaultsService.GetByProfileId() does a findAll to only return vaults that are not private (in the event that logged in user is not the profile owner). Isn't functionally necessary but I would like to for separation of concerns, as well as for more learning experience.
  async getByProfileId(profileId) {
    AppState.vaults = []
    // logger.log("AppState.vaults starts as ", AppState.vaults)
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    // logger.log("getByProfileId returned ", res.data)
    this.makeVaults(res.data)
  }
  makeVaults(vaults){
    if(Array.isArray(vaults)){
      const createdVaults = vaults.map(pojo => new Vault(pojo))
      // logger.log("AppState.vaults is now ", AppState.vaults)
      return AppState.vaults = createdVaults
    }
    const createdVault = new Vault(vaults)
    // logger.log("AppState.activeVault is now ", AppState.activeVault)
    return AppState.activeVault = createdVault
  }
}

export const vaultsService = new VaultsService()