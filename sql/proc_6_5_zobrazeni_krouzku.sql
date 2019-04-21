CREATE OR ALTER PROCEDURE PROJEKT.PROC_6_5_ZOBRAZENI_KROUZKU(@P_DATUMOD DATE, @P_DATUMDO DATE, @P_AKTUALNIDATUM DATE)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE C_CURS_CALENDAR CURSOR LOCAL FOR SELECT *
                                                 FROM PROJEKT.KALENDAR
                                                 WHERE KALENDAR.DATUM >= @P_DATUMOD
                                                   AND KALENDAR.DATUM <= @P_DATUMDO;

        DECLARE
            @V_KALENDAR_DATUM DATE;
        DECLARE
            @V_KALENDAR_IDDENVTYDNU INTEGER;
        DECLARE
            @V_KALENDAR_DEN INTEGER;
        DECLARE
            @V_KALENDAR_MESIC INTEGER;
        DECLARE
            @V_KALENDAR_ROK INTEGER;
        DECLARE
            @V_KALENDAR_SUDY INTEGER;

        DECLARE
            @V_RET_TABLE TABLE
                         (
                             ID                 INTEGER,
                             DATUM              DATE,
                             IDKONKRETNIKROUZEK INTEGER,
                             PROBEHLY           BIT
                         );

        DECLARE
            @V_KROUZEK_IDKROUZEK INTEGER;

        DECLARE
            @V_KROUZEK_IDKONKRETNIKROUZEK INTEGER;
        DECLARE
            @V_KROUZEK_IDPRAVIDELNOST INTEGER;
        DECLARE
            @V_POCET INTEGER;

        OPEN C_CURS_CALENDAR;

        FETCH NEXT FROM C_CURS_CALENDAR INTO @V_KALENDAR_DATUM,@V_KALENDAR_IDDENVTYDNU, @V_KALENDAR_DEN, @V_KALENDAR_MESIC, @V_KALENDAR_ROK, @V_KALENDAR_SUDY;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            DECLARE C_CURS_KROUZKY CURSOR LOCAL FOR SELECT IDKROUZEK, IDPRAVIDELNOST
                                                    FROM PROJEKT.KROUZEK
                                                    WHERE KROUZEK.IDDENVTYDNU = @V_KALENDAR_IDDENVTYDNU;

            OPEN C_CURS_KROUZKY;
            FETCH NEXT FROM C_CURS_KROUZKY INTO @V_KROUZEK_IDKROUZEK,@V_KROUZEK_IDPRAVIDELNOST;
            WHILE @@FETCH_STATUS = 0
            BEGIN
                --IF (@V_KROUZEK_IDPRAVIDELNOST = 0 OR @V_KROUZEK_IDPRAVIDELNOST = 1)
                --BEGIN
                IF ((@V_KROUZEK_IDPRAVIDELNOST = 0 AND @V_KALENDAR_SUDY = 0) OR
                    (@V_KROUZEK_IDPRAVIDELNOST = 1 AND @V_KALENDAR_SUDY = 1))
                    BEGIN
                        --PRINT 'Nespravna pravidelnost'
                        FETCH NEXT FROM C_CURS_KROUZKY INTO @V_KROUZEK_IDKROUZEK,@V_KROUZEK_IDPRAVIDELNOST;
                        CONTINUE;
                    END

                IF (datediff(DAY, @V_KALENDAR_DATUM, @P_AKTUALNIDATUM) < 0)
                    BEGIN
                        PRINT cast(@V_KALENDAR_DATUM AS VARCHAR) + ' Naplanovany krouzek: ' +
                              str(@V_KROUZEK_IDKROUZEK)
                        INSERT @V_RET_TABLE VALUES (@V_KROUZEK_IDKROUZEK, @V_KALENDAR_DATUM, NULL, 0);
                        --zobrazit jako naplanovany
                    END
                ELSE
                    BEGIN
                        SELECT @V_POCET = count(*)
                        FROM PROJEKT.KONKRETNIKROUZEK
                        WHERE KONKRETNIKROUZEK.IDKROUZEK = @V_KROUZEK_IDKROUZEK
                          AND KONKRETNIKROUZEK.DATUM = @V_KALENDAR_DATUM

                        IF (@V_POCET = 0)
                            BEGIN
                                PRINT cast(@V_KALENDAR_DATUM AS VARCHAR) + ' Naplanovany krouzek: ' +
                                      str(@V_KROUZEK_IDKROUZEK)
                                INSERT @V_RET_TABLE VALUES (@V_KROUZEK_IDKROUZEK, @V_KALENDAR_DATUM, NULL, 0);
                                --zobrazit jako naplanovany
                            END
                        ELSE
                            BEGIN

                                PRINT cast(@V_KALENDAR_DATUM AS VARCHAR) + ' Probehly krouzek: ' +
                                      str(@V_KROUZEK_IDKROUZEK)

                                SELECT @V_KROUZEK_IDKONKRETNIKROUZEK = IDKONKRETNIKROUZEK
                                FROM PROJEKT.KONKRETNIKROUZEK
                                WHERE KONKRETNIKROUZEK.IDKROUZEK = @V_KROUZEK_IDKROUZEK
                                  AND KONKRETNIKROUZEK.DATUM = @V_KALENDAR_DATUM

                                INSERT @V_RET_TABLE
                                VALUES (@V_KROUZEK_IDKROUZEK, @V_KALENDAR_DATUM, @V_KROUZEK_IDKONKRETNIKROUZEK,
                                        1);
                                --zobrazit jako probehly
                            END

                    END
                --END

                FETCH NEXT FROM C_CURS_KROUZKY INTO @V_KROUZEK_IDKROUZEK,@V_KROUZEK_IDPRAVIDELNOST;
            END
            CLOSE C_CURS_KROUZKY;
            DEALLOCATE C_CURS_KROUZKY;
            FETCH NEXT FROM C_CURS_CALENDAR INTO @V_KALENDAR_DATUM,@V_KALENDAR_IDDENVTYDNU, @V_KALENDAR_DEN, @V_KALENDAR_MESIC, @V_KALENDAR_ROK, @V_KALENDAR_SUDY;
        END
        SELECT * FROM @V_RET_TABLE;
        CLOSE C_CURS_CALENDAR;
        DEALLOCATE C_CURS_CALENDAR;
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        PRINT 'PROJEKT.PROC_6_5_ZOBRAZENI_KROUZKU exception';
        THROW;
    END CATCH
END
GO
