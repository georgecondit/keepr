using System;
using System.Collections.Generic;
using System.Linq;
using keeprapp.Models;
using keeprapp.Repositories;
using Microsoft.AspNetCore.Http;

namespace keeprapp.Services
{
    public class VaultKeepService
    {
        private readonly VaultKeepRepository _vkrepo;
        private readonly KeeprsRepository _krepo;
        private readonly VaultsRepository _vrepo;


        public VaultKeepService(VaultKeepRepository vkrepo, KeeprsRepository krepo, VaultsRepository vrepo)
        {
            _vkrepo = vkrepo;
            _krepo = krepo;
            _vrepo = vrepo;

        }

        internal IEnumerable<VaultKeepViewModel> Get(int vaultId)
        {
            Vault original = _vrepo.Get(vaultId);
            if (original == null)
            {
                throw new Exception("Inalid Vault Id!");
            }
            if (original.IsPrivate == false)
            {
                IEnumerable<VaultKeepViewModel> VaultKeeps = _krepo.GetKeepsByVaultId(vaultId);
                return VaultKeeps;
            }
            else
            {
                throw new UnauthorizedAccessException("You don't have access to this vault");
            }
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
        {
            IEnumerable<VaultKeepViewModel> vaultKeeps = _krepo.GetKeepsByVaultId(vaultId);
            if (vaultKeeps == null)
            {
                throw new Exception("Invalid Id");
            }
            //NOTE Good idea to get vault first and see if it's private
            return vaultKeeps.ToList();
        }


        internal VaultKeep Create(VaultKeep newVK)
        {

            return _vkrepo.Create(newVK);
        }


        internal string Delete(int id, string userId)
        {
            VaultKeepViewModel original = _vkrepo.GetById(id);
            if (original == null)
            {
                throw new Exception("Invalid Id");
            }
            if (original.CreatorId != userId)
            {
                throw new Exception("You cannot delete something you did not create");
            }
            _vkrepo.Remove(id);
            return "Successsfully Deleted";
        }

        private VaultKeepViewModel GetById(int id)
        {
            VaultKeepViewModel data = _vkrepo.GetById(id);
            if (data == null)
            {
                throw new Exception("Inavalid Id");
            }
            return data;

        }
    }
}