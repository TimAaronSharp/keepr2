import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Keep } from "@/models/Keep.js";


class KeepsService{
  async getAll(){
    AppState.keeps  = []
    // logger.log("AppState.keeps starts as ", AppState.keeps)
    const res = await api.get('api/keeps')
    // logger.log("getAll returned ", res.data)
    const allKeeps = res.data.map(pojo => new Keep(pojo))
    // logger.log("res.data.map has created ", allKeeps)
    AppState.keeps = allKeeps
    // logger.log("AppState.keeps is now ", AppState.keeps)
    
  }
}

export const keepsService = new KeepsService()