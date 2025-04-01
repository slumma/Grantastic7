INSERT INTO users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress, AdminStatus, EmployeeStatus, FacultyStatus, NonFacultyStatus)
VALUES
('samogden', 'password123', 'sam', 'ogden', 'sam@example.com', '555-1234', '123 Elm St', 1, 1, 1, 1),
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


INSERT INTO projectTask (ProjectID, DueDate, Objective)
VALUES
(1, '2025-05-01', 'Initial Research'),
(2, '2025-07-01', 'Development'),
(3, '2025-11-01', 'Final Review'),
(4, '2025-04-01', 'Market Analysis'),
(5, '2025-06-01', 'Product Design'),
(6, '2025-08-01', 'Prototyping'),
(7, '2025-10-01', 'Testing'),
(8, '2025-12-01', 'Launch Plan'),
(9, '2025-03-01', 'Requirement Gathering'),
(10, '2025-09-01', 'Risk Assessment');

INSERT INTO projectTaskStaff (TaskID, AssigneeID, AssignerID, DueDate)
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
(1, 'Tree Fund', 1, 'Submitted', 'Federal', '2025-01-01', 'Grant for tech development', '2025-05-01', 100000, 'Active'),
(2, 'Timeless Treasure', 2, 'Under Review', 'State', '2025-03-01', 'Grant for educational programs', '2025-07-01', 50000, 'Pending'),
(3, 'K-12 Hospital Grant', 3, 'Awarded', 'Business', '2025-06-01', 'Grant for health initiatives', '2025-11-01', 200000, 'Inactive'),
(4, 'Dog Park 5000', 1, 'Submitted', 'University', '2025-02-01', 'Grant for research', '2025-06-01', 150000, 'Active'),
(5, '75 Dalmations', 2, 'Under Review', 'Federal', '2025-04-01', 'Grant for tech infrastructure', '2025-08-01', 80000, 'Pending'),
(6, 'The other 26 Dalmations', 3, 'Awarded', 'State', '2025-05-01', 'Grant for educational tools', '2025-09-01', 120000, 'Inactive'),
(7, 'Egg', 1, 'Submitted', 'Business', '2025-01-15', 'Grant for business development', '2025-06-15', 95000, 'Active'),
(8, 'Peanut Butter', 2, 'Under Review', 'University', '2025-03-15', 'Grant for academic research', '2025-07-15', 50000, 'Pending'),
(9, 'Firetruck!', 3, 'Awarded', 'Federal', '2025-06-15', 'Grant for health research', '2025-11-15', 220000, 'Inactive'),
(10, 'Eggs Benedict', 1, 'Submitted', 'State', '2025-02-15', 'Grant for public health', '2025-06-15', 130000, 'Active'),
(4, 'Benedict Cumberbatch', 2, 'Under Review', 'Business', '2025-04-15', 'Grant for business innovation', '2025-08-15', 85000, 'Pending'),
(9, 'Cabbage Patch Kids', 3, 'Awarded', 'University', '2025-05-15', 'Grant for educational research', '2025-09-15', 140000, 'Inactive'),
(3, 'Kids on the Bus', 1, 'Submitted', 'Federal', '2025-01-20', 'Grant for tech advancement', '2025-06-20', 105000, 'Active'),
(5, 'Bus from Speed', 2, 'Under Review', 'State', '2025-03-20', 'Grant for state projects', '2025-07-20', 75000, 'Pending'),
(1, 'Tom Cruise', 3, 'Awarded', 'Business', '2025-06-20', 'Grant for business ventures', '2025-11-20', 195000, 'Inactive');

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



