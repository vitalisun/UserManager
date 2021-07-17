CREATE SCHEMA `user_management_db` ;

use `user_management_db`;

CREATE TABLE `userinfos` (
  `ID` int NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Status` tinyint NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

insert into `userinfos` (ID, Name, Status)
values(1, 'Tom', 0);

insert into `userinfos` (ID, Name, Status)
values(2, 'Tim', 0);