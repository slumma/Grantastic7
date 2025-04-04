INSERT INTO users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress, AdminStatus, EmployeeStatus, FacultyStatus, NonFacultyStatus)
VALUES
('sarahbennett', 'password123', 'Sarah', 'Bennett', 'sbennett@example.com', '555-1234', '123 Elm St', 1, 1, 1, 1),
('nickclement', 'password456', 'nick', 'clement', 'nickclement@example.com', '555-5678', '456 Oak St', 1, 1, 1, 1),
('nadeemhudson', 'password789', 'nadeem', 'hudson', 'nadeemhudson@example.com', '555-9876', '789 Pine St', 0, 1, 1, 1),
('joshwhite', 'password234', 'josh', 'White', 'joshwhite@example.com', '555-2234', '321 Birch St', 0, 1, 0, 1),
('sharons', 'password567', 'sharon', 'sanchez', 'shrnsnchz@example.com', '555-6789', '654 Cedar St', 0, 0, 0, 1),
('jezell', 'password890', 'jeremy', 'ezell', 'jezell@example.com', '555-7890', '987 Spruce St', 1, 1, 1, 1),
('benfrench', 'password101', 'hailey', 'welch', 'benfrench@example.com', '555-1010', '109 Maple St', 1, 1, 1, 1),
('royrinehart', 'password112', 'roy', 'rinehart', 'royr838@example.com', '555-1212', '210 Oak St', 0, 0, 0, 0),
('ryanbucciero', 'password213', 'ryan', 'bucciero', 'rbucc87392@example.com', '555-1414', '312 Pine St', 1, 1, 1, 1),
('BabikDmx', 'password314', 'dmytro', 'babik', 'dmytrobabik43@example.com', '555-1515', '413 Elm St', 1, 1, 1, 1),
('samO', 'password314', 'sam', 'o', 'samoGden@example.com', '555-1515', '413 Elm St', 1, 1, 1, 1);

INSERT INTO grantSupplier (SupplierName, OrgType, SupplierStatus, BusinessAddress)
VALUES
('TechCorp', 'Private', 'Active', '101 Tech Rd'),
('EduFunds', 'Non-Profit', 'Inactive', '202 Education Blvd'),
('HealthGrant', 'Government', 'Pending', '303 Health Ave'),
('ScienceTrust', 'Non-Profit', 'Active', '404 Research Blvd'),
('MedFunds', 'Private', 'Inactive', '505 Healthcare St'),
('EduTech', 'Government', 'Pending', '606 Innovation Way'),
('GreenGrants', 'Non-Profit', 'Active', '707 Sustainability Ave'),
('ArtFunds', 'Private', 'Inactive', '808 Creative St'),
('SocialAid', 'Government', 'Pending', '909 Welfare Blvd'),
('TechInnovators', 'Private', 'Active', '1010 Future Rd');



INSERT INTO project (ProjectName, DueDate, ProjectDescription)
VALUES
('Project Alpha', '2025-06-01', 'Development of an AI-powered analytics tool'),
('Project Beta', '2025-08-15', 'Enhancement of cybersecurity measures for internal systems'),
('Project Gamma', '2025-12-31', 'Implementation of a cloud-based data storage solution'),
('Project Delta', '2025-04-20', 'Market research and competitor analysis for a new product launch'),
('Project Epsilon', '2025-07-10', 'User interface redesign to improve customer experience'),
('Project Zeta', '2025-09-25', 'Machine learning model optimization for predictive analysis'),
('Project Eta', '2025-11-15', 'Testing and deployment of an automated customer support chatbot'),
('Project Theta', '2025-03-05', 'Requirement gathering and feasibility study for a new software initiative'),
('Project Iota', '2025-10-20', 'Security audit and compliance assessment for regulatory standards'),
('Project Kappa', '2025-05-25', 'Integration of AI-driven automation into business processes');



INSERT INTO BPrep (UserID, CommunicationStatus, SupplierID)
VALUES
(1, 'Active', 1),
(2, 'Inactive', 2),
(3, 'Pending', 3),
(4, 'Active', 4),
(5, 'Inactive', 5),
(6, 'Pending', 6),
(7, 'Active', 7),
(8, 'Inactive', 8),
(9, 'Pending', 9),
(10, 'Active', 10);

INSERT INTO projectStaff (ProjectID, UserID, Leader, Active)
VALUES
(1, 1, 1, 1),
(1, 2, 0, 1),
(2, 3, 1, 1),
(2, 4, 0, 1),
(3, 5, 1, 1),
(3, 6, 0, 1),
(4, 7, 1, 1),
(4, 8, 0, 1),
(5, 9, 1, 1),
(5, 10, 0, 1);


INSERT INTO projectTask (ProjectID, DueDate, Objective, Completed)
VALUES
(1, '2025-04-15', 'Initial Research', 1), -- ProjectTaskID: 1
(1, '2025-05-01', 'Draft Report', 0), -- ProjectTaskID: 2
(1, '2025-05-15', 'Stakeholder Interviews', 1), -- ProjectTaskID: 3
(1, '2025-06-01', 'Review Findings', 0), -- ProjectTaskID: 4
(1, '2025-06-15', 'Submit Report', 1), -- ProjectTaskID: 5

(2, '2025-06-01', 'Project Setup', 1), -- ProjectTaskID: 6
(2, '2025-06-15', 'Database Design', 0), -- ProjectTaskID: 7
(2, '2025-07-01', 'Development', 1), -- ProjectTaskID: 8
(2, '2025-07-15', 'API Integration', 0), -- ProjectTaskID: 9
(2, '2025-08-01', 'Feature Testing', 1), -- ProjectTaskID: 10

(3, '2025-09-01', 'Code Review', 0), -- ProjectTaskID: 11
(3, '2025-09-15', 'Bug Fixing', 1), -- ProjectTaskID: 12
(3, '2025-10-01', 'Performance Optimization', 0), -- ProjectTaskID: 13
(3, '2025-10-15', 'User Acceptance Testing', 1), -- ProjectTaskID: 14
(3, '2025-11-01', 'Final Review', 1), -- ProjectTaskID: 15

(4, '2025-03-15', 'Industry Research', 1), -- ProjectTaskID: 16
(4, '2025-04-01', 'Market Analysis', 0), -- ProjectTaskID: 17
(4, '2025-04-15', 'Competitor Benchmarking', 1), -- ProjectTaskID: 18
(4, '2025-05-01', 'Customer Surveys', 0), -- ProjectTaskID: 19

(5, '2025-05-15', 'Brainstorming Session', 0), -- ProjectTaskID: 20
(5, '2025-06-01', 'Product Design', 1), -- ProjectTaskID: 21
(5, '2025-06-15', '3D Modeling', 0), -- ProjectTaskID: 22
(5, '2025-07-01', 'Material Selection', 1), -- ProjectTaskID: 23
(5, '2025-07-15', 'Prototype Sketching', 1), -- ProjectTaskID: 24

(6, '2025-07-01', 'Gather Requirements', 1), -- ProjectTaskID: 25
(6, '2025-07-15', 'Build First Prototype', 0), -- ProjectTaskID: 26
(6, '2025-08-01', 'Testing and Refinements', 1), -- ProjectTaskID: 27
(6, '2025-08-15', 'User Feedback Collection', 0), -- ProjectTaskID: 28

(7, '2025-09-01', 'Test Plan Development', 1), -- ProjectTaskID: 29
(7, '2025-09-15', 'Alpha Testing', 0), -- ProjectTaskID: 30
(7, '2025-10-01', 'Beta Testing', 1), -- ProjectTaskID: 31
(7, '2025-10-15', 'Usability Testing', 0), -- ProjectTaskID: 32
(7, '2025-11-01', 'Final Bug Fixes', 1), -- ProjectTaskID: 33

(8, '2025-11-15', 'Marketing Strategy', 0), -- ProjectTaskID: 34
(8, '2025-12-01', 'Launch Plan', 1), -- ProjectTaskID: 35
(8, '2025-12-15', 'Public Relations', 0), -- ProjectTaskID: 36
(8, '2026-01-01', 'Final Campaign Review', 1), -- ProjectTaskID: 37

(9, '2025-02-15', 'Stakeholder Meetings', 1), -- ProjectTaskID: 38
(9, '2025-03-01', 'Requirement Gathering', 0), -- ProjectTaskID: 39
(9, '2025-03-15', 'Feature Prioritization', 1), -- ProjectTaskID: 40
(9, '2025-04-01', 'Documentation', 0), -- ProjectTaskID: 41
(9, '2025-04-15', 'Sign-Off from Team', 1), -- ProjectTaskID: 42

(10, '2025-08-15', 'Identify Risks', 0), -- ProjectTaskID: 43
(10, '2025-09-01', 'Risk Assessment', 1), -- ProjectTaskID: 44
(10, '2025-09-15', 'Mitigation Strategies', 0), -- ProjectTaskID: 45
(10, '2025-10-01', 'Contingency Planning', 1), -- ProjectTaskID: 46
(10, '2025-10-15', 'Final Review & Approval', 1); -- ProjectTaskID: 47

INSERT INTO projectTaskStaff (TaskID, AssigneeID, AssignerID, DueDate)
VALUES
(1, 1, 1, '2025-04-15'),  -- Project 1
(2, 2, 1, '2025-05-01'),
(3, 1, 2, '2025-05-15'),
(4, 2, 1, '2025-06-01'),
(5, 1, 2, '2025-06-15'),

(6, 3, 3, '2025-06-01'),  -- Project 2
(7, 4, 3, '2025-06-15'),
(8, 3, 4, '2025-07-01'),
(9, 4, 3, '2025-07-15'),
(10, 3, 4, '2025-08-01'),

(11, 5, 5, '2025-09-01'),  -- Project 3
(12, 6, 5, '2025-09-15'),
(13, 5, 6, '2025-10-01'),
(14, 6, 5, '2025-10-15'),
(15, 5, 6, '2025-11-01'),

(16, 7, 7, '2025-03-15'),  -- Project 4
(17, 8, 7, '2025-04-01'),
(18, 7, 8, '2025-04-15'),
(19, 8, 7, '2025-05-01'),

(20, 9, 9, '2025-05-15'),  -- Project 5
(21, 10, 9, '2025-06-01'),
(22, 9, 10, '2025-06-15'),
(23, 10, 9, '2025-07-01'),
(24, 9, 10, '2025-07-15'),

(25, 9, 9, '2025-07-01'),  -- Project 6
(26, 10, 9, '2025-07-15'),
(27, 9, 10, '2025-08-01'),
(28, 10, 9, '2025-08-15'),

(29, 7, 7, '2025-09-01'),  -- Project 7
(30, 8, 7, '2025-09-15'),
(31, 7, 8, '2025-10-01'),
(32, 8, 7, '2025-10-15'),
(33, 7, 8, '2025-11-01'),

(34, 5, 5, '2025-11-15'),  -- Project 8
(35, 6, 5, '2025-12-01'),
(36, 5, 6, '2025-12-15'),
(37, 6, 5, '2026-01-01'),

(38, 3, 3, '2025-02-15'),  -- Project 9
(39, 4, 3, '2025-03-01'),
(40, 3, 4, '2025-03-15'),
(41, 4, 3, '2025-04-01'),
(42, 3, 4, '2025-04-15'),

(43, 1, 1, '2025-08-15'),  -- Project 10
(44, 2, 1, '2025-09-01'),
(45, 1, 2, '2025-09-15'),
(46, 2, 1, '2025-10-01'),
(47, 1, 2, '2025-10-15');


INSERT INTO meeting (ProjectID, MeetingDate, Purpose)
VALUES
(1, '2025-03-01', 'Kick-off Meeting'),
(2, '2025-05-15', 'Progress Update'),
(3, '2025-09-01', 'Final Review'),
(4, '2025-04-01', 'Team Meeting'),
(5, '2025-06-15', 'Strategy Session'),
(6, '2025-08-01', 'Budget Review'),
(7, '2025-10-01', 'Planning Session'),
(8, '2025-12-01', 'Status Update'),
(9, '2025-03-15', 'Client Meeting'),
(10, '2025-09-15', 'Project Wrap-Up');


INSERT INTO attendance (MeetingID, UserID)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 9),
(5, 10);

INSERT INTO meetingMinutes (MeetingID, UserID, MinutesDate)
VALUES
(1, 1, '2025-03-02'),
(2, 2, '2025-05-16'),
(3, 3, '2025-09-02'),
(4, 4, '2025-04-02'),
(5, 5, '2025-06-16'),
(6, 6, '2025-08-02'),
(7, 7, '2025-10-02'),
(8, 8, '2025-12-02'),
(9, 9, '2025-03-16'),
(10, 10, '2025-09-16');

INSERT INTO projectNotes (ProjectID, AuthorID, Content, noteDate)
VALUES
(1, 1, 'Initial notes for Project Alpha', '2025-02-20'),
(2, 2, 'Development notes for Project Beta', '2025-04-15'),
(3, 3, 'Review notes for Project Gamma', '2025-08-01'),
(4, 4, 'Concept notes for Project Delta', '2025-03-20'),
(5, 5, 'Planning notes for Project Epsilon', '2025-05-15'),
(6, 6, 'Design notes for Project Zeta', '2025-07-01'),
(7, 7, 'Prototype notes for Project Eta', '2025-09-01'),
(8, 8, 'Launch notes for Project Theta', '2025-11-01'),
(9, 9, 'Requirement notes for Project Iota', '2025-02-15'),
(10, 10, 'Assessment notes for Project Kappa', '2025-10-01');

INSERT INTO grants (SupplierID, GrantName, ProjectID, StatusName, Category, SubmissionDate, descriptions, AwardDate, Amount, GrantStatus)
VALUES
(1, 'AI Innovation Grant', 1, 'Submitted', 'Federal', '2025-01-01', 'Grant for tech development', '2025-05-01', 100000, 'Active'),
(2, 'State Education Initiative', 2, 'Under Review', 'State', '2025-03-01', 'Grant for educational programs', '2025-07-01', 50000, 'Pending'),
(3, 'Healthcare Impact Grant', 3, 'Awarded', 'Business', '2025-06-01', 'Grant for health initiatives', '2025-11-01', 200000, 'Inactive'),
(4, 'NextGen Research Fund', 1, 'Submitted', 'University', '2025-02-01', 'Grant for AI research', '2025-06-01', 150000, 'Active'),
(5, 'Infrastructure Enhancement Grant', 2, 'Under Review', 'Federal', '2025-04-01', 'Grant for tech infrastructure', '2025-08-01', 80000, 'Pending'),
(6, 'Educational Tools Development', 3, 'Awarded', 'State', '2025-05-01', 'Grant for educational tools', '2025-09-01', 120000, 'Inactive'),
(8, 'University Research Support', 2, 'Under Review', 'University', '2025-03-15', 'Grant for academic research', '2025-07-15', 50000, 'Pending'),
(9, 'Medical Research Advancement', 3, 'Awarded', 'Federal', '2025-06-15', 'Grant for health research', '2025-11-15', 220000, 'Inactive'),
(4, 'Business Innovation Fund', 2, 'Under Review', 'Business', '2025-04-15', 'Grant for business innovation', '2025-08-15', 85000, 'Pending'),
(9, 'STEM Education Grant', 3, 'Awarded', 'University', '2025-05-15', 'Grant for educational research', '2025-09-15', 140000, 'Inactive'),
(5, 'State Development Initiative', 2, 'Under Review', 'State', '2025-03-20', 'Grant for state projects', '2025-07-20', 75000, 'Pending'),
(1, 'AI Business Accelerator', 3, 'Awarded', 'Business', '2025-06-20', 'Grant for business ventures', '2025-11-20', 195000, 'Inactive');

INSERT INTO grantStaff(GrantID, UserID, UserRole)
VALUES
(1, 1, 'Principal Investigator'),
(1, 2, 'Co-PI'),
(2, 1, 'Researcher'),
(2, 2, 'Principal Investigator'),
(3, 3, 'Assistant'),
(4, 4, 'Researcher'),
(5, 5, 'Researcher'),
(6, 6, 'Researcher'),
(7, 7, 'Researcher'),
(8, 8, 'Researcher'),
(9, 9, 'Researcher'),
(10, 10, 'Researcher');

INSERT INTO grantTask(GrantID, DueDate, Objective)
VALUES
(1, '2025-05-01', 'Industry Trends Analysis'),
(2, '2025-07-01', 'Algorithm Development'),
(3, '2025-11-01', 'Quality Assurance'),
(4, '2025-04-01', 'Competitor Benchmarking'),
(5, '2025-06-01', 'UI/UX Wireframing'),
(6, '2025-08-01', 'Beta Testing'),
(7, '2025-10-01', 'Performance Optimization'),
(8, '2025-12-01', 'Deployment Strategy'),
(9, '2025-03-01', 'User Needs Assessment'),
(10, '2025-09-01', 'Security Evaluation');

INSERT INTO grantTaskStaff (TaskID, AssigneeID, AssignerID, DueDate)
VALUES
(1, 1, 2, '2025-04-25'),
(2, 2, 3, '2025-06-15'),
(3, 3, 1, '2025-10-01'),
(4, 4, 5, '2025-03-25'),
(5, 5, 6, '2025-05-15'),
(6, 6, 4, '2025-07-25'),
(7, 7, 8, '2025-09-15'),
(8, 8, 9, '2025-11-25'),
(9, 9, 10, '2025-02-15'),
(10, 10, 7, '2025-08-15');

INSERT INTO grantNotes (GrantID, AuthorID, Content, noteDate)
VALUES
(1, 1, 'Took out trash', '2025-02-20'),
(2, 2, 'Emptied Dishwasher', '2025-04-15'),
(3, 3, 'Eggs are my favorite food', '2025-08-01'),
(4, 4, 'Dogs > Cats', '2025-03-20'),
(5, 5, 'Money money money money!', '2025-05-15'),
(6, 6, 'Elephant Ellipses', '2025-07-01'),
(7, 7, 'Eta more like beta', '2025-09-01'),
(8, 8, 'icloud.com/checkthispage', '2025-11-01'),
(9, 9, 'im running out of notes to write', '2025-02-15'),
(10, 10, 'thank god this is the last one', '2025-10-01');

/*INSERT INTO grantStatus (GrantID, StatusName, ChangeDate)
VALUES
(1, 'Approved', '2025-02-01'),
(2, 'Pending', '2025-04-01'),
(3, 'Denied', '2025-08-01'),
(4, 'Approved', '2025-06-01'),
(5, 'Pending', '2025-08-01'),
(6, 'Denied', '2025-09-01'),
(7, 'Approved', '2025-07-01'),
(8, 'Pending', '2025-09-01'),
(9, 'Denied', '2025-11-01'),
(10, 'Approved', '2025-05-01');

INSERT INTO supplierStatus (SupplierID, StatusName, ChangeDate)
VALUES
(1, 'Active', '2025-01-01'),
(2, 'Inactive', '2025-03-01'),
(3, 'Pending', '2025-06-01'),
(4, 'Active', '2025-02-01'),
(5, 'Inactive', '2025-04-01'),
(6, 'Pending', '2025-05-01'),
(7, 'Active', '2025-01-15'),
(8, 'Inactive', '2025-03-15'),
(9, 'Pending', '2025-06-15'),
(10, 'Active', '2025-02-15');*/

INSERT INTO userMessage (SenderID, RecipientID, SubjectTitle, Contents, SentTime)
VALUES
(1, 2, 'Hello', 'This is a test message', '2025-02-20 10:00:00'),
(2, 3, 'Reminder', 'Dont forget the meeting tomorrow', '2025-02-21 11:00:00'),
(3, 1, 'Thank you', 'Thanks for your help!', '2025-02-22 09:00:00'),
(4, 5, 'Meeting Update', 'The meeting has been rescheduled', '2025-02-23 08:00:00'),
(5, 6, 'Project Update', 'Here is the latest update on the project', '2025-02-24 07:00:00'),
(6, 4, 'Task Reminder', 'Dont forget to complete your tasks', '2025-02-25 06:00:00'),
(7, 8, 'Client Meeting', 'We have a meeting with the client tomorrow', '2025-02-26 05:00:00'),
(8, 9, 'Weekly Report', 'Please submit your weekly report', '2025-02-27 04:00:00'),
(9, 10, 'feedback request', 'Can you provide feedback', '2025-02-28 03:00:00'),
(10, 7, 'Team Lunch', 'We are having a team lunch on Friday', '2025-03-01 02:00:00');



