/**
  Procedura 3.5. vypocet hodin v zadanem obdobi
 */

BEGIN
    DECLARE
        @P_OBDOBIOD DATE = '2018-01-01'
    DECLARE
        @P_OBDOBIDO DATE = '2018-01-31'
    DECLARE
        @P_IDOSOBA INT = 0
    DECLARE
        @PO_HODINCELKEM FLOAT
    DECLARE
        @RESULT INT
    EXEC
        @RESULT = PROJEKT.PROC_3_5_VYPOCET_HODIN_OBDOBI
                  @P_OBDOBIOD,
                  @P_OBDOBIDO,
                  @P_IDOSOBA,
                  @PO_HODINCELKEM OUTPUT

    SELECT @PO_HODINCELKEM AS 'CELKEM HODIN'--, @RESULT AS RESULT
END

/**
  Procedura 3.6. vypocet hodin celkem
 */

BEGIN
    DECLARE
        @P_IDOSOBA INT = 0
    DECLARE
        @PO_HODINCELKEM FLOAT
    DECLARE
        @RESULT INT
    EXEC
        @RESULT = PROJEKT.PROC_3_6_VYPOCET_HODIN_CELKEM
                  @P_IDOSOBA,
                  @PO_HODINCELKEM OUTPUT

    SELECT @PO_HODINCELKEM AS 'CELKEM HODIN'--, @RESULT AS RESULT
END

/**
Procedura 3.7. vypocet mesicniho platu
*/

BEGIN
    DECLARE
        @P_OBDOBIOD DATE = '2018-01-01'
    DECLARE
        @P_OBDOBIDO DATE = '2018-01-31'
    DECLARE
        @P_IDOSOBA INT = 0
    DECLARE
        @PO_MESICNIPLAT FLOAT
    DECLARE
        @RESULT INT
    EXEC
        @RESULT = PROJEKT.PROC_3_7_VYPOCET_PLATU
                  @P_OBDOBIOD,
                  @P_OBDOBIDO,
                  @P_IDOSOBA,
                  @PO_MESICNIPLAT OUTPUT

    SELECT @PO_MESICNIPLAT AS 'MESICNI PLAT'--,@RESULT AS RESULT
END

/**
Procedura 7.1. naplanovany na probehly
*/
BEGIN
    DECLARE
        @P_IDKROUZEK INT = 4
    DECLARE
        @P_DATUM DATE = '2018-05-08'
    DECLARE
        @P_ZRUSEN BIT = 0
    DECLARE
        @P_POCETZAKU INT = 5
    DECLARE
        @RESULT INT
    EXEC
        @RESULT = PROJEKT.PROC_7_1_NAPLANOVANY_NA_PROBEHLY
                  @P_IDKROUZEK,
                  @P_DATUM,
                  @P_ZRUSEN,
                  @P_POCETZAKU
END

    rollback;

/**
Procedura 9.4. zvyseni mzdy lektorovi
*/
BEGIN
    DECLARE
        @P_IDLEKTOR INT = 4
    DECLARE
        @P_PLATNOSTOD DATE = '2018-05-05'
    DECLARE
        @RESULT INT
    EXEC
        @RESULT = PROJEKT.PROC_9_4_ZVYSENI_MZDY_LEKTOROVI
                  @P_IDLEKTOR,
                  @P_PLATNOSTOD
END



/**
Procedura 6.5. Zobrazeni krouzku
*/
BEGIN
    DECLARE
        @P_DATUMOD DATE = '2018-05-01'
    DECLARE
        @P_DATUMDO DATE = '2018-05-20'
    DECLARE
        @P_AKTUALNIDATUM DATE = '2018-05-15'
    DECLARE
        @RESULT INT
    EXEC
        @RESULT = PROJEKT.PROC_6_5_ZOBRAZENI_KROUZKU
                  @P_DATUMOD,
                  @P_DATUMDO,
                  @P_AKTUALNIDATUM
END
