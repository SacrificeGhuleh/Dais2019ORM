CREATE OR ALTER PROCEDURE PROJEKT.PROC_9_4_ZVYSENI_MZDY_LEKTOROVI(@P_IDLEKTOR INTEGER, @P_PLATNOSTOD DATE)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE
            @V_POCETZAZNAMU INTEGER = 0;
        DECLARE
            @V_HODINCELKEM INTEGER = 0;
        DECLARE
            @V_IDAKTUALNIMZDA INTEGER = 0;
        DECLARE
            @V_HODNOTAZVYSENI INTEGER = 0;
        DECLARE
            @V_NAROKNAZVYSENI BIT = 0;
        DECLARE
            @V_OLD_HODINOVAMZDA_ID INTEGER;
        DECLARE
            @V_OLD_HODINOVAMZDA_IDLEKTOR INTEGER;
        DECLARE
            @V_OLD_HODINOVAMZDA_MZDA INTEGER;
        DECLARE
            @V_OLD_HODINOVAMZDA_PLATNOSTOD DATE;
        DECLARE
            @V_OLD_HODINOVAMZDA_PLATNOSTDO DATE;

        SELECT @V_POCETZAZNAMU = COUNT(*) FROM PROJEKT.HODINOVAMZDA WHERE HODINOVAMZDA.IDLEKTOR = @P_IDLEKTOR;
        PRINT 'Pocet zaznamu: ' + str(@V_POCETZAZNAMU);
        EXEC PROJEKT.PROC_3_6_VYPOCET_HODIN_CELKEM @P_IDLEKTOR, @V_HODINCELKEM OUTPUT;
        PRINT 'Hodin celkem: ' + str(@V_HODINCELKEM);
        SELECT @V_IDAKTUALNIMZDA = HODINOVAMZDA.IDHODINOVAMZDA
        FROM PROJEKT.HODINOVAMZDA
        WHERE HODINOVAMZDA.IDLEKTOR = @P_IDLEKTOR
          AND HODINOVAMZDA.PLATNOSTDO IS NULL;

        SET @V_NAROKNAZVYSENI = 0;
        SET @V_HODNOTAZVYSENI = 0;

        IF (@V_HODINCELKEM > 100 AND @V_POCETZAZNAMU < 2)
            BEGIN
                SET @V_NAROKNAZVYSENI = 1;
                SET @V_HODNOTAZVYSENI = 10;
            END

        IF (@V_HODINCELKEM > 500 AND @V_POCETZAZNAMU < 3)
            BEGIN
                SET @V_NAROKNAZVYSENI = 1;
                SET @V_HODNOTAZVYSENI = 20;
            END


        IF (@V_NAROKNAZVYSENI = 0)
            BEGIN
                PRINT 'Neni narok na zvyseni';
                ROLLBACK;
                RETURN -1;
            END


        UPDATE PROJEKT.HODINOVAMZDA
        SET PLATNOSTDO = @P_PLATNOSTOD
        WHERE IDHODINOVAMZDA = @V_IDAKTUALNIMZDA;

        SELECT @V_OLD_HODINOVAMZDA_ID = IDHODINOVAMZDA,
               @V_OLD_HODINOVAMZDA_IDLEKTOR = IDLEKTOR,
               @V_OLD_HODINOVAMZDA_MZDA = MZDA,
               @V_OLD_HODINOVAMZDA_PLATNOSTDO = PLATNOSTDO,
               @V_OLD_HODINOVAMZDA_PLATNOSTOD = PLATNOSTOD
        FROM PROJEKT.HODINOVAMZDA
        WHERE IDHODINOVAMZDA = @V_IDAKTUALNIMZDA;


        INSERT INTO PROJEKT.HODINOVAMZDA(IDLEKTOR, MZDA, PLATNOSTOD)
        VALUES (@P_IDLEKTOR, @V_OLD_HODINOVAMZDA_MZDA + @V_HODNOTAZVYSENI,
                DATEADD(DAY, 1, @V_OLD_HODINOVAMZDA_PLATNOSTOD));
        PRINT 'Mzda zvysena';
        COMMIT;
        RETURN 0;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        PRINT 'PROJEKT.PROC_9_4_ZVYSENI_MZDY_LEKTOROVI exception';
        THROW;
    END CATCH
END
GO
