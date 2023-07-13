instructionsCREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';



CREATE TABLE recipes(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(255) NOT NULL,
  instructions VARCHAR(255) NOT NULL,
  img VARCHAR(500) NOT NULL,
  category VARCHAR(255) NOT NULL, 
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';


CREATE TABLE ingredients(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(255) NOT NULL,
  quantity VARCHAR(255) NOT NULL DEFAULT "1",
  recipeId INT NOT NULL,

  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';


CREATE TABLE favorites(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,

  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO favorites
(accountId, recipeId)
VALUES("649c9ff5d2802c9ead11a127", LAST_INSERT_ID());
SELECT account FROM favorites JOIN accounts account on account.id = fav.accountId WHERE favorite.recipeId = 1;

SELECT accounts.*
FROM favorites
  JOIN accounts ON accounts.id = favorites.accountId 
  JOIN recipes ON recipes.id = favorites.recipeId

-- DROP TABLE favorites;





