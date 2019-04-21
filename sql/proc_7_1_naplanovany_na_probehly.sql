CREATE OR ALTER PROCEDURE PROJEKT.PROC_7_1_NAPLANOVANY_NA_PROBEHLY(@P_IDKROUZEK INTEGER,
                                                                   @P_DATUM DATE,
                                                                   @P_ZRUSEN BIT,
                                                                   @P_POCETZAKU INT)
AS
BEGIN
    --DECLARE @V_RET INTEGER = 0;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE
            @V_COUNT INTEGER;

        DECLARE
            @V_KROUZEK_PRAVIDELNOST INTEGER;
        DECLARE
            @V_KALENDAR_SUDY INTEGER;

        SELECT @V_COUNT = COUNT(*)
        FROM PROJEKT.KROUZEK
        WHERE KROUZEK.IDDENVTYDNU = (
            SELECT IDDENVTYDNU
            FROM PROJEKT.KALENDAR
            WHERE KALENDAR.DATUM = @P_DATUM)
          AND KROUZEK.IDKROUZEK = @P_IDKROUZEK;


        SELECT @V_KALENDAR_SUDY = SUDY FROM PROJEKT.KALENDAR WHERE DATUM = @P_DATUM;

        SELECT @V_KROUZEK_PRAVIDELNOST = IDPRAVIDELNOST FROM PROJEKT.KROUZEK WHERE IDKROUZEK = @P_IDKROUZEK;

        IF (@V_COUNT < 1)
            BEGIN
                PRINT 'V zadany den se krouzek nekona';
                --SET @V_RET = -1;
                --THROW;
                ROLLBACK;
                RETURN -1;
            END

        IF (@V_KROUZEK_PRAVIDELNOST = 0 OR @V_KROUZEK_PRAVIDELNOST = 1)
            BEGIN
                IF ((@V_KROUZEK_PRAVIDELNOST = 0 AND @V_KALENDAR_SUDY = 0) OR
                    (@V_KROUZEK_PRAVIDELNOST = 1 AND @V_KALENDAR_SUDY = 1))
                    BEGIN
                        PRINT 'Nespravna pravidelnost';
                        --SET @V_RET = -2;
                        --THROW;
                        ROLLBACK;
                        RETURN -2;
                    END
            END

        SELECT @V_COUNT = COUNT(*)
        FROM PROJEKT.KONKRETNIKROUZEK
        WHERE KONKRETNIKROUZEK.IDKROUZEK = @P_IDKROUZEK
          AND KONKRETNIKROUZEK.DATUM = @P_DATUM

        IF (@V_COUNT != 0)
            BEGIN
                PRINT 'Konkretni krouzek uz existuje';
                --SET @V_RET = -3;
                --THROW;
                ROLLBACK;
                RETURN -3;
            END

        INSERT INTO PROJEKT.KONKRETNIKROUZEK(IDKROUZEK, DATUM, ZRUSEN, POCETZAKU)
        VALUES (@P_IDKROUZEK, @P_DATUM, @P_ZRUSEN, @P_POCETZAKU);
        COMMIT;
        RETURN 0;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        PRINT 'PROJEKT.PROC_7_1_NAPLANOVANY_NA_PROBEHLY exception';
        THROW;
        --RETURN @V_RET;
    END CATCH
END
GO
