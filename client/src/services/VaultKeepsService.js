import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService{
  // NOTE 🛠️ Create vaultKeep method.
  async create(vaultKeepData) {
    const res = await api.post('api/vaultKeeps', vaultKeepData)
    logger.log("VaultKeepsService.create returned ", res.data)
  }

}

export const vaultKeepsService = new VaultKeepsService()