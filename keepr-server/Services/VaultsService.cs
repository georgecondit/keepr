using System;
using System.Collections.Generic;
using System.Linq;
using keeprapp.Models;
using keeprapp.Repositories;

namespace keeprapp.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vrepo;
        private readonly KeeprsRepository _krepo;

        public VaultsService(VaultsRepository vrepo, KeeprsRepository krepo)
        {
            _vrepo = vrepo;
            _krepo = krepo;
        }
        internal Vault Get(int vaultId, string userId)
        {
            Vault original = _vrepo.Get(vaultId);
            if (original == null)
            {
                throw new Exception("Invalid Id");
            }
            if (original.IsPrivate == true && original.CreatorId != userId)
            {
                throw new Exception("You don't have access to this vault.");
            }
            else
            {
                return original;
            }
        }

        internal Vault CreateVault(Vault newVault)
        {
            newVault.Id = _vrepo.CreateVault(newVault);
            return newVault;
        }

        internal object GetVaultByAccountId(string id)
        {
            IEnumerable<Vault> Vaults = _vrepo.GetKeeprByAccountId(id);
            return Vaults;
        }

        internal IEnumerable<Vault> GetByProfileId(string profileId)
        {

            IEnumerable<Vault> Vaults = _vrepo.GetVaultsByProfileId(profileId);
            return Vaults.ToList().FindAll(r => !r.IsPrivate);
        }

        internal Vault Edit(Vault editData, string userId)
        {
            Vault original = Get(editData.Id, userId);
            if (original.CreatorId != userId)
            {
                throw new Exception("You cannot edit something you did not create");
            }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;

            return _vrepo.Edit(editData);
        }

        internal object GetVaultsByProfileId(Profile userInfo, string profileId)
        {
            throw new NotImplementedException();
        }

        internal string Delete(int id, string userId)
        {
            Vault original = Get(id, userId);
            if (original.CreatorId != userId)
            {
                throw new Exception("You cannot delete something you did not create");
            }
            _vrepo.Remove(id);
            return "Successsfully Deleted";
        }

    }
}