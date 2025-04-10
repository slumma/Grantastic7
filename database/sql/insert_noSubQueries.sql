INSERT INTO users (Username, Director, AdminAssistant)
VALUES
('sarahbennett', 1, 1),
('nickclement', 1, 1),
('nadeemhudson', 1, 1),
('joshwhite', 0, 1),
('sharons', 0, 1),
('jezell', 0, 0),
('benfrench', 0, 0),
('samO', 0, 0);

INSERT INTO funder (FunderName, OrgType, BusinessAddress)
VALUES
('TechCorp', 'Institution', '101 Tech Rd'),
('EduFunds', 'University', '202 Education Blvd'),
('HealthGrant', 'Federal', '303 Health Ave'),
('ScienceTrust', 'University', '404 Research Blvd'),
('MedFunds', 'Institution', '505 Healthcare St'),
('EduTech', 'State', '606 Innovation Way'),
('GreenGrants', 'University', '707 Sustainability Ave'),
('ArtFunds', 'Institution', '808 Creative St'),
('SocialAid', 'State', '909 Welfare Blvd'),
('TechInnovators', 'Institution', '1010 Future Rd');

INSERT INTO person (UserID, Pronouns, Firstname, LastName)
VALUES
(1, 'She/Her', 'Sarah', 'Bennett'), --personID 1
(2, 'He/Him', 'nick', 'clement'), --personID 2
(3, 'He/Him', 'nadeem', 'hudson'), --personID 3
(4, 'They/Them', 'josh', 'White'), --personID 4
(5, 'She/Her', 'sharon', 'sanchez'), --personID 5
(6, 'He/Him', 'jeremy', 'ezell'), --personID 6
(7, 'He/Him', 'ben', 'french'), --personID 7

(8, 'He/Him', 'sam', 'o'), --personID 8 FUNDER POC (USER)
(null, 'He/Him', 'roy', 'rinehart'); --personID 9 FUNDER POC (NON-USER)

INSERT INTO funderPOC (PersonID, FunderID)
VALUES
(8, 1),
(8, 2),
(8, 3),
(8, 4),
(8, 5),
(8, 6),
(9, 7),
(9, 8),
(9, 9),
(9, 10);

INSERT INTO contact (PersonID, Email, Phone, HomeAddress, City, HomeState, Zip)
VALUES
(1, 'sbennett@example.com', '555-1234', '123 Elm St', 'Harrisonburg', 'VA', '22801'),
(2, 'nickclement@example.com', '555-5678', '456 Oak St', 'Harrisonburg', 'VA', '22801'),
(3, 'nadeemhudson@example.com', '555-9876', '789 Pine St', 'Harrisonburg', 'VA', '22801'),
(4, 'joshwhite@example.com', '555-2234', '321 Birch St', 'Harrisonburg', 'VA', '22801'),
(5, 'shrnsnchz@example.com', '555-6789', '654 Cedar St', 'Harrisonburg', 'VA', '22801'),
(6, 'jezell@example.com', '555-7890', '987 Spruce St', 'Harrisonburg', 'VA', '22801'),
(7, 'benfrench@example.com', '555-1010', '109 Maple St', 'Harrisonburg', 'VA', '22801'),
(8, 'samoGden@example.com', '555-1515', '413 Elm St', 'Harrisonburg', 'VA', '22801'),
(9, 'royr838@example.com', '555-1212', '210 Oak St', 'Harrisonburg', 'VA', '22801');

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

INSERT INTO projectStaff (ProjectID, UserID, Leader, Active)
VALUES
(1, 1, 1, 1),
(1, 2, 0, 1),
(2, 3, 1, 1),
(2, 4, 0, 1),
(3, 5, 1, 1),
(3, 6, 0, 1),
(4, 1, 1, 1),
(4, 2, 0, 1),
(5, 3, 1, 1),
(5, 4, 0, 1),
(6, 5, 1, 1),
(6, 6, 0, 1),
(7, 1, 1, 1),
(7, 2, 0, 1),
(8, 3, 1, 1),
(8, 4, 0, 1),
(9, 5, 1, 1),
(9, 6, 0, 1),
(10, 1, 1, 1),
(10, 2, 0, 1);


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
(3, 1, 1, '2025-05-15'),
(4, 2, 1, '2025-06-01'),
(5, 1, 1, '2025-06-15'),

(6, 3, 3, '2025-06-01'),  -- Project 2
(7, 4, 3, '2025-06-15'),
(8, 3, 3, '2025-07-01'),
(9, 4, 3, '2025-07-15'),
(10, 3, 3, '2025-08-01'),

(11, 5, 5, '2025-09-01'),  -- Project 3
(12, 6, 5, '2025-09-15'),
(13, 5, 5, '2025-10-01'),
(14, 6, 5, '2025-10-15'),
(15, 5, 5, '2025-11-01'),

(16, 1, 1, '2025-03-15'),  -- Project 4
(17, 2, 1, '2025-04-01'),
(18, 1, 1, '2025-04-15'),
(19, 2, 1, '2025-05-01'),

(20, 3, 3, '2025-05-15'),  -- Project 5
(21, 3, 3, '2025-06-01'),
(22, 4, 3, '2025-06-15'),
(23, 4, 3, '2025-07-01'),
(24, 4, 3, '2025-07-15'),

(25, 5, 5, '2025-07-01'),  -- Project 6
(26, 5, 5, '2025-07-15'),
(27, 6, 5, '2025-08-01'),
(28, 6, 5, '2025-08-15'),

(29, 1, 1, '2025-09-01'),  -- Project 7
(30, 1, 1, '2025-09-15'),
(31, 2, 1, '2025-10-01'),
(32, 2, 1, '2025-10-15'),
(33, 2, 1, '2025-11-01'),

(34, 3, 3, '2025-11-15'),  -- Project 8
(35, 3, 3, '2025-12-01'),
(36, 4, 3, '2025-12-15'),
(37, 4, 3, '2026-01-01'),

(38, 5, 5, '2025-02-15'),  -- Project 9
(39, 5, 5, '2025-03-01'),
(40, 6, 5, '2025-03-15'),
(41, 6, 5, '2025-04-01'),
(42, 6, 5, '2025-04-15'),

(43, 1, 1, '2025-08-15'),  -- Project 10
(44, 2, 1, '2025-09-01'),
(45, 1, 1, '2025-09-15'),
(46, 2, 1, '2025-10-01'),
(47, 1, 1, '2025-10-15');


INSERT INTO meeting (ProjectID, MeetingDate, Purpose)
VALUES
(1, '2025-03-01', 'Kick-off Meeting'),
(2, '2025-05-15', 'Progress Update'),
(3, '2025-09-01', 'Final Review'),
(4, '2025-04-01', 'Team Meeting'),
(5, '2025-06-15', 'Strategy Session');

INSERT INTO meeting (GrantID, MeetingDate, Purpose)
VALUES
(1, '2025-03-01', 'Kick-off Meeting'),
(2, '2025-05-15', 'Progress Update'),
(3, '2025-09-01', 'Final Review'),
(4, '2025-04-01', 'Team Meeting'),
(5, '2025-06-15', 'Strategy Session');

INSERT INTO attendance (MeetingID, PersonID)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 2),
(5, 1),
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 6),
(4, 7),
(4, 8),
(5, 2),
(5, 1);

INSERT INTO meetingMinutes (MeetingID, AuthorID, MinutesDate)
VALUES
(1, 1, '2025-03-02'),
(2, 2, '2025-05-16'),
(3, 3, '2025-09-02'),
(4, 4, '2025-04-02'),
(5, 5, '2025-06-16'),
(6, 6, '2025-08-02'),
(7, 4, '2025-10-02'),
(8, 3, '2025-12-02'),
(9, 2, '2025-03-16'),
(10, 1, '2025-09-16');

INSERT INTO projectNotes (ProjectID, AuthorID, Content)
VALUES
(1, 1, 'Initial notes for Project Alpha'),
(2, 2, 'Development notes for Project Beta'),
(3, 3, 'Review notes for Project Gamma'),
(4, 4, 'Concept notes for Project Delta'),
(5, 5, 'Planning notes for Project Epsilon'),
(6, 6, 'Design notes for Project Zeta'),
(7, 4, 'Prototype notes for Project Eta'),
(8, 3, 'Launch notes for Project Theta'),
(9, 2, 'Requirement notes for Project Iota'),
(10, 1, 'Assessment notes for Project Kappa');

INSERT INTO grants (FunderID, GrantName, ProjectID, Category, SubmissionDate, descriptions, AwardDate, Amount)
VALUES
(1, 'AI Innovation Grant', 1, 'Federal', '2025-01-01', 'This grant is focused on advancing the development and application of artificial intelligence technologies. The goal is to create cutting-edge solutions that can be integrated into existing systems, improving efficiency and capability in various industries, from healthcare to business automation. The funds will support research and development teams, along with necessary infrastructure.', '2025-05-01', 100000),
(2, 'State Education Initiative', 2, 'State', '2025-03-01', 'The State Education Initiative aims to enhance the quality of education in underfunded regions. This grant will support the creation and implementation of educational programs designed to bridge gaps in accessibility and quality. Funds will be allocated to developing curricula, training teachers, and providing educational resources to underserved communities.', '2025-07-01', 50000),
(3, 'Healthcare Impact Grant', 3, 'Institution', '2025-06-01', 'This grant is dedicated to funding projects that aim to improve healthcare delivery systems, focusing on technological advancements, patient care innovation, and improving access to services in rural and underserved areas. The grant will fund new research, pilot programs, and solutions for improving healthcare infrastructure.', '2025-11-01', 200000),
(4, 'NextGen Research Fund', 4, 'University', '2025-02-01', 'The NextGen Research Fund is designed to support cutting-edge academic research in the field of artificial intelligence. Researchers will focus on pushing the boundaries of AI theory and practical applications, exploring new techniques and algorithms that could revolutionize industries like healthcare, education, and autonomous systems.', '2025-06-01', 150000),
(5, 'Infrastructure Enhancement Grant', 5, 'Federal', '2025-04-01', 'This federal grant is aimed at enhancing technological infrastructure in regions that are currently lacking in high-speed internet and modern tech resources. The funding will help to build the necessary backbone for digital transformation, focusing on rural broadband projects and other critical infrastructure improvements to support technological growth.', '2025-08-01', 80000),
(6, 'Educational Tools Development', 6, 'State', '2025-05-01', 'This grant supports the development and deployment of innovative educational tools that leverage technology to enhance learning experiences. It will fund the creation of new digital platforms, software, and tools that can be used by educators to improve engagement and the effectiveness of teaching in classrooms across the state.', '2025-09-01', 120000),
(8, 'University Research Support', 7, 'University', '2025-03-15', 'The University Research Support grant will fund academic research across a variety of disciplines, with a focus on interdisciplinary projects that have the potential to yield real-world applications. This includes funding for pilot studies, data collection, and collaboration between different academic departments within the university.', '2025-07-15', 50000),
(9, 'Medical Research Advancement', 8, 'Federal', '2025-06-15', 'The Medical Research Advancement grant is designed to support groundbreaking medical research initiatives. This funding will support new clinical trials, drug development, and public health initiatives aimed at improving patient outcomes and advancing scientific knowledge in fields such as oncology, neuroscience, and infectious diseases.', '2025-11-15', 220000),
(4, 'Business Innovation Fund', 9, 'Institution', '2025-04-15', 'The Business Innovation Fund supports businesses working on innovative projects that aim to create new market opportunities and solve pressing global challenges. The fund will focus on startups and small businesses that are developing breakthrough technologies, services, or processes with the potential for significant industry disruption.', '2025-08-15', 85000),
(9, 'STEM Education Grant', 10, 'University', '2025-05-15', 'The STEM Education Grant is focused on increasing participation in science, technology, engineering, and math education, particularly among underrepresented groups. This grant will support the development of STEM curricula, teacher training programs, and student engagement initiatives to encourage more students to pursue STEM careers.', '2025-09-15', 140000);

INSERT INTO grantStaff(GrantID, UserID, UserRole)
VALUES
(1, 1, 'Principal Investigator'),
(1, 2, 'Co-PI'),

(2, 3, 'Principal Investigator'),
(2, 4, 'Researcher'),

(3, 5, 'Principal Investigator'),
(3, 6, 'Researcher'),

(4, 1, 'Principal Investigator'),
(4, 2, 'Research Assistant'),

(5, 3, 'Principal Investigator'),
(5, 4, 'Data Scientist'),

(6, 5, 'Principal Investigator'),
(6, 6, 'Developer'),

(7, 1, 'Principal Investigator'),
(7, 2, 'QA Engineer'),

(8, 3, 'Principal Investigator'),
(8, 4, 'Marketing Analyst'),

(9, 5, 'Principal Investigator'),
(9, 6, 'Clinical Researcher'),

(10, 1, 'Principal Investigator'),
(10, 2, 'Cybersecurity Specialist');

INSERT INTO grantTask (GrantID, DueDate, Objective, Completed)
VALUES
-- Grant 1
(1, '2025-02-15', 'Needs Assessment', 1),
(1, '2025-03-15', 'Data Collection', 1),
(1, '2025-04-15', 'Preliminary Analysis', 0),
(1, '2025-05-01', 'Industry Trends Analysis', 0),

-- Grant 2
(2, '2025-04-01', 'Stakeholder Interviews', 1),
(2, '2025-05-01', 'Curriculum Design', 0),
(2, '2025-07-01', 'Algorithm Development', 0),

-- Grant 3
(3, '2025-07-15', 'Pilot Testing', 1),
(3, '2025-09-01', 'Data Review', 0),
(3, '2025-11-01', 'Quality Assurance', 1),

-- Grant 4
(4, '2025-03-15', 'Literature Review', 1),
(4, '2025-04-01', 'Competitor Benchmarking', 0),
(4, '2025-06-01', 'White Paper Drafting', 0),

-- Grant 5
(5, '2025-05-01', 'Requirement Gathering', 1),
(5, '2025-06-01', 'UI/UX Wireframing', 0),
(5, '2025-07-01', 'Design Review', 0),

-- Grant 6
(6, '2025-06-15', 'Tool Design', 1),
(6, '2025-07-15', 'User Testing Prep', 0),
(6, '2025-08-01', 'Beta Testing', 0),

-- Grant 7
(7, '2025-08-01', 'System Setup', 1),
(7, '2025-09-01', 'Performance Optimization', 0),
(7, '2025-10-01', 'Documentation Finalization', 0),

-- Grant 8
(8, '2025-10-01', 'Deployment Plan', 1),
(8, '2025-11-01', 'Rollout Timeline Review', 0),
(8, '2025-12-01', 'Deployment Strategy', 0),

-- Grant 9
(9, '2025-02-01', 'Stakeholder Research', 1),
(9, '2025-03-01', 'User Needs Assessment', 0),
(9, '2025-04-01', 'Report Compilation', 0),

-- Grant 10
(10, '2025-07-01', 'Security Audit', 1),
(10, '2025-08-01', 'Penetration Testing', 0),
(10, '2025-09-01', 'Security Evaluation', 0);

INSERT INTO grantTaskStaff (TaskID, AssigneeID, AssignerID, DueDate)
VALUES
-- Grant 1 (Users: 1, 2, 3)
(1, 1, 1, '2025-02-15'),
(2, 2, 1, '2025-03-15'),
(3, 2, 1, '2025-04-15'),
(4, 1, 1, '2025-05-01'),

-- Grant 2 (Users: 4, 5)
(5, 4, 3, '2025-04-01'),
(6, 3, 3, '2025-05-01'),
(7, 4, 3, '2025-07-01'),

-- Grant 3 (Users: 6, 7)
(8, 6, 5, '2025-07-15'),
(9, 5, 5, '2025-09-01'),
(10, 6, 5, '2025-11-01'),

-- Grant 4 (Users: 8, 9)
(11, 1, 1, '2025-03-15'),
(12, 2, 1, '2025-04-01'),
(13, 2, 1, '2025-06-01'),

-- Grant 5 (Users: 10, 1)
(14, 3, 3, '2025-05-01'),
(15, 3, 3, '2025-06-01'),
(16, 4, 3, '2025-07-01'),

-- Grant 6 (Users: 2, 3)
(17, 5, 5, '2025-06-15'),
(18, 6, 5, '2025-07-15'),
(19, 6, 5, '2025-08-01'),

-- Grant 7 (Users: 4, 5)
(20, 1, 1, '2025-08-01'),
(21, 2, 1, '2025-09-01'),
(22, 2, 1, '2025-10-01'),

-- Grant 8 (Users: 6, 7)
(23, 3, 3, '2025-10-01'),
(24, 4, 3, '2025-11-01'),
(25, 3, 3, '2025-12-01'),

-- Grant 9 (Users: 8, 9)
(26, 5, 5, '2025-02-01'),
(27, 5, 5, '2025-03-01'),
(28, 6, 5, '2025-04-01'),

-- Grant 10 (Users: 10, 1)
(29, 2, 1, '2025-07-01'),
(30, 1, 1, '2025-08-01'),
(31, 2, 1, '2025-09-01');

INSERT INTO grantNotes (GrantID, AuthorID, Content)
VALUES
(1, 1, 'Took out trash'),
(2, 3, 'Emptied Dishwasher'),
(3, 5, 'Eggs are my favorite food'),
(4, 2, 'Dogs > Cats'),
(5, 4, 'Money money money money!'),
(6, 6, 'Elephant Ellipses'),
(7, 1, 'Eta more like beta'),
(8, 3, 'icloud.com/checkthispage'),
(9, 5, 'im running out of notes to write'),
(10, 2, 'thank god this is the last one');

INSERT INTO grantStatus (GrantID, StatusName, ChangeDate)
VALUES
(1, 'Active', '2025-02-01'),
(2, 'Pending', '2025-04-01'),
(3, 'Inactive', '2025-08-01'),
(4, 'Active', '2025-06-01'),
(5, 'Pending', '2025-08-01'),
(6, 'Inactive', '2025-09-01'),
(7, 'Active', '2025-07-01'),
(8, 'Inactive', '2025-09-01'),
(9, 'Inactive', '2025-11-01'),
(10, 'Active', '2025-05-01');

INSERT INTO funderStatus (FunderID, StatusName, ChangeDate)
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
(10, 'Active', '2025-02-15');

INSERT INTO userMessage (SenderID, RecipientID, SubjectTitle, Contents, SentTime)
VALUES
(1, 2, 'Hello', 'This is a test message', '2025-02-20 10:00:00'),
(2, 3, 'Reminder', 'Dont forget the meeting tomorrow', '2025-02-21 11:00:00'),
(3, 1, 'Thank you', 'Thanks for your help!', '2025-02-22 09:00:00'),
(4, 5, 'Meeting Update', 'The meeting has been rescheduled', '2025-02-23 08:00:00'),
(5, 6, 'Project Update', 'Here is the latest update on the project', '2025-02-24 07:00:00'),
(6, 4, 'Task Reminder', 'Dont forget to complete your tasks', '2025-02-25 06:00:00'),
(1, 7, 'Client Meeting', 'We have a meeting with the client tomorrow', '2025-02-26 05:00:00'),
(3, 7, 'Weekly Report', 'Please submit your weekly report', '2025-02-27 04:00:00'),
(1, 3, 'feedback request', 'Can you provide feedback', '2025-02-28 03:00:00'),
(2, 2, 'Team Lunch', 'We are having a team lunch on Friday', '2025-03-01 02:00:00');


INSERT INTO funderRep (UserID, CommunicationStatus, FunderID) VALUES
(1, 'Active', 1),
(2, 'Pending', 2),
(3, 'Active', 3),
(4, 'Inactive', 4),
(5, 'Active', 5),
(6, 'Pending', 6),
(7, 'Active', 7),
(8, 'Inactive', 8),
(2, 'Active', 9),  
(1, 'Pending', 10); 

INSERT INTO projectStatus (ProjectID, StatusName, ChangeDate) VALUES
(1, 'Planning', '2025-01-01'),
(1, 'In Progress', '2025-03-01'),
(2, 'Planning', '2025-02-15'),
(2, 'In Progress', '2025-04-01'),
(3, 'In Progress', '2025-06-10'),
(4, 'Completed', '2025-04-20'),
(5, 'Pending', '2025-03-01'),
(6, 'In Progress', '2025-07-01'),
(7, 'Completed', '2025-10-01'),
(8, 'In Review', '2025-12-01');