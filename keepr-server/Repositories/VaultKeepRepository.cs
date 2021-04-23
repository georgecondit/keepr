using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeprapp.Models;


namespace keeprapp.Repositories
{
    public class VaultKeepRepository
    {
        private readonly IDbConnection _db;
        public VaultKeepRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep newVK)
        {
            string sql = @"
            INSERT INTO vaultkeep
            (keepId, vaultId, creatorId)
            VALUES
            (@KeepId, @VaultId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVK);
            newVK.Id = id;
            return newVK;
        }

        internal IEnumerable<VaultKeepViewModel> GetAll(int vaultId)
        {
            string sql = @"
            SELECT
            v.*,
            vk.id* AS VaultKeepId,
            pr.*
            JOIN keeps k ON vk.keepId = keep.id
            JOIN profiles pr ON pr.id = keep.creatorId
            WHERE vk.vaultId = @id;";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { vaultId }, splitOn: "id");

        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM vaultkeep WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }

        internal VaultKeepViewModel GetById(int vaultKeepId)
        {
            string sql = @"
                SELECT 
                k.*,
                vk.id AS VaultKeepId,
                pr.*
                FROM vaultkeep vk
                JOIN keeps k ON vk.keepId = k.id
                JOIN profiles pr ON pr.id = vk.creatorId
                WHERE vk.id = @vaultKeepId;";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { vaultKeepId }, splitOn: "id").FirstOrDefault();
        }
    }
}