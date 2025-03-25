CREATE TABLE users(
    UserID int Identity(1,1) PRIMARY KEY,
    Username nvarchar(200),
	Password nvarchar(200),
    FirstName nvarchar(200),
    LastName nvarchar(200),
    Email nvarchar(200),
    Phone nvarchar(200),
    HomeAddress nvarchar(200),
	PFPFilePath nvarchar(200),
	AdminStatus bit default 0,
	ViewArchives bit default 0);

CREATE TABLE project(
	ProjectID int Identity(1,1) PRIMARY KEY,
	ProjectName nvarchar(200),
	DueDate datetime);



-- Grant Supplier

CREATE TABLE grantSupplier(
	SupplierID int Identity(1,1) PRIMARY KEY,
	SupplierName nvarchar(200),
	OrgType nvarchar(200),
	Address nvarchar(200));

CREATE TABLE supplierStatus(
	SupplierStatusID int Identity(1,1) PRIMARY KEY,
	SupplierID int,
	StatusName nvarchar(200),
	ChangeDate datetime,
	FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID));

CREATE TABLE BPrep(
    UserID INT PRIMARY KEY,
    CommunicationStatus nvarchar(200),
    SupplierID int,
    FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID),
    FOREIGN KEY (UserID) REFERENCES users(UserID));



-- Grant

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
    FOREIGN KEY (SupplierID) REFERENCES grantSupplier(SupplierID),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE grantStatus(
	GrantStatusID int Identity(1,1) PRIMARY KEY,
	GrantID int,
	StatusName nvarchar(200),
	ChangeDate datetime,
	FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE grantStaff(
	GrantStaffID int Identity(1,1) PRIMARY KEY,
	StaffedID int,
	StafferID int,
	GrantID int,
	Active bit default 1,
	ViewGrant bit default 1,
	EditGrant bit default 0,
	PIStatus bit default 0,
	FOREIGN KEY (StaffedID) REFERENCES users(UserID),
	FOREIGN KEY (StafferID) REFERENCES users(UserID),
	FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE grantTask(
	GrantTaskID int Identity(1,1) PRIMARY KEY,
	GrantID int,
	DueDate datetime,
	Objective nvarchar(200),
	FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE grantTaskStaff(
	GrantTaskStaffID int Identity(1,1) PRIMARY KEY,
	GrantID int,
	AssigneeID int,
	AssignerID int,
	FOREIGN KEY (GrantID) REFERENCES grants(GrantID),
	FOREIGN KEY (AssigneeID) REFERENCES users(UserID),
	FOREIGN KEY (AssignerID) REFERENCES users(UserID));



-- Project

CREATE TABLE projectStatus(
	StatusID int Identity(1,1) PRIMARY KEY,
	ProjectID int,
	StatusName nvarchar(200),
	ChangeDate datetime,
	FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE projectStaff(
	ProjectStaffID int Identity(1,1) PRIMARY KEY,
	StaffedID int,
	StafferID int,
	ProjectID int,
	Active bit default 1,
	ViewProject bit default 1,
	EditProject bit default 0,
	PMStatus bit default 0,
	FOREIGN KEY (StaffedID) REFERENCES users(UserID),
	FOREIGN KEY (StafferID) REFERENCES users(UserID),
	FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE projectTask(
	ProjectTaskID int Identity(1,1) PRIMARY KEY,
	ProjectID int,
	DueDate datetime,
	Objective nvarchar(200),
	FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE projectTaskStaff(
    ProjectTaskStaffID int Identity(1,1) PRIMARY KEY,
    ProjectTaskID int, 
    AssigneeID int,
	AssignerID int,
    DueDate date,
    FOREIGN KEY (ProjectTaskID) REFERENCES projectTask(ProjectTaskID),
    FOREIGN KEY (AssigneeID) REFERENCES users(UserID),
	FOREIGN KEY (AssignerID) REFERENCES users(UserID));

CREATE TABLE note(
	NoteID int Identity(1,1) PRIMARY KEY,
	ProjectID int,
	NoteContents nvarchar(1000),
	DatePosted datetime,
	FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));



-- Meeting

CREATE TABLE meeting(
	MeetingID int Identity(1,1) PRIMARY KEY,
	ProjectID int,
	MeetingDate datetime,
	Purpose nvarchar(200),
	FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE attendance(
	AttendanceID int Identity(1,1) PRIMARY KEY,
	MeetingID int,
	UserID int,
	FOREIGN KEY (MeetingID) REFERENCES meeting(MeetingID),
	FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE meetingMinutes(
	MinutesID int Identity(1,1) PRIMARY KEY,
	MeetingID int,
	AuthorID int,
	FOREIGN KEY (MeetingID) REFERENCES meeting(MeetingID),
	FOREIGN KEY (AuthorID) REFERENCES users(UserID));



-- Message

CREATE TABLE userMessage(
    MessageID int Identity(1,1) PRIMARY KEY,
    SenderID int,
    SubjectTitle nvarchar(MAX),
    Contents nvarchar(MAX),
    SentTime datetime DEFAULT GETDATE(),
    FOREIGN KEY (SenderID) REFERENCES users(UserID));

CREATE TABLE messageRecipient(
	MessageRecipientID int Identity(1,1) PRIMARY KEY,
	MessageID int,
	RecipientID int,
	FOREIGN KEY (RecipientID) REFERENCES users(UserID),
	FOREIGN KEY (MessageID) REFERENCES userMessage(MessageID));