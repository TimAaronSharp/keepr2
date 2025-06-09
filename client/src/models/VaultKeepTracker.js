import { Keep } from "./Keep.js"

export class VaultKeepTracker extends Keep{
  constructor(data){
    super(data)
    this.vaultKeepId = data.vaultKeepId
  }
}