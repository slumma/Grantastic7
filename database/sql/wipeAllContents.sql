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