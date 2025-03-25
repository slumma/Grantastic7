DECLARE @searchTerm NVARCHAR(200) = '%project%';

SELECT 'Users' AS TableName, 'Username' AS ColumnName, Username AS FoundValue FROM users WHERE Username LIKE @searchTerm
UNION
SELECT 'Users', 'Email', Email FROM users WHERE Email LIKE @searchTerm
UNION
SELECT 'Users', 'FirstName', FirstName FROM users WHERE FirstName LIKE @searchTerm
UNION
SELECT 'Users', 'LastName', LastName FROM users WHERE LastName LIKE @searchTerm
UNION
SELECT 'Users', 'Phone', Phone FROM users WHERE Phone LIKE @searchTerm
UNION
SELECT 'Users', 'HomeAddress', HomeAddress FROM users WHERE HomeAddress LIKE @searchTerm
UNION
SELECT 'Projects', 'ProjectName', ProjectName FROM project WHERE ProjectName LIKE @searchTerm
UNION
SELECT 'Grants', 'GrantName', GrantName FROM grants WHERE GrantName LIKE @searchTerm
UNION
SELECT 'Grants', 'Descriptions', CAST(descriptions AS NVARCHAR(MAX)) FROM grants WHERE CAST(descriptions AS NVARCHAR(MAX)) LIKE @searchTerm
UNION
SELECT 'Grants', 'StatusName', StatusName FROM grants WHERE StatusName LIKE @searchTerm
UNION
SELECT 'Grants', 'Category', Category FROM grants WHERE Category LIKE @searchTerm
UNION
SELECT 'Grant Suppliers', 'SupplierName', SupplierName FROM grantSupplier WHERE SupplierName LIKE @searchTerm
UNION
SELECT 'Grant Suppliers', 'OrgType', OrgType FROM grantSupplier WHERE OrgType LIKE @searchTerm
UNION
SELECT 'Grant Suppliers', 'BusinessAddress', BusinessAddress FROM grantSupplier WHERE BusinessAddress LIKE @searchTerm
UNION
SELECT 'Business Prep', 'CommunicationStatus', CommunicationStatus FROM BPrep WHERE CommunicationStatus LIKE @searchTerm
UNION
SELECT 'Tasks', 'Objective', Objective FROM task WHERE Objective LIKE @searchTerm
UNION
SELECT 'Meetings', 'Purpose', Purpose FROM meeting WHERE Purpose LIKE @searchTerm
UNION
SELECT 'Project Notes', 'Content', CAST(Content AS NVARCHAR(MAX)) FROM projectNotes WHERE CAST(Content AS NVARCHAR(MAX)) LIKE @searchTerm
UNION
SELECT 'Grant Notes', 'Content', CAST(Content AS NVARCHAR(MAX)) FROM grantNotes WHERE CAST(Content AS NVARCHAR(MAX)) LIKE @searchTerm
UNION
SELECT 'User Messages', 'SubjectTitle', SubjectTitle FROM userMessage WHERE SubjectTitle LIKE @searchTerm
UNION
SELECT 'User Messages', 'Contents', Contents FROM userMessage WHERE Contents LIKE @searchTerm;
