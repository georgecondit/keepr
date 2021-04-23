
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeprapp.Models;

namespace keeprapp.Repositories
{
    public class KeeprsRepository
    {
        private readonly IDbConnection _db;

        public KeeprsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keepr> GetAll()
        {
            string sql = @"
       SELECT 
       keep.*,
       profile.*
       FROM keeps keep 
       JOIN profiles profile ON keep.creatorId = profile.id;";
            return _db.Query<Keepr, Profile, Keepr>(sql, (keep, creator) =>
            {
                keep.Creator = creator; return keep;
            }, splitOn: "id");
        }

        internal Keepr GetById(int id)
        {
            string sql = @"
            SELECT 
            keep.*,
            profile.* 
            FROM keeps keep 
            JOIN profiles profile ON keep.creatorId = profile.id
            WHERE keep.id = @id;";
            return _db.Query<Keepr, Profile, Keepr>(sql, (keepr, creator) =>
            {
                keepr.Creator = creator; return keepr;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }


        internal int Create(Keepr KeeprData)
        {
            string sql = @"
            INSERT INTO Keeps
            (CreatorId, name, description, img)
            VALUES
            (@CreatorId, @Name, @Description, @Img);
            SELECT LAST_INSERT_ID()";
            return _db.ExecuteScalar<int>(sql, KeeprData);
        }

        internal Keepr Edit(Keepr editData)
        {
            string sql = @"
            UPDATE keeps
            SET 
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id;";
            _db.Execute(sql, editData);
            return editData;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByProfileId(string profileId)
        {
            string sql = @"
            SELECT 
            keep.*,
            profile.*
            FROM keeps keep 
            JOIN profiles profile ON keep.creatorId = profile.id
            WHERE keep.creatorId = @profileId;";

            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { profileId }, splitOn: "id");
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id AS VaultKeepId,
            pr.*
            FROM vaultkeep vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN profiles pr ON pr.id = k.creatorId
            WHERE vaultId = @vaultId;";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { vaultId }, splitOn: "id");
        }


    }
}