
-- =============================================
-- Author:		Vivian
-- Create date: 2019-11 
-- Description:	Populate lat/long in [zebra].[Stations] for the first time
-- =============================================
If Exists (Select 1 From [zebra].[Stations] Where [Latitude] IS NULL)
BEGIN
Declare @tbl_latLong table ([StationId] int, [Latitude] Decimal(10, 5), [Longitude] Decimal(10, 5))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6, CAST(57.09279 AS Decimal(10, 5)), CAST(9.84916 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8, CAST(62.56037 AS Decimal(10, 5)), CAST(6.11016 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (15, CAST(56.30230 AS Decimal(10, 5)), CAST(10.62627 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (16, CAST(68.72134 AS Decimal(10, 5)), CAST(-52.78854 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (18, CAST(30.37111 AS Decimal(10, 5)), CAST(48.22833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (20, CAST(53.74000 AS Decimal(10, 5)), CAST(91.38500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (23, CAST(49.02529 AS Decimal(10, 5)), CAST(-122.37735 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (26, CAST(13.85000 AS Decimal(10, 5)), CAST(20.85000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (30, CAST(57.20194 AS Decimal(10, 5)), CAST(-2.19778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (33, CAST(45.44970 AS Decimal(10, 5)), CAST(-98.42148 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (35, CAST(18.24037 AS Decimal(10, 5)), CAST(42.65662 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (36, CAST(5.26139 AS Decimal(10, 5)), CAST(-3.92629 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (37, CAST(32.42013 AS Decimal(10, 5)), CAST(-99.85731 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (39, CAST(32.41124 AS Decimal(10, 5)), CAST(-99.68203 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (43, CAST(5.43329 AS Decimal(10, 5)), CAST(-3.21640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (46, CAST(24.43297 AS Decimal(10, 5)), CAST(54.65114 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (52, CAST(22.37571 AS Decimal(10, 5)), CAST(31.61170 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (53, CAST(9.00657 AS Decimal(10, 5)), CAST(7.26419 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (54, CAST(8.49800 AS Decimal(10, 5)), CAST(-77.27400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (55, CAST(16.75706 AS Decimal(10, 5)), CAST(-99.75395 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (59, CAST(5.60519 AS Decimal(10, 5)), CAST(-0.16679 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (61, CAST(9.18768 AS Decimal(10, 5)), CAST(-77.99402 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (63, CAST(51.87796 AS Decimal(10, 5)), CAST(-176.64603 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (64, CAST(36.98217 AS Decimal(10, 5)), CAST(35.28039 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (68, CAST(8.97792 AS Decimal(10, 5)), CAST(38.79805 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (69, CAST(-34.94500 AS Decimal(10, 5)), CAST(138.53056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (72, CAST(12.82954 AS Decimal(10, 5)), CAST(45.02879 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (73, CAST(37.73135 AS Decimal(10, 5)), CAST(38.46914 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (74, CAST(43.44993 AS Decimal(10, 5)), CAST(39.95659 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (75, CAST(27.83759 AS Decimal(10, 5)), CAST(-0.18641 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (77, CAST(1.39924 AS Decimal(10, 5)), CAST(99.43193 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (82, CAST(16.96600 AS Decimal(10, 5)), CAST(8.00011 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (83, CAST(30.32500 AS Decimal(10, 5)), CAST(-9.41307 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (84, CAST(23.88698 AS Decimal(10, 5)), CAST(91.24045 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (85, CAST(10.82370 AS Decimal(10, 5)), CAST(72.17600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (89, CAST(44.17472 AS Decimal(10, 5)), CAST(0.59056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (94, CAST(27.15583 AS Decimal(10, 5)), CAST(77.96089 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (95, CAST(39.65454 AS Decimal(10, 5)), CAST(43.02598 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (99, CAST(18.49331 AS Decimal(10, 5)), CAST(-67.13471 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (100, CAST(21.70556 AS Decimal(10, 5)), CAST(-102.31786 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (102, CAST(-14.42810 AS Decimal(10, 5)), CAST(-146.25700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (104, CAST(23.07724 AS Decimal(10, 5)), CAST(72.63465 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (106, CAST(31.33743 AS Decimal(10, 5)), CAST(48.76195 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (113, CAST(10.21587 AS Decimal(10, 5)), CAST(169.98148 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (122, CAST(7.27936 AS Decimal(10, 5)), CAST(168.82562 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (127, CAST(-18.83079 AS Decimal(10, 5)), CAST(-159.76490 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (132, CAST(23.83788 AS Decimal(10, 5)), CAST(92.62024 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (133, CAST(41.92364 AS Decimal(10, 5)), CAST(8.80292 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (139, CAST(60.90481 AS Decimal(10, 5)), CAST(-161.22702 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (141, CAST(39.61556 AS Decimal(10, 5)), CAST(140.21861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (143, CAST(68.22333 AS Decimal(10, 5)), CAST(-135.00583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (146, CAST(40.91617 AS Decimal(10, 5)), CAST(-81.44234 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (150, CAST(41.26250 AS Decimal(10, 5)), CAST(80.29170 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (151, CAST(43.86005 AS Decimal(10, 5)), CAST(51.09198 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (152, CAST(50.24583 AS Decimal(10, 5)), CAST(57.20667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (153, CAST(60.81861 AS Decimal(10, 5)), CAST(-78.14861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (155, CAST(7.24674 AS Decimal(10, 5)), CAST(5.30101 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (156, CAST(65.65999 AS Decimal(10, 5)), CAST(-18.07270 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (157, CAST(54.13338 AS Decimal(10, 5)), CAST(-165.77792 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (159, CAST(24.26167 AS Decimal(10, 5)), CAST(55.60917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (162, CAST(35.17710 AS Decimal(10, 5)), CAST(-3.83953 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (169, CAST(25.28531 AS Decimal(10, 5)), CAST(49.48519 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (170, CAST(62.67969 AS Decimal(10, 5)), CAST(-164.64894 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (176, CAST(37.43473 AS Decimal(10, 5)), CAST(-105.86724 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (179, CAST(20.29513 AS Decimal(10, 5)), CAST(41.64016 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (180, CAST(31.53545 AS Decimal(10, 5)), CAST(-84.19435 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (183, CAST(42.74813 AS Decimal(10, 5)), CAST(-73.80234 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (187, CAST(-34.94583 AS Decimal(10, 5)), CAST(117.80806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (194, CAST(35.04199 AS Decimal(10, 5)), CAST(-106.60697 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (195, CAST(-36.06778 AS Decimal(10, 5)), CAST(146.95806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (198, CAST(58.60317 AS Decimal(10, 5)), CAST(125.40730 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (216, CAST(30.91767 AS Decimal(10, 5)), CAST(29.69641 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (220, CAST(31.32243 AS Decimal(10, 5)), CAST(-92.54290 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (226, CAST(40.85587 AS Decimal(10, 5)), CAST(25.95626 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (230, CAST(40.63213 AS Decimal(10, 5)), CAST(8.29077 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (231, CAST(36.69850 AS Decimal(10, 5)), CAST(3.20672 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (233, CAST(38.28217 AS Decimal(10, 5)), CAST(-0.55816 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (236, CAST(-23.80364 AS Decimal(10, 5)), CAST(133.90038 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (240, CAST(56.89919 AS Decimal(10, 5)), CAST(-154.25013 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (242, CAST(25.44006 AS Decimal(10, 5)), CAST(81.73387 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (243, CAST(66.54959 AS Decimal(10, 5)), CAST(-152.63136 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (245, CAST(40.65253 AS Decimal(10, 5)), CAST(-75.43554 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (246, CAST(42.05303 AS Decimal(10, 5)), CAST(-102.80409 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (251, CAST(43.35207 AS Decimal(10, 5)), CAST(77.04051 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (254, CAST(36.84394 AS Decimal(10, 5)), CAST(-2.37010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (256, CAST(-8.13234 AS Decimal(10, 5)), CAST(124.59700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (257, CAST(6.19423 AS Decimal(10, 5)), CAST(100.40234 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (258, CAST(-10.31108 AS Decimal(10, 5)), CAST(150.33756 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (261, CAST(45.07807 AS Decimal(10, 5)), CAST(-83.56026 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (265, CAST(69.97611 AS Decimal(10, 5)), CAST(23.37167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (266, CAST(-9.86609 AS Decimal(10, 5)), CAST(-56.10621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (268, CAST(46.37640 AS Decimal(10, 5)), CAST(96.22110 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (269, CAST(-3.25391 AS Decimal(10, 5)), CAST(-52.25398 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (270, CAST(47.75223 AS Decimal(10, 5)), CAST(88.08738 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (272, CAST(47.48500 AS Decimal(10, 5)), CAST(9.56194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (279, CAST(40.29619 AS Decimal(10, 5)), CAST(-78.32001 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (292, CAST(28.43063 AS Decimal(10, 5)), CAST(129.71254 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (294, CAST(35.21922 AS Decimal(10, 5)), CAST(-101.70629 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (305, CAST(67.10236 AS Decimal(10, 5)), CAST(-157.85951 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (308, CAST(-3.71026 AS Decimal(10, 5)), CAST(128.08914 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (309, CAST(-2.64505 AS Decimal(10, 5)), CAST(37.25310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (322, CAST(31.97270 AS Decimal(10, 5)), CAST(35.99157 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (324, CAST(31.72256 AS Decimal(10, 5)), CAST(35.99321 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (332, CAST(31.70959 AS Decimal(10, 5)), CAST(74.79726 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (333, CAST(52.31030 AS Decimal(10, 5)), CAST(4.76028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (335, CAST(26.25940 AS Decimal(10, 5)), CAST(105.87284 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (336, CAST(-17.34908 AS Decimal(10, 5)), CAST(-145.51229 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (340, CAST(64.73495 AS Decimal(10, 5)), CAST(177.74148 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (342, CAST(52.45384 AS Decimal(10, 5)), CAST(-125.30615 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (343, CAST(68.13738 AS Decimal(10, 5)), CAST(-151.73936 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (346, CAST(45.00210 AS Decimal(10, 5)), CAST(37.34727 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (349, CAST(61.17417 AS Decimal(10, 5)), CAST(-149.99611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (352, CAST(43.61634 AS Decimal(10, 5)), CAST(13.36232 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (354, CAST(-13.70641 AS Decimal(10, 5)), CAST(-73.35038 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (359, CAST(69.29250 AS Decimal(10, 5)), CAST(16.14417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (363, CAST(40.72770 AS Decimal(10, 5)), CAST(72.29400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (372, CAST(18.72682 AS Decimal(10, 5)), CAST(-64.32984 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (373, CAST(-20.24920 AS Decimal(10, 5)), CAST(169.77100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (375, CAST(56.29597 AS Decimal(10, 5)), CAST(12.85555 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (380, CAST(47.56254 AS Decimal(10, 5)), CAST(-0.31268 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (383, CAST(53.24810 AS Decimal(10, 5)), CAST(-4.53534 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (384, CAST(53.84848 AS Decimal(10, 5)), CAST(-89.57849 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (391, CAST(18.20483 AS Decimal(10, 5)), CAST(-63.05508 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (394, CAST(61.58139 AS Decimal(10, 5)), CAST(-159.54278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (396, CAST(-19.23728 AS Decimal(10, 5)), CAST(169.60093 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (397, CAST(-12.13167 AS Decimal(10, 5)), CAST(44.43028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (399, CAST(40.12808 AS Decimal(10, 5)), CAST(32.99508 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (407, CAST(42.22282 AS Decimal(10, 5)), CAST(-83.74550 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (408, CAST(36.82889 AS Decimal(10, 5)), CAST(7.81278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (419, CAST(30.58220 AS Decimal(10, 5)), CAST(117.05000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (422, CAST(41.10530 AS Decimal(10, 5)), CAST(122.85400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (423, CAST(-9.34744 AS Decimal(10, 5)), CAST(-77.59839 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (424, CAST(-14.99941 AS Decimal(10, 5)), CAST(50.32023 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (425, CAST(36.89873 AS Decimal(10, 5)), CAST(30.80046 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (426, CAST(-18.79695 AS Decimal(10, 5)), CAST(47.47881 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (430, CAST(17.13675 AS Decimal(10, 5)), CAST(-61.79267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (431, CAST(10.76780 AS Decimal(10, 5)), CAST(121.93350 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (433, CAST(-23.44448 AS Decimal(10, 5)), CAST(-70.44510 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (436, CAST(-12.34940 AS Decimal(10, 5)), CAST(49.29175 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (440, CAST(62.64639 AS Decimal(10, 5)), CAST(-160.19056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (443, CAST(40.73472 AS Decimal(10, 5)), CAST(140.69083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (448, CAST(7.81196 AS Decimal(10, 5)), CAST(-76.71643 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (449, CAST(-15.57360 AS Decimal(10, 5)), CAST(-146.41499 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (452, CAST(-13.84833 AS Decimal(10, 5)), CAST(-171.74166 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (453, CAST(-13.82997 AS Decimal(10, 5)), CAST(-172.00834 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (459, CAST(44.25721 AS Decimal(10, 5)), CAST(-88.51927 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (463, CAST(29.61162 AS Decimal(10, 5)), CAST(35.01807 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (464, CAST(34.13815 AS Decimal(10, 5)), CAST(49.84729 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (465, CAST(-10.98400 AS Decimal(10, 5)), CAST(-37.07033 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (466, CAST(-21.14130 AS Decimal(10, 5)), CAST(-50.42470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (470, CAST(-7.22787 AS Decimal(10, 5)), CAST(-48.24050 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (478, CAST(30.90659 AS Decimal(10, 5)), CAST(41.13822 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (479, CAST(-0.60055 AS Decimal(10, 5)), CAST(-72.39883 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (483, CAST(-15.48561 AS Decimal(10, 5)), CAST(-145.46705 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (484, CAST(7.06889 AS Decimal(10, 5)), CAST(-70.73750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (487, CAST(-19.56320 AS Decimal(10, 5)), CAST(-46.96040 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (488, CAST(6.03939 AS Decimal(10, 5)), CAST(37.59045 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (494, CAST(40.97164 AS Decimal(10, 5)), CAST(-124.10709 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (496, CAST(73.00598 AS Decimal(10, 5)), CAST(-85.03294 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (497, CAST(68.11495 AS Decimal(10, 5)), CAST(-145.57981 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (498, CAST(38.32639 AS Decimal(10, 5)), CAST(48.42444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (505, CAST(-16.34361 AS Decimal(10, 5)), CAST(-71.56863 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (511, CAST(-18.34853 AS Decimal(10, 5)), CAST(-70.33874 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (513, CAST(-10.18639 AS Decimal(10, 5)), CAST(-59.45750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (516, CAST(64.59828 AS Decimal(10, 5)), CAST(40.71276 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (523, CAST(4.45277 AS Decimal(10, 5)), CAST(-75.76645 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (525, CAST(-30.52810 AS Decimal(10, 5)), CAST(151.61700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (534, CAST(-9.86114 AS Decimal(10, 5)), CAST(161.98010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (537, CAST(-26.69317 AS Decimal(10, 5)), CAST(141.04780 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (543, CAST(24.62900 AS Decimal(10, 5)), CAST(-75.67306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (545, CAST(3.05000 AS Decimal(10, 5)), CAST(30.91667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (546, CAST(12.50385 AS Decimal(10, 5)), CAST(-70.00780 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (548, CAST(-3.36760 AS Decimal(10, 5)), CAST(36.62480 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (549, CAST(-15.23592 AS Decimal(10, 5)), CAST(-146.65310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (551, CAST(61.09411 AS Decimal(10, 5)), CAST(-94.07164 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (552, CAST(65.59076 AS Decimal(10, 5)), CAST(19.28169 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (555, CAST(43.67083 AS Decimal(10, 5)), CAST(142.44750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (569, CAST(35.43733 AS Decimal(10, 5)), CAST(-82.53895 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (571, CAST(37.98681 AS Decimal(10, 5)), CAST(58.36097 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (572, CAST(38.36647 AS Decimal(10, 5)), CAST(-82.55794 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (578, CAST(15.29185 AS Decimal(10, 5)), CAST(38.91067 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (579, CAST(10.01855 AS Decimal(10, 5)), CAST(34.58625 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (580, CAST(39.22304 AS Decimal(10, 5)), CAST(-106.86921 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (583, CAST(27.04651 AS Decimal(10, 5)), CAST(31.01198 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (584, CAST(51.02222 AS Decimal(10, 5)), CAST(71.46694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (587, CAST(46.28333 AS Decimal(10, 5)), CAST(48.00639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (589, CAST(43.56357 AS Decimal(10, 5)), CAST(-6.03462 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (590, CAST(36.57990 AS Decimal(10, 5)), CAST(26.37580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (591, CAST(-25.23985 AS Decimal(10, 5)), CAST(-57.51913 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (592, CAST(23.96436 AS Decimal(10, 5)), CAST(32.81997 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (593, CAST(-9.07468 AS Decimal(10, 5)), CAST(124.90477 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (596, CAST(20.50683 AS Decimal(10, 5)), CAST(-13.04319 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (601, CAST(37.93636 AS Decimal(10, 5)), CAST(23.94447 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (610, CAST(52.22016 AS Decimal(10, 5)), CAST(-174.20673 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (613, CAST(33.87737 AS Decimal(10, 5)), CAST(-84.30456 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (616, CAST(33.64099 AS Decimal(10, 5)), CAST(-84.42265 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (626, CAST(60.86675 AS Decimal(10, 5)), CAST(-162.27314 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (627, CAST(-8.87333 AS Decimal(10, 5)), CAST(161.01100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (628, CAST(70.46802 AS Decimal(10, 5)), CAST(-157.43134 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (630, CAST(52.92750 AS Decimal(10, 5)), CAST(-82.43190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (635, CAST(-9.76578 AS Decimal(10, 5)), CAST(-139.00907 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (636, CAST(47.12190 AS Decimal(10, 5)), CAST(51.82140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (643, CAST(-37.00806 AS Decimal(10, 5)), CAST(174.79167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (648, CAST(33.36986 AS Decimal(10, 5)), CAST(-81.96428 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (651, CAST(44.32050 AS Decimal(10, 5)), CAST(-69.79721 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (653, CAST(-8.70257 AS Decimal(10, 5)), CAST(160.68201 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (656, CAST(59.29667 AS Decimal(10, 5)), CAST(-69.59972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (658, CAST(19.86273 AS Decimal(10, 5)), CAST(75.39811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (660, CAST(44.89139 AS Decimal(10, 5)), CAST(2.42194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (662, CAST(-13.35390 AS Decimal(10, 5)), CAST(141.72099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (665, CAST(30.19453 AS Decimal(10, 5)), CAST(-97.66987 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (676, CAST(43.90700 AS Decimal(10, 5)), CAST(4.90200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (685, CAST(7.06667 AS Decimal(10, 5)), CAST(38.50000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (687, CAST(14.14675 AS Decimal(10, 5)), CAST(38.77283 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (689, CAST(-13.15482 AS Decimal(10, 5)), CAST(-74.20442 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (692, CAST(-25.18910 AS Decimal(10, 5)), CAST(130.97754 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (700, CAST(-2.53224 AS Decimal(10, 5)), CAST(133.43900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (703, CAST(46.52195 AS Decimal(10, 5)), CAST(26.91028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (705, CAST(10.77614 AS Decimal(10, 5)), CAST(123.01907 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (706, CAST(38.89125 AS Decimal(10, 5)), CAST(-6.82133 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (711, CAST(-10.15004 AS Decimal(10, 5)), CAST(142.17476 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (712, CAST(5.53692 AS Decimal(10, 5)), CAST(10.35460 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (717, CAST(26.68121 AS Decimal(10, 5)), CAST(88.32857 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (719, CAST(33.25850 AS Decimal(10, 5)), CAST(44.23285 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (723, CAST(48.33055 AS Decimal(10, 5)), CAST(-70.99639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (725, CAST(11.60807 AS Decimal(10, 5)), CAST(37.32164 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (727, CAST(29.34810 AS Decimal(10, 5)), CAST(71.71800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (729, CAST(-38.72868 AS Decimal(10, 5)), CAST(-62.15301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (733, CAST(7.58465 AS Decimal(10, 5)), CAST(-78.18105 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (734, CAST(6.20292 AS Decimal(10, 5)), CAST(-77.39470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (735, CAST(26.27083 AS Decimal(10, 5)), CAST(50.63361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (737, CAST(47.65839 AS Decimal(10, 5)), CAST(23.47002 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (740, CAST(49.13250 AS Decimal(10, 5)), CAST(-68.20444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (746, CAST(23.71967 AS Decimal(10, 5)), CAST(106.96341 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (748, CAST(-8.81337 AS Decimal(10, 5)), CAST(120.99865 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (752, CAST(3.97400 AS Decimal(10, 5)), CAST(115.61800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (756, CAST(64.29889 AS Decimal(10, 5)), CAST(-96.07778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (757, CAST(35.43329 AS Decimal(10, 5)), CAST(-119.05760 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (760, CAST(40.46075 AS Decimal(10, 5)), CAST(50.05154 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (765, CAST(-6.98913 AS Decimal(10, 5)), CAST(155.88758 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (767, CAST(8.91920 AS Decimal(10, 5)), CAST(-79.59968 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (772, CAST(46.89330 AS Decimal(10, 5)), CAST(75.00500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (777, CAST(-1.26827 AS Decimal(10, 5)), CAST(116.89400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (779, CAST(-28.83390 AS Decimal(10, 5)), CAST(153.56200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (782, CAST(-45.91606 AS Decimal(10, 5)), CAST(-71.68948 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (785, CAST(39.17539 AS Decimal(10, 5)), CAST(-76.66802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (792, CAST(29.08417 AS Decimal(10, 5)), CAST(58.45004 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (793, CAST(-10.95080 AS Decimal(10, 5)), CAST(142.45900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (794, CAST(12.53333 AS Decimal(10, 5)), CAST(-7.95000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (799, CAST(6.03924 AS Decimal(10, 5)), CAST(10.12264 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (802, CAST(34.80860 AS Decimal(10, 5)), CAST(67.81820 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (807, CAST(5.52352 AS Decimal(10, 5)), CAST(95.42037 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (809, CAST(27.21832 AS Decimal(10, 5)), CAST(56.37785 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (811, CAST(-5.24280 AS Decimal(10, 5)), CAST(105.17705 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (812, CAST(26.53200 AS Decimal(10, 5)), CAST(54.82485 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (813, CAST(30.55639 AS Decimal(10, 5)), CAST(49.15194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (814, CAST(4.94420 AS Decimal(10, 5)), CAST(114.92835 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (819, CAST(-6.90069 AS Decimal(10, 5)), CAST(107.57538 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (822, CAST(13.20071 AS Decimal(10, 5)), CAST(77.70879 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (824, CAST(30.55360 AS Decimal(10, 5)), CAST(97.10830 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (825, CAST(13.91258 AS Decimal(10, 5)), CAST(100.60675 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (827, CAST(13.69152 AS Decimal(10, 5)), CAST(100.75089 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (828, CAST(44.80892 AS Decimal(10, 5)), CAST(-68.82275 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (830, CAST(4.39848 AS Decimal(10, 5)), CAST(18.51879 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (833, CAST(44.93357 AS Decimal(10, 5)), CAST(17.29969 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (834, CAST(-3.44176 AS Decimal(10, 5)), CAST(114.75859 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (835, CAST(13.34444 AS Decimal(10, 5)), CAST(-16.65833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (837, CAST(12.66830 AS Decimal(10, 5)), CAST(108.12000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (843, CAST(25.05427 AS Decimal(10, 5)), CAST(99.16577 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (844, CAST(40.56000 AS Decimal(10, 5)), CAST(109.99700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (847, CAST(44.44924 AS Decimal(10, 5)), CAST(-68.36196 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (850, CAST(20.36532 AS Decimal(10, 5)), CAST(-74.50621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (859, CAST(-23.56530 AS Decimal(10, 5)), CAST(145.30701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (860, CAST(41.29708 AS Decimal(10, 5)), CAST(2.07846 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (862, CAST(10.10714 AS Decimal(10, 5)), CAST(-64.68916 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (866, CAST(69.05576 AS Decimal(10, 5)), CAST(18.54036 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (869, CAST(41.13697 AS Decimal(10, 5)), CAST(16.76188 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (871, CAST(8.61957 AS Decimal(10, 5)), CAST(-70.22083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (872, CAST(3.73389 AS Decimal(10, 5)), CAST(115.47900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (873, CAST(22.80100 AS Decimal(10, 5)), CAST(90.30120 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (875, CAST(53.36377 AS Decimal(10, 5)), CAST(83.53853 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (878, CAST(10.04275 AS Decimal(10, 5)), CAST(-69.35862 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (880, CAST(57.02534 AS Decimal(10, 5)), CAST(-7.44958 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (883, CAST(-15.86134 AS Decimal(10, 5)), CAST(-52.38889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (887, CAST(7.02433 AS Decimal(10, 5)), CAST(-73.80680 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (889, CAST(10.88959 AS Decimal(10, 5)), CAST(-74.78082 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (891, CAST(-12.07890 AS Decimal(10, 5)), CAST(-45.00900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (896, CAST(71.28483 AS Decimal(10, 5)), CAST(-156.76590 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (899, CAST(70.13395 AS Decimal(10, 5)), CAST(-143.58045 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (907, CAST(20.45132 AS Decimal(10, 5)), CAST(121.97988 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (910, CAST(47.58958 AS Decimal(10, 5)), CAST(7.52991 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (915, CAST(30.55218 AS Decimal(10, 5)), CAST(47.66386 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (918, CAST(42.55266 AS Decimal(10, 5)), CAST(9.48373 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (919, CAST(1.90547 AS Decimal(10, 5)), CAST(9.80568 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (920, CAST(1.12062 AS Decimal(10, 5)), CAST(104.11529 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (927, CAST(47.62972 AS Decimal(10, 5)), CAST(-65.73889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (928, CAST(-33.40802 AS Decimal(10, 5)), CAST(149.65138 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (930, CAST(37.92897 AS Decimal(10, 5)), CAST(41.11658 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (931, CAST(35.75211 AS Decimal(10, 5)), CAST(6.30859 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (935, CAST(30.53297 AS Decimal(10, 5)), CAST(-91.14955 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (937, CAST(70.60201 AS Decimal(10, 5)), CAST(29.69416 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (939, CAST(7.70576 AS Decimal(10, 5)), CAST(81.67878 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (942, CAST(-3.41241 AS Decimal(10, 5)), CAST(115.99500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (943, CAST(41.61030 AS Decimal(10, 5)), CAST(41.59970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (945, CAST(-5.48707 AS Decimal(10, 5)), CAST(122.56901 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (947, CAST(10.29440 AS Decimal(10, 5)), CAST(9.81667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (953, CAST(43.53211 AS Decimal(10, 5)), CAST(-84.08770 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (955, CAST(20.39717 AS Decimal(10, 5)), CAST(-76.61908 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (964, CAST(53.96560 AS Decimal(10, 5)), CAST(-91.02720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (971, CAST(29.95077 AS Decimal(10, 5)), CAST(-94.02073 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (974, CAST(66.36200 AS Decimal(10, 5)), CAST(-147.40781 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (979, CAST(31.64574 AS Decimal(10, 5)), CAST(-2.26986 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (980, CAST(37.78706 AS Decimal(10, 5)), CAST(-81.12399 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (986, CAST(42.46982 AS Decimal(10, 5)), CAST(-71.28839 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (987, CAST(-24.34610 AS Decimal(10, 5)), CAST(139.46001 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (995, CAST(32.78870 AS Decimal(10, 5)), CAST(21.96430 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (997, CAST(21.53940 AS Decimal(10, 5)), CAST(109.29400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (999, CAST(40.08011 AS Decimal(10, 5)), CAST(116.58456 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1001, CAST(39.78280 AS Decimal(10, 5)), CAST(116.38800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1002, CAST(-19.79333 AS Decimal(10, 5)), CAST(34.91889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1003, CAST(33.82093 AS Decimal(10, 5)), CAST(35.48839 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1004, CAST(36.71200 AS Decimal(10, 5)), CAST(5.06992 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1009, CAST(-1.37925 AS Decimal(10, 5)), CAST(-48.47629 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1010, CAST(-19.72060 AS Decimal(10, 5)), CAST(163.66100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1011, CAST(54.61806 AS Decimal(10, 5)), CAST(-5.87250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1012, CAST(54.65750 AS Decimal(10, 5)), CAST(-6.21583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1015, CAST(15.85929 AS Decimal(10, 5)), CAST(74.61829 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1016, CAST(50.64380 AS Decimal(10, 5)), CAST(36.59010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1018, CAST(44.81844 AS Decimal(10, 5)), CAST(20.30914 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1021, CAST(17.51566 AS Decimal(10, 5)), CAST(-88.19491 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1022, CAST(17.53914 AS Decimal(10, 5)), CAST(-88.30820 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1024, CAST(52.18193 AS Decimal(10, 5)), CAST(-128.15526 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1025, CAST(52.38750 AS Decimal(10, 5)), CAST(-126.59583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1034, CAST(38.59192 AS Decimal(10, 5)), CAST(-89.82512 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1036, CAST(48.79275 AS Decimal(10, 5)), CAST(-122.53753 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1038, CAST(-11.30247 AS Decimal(10, 5)), CAST(159.79887 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1042, CAST(17.26968 AS Decimal(10, 5)), CAST(-88.77691 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1046, CAST(-19.85118 AS Decimal(10, 5)), CAST(-43.95063 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1047, CAST(-19.63375 AS Decimal(10, 5)), CAST(-43.96886 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1050, CAST(47.83811 AS Decimal(10, 5)), CAST(27.78148 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1054, CAST(47.50912 AS Decimal(10, 5)), CAST(-94.93389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1056, CAST(57.48111 AS Decimal(10, 5)), CAST(-7.36278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1061, CAST(32.09679 AS Decimal(10, 5)), CAST(20.26947 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1062, CAST(-3.86370 AS Decimal(10, 5)), CAST(102.33904 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1065, CAST(0.57688 AS Decimal(10, 5)), CAST(29.46982 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1066, CAST(6.31667 AS Decimal(10, 5)), CAST(5.60000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1077, CAST(2.15550 AS Decimal(10, 5)), CAST(117.43200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1084, CAST(52.35851 AS Decimal(10, 5)), CAST(-97.02129 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1085, CAST(60.29339 AS Decimal(10, 5)), CAST(5.21814 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1089, CAST(44.82528 AS Decimal(10, 5)), CAST(0.51861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1095, CAST(70.87100 AS Decimal(10, 5)), CAST(29.02944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1100, CAST(52.38000 AS Decimal(10, 5)), CAST(13.52250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1101, CAST(52.55969 AS Decimal(10, 5)), CAST(13.28771 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1105, CAST(32.36404 AS Decimal(10, 5)), CAST(-64.67870 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1109, CAST(46.91410 AS Decimal(10, 5)), CAST(7.49715 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1114, CAST(4.54861 AS Decimal(10, 5)), CAST(13.72610 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1121, CAST(60.77972 AS Decimal(10, 5)), CAST(-161.83778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1131, CAST(66.91437 AS Decimal(10, 5)), CAST(-151.52842 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1135, CAST(43.32215 AS Decimal(10, 5)), CAST(3.35319 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1137, CAST(26.57080 AS Decimal(10, 5)), CAST(88.07960 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1138, CAST(27.50570 AS Decimal(10, 5)), CAST(83.41625 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1139, CAST(24.26833 AS Decimal(10, 5)), CAST(97.24874 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1140, CAST(27.67863 AS Decimal(10, 5)), CAST(84.42749 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1141, CAST(30.27010 AS Decimal(10, 5)), CAST(74.75580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1142, CAST(21.75221 AS Decimal(10, 5)), CAST(72.18518 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1143, CAST(27.14740 AS Decimal(10, 5)), CAST(87.05080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1144, CAST(23.28747 AS Decimal(10, 5)), CAST(77.33737 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1145, CAST(20.24436 AS Decimal(10, 5)), CAST(85.81778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1146, CAST(23.28783 AS Decimal(10, 5)), CAST(69.67015 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1148, CAST(-1.19002 AS Decimal(10, 5)), CAST(136.10800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1152, CAST(43.46842 AS Decimal(10, 5)), CAST(-1.52333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1174, CAST(53.81857 AS Decimal(10, 5)), CAST(-89.89829 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1175, CAST(28.07060 AS Decimal(10, 5)), CAST(73.20720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1177, CAST(21.98840 AS Decimal(10, 5)), CAST(82.11098 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1178, CAST(43.30110 AS Decimal(10, 5)), CAST(-2.91061 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1182, CAST(45.80829 AS Decimal(10, 5)), CAST(-108.54430 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1183, CAST(55.74032 AS Decimal(10, 5)), CAST(9.15178 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1184, CAST(-24.49390 AS Decimal(10, 5)), CAST(150.57600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1186, CAST(-8.54045 AS Decimal(10, 5)), CAST(118.68779 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1188, CAST(25.69988 AS Decimal(10, 5)), CAST(-79.26466 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1191, CAST(42.20841 AS Decimal(10, 5)), CAST(-75.97909 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1193, CAST(3.12385 AS Decimal(10, 5)), CAST(113.02047 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1197, CAST(26.48229 AS Decimal(10, 5)), CAST(87.26496 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1198, CAST(66.26980 AS Decimal(10, 5)), CAST(-145.81860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1200, CAST(-25.89750 AS Decimal(10, 5)), CAST(139.34801 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1202, CAST(32.89806 AS Decimal(10, 5)), CAST(59.26611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1203, CAST(33.56166 AS Decimal(10, 5)), CAST(-86.75254 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1205, CAST(52.45386 AS Decimal(10, 5)), CAST(-1.74803 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1209, CAST(19.98435 AS Decimal(10, 5)), CAST(42.62088 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1211, CAST(43.06131 AS Decimal(10, 5)), CAST(74.47756 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1214, CAST(34.79329 AS Decimal(10, 5)), CAST(5.73823 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1216, CAST(46.77622 AS Decimal(10, 5)), CAST(-100.75763 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1217, CAST(11.89485 AS Decimal(10, 5)), CAST(-15.65368 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1226, CAST(-24.42980 AS Decimal(10, 5)), CAST(145.43319 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1233, CAST(50.42540 AS Decimal(10, 5)), CAST(127.41200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1237, CAST(51.44361 AS Decimal(10, 5)), CAST(-57.18528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1239, CAST(-15.67905 AS Decimal(10, 5)), CAST(34.97401 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1240, CAST(-41.51833 AS Decimal(10, 5)), CAST(173.87027 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1242, CAST(41.16816 AS Decimal(10, 5)), CAST(-71.57783 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1243, CAST(-29.09272 AS Decimal(10, 5)), CAST(26.30244 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1248, CAST(39.13828 AS Decimal(10, 5)), CAST(-86.61730 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1249, CAST(40.48299 AS Decimal(10, 5)), CAST(-88.91395 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1257, CAST(11.99096 AS Decimal(10, 5)), CAST(-83.77409 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1264, CAST(16.13624 AS Decimal(10, 5)), CAST(-22.88890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1265, CAST(2.84631 AS Decimal(10, 5)), CAST(-60.69007 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1269, CAST(11.16006 AS Decimal(10, 5)), CAST(-4.33097 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1273, CAST(9.34085 AS Decimal(10, 5)), CAST(-82.25084 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1277, CAST(67.26917 AS Decimal(10, 5)), CAST(14.36528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1279, CAST(37.25061 AS Decimal(10, 5)), CAST(27.66431 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1285, CAST(4.70159 AS Decimal(10, 5)), CAST(-74.14695 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1286, CAST(-9.23278 AS Decimal(10, 5)), CAST(142.21800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1287, CAST(43.56156 AS Decimal(10, 5)), CAST(-116.23984 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1288, CAST(37.49296 AS Decimal(10, 5)), CAST(57.30822 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1293, CAST(43.28583 AS Decimal(10, 5)), CAST(16.67972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1297, CAST(44.53544 AS Decimal(10, 5)), CAST(11.28867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1307, CAST(48.07110 AS Decimal(10, 5)), CAST(-65.46030 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1312, CAST(-21.24402 AS Decimal(10, 5)), CAST(-56.45007 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1318, CAST(-16.44438 AS Decimal(10, 5)), CAST(-151.75129 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1321, CAST(44.82834 AS Decimal(10, 5)), CAST(-0.71556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1324, CAST(21.37500 AS Decimal(10, 5)), CAST(0.92389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1332, CAST(60.42843 AS Decimal(10, 5)), CAST(15.50514 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1335, CAST(55.06327 AS Decimal(10, 5)), CAST(14.75956 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1343, CAST(11.27530 AS Decimal(10, 5)), CAST(49.14940 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1345, CAST(31.56005 AS Decimal(10, 5)), CAST(64.36487 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1348, CAST(42.36514 AS Decimal(10, 5)), CAST(-71.01777 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1350, CAST(43.08457 AS Decimal(10, 5)), CAST(-70.82307 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1358, CAST(7.73880 AS Decimal(10, 5)), CAST(-5.07367 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1370, CAST(-22.91330 AS Decimal(10, 5)), CAST(139.89999 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1381, CAST(42.56958 AS Decimal(10, 5)), CAST(27.51524 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1385, CAST(50.78000 AS Decimal(10, 5)), CAST(-1.84250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1394, CAST(45.77660 AS Decimal(10, 5)), CAST(-111.15356 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1402, CAST(41.80284 AS Decimal(10, 5)), CAST(-78.64003 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1406, CAST(41.85780 AS Decimal(10, 5)), CAST(-6.70712 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1409, CAST(46.39774 AS Decimal(10, 5)), CAST(-94.13721 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1415, CAST(49.90738 AS Decimal(10, 5)), CAST(-99.94915 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1418, CAST(-15.87110 AS Decimal(10, 5)), CAST(-47.91862 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1421, CAST(48.17017 AS Decimal(10, 5)), CAST(17.21267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1423, CAST(56.37083 AS Decimal(10, 5)), CAST(101.69861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1429, CAST(-4.25170 AS Decimal(10, 5)), CAST(15.25303 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1435, CAST(53.04750 AS Decimal(10, 5)), CAST(8.78667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1438, CAST(52.10833 AS Decimal(10, 5)), CAST(23.89667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1439, CAST(48.44791 AS Decimal(10, 5)), CAST(-4.41854 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1444, CAST(53.21419 AS Decimal(10, 5)), CAST(34.17645 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1446, CAST(13.07460 AS Decimal(10, 5)), CAST(-59.49246 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1452, CAST(40.65763 AS Decimal(10, 5)), CAST(17.94703 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1453, CAST(-27.38417 AS Decimal(10, 5)), CAST(153.11750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1454, CAST(51.38267 AS Decimal(10, 5)), CAST(-2.71909 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1470, CAST(45.04151 AS Decimal(10, 5)), CAST(1.48646 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1471, CAST(51.75000 AS Decimal(10, 5)), CAST(-1.58333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1474, CAST(49.15127 AS Decimal(10, 5)), CAST(16.69443 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1477, CAST(57.88940 AS Decimal(10, 5)), CAST(-101.67900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1481, CAST(-32.00140 AS Decimal(10, 5)), CAST(141.47200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1483, CAST(65.46111 AS Decimal(10, 5)), CAST(12.21750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1490, CAST(58.54913 AS Decimal(10, 5)), CAST(-155.80647 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1491, CAST(-17.94881 AS Decimal(10, 5)), CAST(122.22719 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1493, CAST(25.90730 AS Decimal(10, 5)), CAST(-97.42609 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1501, CAST(31.25884 AS Decimal(10, 5)), CAST(-81.46621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1509, CAST(50.45920 AS Decimal(10, 5)), CAST(4.45382 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1513, CAST(50.90139 AS Decimal(10, 5)), CAST(4.48444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1519, CAST(7.12650 AS Decimal(10, 5)), CAST(-73.18478 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1524, CAST(44.57216 AS Decimal(10, 5)), CAST(26.10218 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1528, CAST(65.97995 AS Decimal(10, 5)), CAST(-161.13930 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1530, CAST(47.43693 AS Decimal(10, 5)), CAST(19.25559 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1533, CAST(3.81963 AS Decimal(10, 5)), CAST(-76.98977 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1534, CAST(-34.55918 AS Decimal(10, 5)), CAST(-58.41561 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1536, CAST(-34.82222 AS Decimal(10, 5)), CAST(-58.53583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1538, CAST(48.22000 AS Decimal(10, 5)), CAST(87.00000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1541, CAST(42.94034 AS Decimal(10, 5)), CAST(-78.73170 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1545, CAST(54.64000 AS Decimal(10, 5)), CAST(52.80170 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1547, CAST(-3.32402 AS Decimal(10, 5)), CAST(29.31852 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1548, CAST(-5.42232 AS Decimal(10, 5)), CAST(154.67300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1549, CAST(-2.30898 AS Decimal(10, 5)), CAST(28.80880 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1550, CAST(39.76629 AS Decimal(10, 5)), CAST(64.47988 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1551, CAST(-1.33333 AS Decimal(10, 5)), CAST(31.81667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1552, CAST(-20.01743 AS Decimal(10, 5)), CAST(28.61787 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1558, CAST(35.16476 AS Decimal(10, 5)), CAST(-114.55878 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1560, CAST(-7.21505 AS Decimal(10, 5)), CAST(146.64877 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1561, CAST(2.18278 AS Decimal(10, 5)), CAST(22.48170 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1564, CAST(-24.90390 AS Decimal(10, 5)), CAST(152.31900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1566, CAST(1.56572 AS Decimal(10, 5)), CAST(30.22080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1571, CAST(1.10217 AS Decimal(10, 5)), CAST(121.41360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1574, CAST(34.20056 AS Decimal(10, 5)), CAST(-118.35925 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1578, CAST(42.35763 AS Decimal(10, 5)), CAST(-3.62076 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1579, CAST(15.22950 AS Decimal(10, 5)), CAST(103.25300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1580, CAST(-17.74860 AS Decimal(10, 5)), CAST(139.53400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1582, CAST(40.78310 AS Decimal(10, 5)), CAST(-91.12542 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1584, CAST(44.47144 AS Decimal(10, 5)), CAST(-73.15207 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1585, CAST(-40.99890 AS Decimal(10, 5)), CAST(145.73100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1589, CAST(40.25528 AS Decimal(10, 5)), CAST(29.56250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1595, CAST(35.17953 AS Decimal(10, 5)), CAST(128.93822 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1596, CAST(28.94481 AS Decimal(10, 5)), CAST(50.83464 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1598, CAST(12.12150 AS Decimal(10, 5)), CAST(120.10000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1605, CAST(45.95465 AS Decimal(10, 5)), CAST(-112.49808 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1608, CAST(8.95147 AS Decimal(10, 5)), CAST(125.47882 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1611, CAST(53.09680 AS Decimal(10, 5)), CAST(17.97770 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1613, CAST(9.17645 AS Decimal(10, 5)), CAST(105.17582 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1616, CAST(-5.59699 AS Decimal(10, 5)), CAST(12.18835 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1617, CAST(-22.92170 AS Decimal(10, 5)), CAST(-42.07430 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1628, CAST(-11.49331 AS Decimal(10, 5)), CAST(-61.45168 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1631, CAST(49.17333 AS Decimal(10, 5)), CAST(-0.45000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1633, CAST(8.61166 AS Decimal(10, 5)), CAST(124.45553 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1635, CAST(39.25147 AS Decimal(10, 5)), CAST(9.05428 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1642, CAST(-16.88583 AS Decimal(10, 5)), CAST(145.75528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1643, CAST(30.12194 AS Decimal(10, 5)), CAST(31.40556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1645, CAST(-7.13918 AS Decimal(10, 5)), CAST(-78.48940 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1646, CAST(4.97602 AS Decimal(10, 5)), CAST(8.34720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1651, CAST(-22.49662 AS Decimal(10, 5)), CAST(-68.90857 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1652, CAST(12.06667 AS Decimal(10, 5)), CAST(124.53333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1653, CAST(-17.72530 AS Decimal(10, 5)), CAST(-48.60750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1654, CAST(40.87510 AS Decimal(10, 5)), CAST(-74.28070 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1658, CAST(51.12933 AS Decimal(10, 5)), CAST(-114.01294 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1659, CAST(3.54322 AS Decimal(10, 5)), CAST(-76.38158 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1665, CAST(42.53075 AS Decimal(10, 5)), CAST(8.79319 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1667, CAST(21.42043 AS Decimal(10, 5)), CAST(-77.84743 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1669, CAST(52.20500 AS Decimal(10, 5)), CAST(0.17500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1672, CAST(69.10805 AS Decimal(10, 5)), CAST(-105.13833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1678, CAST(9.25361 AS Decimal(10, 5)), CAST(124.70694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1683, CAST(38.80366 AS Decimal(10, 5)), CAST(-76.87215 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1686, CAST(49.95083 AS Decimal(10, 5)), CAST(-125.27083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1691, CAST(55.43722 AS Decimal(10, 5)), CAST(-5.68639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1692, CAST(19.81679 AS Decimal(10, 5)), CAST(-90.50031 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1693, CAST(-7.26992 AS Decimal(10, 5)), CAST(-35.89636 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1697, CAST(-20.46867 AS Decimal(10, 5)), CAST(-54.67250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1700, CAST(-21.69833 AS Decimal(10, 5)), CAST(-41.30167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1702, CAST(10.08510 AS Decimal(10, 5)), CAST(105.71200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1705, CAST(40.13772 AS Decimal(10, 5)), CAST(26.42678 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1710, CAST(-35.30694 AS Decimal(10, 5)), CAST(149.19500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1711, CAST(21.03653 AS Decimal(10, 5)), CAST(-86.87708 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1728, CAST(19.73299 AS Decimal(10, 5)), CAST(-72.19474 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1729, CAST(12.39064 AS Decimal(10, 5)), CAST(-16.74342 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1731, CAST(-40.39016 AS Decimal(10, 5)), CAST(148.01803 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1732, CAST(64.23094 AS Decimal(10, 5)), CAST(-76.52862 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1735, CAST(37.22533 AS Decimal(10, 5)), CAST(-89.57065 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1737, CAST(68.87513 AS Decimal(10, 5)), CAST(-166.11002 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1747, CAST(-33.96481 AS Decimal(10, 5)), CAST(18.60167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1755, CAST(10.60046 AS Decimal(10, 5)), CAST(-66.99811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1764, CAST(43.21598 AS Decimal(10, 5)), CAST(2.30632 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1765, CAST(51.39667 AS Decimal(10, 5)), CAST(-3.34333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1771, CAST(54.93750 AS Decimal(10, 5)), CAST(-2.80917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1772, CAST(32.33734 AS Decimal(10, 5)), CAST(-104.26300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1777, CAST(-24.88060 AS Decimal(10, 5)), CAST(113.67200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1788, CAST(10.44238 AS Decimal(10, 5)), CAST(-75.51296 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1800, CAST(33.36747 AS Decimal(10, 5)), CAST(-7.58997 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1803, CAST(-25.00030 AS Decimal(10, 5)), CAST(-53.50080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1808, CAST(42.90802 AS Decimal(10, 5)), CAST(-106.46392 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1814, CAST(49.29639 AS Decimal(10, 5)), CAST(-117.63250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1815, CAST(43.55500 AS Decimal(10, 5)), CAST(2.29100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1822, CAST(51.72720 AS Decimal(10, 5)), CAST(-91.82440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1831, CAST(-28.59321 AS Decimal(10, 5)), CAST(-65.75092 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1833, CAST(37.46678 AS Decimal(10, 5)), CAST(15.06640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1835, CAST(12.50410 AS Decimal(10, 5)), CAST(124.63750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1836, CAST(11.92417 AS Decimal(10, 5)), CAST(121.95348 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1838, CAST(-12.47920 AS Decimal(10, 5)), CAST(13.48690 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1839, CAST(16.93333 AS Decimal(10, 5)), CAST(121.75000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1840, CAST(7.96847 AS Decimal(10, 5)), CAST(-75.19850 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1846, CAST(-29.19710 AS Decimal(10, 5)), CAST(-51.18750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1847, CAST(17.73470 AS Decimal(10, 5)), CAST(-88.03250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1848, CAST(17.68377 AS Decimal(10, 5)), CAST(-88.04516 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1849, CAST(4.81981 AS Decimal(10, 5)), CAST(-52.36045 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1850, CAST(19.68720 AS Decimal(10, 5)), CAST(-79.87996 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1851, CAST(22.46133 AS Decimal(10, 5)), CAST(-78.32733 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1852, CAST(21.61645 AS Decimal(10, 5)), CAST(-81.54599 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1855, CAST(10.30850 AS Decimal(10, 5)), CAST(123.98028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1856, CAST(37.70137 AS Decimal(10, 5)), CAST(-113.09801 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1858, CAST(41.88445 AS Decimal(10, 5)), CAST(-91.71073 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1859, CAST(-32.12489 AS Decimal(10, 5)), CAST(133.70151 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1864, CAST(65.57361 AS Decimal(10, 5)), CAST(-144.78306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1873, CAST(-6.20181 AS Decimal(10, 5)), CAST(-77.85606 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1874, CAST(42.83747 AS Decimal(10, 5)), CAST(-103.09574 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1876, CAST(25.44335 AS Decimal(10, 5)), CAST(60.38211 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1878, CAST(34.52600 AS Decimal(10, 5)), CAST(65.27000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1880, CAST(66.64926 AS Decimal(10, 5)), CAST(-143.72760 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1884, CAST(45.63805 AS Decimal(10, 5)), CAST(5.88023 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1887, CAST(40.03948 AS Decimal(10, 5)), CAST(-88.27755 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1892, CAST(30.67350 AS Decimal(10, 5)), CAST(76.78850 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1894, CAST(43.99621 AS Decimal(10, 5)), CAST(125.68536 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1895, CAST(28.91890 AS Decimal(10, 5)), CAST(111.64000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1897, CAST(28.18916 AS Decimal(10, 5)), CAST(113.21963 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1898, CAST(9.45588 AS Decimal(10, 5)), CAST(-82.51724 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1899, CAST(36.24750 AS Decimal(10, 5)), CAST(113.12600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1900, CAST(31.91970 AS Decimal(10, 5)), CAST(119.77900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1901, CAST(35.53175 AS Decimal(10, 5)), CAST(24.14968 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1904, CAST(41.53810 AS Decimal(10, 5)), CAST(120.43500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1906, CAST(-27.13420 AS Decimal(10, 5)), CAST(-52.65660 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1910, CAST(32.89851 AS Decimal(10, 5)), CAST(-80.04037 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1911, CAST(38.37315 AS Decimal(10, 5)), CAST(-81.59318 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1912, CAST(-26.41333 AS Decimal(10, 5)), CAST(146.26250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1914, CAST(47.99083 AS Decimal(10, 5)), CAST(-66.33028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1915, CAST(35.22070 AS Decimal(10, 5)), CAST(-80.94413 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1918, CAST(38.13847 AS Decimal(10, 5)), CAST(-78.45279 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1919, CAST(52.76489 AS Decimal(10, 5)), CAST(-56.11731 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1920, CAST(46.28640 AS Decimal(10, 5)), CAST(-63.12650 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1926, CAST(46.86219 AS Decimal(10, 5)), CAST(1.73067 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1930, CAST(-43.81000 AS Decimal(10, 5)), CAST(-176.45722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1932, CAST(35.03507 AS Decimal(10, 5)), CAST(-85.20357 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1936, CAST(56.09030 AS Decimal(10, 5)), CAST(47.34730 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1942, CAST(55.30584 AS Decimal(10, 5)), CAST(61.50333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1946, CAST(30.57853 AS Decimal(10, 5)), CAST(103.94709 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1947, CAST(12.98833 AS Decimal(10, 5)), CAST(80.16578 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1949, CAST(36.71660 AS Decimal(10, 5)), CAST(127.49912 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1953, CAST(59.27360 AS Decimal(10, 5)), CAST(38.01580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1957, CAST(48.26000 AS Decimal(10, 5)), CAST(25.98167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1962, CAST(68.74060 AS Decimal(10, 5)), CAST(161.33800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1964, CAST(53.17806 AS Decimal(10, 5)), CAST(-2.97778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1966, CAST(63.34587 AS Decimal(10, 5)), CAST(-90.72759 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1967, CAST(18.50467 AS Decimal(10, 5)), CAST(-88.32685 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1970, CAST(61.53706 AS Decimal(10, 5)), CAST(-165.60272 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1971, CAST(50.46890 AS Decimal(10, 5)), CAST(-59.63670 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1972, CAST(41.15554 AS Decimal(10, 5)), CAST(-104.81330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1975, CAST(23.21310 AS Decimal(10, 5)), CAST(119.41800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1976, CAST(18.76667 AS Decimal(10, 5)), CAST(98.96667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1977, CAST(19.95229 AS Decimal(10, 5)), CAST(99.88289 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1979, CAST(23.46178 AS Decimal(10, 5)), CAST(120.39283 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1981, CAST(49.77194 AS Decimal(10, 5)), CAST(-74.52806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1982, CAST(41.85003 AS Decimal(10, 5)), CAST(-87.65005 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1983, CAST(42.20074 AS Decimal(10, 5)), CAST(-89.09538 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1988, CAST(41.78680 AS Decimal(10, 5)), CAST(-87.74555 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1990, CAST(41.97959 AS Decimal(10, 5)), CAST(-87.90446 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1996, CAST(64.06662 AS Decimal(10, 5)), CAST(-141.95117 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (1997, CAST(-6.78633 AS Decimal(10, 5)), CAST(-79.82786 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2000, CAST(42.23333 AS Decimal(10, 5)), CAST(118.90944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2001, CAST(56.25558 AS Decimal(10, 5)), CAST(-158.77580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2003, CAST(56.31778 AS Decimal(10, 5)), CAST(-158.59083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2004, CAST(56.31116 AS Decimal(10, 5)), CAST(-158.53593 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2007, CAST(28.70287 AS Decimal(10, 5)), CAST(-105.96457 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2012, CAST(-17.13874 AS Decimal(10, 5)), CAST(144.52888 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2017, CAST(-19.15127 AS Decimal(10, 5)), CAST(33.42896 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2018, CAST(-26.77500 AS Decimal(10, 5)), CAST(150.61700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2024, CAST(38.34318 AS Decimal(10, 5)), CAST(26.14057 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2028, CAST(62.07119 AS Decimal(10, 5)), CAST(-142.04837 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2029, CAST(53.80560 AS Decimal(10, 5)), CAST(-78.91690 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2031, CAST(46.92778 AS Decimal(10, 5)), CAST(28.93083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2033, CAST(52.02632 AS Decimal(10, 5)), CAST(113.30556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2036, CAST(35.88167 AS Decimal(10, 5)), CAST(71.79806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2037, CAST(7.98686 AS Decimal(10, 5)), CAST(-80.41009 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2038, CAST(22.24961 AS Decimal(10, 5)), CAST(91.81329 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2042, CAST(36.21266 AS Decimal(10, 5)), CAST(1.33177 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2043, CAST(48.13570 AS Decimal(10, 5)), CAST(114.64600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2044, CAST(-6.71119 AS Decimal(10, 5)), CAST(156.39570 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2045, CAST(70.62310 AS Decimal(10, 5)), CAST(147.90199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2050, CAST(29.71922 AS Decimal(10, 5)), CAST(106.64168 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2052, CAST(-43.48936 AS Decimal(10, 5)), CAST(172.53223 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2054, CAST(-10.45056 AS Decimal(10, 5)), CAST(105.69028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2055, CAST(1.98616 AS Decimal(10, 5)), CAST(-157.34978 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2056, CAST(61.57912 AS Decimal(10, 5)), CAST(-159.22562 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2059, CAST(10.71120 AS Decimal(10, 5)), CAST(99.36170 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2065, CAST(53.56194 AS Decimal(10, 5)), CAST(-64.10639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2067, CAST(-17.74330 AS Decimal(10, 5)), CAST(-179.34200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2069, CAST(22.15000 AS Decimal(10, 5)), CAST(-80.41417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2070, CAST(-7.64278 AS Decimal(10, 5)), CAST(109.03139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2072, CAST(39.04614 AS Decimal(10, 5)), CAST(-84.66217 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2074, CAST(39.10333 AS Decimal(10, 5)), CAST(-84.41861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2076, CAST(65.82800 AS Decimal(10, 5)), CAST(-144.07600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2080, CAST(8.12190 AS Decimal(10, 5)), CAST(-63.53735 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2081, CAST(25.05380 AS Decimal(10, 5)), CAST(-111.61500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2082, CAST(18.65336 AS Decimal(10, 5)), CAST(-91.80099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2083, CAST(-25.45550 AS Decimal(10, 5)), CAST(-54.84359 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2085, CAST(31.63599 AS Decimal(10, 5)), CAST(-106.42902 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2087, CAST(27.39278 AS Decimal(10, 5)), CAST(-109.83306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2089, CAST(23.70334 AS Decimal(10, 5)), CAST(-98.95649 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2093, CAST(58.83369 AS Decimal(10, 5)), CAST(-158.52971 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2094, CAST(39.29891 AS Decimal(10, 5)), CAST(-80.23171 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2103, CAST(45.78666 AS Decimal(10, 5)), CAST(3.16917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2105, CAST(41.51727 AS Decimal(10, 5)), CAST(-81.68319 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2107, CAST(41.41083 AS Decimal(10, 5)), CAST(-81.84944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2118, CAST(-20.66860 AS Decimal(10, 5)), CAST(140.50400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2120, CAST(34.39200 AS Decimal(10, 5)), CAST(-103.32071 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2122, CAST(34.42646 AS Decimal(10, 5)), CAST(-103.07828 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2125, CAST(46.78517 AS Decimal(10, 5)), CAST(23.68617 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2127, CAST(70.48611 AS Decimal(10, 5)), CAST(-68.51667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2130, CAST(14.69461 AS Decimal(10, 5)), CAST(-91.88149 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2134, CAST(-31.53830 AS Decimal(10, 5)), CAST(145.79401 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2135, CAST(-11.04044 AS Decimal(10, 5)), CAST(-68.78297 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2137, CAST(-0.46289 AS Decimal(10, 5)), CAST(-76.98680 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2138, CAST(-17.42106 AS Decimal(10, 5)), CAST(-66.17711 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2145, CAST(-10.05000 AS Decimal(10, 5)), CAST(143.07001 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2146, CAST(-12.18833 AS Decimal(10, 5)), CAST(96.83389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2149, CAST(44.51995 AS Decimal(10, 5)), CAST(-109.02431 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2150, CAST(-13.76080 AS Decimal(10, 5)), CAST(143.11400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2154, CAST(56.01075 AS Decimal(10, 5)), CAST(-132.83275 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2155, CAST(-30.32056 AS Decimal(10, 5)), CAST(153.11639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2157, CAST(11.03003 AS Decimal(10, 5)), CAST(77.04338 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2162, CAST(55.20556 AS Decimal(10, 5)), CAST(-162.72417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2164, CAST(67.25163 AS Decimal(10, 5)), CAST(-150.20657 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2166, CAST(19.27701 AS Decimal(10, 5)), CAST(-103.57740 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2167, CAST(56.60114 AS Decimal(10, 5)), CAST(-6.62243 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2170, CAST(30.58853 AS Decimal(10, 5)), CAST(-96.36385 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2178, CAST(50.87894 AS Decimal(10, 5)), CAST(7.12292 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2182, CAST(7.18076 AS Decimal(10, 5)), CAST(79.88412 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2190, CAST(56.05750 AS Decimal(10, 5)), CAST(-6.24306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2195, CAST(38.80583 AS Decimal(10, 5)), CAST(-104.70136 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2199, CAST(38.81809 AS Decimal(10, 5)), CAST(-92.21963 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2201, CAST(33.94502 AS Decimal(10, 5)), CAST(-81.12543 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2205, CAST(32.33732 AS Decimal(10, 5)), CAST(-84.99128 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2210, CAST(33.45012 AS Decimal(10, 5)), CAST(-88.59116 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2217, CAST(39.99795 AS Decimal(10, 5)), CAST(-82.88352 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2218, CAST(39.81720 AS Decimal(10, 5)), CAST(-82.93616 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2223, CAST(36.99590 AS Decimal(10, 5)), CAST(14.60920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2226, CAST(-45.78535 AS Decimal(10, 5)), CAST(-67.46551 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2227, CAST(49.71083 AS Decimal(10, 5)), CAST(-124.88667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2231, CAST(8.73183 AS Decimal(10, 5)), CAST(106.63300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2232, CAST(9.57689 AS Decimal(10, 5)), CAST(-13.61196 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2235, CAST(-36.77265 AS Decimal(10, 5)), CAST(-73.06311 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2237, CAST(37.98937 AS Decimal(10, 5)), CAST(-122.05802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2243, CAST(5.08333 AS Decimal(10, 5)), CAST(-76.70000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2244, CAST(-10.63440 AS Decimal(10, 5)), CAST(-51.56360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2250, CAST(44.36222 AS Decimal(10, 5)), CAST(28.48833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2251, CAST(36.27603 AS Decimal(10, 5)), CAST(6.62039 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2253, CAST(8.62876 AS Decimal(10, 5)), CAST(-79.03470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2254, CAST(-29.04000 AS Decimal(10, 5)), CAST(134.72099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2257, CAST(-15.44470 AS Decimal(10, 5)), CAST(145.18401 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2261, CAST(-36.29379 AS Decimal(10, 5)), CAST(148.97397 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2270, CAST(55.61792 AS Decimal(10, 5)), CAST(12.65597 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2273, CAST(55.58560 AS Decimal(10, 5)), CAST(12.13140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2274, CAST(-27.29689 AS Decimal(10, 5)), CAST(-70.41404 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2278, CAST(64.19333 AS Decimal(10, 5)), CAST(-83.35944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2282, CAST(-31.32362 AS Decimal(10, 5)), CAST(-64.20795 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2286, CAST(60.49167 AS Decimal(10, 5)), CAST(-145.47750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2288, CAST(51.84127 AS Decimal(10, 5)), CAST(-8.49111 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2289, CAST(12.16290 AS Decimal(10, 5)), CAST(-83.06380 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2302, CAST(18.38174 AS Decimal(10, 5)), CAST(-88.41179 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2303, CAST(9.33284 AS Decimal(10, 5)), CAST(-75.28499 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2306, CAST(27.77058 AS Decimal(10, 5)), CAST(-97.50138 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2310, CAST(-27.44550 AS Decimal(10, 5)), CAST(-58.76186 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2314, CAST(37.30277 AS Decimal(10, 5)), CAST(-108.62871 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2317, CAST(-19.01193 AS Decimal(10, 5)), CAST(-57.67305 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2318, CAST(39.67144 AS Decimal(10, 5)), CAST(-31.11623 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2321, CAST(7.16464 AS Decimal(10, 5)), CAST(124.21001 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2324, CAST(8.60156 AS Decimal(10, 5)), CAST(-82.96860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2325, CAST(6.35723 AS Decimal(10, 5)), CAST(2.38435 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2337, CAST(52.36972 AS Decimal(10, 5)), CAST(-1.47972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2341, CAST(49.63606 AS Decimal(10, 5)), CAST(-114.09645 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2343, CAST(21.45219 AS Decimal(10, 5)), CAST(91.96389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2346, CAST(20.51643 AS Decimal(10, 5)), CAST(-86.93207 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2349, CAST(55.47861 AS Decimal(10, 5)), CAST(-133.14778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2351, CAST(-16.26500 AS Decimal(10, 5)), CAST(167.92400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2353, CAST(44.31814 AS Decimal(10, 5)), CAST(23.88861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2354, CAST(49.61222 AS Decimal(10, 5)), CAST(-115.78194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2359, CAST(41.77775 AS Decimal(10, 5)), CAST(-124.23580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2371, CAST(61.86616 AS Decimal(10, 5)), CAST(-158.13227 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2372, CAST(22.74563 AS Decimal(10, 5)), CAST(-74.18072 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2375, CAST(54.61060 AS Decimal(10, 5)), CAST(-97.76080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2378, CAST(38.99723 AS Decimal(10, 5)), CAST(17.08017 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2382, CAST(-7.59991 AS Decimal(10, 5)), CAST(-72.76949 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2386, CAST(7.92757 AS Decimal(10, 5)), CAST(-72.51155 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2388, CAST(14.50996 AS Decimal(10, 5)), CAST(78.77283 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2390, CAST(-2.88947 AS Decimal(10, 5)), CAST(-78.98440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2392, CAST(-15.65293 AS Decimal(10, 5)), CAST(-56.11672 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2394, CAST(18.31178 AS Decimal(10, 5)), CAST(-65.30315 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2395, CAST(24.76455 AS Decimal(10, 5)), CAST(-107.47472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2398, CAST(10.45033 AS Decimal(10, 5)), CAST(-64.13047 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2400, CAST(44.54702 AS Decimal(10, 5)), CAST(7.62322 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2401, CAST(-28.03000 AS Decimal(10, 5)), CAST(145.62199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2404, CAST(-25.52848 AS Decimal(10, 5)), CAST(-49.17578 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2415, CAST(10.85810 AS Decimal(10, 5)), CAST(121.06900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2416, CAST(-13.53572 AS Decimal(10, 5)), CAST(-71.93878 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2418, CAST(16.04392 AS Decimal(10, 5)), CAST(108.19937 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2425, CAST(35.89411 AS Decimal(10, 5)), CAST(128.65886 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2431, CAST(14.73971 AS Decimal(10, 5)), CAST(-17.49022 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2432, CAST(23.71830 AS Decimal(10, 5)), CAST(-15.93200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2434, CAST(36.71306 AS Decimal(10, 5)), CAST(28.79250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2435, CAST(43.60886 AS Decimal(10, 5)), CAST(104.36720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2436, CAST(11.75255 AS Decimal(10, 5)), CAST(108.36807 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2437, CAST(28.87830 AS Decimal(10, 5)), CAST(64.39980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2442, CAST(25.64940 AS Decimal(10, 5)), CAST(100.31900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2443, CAST(38.96567 AS Decimal(10, 5)), CAST(121.53860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2446, CAST(32.89595 AS Decimal(10, 5)), CAST(-97.03720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2448, CAST(32.84707 AS Decimal(10, 5)), CAST(-96.85195 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2460, CAST(33.41064 AS Decimal(10, 5)), CAST(36.51427 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2464, CAST(26.47116 AS Decimal(10, 5)), CAST(49.79789 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2467, CAST(40.02470 AS Decimal(10, 5)), CAST(124.28600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2470, CAST(16.98251 AS Decimal(10, 5)), CAST(-88.23099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2476, CAST(-6.87632 AS Decimal(10, 5)), CAST(39.20179 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2482, CAST(-9.57993 AS Decimal(10, 5)), CAST(143.78061 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2483, CAST(-9.08676 AS Decimal(10, 5)), CAST(143.20799 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2486, CAST(-12.41472 AS Decimal(10, 5)), CAST(130.87667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2488, CAST(41.76110 AS Decimal(10, 5)), CAST(59.82670 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2492, CAST(40.06030 AS Decimal(10, 5)), CAST(113.48200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2498, CAST(7.12552 AS Decimal(10, 5)), CAST(125.64578 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2501, CAST(8.39100 AS Decimal(10, 5)), CAST(-82.43499 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2504, CAST(24.44568 AS Decimal(10, 5)), CAST(44.12912 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2505, CAST(14.10390 AS Decimal(10, 5)), CAST(98.20360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2506, CAST(64.04306 AS Decimal(10, 5)), CAST(-139.12778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2507, CAST(55.74233 AS Decimal(10, 5)), CAST(-120.18300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2509, CAST(31.13217 AS Decimal(10, 5)), CAST(107.43007 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2511, CAST(29.10280 AS Decimal(10, 5)), CAST(110.44300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2512, CAST(39.90228 AS Decimal(10, 5)), CAST(-84.21939 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2517, CAST(29.18464 AS Decimal(10, 5)), CAST(-81.06052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2520, CAST(23.17871 AS Decimal(10, 5)), CAST(-75.09400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2523, CAST(58.42509 AS Decimal(10, 5)), CAST(-130.02354 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2525, CAST(49.36300 AS Decimal(10, 5)), CAST(0.16000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2529, CAST(47.48892 AS Decimal(10, 5)), CAST(21.61533 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2531, CAST(39.83448 AS Decimal(10, 5)), CAST(-88.86646 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2539, CAST(49.21083 AS Decimal(10, 5)), CAST(-57.39139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2540, CAST(52.65580 AS Decimal(10, 5)), CAST(-94.06140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2542, CAST(66.06876 AS Decimal(10, 5)), CAST(-162.76709 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2546, CAST(30.18970 AS Decimal(10, 5)), CAST(78.18030 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2548, CAST(29.37190 AS Decimal(10, 5)), CAST(-100.92343 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2552, CAST(28.56650 AS Decimal(10, 5)), CAST(77.10309 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2553, CAST(65.21054 AS Decimal(10, 5)), CAST(-123.43569 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2558, CAST(64.05187 AS Decimal(10, 5)), CAST(-145.72975 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2559, CAST(8.55400 AS Decimal(10, 5)), CAST(34.85800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2566, CAST(37.78557 AS Decimal(10, 5)), CAST(29.70130 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2567, CAST(-8.75000 AS Decimal(10, 5)), CAST(115.16667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2570, CAST(39.85891 AS Decimal(10, 5)), CAST(-104.67326 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2574, CAST(29.96100 AS Decimal(10, 5)), CAST(70.48590 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2581, CAST(41.53934 AS Decimal(10, 5)), CAST(-93.65860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2585, CAST(11.11100 AS Decimal(10, 5)), CAST(39.72500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2589, CAST(42.22205 AS Decimal(10, 5)), CAST(-83.35147 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2590, CAST(42.23782 AS Decimal(10, 5)), CAST(-83.53021 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2595, CAST(48.11361 AS Decimal(10, 5)), CAST(-98.90790 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2596, CAST(-41.16970 AS Decimal(10, 5)), CAST(146.42999 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2598, CAST(32.43528 AS Decimal(10, 5)), CAST(48.39699 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2600, CAST(23.84333 AS Decimal(10, 5)), CAST(90.39778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2604, CAST(28.75330 AS Decimal(10, 5)), CAST(80.58190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2605, CAST(32.16490 AS Decimal(10, 5)), CAST(76.26293 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2613, CAST(27.48333 AS Decimal(10, 5)), CAST(95.01667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2614, CAST(46.79584 AS Decimal(10, 5)), CAST(-102.80046 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2618, CAST(21.39770 AS Decimal(10, 5)), CAST(103.00541 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2624, CAST(73.51781 AS Decimal(10, 5)), CAST(80.37967 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2627, CAST(-8.54655 AS Decimal(10, 5)), CAST(125.52472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2628, CAST(59.04148 AS Decimal(10, 5)), CAST(-158.51183 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2631, CAST(-18.76940 AS Decimal(10, 5)), CAST(169.00101 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2632, CAST(25.88390 AS Decimal(10, 5)), CAST(93.77110 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2635, CAST(48.58768 AS Decimal(10, 5)), CAST(-2.07996 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2641, CAST(8.60012 AS Decimal(10, 5)), CAST(123.34248 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2642, CAST(27.79360 AS Decimal(10, 5)), CAST(99.67720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2643, CAST(9.62470 AS Decimal(10, 5)), CAST(41.85420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2648, CAST(20.71310 AS Decimal(10, 5)), CAST(70.92110 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2650, CAST(-20.18070 AS Decimal(10, 5)), CAST(-44.87090 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2653, CAST(37.89390 AS Decimal(10, 5)), CAST(40.20102 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2655, CAST(24.29277 AS Decimal(10, 5)), CAST(9.45244 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2657, CAST(33.87334 AS Decimal(10, 5)), CAST(10.77553 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2659, CAST(11.54733 AS Decimal(10, 5)), CAST(43.15948 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2663, CAST(48.35722 AS Decimal(10, 5)), CAST(35.10056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2665, CAST(-5.77222 AS Decimal(10, 5)), CAST(134.21201 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2668, CAST(37.76045 AS Decimal(10, 5)), CAST(-99.96697 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2670, CAST(-6.16939 AS Decimal(10, 5)), CAST(35.74933 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2671, CAST(25.26911 AS Decimal(10, 5)), CAST(51.61042 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2675, CAST(47.03901 AS Decimal(10, 5)), CAST(5.42725 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2677, CAST(28.98532 AS Decimal(10, 5)), CAST(82.81893 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2680, CAST(15.33841 AS Decimal(10, 5)), CAST(-61.39186 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2681, CAST(15.54703 AS Decimal(10, 5)), CAST(-61.30000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2685, CAST(55.04413 AS Decimal(10, 5)), CAST(-8.34128 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2691, CAST(39.49387 AS Decimal(10, 5)), CAST(109.86174 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2692, CAST(37.50874 AS Decimal(10, 5)), CAST(118.78727 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2693, CAST(-17.94015 AS Decimal(10, 5)), CAST(138.82024 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2703, CAST(51.51831 AS Decimal(10, 5)), CAST(7.61224 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2706, CAST(31.32411 AS Decimal(10, 5)), CAST(-85.45060 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2708, CAST(4.00608 AS Decimal(10, 5)), CAST(9.71948 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2712, CAST(42.79719 AS Decimal(10, 5)), CAST(-105.38637 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2714, CAST(-22.20190 AS Decimal(10, 5)), CAST(-54.92660 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2726, CAST(51.13277 AS Decimal(10, 5)), CAST(13.76716 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2735, CAST(49.83167 AS Decimal(10, 5)), CAST(-92.74417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2738, CAST(25.25278 AS Decimal(10, 5)), CAST(55.36444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2740, CAST(-32.21667 AS Decimal(10, 5)), CAST(148.57472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2742, CAST(53.42133 AS Decimal(10, 5)), CAST(-6.27008 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2745, CAST(41.17812 AS Decimal(10, 5)), CAST(-78.89864 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2746, CAST(42.56135 AS Decimal(10, 5)), CAST(18.26824 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2747, CAST(42.40278 AS Decimal(10, 5)), CAST(-90.70902 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2755, CAST(46.84188 AS Decimal(10, 5)), CAST(-92.19380 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2756, CAST(46.78327 AS Decimal(10, 5)), CAST(-92.10658 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2758, CAST(9.33371 AS Decimal(10, 5)), CAST(123.30047 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2759, CAST(1.60919 AS Decimal(10, 5)), CAST(101.43400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2766, CAST(56.45250 AS Decimal(10, 5)), CAST(-3.02583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2768, CAST(-7.40089 AS Decimal(10, 5)), CAST(20.81850 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2769, CAST(-45.92806 AS Decimal(10, 5)), CAST(170.19833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2770, CAST(40.16110 AS Decimal(10, 5)), CAST(94.80920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2776, CAST(37.15139 AS Decimal(10, 5)), CAST(-107.75423 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2778, CAST(24.12404 AS Decimal(10, 5)), CAST(-104.53152 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2781, CAST(-29.61270 AS Decimal(10, 5)), CAST(31.11624 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2785, CAST(35.87946 AS Decimal(10, 5)), CAST(-78.78710 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2787, CAST(54.50919 AS Decimal(10, 5)), CAST(-1.42941 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2789, CAST(38.54333 AS Decimal(10, 5)), CAST(68.82500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2790, CAST(51.28066 AS Decimal(10, 5)), CAST(6.76715 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2793, CAST(51.60250 AS Decimal(10, 5)), CAST(6.14222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2795, CAST(53.90014 AS Decimal(10, 5)), CAST(-166.54350 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2829, CAST(-12.80472 AS Decimal(10, 5)), CAST(45.28111 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2830, CAST(64.77639 AS Decimal(10, 5)), CAST(-141.15083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2838, CAST(-33.03572 AS Decimal(10, 5)), CAST(27.82648 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2839, CAST(52.22927 AS Decimal(10, 5)), CAST(-78.52190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2842, CAST(-27.16479 AS Decimal(10, 5)), CAST(-109.42183 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2846, CAST(48.70816 AS Decimal(10, 5)), CAST(-122.91074 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2847, CAST(44.86524 AS Decimal(10, 5)), CAST(-91.48488 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2851, CAST(4.59932 AS Decimal(10, 5)), CAST(168.75367 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2854, CAST(59.19060 AS Decimal(10, 5)), CAST(-2.77222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2858, CAST(55.95000 AS Decimal(10, 5)), CAST(-3.37250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2860, CAST(53.30801 AS Decimal(10, 5)), CAST(-113.58447 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2866, CAST(39.55460 AS Decimal(10, 5)), CAST(27.01380 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2868, CAST(-14.89670 AS Decimal(10, 5)), CAST(141.60899 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2870, CAST(60.21388 AS Decimal(10, 5)), CAST(-162.04142 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2872, CAST(58.18618 AS Decimal(10, 5)), CAST(-157.37484 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2875, CAST(65.28333 AS Decimal(10, 5)), CAST(-14.40000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2881, CAST(51.45014 AS Decimal(10, 5)), CAST(5.37453 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2886, CAST(56.74311 AS Decimal(10, 5)), CAST(60.80273 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2891, CAST(59.35313 AS Decimal(10, 5)), CAST(-157.47452 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2892, CAST(7.59658 AS Decimal(10, 5)), CAST(-74.81080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2897, CAST(-50.28030 AS Decimal(10, 5)), CAST(-72.05310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2902, CAST(33.22069 AS Decimal(10, 5)), CAST(-92.81322 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2907, CAST(13.61489 AS Decimal(10, 5)), CAST(25.32465 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2909, CAST(30.57129 AS Decimal(10, 5)), CAST(2.85959 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2919, CAST(11.20200 AS Decimal(10, 5)), CAST(119.41700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2920, CAST(13.15322 AS Decimal(10, 5)), CAST(30.23268 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2921, CAST(33.51263 AS Decimal(10, 5)), CAST(6.78251 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2922, CAST(-34.60994 AS Decimal(10, 5)), CAST(-58.61259 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2923, CAST(31.84955 AS Decimal(10, 5)), CAST(-106.38026 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2924, CAST(31.80677 AS Decimal(10, 5)), CAST(-106.37832 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2932, CAST(8.62410 AS Decimal(10, 5)), CAST(-71.67282 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2933, CAST(5.31921 AS Decimal(10, 5)), CAST(-72.38488 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2934, CAST(29.56128 AS Decimal(10, 5)), CAST(34.96008 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2935, CAST(38.60413 AS Decimal(10, 5)), CAST(39.29595 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2936, CAST(42.76028 AS Decimal(10, 5)), CAST(10.23944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2938, CAST(-12.01940 AS Decimal(10, 5)), CAST(135.57100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2941, CAST(0.40446 AS Decimal(10, 5)), CAST(35.23893 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2945, CAST(58.19500 AS Decimal(10, 5)), CAST(-136.34722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2947, CAST(64.61658 AS Decimal(10, 5)), CAST(-162.27007 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2949, CAST(46.37393 AS Decimal(10, 5)), CAST(44.33087 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2959, CAST(40.82375 AS Decimal(10, 5)), CAST(-115.79247 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2970, CAST(-17.08911 AS Decimal(10, 5)), CAST(168.34188 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2975, CAST(-23.56750 AS Decimal(10, 5)), CAST(148.17900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2978, CAST(47.09221 AS Decimal(10, 5)), CAST(8.30120 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2980, CAST(62.78019 AS Decimal(10, 5)), CAST(-164.49044 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2987, CAST(-8.84900 AS Decimal(10, 5)), CAST(121.66330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2989, CAST(11.34150 AS Decimal(10, 5)), CAST(162.32802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (2999, CAST(68.36259 AS Decimal(10, 5)), CAST(23.42432 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3003, CAST(30.32030 AS Decimal(10, 5)), CAST(109.48500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3004, CAST(0.04239 AS Decimal(10, 5)), CAST(32.44350 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3006, CAST(6.47427 AS Decimal(10, 5)), CAST(7.56196 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3011, CAST(48.32500 AS Decimal(10, 5)), CAST(6.06700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3016, CAST(36.23760 AS Decimal(10, 5)), CAST(43.96320 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3017, CAST(35.15655 AS Decimal(10, 5)), CAST(33.49804 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3020, CAST(50.97981 AS Decimal(10, 5)), CAST(10.95811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3022, CAST(42.08172 AS Decimal(10, 5)), CAST(-80.17701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3027, CAST(31.94655 AS Decimal(10, 5)), CAST(-4.39912 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3028, CAST(44.79273 AS Decimal(10, 5)), CAST(-71.16431 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3030, CAST(39.71020 AS Decimal(10, 5)), CAST(39.52700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3031, CAST(39.95650 AS Decimal(10, 5)), CAST(41.17017 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3033, CAST(55.51667 AS Decimal(10, 5)), CAST(8.55000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3035, CAST(45.71869 AS Decimal(10, 5)), CAST(-87.09420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3041, CAST(0.97428 AS Decimal(10, 5)), CAST(-79.62687 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3043, CAST(-33.68440 AS Decimal(10, 5)), CAST(121.82300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3045, CAST(-15.50503 AS Decimal(10, 5)), CAST(167.21974 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3046, CAST(-42.90795 AS Decimal(10, 5)), CAST(-71.13947 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3048, CAST(31.40000 AS Decimal(10, 5)), CAST(-9.68000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3055, CAST(-21.37830 AS Decimal(10, 5)), CAST(-174.95799 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3058, CAST(44.11997 AS Decimal(10, 5)), CAST(-123.21424 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3068, CAST(38.03656 AS Decimal(10, 5)), CAST(-87.53701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3076, CAST(58.41879 AS Decimal(10, 5)), CAST(-135.44836 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3077, CAST(50.73444 AS Decimal(10, 5)), CAST(-3.41389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3079, CAST(-16.68670 AS Decimal(10, 5)), CAST(-145.32899 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3081, CAST(61.01556 AS Decimal(10, 5)), CAST(9.28806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3083, CAST(22.35060 AS Decimal(10, 5)), CAST(56.48640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3084, CAST(59.53476 AS Decimal(10, 5)), CAST(-1.62847 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3087, CAST(64.81368 AS Decimal(10, 5)), CAST(-147.85967 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3098, CAST(31.36500 AS Decimal(10, 5)), CAST(72.99480 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3099, CAST(37.12113 AS Decimal(10, 5)), CAST(70.51791 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3101, CAST(-2.92019 AS Decimal(10, 5)), CAST(132.26703 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3103, CAST(-16.05410 AS Decimal(10, 5)), CAST(-145.65700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3108, CAST(41.67621 AS Decimal(10, 5)), CAST(-70.95615 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3116, CAST(54.85133 AS Decimal(10, 5)), CAST(-163.40766 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3120, CAST(32.36566 AS Decimal(10, 5)), CAST(62.16608 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3123, CAST(46.91975 AS Decimal(10, 5)), CAST(-96.82526 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3128, CAST(36.74195 AS Decimal(10, 5)), CAST(-108.22980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3129, CAST(51.27583 AS Decimal(10, 5)), CAST(-0.77633 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3130, CAST(37.01443 AS Decimal(10, 5)), CAST(-7.96591 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3133, CAST(62.06565 AS Decimal(10, 5)), CAST(-7.28055 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3138, CAST(17.91705 AS Decimal(10, 5)), CAST(19.11108 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3140, CAST(36.00508 AS Decimal(10, 5)), CAST(-94.16993 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3141, CAST(36.28194 AS Decimal(10, 5)), CAST(-94.30694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3143, CAST(34.99072 AS Decimal(10, 5)), CAST(-78.88030 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3148, CAST(-12.20250 AS Decimal(10, 5)), CAST(-38.90610 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3149, CAST(-8.10750 AS Decimal(10, 5)), CAST(159.57700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3151, CAST(40.35880 AS Decimal(10, 5)), CAST(71.74500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3155, CAST(-3.85493 AS Decimal(10, 5)), CAST(-32.42334 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3157, CAST(33.92726 AS Decimal(10, 5)), CAST(-4.97796 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3178, CAST(41.50056 AS Decimal(10, 5)), CAST(9.09778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3192, CAST(-14.21579 AS Decimal(10, 5)), CAST(-169.42385 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3193, CAST(-18.18190 AS Decimal(10, 5)), CAST(125.55900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3205, CAST(54.67806 AS Decimal(10, 5)), CAST(-101.68167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3206, CAST(-40.09170 AS Decimal(10, 5)), CAST(147.99300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3207, CAST(42.96531 AS Decimal(10, 5)), CAST(-83.74329 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3211, CAST(43.80995 AS Decimal(10, 5)), CAST(11.20510 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3214, CAST(34.18516 AS Decimal(10, 5)), CAST(-79.72367 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3215, CAST(1.58919 AS Decimal(10, 5)), CAST(-75.56440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3216, CAST(16.91382 AS Decimal(10, 5)), CAST(-89.86638 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3217, CAST(39.45527 AS Decimal(10, 5)), CAST(-31.13136 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3219, CAST(-27.66665 AS Decimal(10, 5)), CAST(-48.55047 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3220, CAST(61.58361 AS Decimal(10, 5)), CAST(5.02472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3223, CAST(41.43290 AS Decimal(10, 5)), CAST(15.53500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3226, CAST(59.33440 AS Decimal(10, 5)), CAST(-107.18200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3232, CAST(61.39216 AS Decimal(10, 5)), CAST(5.76199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3238, CAST(-26.21272 AS Decimal(10, 5)), CAST(-58.22811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3245, CAST(52.20140 AS Decimal(10, 5)), CAST(-81.69690 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3252, CAST(58.76667 AS Decimal(10, 5)), CAST(-111.11667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3257, CAST(-25.03806 AS Decimal(10, 5)), CAST(46.95611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3258, CAST(14.59103 AS Decimal(10, 5)), CAST(-61.00317 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3261, CAST(42.55136 AS Decimal(10, 5)), CAST(-94.19246 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3263, CAST(48.65420 AS Decimal(10, 5)), CAST(-93.43970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3264, CAST(66.24066 AS Decimal(10, 5)), CAST(-128.64780 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3265, CAST(51.56190 AS Decimal(10, 5)), CAST(-87.90780 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3273, CAST(26.07147 AS Decimal(10, 5)), CAST(-80.14400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3281, CAST(56.65333 AS Decimal(10, 5)), CAST(-111.22194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3286, CAST(26.53731 AS Decimal(10, 5)), CAST(-81.75997 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3287, CAST(58.83693 AS Decimal(10, 5)), CAST(-122.59965 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3295, CAST(56.01890 AS Decimal(10, 5)), CAST(-87.67610 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3298, CAST(61.76015 AS Decimal(10, 5)), CAST(-121.23653 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3299, CAST(35.33648 AS Decimal(10, 5)), CAST(-94.36744 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3300, CAST(60.02028 AS Decimal(10, 5)), CAST(-111.96194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3301, CAST(56.23806 AS Decimal(10, 5)), CAST(-120.74028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3306, CAST(40.98640 AS Decimal(10, 5)), CAST(-85.18750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3311, CAST(32.81930 AS Decimal(10, 5)), CAST(-97.36196 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3312, CAST(66.57139 AS Decimal(10, 5)), CAST(-145.25028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3313, CAST(-3.77628 AS Decimal(10, 5)), CAST(-38.53256 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3314, CAST(10.46950 AS Decimal(10, 5)), CAST(-84.57876 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3320, CAST(60.12197 AS Decimal(10, 5)), CAST(-2.05346 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3327, CAST(-1.65616 AS Decimal(10, 5)), CAST(13.43804 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3329, CAST(-21.15960 AS Decimal(10, 5)), CAST(27.47452 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3332, CAST(49.94867 AS Decimal(10, 5)), CAST(7.26389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3334, CAST(50.04060 AS Decimal(10, 5)), CAST(8.55603 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3339, CAST(41.37784 AS Decimal(10, 5)), CAST(-79.86033 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3346, CAST(45.86999 AS Decimal(10, 5)), CAST(-66.53217 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3350, CAST(26.55869 AS Decimal(10, 5)), CAST(-78.69555 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3353, CAST(8.61644 AS Decimal(10, 5)), CAST(-13.19549 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3368, CAST(36.77606 AS Decimal(10, 5)), CAST(-119.71903 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3372, CAST(48.52621 AS Decimal(10, 5)), CAST(-123.02518 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3373, CAST(47.66667 AS Decimal(10, 5)), CAST(9.50000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3383, CAST(28.45272 AS Decimal(10, 5)), CAST(-13.86376 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3384, CAST(32.66667 AS Decimal(10, 5)), CAST(128.83333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3386, CAST(33.58594 AS Decimal(10, 5)), CAST(130.45069 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3387, CAST(37.22743 AS Decimal(10, 5)), CAST(140.43068 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3393, CAST(-8.52517 AS Decimal(10, 5)), CAST(179.19570 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3395, CAST(23.08321 AS Decimal(10, 5)), CAST(113.06923 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3397, CAST(-19.51640 AS Decimal(10, 5)), CAST(170.23199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3398, CAST(-14.31163 AS Decimal(10, 5)), CAST(-178.06752 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3399, CAST(32.87984 AS Decimal(10, 5)), CAST(115.73650 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3400, CAST(46.80194 AS Decimal(10, 5)), CAST(89.50793 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3401, CAST(25.93506 AS Decimal(10, 5)), CAST(119.66327 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3403, CAST(33.73426 AS Decimal(10, 5)), CAST(9.92005 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3404, CAST(-24.55523 AS Decimal(10, 5)), CAST(25.91821 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3405, CAST(30.33757 AS Decimal(10, 5)), CAST(50.82796 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3411, CAST(6.13333 AS Decimal(10, 5)), CAST(-5.95000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3413, CAST(29.69025 AS Decimal(10, 5)), CAST(-82.27149 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3419, CAST(-0.45352 AS Decimal(10, 5)), CAST(-90.26616 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3421, CAST(6.78083 AS Decimal(10, 5)), CAST(47.45470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3422, CAST(1.83831 AS Decimal(10, 5)), CAST(127.78657 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3423, CAST(64.73611 AS Decimal(10, 5)), CAST(-156.93722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3426, CAST(67.13333 AS Decimal(10, 5)), CAST(20.81667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3432, CAST(8.12876 AS Decimal(10, 5)), CAST(34.56310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3433, CAST(63.76667 AS Decimal(10, 5)), CAST(-171.73278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3437, CAST(-0.69334 AS Decimal(10, 5)), CAST(73.15560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3440, CAST(48.94483 AS Decimal(10, 5)), CAST(-54.56081 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3445, CAST(25.85088 AS Decimal(10, 5)), CAST(114.77537 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3456, CAST(37.92764 AS Decimal(10, 5)), CAST(-100.72036 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3462, CAST(-0.46271 AS Decimal(10, 5)), CAST(39.64824 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3464, CAST(8.46091 AS Decimal(10, 5)), CAST(48.57227 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3465, CAST(9.33589 AS Decimal(10, 5)), CAST(13.37010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3470, CAST(48.77528 AS Decimal(10, 5)), CAST(-64.47861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3472, CAST(26.30282 AS Decimal(10, 5)), CAST(43.77391 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3476, CAST(-14.21810 AS Decimal(10, 5)), CAST(167.58701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3479, CAST(26.10609 AS Decimal(10, 5)), CAST(91.58594 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3480, CAST(24.74431 AS Decimal(10, 5)), CAST(84.95118 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3484, CAST(36.94718 AS Decimal(10, 5)), CAST(37.47868 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3488, CAST(54.37808 AS Decimal(10, 5)), CAST(18.46823 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3498, CAST(44.58209 AS Decimal(10, 5)), CAST(38.01248 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3500, CAST(3.23537 AS Decimal(10, 5)), CAST(19.77126 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3502, CAST(13.48333 AS Decimal(10, 5)), CAST(22.46667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3505, CAST(6.10644 AS Decimal(10, 5)), CAST(125.23500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3509, CAST(46.23756 AS Decimal(10, 5)), CAST(6.10921 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3510, CAST(44.41493 AS Decimal(10, 5)), CAST(8.85041 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3513, CAST(-34.00555 AS Decimal(10, 5)), CAST(22.37889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3514, CAST(23.56123 AS Decimal(10, 5)), CAST(-75.87484 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3517, CAST(6.49855 AS Decimal(10, 5)), CAST(-58.25412 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3524, CAST(-28.79572 AS Decimal(10, 5)), CAST(114.70400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3529, CAST(41.90097 AS Decimal(10, 5)), CAST(2.76055 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3530, CAST(50.25905 AS Decimal(10, 5)), CAST(-60.66905 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3536, CAST(32.38411 AS Decimal(10, 5)), CAST(3.79411 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3537, CAST(25.14556 AS Decimal(10, 5)), CAST(10.14265 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3543, CAST(26.75500 AS Decimal(10, 5)), CAST(55.90222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3547, CAST(36.15122 AS Decimal(10, 5)), CAST(-5.34966 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3553, CAST(35.91889 AS Decimal(10, 5)), CAST(74.33389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3554, CAST(56.35739 AS Decimal(10, 5)), CAST(-94.71116 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3555, CAST(44.34887 AS Decimal(10, 5)), CAST(-105.53972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3556, CAST(49.69420 AS Decimal(10, 5)), CAST(-124.51800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3559, CAST(-38.66333 AS Decimal(10, 5)), CAST(177.97833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3563, CAST(-8.09763 AS Decimal(10, 5)), CAST(156.86361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3566, CAST(68.63556 AS Decimal(10, 5)), CAST(-95.84972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3571, CAST(32.38487 AS Decimal(10, 5)), CAST(-94.71188 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3572, CAST(-23.86887 AS Decimal(10, 5)), CAST(151.22035 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3575, CAST(55.87194 AS Decimal(10, 5)), CAST(-4.43306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3577, CAST(55.50944 AS Decimal(10, 5)), CAST(-4.58667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3580, CAST(48.21224 AS Decimal(10, 5)), CAST(-106.61532 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3585, CAST(47.13862 AS Decimal(10, 5)), CAST(-104.80746 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3595, CAST(51.89383 AS Decimal(10, 5)), CAST(-2.16450 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3598, CAST(15.38083 AS Decimal(10, 5)), CAST(73.83142 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3599, CAST(7.01700 AS Decimal(10, 5)), CAST(40.00000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3602, CAST(5.93513 AS Decimal(10, 5)), CAST(43.57857 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3603, CAST(54.55890 AS Decimal(10, 5)), CAST(-94.49140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3604, CAST(54.83970 AS Decimal(10, 5)), CAST(-94.07860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3608, CAST(-16.63203 AS Decimal(10, 5)), CAST(-49.22069 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3611, CAST(-28.16444 AS Decimal(10, 5)), CAST(153.50472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3615, CAST(8.65401 AS Decimal(10, 5)), CAST(-83.18220 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3616, CAST(36.40060 AS Decimal(10, 5)), CAST(94.78610 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3617, CAST(64.54333 AS Decimal(10, 5)), CAST(-163.03944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3618, CAST(-1.67081 AS Decimal(10, 5)), CAST(29.23846 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3619, CAST(52.52702 AS Decimal(10, 5)), CAST(31.01669 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3621, CAST(12.51990 AS Decimal(10, 5)), CAST(37.43405 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3624, CAST(59.11728 AS Decimal(10, 5)), CAST(-161.58140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3627, CAST(53.31601 AS Decimal(10, 5)), CAST(-60.41880 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3629, CAST(26.73971 AS Decimal(10, 5)), CAST(83.44971 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3636, CAST(36.90938 AS Decimal(10, 5)), CAST(54.40134 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3641, CAST(-6.08169 AS Decimal(10, 5)), CAST(145.39188 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3643, CAST(0.63712 AS Decimal(10, 5)), CAST(122.84986 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3649, CAST(57.66818 AS Decimal(10, 5)), CAST(12.29083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3655, CAST(29.02670 AS Decimal(10, 5)), CAST(-10.05030 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3658, CAST(-12.27128 AS Decimal(10, 5)), CAST(136.82017 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3659, CAST(-18.89520 AS Decimal(10, 5)), CAST(-41.98220 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3660, CAST(25.28470 AS Decimal(10, 5)), CAST(-76.33100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3666, CAST(39.09220 AS Decimal(10, 5)), CAST(-28.02980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3667, CAST(-29.75940 AS Decimal(10, 5)), CAST(153.03000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3669, CAST(37.18873 AS Decimal(10, 5)), CAST(-3.77736 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3673, CAST(35.95221 AS Decimal(10, 5)), CAST(-112.14684 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3675, CAST(35.13835 AS Decimal(10, 5)), CAST(-111.67183 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3676, CAST(19.29278 AS Decimal(10, 5)), CAST(-81.35775 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3679, CAST(47.94925 AS Decimal(10, 5)), CAST(-97.17611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3680, CAST(40.96723 AS Decimal(10, 5)), CAST(-98.30895 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3681, CAST(39.12303 AS Decimal(10, 5)), CAST(-108.52868 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3683, CAST(42.88461 AS Decimal(10, 5)), CAST(-85.52958 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3685, CAST(21.44450 AS Decimal(10, 5)), CAST(-71.14230 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3687, CAST(55.17814 AS Decimal(10, 5)), CAST(-118.88040 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3692, CAST(-20.54930 AS Decimal(10, 5)), CAST(130.34720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3701, CAST(62.89504 AS Decimal(10, 5)), CAST(-160.06536 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3702, CAST(46.99107 AS Decimal(10, 5)), CAST(15.43963 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3707, CAST(47.48190 AS Decimal(10, 5)), CAST(-111.37136 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3713, CAST(44.48507 AS Decimal(10, 5)), CAST(-88.12959 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3714, CAST(22.67278 AS Decimal(10, 5)), CAST(121.46639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3720, CAST(36.10613 AS Decimal(10, 5)), CAST(-79.93701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3723, CAST(33.48234 AS Decimal(10, 5)), CAST(-90.98510 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3724, CAST(35.63360 AS Decimal(10, 5)), CAST(-77.38104 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3727, CAST(34.89567 AS Decimal(10, 5)), CAST(-82.21886 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3737, CAST(12.00417 AS Decimal(10, 5)), CAST(-61.78611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3743, CAST(-34.25080 AS Decimal(10, 5)), CAST(146.06700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3746, CAST(66.54662 AS Decimal(10, 5)), CAST(-18.01723 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3748, CAST(76.42552 AS Decimal(10, 5)), CAST(-82.90730 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3749, CAST(53.60165 AS Decimal(10, 5)), CAST(24.04963 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3752, CAST(53.11972 AS Decimal(10, 5)), CAST(6.57944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3753, CAST(-13.97500 AS Decimal(10, 5)), CAST(136.46001 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3755, CAST(42.75975 AS Decimal(10, 5)), CAST(11.07190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3758, CAST(43.29595 AS Decimal(10, 5)), CAST(45.77711 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3762, CAST(20.52180 AS Decimal(10, 5)), CAST(-103.31117 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3769, CAST(13.48299 AS Decimal(10, 5)), CAST(144.79772 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3771, CAST(13.58311 AS Decimal(10, 5)), CAST(144.92825 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3778, CAST(32.39110 AS Decimal(10, 5)), CAST(105.70200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3782, CAST(23.39244 AS Decimal(10, 5)), CAST(113.29879 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3783, CAST(19.90639 AS Decimal(10, 5)), CAST(-75.20694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3785, CAST(25.04571 AS Decimal(10, 5)), CAST(-77.46621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3786, CAST(2.57103 AS Decimal(10, 5)), CAST(-77.89762 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3795, CAST(14.58327 AS Decimal(10, 5)), CAST(-90.52747 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3796, CAST(-2.15742 AS Decimal(10, 5)), CAST(-79.88356 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3797, CAST(-10.81800 AS Decimal(10, 5)), CAST(-65.34719 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3798, CAST(27.96924 AS Decimal(10, 5)), CAST(-110.92376 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3805, CAST(28.02610 AS Decimal(10, 5)), CAST(-114.02400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3810, CAST(25.21834 AS Decimal(10, 5)), CAST(110.04121 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3813, CAST(26.53852 AS Decimal(10, 5)), CAST(106.80070 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3816, CAST(30.40770 AS Decimal(10, 5)), CAST(-89.06976 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3824, CAST(38.53388 AS Decimal(10, 5)), CAST(-106.93365 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3825, CAST(35.90376 AS Decimal(10, 5)), CAST(126.61591 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3827, CAST(1.16638 AS Decimal(10, 5)), CAST(97.70468 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3828, CAST(31.41278 AS Decimal(10, 5)), CAST(37.27389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3835, CAST(58.42257 AS Decimal(10, 5)), CAST(-135.70923 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3841, CAST(25.23306 AS Decimal(10, 5)), CAST(62.32889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3842, CAST(26.29334 AS Decimal(10, 5)), CAST(78.22775 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3843, CAST(35.12639 AS Decimal(10, 5)), CAST(126.80889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3845, CAST(40.73768 AS Decimal(10, 5)), CAST(46.31758 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3848, CAST(40.75103 AS Decimal(10, 5)), CAST(43.85983 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3849, CAST(-19.77763 AS Decimal(10, 5)), CAST(-174.34095 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3851, CAST(33.11500 AS Decimal(10, 5)), CAST(139.78583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3857, CAST(39.70787 AS Decimal(10, 5)), CAST(-77.72944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3858, CAST(60.02010 AS Decimal(10, 5)), CAST(13.57890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3860, CAST(32.80944 AS Decimal(10, 5)), CAST(35.04306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3861, CAST(19.93672 AS Decimal(10, 5)), CAST(110.45963 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3862, CAST(27.43792 AS Decimal(10, 5)), CAST(41.68629 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3863, CAST(49.20500 AS Decimal(10, 5)), CAST(119.82500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3866, CAST(59.24444 AS Decimal(10, 5)), CAST(-135.52361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3868, CAST(20.81940 AS Decimal(10, 5)), CAST(106.72500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3870, CAST(41.77000 AS Decimal(10, 5)), CAST(140.82194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3875, CAST(44.88392 AS Decimal(10, 5)), CAST(-63.51171 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3879, CAST(68.77611 AS Decimal(10, 5)), CAST(-81.24361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3881, CAST(-18.23390 AS Decimal(10, 5)), CAST(127.67000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3883, CAST(56.69113 AS Decimal(10, 5)), CAST(12.82021 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3885, CAST(34.86917 AS Decimal(10, 5)), CAST(48.55250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3887, CAST(53.63039 AS Decimal(10, 5)), CAST(9.98823 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3893, CAST(42.84098 AS Decimal(10, 5)), CAST(93.66909 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3895, CAST(-37.86544 AS Decimal(10, 5)), CAST(175.33270 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3898, CAST(-20.35810 AS Decimal(10, 5)), CAST(148.95200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3901, CAST(70.68037 AS Decimal(10, 5)), CAST(23.67262 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3907, CAST(37.13181 AS Decimal(10, 5)), CAST(-76.49273 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3909, CAST(20.79502 AS Decimal(10, 5)), CAST(-156.01444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3910, CAST(39.42861 AS Decimal(10, 5)), CAST(141.13528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3913, CAST(47.16826 AS Decimal(10, 5)), CAST(-88.48901 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3914, CAST(36.52447 AS Decimal(10, 5)), CAST(114.42724 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3915, CAST(30.22950 AS Decimal(10, 5)), CAST(120.43445 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3917, CAST(6.74567 AS Decimal(10, 5)), CAST(73.16929 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3920, CAST(21.22119 AS Decimal(10, 5)), CAST(105.80718 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3921, CAST(52.45957 AS Decimal(10, 5)), CAST(9.69269 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3925, CAST(43.62834 AS Decimal(10, 5)), CAST(-72.30931 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3927, CAST(33.06360 AS Decimal(10, 5)), CAST(107.00800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3928, CAST(-18.07481 AS Decimal(10, 5)), CAST(-140.94589 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3930, CAST(-17.93181 AS Decimal(10, 5)), CAST(31.09285 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3931, CAST(45.62340 AS Decimal(10, 5)), CAST(126.25033 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3933, CAST(9.51450 AS Decimal(10, 5)), CAST(44.08444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3934, CAST(26.22869 AS Decimal(10, 5)), CAST(-97.65443 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3937, CAST(40.19342 AS Decimal(10, 5)), CAST(-76.76330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3942, CAST(36.26146 AS Decimal(10, 5)), CAST(-93.15462 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3945, CAST(68.49130 AS Decimal(10, 5)), CAST(16.67811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3947, CAST(41.93798 AS Decimal(10, 5)), CAST(-72.68782 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3956, CAST(31.67297 AS Decimal(10, 5)), CAST(6.14044 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3960, CAST(70.48668 AS Decimal(10, 5)), CAST(22.13974 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3961, CAST(6.93100 AS Decimal(10, 5)), CAST(100.39395 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3962, CAST(71.97810 AS Decimal(10, 5)), CAST(102.49100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3972, CAST(59.34527 AS Decimal(10, 5)), CAST(5.20836 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3974, CAST(22.98915 AS Decimal(10, 5)), CAST(-82.40909 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3979, CAST(48.54278 AS Decimal(10, 5)), CAST(-109.76298 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3980, CAST(50.28194 AS Decimal(10, 5)), CAST(-63.61139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3984, CAST(33.92279 AS Decimal(10, 5)), CAST(-118.33591 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3987, CAST(60.83972 AS Decimal(10, 5)), CAST(-115.78278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3989, CAST(40.48109 AS Decimal(10, 5)), CAST(-107.21811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (3992, CAST(38.85057 AS Decimal(10, 5)), CAST(-99.27649 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4000, CAST(63.99589 AS Decimal(10, 5)), CAST(-144.69352 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4006, CAST(31.99082 AS Decimal(10, 5)), CAST(116.96908 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4007, CAST(20.74700 AS Decimal(10, 5)), CAST(96.79200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4011, CAST(50.17162 AS Decimal(10, 5)), CAST(127.30888 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4017, CAST(46.60660 AS Decimal(10, 5)), CAST(-111.98333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4022, CAST(60.31722 AS Decimal(10, 5)), CAST(24.96333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4024, CAST(65.80600 AS Decimal(10, 5)), CAST(15.08280 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4032, CAST(26.90530 AS Decimal(10, 5)), CAST(112.62800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4034, CAST(35.33907 AS Decimal(10, 5)), CAST(25.17513 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4035, CAST(34.21110 AS Decimal(10, 5)), CAST(62.22851 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4039, CAST(53.87871 AS Decimal(10, 5)), CAST(14.15235 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4043, CAST(29.09586 AS Decimal(10, 5)), CAST(-111.04786 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4052, CAST(-25.32184 AS Decimal(10, 5)), CAST(152.88422 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4060, CAST(58.62139 AS Decimal(10, 5)), CAST(-117.16472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4072, CAST(19.71607 AS Decimal(10, 5)), CAST(-155.05605 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4073, CAST(32.22319 AS Decimal(10, 5)), CAST(-80.69754 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4079, CAST(34.43611 AS Decimal(10, 5)), CAST(132.91944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4087, CAST(10.81880 AS Decimal(10, 5)), CAST(106.65186 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4089, CAST(-42.83611 AS Decimal(10, 5)), CAST(147.51028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4092, CAST(32.68734 AS Decimal(10, 5)), CAST(-103.21687 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4100, CAST(40.85142 AS Decimal(10, 5)), CAST(111.82410 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4101, CAST(-42.71361 AS Decimal(10, 5)), CAST(170.98528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4105, CAST(20.78559 AS Decimal(10, 5)), CAST(-76.31511 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4108, CAST(55.48046 AS Decimal(10, 5)), CAST(-132.65313 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4114, CAST(62.18991 AS Decimal(10, 5)), CAST(-159.77369 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4116, CAST(24.87000 AS Decimal(10, 5)), CAST(94.92000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4118, CAST(59.64691 AS Decimal(10, 5)), CAST(-151.47284 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4124, CAST(22.31546 AS Decimal(10, 5)), CAST(113.93372 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4126, CAST(-9.42800 AS Decimal(10, 5)), CAST(160.05479 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4128, CAST(71.00948 AS Decimal(10, 5)), CAST(25.97786 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4130, CAST(21.32452 AS Decimal(10, 5)), CAST(-157.92507 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4135, CAST(21.15152 AS Decimal(10, 5)), CAST(-157.09748 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4136, CAST(58.09698 AS Decimal(10, 5)), CAST(-135.41102 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4137, CAST(61.52417 AS Decimal(10, 5)), CAST(-166.14667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4140, CAST(55.44830 AS Decimal(10, 5)), CAST(-60.22860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4144, CAST(-10.58640 AS Decimal(10, 5)), CAST(142.28999 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4149, CAST(38.51989 AS Decimal(10, 5)), CAST(-28.71587 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4150, CAST(-5.46217 AS Decimal(10, 5)), CAST(150.40500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4151, CAST(34.47787 AS Decimal(10, 5)), CAST(-93.09601 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4153, CAST(37.03852 AS Decimal(10, 5)), CAST(79.86493 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4155, CAST(20.26075 AS Decimal(10, 5)), CAST(100.43669 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4164, CAST(29.60745 AS Decimal(10, 5)), CAST(-95.15882 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4165, CAST(29.98687 AS Decimal(10, 5)), CAST(-95.34212 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4167, CAST(29.65240 AS Decimal(10, 5)), CAST(-95.27723 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4179, CAST(12.63622 AS Decimal(10, 5)), CAST(99.95153 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4181, CAST(-16.68720 AS Decimal(10, 5)), CAST(-151.02200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4182, CAST(27.43738 AS Decimal(10, 5)), CAST(109.70020 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4183, CAST(24.02372 AS Decimal(10, 5)), CAST(121.61691 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4184, CAST(-12.80888 AS Decimal(10, 5)), CAST(15.76055 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4187, CAST(28.56400 AS Decimal(10, 5)), CAST(121.42746 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4188, CAST(-9.87881 AS Decimal(10, 5)), CAST(-76.20480 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4189, CAST(15.77532 AS Decimal(10, 5)), CAST(-96.26257 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4190, CAST(15.36170 AS Decimal(10, 5)), CAST(75.08490 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4195, CAST(16.39928 AS Decimal(10, 5)), CAST(107.70136 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4200, CAST(-20.81500 AS Decimal(10, 5)), CAST(144.22501 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4201, CAST(66.04458 AS Decimal(10, 5)), CAST(-154.26036 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4203, CAST(23.04972 AS Decimal(10, 5)), CAST(114.59873 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4209, CAST(53.57444 AS Decimal(10, 5)), CAST(-0.35083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4214, CAST(13.83375 AS Decimal(10, 5)), CAST(36.87922 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4217, CAST(34.64640 AS Decimal(10, 5)), CAST(-86.77494 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4221, CAST(27.17832 AS Decimal(10, 5)), CAST(33.79944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4224, CAST(65.69461 AS Decimal(10, 5)), CAST(-156.35930 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4230, CAST(41.66733 AS Decimal(10, 5)), CAST(-70.28741 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4231, CAST(55.20611 AS Decimal(10, 5)), CAST(-132.82806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4232, CAST(55.90315 AS Decimal(10, 5)), CAST(-130.02341 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4233, CAST(17.23132 AS Decimal(10, 5)), CAST(78.42986 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4238, CAST(47.17849 AS Decimal(10, 5)), CAST(27.62063 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4241, CAST(7.36246 AS Decimal(10, 5)), CAST(3.97833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4242, CAST(4.42161 AS Decimal(10, 5)), CAST(-75.13330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4245, CAST(38.87286 AS Decimal(10, 5)), CAST(1.37312 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4252, CAST(43.51297 AS Decimal(10, 5)), CAST(-112.06775 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4258, CAST(67.43720 AS Decimal(10, 5)), CAST(86.62190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4260, CAST(59.32373 AS Decimal(10, 5)), CAST(-155.90327 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4261, CAST(69.36500 AS Decimal(10, 5)), CAST(-81.81746 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4263, CAST(-25.59615 AS Decimal(10, 5)), CAST(-54.48721 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4265, CAST(-25.73728 AS Decimal(10, 5)), CAST(-54.47344 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4271, CAST(37.68270 AS Decimal(10, 5)), CAST(26.34710 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4275, CAST(33.74903 AS Decimal(10, 5)), CAST(129.78542 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4278, CAST(33.58661 AS Decimal(10, 5)), CAST(46.40484 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4280, CAST(-22.58886 AS Decimal(10, 5)), CAST(167.45597 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4285, CAST(47.42472 AS Decimal(10, 5)), CAST(-61.77806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4289, CAST(-14.81604 AS Decimal(10, 5)), CAST(-39.03155 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4290, CAST(59.75380 AS Decimal(10, 5)), CAST(-154.91096 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4295, CAST(26.72354 AS Decimal(10, 5)), CAST(8.62265 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4297, CAST(10.82993 AS Decimal(10, 5)), CAST(122.49395 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4298, CAST(8.44021 AS Decimal(10, 5)), CAST(4.49392 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4300, CAST(69.24079 AS Decimal(10, 5)), CAST(-51.06351 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4305, CAST(-5.53129 AS Decimal(10, 5)), CAST(-47.46005 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4309, CAST(24.75995 AS Decimal(10, 5)), CAST(93.89670 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4310, CAST(28.05155 AS Decimal(10, 5)), CAST(9.64291 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4311, CAST(19.56667 AS Decimal(10, 5)), CAST(5.75000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4312, CAST(27.25102 AS Decimal(10, 5)), CAST(2.51202 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4313, CAST(20.97565 AS Decimal(10, 5)), CAST(-73.67509 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4315, CAST(37.45667 AS Decimal(10, 5)), CAST(126.47045 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4317, CAST(14.07810 AS Decimal(10, 5)), CAST(38.27250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4318, CAST(16.53460 AS Decimal(10, 5)), CAST(-88.44149 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4322, CAST(39.71507 AS Decimal(10, 5)), CAST(-86.29762 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4324, CAST(22.72179 AS Decimal(10, 5)), CAST(75.80109 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4333, CAST(-23.87643 AS Decimal(10, 5)), CAST(35.40854 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4342, CAST(47.26022 AS Decimal(10, 5)), CAST(11.34396 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4344, CAST(66.05483 AS Decimal(10, 5)), CAST(60.11032 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4346, CAST(48.56619 AS Decimal(10, 5)), CAST(-93.39881 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4347, CAST(58.46944 AS Decimal(10, 5)), CAST(-78.08052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4349, CAST(68.30417 AS Decimal(10, 5)), CAST(-133.48278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4350, CAST(-46.40842 AS Decimal(10, 5)), CAST(168.30009 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4351, CAST(-29.88830 AS Decimal(10, 5)), CAST(151.14400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4353, CAST(57.54250 AS Decimal(10, 5)), CAST(-4.04750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4358, CAST(39.69640 AS Decimal(10, 5)), CAST(20.82250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4363, CAST(-19.47070 AS Decimal(10, 5)), CAST(-42.48760 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4364, CAST(0.86193 AS Decimal(10, 5)), CAST(-77.67176 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4368, CAST(4.56797 AS Decimal(10, 5)), CAST(101.09200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4369, CAST(-18.85433 AS Decimal(10, 5)), CAST(169.28046 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4371, CAST(63.75503 AS Decimal(10, 5)), CAST(-68.55254 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4372, CAST(-20.53522 AS Decimal(10, 5)), CAST(-70.18127 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4373, CAST(-3.78474 AS Decimal(10, 5)), CAST(-73.30881 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4375, CAST(27.23837 AS Decimal(10, 5)), CAST(60.71986 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4378, CAST(-7.66863 AS Decimal(10, 5)), CAST(35.75211 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4380, CAST(52.26803 AS Decimal(10, 5)), CAST(104.38898 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4381, CAST(45.81522 AS Decimal(10, 5)), CAST(-88.11768 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4382, CAST(46.52521 AS Decimal(10, 5)), CAST(-90.13485 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4384, CAST(66.05810 AS Decimal(10, 5)), CAST(-23.13530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4388, CAST(32.75084 AS Decimal(10, 5)), CAST(51.86127 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4389, CAST(24.39693 AS Decimal(10, 5)), CAST(124.24675 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4391, CAST(2.82761 AS Decimal(10, 5)), CAST(27.58830 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4394, CAST(33.61667 AS Decimal(10, 5)), CAST(73.10000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4395, CAST(53.85691 AS Decimal(10, 5)), CAST(-94.65383 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4396, CAST(55.68194 AS Decimal(10, 5)), CAST(-6.25667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4401, CAST(49.91318 AS Decimal(10, 5)), CAST(-6.29212 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4404, CAST(40.78992 AS Decimal(10, 5)), CAST(-73.09764 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4406, CAST(37.85542 AS Decimal(10, 5)), CAST(30.36840 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4407, CAST(40.97692 AS Decimal(10, 5)), CAST(28.81461 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4409, CAST(40.89861 AS Decimal(10, 5)), CAST(29.30917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4423, CAST(42.49083 AS Decimal(10, 5)), CAST(-76.45833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4430, CAST(68.60727 AS Decimal(10, 5)), CAST(27.40533 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4433, CAST(48.88417 AS Decimal(10, 5)), CAST(24.68611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4434, CAST(56.93940 AS Decimal(10, 5)), CAST(40.94080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4436, CAST(62.41733 AS Decimal(10, 5)), CAST(-77.92528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4437, CAST(34.67639 AS Decimal(10, 5)), CAST(131.79028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4438, CAST(24.78400 AS Decimal(10, 5)), CAST(141.32272 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4439, CAST(17.60157 AS Decimal(10, 5)), CAST(-101.46054 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4440, CAST(16.44934 AS Decimal(10, 5)), CAST(-95.09370 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4441, CAST(56.82810 AS Decimal(10, 5)), CAST(53.45750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4442, CAST(38.29239 AS Decimal(10, 5)), CAST(27.15695 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4446, CAST(35.41361 AS Decimal(10, 5)), CAST(132.89000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4447, CAST(23.17782 AS Decimal(10, 5)), CAST(80.05205 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4455, CAST(32.30570 AS Decimal(10, 5)), CAST(-90.07647 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4457, CAST(35.60239 AS Decimal(10, 5)), CAST(-88.92145 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4458, CAST(43.60660 AS Decimal(10, 5)), CAST(-110.73854 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4462, CAST(30.49187 AS Decimal(10, 5)), CAST(-81.68569 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4466, CAST(34.83051 AS Decimal(10, 5)), CAST(-77.60609 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4475, CAST(28.58730 AS Decimal(10, 5)), CAST(53.58430 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4476, CAST(26.82419 AS Decimal(10, 5)), CAST(75.81216 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4477, CAST(26.88870 AS Decimal(10, 5)), CAST(70.86500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4479, CAST(-6.26612 AS Decimal(10, 5)), CAST(106.89225 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4481, CAST(-6.12557 AS Decimal(10, 5)), CAST(106.65590 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4483, CAST(34.39986 AS Decimal(10, 5)), CAST(70.49865 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4486, CAST(5.90939 AS Decimal(10, 5)), CAST(169.63702 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4488, CAST(-1.63802 AS Decimal(10, 5)), CAST(103.64438 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4490, CAST(46.92971 AS Decimal(10, 5)), CAST(-98.67816 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4491, CAST(42.15000 AS Decimal(10, 5)), CAST(-79.26667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4492, CAST(32.68910 AS Decimal(10, 5)), CAST(74.83740 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4493, CAST(22.46550 AS Decimal(10, 5)), CAST(70.01260 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4496, CAST(26.70880 AS Decimal(10, 5)), CAST(85.92240 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4500, CAST(7.51778 AS Decimal(10, 5)), CAST(-78.15720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4508, CAST(-11.78310 AS Decimal(10, 5)), CAST(-75.47340 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4509, CAST(-2.57695 AS Decimal(10, 5)), CAST(140.51637 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4510, CAST(16.90111 AS Decimal(10, 5)), CAST(42.58583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4512, CAST(21.66982 AS Decimal(10, 5)), CAST(39.15052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4516, CAST(7.56574 AS Decimal(10, 5)), CAST(168.96256 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4517, CAST(33.51131 AS Decimal(10, 5)), CAST(126.49303 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4526, CAST(36.74462 AS Decimal(10, 5)), CAST(-6.06011 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4530, CAST(23.18380 AS Decimal(10, 5)), CAST(89.16083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4532, CAST(26.85534 AS Decimal(10, 5)), CAST(114.73265 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4533, CAST(46.84340 AS Decimal(10, 5)), CAST(130.46500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4536, CAST(39.86054 AS Decimal(10, 5)), CAST(98.34085 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4537, CAST(36.79514 AS Decimal(10, 5)), CAST(5.87361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4538, CAST(9.33085 AS Decimal(10, 5)), CAST(42.91122 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4540, CAST(7.66438 AS Decimal(10, 5)), CAST(36.81918 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4541, CAST(36.85751 AS Decimal(10, 5)), CAST(117.21348 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4543, CAST(29.33860 AS Decimal(10, 5)), CAST(117.17600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4544, CAST(21.97396 AS Decimal(10, 5)), CAST(100.76696 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4546, CAST(35.29276 AS Decimal(10, 5)), CAST(116.33444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4548, CAST(24.79894 AS Decimal(10, 5)), CAST(118.58970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4549, CAST(35.08854 AS Decimal(10, 5)), CAST(128.07037 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4550, CAST(5.78333 AS Decimal(10, 5)), CAST(36.56300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4551, CAST(41.10140 AS Decimal(10, 5)), CAST(121.06200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4552, CAST(-10.87080 AS Decimal(10, 5)), CAST(-61.84650 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4555, CAST(28.72870 AS Decimal(10, 5)), CAST(57.67050 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4556, CAST(29.47794 AS Decimal(10, 5)), CAST(115.80225 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4560, CAST(-7.14838 AS Decimal(10, 5)), CAST(-34.95068 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4561, CAST(26.25109 AS Decimal(10, 5)), CAST(73.04887 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4562, CAST(62.66291 AS Decimal(10, 5)), CAST(29.60755 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4565, CAST(-26.13348 AS Decimal(10, 5)), CAST(28.23606 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4572, CAST(40.31674 AS Decimal(10, 5)), CAST(-78.83447 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4573, CAST(1.64131 AS Decimal(10, 5)), CAST(103.66962 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4574, CAST(-26.22429 AS Decimal(10, 5)), CAST(-48.79762 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4580, CAST(35.83119 AS Decimal(10, 5)), CAST(-90.64622 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4581, CAST(57.75759 AS Decimal(10, 5)), CAST(14.06873 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4583, CAST(37.15181 AS Decimal(10, 5)), CAST(-94.49827 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4585, CAST(26.73150 AS Decimal(10, 5)), CAST(94.17550 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4586, CAST(9.63983 AS Decimal(10, 5)), CAST(8.86905 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4589, CAST(56.70060 AS Decimal(10, 5)), CAST(47.90470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4590, CAST(29.78513 AS Decimal(10, 5)), CAST(40.10001 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4595, CAST(-7.21859 AS Decimal(10, 5)), CAST(-39.27097 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4598, CAST(-11.41765 AS Decimal(10, 5)), CAST(-58.71059 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4601, CAST(-24.39278 AS Decimal(10, 5)), CAST(-65.09778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4602, CAST(-20.66830 AS Decimal(10, 5)), CAST(141.72301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4603, CAST(-15.46710 AS Decimal(10, 5)), CAST(-70.15817 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4606, CAST(29.27436 AS Decimal(10, 5)), CAST(82.19286 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4610, CAST(58.35472 AS Decimal(10, 5)), CAST(-134.57611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4617, CAST(28.96580 AS Decimal(10, 5)), CAST(118.89900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4619, CAST(62.39945 AS Decimal(10, 5)), CAST(25.67825 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4620, CAST(0.48880 AS Decimal(10, 5)), CAST(72.99527 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4626, CAST(6.73255 AS Decimal(10, 5)), CAST(44.24104 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4627, CAST(34.56585 AS Decimal(10, 5)), CAST(69.21233 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4630, CAST(27.15530 AS Decimal(10, 5)), CAST(69.32375 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4631, CAST(1.85917 AS Decimal(10, 5)), CAST(73.52190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4632, CAST(10.69603 AS Decimal(10, 5)), CAST(7.32011 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4635, CAST(-7.32897 AS Decimal(10, 5)), CAST(157.58360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4637, CAST(31.80340 AS Decimal(10, 5)), CAST(130.71941 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4638, CAST(-6.39693 AS Decimal(10, 5)), CAST(143.85447 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4640, CAST(37.53744 AS Decimal(10, 5)), CAST(36.94731 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4641, CAST(20.89861 AS Decimal(10, 5)), CAST(-156.43028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4647, CAST(-3.64452 AS Decimal(10, 5)), CAST(133.69555 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4653, CAST(64.28547 AS Decimal(10, 5)), CAST(27.69241 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4654, CAST(56.97296 AS Decimal(10, 5)), CAST(-133.94639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4655, CAST(59.43390 AS Decimal(10, 5)), CAST(-154.79801 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4656, CAST(-14.99830 AS Decimal(10, 5)), CAST(22.64540 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4658, CAST(37.38300 AS Decimal(10, 5)), CAST(55.45200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4659, CAST(37.06832 AS Decimal(10, 5)), CAST(22.02552 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4660, CAST(42.23476 AS Decimal(10, 5)), CAST(-85.55195 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4662, CAST(21.20948 AS Decimal(10, 5)), CAST(-156.97530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4664, CAST(-5.87556 AS Decimal(10, 5)), CAST(29.25000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4665, CAST(23.18880 AS Decimal(10, 5)), CAST(94.05110 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4666, CAST(-30.78646 AS Decimal(10, 5)), CAST(121.45828 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4667, CAST(11.68636 AS Decimal(10, 5)), CAST(122.38117 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4670, CAST(54.89005 AS Decimal(10, 5)), CAST(20.59263 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4671, CAST(48.31135 AS Decimal(10, 5)), CAST(-114.25596 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4673, CAST(56.68333 AS Decimal(10, 5)), CAST(16.28333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4676, CAST(61.53627 AS Decimal(10, 5)), CAST(-160.34133 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4677, CAST(64.32556 AS Decimal(10, 5)), CAST(-158.74389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4678, CAST(54.54777 AS Decimal(10, 5)), CAST(36.37127 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4680, CAST(36.96327 AS Decimal(10, 5)), CAST(26.94058 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4689, CAST(-2.45987 AS Decimal(10, 5)), CAST(28.90732 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4691, CAST(37.02063 AS Decimal(10, 5)), CAST(41.19139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4696, CAST(50.70222 AS Decimal(10, 5)), CAST(-120.44444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4700, CAST(20.00111 AS Decimal(10, 5)), CAST(-155.66806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4707, CAST(-5.90006 AS Decimal(10, 5)), CAST(22.46917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4709, CAST(31.50737 AS Decimal(10, 5)), CAST(65.85035 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4710, CAST(-19.05778 AS Decimal(10, 5)), CAST(178.15717 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4714, CAST(23.11270 AS Decimal(10, 5)), CAST(70.10030 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4721, CAST(67.01185 AS Decimal(10, 5)), CAST(-50.71366 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4722, CAST(58.70973 AS Decimal(10, 5)), CAST(-65.99267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4723, CAST(61.58853 AS Decimal(10, 5)), CAST(-71.92951 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4724, CAST(60.02305 AS Decimal(10, 5)), CAST(-70.00321 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4728, CAST(12.04759 AS Decimal(10, 5)), CAST(8.52462 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4729, CAST(26.40430 AS Decimal(10, 5)), CAST(80.41012 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4731, CAST(39.12118 AS Decimal(10, 5)), CAST(-94.59153 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4732, CAST(39.29623 AS Decimal(10, 5)), CAST(-94.71775 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4738, CAST(22.57709 AS Decimal(10, 5)), CAST(120.35001 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4742, CAST(20.96256 AS Decimal(10, 5)), CAST(-156.67424 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4748, CAST(24.90655 AS Decimal(10, 5)), CAST(67.16080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4749, CAST(49.67083 AS Decimal(10, 5)), CAST(73.33444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4752, CAST(45.61700 AS Decimal(10, 5)), CAST(84.88300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4760, CAST(58.99076 AS Decimal(10, 5)), CAST(22.83073 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4763, CAST(-5.80075 AS Decimal(10, 5)), CAST(110.47700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4766, CAST(50.20298 AS Decimal(10, 5)), CAST(12.91498 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4771, CAST(48.77935 AS Decimal(10, 5)), CAST(8.08050 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4772, CAST(59.44472 AS Decimal(10, 5)), CAST(13.33750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4774, CAST(57.36738 AS Decimal(10, 5)), CAST(-154.02739 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4777, CAST(35.42141 AS Decimal(10, 5)), CAST(27.14601 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4778, CAST(-20.71220 AS Decimal(10, 5)), CAST(116.77300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4779, CAST(40.56222 AS Decimal(10, 5)), CAST(43.11500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4780, CAST(38.83360 AS Decimal(10, 5)), CAST(65.92150 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4782, CAST(-17.45670 AS Decimal(10, 5)), CAST(140.83000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4783, CAST(56.29746 AS Decimal(10, 5)), CAST(9.12463 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4786, CAST(53.52470 AS Decimal(10, 5)), CAST(-88.64280 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4787, CAST(-10.21667 AS Decimal(10, 5)), CAST(31.13333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4788, CAST(-17.83288 AS Decimal(10, 5)), CAST(25.16240 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4791, CAST(52.28250 AS Decimal(10, 5)), CAST(-81.67780 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4793, CAST(0.18333 AS Decimal(10, 5)), CAST(30.10000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4794, CAST(39.54292 AS Decimal(10, 5)), CAST(76.01996 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4795, CAST(60.86991 AS Decimal(10, 5)), CAST(-162.52446 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4799, CAST(35.42140 AS Decimal(10, 5)), CAST(26.91000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4800, CAST(15.38749 AS Decimal(10, 5)), CAST(36.32884 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4801, CAST(51.40839 AS Decimal(10, 5)), CAST(9.37763 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4802, CAST(41.31420 AS Decimal(10, 5)), CAST(33.79580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4803, CAST(36.14170 AS Decimal(10, 5)), CAST(29.57640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4804, CAST(40.44629 AS Decimal(10, 5)), CAST(21.28219 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4808, CAST(27.69658 AS Decimal(10, 5)), CAST(85.35910 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4813, CAST(50.47425 AS Decimal(10, 5)), CAST(19.08002 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4818, CAST(1.18528 AS Decimal(10, 5)), CAST(127.89600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4819, CAST(21.97583 AS Decimal(10, 5)), CAST(-159.33861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4825, CAST(-15.66461 AS Decimal(10, 5)), CAST(-146.88540 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4826, CAST(54.96392 AS Decimal(10, 5)), CAST(24.08478 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4828, CAST(40.91331 AS Decimal(10, 5)), CAST(24.61922 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4829, CAST(44.27040 AS Decimal(10, 5)), CAST(135.03200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4831, CAST(-2.58280 AS Decimal(10, 5)), CAST(150.81310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4836, CAST(10.04930 AS Decimal(10, 5)), CAST(98.53800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4840, CAST(38.77039 AS Decimal(10, 5)), CAST(35.49543 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4841, CAST(55.60619 AS Decimal(10, 5)), CAST(49.27873 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4842, CAST(40.72696 AS Decimal(10, 5)), CAST(-99.00703 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4846, CAST(52.99158 AS Decimal(10, 5)), CAST(-92.83738 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4847, CAST(38.12007 AS Decimal(10, 5)), CAST(20.50048 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4848, CAST(50.19580 AS Decimal(10, 5)), CAST(-61.26577 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4859, CAST(49.95611 AS Decimal(10, 5)), CAST(-119.37778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4867, CAST(55.27009 AS Decimal(10, 5)), CAST(86.10721 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4868, CAST(65.77917 AS Decimal(10, 5)), CAST(24.58472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4871, CAST(60.57094 AS Decimal(10, 5)), CAST(-151.24304 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4873, CAST(-4.08285 AS Decimal(10, 5)), CAST(122.41400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4875, CAST(21.30348 AS Decimal(10, 5)), CAST(99.63080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4884, CAST(49.78833 AS Decimal(10, 5)), CAST(-94.36306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4893, CAST(-7.96361 AS Decimal(10, 5)), CAST(145.77100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4895, CAST(-35.26278 AS Decimal(10, 5)), CAST(173.91194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4896, CAST(-2.09363 AS Decimal(10, 5)), CAST(101.46938 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4898, CAST(39.60194 AS Decimal(10, 5)), CAST(19.91167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4899, CAST(30.27444 AS Decimal(10, 5)), CAST(56.95111 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4900, CAST(34.34585 AS Decimal(10, 5)), CAST(47.15813 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4903, CAST(52.18090 AS Decimal(10, 5)), CAST(-9.52378 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4904, CAST(4.53930 AS Decimal(10, 5)), CAST(103.42610 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4906, CAST(-1.81664 AS Decimal(10, 5)), CAST(109.96348 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4907, CAST(55.35556 AS Decimal(10, 5)), CAST(-131.71361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4909, CAST(55.34601 AS Decimal(10, 5)), CAST(-131.66381 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4912, CAST(24.55654 AS Decimal(10, 5)), CAST(-81.75926 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4916, CAST(48.52804 AS Decimal(10, 5)), CAST(135.18836 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4917, CAST(24.81720 AS Decimal(10, 5)), CAST(79.91860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4919, CAST(25.98830 AS Decimal(10, 5)), CAST(95.67440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4922, CAST(61.02848 AS Decimal(10, 5)), CAST(69.08607 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4925, CAST(29.25930 AS Decimal(10, 5)), CAST(50.32360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4927, CAST(49.92479 AS Decimal(10, 5)), CAST(36.28999 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4928, CAST(15.58950 AS Decimal(10, 5)), CAST(32.55316 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4929, CAST(26.17099 AS Decimal(10, 5)), CAST(56.24057 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4935, CAST(46.67580 AS Decimal(10, 5)), CAST(32.50640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4939, CAST(16.46667 AS Decimal(10, 5)), CAST(102.78333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4941, CAST(33.43538 AS Decimal(10, 5)), CAST(48.28289 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4943, CAST(47.96168 AS Decimal(10, 5)), CAST(91.62246 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4945, CAST(38.42745 AS Decimal(10, 5)), CAST(44.97357 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4946, CAST(40.21540 AS Decimal(10, 5)), CAST(69.69470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4952, CAST(66.97467 AS Decimal(10, 5)), CAST(-160.43983 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4956, CAST(-6.30361 AS Decimal(10, 5)), CAST(155.72667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4957, CAST(50.34500 AS Decimal(10, 5)), CAST(30.89472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4961, CAST(50.40169 AS Decimal(10, 5)), CAST(30.44970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4963, CAST(-1.96310 AS Decimal(10, 5)), CAST(30.13460 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4964, CAST(-4.88333 AS Decimal(10, 5)), CAST(29.63333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4965, CAST(28.32134 AS Decimal(10, 5)), CAST(129.92810 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4970, CAST(-2.91270 AS Decimal(10, 5)), CAST(38.07046 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4971, CAST(5.64470 AS Decimal(10, 5)), CAST(169.12440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4972, CAST(-3.42941 AS Decimal(10, 5)), CAST(37.07446 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4977, CAST(31.06725 AS Decimal(10, 5)), CAST(-97.82892 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4980, CAST(60.41878 AS Decimal(10, 5)), CAST(-64.81906 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4982, CAST(-8.91047 AS Decimal(10, 5)), CAST(39.50851 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4985, CAST(-28.80283 AS Decimal(10, 5)), CAST(24.76517 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4987, CAST(62.84845 AS Decimal(10, 5)), CAST(-69.87841 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4991, CAST(-2.92290 AS Decimal(10, 5)), CAST(25.91572 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4993, CAST(55.11536 AS Decimal(10, 5)), CAST(-162.27205 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4994, CAST(-39.87796 AS Decimal(10, 5)), CAST(143.88258 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4997, CAST(58.67667 AS Decimal(10, 5)), CAST(-156.64917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (4999, CAST(53.01250 AS Decimal(10, 5)), CAST(-89.85530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5004, CAST(-35.71526 AS Decimal(10, 5)), CAST(137.52570 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5006, CAST(17.93642 AS Decimal(10, 5)), CAST(-76.78193 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5008, CAST(44.22528 AS Decimal(10, 5)), CAST(-76.59694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5012, CAST(24.42789 AS Decimal(10, 5)), CAST(118.35920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5015, CAST(-4.38575 AS Decimal(10, 5)), CAST(15.44457 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5020, CAST(-10.44970 AS Decimal(10, 5)), CAST(161.89799 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5022, CAST(69.72578 AS Decimal(10, 5)), CAST(29.89129 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5025, CAST(40.09330 AS Decimal(10, 5)), CAST(-92.54120 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5027, CAST(58.95778 AS Decimal(10, 5)), CAST(-2.90500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5028, CAST(58.50330 AS Decimal(10, 5)), CAST(49.34830 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5030, CAST(67.46330 AS Decimal(10, 5)), CAST(33.58830 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5031, CAST(67.81667 AS Decimal(10, 5)), CAST(20.33333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5034, CAST(0.48164 AS Decimal(10, 5)), CAST(25.33800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5036, CAST(26.53018 AS Decimal(10, 5)), CAST(53.97283 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5037, CAST(-0.37735 AS Decimal(10, 5)), CAST(42.45920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5040, CAST(-0.08614 AS Decimal(10, 5)), CAST(34.72889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5041, CAST(33.84594 AS Decimal(10, 5)), CAST(131.03469 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5042, CAST(25.94472 AS Decimal(10, 5)), CAST(131.32694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5043, CAST(0.97199 AS Decimal(10, 5)), CAST(34.95856 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5047, CAST(36.27426 AS Decimal(10, 5)), CAST(23.01698 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5049, CAST(58.19110 AS Decimal(10, 5)), CAST(-152.37007 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5051, CAST(67.70102 AS Decimal(10, 5)), CAST(24.84685 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5056, CAST(-6.12571 AS Decimal(10, 5)), CAST(141.28200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5057, CAST(67.73056 AS Decimal(10, 5)), CAST(-164.54683 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5064, CAST(46.64500 AS Decimal(10, 5)), CAST(14.32667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5065, CAST(55.97323 AS Decimal(10, 5)), CAST(21.09386 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5066, CAST(42.16262 AS Decimal(10, 5)), CAST(-121.74279 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5067, CAST(55.57889 AS Decimal(10, 5)), CAST(-133.07667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5075, CAST(53.91028 AS Decimal(10, 5)), CAST(-8.81806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5077, CAST(35.80673 AS Decimal(10, 5)), CAST(-83.99115 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5079, CAST(66.91075 AS Decimal(10, 5)), CAST(-156.88407 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5080, CAST(40.73503 AS Decimal(10, 5)), CAST(30.08334 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5081, CAST(10.15556 AS Decimal(10, 5)), CAST(76.39139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5082, CAST(33.54611 AS Decimal(10, 5)), CAST(133.66944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5083, CAST(57.74972 AS Decimal(10, 5)), CAST(-152.49361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5088, CAST(62.19040 AS Decimal(10, 5)), CAST(74.53380 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5089, CAST(5.99368 AS Decimal(10, 5)), CAST(80.32030 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5091, CAST(9.54675 AS Decimal(10, 5)), CAST(100.06210 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5095, CAST(63.72117 AS Decimal(10, 5)), CAST(23.14313 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5100, CAST(53.32910 AS Decimal(10, 5)), CAST(69.59460 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5102, CAST(12.89900 AS Decimal(10, 5)), CAST(-14.96806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5104, CAST(16.66466 AS Decimal(10, 5)), CAST(74.28935 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5105, CAST(22.65474 AS Decimal(10, 5)), CAST(88.44672 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5107, CAST(-10.76589 AS Decimal(10, 5)), CAST(25.50571 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5111, CAST(36.39461 AS Decimal(10, 5)), CAST(136.40654 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5112, CAST(-6.06793 AS Decimal(10, 5)), CAST(142.85990 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5117, CAST(50.40900 AS Decimal(10, 5)), CAST(136.93401 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5119, CAST(19.73861 AS Decimal(10, 5)), CAST(-156.04556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5122, CAST(-21.05430 AS Decimal(10, 5)), CAST(164.83701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5124, CAST(59.96331 AS Decimal(10, 5)), CAST(-162.87981 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5131, CAST(37.97900 AS Decimal(10, 5)), CAST(32.56186 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5139, CAST(9.38718 AS Decimal(10, 5)), CAST(-5.55666 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5140, CAST(41.69780 AS Decimal(10, 5)), CAST(86.12890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5141, CAST(-17.34583 AS Decimal(10, 5)), CAST(179.42194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5144, CAST(7.36733 AS Decimal(10, 5)), CAST(134.54081 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5146, CAST(36.79333 AS Decimal(10, 5)), CAST(27.09167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5148, CAST(48.66306 AS Decimal(10, 5)), CAST(21.24111 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5150, CAST(5.35698 AS Decimal(10, 5)), CAST(162.95839 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5151, CAST(53.20639 AS Decimal(10, 5)), CAST(63.55083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5156, CAST(6.16685 AS Decimal(10, 5)), CAST(102.29301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5157, CAST(5.93721 AS Decimal(10, 5)), CAST(116.05118 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5161, CAST(-3.29580 AS Decimal(10, 5)), CAST(116.16430 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5163, CAST(61.23651 AS Decimal(10, 5)), CAST(46.69863 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5164, CAST(63.03588 AS Decimal(10, 5)), CAST(-163.52675 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5166, CAST(66.88444 AS Decimal(10, 5)), CAST(-162.59889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5169, CAST(-20.54631 AS Decimal(10, 5)), CAST(164.25563 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5177, CAST(-15.48560 AS Decimal(10, 5)), CAST(141.75101 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5178, CAST(64.93389 AS Decimal(10, 5)), CAST(-161.15806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5179, CAST(64.87750 AS Decimal(10, 5)), CAST(-157.71822 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5180, CAST(40.28611 AS Decimal(10, 5)), CAST(21.84083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5181, CAST(11.13680 AS Decimal(10, 5)), CAST(75.95530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5182, CAST(8.09985 AS Decimal(10, 5)), CAST(98.98527 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5184, CAST(50.07617 AS Decimal(10, 5)), CAST(19.79157 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5186, CAST(63.04860 AS Decimal(10, 5)), CAST(17.76886 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5187, CAST(45.03433 AS Decimal(10, 5)), CAST(39.13977 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5188, CAST(56.17290 AS Decimal(10, 5)), CAST(92.49330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5194, CAST(58.20421 AS Decimal(10, 5)), CAST(8.08537 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5196, CAST(55.92169 AS Decimal(10, 5)), CAST(14.08554 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5203, CAST(2.74558 AS Decimal(10, 5)), CAST(101.70992 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5206, CAST(3.13058 AS Decimal(10, 5)), CAST(101.54933 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5207, CAST(5.38380 AS Decimal(10, 5)), CAST(103.10420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5208, CAST(3.77539 AS Decimal(10, 5)), CAST(103.20906 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5210, CAST(-10.22500 AS Decimal(10, 5)), CAST(142.21800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5211, CAST(1.48470 AS Decimal(10, 5)), CAST(110.34693 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5213, CAST(6.91667 AS Decimal(10, 5)), CAST(116.83333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5214, CAST(24.17873 AS Decimal(10, 5)), CAST(23.31396 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5215, CAST(68.53444 AS Decimal(10, 5)), CAST(-89.80806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5216, CAST(67.81667 AS Decimal(10, 5)), CAST(-115.14389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5218, CAST(-12.40463 AS Decimal(10, 5)), CAST(16.94741 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5222, CAST(31.87670 AS Decimal(10, 5)), CAST(77.15440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5223, CAST(65.57316 AS Decimal(10, 5)), CAST(-37.13233 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5224, CAST(37.98810 AS Decimal(10, 5)), CAST(69.80500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5225, CAST(32.83732 AS Decimal(10, 5)), CAST(130.85505 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5226, CAST(6.71456 AS Decimal(10, 5)), CAST(-1.59082 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5227, CAST(26.36351 AS Decimal(10, 5)), CAST(126.71381 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5232, CAST(25.10429 AS Decimal(10, 5)), CAST(102.94378 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5233, CAST(-15.78042 AS Decimal(10, 5)), CAST(128.71149 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5234, CAST(63.00715 AS Decimal(10, 5)), CAST(27.79776 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5239, CAST(41.71810 AS Decimal(10, 5)), CAST(82.98690 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5241, CAST(58.23000 AS Decimal(10, 5)), CAST(22.50944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5242, CAST(55.47530 AS Decimal(10, 5)), CAST(65.41560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5245, CAST(51.75060 AS Decimal(10, 5)), CAST(36.29560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5252, CAST(43.04097 AS Decimal(10, 5)), CAST(144.19299 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5253, CAST(42.17889 AS Decimal(10, 5)), CAST(42.48194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5254, CAST(58.09228 AS Decimal(10, 5)), CAST(-68.42487 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5255, CAST(55.28338 AS Decimal(10, 5)), CAST(-77.76441 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5256, CAST(65.98758 AS Decimal(10, 5)), CAST(29.23938 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5257, CAST(29.22657 AS Decimal(10, 5)), CAST(47.96893 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5260, CAST(-8.36066 AS Decimal(10, 5)), CAST(160.77453 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5263, CAST(60.79354 AS Decimal(10, 5)), CAST(-161.43916 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5264, CAST(59.87978 AS Decimal(10, 5)), CAST(-163.17053 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5266, CAST(19.42639 AS Decimal(10, 5)), CAST(93.53472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5269, CAST(51.66940 AS Decimal(10, 5)), CAST(94.40060 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5270, CAST(44.70923 AS Decimal(10, 5)), CAST(65.59052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5274, CAST(15.74248 AS Decimal(10, 5)), CAST(-86.85304 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5276, CAST(-0.73333 AS Decimal(10, 5)), CAST(-73.01667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5279, CAST(43.30206 AS Decimal(10, 5)), CAST(-8.37726 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5280, CAST(43.87913 AS Decimal(10, 5)), CAST(-91.25625 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5284, CAST(8.23917 AS Decimal(10, 5)), CAST(-72.27103 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5286, CAST(53.62528 AS Decimal(10, 5)), CAST(-77.70417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5294, CAST(-16.51334 AS Decimal(10, 5)), CAST(-68.19226 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5295, CAST(24.07269 AS Decimal(10, 5)), CAST(-110.36248 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5296, CAST(-1.32861 AS Decimal(10, 5)), CAST(-69.57970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5300, CAST(-29.38164 AS Decimal(10, 5)), CAST(-66.79584 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5303, CAST(46.17917 AS Decimal(10, 5)), CAST(-1.19528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5304, CAST(18.45071 AS Decimal(10, 5)), CAST(-68.91183 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5305, CAST(55.15139 AS Decimal(10, 5)), CAST(-105.26194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5307, CAST(-29.91574 AS Decimal(10, 5)), CAST(-71.20149 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5309, CAST(50.83080 AS Decimal(10, 5)), CAST(-58.97560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5316, CAST(27.15167 AS Decimal(10, 5)), CAST(-13.21917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5317, CAST(-16.46564 AS Decimal(10, 5)), CAST(179.33859 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5322, CAST(5.30000 AS Decimal(10, 5)), CAST(115.25000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5323, CAST(-8.48570 AS Decimal(10, 5)), CAST(119.88930 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5324, CAST(-0.61670 AS Decimal(10, 5)), CAST(127.50000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5326, CAST(58.61799 AS Decimal(10, 5)), CAST(-101.46565 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5333, CAST(-6.56983 AS Decimal(10, 5)), CAST(146.72600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5334, CAST(8.92200 AS Decimal(10, 5)), CAST(166.26662 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5336, CAST(40.41226 AS Decimal(10, 5)), CAST(-86.93668 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5337, CAST(30.20528 AS Decimal(10, 5)), CAST(-91.98761 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5340, CAST(-27.78210 AS Decimal(10, 5)), CAST(-50.28150 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5341, CAST(33.76438 AS Decimal(10, 5)), CAST(2.92834 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5342, CAST(0.09306 AS Decimal(10, 5)), CAST(-76.86750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5344, CAST(6.57737 AS Decimal(10, 5)), CAST(3.32116 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5350, CAST(5.03225 AS Decimal(10, 5)), CAST(118.32400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5351, CAST(31.52156 AS Decimal(10, 5)), CAST(74.40359 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5352, CAST(48.35642 AS Decimal(10, 5)), CAST(7.82561 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5359, CAST(30.12576 AS Decimal(10, 5)), CAST(-93.22293 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5366, CAST(-3.37625 AS Decimal(10, 5)), CAST(35.81852 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5367, CAST(63.88215 AS Decimal(10, 5)), CAST(-152.31093 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5372, CAST(-18.19959 AS Decimal(10, 5)), CAST(-178.81665 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5380, CAST(70.06881 AS Decimal(10, 5)), CAST(24.97349 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5381, CAST(11.97518 AS Decimal(10, 5)), CAST(38.98061 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5383, CAST(2.17300 AS Decimal(10, 5)), CAST(-73.78600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5385, CAST(-16.45761 AS Decimal(10, 5)), CAST(167.82869 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5389, CAST(-16.58420 AS Decimal(10, 5)), CAST(168.15900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5390, CAST(27.36840 AS Decimal(10, 5)), CAST(53.19530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5391, CAST(38.90539 AS Decimal(10, 5)), CAST(16.24227 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5393, CAST(18.26667 AS Decimal(10, 5)), CAST(99.51667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5394, CAST(35.49791 AS Decimal(10, 5)), CAST(12.61808 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5395, CAST(-2.25242 AS Decimal(10, 5)), CAST(40.91310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5396, CAST(20.78914 AS Decimal(10, 5)), CAST(-156.94964 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5401, CAST(40.12148 AS Decimal(10, 5)), CAST(-76.29607 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5406, CAST(50.10280 AS Decimal(10, 5)), CAST(-5.67056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5414, CAST(-5.66162 AS Decimal(10, 5)), CAST(132.73100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5416, CAST(6.32973 AS Decimal(10, 5)), CAST(99.72867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5419, CAST(48.75438 AS Decimal(10, 5)), CAST(-3.47166 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5421, CAST(52.19560 AS Decimal(10, 5)), CAST(-87.93420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5423, CAST(42.77870 AS Decimal(10, 5)), CAST(-84.58736 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5424, CAST(28.95027 AS Decimal(10, 5)), CAST(-13.60556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5425, CAST(36.51524 AS Decimal(10, 5)), CAST(103.62077 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5426, CAST(36.05701 AS Decimal(10, 5)), CAST(103.83987 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5429, CAST(18.17809 AS Decimal(10, 5)), CAST(120.53152 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5432, CAST(61.04455 AS Decimal(10, 5)), CAST(28.14440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5434, CAST(27.67472 AS Decimal(10, 5)), CAST(54.38328 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5435, CAST(41.31248 AS Decimal(10, 5)), CAST(-105.67471 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5436, CAST(-8.27426 AS Decimal(10, 5)), CAST(123.00074 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5438, CAST(27.54474 AS Decimal(10, 5)), CAST(-99.46143 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5440, CAST(34.87512 AS Decimal(10, 5)), CAST(33.62485 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5450, CAST(27.93189 AS Decimal(10, 5)), CAST(-15.38659 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5451, CAST(11.78078 AS Decimal(10, 5)), CAST(-70.15150 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5452, CAST(20.98764 AS Decimal(10, 5)), CAST(-76.93580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5455, CAST(36.08521 AS Decimal(10, 5)), CAST(-115.15068 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5457, CAST(36.23692 AS Decimal(10, 5)), CAST(-115.03309 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5460, CAST(22.97831 AS Decimal(10, 5)), CAST(97.75240 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5463, CAST(35.40109 AS Decimal(10, 5)), CAST(35.94868 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5467, CAST(40.27223 AS Decimal(10, 5)), CAST(-79.40682 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5468, CAST(-41.54528 AS Decimal(10, 5)), CAST(147.21417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5471, CAST(31.46711 AS Decimal(10, 5)), CAST(-89.33701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5478, CAST(26.81150 AS Decimal(10, 5)), CAST(53.35220 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5479, CAST(-28.61564 AS Decimal(10, 5)), CAST(122.42400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5481, CAST(4.84917 AS Decimal(10, 5)), CAST(115.40800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5487, CAST(34.56758 AS Decimal(10, 5)), CAST(-98.41672 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5488, CAST(18.00173 AS Decimal(10, 5)), CAST(-102.22052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5494, CAST(45.08069 AS Decimal(10, 5)), CAST(3.76289 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5495, CAST(50.51738 AS Decimal(10, 5)), CAST(1.62059 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5499, CAST(-22.23333 AS Decimal(10, 5)), CAST(114.08333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5510, CAST(13.15910 AS Decimal(10, 5)), CAST(123.73730 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5512, CAST(34.13590 AS Decimal(10, 5)), CAST(77.54650 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5516, CAST(-27.84330 AS Decimal(10, 5)), CAST(120.70300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5517, CAST(51.42126 AS Decimal(10, 5)), CAST(12.22195 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5523, CAST(68.15425 AS Decimal(10, 5)), CAST(13.61326 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5530, CAST(-12.48230 AS Decimal(10, 5)), CAST(-41.27700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5537, CAST(42.58900 AS Decimal(10, 5)), CAST(-5.65556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5538, CAST(20.99346 AS Decimal(10, 5)), CAST(-101.48085 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5540, CAST(-28.87810 AS Decimal(10, 5)), CAST(121.31500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5546, CAST(37.18494 AS Decimal(10, 5)), CAST(26.79986 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5554, CAST(49.62996 AS Decimal(10, 5)), CAST(-112.79101 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5556, CAST(-4.19355 AS Decimal(10, 5)), CAST(-69.94316 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5561, CAST(59.12701 AS Decimal(10, 5)), CAST(-156.86255 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5564, CAST(37.85818 AS Decimal(10, 5)), CAST(-80.39924 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5565, CAST(46.37433 AS Decimal(10, 5)), CAST(-117.01516 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5568, CAST(-8.38142 AS Decimal(10, 5)), CAST(123.40100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5569, CAST(38.03702 AS Decimal(10, 5)), CAST(-84.60522 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5573, CAST(29.29456 AS Decimal(10, 5)), CAST(90.90032 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5575, CAST(5.22668 AS Decimal(10, 5)), CAST(96.95034 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5578, CAST(34.56743 AS Decimal(10, 5)), CAST(118.87344 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5580, CAST(37.04441 AS Decimal(10, 5)), CAST(-100.95340 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5581, CAST(10.59329 AS Decimal(10, 5)), CAST(-85.54441 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5582, CAST(25.45102 AS Decimal(10, 5)), CAST(107.96247 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5585, CAST(0.45860 AS Decimal(10, 5)), CAST(9.41228 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5587, CAST(-13.27528 AS Decimal(10, 5)), CAST(35.26667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5589, CAST(50.63742 AS Decimal(10, 5)), CAST(5.44322 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5591, CAST(56.51750 AS Decimal(10, 5)), CAST(21.09694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5592, CAST(-20.77480 AS Decimal(10, 5)), CAST(167.23986 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5594, CAST(-3.04361 AS Decimal(10, 5)), CAST(152.62900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5595, CAST(26.67203 AS Decimal(10, 5)), CAST(100.24721 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5596, CAST(9.82238 AS Decimal(10, 5)), CAST(169.30477 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5598, CAST(27.29549 AS Decimal(10, 5)), CAST(94.09765 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5601, CAST(50.57037 AS Decimal(10, 5)), CAST(3.10643 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5604, CAST(-13.78938 AS Decimal(10, 5)), CAST(33.78100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5606, CAST(-12.02189 AS Decimal(10, 5)), CAST(-77.11432 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5608, CAST(4.80830 AS Decimal(10, 5)), CAST(115.01000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5617, CAST(39.91707 AS Decimal(10, 5)), CAST(25.23631 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5618, CAST(45.86278 AS Decimal(10, 5)), CAST(1.17944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5620, CAST(9.95796 AS Decimal(10, 5)), CAST(-83.02201 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5622, CAST(29.30330 AS Decimal(10, 5)), CAST(94.33530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5624, CAST(23.74344 AS Decimal(10, 5)), CAST(100.02481 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5625, CAST(40.85056 AS Decimal(10, 5)), CAST(-96.75946 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5633, CAST(26.34658 AS Decimal(10, 5)), CAST(111.61028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5634, CAST(58.40643 AS Decimal(10, 5)), CAST(15.67732 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5638, CAST(35.04721 AS Decimal(10, 5)), CAST(118.40824 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5639, CAST(48.23322 AS Decimal(10, 5)), CAST(14.18751 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5641, CAST(52.70280 AS Decimal(10, 5)), CAST(39.53780 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5642, CAST(26.31988 AS Decimal(10, 5)), CAST(109.15320 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5644, CAST(2.17066 AS Decimal(10, 5)), CAST(21.49690 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5645, CAST(38.78131 AS Decimal(10, 5)), CAST(-9.13592 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5649, CAST(-28.83030 AS Decimal(10, 5)), CAST(153.25999 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5651, CAST(19.66005 AS Decimal(10, 5)), CAST(-80.08780 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5652, CAST(52.04560 AS Decimal(10, 5)), CAST(-95.46580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5655, CAST(34.73009 AS Decimal(10, 5)), CAST(-92.22431 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5656, CAST(24.20750 AS Decimal(10, 5)), CAST(109.39100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5659, CAST(53.33333 AS Decimal(10, 5)), CAST(-2.85000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5661, CAST(-17.82145 AS Decimal(10, 5)), CAST(25.82022 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5664, CAST(46.22369 AS Decimal(10, 5)), CAST(14.45761 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5665, CAST(53.30917 AS Decimal(10, 5)), CAST(-110.07250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5672, CAST(-12.78690 AS Decimal(10, 5)), CAST(143.30499 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5675, CAST(-3.48333 AS Decimal(10, 5)), CAST(23.46667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5676, CAST(3.12197 AS Decimal(10, 5)), CAST(35.60869 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5677, CAST(51.71667 AS Decimal(10, 5)), CAST(19.40000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5678, CAST(17.44137 AS Decimal(10, 5)), CAST(101.72271 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5682, CAST(42.45731 AS Decimal(10, 5)), CAST(-2.31627 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5683, CAST(19.69150 AS Decimal(10, 5)), CAST(97.21480 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5685, CAST(-4.00000 AS Decimal(10, 5)), CAST(-79.36667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5687, CAST(6.16561 AS Decimal(10, 5)), CAST(1.25451 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5693, CAST(-24.74839 AS Decimal(10, 5)), CAST(31.47403 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5698, CAST(51.15609 AS Decimal(10, 5)), CAST(-0.17818 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5699, CAST(51.47115 AS Decimal(10, 5)), CAST(-0.45649 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5702, CAST(51.50528 AS Decimal(10, 5)), CAST(0.05528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5703, CAST(51.87472 AS Decimal(10, 5)), CAST(-0.36833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5707, CAST(51.88500 AS Decimal(10, 5)), CAST(0.23500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5712, CAST(43.02987 AS Decimal(10, 5)), CAST(-81.14892 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5714, CAST(55.04278 AS Decimal(10, 5)), CAST(-7.16111 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5715, CAST(-23.33363 AS Decimal(10, 5)), CAST(-51.13007 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5718, CAST(3.30000 AS Decimal(10, 5)), CAST(114.78300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5720, CAST(3.18495 AS Decimal(10, 5)), CAST(115.45400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5724, CAST(33.81752 AS Decimal(10, 5)), CAST(-118.15229 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5729, CAST(3.42100 AS Decimal(10, 5)), CAST(115.15400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5733, CAST(3.96700 AS Decimal(10, 5)), CAST(115.05000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5736, CAST(-15.30563 AS Decimal(10, 5)), CAST(167.96748 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5737, CAST(-23.43333 AS Decimal(10, 5)), CAST(144.26667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5739, CAST(25.66719 AS Decimal(10, 5)), CAST(116.74566 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5740, CAST(78.24611 AS Decimal(10, 5)), CAST(15.46556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5741, CAST(-15.86560 AS Decimal(10, 5)), CAST(168.17200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5748, CAST(-31.54036 AS Decimal(10, 5)), CAST(159.07843 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5750, CAST(25.98919 AS Decimal(10, 5)), CAST(-111.34836 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5752, CAST(47.76055 AS Decimal(10, 5)), CAST(-3.44000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5761, CAST(33.94251 AS Decimal(10, 5)), CAST(-118.40897 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5773, CAST(25.68519 AS Decimal(10, 5)), CAST(-109.08081 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5774, CAST(11.94470 AS Decimal(10, 5)), CAST(-66.67285 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5776, CAST(57.70521 AS Decimal(10, 5)), CAST(-3.33917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5779, CAST(-8.50582 AS Decimal(10, 5)), CAST(151.08099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5788, CAST(38.17439 AS Decimal(10, 5)), CAST(-85.73600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5790, CAST(43.17867 AS Decimal(10, 5)), CAST(-0.00644 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5797, CAST(-8.85838 AS Decimal(10, 5)), CAST(13.23118 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5798, CAST(20.96050 AS Decimal(10, 5)), CAST(101.40250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5799, CAST(19.89908 AS Decimal(10, 5)), CAST(102.16255 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5802, CAST(-14.92470 AS Decimal(10, 5)), CAST(13.57500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5803, CAST(33.66370 AS Decimal(10, 5)), CAST(-101.82322 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5807, CAST(-11.59133 AS Decimal(10, 5)), CAST(27.53089 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5813, CAST(26.76059 AS Decimal(10, 5)), CAST(80.88934 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5814, CAST(-26.68694 AS Decimal(10, 5)), CAST(15.24333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5815, CAST(30.85470 AS Decimal(10, 5)), CAST(75.95260 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5821, CAST(-11.76810 AS Decimal(10, 5)), CAST(19.89770 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5825, CAST(46.00427 AS Decimal(10, 5)), CAST(8.91058 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5830, CAST(27.68690 AS Decimal(10, 5)), CAST(86.72970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5832, CAST(65.54376 AS Decimal(10, 5)), CAST(22.12199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5841, CAST(34.73879 AS Decimal(10, 5)), CAST(112.38456 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5844, CAST(-15.33082 AS Decimal(10, 5)), CAST(28.45263 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5851, CAST(62.41812 AS Decimal(10, 5)), CAST(-110.67919 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5853, CAST(-1.03892 AS Decimal(10, 5)), CAST(122.77191 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5854, CAST(49.62658 AS Decimal(10, 5)), CAST(6.21152 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5855, CAST(24.40110 AS Decimal(10, 5)), CAST(98.53170 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5856, CAST(25.67103 AS Decimal(10, 5)), CAST(32.70658 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5858, CAST(28.85220 AS Decimal(10, 5)), CAST(105.39300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5862, CAST(49.81250 AS Decimal(10, 5)), CAST(23.95611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5865, CAST(64.54832 AS Decimal(10, 5)), CAST(18.71622 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5866, CAST(50.95611 AS Decimal(10, 5)), CAST(0.93917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5867, CAST(37.32606 AS Decimal(10, 5)), CAST(-79.20450 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5876, CAST(45.36294 AS Decimal(10, 5)), CAST(5.32937 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5877, CAST(45.72639 AS Decimal(10, 5)), CAST(5.09083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5883, CAST(50.91166 AS Decimal(10, 5)), CAST(5.77014 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5887, CAST(-9.95030 AS Decimal(10, 5)), CAST(142.19530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5890, CAST(0.05066 AS Decimal(10, 5)), CAST(-51.07218 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5893, CAST(22.14956 AS Decimal(10, 5)), CAST(113.59156 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5897, CAST(-9.51081 AS Decimal(10, 5)), CAST(-35.79168 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5902, CAST(-21.17167 AS Decimal(10, 5)), CAST(149.17972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5910, CAST(32.69625 AS Decimal(10, 5)), CAST(-83.64199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5915, CAST(-5.20827 AS Decimal(10, 5)), CAST(145.78960 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5916, CAST(32.69778 AS Decimal(10, 5)), CAST(-16.77444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5918, CAST(24.54541 AS Decimal(10, 5)), CAST(39.70090 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5923, CAST(43.13651 AS Decimal(10, 5)), CAST(-89.34580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5925, CAST(40.49181 AS Decimal(10, 5)), CAST(-3.56948 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5928, CAST(9.83451 AS Decimal(10, 5)), CAST(78.09338 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5929, CAST(19.30229 AS Decimal(10, 5)), CAST(97.97360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5930, CAST(16.69978 AS Decimal(10, 5)), CAST(98.54508 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5932, CAST(-14.99379 AS Decimal(10, 5)), CAST(168.07829 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5934, CAST(-7.91269 AS Decimal(10, 5)), CAST(39.66588 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5937, CAST(59.91099 AS Decimal(10, 5)), CAST(150.72044 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5945, CAST(53.39310 AS Decimal(10, 5)), CAST(58.75570 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5951, CAST(-4.67434 AS Decimal(10, 5)), CAST(55.52184 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5956, CAST(11.85535 AS Decimal(10, 5)), CAST(13.08095 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5963, CAST(15.15615 AS Decimal(10, 5)), CAST(-23.21368 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5966, CAST(8.16332 AS Decimal(10, 5)), CAST(168.17356 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5968, CAST(-15.66714 AS Decimal(10, 5)), CAST(46.35183 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5969, CAST(7.06476 AS Decimal(10, 5)), CAST(171.27202 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5970, CAST(-3.48333 AS Decimal(10, 5)), CAST(12.61667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5971, CAST(13.46737 AS Decimal(10, 5)), CAST(39.53346 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5972, CAST(-16.58390 AS Decimal(10, 5)), CAST(-143.65800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5973, CAST(42.81680 AS Decimal(10, 5)), CAST(47.65230 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5977, CAST(55.07756 AS Decimal(10, 5)), CAST(-59.18708 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5980, CAST(23.56867 AS Decimal(10, 5)), CAST(119.62831 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5985, CAST(3.75527 AS Decimal(10, 5)), CAST(8.70872 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5986, CAST(2.26336 AS Decimal(10, 5)), CAST(102.25155 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5988, CAST(36.67490 AS Decimal(10, 5)), CAST(-4.49911 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5993, CAST(-7.92640 AS Decimal(10, 5)), CAST(112.71460 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5995, CAST(-35.49360 AS Decimal(10, 5)), CAST(-69.57427 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5996, CAST(38.43535 AS Decimal(10, 5)), CAST(38.09101 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (5999, CAST(4.19183 AS Decimal(10, 5)), CAST(73.52913 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6002, CAST(44.56870 AS Decimal(10, 5)), CAST(14.39385 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6004, CAST(-3.23333 AS Decimal(10, 5)), CAST(40.10000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6010, CAST(55.54205 AS Decimal(10, 5)), CAST(13.37200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6015, CAST(35.85750 AS Decimal(10, 5)), CAST(14.47722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6021, CAST(37.62382 AS Decimal(10, 5)), CAST(-118.83957 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6023, CAST(-2.58670 AS Decimal(10, 5)), CAST(119.02930 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6024, CAST(7.27207 AS Decimal(10, 5)), CAST(-7.58736 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6026, CAST(1.54945 AS Decimal(10, 5)), CAST(124.92588 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6027, CAST(12.14149 AS Decimal(10, 5)), CAST(-86.16818 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6036, CAST(18.84647 AS Decimal(10, 5)), CAST(93.68355 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6037, CAST(-3.03861 AS Decimal(10, 5)), CAST(-60.04972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6039, CAST(53.35374 AS Decimal(10, 5)), CAST(-2.27495 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6042, CAST(42.92778 AS Decimal(10, 5)), CAST(-71.43720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6044, CAST(21.69061 AS Decimal(10, 5)), CAST(95.97862 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6055, CAST(12.96127 AS Decimal(10, 5)), CAST(74.89007 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6062, CAST(39.13711 AS Decimal(10, 5)), CAST(-96.67161 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6064, CAST(-14.43418 AS Decimal(10, 5)), CAST(-146.06641 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6069, CAST(14.50499 AS Decimal(10, 5)), CAST(121.00445 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6071, CAST(-12.05610 AS Decimal(10, 5)), CAST(134.23399 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6072, CAST(44.27501 AS Decimal(10, 5)), CAST(-86.25436 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6078, CAST(5.02960 AS Decimal(10, 5)), CAST(-75.46470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6085, CAST(64.99750 AS Decimal(10, 5)), CAST(-150.64389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6088, CAST(49.47250 AS Decimal(10, 5)), CAST(8.51556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6090, CAST(-0.89183 AS Decimal(10, 5)), CAST(134.04918 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6095, CAST(40.82142 AS Decimal(10, 5)), CAST(-82.51664 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6099, CAST(-0.94608 AS Decimal(10, 5)), CAST(-80.67881 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6104, CAST(-2.06194 AS Decimal(10, 5)), CAST(147.42417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6106, CAST(20.28817 AS Decimal(10, 5)), CAST(-77.08930 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6107, CAST(19.14478 AS Decimal(10, 5)), CAST(-104.55863 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6108, CAST(49.56926 AS Decimal(10, 5)), CAST(117.32926 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6115, CAST(-25.91973 AS Decimal(10, 5)), CAST(32.57351 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6117, CAST(-37.93417 AS Decimal(10, 5)), CAST(-57.57333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6118, CAST(-1.40534 AS Decimal(10, 5)), CAST(35.00974 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6119, CAST(-5.36859 AS Decimal(10, 5)), CAST(-49.13802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6120, CAST(10.55821 AS Decimal(10, 5)), CAST(-71.72786 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6122, CAST(13.50253 AS Decimal(10, 5)), CAST(7.12675 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6129, CAST(-9.86167 AS Decimal(10, 5)), CAST(160.82500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6138, CAST(37.22343 AS Decimal(10, 5)), CAST(40.63190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6139, CAST(-21.48168 AS Decimal(10, 5)), CAST(168.03751 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6146, CAST(-30.85741 AS Decimal(10, 5)), CAST(30.34302 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6150, CAST(46.47986 AS Decimal(10, 5)), CAST(15.68613 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6159, CAST(-22.19690 AS Decimal(10, 5)), CAST(-49.92640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6162, CAST(13.35950 AS Decimal(10, 5)), CAST(121.82630 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6163, CAST(-23.47639 AS Decimal(10, 5)), CAST(-52.01641 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6164, CAST(37.74906 AS Decimal(10, 5)), CAST(-89.01414 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6180, CAST(18.13600 AS Decimal(10, 5)), CAST(55.18210 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6182, CAST(-15.43670 AS Decimal(10, 5)), CAST(49.68830 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6184, CAST(10.45140 AS Decimal(10, 5)), CAST(14.25740 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6186, CAST(46.35361 AS Decimal(10, 5)), CAST(-87.39528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6187, CAST(31.60689 AS Decimal(10, 5)), CAST(-8.03630 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6190, CAST(25.55646 AS Decimal(10, 5)), CAST(34.59509 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6194, CAST(43.43556 AS Decimal(10, 5)), CAST(5.21361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6196, CAST(26.51139 AS Decimal(10, 5)), CAST(-77.08361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6197, CAST(61.86457 AS Decimal(10, 5)), CAST(-162.03144 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6203, CAST(41.39016 AS Decimal(10, 5)), CAST(-70.61265 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6206, CAST(4.17865 AS Decimal(10, 5)), CAST(114.33043 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6207, CAST(37.61940 AS Decimal(10, 5)), CAST(61.89670 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6219, CAST(12.36689 AS Decimal(10, 5)), CAST(123.62784 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6221, CAST(-29.46226 AS Decimal(10, 5)), CAST(27.55250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6222, CAST(36.23519 AS Decimal(10, 5)), CAST(59.64097 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6227, CAST(43.15339 AS Decimal(10, 5)), CAST(-93.33566 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6229, CAST(44.93330 AS Decimal(10, 5)), CAST(-74.84298 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6230, CAST(54.02783 AS Decimal(10, 5)), CAST(-132.12624 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6237, CAST(-14.86810 AS Decimal(10, 5)), CAST(-148.71700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6238, CAST(3.34812 AS Decimal(10, 5)), CAST(106.25800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6241, CAST(25.76989 AS Decimal(10, 5)), CAST(-97.52531 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6250, CAST(26.22420 AS Decimal(10, 5)), CAST(120.00300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6251, CAST(36.16676 AS Decimal(10, 5)), CAST(137.92267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6252, CAST(33.82722 AS Decimal(10, 5)), CAST(132.69972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6256, CAST(9.74814 AS Decimal(10, 5)), CAST(-63.15469 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6260, CAST(16.44166 AS Decimal(10, 5)), CAST(97.65766 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6261, CAST(-8.63910 AS Decimal(10, 5)), CAST(122.23860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6262, CAST(-19.97256 AS Decimal(10, 5)), CAST(23.43109 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6263, CAST(-16.42650 AS Decimal(10, 5)), CAST(-152.24400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6264, CAST(-20.43023 AS Decimal(10, 5)), CAST(57.68360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6269, CAST(22.37900 AS Decimal(10, 5)), CAST(-73.01400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6270, CAST(18.25430 AS Decimal(10, 5)), CAST(-67.14966 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6273, CAST(63.61667 AS Decimal(10, 5)), CAST(-135.86667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6276, CAST(36.70702 AS Decimal(10, 5)), CAST(67.20957 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6277, CAST(23.16136 AS Decimal(10, 5)), CAST(-106.26607 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6282, CAST(0.02260 AS Decimal(10, 5)), CAST(18.28874 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6285, CAST(-8.91908 AS Decimal(10, 5)), CAST(33.46298 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6289, CAST(-6.12124 AS Decimal(10, 5)), CAST(23.56901 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6292, CAST(26.17591 AS Decimal(10, 5)), CAST(-98.23862 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6293, CAST(-16.44257 AS Decimal(10, 5)), CAST(136.07602 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6297, CAST(40.20611 AS Decimal(10, 5)), CAST(-100.59210 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6298, CAST(62.95278 AS Decimal(10, 5)), CAST(-155.60556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6307, CAST(6.21996 AS Decimal(10, 5)), CAST(-75.59052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6308, CAST(6.16454 AS Decimal(10, 5)), CAST(-75.42312 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6310, CAST(42.36955 AS Decimal(10, 5)), CAST(-122.87311 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6313, CAST(50.01889 AS Decimal(10, 5)), CAST(-110.72083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6316, CAST(-26.60848 AS Decimal(10, 5)), CAST(118.54548 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6321, CAST(71.02902 AS Decimal(10, 5)), CAST(27.82640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6322, CAST(24.26535 AS Decimal(10, 5)), CAST(116.09991 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6323, CAST(10.28350 AS Decimal(10, 5)), CAST(170.86967 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6327, CAST(60.37139 AS Decimal(10, 5)), CAST(-166.27056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6328, CAST(4.00694 AS Decimal(10, 5)), CAST(126.67300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6329, CAST(28.10278 AS Decimal(10, 5)), CAST(-80.64583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6330, CAST(-38.03944 AS Decimal(10, 5)), CAST(144.46944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6332, CAST(-37.72806 AS Decimal(10, 5)), CAST(144.90194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6334, CAST(-37.67333 AS Decimal(10, 5)), CAST(144.84333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6338, CAST(35.27982 AS Decimal(10, 5)), CAST(-2.95626 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6343, CAST(43.88061 AS Decimal(10, 5)), CAST(144.16405 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6344, CAST(47.98751 AS Decimal(10, 5)), CAST(10.23622 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6346, CAST(35.04218 AS Decimal(10, 5)), CAST(-89.98157 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6350, CAST(44.50211 AS Decimal(10, 5)), CAST(3.53282 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6354, CAST(-6.15030 AS Decimal(10, 5)), CAST(143.65714 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6355, CAST(-32.83172 AS Decimal(10, 5)), CAST(-68.79286 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6357, CAST(-14.65760 AS Decimal(10, 5)), CAST(17.71980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6358, CAST(39.86260 AS Decimal(10, 5)), CAST(4.21865 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6362, CAST(-8.52029 AS Decimal(10, 5)), CAST(140.41845 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6365, CAST(37.28439 AS Decimal(10, 5)), CAST(-120.51464 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6370, CAST(20.93698 AS Decimal(10, 5)), CAST(-89.65767 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6373, CAST(32.33420 AS Decimal(10, 5)), CAST(-88.74573 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6374, CAST(-36.90860 AS Decimal(10, 5)), CAST(149.90100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6381, CAST(31.32536 AS Decimal(10, 5)), CAST(27.22169 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6385, CAST(40.82937 AS Decimal(10, 5)), CAST(35.52199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6393, CAST(55.13083 AS Decimal(10, 5)), CAST(-131.57806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6395, CAST(48.98255 AS Decimal(10, 5)), CAST(6.24132 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6396, CAST(4.04347 AS Decimal(10, 5)), CAST(96.25079 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6398, CAST(32.62962 AS Decimal(10, 5)), CAST(-115.24796 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6400, CAST(19.43630 AS Decimal(10, 5)), CAST(-99.07210 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6403, CAST(19.33707 AS Decimal(10, 5)), CAST(-99.56601 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6405, CAST(-13.25889 AS Decimal(10, 5)), CAST(31.93667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6408, CAST(25.79343 AS Decimal(10, 5)), CAST(-80.29005 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6414, CAST(31.42810 AS Decimal(10, 5)), CAST(104.74100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6419, CAST(-22.80250 AS Decimal(10, 5)), CAST(148.70500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6425, CAST(31.94262 AS Decimal(10, 5)), CAST(-102.20208 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6430, CAST(37.43513 AS Decimal(10, 5)), CAST(25.34810 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6433, CAST(45.46081 AS Decimal(10, 5)), CAST(9.27952 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6434, CAST(45.62754 AS Decimal(10, 5)), CAST(8.71508 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6436, CAST(45.67389 AS Decimal(10, 5)), CAST(9.70417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6437, CAST(44.82448 AS Decimal(10, 5)), CAST(10.29637 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6442, CAST(-34.22917 AS Decimal(10, 5)), CAST(142.08556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6446, CAST(6.04040 AS Decimal(10, 5)), CAST(171.98429 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6447, CAST(6.08293 AS Decimal(10, 5)), CAST(171.72899 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6448, CAST(-12.09440 AS Decimal(10, 5)), CAST(134.89400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6453, CAST(36.69680 AS Decimal(10, 5)), CAST(24.47621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6457, CAST(42.94668 AS Decimal(10, 5)), CAST(-87.89675 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6463, CAST(25.84653 AS Decimal(10, 5)), CAST(131.26349 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6464, CAST(18.10342 AS Decimal(10, 5)), CAST(-94.58068 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6473, CAST(44.22507 AS Decimal(10, 5)), CAST(43.08189 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6490, CAST(48.25945 AS Decimal(10, 5)), CAST(-101.28100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6492, CAST(53.86447 AS Decimal(10, 5)), CAST(27.53968 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6493, CAST(53.88247 AS Decimal(10, 5)), CAST(28.03073 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6495, CAST(65.14712 AS Decimal(10, 5)), CAST(-149.36631 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6505, CAST(4.32201 AS Decimal(10, 5)), CAST(113.98681 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6507, CAST(62.53469 AS Decimal(10, 5)), CAST(114.03893 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6510, CAST(40.70322 AS Decimal(10, 5)), CAST(141.36836 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6511, CAST(-10.68920 AS Decimal(10, 5)), CAST(152.83800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6514, CAST(46.91603 AS Decimal(10, 5)), CAST(-114.09150 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6515, CAST(32.32500 AS Decimal(10, 5)), CAST(15.06100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6526, CAST(1.25366 AS Decimal(10, 5)), CAST(-70.23388 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6529, CAST(24.78283 AS Decimal(10, 5)), CAST(125.29511 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6531, CAST(31.87722 AS Decimal(10, 5)), CAST(131.44861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6536, CAST(-25.79844 AS Decimal(10, 5)), CAST(25.54803 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6537, CAST(66.36458 AS Decimal(10, 5)), CAST(14.30012 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6538, CAST(20.65166 AS Decimal(10, 5)), CAST(-74.92689 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6539, CAST(38.75914 AS Decimal(10, 5)), CAST(-109.74707 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6549, CAST(30.68825 AS Decimal(10, 5)), CAST(-88.24195 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6559, CAST(2.01575 AS Decimal(10, 5)), CAST(45.30410 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6565, CAST(27.33520 AS Decimal(10, 5)), CAST(68.14310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6571, CAST(62.74472 AS Decimal(10, 5)), CAST(7.26250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6572, CAST(41.44865 AS Decimal(10, 5)), CAST(-90.50652 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6574, CAST(-4.03483 AS Decimal(10, 5)), CAST(39.59425 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6579, CAST(35.75806 AS Decimal(10, 5)), CAST(10.75472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6580, CAST(44.30391 AS Decimal(10, 5)), CAST(143.40403 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6581, CAST(26.95478 AS Decimal(10, 5)), CAST(-101.46771 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6582, CAST(46.11311 AS Decimal(10, 5)), CAST(-64.68557 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6585, CAST(20.51680 AS Decimal(10, 5)), CAST(99.25680 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6590, CAST(-25.88937 AS Decimal(10, 5)), CAST(113.57554 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6593, CAST(-7.41694 AS Decimal(10, 5)), CAST(155.56500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6594, CAST(32.51070 AS Decimal(10, 5)), CAST(-92.03763 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6597, CAST(6.23379 AS Decimal(10, 5)), CAST(-10.36231 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6598, CAST(6.28972 AS Decimal(10, 5)), CAST(-10.75806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6601, CAST(48.60485 AS Decimal(10, 5)), CAST(-68.21209 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6603, CAST(46.40940 AS Decimal(10, 5)), CAST(-74.78000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6613, CAST(43.72568 AS Decimal(10, 5)), CAST(7.41948 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6619, CAST(-19.82700 AS Decimal(10, 5)), CAST(-63.96100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6620, CAST(18.50372 AS Decimal(10, 5)), CAST(-77.91336 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6625, CAST(8.82374 AS Decimal(10, 5)), CAST(-75.82583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6628, CAST(25.77849 AS Decimal(10, 5)), CAST(-100.10688 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6630, CAST(-16.70692 AS Decimal(10, 5)), CAST(-43.81890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6632, CAST(-34.83842 AS Decimal(10, 5)), CAST(-56.03081 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6633, CAST(32.30014 AS Decimal(10, 5)), CAST(-86.39330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6644, CAST(43.57762 AS Decimal(10, 5)), CAST(3.95860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6649, CAST(45.68194 AS Decimal(10, 5)), CAST(-74.00528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6650, CAST(45.46106 AS Decimal(10, 5)), CAST(-73.75019 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6651, CAST(45.51621 AS Decimal(10, 5)), CAST(-73.42069 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6654, CAST(38.50860 AS Decimal(10, 5)), CAST(-107.89423 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6655, CAST(16.79123 AS Decimal(10, 5)), CAST(-62.19317 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6659, CAST(-28.09940 AS Decimal(10, 5)), CAST(140.19701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6662, CAST(-17.48879 AS Decimal(10, 5)), CAST(-149.76202 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6665, CAST(51.29111 AS Decimal(10, 5)), CAST(-80.60778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6667, CAST(60.95791 AS Decimal(10, 5)), CAST(14.51138 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6669, CAST(-22.05780 AS Decimal(10, 5)), CAST(148.07700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6671, CAST(-29.49890 AS Decimal(10, 5)), CAST(149.84500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6673, CAST(19.84994 AS Decimal(10, 5)), CAST(-101.02550 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6677, CAST(39.64286 AS Decimal(10, 5)), CAST(-79.91617 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6683, CAST(-16.66159 AS Decimal(10, 5)), CAST(139.16927 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6684, CAST(-6.36333 AS Decimal(10, 5)), CAST(143.23801 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6689, CAST(49.66330 AS Decimal(10, 5)), CAST(100.09900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6690, CAST(-20.28490 AS Decimal(10, 5)), CAST(44.31765 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6693, CAST(-11.53591 AS Decimal(10, 5)), CAST(43.27420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6694, CAST(2.04599 AS Decimal(10, 5)), CAST(128.32500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6697, CAST(40.79927 AS Decimal(10, 5)), CAST(-74.41432 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6699, CAST(-35.89780 AS Decimal(10, 5)), CAST(150.14400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6703, CAST(55.41450 AS Decimal(10, 5)), CAST(37.89990 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6707, CAST(55.97194 AS Decimal(10, 5)), CAST(37.41389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6708, CAST(55.59153 AS Decimal(10, 5)), CAST(37.26149 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6715, CAST(65.78400 AS Decimal(10, 5)), CAST(13.21491 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6721, CAST(-5.20192 AS Decimal(10, 5)), CAST(-37.36435 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6724, CAST(43.28290 AS Decimal(10, 5)), CAST(17.84588 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6729, CAST(-13.66659 AS Decimal(10, 5)), CAST(167.71149 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6741, CAST(8.61667 AS Decimal(10, 5)), CAST(16.06667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6750, CAST(-37.74560 AS Decimal(10, 5)), CAST(140.78500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6752, CAST(-5.82679 AS Decimal(10, 5)), CAST(144.29601 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6756, CAST(-20.66389 AS Decimal(10, 5)), CAST(139.48861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6757, CAST(-27.28583 AS Decimal(10, 5)), CAST(120.55105 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6758, CAST(-28.11610 AS Decimal(10, 5)), CAST(117.84200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6772, CAST(62.09468 AS Decimal(10, 5)), CAST(-163.68527 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6780, CAST(-17.63392 AS Decimal(10, 5)), CAST(24.17739 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6793, CAST(-10.33906 AS Decimal(10, 5)), CAST(40.18178 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6796, CAST(44.52410 AS Decimal(10, 5)), CAST(129.56900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6797, CAST(-32.56250 AS Decimal(10, 5)), CAST(149.61099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6800, CAST(52.13464 AS Decimal(10, 5)), CAST(7.68483 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6804, CAST(2.90712 AS Decimal(10, 5)), CAST(112.07831 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6807, CAST(19.38719 AS Decimal(10, 5)), CAST(56.40046 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6808, CAST(-2.54186 AS Decimal(10, 5)), CAST(101.08800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6810, CAST(8.94532 AS Decimal(10, 5)), CAST(-77.73308 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6813, CAST(47.58958 AS Decimal(10, 5)), CAST(7.52991 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6821, CAST(30.20322 AS Decimal(10, 5)), CAST(71.41911 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6822, CAST(4.04655 AS Decimal(10, 5)), CAST(114.80567 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6823, CAST(19.08869 AS Decimal(10, 5)), CAST(72.86792 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6827, CAST(-8.32797 AS Decimal(10, 5)), CAST(157.26309 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6832, CAST(48.35378 AS Decimal(10, 5)), CAST(11.78609 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6836, CAST(37.77497 AS Decimal(10, 5)), CAST(-0.81239 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6838, CAST(68.78167 AS Decimal(10, 5)), CAST(32.75082 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6842, CAST(-9.91667 AS Decimal(10, 5)), CAST(144.05499 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6844, CAST(38.74777 AS Decimal(10, 5)), CAST(41.66124 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6845, CAST(23.60090 AS Decimal(10, 5)), CAST(58.28605 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6849, CAST(43.16946 AS Decimal(10, 5)), CAST(-86.23811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6854, CAST(53.44140 AS Decimal(10, 5)), CAST(-91.76280 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6855, CAST(-1.50000 AS Decimal(10, 5)), CAST(33.80000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6866, CAST(-2.44449 AS Decimal(10, 5)), CAST(32.93267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6868, CAST(12.43980 AS Decimal(10, 5)), CAST(98.62150 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6869, CAST(25.38170 AS Decimal(10, 5)), CAST(97.35417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6875, CAST(12.30720 AS Decimal(10, 5)), CAST(76.64970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6878, CAST(39.05428 AS Decimal(10, 5)), CAST(26.60279 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6883, CAST(-3.36818 AS Decimal(10, 5)), CAST(135.49641 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6885, CAST(-14.47952 AS Decimal(10, 5)), CAST(40.71284 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6890, CAST(-17.75539 AS Decimal(10, 5)), CAST(177.44338 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6891, CAST(34.98889 AS Decimal(10, 5)), CAST(-3.02833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6893, CAST(65.48090 AS Decimal(10, 5)), CAST(72.69890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6895, CAST(13.58489 AS Decimal(10, 5)), CAST(123.27024 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6897, CAST(32.91694 AS Decimal(10, 5)), CAST(129.91361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6898, CAST(35.25722 AS Decimal(10, 5)), CAST(136.92634 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6899, CAST(35.25722 AS Decimal(10, 5)), CAST(136.92634 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6901, CAST(21.09219 AS Decimal(10, 5)), CAST(79.04718 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6902, CAST(3.68321 AS Decimal(10, 5)), CAST(125.52802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6903, CAST(56.54981 AS Decimal(10, 5)), CAST(-61.68308 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6904, CAST(-1.33030 AS Decimal(10, 5)), CAST(36.92505 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6906, CAST(-1.32172 AS Decimal(10, 5)), CAST(36.81483 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6907, CAST(43.57750 AS Decimal(10, 5)), CAST(144.96000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6908, CAST(39.18880 AS Decimal(10, 5)), CAST(45.45840 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6909, CAST(17.38821 AS Decimal(10, 5)), CAST(104.64739 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6910, CAST(14.94950 AS Decimal(10, 5)), CAST(102.31300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6911, CAST(8.53892 AS Decimal(10, 5)), CAST(99.94271 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6916, CAST(43.51278 AS Decimal(10, 5)), CAST(43.63667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6917, CAST(40.98490 AS Decimal(10, 5)), CAST(71.55683 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6922, CAST(5.63264 AS Decimal(10, 5)), CAST(168.12249 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6923, CAST(-15.26120 AS Decimal(10, 5)), CAST(12.14680 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6924, CAST(-3.23607 AS Decimal(10, 5)), CAST(127.09971 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6925, CAST(-15.10639 AS Decimal(10, 5)), CAST(39.29250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6926, CAST(-3.82081 AS Decimal(10, 5)), CAST(126.71800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6928, CAST(64.46667 AS Decimal(10, 5)), CAST(11.58333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6935, CAST(18.80000 AS Decimal(10, 5)), CAST(100.78333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6936, CAST(49.05233 AS Decimal(10, 5)), CAST(-123.87017 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6939, CAST(28.86411 AS Decimal(10, 5)), CAST(115.90456 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6940, CAST(30.75577 AS Decimal(10, 5)), CAST(106.06073 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6942, CAST(19.18173 AS Decimal(10, 5)), CAST(77.32251 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6944, CAST(26.15978 AS Decimal(10, 5)), CAST(119.95847 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6949, CAST(32.00102 AS Decimal(10, 5)), CAST(118.81607 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6950, CAST(22.60827 AS Decimal(10, 5)), CAST(108.17244 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6952, CAST(47.15319 AS Decimal(10, 5)), CAST(-1.61072 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6954, CAST(32.07080 AS Decimal(10, 5)), CAST(120.97600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6955, CAST(41.25693 AS Decimal(10, 5)), CAST(-70.05934 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6957, CAST(59.35451 AS Decimal(10, 5)), CAST(-151.92261 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6958, CAST(32.98080 AS Decimal(10, 5)), CAST(112.61500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6959, CAST(-0.06667 AS Decimal(10, 5)), CAST(37.03333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6961, CAST(38.21297 AS Decimal(10, 5)), CAST(-122.28164 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6966, CAST(-39.46635 AS Decimal(10, 5)), CAST(176.86778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6968, CAST(40.88603 AS Decimal(10, 5)), CAST(14.29078 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6978, CAST(6.52095 AS Decimal(10, 5)), CAST(101.74470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6982, CAST(-30.31920 AS Decimal(10, 5)), CAST(149.82700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6983, CAST(-34.70220 AS Decimal(10, 5)), CAST(146.51199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6988, CAST(61.16052 AS Decimal(10, 5)), CAST(-45.42598 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6990, CAST(67.64000 AS Decimal(10, 5)), CAST(53.12190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6992, CAST(36.11950 AS Decimal(10, 5)), CAST(-86.68416 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6993, CAST(20.11944 AS Decimal(10, 5)), CAST(73.91368 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (6995, CAST(25.04571 AS Decimal(10, 5)), CAST(-77.46621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7001, CAST(-5.76865 AS Decimal(10, 5)), CAST(-35.37319 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7002, CAST(50.19000 AS Decimal(10, 5)), CAST(-61.78917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7006, CAST(55.91396 AS Decimal(10, 5)), CAST(-61.18552 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7007, CAST(3.90871 AS Decimal(10, 5)), CAST(108.38790 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7009, CAST(-0.54518 AS Decimal(10, 5)), CAST(166.91618 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7011, CAST(-26.88000 AS Decimal(10, 5)), CAST(-48.65140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7012, CAST(40.11720 AS Decimal(10, 5)), CAST(65.17080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7015, CAST(37.07972 AS Decimal(10, 5)), CAST(25.36694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7019, CAST(12.13369 AS Decimal(10, 5)), CAST(15.03402 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7021, CAST(-12.99814 AS Decimal(10, 5)), CAST(28.66494 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7026, CAST(70.74311 AS Decimal(10, 5)), CAST(-22.65038 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7034, CAST(2.95015 AS Decimal(10, 5)), CAST(-75.29400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7039, CAST(-41.29661 AS Decimal(10, 5)), CAST(173.22414 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7040, CAST(56.00796 AS Decimal(10, 5)), CAST(-161.16755 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7044, CAST(51.69105 AS Decimal(10, 5)), CAST(-76.13621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7047, CAST(28.10360 AS Decimal(10, 5)), CAST(81.66700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7049, CAST(56.91392 AS Decimal(10, 5)), CAST(124.91382 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7057, CAST(-38.94900 AS Decimal(10, 5)), CAST(-68.15571 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7065, CAST(17.20568 AS Decimal(10, 5)), CAST(-62.58987 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7066, CAST(38.77187 AS Decimal(10, 5)), CAST(34.53455 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7068, CAST(35.07719 AS Decimal(10, 5)), CAST(-77.04314 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7073, CAST(41.26707 AS Decimal(10, 5)), CAST(-72.88458 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7078, CAST(59.72690 AS Decimal(10, 5)), CAST(-157.26688 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7081, CAST(29.98724 AS Decimal(10, 5)), CAST(-90.25651 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7085, CAST(-39.00861 AS Decimal(10, 5)), CAST(174.17917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7088, CAST(59.45002 AS Decimal(10, 5)), CAST(-157.37151 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7097, CAST(40.63983 AS Decimal(10, 5)), CAST(-73.77874 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7098, CAST(40.77607 AS Decimal(10, 5)), CAST(-73.87269 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7111, CAST(55.03750 AS Decimal(10, 5)), CAST(-1.69167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7114, CAST(-32.79564 AS Decimal(10, 5)), CAST(151.83449 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7117, CAST(-23.41780 AS Decimal(10, 5)), CAST(119.80300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7124, CAST(50.44056 AS Decimal(10, 5)), CAST(-4.99541 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7126, CAST(60.93675 AS Decimal(10, 5)), CAST(-164.63801 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7130, CAST(-24.38807 AS Decimal(10, 5)), CAST(31.32528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7131, CAST(7.35906 AS Decimal(10, 5)), CAST(13.56009 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7137, CAST(11.99806 AS Decimal(10, 5)), CAST(109.21944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7139, CAST(43.10144 AS Decimal(10, 5)), CAST(-78.94117 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7141, CAST(13.48155 AS Decimal(10, 5)), CAST(2.18361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7144, CAST(-16.11906 AS Decimal(10, 5)), CAST(-146.36802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7147, CAST(43.66272 AS Decimal(10, 5)), CAST(7.20787 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7154, CAST(60.47275 AS Decimal(10, 5)), CAST(-164.69940 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7155, CAST(37.95589 AS Decimal(10, 5)), CAST(139.12072 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7158, CAST(63.01856 AS Decimal(10, 5)), CAST(-154.35834 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7159, CAST(52.94158 AS Decimal(10, 5)), CAST(-168.84956 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7163, CAST(43.75744 AS Decimal(10, 5)), CAST(4.41635 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7166, CAST(29.82670 AS Decimal(10, 5)), CAST(121.46200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7176, CAST(43.33729 AS Decimal(10, 5)), CAST(21.85372 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7181, CAST(-15.57080 AS Decimal(10, 5)), CAST(-175.63300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7182, CAST(-15.97688 AS Decimal(10, 5)), CAST(-173.79178 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7183, CAST(-19.08003 AS Decimal(10, 5)), CAST(-169.92564 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7184, CAST(55.56470 AS Decimal(10, 5)), CAST(52.09250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7185, CAST(60.94927 AS Decimal(10, 5)), CAST(76.48362 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7186, CAST(56.23012 AS Decimal(10, 5)), CAST(43.78404 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7193, CAST(67.56529 AS Decimal(10, 5)), CAST(-162.97389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7197, CAST(63.18330 AS Decimal(10, 5)), CAST(75.27000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7201, CAST(64.51079 AS Decimal(10, 5)), CAST(-165.44457 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7202, CAST(59.97904 AS Decimal(10, 5)), CAST(-154.83969 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7206, CAST(66.81794 AS Decimal(10, 5)), CAST(-161.03018 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7219, CAST(36.89543 AS Decimal(10, 5)), CAST(-76.20049 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7222, CAST(-29.04162 AS Decimal(10, 5)), CAST(167.93874 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7223, CAST(69.31110 AS Decimal(10, 5)), CAST(87.33220 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7225, CAST(65.28107 AS Decimal(10, 5)), CAST(-126.79793 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7227, CAST(-17.68763 AS Decimal(10, 5)), CAST(141.07208 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7229, CAST(58.58597 AS Decimal(10, 5)), CAST(16.24054 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7232, CAST(-16.07970 AS Decimal(10, 5)), CAST(167.40100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7234, CAST(46.36310 AS Decimal(10, 5)), CAST(-79.42403 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7235, CAST(43.41706 AS Decimal(10, 5)), CAST(-124.24373 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7237, CAST(25.47551 AS Decimal(10, 5)), CAST(-76.68161 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7238, CAST(41.13182 AS Decimal(10, 5)), CAST(-100.69665 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7239, CAST(59.36750 AS Decimal(10, 5)), CAST(-2.43444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7240, CAST(52.49000 AS Decimal(10, 5)), CAST(-92.97110 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7246, CAST(62.96111 AS Decimal(10, 5)), CAST(-141.92889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7248, CAST(53.95823 AS Decimal(10, 5)), CAST(-97.84359 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7249, CAST(52.67583 AS Decimal(10, 5)), CAST(1.28278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7252, CAST(9.97649 AS Decimal(10, 5)), CAST(-85.65300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7253, CAST(-13.31207 AS Decimal(10, 5)), CAST(48.31482 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7254, CAST(59.56568 AS Decimal(10, 5)), CAST(9.21222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7258, CAST(52.83111 AS Decimal(10, 5)), CAST(-1.32806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7263, CAST(20.92857 AS Decimal(10, 5)), CAST(-17.03151 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7264, CAST(18.09786 AS Decimal(10, 5)), CAST(-15.94796 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7265, CAST(-22.25830 AS Decimal(10, 5)), CAST(166.47301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7267, CAST(-22.01455 AS Decimal(10, 5)), CAST(166.21297 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7278, CAST(53.80775 AS Decimal(10, 5)), CAST(86.87405 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7280, CAST(55.01260 AS Decimal(10, 5)), CAST(82.65070 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7281, CAST(66.07257 AS Decimal(10, 5)), CAST(76.52181 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7282, CAST(36.66249 AS Decimal(10, 5)), CAST(51.47454 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7289, CAST(21.83668 AS Decimal(10, 5)), CAST(-82.78074 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7291, CAST(27.44392 AS Decimal(10, 5)), CAST(-99.57046 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7293, CAST(70.21028 AS Decimal(10, 5)), CAST(-151.00472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7295, CAST(-8.79560 AS Decimal(10, 5)), CAST(-140.22900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7296, CAST(-21.24121 AS Decimal(10, 5)), CAST(-175.14964 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7297, CAST(42.48000 AS Decimal(10, 5)), CAST(59.63000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7299, CAST(64.72491 AS Decimal(10, 5)), CAST(-158.08260 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7304, CAST(60.90563 AS Decimal(10, 5)), CAST(-162.44027 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7307, CAST(4.13653 AS Decimal(10, 5)), CAST(117.66700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7309, CAST(5.70970 AS Decimal(10, 5)), CAST(-77.26250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7315, CAST(64.19092 AS Decimal(10, 5)), CAST(-51.67806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7318, CAST(62.11000 AS Decimal(10, 5)), CAST(65.61500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7319, CAST(12.05401 AS Decimal(10, 5)), CAST(24.95600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7320, CAST(21.17880 AS Decimal(10, 5)), CAST(94.93020 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7339, CAST(37.73071 AS Decimal(10, 5)), CAST(-122.21955 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7345, CAST(17.00135 AS Decimal(10, 5)), CAST(-96.72496 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7346, CAST(56.46350 AS Decimal(10, 5)), CAST(-5.39967 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7351, CAST(5.36667 AS Decimal(10, 5)), CAST(48.51667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7356, CAST(42.73333 AS Decimal(10, 5)), CAST(143.21667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7360, CAST(29.16846 AS Decimal(10, 5)), CAST(-82.22297 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7370, CAST(40.19187 AS Decimal(10, 5)), CAST(140.37143 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7372, CAST(55.47666 AS Decimal(10, 5)), CAST(10.33093 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7375, CAST(46.42677 AS Decimal(10, 5)), CAST(30.67646 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7376, CAST(9.53900 AS Decimal(10, 5)), CAST(-7.56100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7382, CAST(-14.18480 AS Decimal(10, 5)), CAST(-169.67069 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7386, CAST(41.19633 AS Decimal(10, 5)), CAST(-112.01161 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7387, CAST(44.67843 AS Decimal(10, 5)), CAST(-75.47597 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7392, CAST(41.17996 AS Decimal(10, 5)), CAST(20.74233 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7395, CAST(33.47944 AS Decimal(10, 5)), CAST(131.73722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7399, CAST(34.75694 AS Decimal(10, 5)), CAST(133.85528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7402, CAST(36.17994 AS Decimal(10, 5)), CAST(133.32862 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7403, CAST(26.35000 AS Decimal(10, 5)), CAST(127.76667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7405, CAST(26.19581 AS Decimal(10, 5)), CAST(127.64587 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7407, CAST(27.43269 AS Decimal(10, 5)), CAST(128.70518 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7412, CAST(35.39312 AS Decimal(10, 5)), CAST(-97.60087 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7417, CAST(-4.90668 AS Decimal(10, 5)), CAST(140.62646 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7419, CAST(42.07167 AS Decimal(10, 5)), CAST(139.43291 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7424, CAST(40.89866 AS Decimal(10, 5)), CAST(9.51763 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7425, CAST(67.57056 AS Decimal(10, 5)), CAST(-139.83917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7431, CAST(57.15996 AS Decimal(10, 5)), CAST(-154.23032 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7443, CAST(-30.48500 AS Decimal(10, 5)), CAST(136.87700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7444, CAST(41.30110 AS Decimal(10, 5)), CAST(-95.89848 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7455, CAST(54.96704 AS Decimal(10, 5)), CAST(73.31051 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7457, CAST(-17.88049 AS Decimal(10, 5)), CAST(15.94747 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7458, CAST(42.46945 AS Decimal(10, 5)), CAST(-98.68759 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7462, CAST(-17.04350 AS Decimal(10, 5)), CAST(15.68380 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7468, CAST(-21.66844 AS Decimal(10, 5)), CAST(115.11269 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7469, CAST(34.05584 AS Decimal(10, 5)), CAST(-117.60200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7482, CAST(47.02528 AS Decimal(10, 5)), CAST(21.90250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7484, CAST(35.62386 AS Decimal(10, 5)), CAST(-0.62118 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7491, CAST(-33.38170 AS Decimal(10, 5)), CAST(149.13300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7492, CAST(18.07447 AS Decimal(10, 5)), CAST(-88.54837 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7494, CAST(-28.58333 AS Decimal(10, 5)), CAST(16.43333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7496, CAST(-37.79000 AS Decimal(10, 5)), CAST(148.61000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7498, CAST(22.02884 AS Decimal(10, 5)), CAST(121.53364 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7501, CAST(59.22373 AS Decimal(10, 5)), CAST(15.03796 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7504, CAST(51.79579 AS Decimal(10, 5)), CAST(55.45674 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7510, CAST(63.69891 AS Decimal(10, 5)), CAST(9.60400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7512, CAST(28.43103 AS Decimal(10, 5)), CAST(-81.30768 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7514, CAST(28.77605 AS Decimal(10, 5)), CAST(-81.24506 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7518, CAST(11.05800 AS Decimal(10, 5)), CAST(124.56500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7519, CAST(63.40834 AS Decimal(10, 5)), CAST(18.99004 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7524, CAST(51.07250 AS Decimal(10, 5)), CAST(58.59560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7525, CAST(62.17979 AS Decimal(10, 5)), CAST(6.07325 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7526, CAST(-17.96259 AS Decimal(10, 5)), CAST(-67.07624 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7528, CAST(34.78553 AS Decimal(10, 5)), CAST(135.43822 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7529, CAST(34.42731 AS Decimal(10, 5)), CAST(135.24407 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7530, CAST(34.63667 AS Decimal(10, 5)), CAST(135.22788 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7537, CAST(40.60900 AS Decimal(10, 5)), CAST(72.79330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7544, CAST(45.46267 AS Decimal(10, 5)), CAST(18.81016 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7547, CAST(60.19392 AS Decimal(10, 5)), CAST(11.10036 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7550, CAST(59.18670 AS Decimal(10, 5)), CAST(10.25863 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7553, CAST(-40.61120 AS Decimal(10, 5)), CAST(-73.06100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7556, CAST(63.19332 AS Decimal(10, 5)), CAST(14.50401 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7557, CAST(49.69629 AS Decimal(10, 5)), CAST(18.11105 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7567, CAST(45.52169 AS Decimal(10, 5)), CAST(-75.56359 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7568, CAST(45.32250 AS Decimal(10, 5)), CAST(-75.66917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7577, CAST(12.35319 AS Decimal(10, 5)), CAST(-1.51242 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7581, CAST(9.60000 AS Decimal(10, 5)), CAST(-4.05000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7582, CAST(31.91722 AS Decimal(10, 5)), CAST(5.41278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7583, CAST(30.93905 AS Decimal(10, 5)), CAST(-6.90943 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7584, CAST(20.68270 AS Decimal(10, 5)), CAST(101.99400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7589, CAST(34.78715 AS Decimal(10, 5)), CAST(-1.92399 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7590, CAST(64.93006 AS Decimal(10, 5)), CAST(25.35456 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7596, CAST(-20.64056 AS Decimal(10, 5)), CAST(166.57278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7600, CAST(29.94025 AS Decimal(10, 5)), CAST(34.93585 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7606, CAST(37.74224 AS Decimal(10, 5)), CAST(-87.16570 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7607, CAST(5.42706 AS Decimal(10, 5)), CAST(7.20603 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7609, CAST(51.83652 AS Decimal(10, 5)), CAST(-1.32016 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7612, CAST(54.93330 AS Decimal(10, 5)), CAST(-95.27890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7615, CAST(8.17851 AS Decimal(10, 5)), CAST(123.84200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7621, CAST(-16.43195 AS Decimal(10, 5)), CAST(168.23555 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7622, CAST(62.01295 AS Decimal(10, 5)), CAST(-49.66867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7627, CAST(-0.78730 AS Decimal(10, 5)), CAST(100.28239 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7628, CAST(51.60810 AS Decimal(10, 5)), CAST(8.61139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7630, CAST(37.06083 AS Decimal(10, 5)), CAST(-88.77375 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7632, CAST(7.83073 AS Decimal(10, 5)), CAST(123.46118 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7633, CAST(36.92582 AS Decimal(10, 5)), CAST(-111.44905 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7634, CAST(-14.33100 AS Decimal(10, 5)), CAST(-170.71050 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7642, CAST(67.24560 AS Decimal(10, 5)), CAST(23.06890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7646, CAST(15.13210 AS Decimal(10, 5)), CAST(105.78100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7647, CAST(51.21170 AS Decimal(10, 5)), CAST(-58.65830 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7648, CAST(2.20256 AS Decimal(10, 5)), CAST(31.55445 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7652, CAST(-2.22513 AS Decimal(10, 5)), CAST(113.94300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7655, CAST(-2.89825 AS Decimal(10, 5)), CAST(104.69990 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7656, CAST(17.53340 AS Decimal(10, 5)), CAST(-91.98450 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7657, CAST(38.17596 AS Decimal(10, 5)), CAST(13.09102 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7660, CAST(-18.75530 AS Decimal(10, 5)), CAST(146.58099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7662, CAST(33.74559 AS Decimal(10, 5)), CAST(-116.27061 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7664, CAST(33.82807 AS Decimal(10, 5)), CAST(-116.50696 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7666, CAST(39.55000 AS Decimal(10, 5)), CAST(2.73333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7667, CAST(8.95028 AS Decimal(10, 5)), CAST(-83.46811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7670, CAST(-10.29150 AS Decimal(10, 5)), CAST(-48.35700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7674, CAST(-40.32044 AS Decimal(10, 5)), CAST(175.61671 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7679, CAST(-0.91854 AS Decimal(10, 5)), CAST(119.90964 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7684, CAST(42.77004 AS Decimal(10, 5)), CAST(-1.64633 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7686, CAST(26.53817 AS Decimal(10, 5)), CAST(101.79440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7691, CAST(8.97334 AS Decimal(10, 5)), CAST(-79.55558 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7692, CAST(9.07136 AS Decimal(10, 5)), CAST(-79.38345 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7699, CAST(-2.70335 AS Decimal(10, 5)), CAST(111.67062 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7700, CAST(-2.16319 AS Decimal(10, 5)), CAST(106.13922 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7702, CAST(66.14500 AS Decimal(10, 5)), CAST(-65.71361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7705, CAST(26.95455 AS Decimal(10, 5)), CAST(64.13252 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7706, CAST(36.81652 AS Decimal(10, 5)), CAST(11.96886 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7707, CAST(29.03340 AS Decimal(10, 5)), CAST(79.47370 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7709, CAST(60.32176 AS Decimal(10, 5)), CAST(-1.69335 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7710, CAST(59.35105 AS Decimal(10, 5)), CAST(-2.90018 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7711, CAST(-17.55375 AS Decimal(10, 5)), CAST(-149.60724 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7712, CAST(34.71804 AS Decimal(10, 5)), CAST(32.48573 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7715, CAST(-23.17228 AS Decimal(10, 5)), CAST(117.74700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7719, CAST(9.35000 AS Decimal(10, 5)), CAST(2.61667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7723, CAST(5.45283 AS Decimal(10, 5)), CAST(-55.18778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7724, CAST(5.76946 AS Decimal(10, 5)), CAST(-55.22235 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7725, CAST(-31.79478 AS Decimal(10, 5)), CAST(-60.48036 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7726, CAST(-25.54020 AS Decimal(10, 5)), CAST(-48.53116 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7729, CAST(-40.90472 AS Decimal(10, 5)), CAST(174.98917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7730, CAST(-9.64167 AS Decimal(10, 5)), CAST(161.42500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7733, CAST(50.01340 AS Decimal(10, 5)), CAST(15.73860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7738, CAST(49.45444 AS Decimal(10, 5)), CAST(2.11278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7740, CAST(49.01278 AS Decimal(10, 5)), CAST(2.55000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7747, CAST(48.96944 AS Decimal(10, 5)), CAST(2.44139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7749, CAST(48.72528 AS Decimal(10, 5)), CAST(2.35944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7752, CAST(48.77611 AS Decimal(10, 5)), CAST(4.18444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7758, CAST(-33.13140 AS Decimal(10, 5)), CAST(148.23900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7761, CAST(-2.89375 AS Decimal(10, 5)), CAST(-41.73196 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7765, CAST(27.40320 AS Decimal(10, 5)), CAST(89.42460 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7766, CAST(37.01071 AS Decimal(10, 5)), CAST(25.12809 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7768, CAST(39.60472 AS Decimal(10, 5)), CAST(47.87778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7773, CAST(46.26458 AS Decimal(10, 5)), CAST(-119.11890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7775, CAST(28.06610 AS Decimal(10, 5)), CAST(95.33560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7784, CAST(-28.24399 AS Decimal(10, 5)), CAST(-52.32656 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7787, CAST(1.39625 AS Decimal(10, 5)), CAST(-77.29148 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7789, CAST(32.23380 AS Decimal(10, 5)), CAST(75.63460 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7790, CAST(25.59132 AS Decimal(10, 5)), CAST(85.08799 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7794, CAST(38.15111 AS Decimal(10, 5)), CAST(21.42556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7800, CAST(43.38000 AS Decimal(10, 5)), CAST(-0.41861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7802, CAST(69.36169 AS Decimal(10, 5)), CAST(-124.07487 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7803, CAST(-9.40088 AS Decimal(10, 5)), CAST(-38.25057 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7805, CAST(52.19500 AS Decimal(10, 5)), CAST(77.07390 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7813, CAST(54.98810 AS Decimal(10, 5)), CAST(-85.44330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7815, CAST(65.12110 AS Decimal(10, 5)), CAST(57.13080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7820, CAST(59.79701 AS Decimal(10, 5)), CAST(-154.12964 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7825, CAST(0.46079 AS Decimal(10, 5)), CAST(101.44454 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7826, CAST(-29.12060 AS Decimal(10, 5)), CAST(28.50530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7827, CAST(57.95672 AS Decimal(10, 5)), CAST(-136.23223 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7829, CAST(45.57150 AS Decimal(10, 5)), CAST(-84.78618 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7830, CAST(-31.71835 AS Decimal(10, 5)), CAST(-52.32769 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7831, CAST(-12.98675 AS Decimal(10, 5)), CAST(40.52249 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7832, CAST(-5.25726 AS Decimal(10, 5)), CAST(39.81142 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7835, CAST(5.29714 AS Decimal(10, 5)), CAST(100.27686 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7838, CAST(45.69430 AS Decimal(10, 5)), CAST(-118.84166 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7848, CAST(30.47353 AS Decimal(10, 5)), CAST(-87.18664 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7850, CAST(49.46306 AS Decimal(10, 5)), CAST(-119.60222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7851, CAST(53.11667 AS Decimal(10, 5)), CAST(45.01667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7853, CAST(40.66000 AS Decimal(10, 5)), CAST(-89.69455 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7858, CAST(4.81267 AS Decimal(10, 5)), CAST(-75.73952 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7859, CAST(45.19719 AS Decimal(10, 5)), CAST(0.81316 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7862, CAST(57.91452 AS Decimal(10, 5)), CAST(56.02121 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7863, CAST(42.74044 AS Decimal(10, 5)), CAST(2.87067 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7871, CAST(-31.94028 AS Decimal(10, 5)), CAST(115.96694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7874, CAST(43.09591 AS Decimal(10, 5)), CAST(12.51322 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7875, CAST(42.43286 AS Decimal(10, 5)), CAST(14.18678 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7877, CAST(33.99391 AS Decimal(10, 5)), CAST(71.51458 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7881, CAST(56.81155 AS Decimal(10, 5)), CAST(-132.96706 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7887, CAST(-9.36241 AS Decimal(10, 5)), CAST(-40.56910 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7888, CAST(54.77470 AS Decimal(10, 5)), CAST(69.18390 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7889, CAST(53.16789 AS Decimal(10, 5)), CAST(158.45367 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7891, CAST(61.88520 AS Decimal(10, 5)), CAST(34.15470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7892, CAST(69.78364 AS Decimal(10, 5)), CAST(170.59690 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7894, CAST(-23.93717 AS Decimal(10, 5)), CAST(31.15539 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7898, CAST(27.51730 AS Decimal(10, 5)), CAST(86.58483 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7899, CAST(16.67600 AS Decimal(10, 5)), CAST(101.19500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7901, CAST(39.87835 AS Decimal(10, 5)), CAST(-75.24018 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7909, CAST(40.27677 AS Decimal(10, 5)), CAST(-74.81294 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7911, CAST(-27.84940 AS Decimal(10, 5)), CAST(32.30970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7912, CAST(16.77759 AS Decimal(10, 5)), CAST(100.28282 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7913, CAST(11.54656 AS Decimal(10, 5)), CAST(104.84414 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7918, CAST(33.43551 AS Decimal(10, 5)), CAST(-111.99807 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7919, CAST(33.30664 AS Decimal(10, 5)), CAST(-111.66793 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7920, CAST(22.00000 AS Decimal(10, 5)), CAST(101.90000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7921, CAST(18.13166 AS Decimal(10, 5)), CAST(100.16446 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7922, CAST(10.22700 AS Decimal(10, 5)), CAST(103.96700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7925, CAST(8.11320 AS Decimal(10, 5)), CAST(98.31687 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7932, CAST(51.44639 AS Decimal(10, 5)), CAST(-90.21417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7933, CAST(38.55434 AS Decimal(10, 5)), CAST(-28.44075 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7935, CAST(-41.34610 AS Decimal(10, 5)), CAST(173.95599 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7936, CAST(28.62958 AS Decimal(10, 5)), CAST(-100.53777 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7937, CAST(44.38248 AS Decimal(10, 5)), CAST(-100.28596 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7939, CAST(-29.64898 AS Decimal(10, 5)), CAST(30.39867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7941, CAST(51.81970 AS Decimal(10, 5)), CAST(-93.97330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7944, CAST(57.58036 AS Decimal(10, 5)), CAST(-157.57010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7946, CAST(57.41979 AS Decimal(10, 5)), CAST(-157.73520 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7947, CAST(61.93486 AS Decimal(10, 5)), CAST(-162.90204 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7968, CAST(43.68392 AS Decimal(10, 5)), CAST(10.39275 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7969, CAST(-13.74486 AS Decimal(10, 5)), CAST(-76.22028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7970, CAST(1.85777 AS Decimal(10, 5)), CAST(-76.08570 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7976, CAST(40.49608 AS Decimal(10, 5)), CAST(-80.25547 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7980, CAST(-5.20575 AS Decimal(10, 5)), CAST(-80.61644 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7981, CAST(16.53729 AS Decimal(10, 5)), CAST(-88.36171 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7986, CAST(44.81415 AS Decimal(10, 5)), CAST(136.28913 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7987, CAST(59.01555 AS Decimal(10, 5)), CAST(-161.82308 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (7997, CAST(9.30972 AS Decimal(10, 5)), CAST(-78.23459 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8000, CAST(13.99037 AS Decimal(10, 5)), CAST(108.02376 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8002, CAST(-34.09028 AS Decimal(10, 5)), CAST(23.32778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8006, CAST(42.06781 AS Decimal(10, 5)), CAST(24.85083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8007, CAST(50.42278 AS Decimal(10, 5)), CAST(-4.10583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8012, CAST(42.91297 AS Decimal(10, 5)), CAST(-112.59498 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8015, CAST(42.35939 AS Decimal(10, 5)), CAST(19.25189 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8018, CAST(35.98786 AS Decimal(10, 5)), CAST(129.42049 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8019, CAST(6.98510 AS Decimal(10, 5)), CAST(158.20899 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8020, CAST(56.35816 AS Decimal(10, 5)), CAST(-133.62891 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8021, CAST(68.34861 AS Decimal(10, 5)), CAST(-166.79917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8022, CAST(69.73302 AS Decimal(10, 5)), CAST(-163.01568 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8024, CAST(-4.81603 AS Decimal(10, 5)), CAST(11.88660 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8025, CAST(16.26531 AS Decimal(10, 5)), CAST(-61.53181 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8027, CAST(58.27670 AS Decimal(10, 5)), CAST(-104.08200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8029, CAST(46.58774 AS Decimal(10, 5)), CAST(0.30667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8031, CAST(28.20088 AS Decimal(10, 5)), CAST(83.98206 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8034, CAST(-23.84761 AS Decimal(10, 5)), CAST(29.45735 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8036, CAST(66.40056 AS Decimal(10, 5)), CAST(112.03028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8042, CAST(18.00976 AS Decimal(10, 5)), CAST(-66.56247 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8043, CAST(72.68674 AS Decimal(10, 5)), CAST(-77.97319 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8044, CAST(11.96870 AS Decimal(10, 5)), CAST(79.81010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8047, CAST(37.74118 AS Decimal(10, 5)), CAST(-25.69787 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8048, CAST(-25.18470 AS Decimal(10, 5)), CAST(-50.14410 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8052, CAST(42.66331 AS Decimal(10, 5)), CAST(-83.41711 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8053, CAST(-0.15071 AS Decimal(10, 5)), CAST(109.40389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8057, CAST(2.45440 AS Decimal(10, 5)), CAST(-76.60932 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8060, CAST(52.11330 AS Decimal(10, 5)), CAST(-94.25560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8062, CAST(-8.80454 AS Decimal(10, 5)), CAST(148.30901 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8063, CAST(49.07359 AS Decimal(10, 5)), CAST(20.24114 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8065, CAST(21.64870 AS Decimal(10, 5)), CAST(69.65720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8070, CAST(61.46169 AS Decimal(10, 5)), CAST(21.79998 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8071, CAST(10.91293 AS Decimal(10, 5)), CAST(-63.96758 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8073, CAST(49.31933 AS Decimal(10, 5)), CAST(-124.92979 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8077, CAST(60.20190 AS Decimal(10, 5)), CAST(-154.32429 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8081, CAST(18.58005 AS Decimal(10, 5)), CAST(-72.29254 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8082, CAST(-32.50690 AS Decimal(10, 5)), CAST(137.71700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8083, CAST(57.93062 AS Decimal(10, 5)), CAST(-153.03959 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8085, CAST(11.64101 AS Decimal(10, 5)), CAST(92.72997 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8089, CAST(12.98863 AS Decimal(10, 5)), CAST(-61.26238 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8090, CAST(-33.98492 AS Decimal(10, 5)), CAST(25.61727 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8093, CAST(-0.71174 AS Decimal(10, 5)), CAST(8.75438 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8094, CAST(59.35091 AS Decimal(10, 5)), CAST(-151.83215 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8095, CAST(5.01549 AS Decimal(10, 5)), CAST(6.94959 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8097, CAST(50.68056 AS Decimal(10, 5)), CAST(-127.36667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8099, CAST(-20.37778 AS Decimal(10, 5)), CAST(118.62639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8100, CAST(56.95889 AS Decimal(10, 5)), CAST(-158.63250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8110, CAST(-34.60530 AS Decimal(10, 5)), CAST(135.88000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8112, CAST(-31.43580 AS Decimal(10, 5)), CAST(152.86301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8114, CAST(49.83639 AS Decimal(10, 5)), CAST(-64.28861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8115, CAST(56.00599 AS Decimal(10, 5)), CAST(-160.55986 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8116, CAST(-9.44338 AS Decimal(10, 5)), CAST(147.22005 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8118, CAST(10.59537 AS Decimal(10, 5)), CAST(-61.33724 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8127, CAST(19.43333 AS Decimal(10, 5)), CAST(37.23333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8130, CAST(-17.69933 AS Decimal(10, 5)), CAST(168.31979 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8132, CAST(58.48995 AS Decimal(10, 5)), CAST(-152.58323 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8137, CAST(37.14930 AS Decimal(10, 5)), CAST(-8.58396 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8138, CAST(43.64728 AS Decimal(10, 5)), CAST(-70.30992 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8139, CAST(45.58919 AS Decimal(10, 5)), CAST(-122.59353 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8140, CAST(-38.31913 AS Decimal(10, 5)), CAST(141.47032 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8141, CAST(41.24806 AS Decimal(10, 5)), CAST(-8.68139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8143, CAST(-29.99443 AS Decimal(10, 5)), CAST(-51.17143 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8151, CAST(33.07339 AS Decimal(10, 5)), CAST(-16.34998 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8152, CAST(-16.43864 AS Decimal(10, 5)), CAST(-39.08092 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8154, CAST(-8.70929 AS Decimal(10, 5)), CAST(-63.90228 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8155, CAST(45.47488 AS Decimal(10, 5)), CAST(13.61481 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8159, CAST(-53.25370 AS Decimal(10, 5)), CAST(-70.31920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8161, CAST(-27.38584 AS Decimal(10, 5)), CAST(-55.97073 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8162, CAST(-1.41675 AS Decimal(10, 5)), CAST(120.65767 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8163, CAST(54.91031 AS Decimal(10, 5)), CAST(-59.78569 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8166, CAST(-19.54307 AS Decimal(10, 5)), CAST(-65.72371 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8173, CAST(60.05056 AS Decimal(10, 5)), CAST(-77.28694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8176, CAST(49.83444 AS Decimal(10, 5)), CAST(-124.50162 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8177, CAST(20.60025 AS Decimal(10, 5)), CAST(-97.46079 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8179, CAST(52.42103 AS Decimal(10, 5)), CAST(16.82633 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8183, CAST(50.10083 AS Decimal(10, 5)), CAST(14.26000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8184, CAST(14.94197 AS Decimal(10, 5)), CAST(-23.48448 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8186, CAST(-4.31929 AS Decimal(10, 5)), CAST(55.69142 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8192, CAST(34.65091 AS Decimal(10, 5)), CAST(-112.42044 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8195, CAST(-22.17506 AS Decimal(10, 5)), CAST(-51.42464 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8197, CAST(46.69547 AS Decimal(10, 5)), CAST(-68.04747 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8203, CAST(-25.65386 AS Decimal(10, 5)), CAST(28.22423 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8204, CAST(38.92547 AS Decimal(10, 5)), CAST(20.76531 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8207, CAST(53.21577 AS Decimal(10, 5)), CAST(-105.67846 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8208, CAST(53.88737 AS Decimal(10, 5)), CAST(-122.67600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8211, CAST(54.28611 AS Decimal(10, 5)), CAST(-130.44472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8218, CAST(1.66269 AS Decimal(10, 5)), CAST(7.41105 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8220, CAST(42.57278 AS Decimal(10, 5)), CAST(21.03583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8225, CAST(-20.49500 AS Decimal(10, 5)), CAST(148.55222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8229, CAST(41.72610 AS Decimal(10, 5)), CAST(-71.43524 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8231, CAST(21.77361 AS Decimal(10, 5)), CAST(-72.26583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8233, CAST(42.07205 AS Decimal(10, 5)), CAST(-70.22058 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8235, CAST(40.21551 AS Decimal(10, 5)), CAST(-111.72132 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8237, CAST(70.19472 AS Decimal(10, 5)), CAST(-148.46500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8238, CAST(57.78390 AS Decimal(10, 5)), CAST(28.39560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8240, CAST(-8.37794 AS Decimal(10, 5)), CAST(-74.57430 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8242, CAST(19.15814 AS Decimal(10, 5)), CAST(-98.37145 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8243, CAST(38.28889 AS Decimal(10, 5)), CAST(-104.49692 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8247, CAST(0.50523 AS Decimal(10, 5)), CAST(-76.50084 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8248, CAST(5.61999 AS Decimal(10, 5)), CAST(-67.60610 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8249, CAST(15.73088 AS Decimal(10, 5)), CAST(-88.58377 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8252, CAST(10.48050 AS Decimal(10, 5)), CAST(-68.07303 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8253, CAST(14.04719 AS Decimal(10, 5)), CAST(-83.38672 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8254, CAST(6.18472 AS Decimal(10, 5)), CAST(-67.49316 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8258, CAST(15.87686 AS Decimal(10, 5)), CAST(-97.08912 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8259, CAST(3.85353 AS Decimal(10, 5)), CAST(-67.90620 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8260, CAST(8.53333 AS Decimal(10, 5)), CAST(-83.30000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8266, CAST(-0.18228 AS Decimal(10, 5)), CAST(-74.77080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8267, CAST(15.26231 AS Decimal(10, 5)), CAST(-83.78118 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8268, CAST(-42.75917 AS Decimal(10, 5)), CAST(-65.10278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8269, CAST(-12.61361 AS Decimal(10, 5)), CAST(-69.22861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8270, CAST(-41.43889 AS Decimal(10, 5)), CAST(-73.09395 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8271, CAST(-51.67150 AS Decimal(10, 5)), CAST(-72.52840 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8272, CAST(8.66728 AS Decimal(10, 5)), CAST(-77.41847 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8273, CAST(8.28853 AS Decimal(10, 5)), CAST(-62.76036 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8276, CAST(19.75790 AS Decimal(10, 5)), CAST(-70.57003 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8277, CAST(9.74205 AS Decimal(10, 5)), CAST(118.75757 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8280, CAST(-18.97528 AS Decimal(10, 5)), CAST(-57.82059 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8281, CAST(20.68008 AS Decimal(10, 5)), CAST(-105.25417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8283, CAST(-54.93110 AS Decimal(10, 5)), CAST(-67.62630 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8289, CAST(44.89353 AS Decimal(10, 5)), CAST(13.92219 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8292, CAST(46.74378 AS Decimal(10, 5)), CAST(-117.10934 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8294, CAST(18.58223 AS Decimal(10, 5)), CAST(73.91988 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8297, CAST(-53.00264 AS Decimal(10, 5)), CAST(-70.85459 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8298, CAST(18.56737 AS Decimal(10, 5)), CAST(-68.36343 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8302, CAST(-34.85514 AS Decimal(10, 5)), CAST(-55.09428 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8303, CAST(16.10200 AS Decimal(10, 5)), CAST(-88.80891 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8304, CAST(26.91601 AS Decimal(10, 5)), CAST(-81.99691 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8309, CAST(27.33140 AS Decimal(10, 5)), CAST(97.42607 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8313, CAST(0.83558 AS Decimal(10, 5)), CAST(112.93700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8316, CAST(39.22406 AS Decimal(10, 5)), CAST(125.67015 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8318, CAST(77.48860 AS Decimal(10, 5)), CAST(-69.38870 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8319, CAST(70.73420 AS Decimal(10, 5)), CAST(-52.69620 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8321, CAST(28.33556 AS Decimal(10, 5)), CAST(46.12472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8324, CAST(21.37477 AS Decimal(10, 5)), CAST(57.05420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8332, CAST(38.14940 AS Decimal(10, 5)), CAST(85.53280 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8333, CAST(67.54587 AS Decimal(10, 5)), CAST(-64.03304 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8334, CAST(36.26748 AS Decimal(10, 5)), CAST(120.38335 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8335, CAST(35.79970 AS Decimal(10, 5)), CAST(107.60300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8337, CAST(47.23963 AS Decimal(10, 5)), CAST(123.91813 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8340, CAST(49.33944 AS Decimal(10, 5)), CAST(-124.39643 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8344, CAST(61.04721 AS Decimal(10, 5)), CAST(-69.61843 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8345, CAST(46.79227 AS Decimal(10, 5)), CAST(-71.38431 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8352, CAST(-45.01837 AS Decimal(10, 5)), CAST(168.74007 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8355, CAST(-17.85550 AS Decimal(10, 5)), CAST(36.86911 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8356, CAST(9.44316 AS Decimal(10, 5)), CAST(-84.12980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8357, CAST(20.61729 AS Decimal(10, 5)), CAST(-100.18566 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8358, CAST(53.02611 AS Decimal(10, 5)), CAST(-122.51028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8359, CAST(30.25137 AS Decimal(10, 5)), CAST(66.93776 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8361, CAST(13.95500 AS Decimal(10, 5)), CAST(109.04200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8362, CAST(5.69076 AS Decimal(10, 5)), CAST(-76.64118 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8365, CAST(-26.61220 AS Decimal(10, 5)), CAST(144.25301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8366, CAST(47.97498 AS Decimal(10, 5)), CAST(-4.16779 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8369, CAST(39.93771 AS Decimal(10, 5)), CAST(-91.19399 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8372, CAST(59.75898 AS Decimal(10, 5)), CAST(-161.85364 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8374, CAST(-0.14114 AS Decimal(10, 5)), CAST(-78.48821 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8381, CAST(34.05147 AS Decimal(10, 5)), CAST(-6.75152 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8382, CAST(-4.34046 AS Decimal(10, 5)), CAST(152.38000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8384, CAST(9.95803 AS Decimal(10, 5)), CAST(105.13238 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8389, CAST(64.11599 AS Decimal(10, 5)), CAST(-117.30958 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8392, CAST(29.62642 AS Decimal(10, 5)), CAST(43.49061 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8393, CAST(30.29770 AS Decimal(10, 5)), CAST(56.05110 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8396, CAST(-4.76300 AS Decimal(10, 5)), CAST(122.56600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8397, CAST(28.38390 AS Decimal(10, 5)), CAST(70.27960 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8398, CAST(-16.72290 AS Decimal(10, 5)), CAST(-151.46600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8407, CAST(21.18040 AS Decimal(10, 5)), CAST(81.73880 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8408, CAST(-23.88615 AS Decimal(10, 5)), CAST(-147.66522 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8409, CAST(17.11040 AS Decimal(10, 5)), CAST(81.81820 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8410, CAST(26.50800 AS Decimal(10, 5)), CAST(86.73800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8411, CAST(22.30918 AS Decimal(10, 5)), CAST(70.77953 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8413, CAST(24.43722 AS Decimal(10, 5)), CAST(88.61651 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8419, CAST(-8.16806 AS Decimal(10, 5)), CAST(157.64301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8423, CAST(65.51091 AS Decimal(10, 5)), CAST(-150.14877 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8424, CAST(36.90991 AS Decimal(10, 5)), CAST(50.67959 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8429, CAST(23.31425 AS Decimal(10, 5)), CAST(85.32167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8434, CAST(-14.95428 AS Decimal(10, 5)), CAST(-147.66080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8436, CAST(62.81027 AS Decimal(10, 5)), CAST(-92.11332 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8437, CAST(9.77762 AS Decimal(10, 5)), CAST(98.58548 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8441, CAST(44.03938 AS Decimal(10, 5)), CAST(-103.05985 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8443, CAST(-16.04717 AS Decimal(10, 5)), CAST(-142.47701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8444, CAST(-21.20274 AS Decimal(10, 5)), CAST(-159.80556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8445, CAST(25.61348 AS Decimal(10, 5)), CAST(55.93882 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8446, CAST(37.32531 AS Decimal(10, 5)), CAST(49.60582 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8460, CAST(-33.79000 AS Decimal(10, 5)), CAST(120.20000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8469, CAST(-8.12679 AS Decimal(10, 5)), CAST(-34.92304 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8471, CAST(-29.21028 AS Decimal(10, 5)), CAST(-59.68000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8473, CAST(52.18010 AS Decimal(10, 5)), CAST(-113.89269 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8474, CAST(61.78452 AS Decimal(10, 5)), CAST(-157.34176 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8475, CAST(68.02875 AS Decimal(10, 5)), CAST(-162.90855 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8476, CAST(51.06694 AS Decimal(10, 5)), CAST(-93.79306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8478, CAST(54.16720 AS Decimal(10, 5)), CAST(-93.55720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8481, CAST(40.50876 AS Decimal(10, 5)), CAST(-122.29445 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8492, CAST(38.07121 AS Decimal(10, 5)), CAST(15.65156 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8495, CAST(50.43194 AS Decimal(10, 5)), CAST(-104.66583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8502, CAST(-0.35281 AS Decimal(10, 5)), CAST(102.33492 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8504, CAST(-11.55006 AS Decimal(10, 5)), CAST(160.06273 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8506, CAST(48.06951 AS Decimal(10, 5)), CAST(-1.73479 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8507, CAST(39.50597 AS Decimal(10, 5)), CAST(-119.77312 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8509, CAST(47.49288 AS Decimal(10, 5)), CAST(-122.21568 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8510, CAST(66.52366 AS Decimal(10, 5)), CAST(-86.22808 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8514, CAST(-27.44999 AS Decimal(10, 5)), CAST(-59.05613 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8516, CAST(74.71694 AS Decimal(10, 5)), CAST(-94.96944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8520, CAST(41.14739 AS Decimal(10, 5)), CAST(1.16717 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8527, CAST(63.98500 AS Decimal(10, 5)), CAST(-22.60556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8529, CAST(64.13000 AS Decimal(10, 5)), CAST(-21.94056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8530, CAST(26.00891 AS Decimal(10, 5)), CAST(-98.22851 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8533, CAST(45.62692 AS Decimal(10, 5)), CAST(-89.46310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8534, CAST(36.40542 AS Decimal(10, 5)), CAST(28.08619 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8536, CAST(-21.13417 AS Decimal(10, 5)), CAST(-47.77419 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8537, CAST(-11.00000 AS Decimal(10, 5)), CAST(-66.11667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8540, CAST(-28.74104 AS Decimal(10, 5)), CAST(32.09211 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8545, CAST(-20.70190 AS Decimal(10, 5)), CAST(143.11501 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8546, CAST(37.50517 AS Decimal(10, 5)), CAST(-77.31967 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8552, CAST(56.92361 AS Decimal(10, 5)), CAST(23.97111 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8553, CAST(54.17937 AS Decimal(10, 5)), CAST(-58.45748 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8555, CAST(45.21689 AS Decimal(10, 5)), CAST(14.57027 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8556, CAST(-22.64000 AS Decimal(10, 5)), CAST(-152.80000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8557, CAST(44.02029 AS Decimal(10, 5)), CAST(12.61175 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8563, CAST(-9.86916 AS Decimal(10, 5)), CAST(-67.89407 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8565, CAST(-33.08513 AS Decimal(10, 5)), CAST(-64.26131 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8566, CAST(-22.79865 AS Decimal(10, 5)), CAST(-43.23077 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8568, CAST(-22.91046 AS Decimal(10, 5)), CAST(-43.16313 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8572, CAST(-51.60887 AS Decimal(10, 5)), CAST(-69.31264 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8574, CAST(-53.77767 AS Decimal(10, 5)), CAST(-67.74939 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8575, CAST(-27.49600 AS Decimal(10, 5)), CAST(-64.93500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8581, CAST(-17.83472 AS Decimal(10, 5)), CAST(-50.95611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8582, CAST(11.52622 AS Decimal(10, 5)), CAST(-72.92596 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8585, CAST(45.24201 AS Decimal(10, 5)), CAST(141.18643 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8592, CAST(33.89267 AS Decimal(10, 5)), CAST(-117.26081 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8595, CAST(43.06385 AS Decimal(10, 5)), CAST(-108.46011 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8599, CAST(24.95764 AS Decimal(10, 5)), CAST(46.69878 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8603, CAST(37.32541 AS Decimal(10, 5)), CAST(-79.97532 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8605, CAST(16.31681 AS Decimal(10, 5)), CAST(-86.52296 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8618, CAST(43.90995 AS Decimal(10, 5)), CAST(-92.49630 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8620, CAST(43.11887 AS Decimal(10, 5)), CAST(-77.67239 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8623, CAST(24.89177 AS Decimal(10, 5)), CAST(-76.17044 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8624, CAST(41.59413 AS Decimal(10, 5)), CAST(-109.06568 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8629, CAST(-23.37651 AS Decimal(10, 5)), CAST(150.47392 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8631, CAST(44.06111 AS Decimal(10, 5)), CAST(-69.09629 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8636, CAST(44.40787 AS Decimal(10, 5)), CAST(2.48267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8637, CAST(-19.75766 AS Decimal(10, 5)), CAST(63.36098 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8645, CAST(16.11680 AS Decimal(10, 5)), CAST(103.77400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8652, CAST(-26.54469 AS Decimal(10, 5)), CAST(148.77548 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8655, CAST(41.79936 AS Decimal(10, 5)), CAST(12.59494 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8656, CAST(41.79352 AS Decimal(10, 5)), CAST(12.25259 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8664, CAST(-16.58600 AS Decimal(10, 5)), CAST(-54.72480 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8666, CAST(56.26667 AS Decimal(10, 5)), CAST(15.26500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8674, CAST(62.57841 AS Decimal(10, 5)), CAST(11.34235 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8676, CAST(-32.90361 AS Decimal(10, 5)), CAST(-60.78500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8681, CAST(11.78589 AS Decimal(10, 5)), CAST(34.33666 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8688, CAST(67.52780 AS Decimal(10, 5)), CAST(12.10330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8689, CAST(53.91667 AS Decimal(10, 5)), CAST(12.26667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8690, CAST(47.25821 AS Decimal(10, 5)), CAST(39.81809 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8691, CAST(33.30149 AS Decimal(10, 5)), CAST(-104.53052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8692, CAST(14.17415 AS Decimal(10, 5)), CAST(145.24396 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8695, CAST(-10.48430 AS Decimal(10, 5)), CAST(123.37400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8696, CAST(-38.10917 AS Decimal(10, 5)), CAST(176.31722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8697, CAST(51.95548 AS Decimal(10, 5)), CAST(4.43685 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8700, CAST(-12.48267 AS Decimal(10, 5)), CAST(177.07060 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8703, CAST(49.38417 AS Decimal(10, 5)), CAST(1.17480 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8704, CAST(52.94360 AS Decimal(10, 5)), CAST(-91.31280 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8709, CAST(48.20611 AS Decimal(10, 5)), CAST(-78.83556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8710, CAST(66.56482 AS Decimal(10, 5)), CAST(25.83041 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8713, CAST(11.59767 AS Decimal(10, 5)), CAST(122.75167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8717, CAST(64.72759 AS Decimal(10, 5)), CAST(-155.46611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8732, CAST(-17.95526 AS Decimal(10, 5)), CAST(19.72308 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8734, CAST(-14.42737 AS Decimal(10, 5)), CAST(-67.50069 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8735, CAST(-22.43189 AS Decimal(10, 5)), CAST(-151.36622 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8739, CAST(-8.59610 AS Decimal(10, 5)), CAST(120.47790 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8741, CAST(43.52714 AS Decimal(10, 5)), CAST(-72.94664 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8747, CAST(50.10996 AS Decimal(10, 5)), CAST(22.01900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8749, CAST(49.21455 AS Decimal(10, 5)), CAST(7.10951 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8761, CAST(5.86667 AS Decimal(10, 5)), CAST(95.31667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8765, CAST(36.16808 AS Decimal(10, 5)), CAST(57.59518 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8766, CAST(53.89110 AS Decimal(10, 5)), CAST(-92.19640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8767, CAST(71.99389 AS Decimal(10, 5)), CAST(-125.24250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8772, CAST(38.69518 AS Decimal(10, 5)), CAST(-121.59163 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8774, CAST(37.89381 AS Decimal(10, 5)), CAST(-121.23911 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8782, CAST(33.14972 AS Decimal(10, 5)), CAST(130.30222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8790, CAST(-9.37785 AS Decimal(10, 5)), CAST(142.62443 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8793, CAST(25.75920 AS Decimal(10, 5)), CAST(88.90890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8796, CAST(45.54639 AS Decimal(10, 5)), CAST(-94.05972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8798, CAST(37.09582 AS Decimal(10, 5)), CAST(-113.59301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8800, CAST(45.32825 AS Decimal(10, 5)), CAST(-65.89119 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8801, CAST(62.05877 AS Decimal(10, 5)), CAST(-163.29765 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8802, CAST(57.15811 AS Decimal(10, 5)), CAST(-170.22991 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8806, CAST(-17.09389 AS Decimal(10, 5)), CAST(49.81583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8808, CAST(15.11772 AS Decimal(10, 5)), CAST(145.72609 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8809, CAST(17.19454 AS Decimal(10, 5)), CAST(104.11809 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8810, CAST(16.74139 AS Decimal(10, 5)), CAST(-22.94944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8812, CAST(17.03872 AS Decimal(10, 5)), CAST(54.09130 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8813, CAST(40.95212 AS Decimal(10, 5)), CAST(-5.50199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8817, CAST(66.58915 AS Decimal(10, 5)), CAST(66.59569 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8819, CAST(11.78330 AS Decimal(10, 5)), CAST(78.06560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8825, CAST(38.79060 AS Decimal(10, 5)), CAST(-97.64141 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8829, CAST(-2.20329 AS Decimal(10, 5)), CAST(-80.99233 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8832, CAST(38.34040 AS Decimal(10, 5)), CAST(-75.51020 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8834, CAST(62.17920 AS Decimal(10, 5)), CAST(-75.66726 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8838, CAST(21.33393 AS Decimal(10, 5)), CAST(-71.20338 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8839, CAST(40.78688 AS Decimal(10, 5)), CAST(-111.98203 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8840, CAST(-24.85598 AS Decimal(10, 5)), CAST(-65.48617 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8841, CAST(25.54950 AS Decimal(10, 5)), CAST(-100.92867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8844, CAST(-12.91099 AS Decimal(10, 5)), CAST(-38.33104 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8846, CAST(47.79330 AS Decimal(10, 5)), CAST(13.00433 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8849, CAST(20.41840 AS Decimal(10, 5)), CAST(104.06700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8850, CAST(19.26918 AS Decimal(10, 5)), CAST(-69.73726 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8851, CAST(53.50816 AS Decimal(10, 5)), CAST(50.15156 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8853, CAST(-0.48271 AS Decimal(10, 5)), CAST(117.15820 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8854, CAST(39.70055 AS Decimal(10, 5)), CAST(66.98383 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8855, CAST(-14.27861 AS Decimal(10, 5)), CAST(50.17472 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8857, CAST(0.53330 AS Decimal(10, 5)), CAST(37.53307 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8859, CAST(37.69000 AS Decimal(10, 5)), CAST(26.91167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8860, CAST(-2.49919 AS Decimal(10, 5)), CAST(112.97500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8861, CAST(41.25845 AS Decimal(10, 5)), CAST(36.55598 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8863, CAST(12.58359 AS Decimal(10, 5)), CAST(-81.71119 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8866, CAST(31.35850 AS Decimal(10, 5)), CAST(-100.50287 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8869, CAST(29.53384 AS Decimal(10, 5)), CAST(-98.47002 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8870, CAST(29.38003 AS Decimal(10, 5)), CAST(-98.58649 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8873, CAST(29.52967 AS Decimal(10, 5)), CAST(-98.27918 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8882, CAST(37.51077 AS Decimal(10, 5)), CAST(-122.25052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8883, CAST(11.13340 AS Decimal(10, 5)), CAST(-84.77000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8884, CAST(-41.15117 AS Decimal(10, 5)), CAST(-71.15754 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8885, CAST(-0.91021 AS Decimal(10, 5)), CAST(-89.61745 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8890, CAST(32.73235 AS Decimal(10, 5)), CAST(-117.19744 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8893, CAST(32.81588 AS Decimal(10, 5)), CAST(-117.14114 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8904, CAST(7.88332 AS Decimal(10, 5)), CAST(-67.44402 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8907, CAST(37.61882 AS Decimal(10, 5)), CAST(-122.37580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8913, CAST(-16.38333 AS Decimal(10, 5)), CAST(-60.96667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8919, CAST(37.36189 AS Decimal(10, 5)), CAST(-121.92885 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8921, CAST(9.99386 AS Decimal(10, 5)), CAST(-84.20881 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8923, CAST(12.36152 AS Decimal(10, 5)), CAST(121.04664 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8924, CAST(23.15185 AS Decimal(10, 5)), CAST(-109.72104 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8925, CAST(2.57969 AS Decimal(10, 5)), CAST(-72.63936 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8929, CAST(18.45697 AS Decimal(10, 5)), CAST(-66.09715 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8930, CAST(18.43851 AS Decimal(10, 5)), CAST(-66.00297 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8932, CAST(-31.57147 AS Decimal(10, 5)), CAST(-68.41819 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8938, CAST(-33.27319 AS Decimal(10, 5)), CAST(-66.35642 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8943, CAST(22.25430 AS Decimal(10, 5)), CAST(-100.93081 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8947, CAST(-40.07528 AS Decimal(10, 5)), CAST(-71.13722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8953, CAST(17.91406 AS Decimal(10, 5)), CAST(-87.97096 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8955, CAST(4.74672 AS Decimal(10, 5)), CAST(-6.66082 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8959, CAST(15.45264 AS Decimal(10, 5)), CAST(-87.92356 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8965, CAST(-34.58831 AS Decimal(10, 5)), CAST(-68.40385 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8967, CAST(24.06328 AS Decimal(10, 5)), CAST(-74.52397 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8968, CAST(13.44095 AS Decimal(10, 5)), CAST(-89.05573 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8970, CAST(28.02987 AS Decimal(10, 5)), CAST(-17.21455 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8971, CAST(43.35652 AS Decimal(10, 5)), CAST(-1.79061 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8972, CAST(8.94515 AS Decimal(10, 5)), CAST(-64.15108 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8973, CAST(2.14757 AS Decimal(10, 5)), CAST(-74.76327 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8975, CAST(15.47626 AS Decimal(10, 5)), CAST(44.21974 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8977, CAST(35.24586 AS Decimal(10, 5)), CAST(47.00925 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8979, CAST(55.31448 AS Decimal(10, 5)), CAST(-160.52226 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8980, CAST(5.90090 AS Decimal(10, 5)), CAST(118.05949 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8981, CAST(61.83333 AS Decimal(10, 5)), CAST(6.11667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8982, CAST(59.25030 AS Decimal(10, 5)), CAST(-2.57667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8986, CAST(65.95683 AS Decimal(10, 5)), CAST(12.46894 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8988, CAST(53.25398 AS Decimal(10, 5)), CAST(-131.81373 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (8993, CAST(53.06420 AS Decimal(10, 5)), CAST(-93.34440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9002, CAST(56.53703 AS Decimal(10, 5)), CAST(-79.24872 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9004, CAST(37.44556 AS Decimal(10, 5)), CAST(38.90216 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9009, CAST(33.67803 AS Decimal(10, 5)), CAST(-117.86368 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9012, CAST(-10.84840 AS Decimal(10, 5)), CAST(162.45440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9014, CAST(34.42611 AS Decimal(10, 5)), CAST(-119.84125 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9022, CAST(22.49190 AS Decimal(10, 5)), CAST(-79.94382 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9025, CAST(-17.64476 AS Decimal(10, 5)), CAST(-63.13536 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9031, CAST(28.62648 AS Decimal(10, 5)), CAST(-17.75561 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9033, CAST(-10.71923 AS Decimal(10, 5)), CAST(165.79811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9039, CAST(-31.71167 AS Decimal(10, 5)), CAST(-60.81167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9043, CAST(34.89887 AS Decimal(10, 5)), CAST(-120.45822 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9046, CAST(36.97139 AS Decimal(10, 5)), CAST(-25.17064 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9048, CAST(11.11965 AS Decimal(10, 5)), CAST(-74.23065 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9052, CAST(38.50880 AS Decimal(10, 5)), CAST(-122.81388 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9053, CAST(-36.58832 AS Decimal(10, 5)), CAST(-64.27569 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9064, CAST(43.42720 AS Decimal(10, 5)), CAST(-3.82335 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9065, CAST(-2.42243 AS Decimal(10, 5)), CAST(-54.79279 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9066, CAST(-33.39297 AS Decimal(10, 5)), CAST(-70.78580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9069, CAST(19.96977 AS Decimal(10, 5)), CAST(-75.83541 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9070, CAST(19.40609 AS Decimal(10, 5)), CAST(-70.60469 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9073, CAST(42.89766 AS Decimal(10, 5)), CAST(-8.41793 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9074, CAST(-27.76562 AS Decimal(10, 5)), CAST(-64.31012 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9076, CAST(-28.28197 AS Decimal(10, 5)), CAST(-54.17122 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9079, CAST(18.42966 AS Decimal(10, 5)), CAST(-69.66893 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9081, CAST(7.56511 AS Decimal(10, 5)), CAST(-72.03512 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9083, CAST(18.30289 AS Decimal(10, 5)), CAST(109.41227 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9089, CAST(-11.63240 AS Decimal(10, 5)), CAST(-50.68960 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9091, CAST(14.88500 AS Decimal(10, 5)), CAST(-24.48000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9097, CAST(38.66550 AS Decimal(10, 5)), CAST(-28.17580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9099, CAST(-20.81660 AS Decimal(10, 5)), CAST(-49.40650 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9100, CAST(-23.22817 AS Decimal(10, 5)), CAST(-45.86274 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9104, CAST(-2.58536 AS Decimal(10, 5)), CAST(-44.23414 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9108, CAST(16.58840 AS Decimal(10, 5)), CAST(-24.28470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9109, CAST(-23.62754 AS Decimal(10, 5)), CAST(-46.65597 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9110, CAST(-23.43057 AS Decimal(10, 5)), CAST(-46.48101 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9112, CAST(-23.00738 AS Decimal(10, 5)), CAST(-47.13452 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9114, CAST(0.37817 AS Decimal(10, 5)), CAST(6.71215 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9115, CAST(16.83326 AS Decimal(10, 5)), CAST(-25.05552 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9119, CAST(42.77520 AS Decimal(10, 5)), CAST(141.69228 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9121, CAST(43.11614 AS Decimal(10, 5)), CAST(141.38022 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9124, CAST(-15.47080 AS Decimal(10, 5)), CAST(168.15199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9125, CAST(43.82458 AS Decimal(10, 5)), CAST(18.33147 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9127, CAST(44.38025 AS Decimal(10, 5)), CAST(-74.20483 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9128, CAST(54.12513 AS Decimal(10, 5)), CAST(45.21226 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9130, CAST(51.56500 AS Decimal(10, 5)), CAST(46.04667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9132, CAST(6.95472 AS Decimal(10, 5)), CAST(-71.85806 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9138, CAST(9.15000 AS Decimal(10, 5)), CAST(18.38333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9143, CAST(42.99944 AS Decimal(10, 5)), CAST(-82.30889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9148, CAST(36.63580 AS Decimal(10, 5)), CAST(53.19360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9150, CAST(52.17083 AS Decimal(10, 5)), CAST(-106.69972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9156, CAST(47.70327 AS Decimal(10, 5)), CAST(22.88570 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9160, CAST(46.25221 AS Decimal(10, 5)), CAST(-84.47009 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9163, CAST(46.48500 AS Decimal(10, 5)), CAST(-84.50944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9164, CAST(-7.98835 AS Decimal(10, 5)), CAST(131.30529 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9167, CAST(-9.68907 AS Decimal(10, 5)), CAST(20.43190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9171, CAST(32.12758 AS Decimal(10, 5)), CAST(-81.20214 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9172, CAST(16.55659 AS Decimal(10, 5)), CAST(104.75953 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9176, CAST(61.94306 AS Decimal(10, 5)), CAST(28.94514 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9177, CAST(63.68639 AS Decimal(10, 5)), CAST(-170.49250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9178, CAST(-16.80323 AS Decimal(10, 5)), CAST(179.34099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9180, CAST(26.96805 AS Decimal(10, 5)), CAST(68.87947 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9190, CAST(54.80528 AS Decimal(10, 5)), CAST(-66.80528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9206, CAST(41.87413 AS Decimal(10, 5)), CAST(-103.60050 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9215, CAST(47.53505 AS Decimal(10, 5)), CAST(-122.30624 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9216, CAST(47.62799 AS Decimal(10, 5)), CAST(-122.33933 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9218, CAST(47.44899 AS Decimal(10, 5)), CAST(-122.30929 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9220, CAST(26.98696 AS Decimal(10, 5)), CAST(14.47252 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9231, CAST(-8.57889 AS Decimal(10, 5)), CAST(157.87601 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9238, CAST(15.96611 AS Decimal(10, 5)), CAST(48.78830 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9240, CAST(66.60111 AS Decimal(10, 5)), CAST(-159.99194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9242, CAST(59.44222 AS Decimal(10, 5)), CAST(-151.70389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9251, CAST(-6.97261 AS Decimal(10, 5)), CAST(110.37530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9254, CAST(11.54302 AS Decimal(10, 5)), CAST(41.24027 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9255, CAST(50.35130 AS Decimal(10, 5)), CAST(80.23440 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9260, CAST(38.13972 AS Decimal(10, 5)), CAST(140.91694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9269, CAST(42.33860 AS Decimal(10, 5)), CAST(1.40917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9270, CAST(37.56600 AS Decimal(10, 5)), CAST(126.80077 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9271, CAST(37.44920 AS Decimal(10, 5)), CAST(126.45095 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9275, CAST(50.22333 AS Decimal(10, 5)), CAST(-66.26556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9278, CAST(-2.45806 AS Decimal(10, 5)), CAST(34.82250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9283, CAST(-1.87400 AS Decimal(10, 5)), CAST(136.23900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9295, CAST(37.41800 AS Decimal(10, 5)), CAST(-5.89311 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9300, CAST(34.71795 AS Decimal(10, 5)), CAST(10.69097 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9302, CAST(62.68747 AS Decimal(10, 5)), CAST(-159.56682 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9303, CAST(32.29458 AS Decimal(10, 5)), CAST(50.83770 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9307, CAST(64.36623 AS Decimal(10, 5)), CAST(-161.21579 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9308, CAST(55.86560 AS Decimal(10, 5)), CAST(-92.08140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9310, CAST(31.19787 AS Decimal(10, 5)), CAST(121.33632 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9314, CAST(52.70198 AS Decimal(10, 5)), CAST(-8.92482 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9316, CAST(23.42742 AS Decimal(10, 5)), CAST(116.76244 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9319, CAST(25.32858 AS Decimal(10, 5)), CAST(55.51715 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9321, CAST(27.97729 AS Decimal(10, 5)), CAST(34.39495 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9322, CAST(17.46688 AS Decimal(10, 5)), CAST(47.12143 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9346, CAST(41.63984 AS Decimal(10, 5)), CAST(123.48342 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9347, CAST(22.63926 AS Decimal(10, 5)), CAST(113.81066 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9350, CAST(44.77397 AS Decimal(10, 5)), CAST(-106.97128 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9352, CAST(60.19220 AS Decimal(10, 5)), CAST(-1.24361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9355, CAST(59.87889 AS Decimal(10, 5)), CAST(-1.29556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9356, CAST(38.28070 AS Decimal(10, 5)), CAST(114.69700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9359, CAST(25.70360 AS Decimal(10, 5)), CAST(91.97870 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9361, CAST(42.36417 AS Decimal(10, 5)), CAST(69.47889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9364, CAST(33.66222 AS Decimal(10, 5)), CAST(135.36444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9365, CAST(29.53924 AS Decimal(10, 5)), CAST(52.58979 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9368, CAST(66.25306 AS Decimal(10, 5)), CAST(-166.07927 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9372, CAST(38.81222 AS Decimal(10, 5)), CAST(139.78722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9374, CAST(34.26532 AS Decimal(10, 5)), CAST(-110.00511 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9375, CAST(32.49696 AS Decimal(10, 5)), CAST(-93.66762 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9378, CAST(32.45416 AS Decimal(10, 5)), CAST(-93.82912 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9380, CAST(66.88497 AS Decimal(10, 5)), CAST(-157.15409 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9382, CAST(32.53556 AS Decimal(10, 5)), CAST(74.36389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9390, CAST(45.78560 AS Decimal(10, 5)), CAST(24.09134 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9391, CAST(2.26160 AS Decimal(10, 5)), CAST(111.98532 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9392, CAST(11.46000 AS Decimal(10, 5)), CAST(123.25100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9396, CAST(47.70668 AS Decimal(10, 5)), CAST(-104.19272 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9402, CAST(13.41067 AS Decimal(10, 5)), CAST(103.81284 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9414, CAST(10.57970 AS Decimal(10, 5)), CAST(103.63700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9419, CAST(24.91290 AS Decimal(10, 5)), CAST(92.97870 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9426, CAST(32.63647 AS Decimal(10, 5)), CAST(-108.15643 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9427, CAST(16.72530 AS Decimal(10, 5)), CAST(-88.34000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9431, CAST(22.79330 AS Decimal(10, 5)), CAST(100.95900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9432, CAST(27.15946 AS Decimal(10, 5)), CAST(84.98012 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9435, CAST(-2.66667 AS Decimal(10, 5)), CAST(152.00000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9437, CAST(45.05222 AS Decimal(10, 5)), CAST(33.97514 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9438, CAST(29.97110 AS Decimal(10, 5)), CAST(81.81890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9439, CAST(31.08180 AS Decimal(10, 5)), CAST(77.06800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9444, CAST(1.35514 AS Decimal(10, 5)), CAST(103.99006 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9447, CAST(1.41722 AS Decimal(10, 5)), CAST(103.86583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9455, CAST(-11.88500 AS Decimal(10, 5)), CAST(-55.58611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9458, CAST(0.06362 AS Decimal(10, 5)), CAST(111.47343 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9460, CAST(46.21959 AS Decimal(10, 5)), CAST(7.32676 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9461, CAST(42.40300 AS Decimal(10, 5)), CAST(-96.37992 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9462, CAST(43.58136 AS Decimal(10, 5)), CAST(-96.74172 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9463, CAST(50.11389 AS Decimal(10, 5)), CAST(-91.90528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9469, CAST(29.55039 AS Decimal(10, 5)), CAST(55.66526 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9471, CAST(-27.64861 AS Decimal(10, 5)), CAST(22.99928 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9472, CAST(66.95150 AS Decimal(10, 5)), CAST(-53.72263 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9479, CAST(35.21610 AS Decimal(10, 5)), CAST(26.10130 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9481, CAST(57.04718 AS Decimal(10, 5)), CAST(-135.36163 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9483, CAST(20.13271 AS Decimal(10, 5)), CAST(92.87263 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9484, CAST(13.72722 AS Decimal(10, 5)), CAST(-84.77778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9485, CAST(39.81383 AS Decimal(10, 5)), CAST(36.90350 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9490, CAST(59.46038 AS Decimal(10, 5)), CAST(-135.31663 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9491, CAST(35.33550 AS Decimal(10, 5)), CAST(75.53600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9493, CAST(64.62477 AS Decimal(10, 5)), CAST(21.07689 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9495, CAST(39.17710 AS Decimal(10, 5)), CAST(23.50368 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9499, CAST(38.96760 AS Decimal(10, 5)), CAST(24.48720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9504, CAST(41.96162 AS Decimal(10, 5)), CAST(21.62138 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9506, CAST(58.45640 AS Decimal(10, 5)), CAST(13.97267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9507, CAST(-24.96279 AS Decimal(10, 5)), CAST(31.58964 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9512, CAST(61.70917 AS Decimal(10, 5)), CAST(-157.15556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9513, CAST(48.63806 AS Decimal(10, 5)), CAST(19.13417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9522, CAST(54.82472 AS Decimal(10, 5)), CAST(-127.18278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9524, CAST(36.01144 AS Decimal(10, 5)), CAST(-86.51227 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9527, CAST(64.19166 AS Decimal(10, 5)), CAST(-114.07911 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9543, CAST(42.69519 AS Decimal(10, 5)), CAST(23.40617 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9545, CAST(61.15000 AS Decimal(10, 5)), CAST(7.13333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9549, CAST(12.91632 AS Decimal(10, 5)), CAST(5.20719 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9550, CAST(-13.85437 AS Decimal(10, 5)), CAST(167.53730 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9557, CAST(-7.51462 AS Decimal(10, 5)), CAST(110.74962 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9560, CAST(65.03117 AS Decimal(10, 5)), CAST(35.72273 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9563, CAST(-12.18333 AS Decimal(10, 5)), CAST(26.38333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9565, CAST(54.96437 AS Decimal(10, 5)), CAST(9.79173 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9568, CAST(-10.68333 AS Decimal(10, 5)), CAST(35.58333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9575, CAST(69.78619 AS Decimal(10, 5)), CAST(20.96088 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9578, CAST(-0.89398 AS Decimal(10, 5)), CAST(131.28499 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9587, CAST(41.70118 AS Decimal(10, 5)), CAST(-86.31224 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9588, CAST(21.51570 AS Decimal(10, 5)), CAST(-71.52850 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9590, CAST(56.79280 AS Decimal(10, 5)), CAST(-98.90720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9593, CAST(58.70424 AS Decimal(10, 5)), CAST(-157.00655 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9595, CAST(-16.48646 AS Decimal(10, 5)), CAST(167.44726 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9597, CAST(50.95026 AS Decimal(10, 5)), CAST(-1.35680 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9603, CAST(61.32662 AS Decimal(10, 5)), CAST(63.60191 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9604, CAST(-6.14109 AS Decimal(10, 5)), CAST(12.37180 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9618, CAST(43.53894 AS Decimal(10, 5)), CAST(16.29796 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9621, CAST(47.61986 AS Decimal(10, 5)), CAST(-117.53384 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9625, CAST(22.44180 AS Decimal(10, 5)), CAST(-73.97090 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9628, CAST(39.84410 AS Decimal(10, 5)), CAST(-89.67789 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9633, CAST(37.24421 AS Decimal(10, 5)), CAST(-93.38686 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9640, CAST(33.98710 AS Decimal(10, 5)), CAST(74.77420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9642, CAST(51.39150 AS Decimal(10, 5)), CAST(-56.08606 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9644, CAST(29.95782 AS Decimal(10, 5)), CAST(-81.34198 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9650, CAST(17.69941 AS Decimal(10, 5)), CAST(-64.79588 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9653, CAST(17.74844 AS Decimal(10, 5)), CAST(-64.70093 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9654, CAST(-20.88710 AS Decimal(10, 5)), CAST(55.51031 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9661, CAST(-28.04970 AS Decimal(10, 5)), CAST(148.59500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9662, CAST(56.57766 AS Decimal(10, 5)), CAST(-169.66302 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9671, CAST(47.61600 AS Decimal(10, 5)), CAST(-52.74313 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9673, CAST(17.31119 AS Decimal(10, 5)), CAST(-62.71867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9678, CAST(38.74321 AS Decimal(10, 5)), CAST(-90.36591 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9683, CAST(13.73319 AS Decimal(10, 5)), CAST(-60.95260 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9685, CAST(14.02023 AS Decimal(10, 5)), CAST(-60.99294 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9696, CAST(63.49020 AS Decimal(10, 5)), CAST(-162.11010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9699, CAST(47.31219 AS Decimal(10, 5)), CAST(-2.14918 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9708, CAST(59.80029 AS Decimal(10, 5)), CAST(30.26250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9710, CAST(46.76271 AS Decimal(10, 5)), CAST(-56.17528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9711, CAST(-21.32004 AS Decimal(10, 5)), CAST(55.42358 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9716, CAST(18.33523 AS Decimal(10, 5)), CAST(-64.97292 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9718, CAST(18.33635 AS Decimal(10, 5)), CAST(-64.94015 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9719, CAST(13.14486 AS Decimal(10, 5)), CAST(-61.20972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9729, CAST(40.84867 AS Decimal(10, 5)), CAST(-77.84917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9733, CAST(38.26292 AS Decimal(10, 5)), CAST(-78.90202 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9735, CAST(58.87678 AS Decimal(10, 5)), CAST(5.63786 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9736, CAST(45.10916 AS Decimal(10, 5)), CAST(42.11278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9738, CAST(53.84560 AS Decimal(10, 5)), CAST(-94.85190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9741, CAST(63.51490 AS Decimal(10, 5)), CAST(-162.28568 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9744, CAST(23.58169 AS Decimal(10, 5)), CAST(-75.26652 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9747, CAST(48.54417 AS Decimal(10, 5)), CAST(-58.55000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9753, CAST(66.01460 AS Decimal(10, 5)), CAST(-149.05983 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9756, CAST(36.16122 AS Decimal(10, 5)), CAST(-97.08569 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9760, CAST(59.65200 AS Decimal(10, 5)), CAST(17.93132 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9761, CAST(59.35538 AS Decimal(10, 5)), CAST(17.94561 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9764, CAST(58.78864 AS Decimal(10, 5)), CAST(16.91219 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9765, CAST(59.58944 AS Decimal(10, 5)), CAST(16.63361 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9769, CAST(68.58333 AS Decimal(10, 5)), CAST(15.01667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9771, CAST(59.25140 AS Decimal(10, 5)), CAST(-105.83679 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9772, CAST(61.78725 AS Decimal(10, 5)), CAST(-156.58997 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9773, CAST(59.79192 AS Decimal(10, 5)), CAST(5.34085 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9777, CAST(58.21556 AS Decimal(10, 5)), CAST(-6.33111 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9780, CAST(44.53734 AS Decimal(10, 5)), CAST(-72.61528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9786, CAST(48.53832 AS Decimal(10, 5)), CAST(7.62823 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9795, CAST(59.15530 AS Decimal(10, 5)), CAST(-2.64139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9810, CAST(48.69034 AS Decimal(10, 5)), CAST(9.19521 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9816, CAST(-7.58556 AS Decimal(10, 5)), CAST(158.73100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9817, CAST(14.79445 AS Decimal(10, 5)), CAST(120.27136 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9818, CAST(47.68750 AS Decimal(10, 5)), CAST(26.35406 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9819, CAST(-19.00708 AS Decimal(10, 5)), CAST(-65.28875 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9821, CAST(46.62500 AS Decimal(10, 5)), CAST(-80.79889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9825, CAST(-10.20830 AS Decimal(10, 5)), CAST(142.82500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9832, CAST(17.23800 AS Decimal(10, 5)), CAST(99.81820 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9835, CAST(27.72194 AS Decimal(10, 5)), CAST(68.79194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9838, CAST(35.56175 AS Decimal(10, 5)), CAST(45.31674 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9846, CAST(-8.48876 AS Decimal(10, 5)), CAST(117.41175 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9849, CAST(-7.02425 AS Decimal(10, 5)), CAST(113.89021 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9850, CAST(52.70860 AS Decimal(10, 5)), CAST(-88.54190 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9858, CAST(-25.33380 AS Decimal(10, 5)), CAST(27.17340 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9864, CAST(62.52813 AS Decimal(10, 5)), CAST(17.44393 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9867, CAST(-26.60330 AS Decimal(10, 5)), CAST(153.09100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9871, CAST(-7.38004 AS Decimal(10, 5)), CAST(112.78509 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9872, CAST(21.11410 AS Decimal(10, 5)), CAST(72.74180 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9873, CAST(9.13260 AS Decimal(10, 5)), CAST(99.13559 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9876, CAST(61.34369 AS Decimal(10, 5)), CAST(73.40184 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9878, CAST(9.76087 AS Decimal(10, 5)), CAST(125.48110 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9880, CAST(28.60000 AS Decimal(10, 5)), CAST(81.61667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9885, CAST(-18.04327 AS Decimal(10, 5)), CAST(178.55923 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9888, CAST(31.26310 AS Decimal(10, 5)), CAST(120.40100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9891, CAST(62.05000 AS Decimal(10, 5)), CAST(14.43333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9893, CAST(68.24503 AS Decimal(10, 5)), CAST(14.66958 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9898, CAST(51.60533 AS Decimal(10, 5)), CAST(-4.06783 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9906, CAST(-33.94611 AS Decimal(10, 5)), CAST(151.17722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9910, CAST(46.16139 AS Decimal(10, 5)), CAST(-60.04778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9911, CAST(61.64705 AS Decimal(10, 5)), CAST(50.84505 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9912, CAST(24.96135 AS Decimal(10, 5)), CAST(91.87092 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9914, CAST(43.11119 AS Decimal(10, 5)), CAST(-76.10631 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9916, CAST(37.42279 AS Decimal(10, 5)), CAST(24.95094 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9918, CAST(53.58473 AS Decimal(10, 5)), CAST(14.90221 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9921, CAST(53.48333 AS Decimal(10, 5)), CAST(20.93333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9926, CAST(33.66775 AS Decimal(10, 5)), CAST(56.89267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9927, CAST(-4.25567 AS Decimal(10, 5)), CAST(-69.93583 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9931, CAST(12.31195 AS Decimal(10, 5)), CAST(122.08008 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9934, CAST(-5.08333 AS Decimal(10, 5)), CAST(32.83333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9936, CAST(38.13340 AS Decimal(10, 5)), CAST(46.23420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9938, CAST(-5.27921 AS Decimal(10, 5)), CAST(141.22697 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9939, CAST(28.36542 AS Decimal(10, 5)), CAST(36.61889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9940, CAST(46.67250 AS Decimal(10, 5)), CAST(83.34080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9941, CAST(20.48380 AS Decimal(10, 5)), CAST(99.93540 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9942, CAST(11.22763 AS Decimal(10, 5)), CAST(125.02776 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9943, CAST(-18.05306 AS Decimal(10, 5)), CAST(-70.27556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9950, CAST(58.70610 AS Decimal(10, 5)), CAST(-98.51220 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9952, CAST(9.66570 AS Decimal(10, 5)), CAST(123.85330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9963, CAST(24.26250 AS Decimal(10, 5)), CAST(120.62694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9964, CAST(21.48342 AS Decimal(10, 5)), CAST(40.54433 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9965, CAST(22.95036 AS Decimal(10, 5)), CAST(120.20578 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9967, CAST(25.06405 AS Decimal(10, 5)), CAST(121.55022 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9968, CAST(25.07773 AS Decimal(10, 5)), CAST(121.23282 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9971, CAST(22.75499 AS Decimal(10, 5)), CAST(121.10168 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9972, CAST(37.74690 AS Decimal(10, 5)), CAST(112.62843 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9976, CAST(34.21417 AS Decimal(10, 5)), CAST(134.01556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9977, CAST(-14.70950 AS Decimal(10, 5)), CAST(-145.24600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9978, CAST(-14.45621 AS Decimal(10, 5)), CAST(-145.02631 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9980, CAST(4.89606 AS Decimal(10, 5)), CAST(-1.77476 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9981, CAST(62.99306 AS Decimal(10, 5)), CAST(-156.06596 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9984, CAST(-4.57664 AS Decimal(10, 5)), CAST(-81.25414 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9988, CAST(45.12620 AS Decimal(10, 5)), CAST(78.44700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9993, CAST(30.39653 AS Decimal(10, 5)), CAST(-84.35033 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (9996, CAST(69.54667 AS Decimal(10, 5)), CAST(-93.57667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10000, CAST(9.55719 AS Decimal(10, 5)), CAST(-0.86321 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10003, CAST(22.81146 AS Decimal(10, 5)), CAST(5.45108 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10004, CAST(10.31495 AS Decimal(10, 5)), CAST(-85.81541 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10005, CAST(-18.10952 AS Decimal(10, 5)), CAST(49.39254 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10009, CAST(-9.40980 AS Decimal(10, 5)), CAST(119.24620 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10010, CAST(9.73852 AS Decimal(10, 5)), CAST(-85.01380 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10011, CAST(52.80610 AS Decimal(10, 5)), CAST(41.48280 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10013, CAST(6.45111 AS Decimal(10, 5)), CAST(-71.75972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10015, CAST(15.40553 AS Decimal(10, 5)), CAST(108.70594 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10016, CAST(27.97558 AS Decimal(10, 5)), CAST(-82.53287 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10020, CAST(27.91017 AS Decimal(10, 5)), CAST(-82.68739 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10023, CAST(61.41415 AS Decimal(10, 5)), CAST(23.60439 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10024, CAST(22.29144 AS Decimal(10, 5)), CAST(-97.86630 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10026, CAST(-31.08389 AS Decimal(10, 5)), CAST(150.84667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10027, CAST(28.44819 AS Decimal(10, 5)), CAST(-11.16135 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10029, CAST(-3.04504 AS Decimal(10, 5)), CAST(119.82179 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10032, CAST(-6.09707 AS Decimal(10, 5)), CAST(140.30322 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10034, CAST(65.17417 AS Decimal(10, 5)), CAST(-152.10917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10038, CAST(9.07242 AS Decimal(10, 5)), CAST(126.17059 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10040, CAST(30.60507 AS Decimal(10, 5)), CAST(130.99123 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10041, CAST(-5.09308 AS Decimal(10, 5)), CAST(39.07253 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10043, CAST(-14.66000 AS Decimal(10, 5)), CAST(-57.45000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10044, CAST(35.72692 AS Decimal(10, 5)), CAST(-5.91689 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10046, CAST(-2.75016 AS Decimal(10, 5)), CAST(107.75485 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10048, CAST(0.92400 AS Decimal(10, 5)), CAST(104.53300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10050, CAST(2.83641 AS Decimal(10, 5)), CAST(117.37400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10052, CAST(-19.45510 AS Decimal(10, 5)), CAST(169.22400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10056, CAST(14.79434 AS Decimal(10, 5)), CAST(-92.37002 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10063, CAST(3.32622 AS Decimal(10, 5)), CAST(117.56714 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10065, CAST(24.65389 AS Decimal(10, 5)), CAST(124.67528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10067, CAST(-2.89500 AS Decimal(10, 5)), CAST(-69.74700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10070, CAST(-6.50874 AS Decimal(10, 5)), CAST(-76.37325 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10072, CAST(1.38164 AS Decimal(10, 5)), CAST(173.14704 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10076, CAST(-31.88860 AS Decimal(10, 5)), CAST(152.51401 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10079, CAST(-5.84500 AS Decimal(10, 5)), CAST(142.94800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10080, CAST(-21.55574 AS Decimal(10, 5)), CAST(-64.70132 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10082, CAST(-25.80175 AS Decimal(10, 5)), CAST(149.89802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10086, CAST(58.30746 AS Decimal(10, 5)), CAST(26.69043 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10088, CAST(41.25786 AS Decimal(10, 5)), CAST(69.28119 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10090, CAST(-7.33333 AS Decimal(10, 5)), CAST(108.25000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10092, CAST(58.66672 AS Decimal(10, 5)), CAST(-69.95617 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10096, CAST(62.89417 AS Decimal(10, 5)), CAST(-155.97639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10100, CAST(-38.73823 AS Decimal(10, 5)), CAST(176.08021 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10102, CAST(-37.67194 AS Decimal(10, 5)), CAST(176.19611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10104, CAST(-16.69079 AS Decimal(10, 5)), CAST(-179.87683 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10106, CAST(4.31337 AS Decimal(10, 5)), CAST(118.12195 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10107, CAST(5.04699 AS Decimal(10, 5)), CAST(119.74300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10110, CAST(11.05000 AS Decimal(10, 5)), CAST(119.51900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10111, CAST(35.43161 AS Decimal(10, 5)), CAST(8.12072 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10112, CAST(41.66951 AS Decimal(10, 5)), CAST(44.95460 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10115, CAST(-45.53227 AS Decimal(10, 5)), CAST(167.64304 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10116, CAST(-3.38294 AS Decimal(10, 5)), CAST(-64.72406 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10117, CAST(14.06088 AS Decimal(10, 5)), CAST(-87.21720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10119, CAST(35.41102 AS Decimal(10, 5)), CAST(51.15582 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10120, CAST(35.68920 AS Decimal(10, 5)), CAST(51.31330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10123, CAST(-17.52473 AS Decimal(10, 5)), CAST(-39.66983 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10126, CAST(41.13825 AS Decimal(10, 5)), CAST(27.91909 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10127, CAST(32.01361 AS Decimal(10, 5)), CAST(34.88657 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10129, CAST(32.11472 AS Decimal(10, 5)), CAST(34.78222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10135, CAST(-21.71500 AS Decimal(10, 5)), CAST(122.22900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10137, CAST(65.24082 AS Decimal(10, 5)), CAST(-166.33048 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10138, CAST(65.33088 AS Decimal(10, 5)), CAST(-166.47362 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10139, CAST(37.95360 AS Decimal(10, 5)), CAST(-107.90895 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10142, CAST(-4.52828 AS Decimal(10, 5)), CAST(136.88699 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10146, CAST(-38.76682 AS Decimal(10, 5)), CAST(-72.63710 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10147, CAST(57.77873 AS Decimal(10, 5)), CAST(-135.21888 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10149, CAST(28.48265 AS Decimal(10, 5)), CAST(-16.34154 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10150, CAST(28.04447 AS Decimal(10, 5)), CAST(-16.57249 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10154, CAST(-19.63333 AS Decimal(10, 5)), CAST(134.16667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10156, CAST(21.41945 AS Decimal(10, 5)), CAST(-104.84258 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10161, CAST(38.76184 AS Decimal(10, 5)), CAST(-27.09080 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10162, CAST(-5.05994 AS Decimal(10, 5)), CAST(-42.82348 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10164, CAST(37.28667 AS Decimal(10, 5)), CAST(67.31000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10166, CAST(0.83141 AS Decimal(10, 5)), CAST(127.38149 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10168, CAST(54.46851 AS Decimal(10, 5)), CAST(-128.57622 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10173, CAST(15.86447 AS Decimal(10, 5)), CAST(-61.58000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10180, CAST(-16.10482 AS Decimal(10, 5)), CAST(33.64018 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10182, CAST(50.67440 AS Decimal(10, 5)), CAST(-59.38360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10183, CAST(40.85010 AS Decimal(10, 5)), CAST(-74.06042 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10186, CAST(35.59433 AS Decimal(10, 5)), CAST(-5.32002 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10188, CAST(33.45346 AS Decimal(10, 5)), CAST(-93.99102 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10189, CAST(26.70910 AS Decimal(10, 5)), CAST(92.78470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10197, CAST(18.46038 AS Decimal(10, 5)), CAST(94.30001 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10198, CAST(-24.49390 AS Decimal(10, 5)), CAST(150.57600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10200, CAST(-27.98677 AS Decimal(10, 5)), CAST(143.81554 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10201, CAST(24.31425 AS Decimal(10, 5)), CAST(-75.45882 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10205, CAST(53.97139 AS Decimal(10, 5)), CAST(-101.09111 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10210, CAST(33.62780 AS Decimal(10, 5)), CAST(-116.16056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10212, CAST(40.51973 AS Decimal(10, 5)), CAST(22.97095 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10214, CAST(48.06615 AS Decimal(10, 5)), CAST(-96.17980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10218, CAST(36.39917 AS Decimal(10, 5)), CAST(25.47933 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10220, CAST(8.48212 AS Decimal(10, 5)), CAST(76.92011 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10224, CAST(55.80111 AS Decimal(10, 5)), CAST(-97.86417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10228, CAST(66.21830 AS Decimal(10, 5)), CAST(-15.33470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10234, CAST(48.37194 AS Decimal(10, 5)), CAST(-89.32389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10237, CAST(39.13016 AS Decimal(10, 5)), CAST(117.35287 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10240, CAST(35.34114 AS Decimal(10, 5)), CAST(1.46315 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10249, CAST(-21.09610 AS Decimal(10, 5)), CAST(167.80400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10251, CAST(32.54334 AS Decimal(10, 5)), CAST(-116.97375 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10255, CAST(-15.11971 AS Decimal(10, 5)), CAST(-148.23113 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10258, CAST(71.69767 AS Decimal(10, 5)), CAST(128.90302 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10261, CAST(-44.29835 AS Decimal(10, 5)), CAST(171.22010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10268, CAST(29.23712 AS Decimal(10, 5)), CAST(0.27603 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10269, CAST(45.80986 AS Decimal(10, 5)), CAST(21.33786 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10270, CAST(48.56920 AS Decimal(10, 5)), CAST(-81.37479 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10271, CAST(65.56306 AS Decimal(10, 5)), CAST(-167.92139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10274, CAST(27.70037 AS Decimal(10, 5)), CAST(-8.16710 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10275, CAST(-9.28708 AS Decimal(10, 5)), CAST(-76.00477 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10278, CAST(14.99984 AS Decimal(10, 5)), CAST(145.62387 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10284, CAST(41.42190 AS Decimal(10, 5)), CAST(19.71296 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10285, CAST(56.49917 AS Decimal(10, 5)), CAST(-6.86917 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10286, CAST(46.46771 AS Decimal(10, 5)), CAST(24.41252 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10287, CAST(32.60412 AS Decimal(10, 5)), CAST(65.86595 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10288, CAST(10.76536 AS Decimal(10, 5)), CAST(78.70972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10289, CAST(13.63250 AS Decimal(10, 5)), CAST(79.54330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10292, CAST(42.40472 AS Decimal(10, 5)), CAST(18.72278 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10295, CAST(35.01667 AS Decimal(10, 5)), CAST(-1.45000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10297, CAST(11.14966 AS Decimal(10, 5)), CAST(-60.83219 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10300, CAST(31.86100 AS Decimal(10, 5)), CAST(23.90700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10307, CAST(49.07977 AS Decimal(10, 5)), CAST(-125.77775 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10309, CAST(59.05278 AS Decimal(10, 5)), CAST(-160.39667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10311, CAST(63.30769 AS Decimal(10, 5)), CAST(-143.01307 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10315, CAST(60.53778 AS Decimal(10, 5)), CAST(-165.08802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10316, CAST(27.83638 AS Decimal(10, 5)), CAST(128.88125 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10317, CAST(34.13281 AS Decimal(10, 5)), CAST(134.60664 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10318, CAST(35.55226 AS Decimal(10, 5)), CAST(139.77969 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10320, CAST(35.76472 AS Decimal(10, 5)), CAST(140.38639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10325, CAST(41.59247 AS Decimal(10, 5)), CAST(-83.80606 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10329, CAST(1.12240 AS Decimal(10, 5)), CAST(120.79570 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10330, CAST(9.50945 AS Decimal(10, 5)), CAST(-75.58540 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10336, CAST(56.38661 AS Decimal(10, 5)), CAST(85.21069 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10337, CAST(42.25476 AS Decimal(10, 5)), CAST(125.70587 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10338, CAST(43.55670 AS Decimal(10, 5)), CAST(122.20000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10339, CAST(-16.89110 AS Decimal(10, 5)), CAST(168.55099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10340, CAST(27.88369 AS Decimal(10, 5)), CAST(109.30813 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10355, CAST(43.17094 AS Decimal(10, 5)), CAST(-79.92914 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10357, CAST(43.68066 AS Decimal(10, 5)), CAST(-79.61286 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10358, CAST(43.62974 AS Decimal(10, 5)), CAST(-79.39828 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10363, CAST(25.56828 AS Decimal(10, 5)), CAST(-103.41058 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10365, CAST(-13.32855 AS Decimal(10, 5)), CAST(166.64086 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10367, CAST(60.15438 AS Decimal(10, 5)), CAST(12.99629 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10372, CAST(10.56900 AS Decimal(10, 5)), CAST(-83.51480 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10377, CAST(35.53007 AS Decimal(10, 5)), CAST(134.16655 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10380, CAST(33.06780 AS Decimal(10, 5)), CAST(6.08867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10381, CAST(-20.79000 AS Decimal(10, 5)), CAST(165.25900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10382, CAST(43.09734 AS Decimal(10, 5)), CAST(6.14603 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10384, CAST(43.62908 AS Decimal(10, 5)), CAST(1.36382 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10392, CAST(47.43222 AS Decimal(10, 5)), CAST(0.72761 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10394, CAST(-19.25250 AS Decimal(10, 5)), CAST(146.76528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10395, CAST(36.64833 AS Decimal(10, 5)), CAST(137.18750 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10396, CAST(35.51278 AS Decimal(10, 5)), CAST(134.78694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10397, CAST(33.93972 AS Decimal(10, 5)), CAST(8.11056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10398, CAST(40.99511 AS Decimal(10, 5)), CAST(39.78973 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10399, CAST(49.05685 AS Decimal(10, 5)), CAST(-117.60793 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10400, CAST(7.50874 AS Decimal(10, 5)), CAST(99.61658 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10401, CAST(37.91140 AS Decimal(10, 5)), CAST(12.48796 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10403, CAST(12.27471 AS Decimal(10, 5)), CAST(102.31890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10404, CAST(44.73720 AS Decimal(10, 5)), CAST(-85.57969 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10405, CAST(26.74055 AS Decimal(10, 5)), CAST(-77.38358 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10408, CAST(-43.21050 AS Decimal(10, 5)), CAST(-65.27032 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10418, CAST(45.82279 AS Decimal(10, 5)), CAST(13.48464 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10419, CAST(8.53851 AS Decimal(10, 5)), CAST(81.18185 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10420, CAST(-14.81874 AS Decimal(10, 5)), CAST(-64.91802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10426, CAST(32.66354 AS Decimal(10, 5)), CAST(13.15901 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10429, CAST(58.31806 AS Decimal(10, 5)), CAST(12.34500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10431, CAST(69.68333 AS Decimal(10, 5)), CAST(18.91667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10434, CAST(63.45756 AS Decimal(10, 5)), CAST(10.92425 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10438, CAST(39.31991 AS Decimal(10, 5)), CAST(-120.14047 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10440, CAST(-8.08141 AS Decimal(10, 5)), CAST(-79.10876 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10441, CAST(7.46102 AS Decimal(10, 5)), CAST(151.84231 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10457, CAST(34.28489 AS Decimal(10, 5)), CAST(129.33055 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10461, CAST(-23.36535 AS Decimal(10, 5)), CAST(-149.52407 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10464, CAST(32.11619 AS Decimal(10, 5)), CAST(-110.94176 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10467, CAST(-26.84086 AS Decimal(10, 5)), CAST(-65.10494 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10469, CAST(9.08899 AS Decimal(10, 5)), CAST(-62.09417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10472, CAST(-9.07595 AS Decimal(10, 5)), CAST(149.31984 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10473, CAST(17.64868 AS Decimal(10, 5)), CAST(121.73255 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10474, CAST(69.43333 AS Decimal(10, 5)), CAST(-133.02639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10479, CAST(45.06249 AS Decimal(10, 5)), CAST(28.71431 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10480, CAST(-23.38337 AS Decimal(10, 5)), CAST(43.72845 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10482, CAST(64.90950 AS Decimal(10, 5)), CAST(-125.57124 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10487, CAST(36.19839 AS Decimal(10, 5)), CAST(-95.88811 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10493, CAST(61.09676 AS Decimal(10, 5)), CAST(-160.96842 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10496, CAST(1.81442 AS Decimal(10, 5)), CAST(-78.74920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10498, CAST(-3.55253 AS Decimal(10, 5)), CAST(-80.38136 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10501, CAST(27.31500 AS Decimal(10, 5)), CAST(87.19330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10506, CAST(36.85103 AS Decimal(10, 5)), CAST(10.22722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10507, CAST(60.33535 AS Decimal(10, 5)), CAST(-162.66701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10508, CAST(60.57560 AS Decimal(10, 5)), CAST(-165.27313 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10509, CAST(29.73053 AS Decimal(10, 5)), CAST(118.25345 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10511, CAST(34.26637 AS Decimal(10, 5)), CAST(-88.76740 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10514, CAST(31.69194 AS Decimal(10, 5)), CAST(38.73417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10515, CAST(25.98639 AS Decimal(10, 5)), CAST(63.02833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10519, CAST(45.20076 AS Decimal(10, 5)), CAST(7.64963 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10521, CAST(39.08330 AS Decimal(10, 5)), CAST(63.61330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10522, CAST(40.06330 AS Decimal(10, 5)), CAST(53.00720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10523, CAST(60.51414 AS Decimal(10, 5)), CAST(22.26281 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10525, CAST(33.22040 AS Decimal(10, 5)), CAST(-87.61140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10527, CAST(8.72308 AS Decimal(10, 5)), CAST(78.02731 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10529, CAST(16.56182 AS Decimal(10, 5)), CAST(-93.02608 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10530, CAST(13.04424 AS Decimal(10, 5)), CAST(109.33856 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10531, CAST(44.45866 AS Decimal(10, 5)), CAST(18.72478 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10533, CAST(42.48158 AS Decimal(10, 5)), CAST(-114.48754 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10534, CAST(59.07562 AS Decimal(10, 5)), CAST(-160.27304 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10535, CAST(32.35404 AS Decimal(10, 5)), CAST(-95.40245 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10536, CAST(55.28420 AS Decimal(10, 5)), CAST(124.77900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10539, CAST(57.18960 AS Decimal(10, 5)), CAST(65.32430 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10541, CAST(-8.93631 AS Decimal(10, 5)), CAST(-139.55387 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10542, CAST(-9.34742 AS Decimal(10, 5)), CAST(-140.07964 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10544, CAST(26.56750 AS Decimal(10, 5)), CAST(12.82310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10546, CAST(33.93000 AS Decimal(10, 5)), CAST(131.27861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10547, CAST(-19.76500 AS Decimal(10, 5)), CAST(-47.96478 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10548, CAST(-18.88284 AS Decimal(10, 5)), CAST(-48.22559 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10549, CAST(15.25128 AS Decimal(10, 5)), CAST(104.87023 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10551, CAST(24.61784 AS Decimal(10, 5)), CAST(73.89662 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10555, CAST(17.38644 AS Decimal(10, 5)), CAST(102.78825 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10557, CAST(54.56373 AS Decimal(10, 5)), CAST(55.88037 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10560, CAST(49.02944 AS Decimal(10, 5)), CAST(17.43972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10563, CAST(8.92944 AS Decimal(10, 5)), CAST(165.76496 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10564, CAST(-5.07671 AS Decimal(10, 5)), CAST(119.54885 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10565, CAST(63.56690 AS Decimal(10, 5)), CAST(53.80470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10567, CAST(-4.29333 AS Decimal(10, 5)), CAST(39.57110 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10568, CAST(47.84306 AS Decimal(10, 5)), CAST(106.76664 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10569, CAST(49.96887 AS Decimal(10, 5)), CAST(92.08054 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10570, CAST(46.08300 AS Decimal(10, 5)), CAST(122.01700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10571, CAST(51.80776 AS Decimal(10, 5)), CAST(107.43764 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10572, CAST(-16.32917 AS Decimal(10, 5)), CAST(168.30042 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10573, CAST(48.99390 AS Decimal(10, 5)), CAST(89.92181 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10574, CAST(47.71470 AS Decimal(10, 5)), CAST(96.84720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10577, CAST(35.59349 AS Decimal(10, 5)), CAST(129.35172 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10578, CAST(70.76278 AS Decimal(10, 5)), CAST(-117.80611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10580, CAST(-24.78540 AS Decimal(10, 5)), CAST(31.35490 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10581, CAST(54.40102 AS Decimal(10, 5)), CAST(48.80266 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10583, CAST(63.79183 AS Decimal(10, 5)), CAST(20.28276 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10585, CAST(56.53909 AS Decimal(10, 5)), CAST(-76.51939 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10591, CAST(-31.54790 AS Decimal(10, 5)), CAST(28.67429 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10593, CAST(-15.35740 AS Decimal(10, 5)), CAST(-38.99822 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10594, CAST(63.88830 AS Decimal(10, 5)), CAST(-160.79984 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10609, CAST(-28.39910 AS Decimal(10, 5)), CAST(21.26024 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10616, CAST(60.10330 AS Decimal(10, 5)), CAST(64.82670 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10617, CAST(51.15083 AS Decimal(10, 5)), CAST(51.54306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10618, CAST(59.56140 AS Decimal(10, 5)), CAST(-108.48100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10619, CAST(41.58427 AS Decimal(10, 5)), CAST(60.64171 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10623, CAST(37.66812 AS Decimal(10, 5)), CAST(45.06870 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10626, CAST(19.39669 AS Decimal(10, 5)), CAST(-102.03906 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10628, CAST(-29.78218 AS Decimal(10, 5)), CAST(-57.03819 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10629, CAST(43.90711 AS Decimal(10, 5)), CAST(87.47424 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10631, CAST(38.68148 AS Decimal(10, 5)), CAST(29.47168 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10633, CAST(-54.84328 AS Decimal(10, 5)), CAST(-68.29575 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10635, CAST(66.00470 AS Decimal(10, 5)), CAST(57.36720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10638, CAST(50.03660 AS Decimal(10, 5)), CAST(82.49420 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10639, CAST(56.85670 AS Decimal(10, 5)), CAST(105.73000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10641, CAST(12.67994 AS Decimal(10, 5)), CAST(101.00503 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10645, CAST(16.11310 AS Decimal(10, 5)), CAST(-86.88030 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10646, CAST(11.22258 AS Decimal(10, 5)), CAST(169.85428 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10660, CAST(63.05065 AS Decimal(10, 5)), CAST(21.76217 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10661, CAST(22.33620 AS Decimal(10, 5)), CAST(73.22630 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10662, CAST(70.06477 AS Decimal(10, 5)), CAST(29.83990 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10667, CAST(39.64249 AS Decimal(10, 5)), CAST(-106.91810 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10670, CAST(48.05018 AS Decimal(10, 5)), CAST(-77.78279 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10674, CAST(61.13389 AS Decimal(10, 5)), CAST(-146.24833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10675, CAST(-39.64996 AS Decimal(10, 5)), CAST(-73.08611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10678, CAST(30.78390 AS Decimal(10, 5)), CAST(-83.27144 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10679, CAST(-13.29650 AS Decimal(10, 5)), CAST(-38.99240 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10682, CAST(39.48931 AS Decimal(10, 5)), CAST(-0.48163 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10683, CAST(10.14961 AS Decimal(10, 5)), CAST(-67.92937 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10687, CAST(-16.79610 AS Decimal(10, 5)), CAST(168.17700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10690, CAST(41.70611 AS Decimal(10, 5)), CAST(-4.85194 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10693, CAST(10.43504 AS Decimal(10, 5)), CAST(-73.24951 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10701, CAST(30.47527 AS Decimal(10, 5)), CAST(-86.51896 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10704, CAST(27.81485 AS Decimal(10, 5)), CAST(-17.88706 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10705, CAST(38.46822 AS Decimal(10, 5)), CAST(43.33230 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10711, CAST(49.19489 AS Decimal(10, 5)), CAST(-123.17923 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10715, CAST(-2.69152 AS Decimal(10, 5)), CAST(141.30150 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10718, CAST(-17.24480 AS Decimal(10, 5)), CAST(-178.95913 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10719, CAST(23.03445 AS Decimal(10, 5)), CAST(-81.43528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10720, CAST(25.45236 AS Decimal(10, 5)), CAST(82.85934 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10722, CAST(70.35539 AS Decimal(10, 5)), CAST(31.04489 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10724, CAST(-21.59010 AS Decimal(10, 5)), CAST(-45.47330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10726, CAST(43.23207 AS Decimal(10, 5)), CAST(27.82511 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10732, CAST(-18.58557 AS Decimal(10, 5)), CAST(-173.96258 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10733, CAST(56.92914 AS Decimal(10, 5)), CAST(14.72799 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10739, CAST(60.78830 AS Decimal(10, 5)), CAST(46.26000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10742, CAST(67.00672 AS Decimal(10, 5)), CAST(-146.38195 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10744, CAST(45.50487 AS Decimal(10, 5)), CAST(12.34138 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10747, CAST(45.64840 AS Decimal(10, 5)), CAST(12.19442 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10749, CAST(19.14593 AS Decimal(10, 5)), CAST(-96.18727 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10755, CAST(40.43885 AS Decimal(10, 5)), CAST(-109.51097 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10757, CAST(27.65275 AS Decimal(10, 5)), CAST(-80.42048 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10758, CAST(45.39571 AS Decimal(10, 5)), CAST(10.88853 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10760, CAST(45.42889 AS Decimal(10, 5)), CAST(10.33056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10778, CAST(48.64721 AS Decimal(10, 5)), CAST(-123.42716 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10782, CAST(28.85277 AS Decimal(10, 5)), CAST(-96.91860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10783, CAST(-18.09588 AS Decimal(10, 5)), CAST(25.83901 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10790, CAST(-40.86922 AS Decimal(10, 5)), CAST(-63.00039 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10793, CAST(48.12210 AS Decimal(10, 5)), CAST(16.55751 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10798, CAST(17.98832 AS Decimal(10, 5)), CAST(102.56326 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10799, CAST(18.13478 AS Decimal(10, 5)), CAST(-65.49035 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10804, CAST(42.23180 AS Decimal(10, 5)), CAST(-8.62678 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10805, CAST(16.53043 AS Decimal(10, 5)), CAST(80.79685 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10807, CAST(41.27433 AS Decimal(10, 5)), CAST(-7.72047 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10810, CAST(-22.01778 AS Decimal(10, 5)), CAST(35.31222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10812, CAST(64.57906 AS Decimal(10, 5)), CAST(16.83432 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10813, CAST(-12.69438 AS Decimal(10, 5)), CAST(-60.09827 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10818, CAST(0.98238 AS Decimal(10, 5)), CAST(-76.60545 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10819, CAST(17.99700 AS Decimal(10, 5)), CAST(-92.81736 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10823, CAST(4.16788 AS Decimal(10, 5)), CAST(-73.61376 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10827, CAST(54.63413 AS Decimal(10, 5)), CAST(25.28577 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10830, CAST(18.73757 AS Decimal(10, 5)), CAST(105.67076 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10832, CAST(49.23990 AS Decimal(10, 5)), CAST(28.61400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10835, CAST(13.57770 AS Decimal(10, 5)), CAST(124.20580 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10836, CAST(18.44586 AS Decimal(10, 5)), CAST(-64.42816 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10839, CAST(57.66280 AS Decimal(10, 5)), CAST(18.34621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10840, CAST(40.72550 AS Decimal(10, 5)), CAST(-7.88899 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10841, CAST(17.72120 AS Decimal(10, 5)), CAST(83.22450 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10843, CAST(55.12650 AS Decimal(10, 5)), CAST(30.34964 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10844, CAST(-20.25806 AS Decimal(10, 5)), CAST(-40.28639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10845, CAST(-14.86276 AS Decimal(10, 5)), CAST(-40.86311 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10846, CAST(42.88284 AS Decimal(10, 5)), CAST(-2.72447 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10850, CAST(43.20510 AS Decimal(10, 5)), CAST(44.60660 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10851, CAST(43.39895 AS Decimal(10, 5)), CAST(132.14802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10858, CAST(48.78253 AS Decimal(10, 5)), CAST(44.34554 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10860, CAST(39.21962 AS Decimal(10, 5)), CAST(22.79434 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10862, CAST(65.72057 AS Decimal(10, 5)), CAST(-14.85060 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10863, CAST(67.48860 AS Decimal(10, 5)), CAST(63.99310 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10864, CAST(51.81421 AS Decimal(10, 5)), CAST(39.22959 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10876, CAST(52.92194 AS Decimal(10, 5)), CAST(-66.86444 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10880, CAST(31.60879 AS Decimal(10, 5)), CAST(-97.22538 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10884, CAST(20.50427 AS Decimal(10, 5)), CAST(45.19956 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10891, CAST(-35.16528 AS Decimal(10, 5)), CAST(147.46639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10899, CAST(-9.66667 AS Decimal(10, 5)), CAST(120.33333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10900, CAST(70.63752 AS Decimal(10, 5)), CAST(-160.01022 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10903, CAST(37.29310 AS Decimal(10, 5)), CAST(136.96201 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10904, CAST(1.73324 AS Decimal(10, 5)), CAST(40.09161 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10909, CAST(45.40417 AS Decimal(10, 5)), CAST(141.80083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10911, CAST(-15.41200 AS Decimal(10, 5)), CAST(167.69099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10914, CAST(65.61704 AS Decimal(10, 5)), CAST(-168.09489 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10917, CAST(46.09430 AS Decimal(10, 5)), CAST(-118.28802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10919, CAST(-13.23828 AS Decimal(10, 5)), CAST(-176.19923 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10923, CAST(-22.97989 AS Decimal(10, 5)), CAST(14.64533 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10924, CAST(-4.09758 AS Decimal(10, 5)), CAST(138.95199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10927, CAST(-39.96255 AS Decimal(10, 5)), CAST(175.02289 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10933, CAST(30.80179 AS Decimal(10, 5)), CAST(108.43270 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10935, CAST(-5.63550 AS Decimal(10, 5)), CAST(143.89236 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10945, CAST(38.73031 AS Decimal(10, 5)), CAST(-93.54786 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10948, CAST(-38.29530 AS Decimal(10, 5)), CAST(142.44701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10950, CAST(52.16575 AS Decimal(10, 5)), CAST(20.96712 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10956, CAST(38.94877 AS Decimal(10, 5)), CAST(-77.44910 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10960, CAST(38.85233 AS Decimal(10, 5)), CAST(-77.03720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10966, CAST(51.47530 AS Decimal(10, 5)), CAST(-78.75489 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10967, CAST(14.73920 AS Decimal(10, 5)), CAST(-83.96940 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10971, CAST(55.29484 AS Decimal(10, 5)), CAST(-133.24575 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10974, CAST(42.55665 AS Decimal(10, 5)), CAST(-92.40019 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10976, CAST(43.99172 AS Decimal(10, 5)), CAST(-76.02131 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10977, CAST(44.91385 AS Decimal(10, 5)), CAST(-97.15452 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (10988, CAST(44.77830 AS Decimal(10, 5)), CAST(-89.66623 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11001, CAST(52.96074 AS Decimal(10, 5)), CAST(-87.37495 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11004, CAST(26.19855 AS Decimal(10, 5)), CAST(36.47638 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11007, CAST(6.25449 AS Decimal(10, 5)), CAST(81.23520 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11008, CAST(36.64670 AS Decimal(10, 5)), CAST(119.11900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11009, CAST(37.18710 AS Decimal(10, 5)), CAST(122.22900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11012, CAST(-12.67861 AS Decimal(10, 5)), CAST(141.92528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11016, CAST(-41.32722 AS Decimal(10, 5)), CAST(174.80528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11020, CAST(53.01083 AS Decimal(10, 5)), CAST(-78.83028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11021, CAST(47.39874 AS Decimal(10, 5)), CAST(-120.20673 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11024, CAST(23.55596 AS Decimal(10, 5)), CAST(104.32497 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11025, CAST(27.91220 AS Decimal(10, 5)), CAST(120.85200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11034, CAST(26.68316 AS Decimal(10, 5)), CAST(-80.09559 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11038, CAST(57.76834 AS Decimal(10, 5)), CAST(-153.54037 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11040, CAST(44.68826 AS Decimal(10, 5)), CAST(-111.11828 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11042, CAST(41.06704 AS Decimal(10, 5)), CAST(-73.70707 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11043, CAST(54.91325 AS Decimal(10, 5)), CAST(8.34047 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11044, CAST(41.34954 AS Decimal(10, 5)), CAST(-71.80340 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11047, CAST(-41.73683 AS Decimal(10, 5)), CAST(171.57941 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11048, CAST(59.35001 AS Decimal(10, 5)), CAST(-2.95037 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11053, CAST(-3.58383 AS Decimal(10, 5)), CAST(143.66919 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11056, CAST(63.13159 AS Decimal(10, 5)), CAST(-117.24764 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11057, CAST(-37.91822 AS Decimal(10, 5)), CAST(176.91022 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11058, CAST(62.24029 AS Decimal(10, 5)), CAST(-92.59767 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11061, CAST(-35.76861 AS Decimal(10, 5)), CAST(174.36500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11067, CAST(64.68795 AS Decimal(10, 5)), CAST(-163.40905 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11074, CAST(60.70955 AS Decimal(10, 5)), CAST(-135.06727 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11078, CAST(-33.05890 AS Decimal(10, 5)), CAST(137.51401 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11084, CAST(37.65724 AS Decimal(10, 5)), CAST(-97.43977 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11087, CAST(33.97440 AS Decimal(10, 5)), CAST(-98.50271 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11088, CAST(58.45889 AS Decimal(10, 5)), CAST(-3.09306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11105, CAST(52.18306 AS Decimal(10, 5)), CAST(-122.05417 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11106, CAST(41.24175 AS Decimal(10, 5)), CAST(-76.92163 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11107, CAST(48.17669 AS Decimal(10, 5)), CAST(-103.63853 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11115, CAST(34.27056 AS Decimal(10, 5)), CAST(-77.90250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11118, CAST(-26.62920 AS Decimal(10, 5)), CAST(120.22100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11122, CAST(-22.60724 AS Decimal(10, 5)), CAST(17.07885 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11123, CAST(-22.48306 AS Decimal(10, 5)), CAST(17.46672 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11127, CAST(-25.41310 AS Decimal(10, 5)), CAST(142.66701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11128, CAST(42.27032 AS Decimal(10, 5)), CAST(-82.96201 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11136, CAST(49.90572 AS Decimal(10, 5)), CAST(-97.23351 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11141, CAST(36.13635 AS Decimal(10, 5)), CAST(-80.22817 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11145, CAST(-22.36360 AS Decimal(10, 5)), CAST(143.08600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11162, CAST(7.45220 AS Decimal(10, 5)), CAST(168.55131 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11164, CAST(48.09446 AS Decimal(10, 5)), CAST(-105.57555 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11167, CAST(58.10690 AS Decimal(10, 5)), CAST(-103.17200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11169, CAST(-34.56044 AS Decimal(10, 5)), CAST(150.78890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11172, CAST(23.36944 AS Decimal(10, 5)), CAST(119.50389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11176, CAST(37.43808 AS Decimal(10, 5)), CAST(127.96038 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11189, CAST(42.26734 AS Decimal(10, 5)), CAST(-71.87571 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11193, CAST(10.17313 AS Decimal(10, 5)), CAST(166.00204 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11194, CAST(9.45931 AS Decimal(10, 5)), CAST(170.23779 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11198, CAST(51.10268 AS Decimal(10, 5)), CAST(16.88584 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11200, CAST(39.79185 AS Decimal(10, 5)), CAST(106.80187 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11203, CAST(30.78376 AS Decimal(10, 5)), CAST(114.20810 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11205, CAST(52.89390 AS Decimal(10, 5)), CAST(-89.28920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11208, CAST(31.49440 AS Decimal(10, 5)), CAST(120.42900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11209, CAST(27.70190 AS Decimal(10, 5)), CAST(118.00100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11220, CAST(34.44712 AS Decimal(10, 5)), CAST(108.75159 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11221, CAST(34.37497 AS Decimal(10, 5)), CAST(109.12239 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11222, CAST(24.54404 AS Decimal(10, 5)), CAST(118.12774 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11223, CAST(32.15060 AS Decimal(10, 5)), CAST(112.29100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11224, CAST(27.98910 AS Decimal(10, 5)), CAST(102.18400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11225, CAST(19.44948 AS Decimal(10, 5)), CAST(103.16158 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11227, CAST(43.91560 AS Decimal(10, 5)), CAST(115.96400 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11233, CAST(25.08502 AS Decimal(10, 5)), CAST(104.95733 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11234, CAST(36.52785 AS Decimal(10, 5)), CAST(102.03677 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11235, CAST(43.43000 AS Decimal(10, 5)), CAST(83.38000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11236, CAST(34.05757 AS Decimal(10, 5)), CAST(117.55483 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11238, CAST(-21.96092 AS Decimal(10, 5)), CAST(-63.65167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11244, CAST(46.56818 AS Decimal(10, 5)), CAST(-120.54368 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11245, CAST(30.38557 AS Decimal(10, 5)), CAST(130.65902 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11246, CAST(59.50306 AS Decimal(10, 5)), CAST(-139.66028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11247, CAST(62.09325 AS Decimal(10, 5)), CAST(129.77067 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11252, CAST(-9.90111 AS Decimal(10, 5)), CAST(142.77600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11253, CAST(38.41189 AS Decimal(10, 5)), CAST(140.37133 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11255, CAST(36.63785 AS Decimal(10, 5)), CAST(109.55223 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11256, CAST(24.14424 AS Decimal(10, 5)), CAST(38.06335 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11257, CAST(33.43034 AS Decimal(10, 5)), CAST(120.20082 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11261, CAST(16.90731 AS Decimal(10, 5)), CAST(96.13322 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11263, CAST(38.06130 AS Decimal(10, 5)), CAST(128.66901 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11264, CAST(42.88280 AS Decimal(10, 5)), CAST(129.45100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11266, CAST(37.40016 AS Decimal(10, 5)), CAST(121.37026 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11268, CAST(3.72256 AS Decimal(10, 5)), CAST(11.55327 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11270, CAST(9.49995 AS Decimal(10, 5)), CAST(138.08595 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11274, CAST(57.56070 AS Decimal(10, 5)), CAST(40.15740 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11277, CAST(30.70048 AS Decimal(10, 5)), CAST(51.54509 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11281, CAST(31.90491 AS Decimal(10, 5)), CAST(54.27650 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11288, CAST(62.46278 AS Decimal(10, 5)), CAST(-114.44028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11291, CAST(34.84233 AS Decimal(10, 5)), CAST(127.61685 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11293, CAST(40.15250 AS Decimal(10, 5)), CAST(44.40222 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11297, CAST(28.80144 AS Decimal(10, 5)), CAST(104.55161 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11298, CAST(30.67100 AS Decimal(10, 5)), CAST(111.44100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11300, CAST(38.32280 AS Decimal(10, 5)), CAST(106.38817 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11301, CAST(43.95507 AS Decimal(10, 5)), CAST(81.32865 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11302, CAST(29.34470 AS Decimal(10, 5)), CAST(120.03200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11305, CAST(-7.78813 AS Decimal(10, 5)), CAST(110.43217 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11308, CAST(9.25755 AS Decimal(10, 5)), CAST(12.43042 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11309, CAST(35.49784 AS Decimal(10, 5)), CAST(133.24551 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11310, CAST(24.46694 AS Decimal(10, 5)), CAST(122.97778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11314, CAST(56.08940 AS Decimal(10, 5)), CAST(-96.08920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11315, CAST(-9.75288 AS Decimal(10, 5)), CAST(143.40531 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11319, CAST(27.04396 AS Decimal(10, 5)), CAST(128.40152 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11323, CAST(41.25721 AS Decimal(10, 5)), CAST(-80.66724 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11329, CAST(38.26920 AS Decimal(10, 5)), CAST(109.73100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11330, CAST(32.65644 AS Decimal(10, 5)), CAST(-114.60578 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11333, CAST(35.11489 AS Decimal(10, 5)), CAST(111.03556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11336, CAST(46.89627 AS Decimal(10, 5)), CAST(142.73867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11339, CAST(31.09833 AS Decimal(10, 5)), CAST(61.54389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11342, CAST(22.89711 AS Decimal(10, 5)), CAST(-102.68689 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11343, CAST(57.55343 AS Decimal(10, 5)), CAST(-153.74652 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11344, CAST(44.10827 AS Decimal(10, 5)), CAST(15.34670 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11346, CAST(45.74056 AS Decimal(10, 5)), CAST(16.06833 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11347, CAST(29.47569 AS Decimal(10, 5)), CAST(60.90619 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11349, CAST(47.48717 AS Decimal(10, 5)), CAST(84.88621 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11350, CAST(37.75085 AS Decimal(10, 5)), CAST(20.88425 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11354, CAST(6.92242 AS Decimal(10, 5)), CAST(122.06042 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11358, CAST(36.77365 AS Decimal(10, 5)), CAST(48.35942 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11359, CAST(-6.22203 AS Decimal(10, 5)), CAST(39.22489 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11362, CAST(47.86722 AS Decimal(10, 5)), CAST(35.31639 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11364, CAST(41.66624 AS Decimal(10, 5)), CAST(-1.04155 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11366, CAST(30.97220 AS Decimal(10, 5)), CAST(61.86612 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11376, CAST(42.85360 AS Decimal(10, 5)), CAST(71.30360 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11377, CAST(21.21446 AS Decimal(10, 5)), CAST(110.35848 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11378, CAST(27.32560 AS Decimal(10, 5)), CAST(103.75500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11379, CAST(34.51967 AS Decimal(10, 5)), CAST(113.84089 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11380, CAST(47.70872 AS Decimal(10, 5)), CAST(67.73767 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11382, CAST(31.35806 AS Decimal(10, 5)), CAST(69.46333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11385, CAST(29.93420 AS Decimal(10, 5)), CAST(122.36200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11386, CAST(22.00640 AS Decimal(10, 5)), CAST(113.37600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11387, CAST(52.13852 AS Decimal(10, 5)), CAST(15.79856 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11388, CAST(12.55569 AS Decimal(10, 5)), CAST(-16.28268 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11391, CAST(13.77900 AS Decimal(10, 5)), CAST(8.98376 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11397, CAST(41.50611 AS Decimal(10, 5)), CAST(32.08861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11398, CAST(22.75754 AS Decimal(10, 5)), CAST(-12.47867 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11402, CAST(27.59076 AS Decimal(10, 5)), CAST(106.99825 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11403, CAST(47.45264 AS Decimal(10, 5)), CAST(8.56050 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11409, CAST(-16.74810 AS Decimal(10, 5)), CAST(-179.66701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11414, CAST(63.68690 AS Decimal(10, 5)), CAST(66.69860 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11415, CAST(63.92100 AS Decimal(10, 5)), CAST(65.03050 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11417, CAST(63.20000 AS Decimal(10, 5)), CAST(64.45000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11418, CAST(-25.93851 AS Decimal(10, 5)), CAST(27.92613 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11421, CAST(30.13919 AS Decimal(10, 5)), CAST(101.74301 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11431, CAST(27.37960 AS Decimal(10, 5)), CAST(52.73770 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11432, CAST(58.74051 AS Decimal(10, 5)), CAST(-94.07267 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11435, CAST(53.51742 AS Decimal(10, 5)), CAST(142.89196 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11437, CAST(49.18000 AS Decimal(10, 5)), CAST(142.10000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11440, CAST(35.98559 AS Decimal(10, 5)), CAST(-113.81945 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11441, CAST(36.36190 AS Decimal(10, 5)), CAST(36.28346 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11442, CAST(34.99140 AS Decimal(10, 5)), CAST(126.38300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11443, CAST(40.69709 AS Decimal(10, 5)), CAST(-74.17557 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11448, CAST(-20.44993 AS Decimal(10, 5)), CAST(-66.84211 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11449, CAST(35.94818 AS Decimal(10, 5)), CAST(-114.85771 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11456, CAST(44.65000 AS Decimal(10, 5)), CAST(-73.46667 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11460, CAST(37.86640 AS Decimal(10, 5)), CAST(68.86470 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11461, CAST(2.17784 AS Decimal(10, 5)), CAST(111.20200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11462, CAST(52.91465 AS Decimal(10, 5)), CAST(122.42769 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11467, CAST(17.51500 AS Decimal(10, 5)), CAST(106.59056 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11477, CAST(42.06500 AS Decimal(10, 5)), CAST(127.60500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11480, CAST(36.42432 AS Decimal(10, 5)), CAST(55.10313 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11558, CAST(60.12220 AS Decimal(10, 5)), CAST(19.89816 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11567, CAST(49.49575 AS Decimal(10, 5)), CAST(11.07576 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11595, CAST(24.93933 AS Decimal(10, 5)), CAST(98.48368 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11598, CAST(34.55994 AS Decimal(10, 5)), CAST(105.85893 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11622, CAST(34.79699 AS Decimal(10, 5)), CAST(138.18153 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11623, CAST(15.17483 AS Decimal(10, 5)), CAST(76.63406 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11643, CAST(38.74997 AS Decimal(10, 5)), CAST(48.81243 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11645, CAST(37.57037 AS Decimal(10, 5)), CAST(105.15199 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11646, CAST(36.53764 AS Decimal(10, 5)), CAST(-93.20011 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11647, CAST(33.71861 AS Decimal(10, 5)), CAST(1.08778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11649, CAST(51.80000 AS Decimal(10, 5)), CAST(143.16700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11651, CAST(43.96119 AS Decimal(10, 5)), CAST(145.68455 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11670, CAST(44.89540 AS Decimal(10, 5)), CAST(82.30030 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11683, CAST(46.75012 AS Decimal(10, 5)), CAST(125.13793 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11685, CAST(9.85917 AS Decimal(10, 5)), CAST(126.01500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11687, CAST(8.71892 AS Decimal(10, 5)), CAST(-83.63921 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11688, CAST(24.89780 AS Decimal(10, 5)), CAST(55.15905 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11690, CAST(36.07579 AS Decimal(10, 5)), CAST(10.43859 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11692, CAST(43.42330 AS Decimal(10, 5)), CAST(112.09100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11701, CAST(36.07616 AS Decimal(10, 5)), CAST(106.21797 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11706, CAST(36.18108 AS Decimal(10, 5)), CAST(140.41544 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11709, CAST(45.28793 AS Decimal(10, 5)), CAST(131.20010 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11729, CAST(41.72889 AS Decimal(10, 5)), CAST(0.53775 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11744, CAST(31.99152 AS Decimal(10, 5)), CAST(44.39622 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11746, CAST(51.20013 AS Decimal(10, 5)), CAST(2.87245 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11766, CAST(42.11938 AS Decimal(10, 5)), CAST(15.50226 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11771, CAST(32.09970 AS Decimal(10, 5)), CAST(80.05330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11779, CAST(17.90443 AS Decimal(10, 5)), CAST(-62.84392 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11780, CAST(18.10019 AS Decimal(10, 5)), CAST(-63.04755 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11787, CAST(59.41651 AS Decimal(10, 5)), CAST(24.79940 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11788, CAST(39.72069 AS Decimal(10, 5)), CAST(117.99252 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11796, CAST(43.03030 AS Decimal(10, 5)), CAST(89.09720 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11799, CAST(65.43333 AS Decimal(10, 5)), CAST(52.19913 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11800, CAST(67.65540 AS Decimal(10, 5)), CAST(12.72700 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11808, CAST(41.88465 AS Decimal(10, 5)), CAST(16.17593 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11811, CAST(47.75140 AS Decimal(10, 5)), CAST(129.01800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11812, CAST(32.83640 AS Decimal(10, 5)), CAST(97.03610 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11816, CAST(47.31000 AS Decimal(10, 5)), CAST(119.91100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11818, CAST(25.11667 AS Decimal(10, 5)), CAST(51.30000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11819, CAST(26.48970 AS Decimal(10, 5)), CAST(38.10530 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11820, CAST(49.70662 AS Decimal(10, 5)), CAST(-2.21553 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11827, CAST(51.18944 AS Decimal(10, 5)), CAST(4.46028 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11829, CAST(-22.16515 AS Decimal(10, 5)), CAST(-49.07138 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11830, CAST(33.65557 AS Decimal(10, 5)), CAST(-7.22102 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11831, CAST(57.86521 AS Decimal(10, 5)), CAST(114.24219 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11833, CAST(52.37830 AS Decimal(10, 5)), CAST(140.44800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11834, CAST(12.13104 AS Decimal(10, 5)), CAST(-68.26851 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11836, CAST(39.99902 AS Decimal(10, 5)), CAST(0.02618 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11840, CAST(12.18885 AS Decimal(10, 5)), CAST(-68.95980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11852, CAST(39.80990 AS Decimal(10, 5)), CAST(30.51940 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11857, CAST(36.29922 AS Decimal(10, 5)), CAST(32.30060 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11858, CAST(10.29810 AS Decimal(10, 5)), CAST(10.89890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11860, CAST(49.43496 AS Decimal(10, 5)), CAST(-2.60197 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11869, CAST(33.78685 AS Decimal(10, 5)), CAST(119.12853 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11874, CAST(54.08333 AS Decimal(10, 5)), CAST(-4.62389 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11877, CAST(49.20787 AS Decimal(10, 5)), CAST(-2.19456 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11883, CAST(57.77264 AS Decimal(10, 5)), CAST(108.06324 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11890, CAST(60.72249 AS Decimal(10, 5)), CAST(114.82279 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11908, CAST(29.51470 AS Decimal(10, 5)), CAST(108.83200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11912, CAST(29.35190 AS Decimal(10, 5)), CAST(89.31000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11913, CAST(64.83847 AS Decimal(10, 5)), CAST(11.14584 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11914, CAST(17.64545 AS Decimal(10, 5)), CAST(-63.22074 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11917, CAST(33.12809 AS Decimal(10, 5)), CAST(-117.28115 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11920, CAST(-3.44385 AS Decimal(10, 5)), CAST(-80.00982 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11923, CAST(36.17838 AS Decimal(10, 5)), CAST(5.32562 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11926, CAST(26.33969 AS Decimal(10, 5)), CAST(31.73920 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11929, CAST(17.49649 AS Decimal(10, 5)), CAST(-62.97944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11930, CAST(18.04095 AS Decimal(10, 5)), CAST(-63.10890 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11939, CAST(54.27555 AS Decimal(10, 5)), CAST(48.24294 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11943, CAST(61.27448 AS Decimal(10, 5)), CAST(108.02487 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11944, CAST(30.26390 AS Decimal(10, 5)), CAST(-5.85306 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11947, CAST(72.88392 AS Decimal(10, 5)), CAST(-55.59820 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11948, CAST(32.52684 AS Decimal(10, 5)), CAST(102.35464 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11958, CAST(-2.79671 AS Decimal(10, 5)), CAST(-76.46562 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11959, CAST(15.18599 AS Decimal(10, 5)), CAST(120.56033 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11961, CAST(6.20856 AS Decimal(10, 5)), CAST(6.66108 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11962, CAST(-7.96960 AS Decimal(10, 5)), CAST(-14.39366 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11973, CAST(40.92640 AS Decimal(10, 5)), CAST(107.73900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11977, CAST(52.36213 AS Decimal(10, 5)), CAST(13.50168 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11979, CAST(27.30036 AS Decimal(10, 5)), CAST(105.30159 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11981, CAST(38.86016 AS Decimal(10, 5)), CAST(40.59414 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (11986, CAST(61.59529 AS Decimal(10, 5)), CAST(90.00841 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12000, CAST(-42.33847 AS Decimal(10, 5)), CAST(-73.71547 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12001, CAST(30.73579 AS Decimal(10, 5)), CAST(117.69464 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12013, CAST(29.31613 AS Decimal(10, 5)), CAST(100.04828 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12018, CAST(24.50308 AS Decimal(10, 5)), CAST(52.33595 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12021, CAST(5.15692 AS Decimal(10, 5)), CAST(73.13016 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12022, CAST(5.99083 AS Decimal(10, 5)), CAST(80.73330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12037, CAST(-0.30944 AS Decimal(10, 5)), CAST(73.43250 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12038, CAST(40.82667 AS Decimal(10, 5)), CAST(47.71276 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12040, CAST(26.88389 AS Decimal(10, 5)), CAST(90.46836 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12044, CAST(51.96920 AS Decimal(10, 5)), CAST(85.83640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12047, CAST(-2.15948 AS Decimal(10, 5)), CAST(34.22175 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12051, CAST(6.28389 AS Decimal(10, 5)), CAST(81.12390 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12053, CAST(6.87200 AS Decimal(10, 5)), CAST(80.56800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12054, CAST(-20.06000 AS Decimal(10, 5)), CAST(148.88100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12060, CAST(-24.36704 AS Decimal(10, 5)), CAST(31.05483 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12063, CAST(26.97410 AS Decimal(10, 5)), CAST(107.98046 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12065, CAST(39.97495 AS Decimal(10, 5)), CAST(43.87939 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12066, CAST(70.49556 AS Decimal(10, 5)), CAST(-51.30305 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12068, CAST(73.20184 AS Decimal(10, 5)), CAST(-56.01350 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12072, CAST(65.54878 AS Decimal(10, 5)), CAST(-38.97674 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12073, CAST(34.14386 AS Decimal(10, 5)), CAST(132.23575 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12076, CAST(27.56299 AS Decimal(10, 5)), CAST(90.74651 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12080, CAST(50.37815 AS Decimal(10, 5)), CAST(124.11482 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12083, CAST(38.54189 AS Decimal(10, 5)), CAST(102.34784 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12084, CAST(4.87201 AS Decimal(10, 5)), CAST(31.60112 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12085, CAST(-21.51177 AS Decimal(10, 5)), CAST(-43.17052 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12086, CAST(7.32978 AS Decimal(10, 5)), CAST(80.64003 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12088, CAST(72.38017 AS Decimal(10, 5)), CAST(-55.54551 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12091, CAST(-14.52111 AS Decimal(10, 5)), CAST(132.37778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12096, CAST(0.73294 AS Decimal(10, 5)), CAST(73.43353 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12101, CAST(74.57943 AS Decimal(10, 5)), CAST(-57.23538 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12104, CAST(-10.17167 AS Decimal(10, 5)), CAST(123.67129 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12106, CAST(39.11042 AS Decimal(10, 5)), CAST(30.13215 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12107, CAST(65.86392 AS Decimal(10, 5)), CAST(-36.99792 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12108, CAST(8.72205 AS Decimal(10, 5)), CAST(167.73593 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12110, CAST(34.92141 AS Decimal(10, 5)), CAST(135.74231 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12120, CAST(51.57139 AS Decimal(10, 5)), CAST(0.69556 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12124, CAST(51.23997 AS Decimal(10, 5)), CAST(22.71333 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12126, CAST(3.47059 AS Decimal(10, 5)), CAST(72.83584 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12129, CAST(9.55897 AS Decimal(10, 5)), CAST(31.65224 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12130, CAST(-11.85470 AS Decimal(10, 5)), CAST(-72.93970 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12132, CAST(65.41250 AS Decimal(10, 5)), CAST(-52.93940 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12135, CAST(0.16119 AS Decimal(10, 5)), CAST(38.19474 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12138, CAST(-12.29810 AS Decimal(10, 5)), CAST(43.76640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12152, CAST(19.62250 AS Decimal(10, 5)), CAST(96.20140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12154, CAST(43.31951 AS Decimal(10, 5)), CAST(45.01296 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12155, CAST(-25.38320 AS Decimal(10, 5)), CAST(31.10560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12158, CAST(70.78836 AS Decimal(10, 5)), CAST(-53.65587 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12160, CAST(52.45117 AS Decimal(10, 5)), CAST(20.65271 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12162, CAST(74.10986 AS Decimal(10, 5)), CAST(-57.06513 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12167, CAST(-22.08254 AS Decimal(10, 5)), CAST(140.55779 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12185, CAST(4.84162 AS Decimal(10, 5)), CAST(7.01765 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12191, CAST(-8.75914 AS Decimal(10, 5)), CAST(116.27655 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12203, CAST(70.81114 AS Decimal(10, 5)), CAST(-51.63139 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12210, CAST(8.26246 AS Decimal(10, 5)), CAST(-79.07804 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12214, CAST(76.01863 AS Decimal(10, 5)), CAST(-65.11762 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12217, CAST(65.90593 AS Decimal(10, 5)), CAST(-36.37822 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12219, CAST(31.14338 AS Decimal(10, 5)), CAST(121.80521 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12225, CAST(7.95667 AS Decimal(10, 5)), CAST(80.72850 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12227, CAST(42.02088 AS Decimal(10, 5)), CAST(35.08061 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12228, CAST(77.78651 AS Decimal(10, 5)), CAST(-70.63857 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12249, CAST(59.88670 AS Decimal(10, 5)), CAST(111.03727 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12252, CAST(73.36741 AS Decimal(10, 5)), CAST(-56.07289 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12255, CAST(19.90299 AS Decimal(10, 5)), CAST(105.46892 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12258, CAST(65.89201 AS Decimal(10, 5)), CAST(-37.78341 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12262, CAST(27.25600 AS Decimal(10, 5)), CAST(91.51457 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12265, CAST(32.89410 AS Decimal(10, 5)), CAST(13.27600 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12270, CAST(65.79663 AS Decimal(10, 5)), CAST(87.93165 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12271, CAST(71.05020 AS Decimal(10, 5)), CAST(-51.88802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12273, CAST(72.15682 AS Decimal(10, 5)), CAST(-55.52814 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12274, CAST(9.12727 AS Decimal(10, 5)), CAST(-77.93328 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12275, CAST(4.87649 AS Decimal(10, 5)), CAST(8.08577 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12281, CAST(-3.81030 AS Decimal(10, 5)), CAST(39.79716 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12285, CAST(5.59611 AS Decimal(10, 5)), CAST(5.81778 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12286, CAST(7.72583 AS Decimal(10, 5)), CAST(27.97500 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12290, CAST(56.48417 AS Decimal(10, 5)), CAST(-132.36972 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12291, CAST(34.81070 AS Decimal(10, 5)), CAST(102.64492 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12294, CAST(32.56310 AS Decimal(10, 5)), CAST(119.71900 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12295, CAST(27.80347 AS Decimal(10, 5)), CAST(114.30820 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12302, CAST(40.73920 AS Decimal(10, 5)), CAST(114.93100 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12303, CAST(38.80311 AS Decimal(10, 5)), CAST(100.67528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12313, CAST(38.82286 AS Decimal(10, 5)), CAST(105.62616 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12314, CAST(39.22213 AS Decimal(10, 5)), CAST(101.54942 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12318, CAST(39.48152 AS Decimal(10, 5)), CAST(54.36275 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12319, CAST(-27.39995 AS Decimal(10, 5)), CAST(141.81262 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12322, CAST(-22.67433 AS Decimal(10, 5)), CAST(119.16553 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12323, CAST(67.64875 AS Decimal(10, 5)), CAST(134.69496 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12325, CAST(68.55712 AS Decimal(10, 5)), CAST(146.23352 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12341, CAST(32.51625 AS Decimal(10, 5)), CAST(-84.93882 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12343, CAST(35.38555 AS Decimal(10, 5)), CAST(-80.71097 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12344, CAST(-22.96661 AS Decimal(10, 5)), CAST(118.81413 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12348, CAST(37.12501 AS Decimal(10, 5)), CAST(97.26849 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12349, CAST(69.39258 AS Decimal(10, 5)), CAST(139.90196 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12352, CAST(42.01488 AS Decimal(10, 5)), CAST(101.00195 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12353, CAST(61.92455 AS Decimal(10, 5)), CAST(159.23099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12358, CAST(48.19447 AS Decimal(10, 5)), CAST(134.35799 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12368, CAST(37.55009 AS Decimal(10, 5)), CAST(44.23774 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12371, CAST(24.78401 AS Decimal(10, 5)), CAST(107.69975 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12375, CAST(39.54256 AS Decimal(10, 5)), CAST(-8.16036 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12380, CAST(67.84705 AS Decimal(10, 5)), CAST(166.13877 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12381, CAST(62.78614 AS Decimal(10, 5)), CAST(136.84227 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12382, CAST(66.45583 AS Decimal(10, 5)), CAST(143.25314 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12385, CAST(6.90767 AS Decimal(10, 5)), CAST(79.91241 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12388, CAST(-15.63341 AS Decimal(10, 5)), CAST(29.60373 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12389, CAST(-15.72691 AS Decimal(10, 5)), CAST(29.29736 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12390, CAST(37.68324 AS Decimal(10, 5)), CAST(111.14280 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12393, CAST(62.10728 AS Decimal(10, 5)), CAST(129.54346 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12397, CAST(3.63866 AS Decimal(10, 5)), CAST(98.87403 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12400, CAST(-26.80936 AS Decimal(10, 5)), CAST(150.16517 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12402, CAST(22.22396 AS Decimal(10, 5)), CAST(95.09328 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12408, CAST(53.15387 AS Decimal(10, 5)), CAST(140.65487 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12409, CAST(63.29718 AS Decimal(10, 5)), CAST(118.34696 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12411, CAST(60.39919 AS Decimal(10, 5)), CAST(120.46406 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12412, CAST(68.51551 AS Decimal(10, 5)), CAST(112.47923 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12416, CAST(7.55676 AS Decimal(10, 5)), CAST(-80.02330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12420, CAST(-21.81166 AS Decimal(10, 5)), CAST(139.92329 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12421, CAST(76.53120 AS Decimal(10, 5)), CAST(-68.70316 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12434, CAST(67.79199 AS Decimal(10, 5)), CAST(130.39089 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12435, CAST(9.95705 AS Decimal(10, 5)), CAST(-84.13980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12436, CAST(71.92897 AS Decimal(10, 5)), CAST(114.08229 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12439, CAST(31.63365 AS Decimal(10, 5)), CAST(110.33792 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12443, CAST(37.36390 AS Decimal(10, 5)), CAST(42.05987 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12444, CAST(67.47979 AS Decimal(10, 5)), CAST(153.73512 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12447, CAST(50.27368 AS Decimal(10, 5)), CAST(-111.18928 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12449, CAST(62.18481 AS Decimal(10, 5)), CAST(117.63619 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12452, CAST(65.61228 AS Decimal(10, 5)), CAST(-37.61830 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12455, CAST(2.20831 AS Decimal(10, 5)), CAST(73.14775 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12458, CAST(38.94716 AS Decimal(10, 5)), CAST(-95.67385 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12459, CAST(43.45747 AS Decimal(10, 5)), CAST(-80.38593 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12460, CAST(-21.84059 AS Decimal(10, 5)), CAST(140.89163 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12461, CAST(-20.75306 AS Decimal(10, 5)), CAST(-51.68200 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12463, CAST(72.79020 AS Decimal(10, 5)), CAST(-56.12889 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12464, CAST(47.08953 AS Decimal(10, 5)), CAST(81.65897 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12465, CAST(70.01120 AS Decimal(10, 5)), CAST(135.64673 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12466, CAST(60.36391 AS Decimal(10, 5)), CAST(134.44880 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12467, CAST(64.54933 AS Decimal(10, 5)), CAST(143.11083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12468, CAST(70.68035 AS Decimal(10, 5)), CAST(-52.11176 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12470, CAST(49.29034 AS Decimal(10, 5)), CAST(-123.11511 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12471, CAST(63.45925 AS Decimal(10, 5)), CAST(120.27643 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12472, CAST(48.42407 AS Decimal(10, 5)), CAST(-123.37198 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12473, CAST(63.75623 AS Decimal(10, 5)), CAST(121.69410 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12488, CAST(66.79664 AS Decimal(10, 5)), CAST(123.36136 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12490, CAST(65.74699 AS Decimal(10, 5)), CAST(150.89155 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12493, CAST(-8.30998 AS Decimal(10, 5)), CAST(114.34017 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12494, CAST(32.39727 AS Decimal(10, 5)), CAST(-6.32390 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12500, CAST(6.80725 AS Decimal(10, 5)), CAST(-58.10605 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12508, CAST(-8.24186 AS Decimal(10, 5)), CAST(113.69378 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12515, CAST(26.60626 AS Decimal(10, 5)), CAST(104.97188 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12518, CAST(15.86905 AS Decimal(10, 5)), CAST(-61.27002 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12521, CAST(-51.82278 AS Decimal(10, 5)), CAST(-58.44722 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12523, CAST(40.96473 AS Decimal(10, 5)), CAST(38.08024 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12527, CAST(-3.08464 AS Decimal(10, 5)), CAST(120.24519 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12529, CAST(39.66273 AS Decimal(10, 5)), CAST(119.05807 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12532, CAST(8.37503 AS Decimal(10, 5)), CAST(-80.12816 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12537, CAST(18.57250 AS Decimal(10, 5)), CAST(-69.98560 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12539, CAST(1.55594 AS Decimal(10, 5)), CAST(98.88891 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12542, CAST(18.44483 AS Decimal(10, 5)), CAST(-64.54297 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12556, CAST(39.45762 AS Decimal(10, 5)), CAST(-74.57654 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12559, CAST(57.50550 AS Decimal(10, 5)), CAST(-134.59627 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12560, CAST(56.94003 AS Decimal(10, 5)), CAST(-154.18083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12562, CAST(57.47161 AS Decimal(10, 5)), CAST(-153.81542 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12565, CAST(32.48225 AS Decimal(10, 5)), CAST(130.15893 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12567, CAST(19.13969 AS Decimal(10, 5)), CAST(110.45577 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12587, CAST(7.10869 AS Decimal(10, 5)), CAST(79.86934 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12593, CAST(60.15708 AS Decimal(10, 5)), CAST(-164.28577 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12595, CAST(45.50587 AS Decimal(10, 5)), CAST(123.01958 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12597, CAST(-4.85614 AS Decimal(10, 5)), CAST(139.48105 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12602, CAST(19.49730 AS Decimal(10, 5)), CAST(57.63861 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12606, CAST(2.25899 AS Decimal(10, 5)), CAST(98.99351 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12611, CAST(11.43140 AS Decimal(10, 5)), CAST(-86.03072 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12613, CAST(30.35780 AS Decimal(10, 5)), CAST(-85.79892 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12615, CAST(55.95002 AS Decimal(10, 5)), CAST(-133.66662 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12623, CAST(35.17088 AS Decimal(10, 5)), CAST(-79.01447 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12627, CAST(48.53732 AS Decimal(10, 5)), CAST(-123.00962 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12629, CAST(32.98586 AS Decimal(10, 5)), CAST(-97.31460 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12637, CAST(34.41576 AS Decimal(10, 5)), CAST(100.31118 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12647, CAST(-15.96187 AS Decimal(10, 5)), CAST(-5.64594 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12653, CAST(38.26784 AS Decimal(10, 5)), CAST(90.83638 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12654, CAST(45.48673 AS Decimal(10, 5)), CAST(119.40851 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12655, CAST(5.70849 AS Decimal(10, 5)), CAST(73.02492 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12659, CAST(45.25688 AS Decimal(10, 5)), CAST(147.95425 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12661, CAST(-5.59531 AS Decimal(10, 5)), CAST(-78.77300 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12664, CAST(-28.67598 AS Decimal(10, 5)), CAST(-49.06202 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12668, CAST(47.75482 AS Decimal(10, 5)), CAST(-122.25929 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12670, CAST(-0.71933 AS Decimal(10, 5)), CAST(29.69819 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12671, CAST(60.90764 AS Decimal(10, 5)), CAST(-161.43547 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12672, CAST(57.53511 AS Decimal(10, 5)), CAST(-153.97842 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12674, CAST(58.98897 AS Decimal(10, 5)), CAST(-159.04997 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12675, CAST(57.02584 AS Decimal(10, 5)), CAST(-154.14532 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12676, CAST(57.92536 AS Decimal(10, 5)), CAST(-152.49676 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12678, CAST(59.93306 AS Decimal(10, 5)), CAST(-164.03083 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12679, CAST(55.90903 AS Decimal(10, 5)), CAST(-159.15846 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12682, CAST(63.11178 AS Decimal(10, 5)), CAST(7.82452 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12683, CAST(55.68177 AS Decimal(10, 5)), CAST(-132.52445 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12695, CAST(36.13108 AS Decimal(10, 5)), CAST(111.63757 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12709, CAST(-26.35680 AS Decimal(10, 5)), CAST(31.71701 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12712, CAST(33.68240 AS Decimal(10, 5)), CAST(-78.92324 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12715, CAST(21.45045 AS Decimal(10, 5)), CAST(-157.76800 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12717, CAST(28.36186 AS Decimal(10, 5)), CAST(-97.65748 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12718, CAST(55.86457 AS Decimal(10, 5)), CAST(-133.19249 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12720, CAST(27.54587 AS Decimal(10, 5)), CAST(100.76677 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12726, CAST(41.49874 AS Decimal(10, 5)), CAST(-74.10045 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12729, CAST(47.86604 AS Decimal(10, 5)), CAST(122.76466 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12734, CAST(24.39025 AS Decimal(10, 5)), CAST(56.62319 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12735, CAST(57.21895 AS Decimal(10, 5)), CAST(-153.26758 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12737, CAST(57.88378 AS Decimal(10, 5)), CAST(-152.85261 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12748, CAST(60.70690 AS Decimal(10, 5)), CAST(-161.77325 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12754, CAST(56.32715 AS Decimal(10, 5)), CAST(-133.60923 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12771, CAST(23.62534 AS Decimal(10, 5)), CAST(87.24140 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12772, CAST(-29.71136 AS Decimal(10, 5)), CAST(-53.68815 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12774, CAST(35.39619 AS Decimal(10, 5)), CAST(119.34109 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12775, CAST(-0.43490 AS Decimal(10, 5)), CAST(130.77078 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12778, CAST(61.77994 AS Decimal(10, 5)), CAST(-161.31765 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12784, CAST(35.61698 AS Decimal(10, 5)), CAST(-106.08919 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12787, CAST(35.23756 AS Decimal(10, 5)), CAST(-120.64147 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12788, CAST(71.23685 AS Decimal(10, 5)), CAST(72.15305 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12789, CAST(61.84467 AS Decimal(10, 5)), CAST(-165.57998 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12799, CAST(28.37992 AS Decimal(10, 5)), CAST(117.96535 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12800, CAST(26.42777 AS Decimal(10, 5)), CAST(117.83678 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12801, CAST(17.10492 AS Decimal(10, 5)), CAST(-89.10099 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12804, CAST(62.52406 AS Decimal(10, 5)), CAST(-164.84415 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12806, CAST(58.35973 AS Decimal(10, 5)), CAST(-152.25497 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12809, CAST(37.74163 AS Decimal(10, 5)), CAST(-92.14074 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12821, CAST(-27.55892 AS Decimal(10, 5)), CAST(151.79591 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12822, CAST(41.12980 AS Decimal(10, 5)), CAST(113.10802 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12823, CAST(57.73037 AS Decimal(10, 5)), CAST(-153.32142 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12824, CAST(19.77157 AS Decimal(10, 5)), CAST(94.02654 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12825, CAST(11.15112 AS Decimal(10, 5)), CAST(98.73550 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12828, CAST(30.40020 AS Decimal(10, 5)), CAST(-86.47134 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12833, CAST(32.59180 AS Decimal(10, 5)), CAST(110.90767 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12836, CAST(60.69344 AS Decimal(10, 5)), CAST(-161.97779 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12840, CAST(38.59702 AS Decimal(10, 5)), CAST(112.96915 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12841, CAST(56.11581 AS Decimal(10, 5)), CAST(-133.12296 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12842, CAST(41.45958 AS Decimal(10, 5)), CAST(108.53454 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12845, CAST(49.16936 AS Decimal(10, 5)), CAST(-123.93552 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12849, CAST(40.53937 AS Decimal(10, 5)), CAST(122.35491 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12868, CAST(53.48181 AS Decimal(10, 5)), CAST(-1.00542 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12877, CAST(38.72250 AS Decimal(10, 5)), CAST(-9.35424 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12881, CAST(23.27652 AS Decimal(10, 5)), CAST(99.37039 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12884, CAST(48.44401 AS Decimal(10, 5)), CAST(126.12322 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12891, CAST(-8.14047 AS Decimal(10, 5)), CAST(127.90712 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12892, CAST(-1.58642 AS Decimal(10, 5)), CAST(35.25680 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12894, CAST(-6.17853 AS Decimal(10, 5)), CAST(120.43724 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12915, CAST(-4.02404 AS Decimal(10, 5)), CAST(103.37826 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12920, CAST(-12.48073 AS Decimal(10, 5)), CAST(-55.67330 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12924, CAST(4.72083 AS Decimal(10, 5)), CAST(96.84937 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12933, CAST(55.55320 AS Decimal(10, 5)), CAST(38.14951 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12934, CAST(31.77598 AS Decimal(10, 5)), CAST(12.24980 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12936, CAST(52.31917 AS Decimal(10, 5)), CAST(10.55611 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12937, CAST(46.68640 AS Decimal(10, 5)), CAST(17.15910 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12939, CAST(-23.07990 AS Decimal(10, 5)), CAST(-134.89000 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12941, CAST(53.86590 AS Decimal(10, 5)), CAST(-1.66057 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12944, CAST(39.34507 AS Decimal(10, 5)), CAST(-81.43901 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12945, CAST(44.25401 AS Decimal(10, 5)), CAST(-121.14975 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12946, CAST(36.58790 AS Decimal(10, 5)), CAST(-121.83939 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12949, CAST(32.83422 AS Decimal(10, 5)), CAST(-115.57944 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12951, CAST(32.82922 AS Decimal(10, 5)), CAST(-115.67167 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12953, CAST(43.50657 AS Decimal(10, 5)), CAST(-114.29782 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12954, CAST(27.39544 AS Decimal(10, 5)), CAST(-82.55439 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12955, CAST(47.39178 AS Decimal(10, 5)), CAST(-92.84233 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12956, CAST(42.15980 AS Decimal(10, 5)), CAST(-76.89107 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12957, CAST(36.48069 AS Decimal(10, 5)), CAST(-82.40709 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12958, CAST(41.33869 AS Decimal(10, 5)), CAST(-75.72353 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12961, CAST(-4.18110 AS Decimal(10, 5)), CAST(121.61340 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12962, CAST(30.32232 AS Decimal(10, 5)), CAST(112.27933 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12964, CAST(32.85417 AS Decimal(10, 5)), CAST(103.68528 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12965, CAST(-6.11781 AS Decimal(10, 5)), CAST(-50.00347 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12970, CAST(44.88217 AS Decimal(10, 5)), CAST(-93.20835 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (12972, CAST(34.74509 AS Decimal(10, 5)), CAST(-87.61003 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13034, CAST(-24.74060 AS Decimal(10, 5)), CAST(31.51814 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13036, CAST(45.62352 AS Decimal(10, 5)), CAST(63.21379 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13037, CAST(-16.98973 AS Decimal(10, 5)), CAST(-65.14107 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13038, CAST(41.12243 AS Decimal(10, 5)), CAST(118.07402 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13047, CAST(42.58775 AS Decimal(10, 5)), CAST(76.71225 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13048, CAST(22.41572 AS Decimal(10, 5)), CAST(99.78646 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13049, CAST(47.20651 AS Decimal(10, 5)), CAST(132.62386 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13051, CAST(-1.27165 AS Decimal(10, 5)), CAST(34.95429 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13053, CAST(-1.26427 AS Decimal(10, 5)), CAST(35.02304 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13055, CAST(-1.40860 AS Decimal(10, 5)), CAST(35.11024 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13061, CAST(44.24210 AS Decimal(10, 5)), CAST(85.89055 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13068, CAST(26.80354 AS Decimal(10, 5)), CAST(110.65404 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13073, CAST(45.21077 AS Decimal(10, 5)), CAST(124.44875 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13079, CAST(-2.90567 AS Decimal(10, 5)), CAST(-40.35587 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13080, CAST(33.89533 AS Decimal(10, 5)), CAST(51.57704 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13084, CAST(6.09682 AS Decimal(10, 5)), CAST(46.63825 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13085, CAST(4.76428 AS Decimal(10, 5)), CAST(45.23689 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13091, CAST(5.31712 AS Decimal(10, 5)), CAST(45.98551 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13094, CAST(13.69949 AS Decimal(10, 5)), CAST(-89.11986 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13097, CAST(30.95671 AS Decimal(10, 5)), CAST(46.18584 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13099, CAST(2.66597 AS Decimal(10, 5)), CAST(72.88510 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13104, CAST(36.26018 AS Decimal(10, 5)), CAST(-113.23122 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13108, CAST(-2.20385 AS Decimal(10, 5)), CAST(121.65989 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13118, CAST(27.81638 AS Decimal(10, 5)), CAST(106.33268 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13140, CAST(-3.84674 AS Decimal(10, 5)), CAST(32.68579 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13143, CAST(19.68876 AS Decimal(10, 5)), CAST(74.37766 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13148, CAST(11.91665 AS Decimal(10, 5)), CAST(75.54663 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13162, CAST(26.59114 AS Decimal(10, 5)), CAST(74.81329 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13164, CAST(38.27902 AS Decimal(10, 5)), CAST(77.07029 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13167, CAST(31.43569 AS Decimal(10, 5)), CAST(75.75640 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13172, CAST(44.55749 AS Decimal(10, 5)), CAST(135.49178 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13173, CAST(45.87833 AS Decimal(10, 5)), CAST(133.73626 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13185, CAST(-1.29971 AS Decimal(10, 5)), CAST(35.06218 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13186, CAST(45.08252 AS Decimal(10, 5)), CAST(136.59123 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13191, CAST(29.31379 AS Decimal(10, 5)), CAST(113.28106 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13195, CAST(14.41864 AS Decimal(10, 5)), CAST(122.03905 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13201, CAST(-6.64466 AS Decimal(10, 5)), CAST(108.16074 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13202, CAST(33.79042 AS Decimal(10, 5)), CAST(105.79542 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13205, CAST(45.84145 AS Decimal(10, 5)), CAST(137.67277 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13208, CAST(14.67111 AS Decimal(10, 5)), CAST(-17.06694 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13209, CAST(-54.05010 AS Decimal(10, 5)), CAST(-68.81068 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13210, CAST(47.17592 AS Decimal(10, 5)), CAST(138.66315 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13211, CAST(17.75206 AS Decimal(10, 5)), CAST(-89.92501 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13212, CAST(46.54174 AS Decimal(10, 5)), CAST(138.32165 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13214, CAST(42.91973 AS Decimal(10, 5)), CAST(133.90394 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13215, CAST(-5.29096 AS Decimal(10, 5)), CAST(123.63499 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13216, CAST(25.65438 AS Decimal(10, 5)), CAST(57.80012 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13219, CAST(32.54082 AS Decimal(10, 5)), CAST(114.07911 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13221, CAST(2.96427 AS Decimal(10, 5)), CAST(105.75457 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13222, CAST(38.00877 AS Decimal(10, 5)), CAST(100.64238 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13231, CAST(46.19066 AS Decimal(10, 5)), CAST(80.83104 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13239, CAST(38.97470 AS Decimal(10, 5)), CAST(88.00838 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13240, CAST(10.52156 AS Decimal(10, 5)), CAST(119.27051 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13260, CAST(-9.01748 AS Decimal(10, 5)), CAST(160.95392 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13268, CAST(41.26290 AS Decimal(10, 5)), CAST(28.74242 AS Decimal(10, 5)))

INSERT @tbl_latLong ([StationId], [Latitude], [Longitude]) VALUES (13274, CAST(21.11510 AS Decimal(10, 5)), CAST(107.42023 AS Decimal(10, 5)))

Update [zebra].[Stations] 
	Set [Latitude]=f2.[Latitude], [Longitude]=f2.[Longitude]
	From [zebra].[Stations] as f1, @tbl_latLong as f2
	Where f1.StationId=f2.StationId

END
