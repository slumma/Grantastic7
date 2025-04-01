CREATE TABLE users(
    UserID int Identity(1,1) PRIMARY KEY,
    Username nvarchar(200),
	Password nvarchar(200),
    FirstName nvarchar(200),
    LastName nvarchar(200),
    Email nvarchar(200),
    Phone nvarchar(200),
    HomeAddress nvarchar(200),
	AdminStatus bit default 0,
	EmployeeStatus bit default 0,
	FacultyStatus bit default 0,
	NonFacultyStatus bit default 0);

CREATE TABLE grantSupplier(
    SupplierID int Identity(1,1) PRIMARY KEY,
    SupplierName nvarchar(200),
    OrgType nvarchar(200),
	SupplierStatus nvarchar(200), --temporary
    BusinessAddress nvarchar(200));

CREATE TABLE BPrep (
    UserID INT PRIMARY KEY,
    CommunicationStatus nvarchar(200),
    SupplierID int,
    FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID),
    FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE project(
    ProjectID int Identity(1,1) PRIMARY KEY,
    ProjectName nvarchar(200), 
    DueDate datetime);

CREATE TABLE projectStaff(
    ProjectStaffID int Identity(1,1) PRIMARY KEY, 
    ProjectID int, 
    UserID int, 
    Leader bit,
    Active bit,
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID),
    FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE projectTask(
    TaskID int Identity(1,1) PRIMARY KEY,
    ProjectID int, 
    DueDate date,
    Objective nvarchar(200),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE projectTaskStaff(
    ProjectTaskStaffID int Identity(1,1) PRIMARY KEY,
    TaskID int, 
    AssigneeID int,
	AssignerID int,
    DueDate date,
    FOREIGN KEY (TaskID) REFERENCES projectTask(TaskID),
    FOREIGN KEY (AssigneeID) REFERENCES users(UserID),
	FOREIGN KEY (AssignerID) REFERENCES users(UserID));

CREATE TABLE meeting(
    MeetingID int Identity(1,1) PRIMARY KEY,
    ProjectID int, 
    MeetingDate date,
    Purpose nvarchar(200),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE attendance(
    AttendanceID int Identity(1,1) PRIMARY KEY,
    MeetingID int,
    UserID int,
    FOREIGN KEY (MeetingID) REFERENCES meeting(MeetingID),
    FOREIGN KEY (UserID) REFERENCES users(UserID));
	--test
CREATE TABLE meetingMinutes(
    MinutesID int Identity(1,1) PRIMARY KEY,
    MeetingID int,
    UserID int, 
    MinutesDate date,
    FOREIGN KEY (MeetingID) REFERENCES meeting(MeetingID),
    FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE projectNotes(
    NotesID int Identity(1,1) PRIMARY KEY,
    ProjectID int, 
	AuthorID int,
    Content text, 
    NoteDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID),
	FOREIGN KEY (AuthorID) REFERENCES users(UserID));

CREATE TABLE grants(
    GrantID int Identity(1,1) PRIMARY KEY,
	GrantName nvarchar(200),
    SupplierID int, 
    ProjectID int,
    StatusName nvarchar(200),
    Category nvarchar(200),
    SubmissionDate date, 
    descriptions text,
    AwardDate date,
    Amount float,
	GrantStatus nvarchar(200), --temporary
    FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));
	
CREATE TABLE grantTask(
    TaskID int Identity(1,1) PRIMARY KEY,
    GrantID int, 
    DueDate date,
    Objective nvarchar(200),
    FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE grantTaskStaff(
    GrantTaskStaffID int Identity(1,1) PRIMARY KEY,
    TaskID int, 
    AssigneeID int,
	AssignerID int,
    DueDate date,
    FOREIGN KEY (TaskID) REFERENCES grantTask(TaskID),
    FOREIGN KEY (AssigneeID) REFERENCES users(UserID),
	FOREIGN KEY (AssignerID) REFERENCES users(UserID));

CREATE TABLE grantStaff(
	grantStaffID int Identity(1,1) PRIMARY KEY,
	GrantID int,
	UserID int,
	UserRole varchar(200)
	FOREIGN KEY (UserID) REFERENCES users(UserID),
    FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE grantNotes(
    NotesID int Identity(1,1) PRIMARY KEY,
    GrantID int, 
	AuthorID int,
    Content text, 
    NoteDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (GrantID) REFERENCES grants(GrantID),
	FOREIGN KEY (AuthorID) REFERENCES users(UserID));


/* CREATE TABLE grantStatus(
    StatusID int Identity(1,1) PRIMARY KEY,
    GrantID int,
    StatusName nvarchar(200),
    ChangeDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE supplierStatus(
    StatusID int Identity(1,1) PRIMARY KEY,
    SupplierID int, 
    StatusName nvarchar(200),
    ChangeDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID)); */

CREATE TABLE userMessage(
    MessageID int Identity(1,1) PRIMARY KEY,
    SenderID int,
    RecipientID int,
    SubjectTitle nvarchar(MAX),
    Contents nvarchar(MAX),
    SentTime datetime DEFAULT GETDATE(),
    FOREIGN KEY (SenderID) REFERENCES users(UserID));
