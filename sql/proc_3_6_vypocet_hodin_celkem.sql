CREATE OR ALTER PROCEDURE PROJEKT.PROC_3_6_VYPOCET_HODIN_CELKEM(@P_IDOSOBA INTEGER,
                                                                @PO_HODINCELKEM FLOAT OUT)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE
            @V_HODINCELKEM FLOAT = 0;
        DECLARE
            @V_CASKONANIDO DATETIME;
        DECLARE
            @V_CASKONANIOD DATETIME;
        DECLARE
            @V_IDKROUZEK INT = 0;
        DECLARE
            @V_IDKONKRETNIKROUZEK INT = 0;
        DECLARE
            @V_DATUM DATE = NULL;
        DECLARE
            @V_ZRUSEN BIT;
        DECLARE
            @V_MESTO VARCHAR(20);
        DECLARE
            @V_DUR FLOAT = 0;

        DECLARE C_CURS CURSOR LOCAL FOR SELECT CASKONANIOD,
                                               CASKONANIDO,
                                               KROUZEK.IDKROUZEK,
                                               IDKONKRETNIKROUZEK,
                                               DATUM,
                                               ZRUSEN,
                                               MESTO
                                        FROM PROJEKT.KONKRETNIKROUZEK
                                                 LEFT JOIN PROJEKT.KROUZEK ON KONKRETNIKROUZEK.IDKROUZEK = KROUZEK.IDKROUZEK
                                                 LEFT JOIN PROJEKT.VYUCUJICIKROUZEK
                                                           ON KROUZEK.IDKROUZEK = VYUCUJICIKROUZEK.IDKROUZEK
                                                 LEFT JOIN PROJEKT.SKOLA ON KROUZEK.IDSKOLA = SKOLA.IDSKOLA
                                                 LEFT JOIN PROJEKT.ADRESA ON SKOLA.IDADRESA = ADRESA.IDADRESA
                                        WHERE VYUCUJICIKROUZEK.IDLEKTOR = @P_IDOSOBA;

        OPEN C_CURS;

        FETCH NEXT FROM C_CURS INTO @V_CASKONANIOD,@V_CASKONANIDO, @V_IDKROUZEK, @V_IDKONKRETNIKROUZEK, @V_DATUM, @V_ZRUSEN, @V_MESTO;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            --substring ostrava
            IF CHARINDEX('ostrava', LOWER(@V_MESTO)) > 0
                BEGIN
                    SET @V_HODINCELKEM = (@V_HODINCELKEM + 1);
                END


            SET @V_DUR = datediff(MINUTE, @V_CASKONANIOD, @V_CASKONANIDO) / 60;
            SET @V_DUR = ROUND(@V_DUR, 0);

            IF @V_ZRUSEN = 1
                BEGIN
                    SET @V_HODINCELKEM = @V_HODINCELKEM + (@V_DUR / 2);
                END
            ELSE
                BEGIN
                    SET @V_HODINCELKEM = @V_HODINCELKEM + @V_DUR;
                END

            FETCH NEXT FROM C_CURS INTO @V_CASKONANIOD,@V_CASKONANIDO, @V_IDKROUZEK, @V_IDKONKRETNIKROUZEK, @V_DATUM, @V_ZRUSEN, @V_MESTO;
        END

        SET @PO_HODINCELKEM = @V_HODINCELKEM;

        CLOSE C_CURS;
        DEALLOCATE C_CURS;
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        PRINT 'PROJEKT.PROC_3_6_VYPOCET_HODIN_CELKEM exception'
        SET @PO_HODINCELKEM = 0;
    END CATCH
END
GO

