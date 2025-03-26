INSERT INTO users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress, PFPFilePath, AdminStatus, ViewArchives) VALUES
('johnDoe', 'password123', 'John', 'Doe', 'john.doe@example.com', '123-456-7890', '123 Elm St, Harrisonburg, VA', '/pfp/johndoe.jpg', 1, 1),
('janeSmith', 'securepwd', 'Jane', 'Smith', 'jane.smith@example.com', '987-654-3210', '456 Oak St, Harrisonburg, VA', '/pfp/janesmith.jpg', 0, 1),
('markJohnson', 'abcd@123', 'Mark', 'Johnson', 'mark.j@example.com', '567-890-1234', '789 Birch St, Harrisonburg, VA', '/pfp/markjohnson.jpg', 0, 0),
('annaBrown', 'qwerty456', 'Anna', 'Brown', 'anna.brown@example.com', '234-567-8901', '321 Cedar St, Harrisonburg, VA', '/pfp/annabrown.jpg', 1, 1),
('lisaRay', 'lisaSecure789', 'Lisa', 'Ray', 'lisa.ray@example.com', '345-678-9012', '654 Maple St, Harrisonburg, VA', '/pfp/lisaray.jpg', 0, 0);

INSERT INTO project (ProjectName, DueDate) VALUES
('Website Redesign', '2025-05-20'),
('CRM Development', '2025-06-15'),
('Community Outreach', '2025-07-01'),
('New Marketing Campaign', '2025-08-10'),
('Cloud Migration', '2025-09-15');

INSERT INTO grantSupplier (SupplierName, OrgType, Address) VALUES
('Tech Supplies Inc.', 'Corporate', '789 Pine St, Harrisonburg, VA'),
('Global Solutions Co.', 'Non-Profit', '101 Maple St, Harrisonburg, VA'),
('Innovative Partners', 'Partnership', '222 Elm St, Harrisonburg, VA'),
('Creative Minds Ltd.', 'LLC', '333 Oak St, Harrisonburg, VA'),
('Charity Foundation', 'Foundation', '555 Cedar St, Harrisonburg, VA');

INSERT INTO supplierStatus (SupplierID, StatusName, ChangeDate) VALUES
(1, 'Active', '2025-03-01'),
(2, 'Inactive', '2025-03-10'),
(3, 'Pending', '2025-03-15'),
(4, 'Active', '2025-03-20'),
(5, 'Inactive', '2025-03-25');

INSERT INTO BPrep (UserID, CommunicationStatus, SupplierID) VALUES
(1, 'In Progress', 1),
(2, 'Completed', 2),
(3, 'Pending', 3),
(4, 'Awaiting Response', 4),
(5, 'Completed', 5);

INSERT INTO grants (GrantName, SupplierID, ProjectID, StatusName, Category, SubmissionDate, descriptions, AwardDate, Amount) VALUES
('Education Grant', 1, 1, 'Pending', 'Education', '2025-02-01', 'Funds for education programs', '2025-02-15', 50000),
('Tech Innovation Grant', 2, 2, 'Awarded', 'Technology', '2025-03-05', 'Funds for tech innovation', '2025-03-20', 75000),
('Health Research Grant', 3, 3, 'Under Review', 'Health', '2025-04-01', 'Research in health sciences', '2025-04-20', 100000),
('Community Development Grant', 4, 4, 'Pending', 'Community', '2025-05-01', 'Development programs for the community', '2025-05-15', 25000),
('Environmental Grant', 5, 5, 'Awarded', 'Environment', '2025-06-01', 'Projects on environmental sustainability', '2025-06-20', 40000);

INSERT INTO grantStaff (StaffedID, StafferID, GrantID, Active, ViewGrant, EditGrant, PIStatus) VALUES
(1, 2, 1, 1, 1, 0, 0),
(2, 1, 2, 1, 1, 1, 1),
(3, 4, 3, 1, 1, 0, 1),
(4, 3, 4, 1, 1, 1, 0),
(5, 5, 5, 1, 1, 0, 1);

INSERT INTO projectStaff (StaffedID, StafferID, ProjectID, Active, ViewProject, EditProject, PMStatus) VALUES
(1, 2, 1, 1, 1, 0, 0),
(2, 1, 2, 1, 1, 1, 1),
(3, 4, 3, 1, 1, 0, 1),
(4, 3, 4, 1, 1, 1, 0),
(5, 5, 5, 1, 1, 0, 1);

INSERT INTO note (ProjectID, NoteContents, DatePosted) VALUES
(1, 'Initial brainstorming completed.', '2025-03-10'),
(2, 'Technical feasibility reviewed.', '2025-03-12'),
(3, 'Stakeholder inputs gathered.', '2025-03-15'),
(4, 'Budget estimates finalized.', '2025-03-18'),
(5, 'Draft proposal submitted.', '2025-03-20');

INSERT INTO meeting (ProjectID, MeetingDate, Purpose) VALUES
(1, '2025-03-18', 'Planning Discussion'),
(2, '2025-03-20', 'Progress Update'),
(3, '2025-03-25', 'Stakeholder Meeting'),
(4, '2025-03-28', 'Risk Analysis'),
(5, '2025-04-01', 'Final Presentation');

INSERT INTO grantTask (GrantID, DueDate, Objective) VALUES
(1, '2025-04-01', 'Prepare grant application'),
(2, '2025-04-10', 'Review budget allocation'),
(3, '2025-04-15', 'Draft project proposal'),
(4, '2025-04-20', 'Submit supporting documents'),
(5, '2025-04-25', 'Final review and submission');

INSERT INTO grantTaskStaff (GrantID, AssigneeID, AssignerID) VALUES
(1, 1, 2),
(2, 3, 4),
(3, 2, 1),
(4, 4, 5),
(5, 5, 3);

INSERT INTO projectTask (ProjectID, DueDate, Objective) VALUES
(1, '2025-03-25', 'Create website prototype'),
(2, '2025-03-30', 'Develop CRM user authentication'),
(3, '2025-04-05', 'Outline community outreach strategies'),
(4, '2025-04-10', 'Design marketing materials'),
(5, '2025-04-15', 'Implement cloud storage solution');

INSERT INTO projectTaskStaff (ProjectTaskID, AssigneeID, AssignerID, DueDate) VALUES
(1, 1, 2, '2025-03-25'),
(2, 2, 3, '2025-03-30'),
(3, 3, 4, '2025-04-05'),
(4, 4, 5, '2025-04-10'),
(5, 5, 1, '2025-04-15');

INSERT INTO meetingMinutes (MeetingID, AuthorID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

INSERT INTO attendance (MeetingID, UserID) VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 5),
(3, 1),
(4, 2),
(4, 3),
(5, 4),
(5, 5);

INSERT INTO userMessage (SenderID, SubjectTitle, Contents) VALUES
(1, 'Website Project Update', 'The initial design phase has been completed.'),
(2, 'CRM System Milestone', 'We’ve achieved the authentication module integration.'),
(3, 'Community Outreach', 'Gathering volunteer responses for the next meeting.'),
(4, 'Marketing Campaign Progress', 'We’ve finalized the promotional materials.'),
(5, 'Cloud Migration Report', 'The migration process has begun successfully.');

INSERT INTO messageRecipient (MessageID, RecipientID) VALUES
(1, 2),
(2, 3),
(3, 4),
(4, 5),
(5, 1);
