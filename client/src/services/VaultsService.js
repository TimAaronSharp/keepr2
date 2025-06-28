import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Vault } from "@/models/Vault.js"

class VaultsService{
    
  // NOTE 🛠️ Create (post) vault request to server.
  async create(vaultData){
    const res = await api.post('api/vaults', vaultData)
    logger.log("VaultsService.create() returned ", res.data)
    this.make(res.data)
  }

  // NOTE 💣 Delete (delete) vault request to server.
  async delete(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`)
    logger.log("VaultsService.delete() returned ", res.data)
    this.unMake(vaultId)
  }

  async edit(editedVaultData, vaultId) {
    const res = await api.put(`api/vaults/${vaultId}`, editedVaultData)
    logger.log("VaultsService.edit() returned ", res.data)
    this.make(res.data)
  }
  
  // NOTE 🔍 Get by id (get) vault request to server.
  async getById(vaultId) {
    AppState.activeVault = null
    const res = await api.get(`api/vaults/${vaultId}`)
    // logger.log("VaultsService.getById returned ", res.data)
    this.make(res.data)
  }
  // NOTE Look into making IsPrivateCheck() on the server versatile to use amongst all methods, like your make/unMake methods. (Find a way to check if someone is a List of something). |server| - VaultsService.GetByProfileId() does a findAll to only return vaults that are not private (in the event that logged in user is not the profile owner). Isn't functionally necessary but I would like to for separation of concerns, as well as for more learning experience.

  // NOTE 🔍📄 Get by profile id (get) vault request to server.
  async getByProfileId(profileId) {
    AppState.vaults = []
    // logger.log("AppState.vaults starts as ", AppState.vaults)
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    // logger.log("getByProfileId returned ", res.data)
    this.make(res.data)
  }

  // NOTE ⚒️ Make new Vault class objects with res.data from server and assigns to AppState.vaults/activeVault (For responsive creation, as well as get methods).
  make(vaults){
    if(Array.isArray(vaults)){
      const createdVaults = vaults.map(pojo => new Vault(pojo))
      // logger.log("AppState.vaults is now ", AppState.vaults)
      return AppState.vaults = createdVaults
    }
    const createdVault = new Vault(vaults)
    // logger.log("AppState.activeVault is now ", AppState.activeVault)
    return AppState.activeVault = createdVault
  }
  
  // NOTE ⛏️ Unmakes vault for responsive deletion (finds vault in AppState.vaults and splices out upon vault deletion).
  unMake(vaultId){
    const vaultIndex = AppState.vaults.findIndex(vault => vault.id == vaultId)
    AppState.vaults.splice(vaultIndex, 1)
  }
}

export const vaultsService = new VaultsService()