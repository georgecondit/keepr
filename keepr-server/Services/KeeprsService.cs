using System;
using System.Collections.Generic;
using System.Linq;
using keeprapp.Models;
using keeprapp.Repositories;

namespace keeprapp.Services
{
    public class KeeprsService
    {
        private readonly KeeprsRepository _repo;
        private readonly VaultsRepository _vrepo;
        public KeeprsService(KeeprsRepository repo, VaultsRepository vrepo)
        {
            _repo = repo;
            _vrepo = vrepo;

        }

        public IEnumerable<Keepr> GetAll()
        {
            IEnumerable<Keepr> Keeps = _repo.GetAll();
            return Keeps;
        }

        internal Keepr GetById(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }


        public Keepr Create(Keepr newKeepr)
        {
            newKeepr.Id = _repo.Create(newKeepr);
            return newKeepr;
        }

        internal Keepr Edit(Keepr editData, string userId)
        {
            Keepr original = GetById(editData.Id);
            if (original.CreatorId != userId)
            {
                throw new Exception("Access Denied: Cannot Edit a Keep You did not Create");

            }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;
            editData.Img = editData.Img == null ? original.Img : editData.Img;

            return _repo.Edit(editData);

        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, Profile userInfo)
        {
            Vault original = _vrepo.Get(vaultId);
            if (original == null)
            {
                throw new Exception("Invalid Id");
            }
            if (original.IsPrivate == true && original.CreatorId != userInfo?.Id)
            {
                throw new Exception("This is private and you're not the owner!");
            }
            return _repo.GetKeepsByVaultId(vaultId);

        }

        internal string Delete(int id, string userId)
        {
            Keepr original = GetById(id);
            if (original.CreatorId != userId) 
            { throw new Exception("Access Denied: Cannot Delete a Keep You did not Create"); }
            _repo.Remove(id);
            return "successfully deleted";
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByProfileId(string profileId)
        {
            IEnumerable<VaultKeepViewModel> keeps = _repo.GetKeepsByProfileId(profileId);
            return keeps.ToList();
        }


        internal IEnumerable<VaultKeepViewModel> GetByAccountId(string id)
        {
            return _repo.GetKeepsByProfileId(id);
        }



    }
}