CREATE OR ALTER PROCEDURE PROJEKT.PROC_3_7_VYPOCET_PLATU(@P_OBDOBIOD DATE,
                                                         @P_OBDOBIDO DATE,
                                                         @P_IDOSOBA INTEGER,
                                                         @PO_MESICNIPLAT FLOAT OUT)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE
            @V_HODINCELKEM FLOAT = 0;

        DECLARE
            @V_MZDA FLOAT = 0;

        DECLARE
            @V_PLATNOSTOD DATE;

        DECLARE
            @V_PLATNOSTDO DATE;

        DECLARE C_CURS_PLAT CURSOR LOCAL FOR SELECT MZDA, PLATNOSTOD, PLATNOSTDO
                                             FROM PROJEKT.HODINOVAMZDA
                                             WHERE HODINOVAMZDA.IDLEKTOR = @P_IDOSOBA;

        OPEN C_CURS_PLAT;

        SET @PO_MESICNIPLAT = 0;

        FETCH NEXT FROM C_CURS_PLAT INTO @V_MZDA, @V_PLATNOSTOD, @V_PLATNOSTDO;

        WHILE @@FETCH_STATUS = 0
        BEGIN

            IF (@V_PLATNOSTDO IS NULL)
                BEGIN
                    SET @V_PLATNOSTDO = '2100-01-01';
                END

            IF (datediff(DAY, @V_PLATNOSTDO, @P_OBDOBIOD) > 0)
                BEGIN
                    CONTINUE;
                END

            IF (datediff(DAY, @V_PLATNOSTOD, @P_OBDOBIOD) < 0)
                BEGIN
                    SET @V_PLATNOSTOD = @P_OBDOBIOD;
                END

            IF (datediff(DAY, @V_PLATNOSTDO, @P_OBDOBIDO) < 0)
                BEGIN
                    SET @V_PLATNOSTDO = @P_OBDOBIDO;
                END

            EXEC PROJEKT.PROC_3_6_VYPOCET_HODIN_CELKEM
                 @P_IDOSOBA,
                 @V_HODINCELKEM OUTPUT

            SET @PO_MESICNIPLAT = @PO_MESICNIPLAT + (@V_HODINCELKEM * @V_MZDA);

            FETCH NEXT FROM C_CURS_PLAT INTO @V_MZDA, @V_PLATNOSTOD, @V_PLATNOSTDO;
        END

        CLOSE C_CURS_PLAT;
        DEALLOCATE C_CURS_PLAT;
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        PRINT 'PROJEKT.PROC_3_7_VYPOCET_PLATU exception'
        SET @PO_MESICNIPLAT = 0;
        THROW;
    END CATCH
END
GO
