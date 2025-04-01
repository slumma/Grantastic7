-- Disable foreign key checks temporarily to avoid constraint violations
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

-- Delete data from all tables in the correct order to avoid foreign key issues
DELETE FROM meetingMinutes;
DELETE FROM attendance;
DELETE FROM userMessage;
/*DELETE FROM grantStatus;
DELETE FROM supplierStatus;*/
DELETE FROM grants;
DELETE FROM projectNotes;
DELETE FROM grantNotes;
DELETE FROM projectTaskStaff;
DELETE FROM grantTaskStaff;
DELETE FROM grantStaff;
DELETE FROM projectTask;
DELETE FROM grantTask;
DELETE FROM projectStaff;
DELETE FROM meeting;
/*DELETE FROM faculty;
DELETE FROM nonfaculty;
DELETE FROM employee;*/
DELETE FROM BPrep;
DELETE FROM project;
DELETE FROM grantSupplier;
DELETE FROM users;

-- Re-enable foreign key checks
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';


--------------- DROPS ALL TABLES ----------------------------
-- Disable all foreign key constraints
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all";

-- Drop all foreign key constraints
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql = @sql + 'ALTER TABLE ' + t.name + ' DROP CONSTRAINT ' + fk.name + ';'
FROM sys.foreign_keys fk
JOIN sys.tables t ON fk.parent_object_id = t.object_id;

EXEC sp_executesql @sql;

-- Drop all tables
EXEC sp_MSforeachtable "DROP TABLE ?";

-- Re-enable all foreign key constraints (if any remain)
EXEC sp_MSforeachtable "ALTER TABLE ? CHECK CONSTRAINT all";

