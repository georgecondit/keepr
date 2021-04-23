import { api } from './AxiosService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { Vault } from '../Models/VaultModel'

export default class VaultsService {
  async get() {
    try {
      const res = await api.get('api/vaults')
      const vaults = res.data.map(v => new Vault(v))
      AppState.vaults = vaults
      return vaults
    } catch (error) {
      logger.error(error)
    }
  }

  async getById(id) {
    try {
      const res = await api.get('api/vaults' + id)
      const vault = res.data
      AppState.vault = vault
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultsByProfileId(id) {
    try {
      const res = await api.get('profile/' + id + '/vaults')
      AppState.vault.profile = res.data
      AppState.profile = res.data
      logger.log(res.data)
    } catch (error) {
      logger.error(error)
    }
  }

  async createVault(vault) {
    try {
      const res = await api.vaults.post('api/vaults', vault)
      AppState.vaults.push(res.data)
      this.getAll()
      return res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async edit(vault) {
    try {
      const res = await api.put('api/vaults' + vault.id, vault)
      this.getById(res.data._id)
    } catch (error) {
      logger.error(error)
    }
  }

  async delete(id) {
    const res = window.confirm('Are you sure you want to delete this Vault')
    if (!res) {
      return
    }
    try {
      await api.delete('api/vaults' + id)
      this.getById(id)
    } catch (error) {
      logger.error(error)
    }
  }
}

export const vaultsService = new VaultsService()
