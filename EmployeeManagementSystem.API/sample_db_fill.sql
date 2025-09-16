BEGIN TRY
    BEGIN TRAN;

-- ========================================
-- Step 1: Get UserIDs by UserName from AspNetUsers
-- ========================================
DECLARE @OmarUserID UNIQUEIDENTIFIER,
        @AdminUserID UNIQUEIDENTIFIER,
        @RogerUserID UNIQUEIDENTIFIER,
        @BesheerUserID UNIQUEIDENTIFIER;

SELECT @OmarUserID = Id
FROM AspNetUsers
WHERE UserName = 'Omar';

SELECT @AdminUserID = Id
FROM AspNetUsers
WHERE UserName = 'admin';

SELECT @RogerUserID = Id
FROM AspNetUsers
WHERE UserName = 'Roger';

SELECT @BesheerUserID = Id
FROM AspNetUsers
WHERE UserName = 'Besheer';

DECLARE @OmarRoleID UNIQUEIDENTIFIER,
        @AdminRoleID UNIQUEIDENTIFIER,
        @RogerRoleID UNIQUEIDENTIFIER,
        @BesheerRoleID UNIQUEIDENTIFIER;

SELECT @OmarRoleID = RoleId
FROM AspNetUserRoles
WHERE UserId = @OmarUserID;

SELECT @AdminRoleID = RoleId
FROM AspNetUserRoles
WHERE UserId = @AdminUserID;

SELECT @RogerRoleID = RoleId
FROM AspNetUserRoles
WHERE UserId = @RogerUserID;

SELECT @BesheerRoleID = RoleId
FROM AspNetUserRoles
WHERE UserId = @BesheerUserID;

-- ==============================================
INSERT INTO [EmployeeManagementDB].[dbo].[VacationLevel] ([Type1_Days], [Type2_Days])
VALUES (14, 7);
INSERT INTO [EmployeeManagementDB].[dbo].[VacationLevel] ([Type1_Days], [Type2_Days])
VALUES (8, 3);
INSERT INTO [EmployeeManagementDB].[dbo].[VacationLevel] ([Type1_Days], [Type2_Days])
VALUES (24, 11);

-- ========== ADMIN (top of hierarchy) ==========
DECLARE @AdminID INT;

INSERT INTO [EmployeeManagementDB].[dbo].[Employee]
(
    [National_ID],[First_Name],[Last_Name],[BirthDate],[HireDate],
    [Address],[Military_Status],[ManagerID],
    [Type1_Balance],[Type2_Balance],[Vacation_Level_ID],
    [EmployementStatus],[UserID]
)
VALUES
(
    '29912345648901','admin','ceo','1995-05-15',GETDATE(),
    '123 Not Main Street','Completed',NULL,
    200,30,1,1, @AdminUserID
);

SET @AdminID = SCOPE_IDENTITY();


-- ========== BESHEER (Manager under Admin) ==========
DECLARE @BesheerID INT;

INSERT INTO [EmployeeManagementDB].[dbo].[Employee]
(
    [National_ID],[First_Name],[Last_Name],[BirthDate],[HireDate],
    [Address],[Military_Status],[ManagerID],
    [Type1_Balance],[Type2_Balance],[Vacation_Level_ID],
    [EmployementStatus],[UserID]
)
VALUES
(
    '29912345648901','Besheer','ay 7aga','1995-05-15',GETDATE(),
    '123 Not Main Street','Completed',@AdminID,
    200,30,1,1,@BesheerUserID
);

SET @BesheerID = SCOPE_IDENTITY();


-- ========== ROGER (Head of Unit under Besheer) ==========
DECLARE @RogerID INT;

INSERT INTO [EmployeeManagementDB].[dbo].[Employee]
(
    [National_ID],[First_Name],[Last_Name],[BirthDate],[HireDate],
    [Address],[Military_Status],[ManagerID],
    [Type1_Balance],[Type2_Balance],[Vacation_Level_ID],
    [EmployementStatus],[UserID]
)
VALUES
(
    '21912345678901','Roger','Sherif','1995-05-15',GETDATE(),
    '123 Not Main Street','Completed',@BesheerID,
    20,3,1,1,@RogerUserID
);

SET @RogerID = SCOPE_IDENTITY();


-- ========== OMAR (Employee under Roger) ==========
DECLARE @OmarID INT;
INSERT INTO [EmployeeManagementDB].[dbo].[Employee]
(
    [National_ID],[First_Name],[Last_Name],[BirthDate],[HireDate],
    [Address],[Military_Status],[ManagerID],
    [Type1_Balance],[Type2_Balance],[Vacation_Level_ID],
    [EmployementStatus],[UserID]
)
VALUES
(
    '29912345678909','Omar','Dardir','1995-05-15',GETDATE(),
    '123 Main Street','Completed',@RogerID,
    14,11,1,1,@OmarUserID
);
SET @OmarID = SCOPE_IDENTITY();


-- ========== Inserting projecs ==========
DECLARE @NewProjects TABLE (ProjectID INT, ProjectName NVARCHAR(200));

INSERT INTO dbo.Project (ProjectHead, ProjectName, CustomerName, Status, Revenue, Expenses, CreatedAt)
OUTPUT inserted.ProjectID, inserted.ProjectName INTO @NewProjects
VALUES
(@AdminID, 'Cloud Migration',       'FutureTech Ltd.',    'In Progress',  60000.00, 30000.00, '2025-09-12 09:00:00.0000000'),
(@AdminID, 'AI Analytics Platform', 'DataVision Group',   'Planned',      90000.00, 10000.00, '2025-09-13 11:15:00.0000000'),
(@AdminID, 'E-Commerce Overhaul',   'RetailPro Solutions','Completed',    45000.00, 25000.00, '2025-09-10 14:45:00.0000000');

-- Extract IDs to variables for convenience
DECLARE @Proj1 INT = (SELECT ProjectID FROM @NewProjects WHERE ProjectName = 'Cloud Migration');
DECLARE @Proj2 INT = (SELECT ProjectID FROM @NewProjects WHERE ProjectName = 'AI Analytics Platform');
DECLARE @Proj3 INT = (SELECT ProjectID FROM @NewProjects WHERE ProjectName = 'E-Commerce Overhaul');

-- ============================================
-- (2) Insert 3 attendance sessions for Omar
-- ============================================
DECLARE @NewSessions TABLE (AttendanceID INT, WorkDate DATE);

INSERT INTO dbo.Attendance (EmployeeID, [Date], CheckIn, CheckOut, AFK_Time)
OUTPUT inserted.SessionID, inserted.[Date] INTO @NewSessions
VALUES
(@OmarID, '2025-09-15', '09:00:00', '17:00:00', '00:15:00'),
(@OmarID, '2025-09-16', '09:00:00', '17:00:00', '00:20:00'),
(@OmarID, '2025-09-17', '09:00:00', '17:00:00', '00:10:00');


-- ============================================
-- (3) For each session, insert 2 EmployeeProject logs with non-overlapping times
-- ============================================
DECLARE @Sess1 INT = (SELECT AttendanceID FROM @NewSessions WHERE WorkDate='2025-09-15');
DECLARE @Sess2 INT = (SELECT AttendanceID FROM @NewSessions WHERE WorkDate='2025-09-16');
DECLARE @Sess3 INT = (SELECT AttendanceID FROM @NewSessions WHERE WorkDate='2025-09-17');

-- Session 1 (Proj1 then Proj2)
INSERT INTO dbo.EmployeeProject (EmployeeID, ProjectID, SessionID, [Date], StartTime, EndTime)
VALUES
(@OmarID, @Proj1, @Sess1, '2025-09-15', '09:00:00', '12:30:00'),
(@OmarID, @Proj2, @Sess1, '2025-09-15', '13:00:00', '17:00:00');

-- Session 2 (Proj2 then Proj3)
INSERT INTO dbo.EmployeeProject (EmployeeID, ProjectID, SessionID, [Date], StartTime, EndTime)
VALUES
(@OmarID, @Proj2, @Sess2, '2025-09-16', '09:00:00', '12:00:00'),
(@OmarID, @Proj3, @Sess2, '2025-09-16', '13:00:00', '17:00:00');

-- Session 3 (Proj3 then Proj1)
INSERT INTO dbo.EmployeeProject (EmployeeID, ProjectID, SessionId, [Date], StartTime, EndTime)
VALUES
(@OmarID, @Proj3, @Sess3, '2025-09-17', '09:00:00', '12:00:00'),
(@OmarID, @Proj1, @Sess3, '2025-09-17', '13:00:00', '17:00:00');

-- =============================
-- (1) Omar - Approved Request #1
-- =============================
DECLARE @Req_Omar1 INT;

INSERT INTO dbo.VacationRequest (EmployeeID, RequestDate, StartDate, EndDate, Status, AwaitingApproval)
VALUES (@OmarID, GETDATE()-40, '2025-09-01', '2025-09-05', 'Approved', @AdminID);

SET @Req_Omar1 = SCOPE_IDENTITY();

INSERT INTO dbo.VacationApproval (RequestID, ApprovedByID, ApprovalDate, Decision, ApprovedByRoleID)
VALUES
(@Req_Omar1, @RogerID,  GETDATE()-39, 'Approved', @RogerRoleID),
(@Req_Omar1, @BesheerID,GETDATE()-38, 'Approved', @BesheerRoleID),
(@Req_Omar1, @AdminID,  GETDATE()-37, 'Approved', @AdminRoleID);


-- =============================
-- (2) Omar - Approved Request #2
-- =============================
DECLARE @Req_Omar2 INT;

INSERT INTO dbo.VacationRequest (EmployeeID, RequestDate, StartDate, EndDate, Status, AwaitingApproval)
VALUES (@OmarID, GETDATE()-25, '2025-09-10', '2025-09-14', 'Approved', @AdminID);

SET @Req_Omar2 = SCOPE_IDENTITY();

INSERT INTO dbo.VacationApproval (RequestID, ApprovedByID, ApprovalDate, Decision, ApprovedByRoleId)
VALUES
(@Req_Omar2, @RogerID,  GETDATE()-24, 'Approved', @RogerRoleID),
(@Req_Omar2, @BesheerID,GETDATE()-23, 'Approved', @BesheerRoleID),
(@Req_Omar2, @AdminID,  GETDATE()-22, 'Approved', @AdminRoleID);


-- =============================
-- (3) Omar - Rejected request
-- Roger approved then Besheer rejected (stops here)
-- =============================
DECLARE @Req_Omar3 INT;

INSERT INTO dbo.VacationRequest (EmployeeID, RequestDate, StartDate, EndDate, Status, AwaitingApproval)
VALUES (@OmarID, GETDATE()-10, '2025-09-20', '2025-09-22', 'Rejected', @BesheerID);

SET @Req_Omar3 = SCOPE_IDENTITY();

INSERT INTO dbo.VacationApproval (RequestID, ApprovedByID, ApprovalDate, Decision, ApprovedByRoleID)
VALUES
(@Req_Omar3, @RogerID,  GETDATE()-9, 'Approved', @RogerRoleID),
(@Req_Omar3, @BesheerID,GETDATE()-8, 'Rejected', @BesheerRoleID);


-- =============================
-- (4) Omar - Still pending (no approvals yet)
-- =============================
INSERT INTO dbo.VacationRequest (EmployeeID, RequestDate, StartDate, EndDate, Status, AwaitingApproval)
VALUES (@OmarID, GETDATE()-2, '2025-10-01', '2025-10-03', 'Pending', @RogerID);



-- =============================
-- (5) Roger - Approved vacation
-- Goes through Besheer -> Admin
-- =============================
DECLARE @Req_Roger1 INT;

INSERT INTO dbo.VacationRequest (EmployeeID, RequestDate, StartDate, EndDate, Status, AwaitingApproval)
VALUES (@RogerID, GETDATE()-15, '2025-09-25', '2025-09-28', 'Approved', @AdminID);

SET @Req_Roger1 = SCOPE_IDENTITY();

INSERT INTO dbo.VacationApproval (RequestID, ApprovedByID, ApprovalDate, Decision, ApprovedByRoleID)
VALUES
(@Req_Roger1, @BesheerID, GETDATE()-14, 'Approved', @BesheerRoleID),
(@Req_Roger1, @AdminID,   GETDATE()-13, 'Approved', @AdminRoleID);

COMMIT TRAN;
END TRY
BEGIN CATCH
    ROLLBACK TRAN;

    
    PRINT 'Error Number: ' + CAST(ERROR_NUMBER() AS NVARCHAR(10));
    PRINT 'Error Message: ' + ERROR_MESSAGE();
    PRINT 'Error Line: ' + CAST(ERROR_LINE() AS NVARCHAR(10));
END CATCH