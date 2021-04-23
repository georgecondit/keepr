import { api } from './AxiosService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { Keepr } from '../Models/KeeprModel'

export default class KeeprsService {
  async get() {
    try {
      const res = await api.get('api/keeps')
      logger.log('ANything', res.data)
      AppState.keeps = res.data.map(k => new Keepr(k))
    } catch (error) {
      logger.error('This is the error', error)
    }
  }

  async getById(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      AppState.keepr = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepsByVaultId(id) {
    try {
      const res = await api.get('api/vaults/' + id + '/keeps')
      AppState.keepr.vaults = res.data
      AppState.vault = res.data
      logger.log(res.data)
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepsByProfileId(id) {
    try {
      const res = await api.get('profile/' + id + '/keeps')
      AppState.keepr.profile = res.data
      AppState.profile = res.data
      logger.log(res.data)
    } catch (error) {
      logger.error(error)
    }
  }

  async createKeepr(keepr) {
    try {
      const res = await api.post('api/keeps/', keepr)
      AppState.keepr.push(res.data)
      return res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async edit(keepr) {
    try {
      const res = await api.put('api/keeps/' + keepr.id, keepr)
      this.getById(res.data._id)
    } catch (error) {
      logger.error(error)
    }
  }

  async delete(id) {
    const res = window.confirm('Are you sure you want to delete this Keepr')
    if (!res) {
      return
    }
    try {
      await api.delete('api/keeps/' + id)
      this.getById(id)
    } catch (error) {
      logger.error(error)
    }
  }
}

export const keeprsService = new KeeprsService()
