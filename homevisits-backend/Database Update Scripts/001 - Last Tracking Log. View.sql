CREATE VIEW [HomeVisits].[ChemistsLastTrackingLogView] as 

SELECT        c.ChemistId, u.Name, u.PhoneNumber, ctl.Latitude, ctl.Longitude, ctl.MobileBatteryPercentage, ctl.CreationDate,
			  v.VisitNo, v.VisitDate, v.VisitTime, gz.NameAr AS AreaNameAr, gz.NameEn as AreaNameEN
FROM            HomeVisits.Chemists AS c INNER JOIN
                         HomeVisits.Users AS u ON c.ChemistId = u.UserId INNER JOIN
                         HomeVisits.ChemistTrackingLog AS ctl ON c.ChemistId = ctl.ChemistId INNER JOIN
						 HomeVisits.ChemistAssignedGeoZones cagz ON c.ChemistId = cagz.ChemistId INNER JOIN
						 HomeVisits.GeoZones gz ON cagz.GeoZoneId = gz.GeoZoneId INNER JOIN
                         HomeVisits.Visits AS v ON c.ChemistId = v.ChemistId INNER JOIN
                             (SELECT        c.ChemistId, MAX(ctl.CreationDate) AS LastTrackingLog
                               FROM            HomeVisits.Chemists AS c INNER JOIN
                                                         HomeVisits.ChemistTrackingLog AS ctl ON c.ChemistId = ctl.ChemistId
                               GROUP BY c.ChemistId) AS tl ON tl.ChemistId = c.ChemistId AND ctl.CreationDate = tl.LastTrackingLog INNER JOIN
                             (SELECT        ChemistId, MAX(VisitDate) AS VisitDate, MAX(VisitTime) AS VisitTime
                               FROM            HomeVisits.Visits AS v
                               GROUP BY ChemistId) AS MAXV ON MAXV.ChemistId = v.ChemistId AND v.VisitDate = MAXV.VisitDate AND COALESCE (v.VisitTime, '00:00:00.0000000') = COALESCE (MAXV.VisitTime, '00:00:00.0000000')
						WHERE cagz.IsDeleted = 0. AND gz.IsDeleted = 0 AND u.IsDeleted = 0 AND u.IsActive = 1
