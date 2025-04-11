CREATE TABLE users(
    UserID int Identity(1,1) PRIMARY KEY,
    Username nvarchar(200),
	Director bit default 0,
	AdminAssistant bit default 0);

CREATE TABLE funder(
    FunderID int Identity(1,1) PRIMARY KEY,
    FunderName nvarchar(200),
    OrgType nvarchar(200),
    BusinessAddress nvarchar(200));

CREATE TABLE funderNote(
	FunderNoteID int Identity(1,1) PRIMARY KEY,
	FunderID int,
	Contents nvarchar(max),
	DateAdded datetime default getdate(),
	AuthorID int,
	FOREIGN KEY (FunderID) REFERENCES funder(FunderID),
	FOREIGN KEY (AuthorID) REFERENCES users(UserID));

CREATE TABLE person(
	PersonID int Identity(1,1) PRIMARY KEY,
	UserID int,
	Pronouns nvarchar(200),
	FirstName nvarchar(200),
	LastName nvarchar(200),
	FOREIGN KEY (UserID) REFERENCES users(UserID));

CREATE TABLE contact(
	ContactID int Identity(1,1) PRIMARY KEY,
	Email nvarchar(200),
    Phone nvarchar(200),
    HomeAddress nvarchar(200),
	City nvarchar(200),
	HomeState nvarchar(200),
	Zip nvarchar(200),
	PersonID int,
	FOREIGN KEY (PersonID) REFERENCES person(PersonID));

CREATE TABLE funderPOC(
	FunderPOCID int Identity(1,1) PRIMARY KEY,
	PersonID int,
	FunderID int,
	CommunicationStatus nvarchar(200),
	FOREIGN KEY (PersonID) REFERENCES person(PersonID),
	FOREIGN KEY (FunderID) REFERENCES funder(FunderID));

CREATE TABLE funderRep (
    FunderRepID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    FunderID INT,
    CommunicationStatus NVARCHAR(200),
    FOREIGN KEY (FunderID) REFERENCES funder(FunderID),
    FOREIGN KEY (UserID) REFERENCES users(UserID)
);

CREATE TABLE externalPartner (
    externalPartnerID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT,
    FOREIGN KEY (UserID) REFERENCES users(UserID)
);

CREATE TABLE project(
    ProjectID int Identity(1,1) PRIMARY KEY,
    ProjectName nvarchar(200), 
	ProjectDescription nvarchar(200),
    DueDate datetime,
	DateAdded datetime default getdate());

CREATE TABLE projectStatus(
	ProjectStatusID int Identity (1,1) PRIMARY KEY,
	ProjectID int,
	StatusName varchar(200),
	ChangeDate datetime default getdate(),
	FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

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
	Completed bit default 0,
	DateAdded datetime default getdate(),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE projectTaskStaff(
    ProjectTaskStaffID int Identity(1,1) PRIMARY KEY,
    TaskID int, 
    AssigneeID int,
	AssignerID int,
    DueDate date,
	DateAdded datetime default getdate(),
    FOREIGN KEY (TaskID) REFERENCES projectTask(TaskID),
    FOREIGN KEY (AssigneeID) REFERENCES users(UserID),
	FOREIGN KEY (AssignerID) REFERENCES users(UserID));

CREATE TABLE meeting(
    MeetingID int Identity(1,1) PRIMARY KEY,
    ProjectID int, 
	GrantID int,
    MeetingDate date,
    Purpose nvarchar(200),
	DateAdded datetime default getdate(),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));

CREATE TABLE attendance(
    AttendanceID int Identity(1,1) PRIMARY KEY,
    MeetingID int,
    PersonID int,
    FOREIGN KEY (MeetingID) REFERENCES meeting(MeetingID),
    FOREIGN KEY (PersonID) REFERENCES person(PersonID));

CREATE TABLE meetingMinutes(
    MinutesID int Identity(1,1) PRIMARY KEY,
    MeetingID int,
    AuthorID int, 
    MinutesDate date,
	DateAdded datetime default getdate(),
    FOREIGN KEY (MeetingID) REFERENCES meeting(MeetingID),
    FOREIGN KEY (AuthorID) REFERENCES users(UserID));

CREATE TABLE projectNotes(
    NotesID int Identity(1,1) PRIMARY KEY,
    ProjectID int, 
	AuthorID int,
    Content nvarchar(max),
	DateAdded datetime default getdate(),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID),
	FOREIGN KEY (AuthorID) REFERENCES users(UserID));

CREATE TABLE grants(
    GrantID int Identity(1,1) PRIMARY KEY,
	GrantName nvarchar(200),
    FunderID int, 
    ProjectID int,
    Category nvarchar(200),
    SubmissionDate date, 
    descriptions nvarchar(max),
    AwardDate date,
    Amount float,
	DateAdded datetime default getdate(),
    FOREIGN KEY (FunderID) REFERENCES funder(FunderID),
    FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));
	
CREATE TABLE grantTask(
    TaskID int Identity(1,1) PRIMARY KEY,
    GrantID int, 
    DueDate date,
    Objective nvarchar(200),
	Completed bit default 0,
	DateAdded datetime default getdate(),
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
	PrincipalInvestigator bit default 0,
	CoPI bit default 0,
	UserID int,
	UserRole varchar(200),
	FOREIGN KEY (UserID) REFERENCES users(UserID),
	DateAdded datetime default getdate(),
    FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE grantNotes(
    NotesID int Identity(1,1) PRIMARY KEY,
    GrantID int, 
	AuthorID int,
    Content nvarchar(max), 
	DateAdded datetime default getdate(),
    FOREIGN KEY (GrantID) REFERENCES grants(GrantID),
	FOREIGN KEY (AuthorID) REFERENCES users(UserID));

CREATE TABLE grantStatus(
    StatusID int Identity(1,1) PRIMARY KEY,
    GrantID int,
    StatusName nvarchar(200),
    ChangeDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (GrantID) REFERENCES grants(GrantID));

CREATE TABLE funderStatus(
    StatusID int Identity(1,1) PRIMARY KEY,
    FunderID int, 
    StatusName nvarchar(200),
    ChangeDate datetime DEFAULT GETDATE(),
    FOREIGN KEY (FunderID) REFERENCES funder(FunderID));

CREATE TABLE userMessage(
    MessageID int Identity(1,1) PRIMARY KEY,
    SenderID int,
    RecipientID int,
    SubjectTitle nvarchar(MAX),
    Contents nvarchar(MAX),
    SentTime datetime DEFAULT GETDATE(),
    FOREIGN KEY (SenderID) REFERENCES users(UserID));

CREATE TABLE files(
	FileID int Identity(1,1) PRIMARY KEY,
	FilePath nvarchar(200),
	FileType nvarchar(200),
	NameFile nvarchar(200));

CREATE TABLE grantFile(
	GrantFileID int Identity(1,1) PRIMARY KEY,
	GrantID int,
	FileID int,
	FOREIGN KEY (FileID) REFERENCES files(FileID),
	FOREIGN KEY (GrantID) REFERENCES grants(GrantID));
	
CREATE TABLE projectFile(
	ProjectFileID int Identity(1,1) PRIMARY KEY,
	ProjectID int,
	FileID int,
	FOREIGN KEY (FileID) REFERENCES files(FileID),
	FOREIGN KEY (ProjectID) REFERENCES project(ProjectID));