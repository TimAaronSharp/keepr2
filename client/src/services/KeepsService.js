import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Keep } from "@/models/Keep.js";


class KeepsService{
  async create(keepData) {
    const res = await api.post('api/keeps', keepData)
    logger.log("create returned ", res.data)
    this.makeKeeps(res.data)
  }
  // NOTE 💣 Delete keep request to the server.
  async delete(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log("delete returned ", res.data)
    this.unMakeKeep(keepId)
  }
  // NOTE 🧺 Get all keeps request to the server.
  async getAll(){
    AppState.keeps  = []
    // logger.log("AppState.keeps starts as ", AppState.keeps)
    const res = await api.get('api/keeps')
    this.makeKeeps(res.data)
    // logger.log("getAll returned ", res.data)
    // logger.log("res.data.map has created ", allKeeps)
    // logger.log("AppState.keeps is now ", AppState.keeps)
  }
  // NOTE 🔍 Get keep by id request to the server.
  async getById(keepId) {
    AppState.activeKeep = null
    // logger.log("AppState.keep starts as ", AppState.keep)
    const res = await api(`api/keeps/${keepId}`)
    // logger.log("getById returned ", res.data)
    this.makeKeeps(res.data)
    logger.log("AppState.keep is now ", AppState.activeKeep) 
  }
  // NOTE 🛠️ Constructs keeps from response data received from server.
  makeKeeps(keeps){
    if (Array.isArray(keeps)) {
      const createdKeeps = keeps.map(pojo => new Keep(pojo))
      return AppState.keeps = createdKeeps
    }
    const createdKeep = new Keep(keeps)
    return AppState.activeKeep = createdKeep
  }
  // NOTE ⛏️ Finds index of keep in AppState.keeps and responsively splices it out.
  unMakeKeep(keepId){
    const keepIndex = AppState.keeps.findIndex(keep => keep.id == keepId)
    AppState.keeps.splice(keepIndex, 1)
  }
}

export const keepsService = new KeepsService()