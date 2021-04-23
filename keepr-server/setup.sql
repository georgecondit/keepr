
USE keepr40;
SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE keeps;
SET FOREIGN_KEY_CHECKS = 1;


-- DROP TABLE profiles;
-- CREATE TABLE profiles
-- (
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(255),
--     email VARCHAR(255),
--     picture VARCHAR(255),

--     PRIMARY KEY (id)
-- );


-- ALTER TABLE keeps
-- ADD COLUMN img VARCHAR(255);

-- DROP TABLE keeps;

-- CREATE TABLE keeps
-- (
-- id INT NOT NULL AUTO_INCREMENT,
-- creatorId VARCHAR(255) NOT NULL,
-- name VARCHAR(255),
-- description VARCHAR(255),
-- img VARCHAR (255) NOT NULL,
-- views INT NOT NULL DEFAULT 0,
-- shared INT NOT NULL DEFAULT 0,
-- keeps INT NOT NULL DEFAULT 0,

-- PRIMARY KEY(id),

-- FOREIGN KEY (creatorId)
-- REFERENCES profiles (id)
-- ON DELETE CASCADE
-- );


-- CREATE TABLE vaults
-- (
--  id INT NOT NULL AUTO_INCREMENT,
--  creatorId VARCHAR(255) NOT NULL,
--  name VARCHAR(255) NOT NULL,
--  description VARCHAR(255) NOT NULL,
--  isPrivate TINYINT NOT NULL,

--  PRIMARY KEY (id),

--  FOREIGN KEY (creatorId)
--  REFERENCES profiles (id)
--  ON DELETE CASCADE

-- );
    -- DROP TABLE vaultkeep;
-- CREATE TABLE vaultkeep
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     keepId INT,
--     vaultId INT,
--     creatorId VARCHAR(255),
--     PRIMARY KEY(id),

--     FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE,

--     FOREIGN KEY (keepId)
--     REFERENCES keeps (id)
--     ON DELETE CASCADE,

--     FOREIGN KEY (vaultId)
--     REFERENCES vaults (id)
--     ON DELETE CASCADE

-- )
