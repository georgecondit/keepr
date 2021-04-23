using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeprapp.Models;

namespace keeprapp.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Vault> Get()
        {
            string sql = @"
      SELECT 
      vault.*,
      profile.*
      FROM vaults vault
      JOIN profiles profile ON vault.creatorId = profile.id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, creator) =>
            {
                vault.Creator = creator;
                return vault;
            }, splitOn: "id");
        }

        internal Vault Get(int id)
        {
            string sql = @"
      SELECT 
      vault.*,
      profile.*
      FROM vaults vault
      JOIN profiles profile ON vault.creatorId = profile.id
      WHERE vault.id = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, creator) =>
            {
                vault.Creator = creator;
                return vault;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal int CreateVault(Vault newVault)
        {
            string sql = @"
      INSERT INTO vaults
      (name, description, isprivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newVault);
        }

        internal IEnumerable<Vault> GetKeeprByAccountId(string id)
        {
            string sql = @"
      SELECT 
      vault.*,
      profile.*
      FROM vaults vault
      JOIN profiles profile ON vault.creatorId = profile.id
      WHERE vault.keeprId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, creator) =>
            {
                vault.Creator = creator;
                return vault;
            }, new { id }, splitOn: "id");
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            string sql = @"
      SELECT 
      keep.*,
      vk.id AS VaultKeepId,
      pr.*
      FROM vaultkeep vk
      JOIN keeps keep ON vk.keepId = keep.id
      JOIN profiles pr ON pr.id = keep.creatorId
      WHERE vk.vaultId = @id;";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id");
        }

        internal IEnumerable<Vault> GetVaultsByProfileId(string id)
        {
            string sql = @"
      SELECT 
      vault.*,
      profile.*
      FROM vaults vault
      JOIN profiles profile ON vault.creatorId = profile.id
      WHERE vault.creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, creator) =>
            {
                vault.Creator = creator;
                return vault;
            }, new { id }, splitOn: "id");
        }


        internal Vault Edit(Vault editData)
        {
            string sql = @"
      UPDATE vaults
      SET
      name = @Name, 
      description = @Description,
      isprivate = @IsPrivate
      WHERE id = @Id;";
            _db.Execute(sql, editData);
            return editData;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }

}
