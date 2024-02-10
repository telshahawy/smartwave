CREATE VIEW HomeVisits.ChemistVisitsScheduleView AS 

SELECT        v.VisitId,
			  v.VisitDate,
			  v.VisitCode,
			  v.PatientId,
			  p.Name AS PatientName,
			  p.PatientNo,
			  t.PhoneNumber,
			  v.ChemistId,
			  vst.VisitStatusTypeId,
			  vst.StatusNameEn,
			  vst.StatusNameAr,
			  v.PatientAddressId,
			  gz.GeoZoneId, 
			  gz.NameEn AS GeoZoneNameEn,
			  gz.NameAr AS GeoZoneNameAr,
			  pa.Building,
			  pa.street,
			  g.GoverNameEn,
			  g.GoverNameAr,
			  v.TimeZoneGeoZoneId,
			  TZF.StartTime,
			  TZF.EndTime,
			  v.VisitTime
FROM            HomeVisits.Visits AS v INNER JOIN
                         HomeVisits.TimeZoneFrames AS TZF ON TZF.TimeZoneFrameId = v.TimeZoneGeoZoneId INNER JOIN
                         HomeVisits.PatientAddress AS pa ON v.PatientAddressId = pa.PatientAddressId INNER JOIN
                             (SELECT        VisitId,
                                                             (SELECT        TOP (1) VisitStatusId
                                                               FROM            (SELECT        vs.VisitId, vs.VisitStatusId, vs.CreationDate
                                                                                         FROM            HomeVisits.VisitStatus AS vs INNER JOIN
                                                                                                                   HomeVisits.VisitStatusTypes AS vst ON vs.VisitStatusTypeId = vst.VisitStatusTypeId
                                                                                         GROUP BY vs.VisitId, vs.VisitStatusId, vs.CreationDate) AS x
                                                               WHERE        (VisitId = v.VisitId)
                                                               ORDER BY CreationDate DESC) AS VisitStatusId,
                                                             (SELECT        TOP (1) VisitStatusTypeId
                                                               FROM            (SELECT        vs.VisitId, vs.VisitStatusId, vs.CreationDate, vs.VisitStatusTypeId
                                                                                         FROM            HomeVisits.VisitStatus AS vs INNER JOIN
                                                                                                                   HomeVisits.VisitStatusTypes AS vst ON vs.VisitStatusTypeId = vst.VisitStatusTypeId
                                                                                         GROUP BY vs.VisitId, vs.VisitStatusId, vs.VisitStatusTypeId, vs.CreationDate) AS x
                                                               WHERE        (VisitId = v.VisitId)
                                                               ORDER BY CreationDate DESC) AS VisitStatusTypeId
                               FROM            HomeVisits.Visits AS v) AS vs ON vs.VisitId = v.VisitId INNER JOIN
                         HomeVisits.VisitStatusTypes AS vst ON vs.VisitStatusTypeId = vst.VisitStatusTypeId INNER JOIN
                         HomeVisits.Patients AS p ON v.PatientId = p.PatientId INNER JOIN
                             (SELECT        VisitId,
                                                             (SELECT        TOP (1) PhoneNumber
                                                               FROM            (SELECT        vs.PhoneNumber, vs.PatientId, vs.CreatedAt
                                                                                         FROM            HomeVisits.PatientPhones AS vs INNER JOIN
                                                                                                                   HomeVisits.Patients AS vst ON vs.PatientId = vst.PatientId
                                                                                         GROUP BY vs.PatientId, vs.PhoneNumber, vs.CreatedAt) AS x
                                                               WHERE        (PatientId = v.PatientId)
                                                               ORDER BY CreatedAt) AS PhoneNumber
                               FROM            HomeVisits.Visits AS v) AS t ON t.VisitId = v.VisitId LEFT OUTER JOIN
                         HomeVisits.Chemists AS c ON c.ChemistId = v.ChemistId LEFT OUTER JOIN
                         HomeVisits.Users AS u ON u.UserId = c.ChemistId INNER JOIN
                         HomeVisits.GeoZones AS gz ON gz.GeoZoneId = pa.GeoZoneId INNER JOIN
                         HomeVisits.Governats AS g ON gz.GovernateId = g.GovernateId LEFT OUTER JOIN
                         HomeVisits.AgeSegments AS ags ON ags.AgeSegmentId = v.RelativeAgeSegmentId