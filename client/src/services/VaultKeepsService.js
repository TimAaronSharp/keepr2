import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { VaultKeepTracker } from "@/models/VaultKeepTracker.js"

class VaultKeepsService{
  // NOTE 🛠️ Create vaultKeep method.
  async create(vaultKeepData) {
    const res = await api.post('api/vaultKeeps', vaultKeepData)
    // logger.log("VaultKeepsService.create returned ", res.data)
    this.make(res.data)
    logger.log("VaultKeepsService.make() created ", AppState.vaultKeepTrackers)
  }

  // NOTE 🧺🔍 Get vaultKeepTrackers by vault id method.
  async getVaultKeepTrackersByVaultId(vaultId){
    AppState.vaultKeepTrackers = []
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    // logger.log("VaultKeepsService.getVaultKeepTrackersByVaultId returned ", res.data)
    this.make(res.data)
    logger.log("VaultKeepsService.make() created ", AppState.vaultKeepTrackers)
  }

  // NOTE ⚒️ Make new vaultKeepTracker class objects with res.data from server.
  async make(vaultKeepTrackers){
      return AppState.vaultKeepTrackers = vaultKeepTrackers.map(pojo => new VaultKeepTracker(pojo))
  }
}

export const vaultKeepsService = new VaultKeepsService()