import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'
import { profilesService } from './ProfilesService.js'

class AccountService {
  async edit(editedAccountData) {
    const res = await api.put('/account', editedAccountData)
    logger.log("AccountService.edit returned ", res.data)
    // this.getAccount()
    profilesService.getProfile(res.data.id)
  }
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
