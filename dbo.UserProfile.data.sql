SET IDENTITY_INSERT [dbo].[UserProfile] ON
INSERT INTO [dbo].[UserProfile] ([UserProfileId], [ApplicationUserId], [ConfirmPassword], [Email], [FirstName], [LastName], [OldPassword], [Password]) VALUES (1, N'f72edf28-2d7c-4c07-b510-4f9e6ba9b81d', NULL, N'super@admin.com', N'Super', N'Admin', NULL, NULL)
INSERT INTO [dbo].[UserProfile] ([UserProfileId], [ApplicationUserId], [ConfirmPassword], [Email], [FirstName], [LastName], [OldPassword], [Password]) VALUES (2, N'fab33033-cf01-4ebb-9fb3-c07957ea0449', NULL, N'cmedders@amgen.com', N'cross', N'medders', NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
