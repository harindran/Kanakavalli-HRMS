-------Default Series--------------------------------------------------------------------
CREATE  PROCEDURE "MIPL_GetDefaultSeries" (IN objectcode varchar(10), IN userid varchar(100), IN Date DATE) AS dftseris varchar(100);
indicator varchar(100);
groupid varchar(100);
BEGIN 
SELECT "Indicator" INTO indicator FROM OFPR WHERE :Date between "F_RefDate" and "T_RefDate";

SELECT Top 1 IFNULL(T1."Series", T0."DfltSeries") INTO dftseris FROM "ONNM" T0 LEFT OUTER JOIN (SELECT "ObjectCode", "Series" FROM "NNM2"
 T0 INNER JOIN OUSR T1 ON T0."UserSign" = T1."USERID" WHERE T0."ObjectCode" = :objectcode AND T1."USER_CODE" = :userid ) 
 AS T1 ON T0."ObjectCode" = T1."ObjectCode";

SELECT  "GroupCode", (CASE WHEN "Indicator" = :indicator THEN :dftseris ELSE '-1' END) INTO groupid, dftseris FROM "NNM1" where "Indicator"=:indicator
and  "ObjectCode" = :objectcode;

IF :dftseris = '-1' THEN SELECT (SELECT TOP 1 "Series" FROM "NNM1" WHERE "ObjectCode" = :objectcode AND "GroupCode" = :groupid 
AND "Indicator" = :indicator) INTO dftseris FROM DUMMY;
END IF;

SELECT  "Series", "SeriesName", :dftseris AS "dflt" FROM "NNM1" WHERE "ObjectCode" = :objectcode AND "Indicator" = :indicator;


END;