/* BOARDS INSERT */
INSERT INTO Boards(Id, Name) VALUES(1, 'Platform Launch');
INSERT INTO Boards(Id, Name) VALUES(2, 'Marketing Plan');
INSERT INTO Boards(Id, Name) VALUES(3, 'Roadmap');

/* COLUMNS INSERT */
INSERT INTO Columns(Id, Name, BoardId) VALUES(1, 'Todo', 1);
INSERT INTO Columns(Id, Name, BoardId) VALUES(2, 'Doing', 1);
INSERT INTO Columns(Id, Name, BoardId) VALUES(3, 'Done', 1);

INSERT INTO Columns(Id, Name, BoardId) VALUES(4, 'Todo', 2);
INSERT INTO Columns(Id, Name, BoardId) VALUES(5, 'Doing', 2);
INSERT INTO Columns(Id, Name, BoardId) VALUES(6, 'Done', 2);

INSERT INTO Columns(Id, Name, BoardId) VALUES(7, 'Now', 3);
INSERT INTO Columns(Id, Name, BoardId) VALUES(8, 'Next', 3);
INSERT INTO Columns(Id, Name, BoardId) VALUES(9, 'Later', 3);


/* TASKS INSERT */
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(1, 1,'Build UI for onboarding flow', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(2, 1, 'Build UI for search', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(3, 1, 'Build settings UI', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(4, 1, 'QA and test all major user journeys', 'Once we feel version one is ready, we need to rigorously test it both internally and externally to identify any major gaps.');
		
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(5, 2, 'Design settings and search pages', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(6, 2, 'Add account management endpoints', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(7, 2, 'Design onboarding flow', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(8, 2, 'Add search enpoints', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(9, 2, 'Add authentication endpoints', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(10, 2, 'Research pricing points of various competitors and trial different business models', 'We know what we''re planning to build for version one. Now we need to finalise the first pricing model we''ll use. Keep iterating the subtasks until we have a coherent proposition.');
		
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(11, 3, 'Conduct 5 wireframe tests', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(12, 3, 'Create wireframe prototype', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(13, 3, 'Review results of usability tests and iterate', 'Keep iterating through the subtasks until we''re clear on the core concepts for the app.');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(14, 3, 'Create paper prototypes and conduct 10 usability tests with potential customers', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(15, 3, 'Market discovery', 'We need to define and refine our core product. Interviews will help us learn common pain points and help us define the strongest MVP.');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(16, 3, 'Competitor analysis', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(17, 3, 'Research the market', 'We need to get a solid overview of the market to ensure we have up-to-date estimates of market size and demand.');
		
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(18, 4, 'Plan Product Hunt launch', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(19, 4, 'Share on Show HN', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(20, 4, 'Write launch article to publish on multiple channels', '');
		
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(21, 7, 'Launch version one', '');
INSERT INTO Tasks(Id, ColumnId, Title, Description) VALUES(22, 7, 'Review early feedback and plan next steps for roadmap', 'Beyond the initial launch, we''re keeping the initial roadmap completely empty. This meeting will help us plan out our next steps based on actual customer feedback.');


/* SUBTASKS INSERT */
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(1, 1, 'Sign up page', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(2, 1, 'Sign in page', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(3, 1, 'Welcome page', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(4, 2, 'Search page', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(5, 3, '"Account page', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(6, 3, 'Billing page', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(7, 4, 'Internal testing', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(8, 4, 'External testing', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(9, 5, 'Settings - Account page', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(10, 5, 'Settings - Billing page', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(11, 5, 'Search page', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(12, 6, 'Upgrade plan', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(13, 6, 'Cancel plan', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(14, 6, 'Update payment method', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(15, 7, 'Sign up page', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(16, 7, 'Sign in page', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(17, 7, 'Welcome page', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(18, 8, 'Add search endpoint', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(19, 8, 'Define search filters', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(20, 9, 'Define user model', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(21, 9, 'Add auth endpoints', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(22, 10, 'Research competitor pricing and business models', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(23, 10, 'Outline a business model that works for our solution', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(24, 10, 'Talk to potential customers about our proposed solution and ask for fair price expectancy', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(25, 11, 'Complete 5 wireframe prototype tests', 'true');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(26, 12, 'Create clickable wireframe prototype in Balsamiq', 'true');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(27, 13, 'Meet to review notes from previous tests and plan changes', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(28, 13, 'Make changes to paper prototypes', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(29, 13, 'Conduct 5 usability tests', 'true');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(30, 14, 'Create paper prototypes for version one', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(31, 14, 'Complete 10 usability tests', 'true');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(32, 15, 'Interview 10 prospective customers', 'true');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(33, 16, 'Find direct and indirect competitors', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(34, 16, 'SWOT analysis for each competitor', 'true');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(35, 17, 'Write up research analysis', 'true');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(36, 17, 'Calculate TAM', 'true');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(37, 18, 'Find hunter', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(38, 18, 'Gather assets', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(39, 18, 'Draft product page', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(40, 18, 'Notify customers', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(41, 18, 'Notify network', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(42, 18, 'Launch!', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(43, 19, 'Draft out HN post', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(44, 19, 'Get feedback and refine', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(45, 19, 'Publish post', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(46, 20, 'Write article', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(47, 20, 'Publish on LinkedIn', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(48, 20, 'Publish on Inndie Hackers', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(49, 20, 'Publish on Medium', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(50, 21, 'Launch privately to our waitlist', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(51, 21, 'Launch publicly on PH, HN, etc.', 'false');

INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(52, 22, 'Interview 10 customers', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(53, 22, 'Review common customer pain points and suggestions', 'false');
INSERT INTO Subtasks(Id, TaskId, Title, Completed) VALUES(54, 22, 'Outline next steps for our roadmap', 'false');