INSERT INTO userMessage (SenderID, RecipientID, SubjectTitle, Contents)
VALUES 
(1, 2, 'Project Planning', 'Hey Nick, can you send me the updated timeline for the project?'),
(1, 3, 'System Access', 'Nadeem, are you still having trouble accessing the database?'),
(1, 4, 'Budget Review', 'Josh, let’s go over the budget for this quarter tomorrow.'),
(1, 5, 'Meeting Reschedule', 'Sharon, we need to move our meeting to Friday at 10 AM.'),
(1, 6, 'Training Session', 'Jeremy, can you confirm the attendees for next week’s training?'),
(1, 7, 'Report Submission', 'Hailey, the report is due by noon. Do you need any help finalizing it?'),
(1, 8, 'Supplier Follow-Up', 'Roy, have we received a response from the supplier yet?'),
(1, 9, 'Code Deployment', 'Ryan, let’s plan the deployment for this weekend.'),
(1, 10, 'Security Review', 'Dmytro, I’d like your input on the security updates before we push them live.'),
(1, 11, 'Database Migration', 'Sam O, we need to finalize the migration plan by Friday.');

-- Replies to Sam Ogden from various users
INSERT INTO userMessage (SenderID, RecipientID, SubjectTitle, Contents)
VALUES 
(2, 1, 'Re: Project Planning', 'Hey Sam, I’ll send you the updated timeline later today.'),
(3, 1, 'Re: System Access', 'I was able to log in after IT reset my credentials. Thanks!'),
(4, 1, 'Re: Budget Review', 'Sounds good, I’ll bring the latest numbers.'),
(5, 1, 'Re: Meeting Reschedule', 'Friday works for me. Thanks for letting me know.'),
(6, 1, 'Re: Training Session', 'All attendees confirmed. We’re good to go!'),
(7, 1, 'Re: Report Submission', 'I just need to double-check a few numbers, then I’ll submit it.'),
(8, 1, 'Re: Supplier Follow-Up', 'Yes, they responded. I’ll forward their email to you.'),
(9, 1, 'Re: Code Deployment', 'Let’s sync up tomorrow to plan it out.'),
(10, 1, 'Re: Security Review', 'I’ll review the updates and get back to you with my thoughts.'),
(11, 1, 'Re: Database Migration', 'I have a draft plan ready. Let’s discuss tomorrow.');

-- More back-and-forth messages between Sam Ogden and others
INSERT INTO userMessage (SenderID, RecipientID, SubjectTitle, Contents)
VALUES 
(1, 2, 'Follow-up on Project', 'Nick, I noticed some gaps in the timeline. Can we discuss?'),
(2, 1, 'Re: Follow-up on Project', 'Absolutely. I’ll set up a meeting for tomorrow.'),
(1, 3, 'Database Permissions', 'Nadeem, do you have the right permissions now?'),
(3, 1, 'Re: Database Permissions', 'Yes, IT resolved the issue. Thanks for checking.'),
(1, 4, 'Quarterly Review', 'Josh, let’s plan for the quarterly review next week.'),
(4, 1, 'Re: Quarterly Review', 'Let’s do it Tuesday. Does that work for you?'),
(1, 5, 'Presentation Prep', 'Sharon, do you need help with the slides for your presentation?'),
(5, 1, 'Re: Presentation Prep', 'Yes, I could use some feedback on the layout.'),
(1, 6, 'Tech Training Notes', 'Jeremy, can you share your notes from the last training session?'),
(6, 1, 'Re: Tech Training Notes', 'I’ll upload them to the shared folder shortly.'),
(1, 7, 'Marketing Strategy', 'Hailey, let’s meet to finalize the marketing strategy.'),
(7, 1, 'Re: Marketing Strategy', 'I’m available this afternoon. Let me know when you’re free.'),
(1, 8, 'Supplier Contract', 'Roy, let’s review the supplier contract before signing.'),
(8, 1, 'Re: Supplier Contract', 'I’ll go through it and highlight any concerns.'),
(1, 9, 'DevOps Pipeline', 'Ryan, do we have all the approvals for the new pipeline?'),
(9, 1, 'Re: DevOps Pipeline', 'Not yet. I’ll follow up with management today.'),
(1, 10, 'Cybersecurity Audit', 'Dmytro, we need to conduct an audit of our security policies.'),
(10, 1, 'Re: Cybersecurity Audit', 'I’ll draft a checklist and send it over.'),
(1, 11, 'Database Performance', 'Sam O, we should check the database performance logs.'),
(11, 1, 'Re: Database Performance', 'Good idea. I’ll pull the latest logs for analysis.');