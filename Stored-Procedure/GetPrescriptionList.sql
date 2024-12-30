CREATE PROCEDURE GetPrescriptionList
AS
BEGIN
    SELECT 
        p.ID,
        p.DESCRIPTION,
        (SELECT pe.FirstName + ' ' + pe.LastName FROM PATIENT pa JOIN PERSON pe ON pa.NATIONALCODE = pe.NationalCode WHERE pa.ID = p.PATIENTID) AS PATIENTNAME,
        (SELECT pe.FirstName + ' ' + pe.LastName FROM DOCTOR d JOIN PERSON pe ON d.PersonId = pe.NationalCode WHERE d.M_S_N = p.DOCTORID) AS DOCTORNAME
    FROM 
        PRESCRIOTION p
END