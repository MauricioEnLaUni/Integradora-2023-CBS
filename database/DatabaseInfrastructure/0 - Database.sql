-- Database Infrastructure
CREATE DATABASE `fictichos_erp`
CHARSET utf8mb4
COLLATE utf8mb4_general_ci;
USE `fictichos_erp`;
SET NAMES 'utf8mb4';

-- Dummy table used for granting usage without
-- actually granting permissions to anything
-- within the database.
DROP TABLE IF EXISTS `null`;
CREATE TABLE `null`(
	`id` INT
) ENGINE = InnoDB;