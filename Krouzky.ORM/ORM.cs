#region UsingRegion

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DAO;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM {
    #region UsingRegion

    #endregion

    public class ORM {
        private static readonly Random random = new Random();
        private static ORM instance_;

        private ORM() {
            this.dao = new DAO();
            this.dto = new DTO(this.dao);
        }

        public static ORM instance => instance_ ?? (instance_ = new ORM());

        public DTO dto { get; }
        public DAO dao { get; }


        public int calculateHoursInPeriod(DateTime obdobiOd, DateTime obdobiDo, int idLektor) {
            Console.WriteLine("Funkce 3.5. Vypocet oducenych hodin v zadanem obdobi.");
            Console.WriteLine("\tOd:{0}", obdobiOd);
            Console.WriteLine("\tDo:{0}", obdobiDo);
            Console.WriteLine("\tIdLektor:{0}", idLektor);

            /*PROJEKT.PROC_3_5_VYPOCET_HODIN_OBDOBI(@P_OBDOBIOD DATE,
                                            @P_OBDOBIDO DATE,
                                            @P_IDOSOBA INTEGER,
                                            @PO_HODINCELKEM FLOAT OUT)*/

            Database.Database db = this.dao.db;

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("PROJEKT.PROC_3_5_VYPOCET_HODIN_OBDOBI");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            //3. create input parameters
            SqlParameter dbInputObdobiOd = new SqlParameter();
            dbInputObdobiOd.ParameterName = "@P_OBDOBIOD";
            dbInputObdobiOd.DbType = DbType.Date;
            dbInputObdobiOd.Value = obdobiOd;
            dbInputObdobiOd.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputObdobiOd);


            SqlParameter dbInputObdobiDo = new SqlParameter();
            dbInputObdobiDo.ParameterName = "@P_OBDOBIDO";
            dbInputObdobiDo.DbType = DbType.Date;
            dbInputObdobiDo.Value = obdobiDo;
            dbInputObdobiDo.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputObdobiDo);


            SqlParameter dbInputidLektor = new SqlParameter();
            dbInputidLektor.ParameterName = "@P_IDOSOBA";
            dbInputidLektor.DbType = DbType.Int32;
            dbInputidLektor.Value = idLektor;
            dbInputidLektor.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputidLektor);

            //4. create output parameters
            SqlParameter dbOutputHodinCelkem = new SqlParameter();
            dbOutputHodinCelkem.ParameterName = "@PO_HODINCELKEM";
            dbOutputHodinCelkem.DbType = DbType.Double;
            dbOutputHodinCelkem.Direction = ParameterDirection.Output;
            command.Parameters.Add(dbOutputHodinCelkem);

            int ret = db.ExecuteNonQuery(command);

            // 5. get values of the output parameters
            double result = (double) command.Parameters["@PO_HODINCELKEM"].Value;
            Console.WriteLine("\tVysledek:{0}", result);
            return (int) Math.Ceiling(result);
        }

        public int calculateHoursTotal(int idLektor) {
            /*PROJEKT.PROC_3_6_VYPOCET_HODIN_CELKEM(@P_IDOSOBA INTEGER,
            @PO_HODINCELKEM FLOAT OUT)*/

            Console.WriteLine("Funkce 3.6. Vypocet oducenych hodin celkem.");
            Console.WriteLine("\tIdLektor:{0}", idLektor);

            Database.Database db = this.dao.db;

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("PROJEKT.PROC_3_6_VYPOCET_HODIN_CELKEM");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            //3. create input parameters
            SqlParameter dbInputidLektor = new SqlParameter();
            dbInputidLektor.ParameterName = "@P_IDOSOBA";
            dbInputidLektor.DbType = DbType.Int32;
            dbInputidLektor.Value = idLektor;
            dbInputidLektor.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputidLektor);

            //4. create output parameters
            SqlParameter dbOutputHodinCelkem = new SqlParameter();
            dbOutputHodinCelkem.ParameterName = "@PO_HODINCELKEM";
            dbOutputHodinCelkem.DbType = DbType.Double;
            dbOutputHodinCelkem.Direction = ParameterDirection.Output;
            command.Parameters.Add(dbOutputHodinCelkem);

            int ret = db.ExecuteNonQuery(command);

            double result = (double) command.Parameters["@PO_HODINCELKEM"].Value;
            Console.WriteLine("\tVysledek:{0}", result);
            return (int) Math.Ceiling(result);
        }

        public int calculateSalary(DateTime obdobiOd, DateTime obdobiDo, int idLektor) {
            /*
             * PROJEKT.PROC_3_7_VYPOCET_PLATU(@P_OBDOBIOD DATE,
                                                 @P_OBDOBIDO DATE,
                                                 @P_IDOSOBA INTEGER,
                                                 @PO_MESICNIPLAT FLOAT OUT)
             */
            Console.WriteLine("Funkce 3.7. Vypocet mesicniho platu.");
            Console.WriteLine("\tOd:{0}", obdobiOd);
            Console.WriteLine("\tDo:{0}", obdobiDo);
            Console.WriteLine("\tIdLektor:{0}", idLektor);


            Database.Database db = this.dao.db;

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("PROJEKT.PROC_3_7_VYPOCET_PLATU");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            //3. create input parameters
            SqlParameter dbInputObdobiOd = new SqlParameter();
            dbInputObdobiOd.ParameterName = "@P_OBDOBIOD";
            dbInputObdobiOd.DbType = DbType.Date;
            dbInputObdobiOd.Value = obdobiOd;
            dbInputObdobiOd.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputObdobiOd);


            SqlParameter dbInputObdobiDo = new SqlParameter();
            dbInputObdobiDo.ParameterName = "@P_OBDOBIDO";
            dbInputObdobiDo.DbType = DbType.Date;
            dbInputObdobiDo.Value = obdobiDo;
            dbInputObdobiDo.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputObdobiDo);


            SqlParameter dbInputidLektor = new SqlParameter();
            dbInputidLektor.ParameterName = "@P_IDOSOBA";
            dbInputidLektor.DbType = DbType.Int32;
            dbInputidLektor.Value = idLektor;
            dbInputidLektor.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputidLektor);

            //4. create output parameters
            SqlParameter dbOutputMesicniPlat = new SqlParameter();
            dbOutputMesicniPlat.ParameterName = "@PO_MESICNIPLAT";
            dbOutputMesicniPlat.DbType = DbType.Double;
            dbOutputMesicniPlat.Direction = ParameterDirection.Output;
            command.Parameters.Add(dbOutputMesicniPlat);

            int ret = db.ExecuteNonQuery(command);

            // 5. get values of the output parameters
            double result = (double) command.Parameters["@PO_MESICNIPLAT"].Value;
            Console.WriteLine("\tVysledek:{0}", result);
            return (int) Math.Ceiling(result);
        }

        public Tuple<Collection<KonkretniKrouzekPrototype>, Collection<KonkretniKrouzek>> getPlannedAndPassed(
            DateTime datumOd, DateTime datumDo, DateTime aktualniDatum) {
            /*
             * PROCEDURE PROJEKT.PROC_6_5_ZOBRAZENI_KROUZKU(@P_DATUMOD DATE,
             *                                      @P_DATUMDO DATE,
             *                                      @P_AKTUALNIDATUM DATE)
             */

            Collection<KonkretniKrouzekPrototype> naplanovaneKrouzky = new Collection<KonkretniKrouzekPrototype>();
            Collection<KonkretniKrouzek> probehleKrouzky = new Collection<KonkretniKrouzek>();

            Tuple<Collection<KonkretniKrouzekPrototype>, Collection<KonkretniKrouzek>> krouzkyTuple =
                new Tuple<Collection<KonkretniKrouzekPrototype>, Collection<KonkretniKrouzek>>(naplanovaneKrouzky,
                    probehleKrouzky);

            Console.WriteLine("Funkce 6.5. Zobrazeni naplanovanych a probehlych krouzku.");
            Console.WriteLine("\tOd:{0}", datumOd);
            Console.WriteLine("\tDo:{0}", datumDo);
            Console.WriteLine("\tAktualniDatum:{0}", aktualniDatum);

            Database.Database db = this.dao.db;

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("PROJEKT.PROC_6_5_ZOBRAZENI_KROUZKU");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            //3. create input parameters
            SqlParameter dbInputObdobiOd = new SqlParameter();
            dbInputObdobiOd.ParameterName = "@P_DATUMOD";
            dbInputObdobiOd.DbType = DbType.Date;
            dbInputObdobiOd.Value = datumOd;
            dbInputObdobiOd.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputObdobiOd);


            SqlParameter dbInputObdobiDo = new SqlParameter();
            dbInputObdobiDo.ParameterName = "@P_DATUMDO";
            dbInputObdobiDo.DbType = DbType.Date;
            dbInputObdobiDo.Value = datumDo;
            dbInputObdobiDo.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputObdobiDo);


            SqlParameter dbInputAktualniDatum = new SqlParameter();
            dbInputAktualniDatum.ParameterName = "@P_AKTUALNIDATUM";
            dbInputAktualniDatum.DbType = DbType.Date;
            dbInputAktualniDatum.Value = aktualniDatum;
            dbInputAktualniDatum.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputAktualniDatum);

            int ret = db.ExecuteNonQuery(command);

            SqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read()) {
                Console.WriteLine("Datum: {0} ID: {1} Konkretni ID: {2} Probehly: {3}", rdr["ID"],
                    rdr["IDKONKRETNIKROUZEK"], rdr["DATUM"], rdr["PROBEHLY"]);
                bool probehly = (bool) rdr["PROBEHLY"];

                if (probehly) {
                    KonkretniKrouzek k = null;
                    int idKonkretniKrouzek = (int) rdr["IDKONKRETNIKROUZEK"];
                    //if (!KonkretniKrouzek.instances.ContainsKey(idKonkretniKrouzek))
                        this.dao.konkretniKrouzekTable.SelectOne(idKonkretniKrouzek, "@idKonkretniKrouzek", out k, db);
                    //else
                        //k = KonkretniKrouzek.instances[idKonkretniKrouzek];

                    if (k != null)
                        krouzkyTuple.Item2.Add(k);
                }
                else {
                    int id = (int) rdr["ID"];
                    DateTime datum = (DateTime) rdr["DATUM"];
                    krouzkyTuple.Item1.Add(new KonkretniKrouzekPrototype(id, datum));
                }
            }

            return krouzkyTuple;
        }

        public bool promotePlannedToPassed(int idKrouzek, DateTime datum, bool zrusen, int pocetZaku) {
            /*PROJEKT.PROC_7_1_NAPLANOVANY_NA_PROBEHLY(@P_IDKROUZEK INTEGER,
                                               @P_DATUM DATE,
                                               @P_ZRUSEN BIT,
                                               @P_POCETZAKU INT)
           */
            Console.WriteLine("Funkce 7.1. Povyseni naplanovaneho krouzku na probehly.");
            Console.WriteLine("\tidKrouzek:{0}", idKrouzek);
            Console.WriteLine("\tdatumo:{0}", datum);
            Console.WriteLine("\tzrusen:{0}", zrusen);
            Console.WriteLine("\tpocetZaku:{0}", pocetZaku);

            Database.Database db = this.dao.db;

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("PROJEKT.PROC_7_1_NAPLANOVANY_NA_PROBEHLY");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            //3. create input parameters
            SqlParameter dbInputIdKrouzek = new SqlParameter();
            dbInputIdKrouzek.ParameterName = "@P_IDKROUZEK";
            dbInputIdKrouzek.DbType = DbType.Int32;
            dbInputIdKrouzek.Value = idKrouzek;
            dbInputIdKrouzek.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputIdKrouzek);


            SqlParameter dbInputDatun = new SqlParameter();
            dbInputDatun.ParameterName = "@P_DATUM";
            dbInputDatun.DbType = DbType.Date;
            dbInputDatun.Value = datum;
            dbInputDatun.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputDatun);


            SqlParameter dbInputZrusen = new SqlParameter();
            dbInputZrusen.ParameterName = "@P_ZRUSEN";
            dbInputZrusen.DbType = DbType.Int32;
            dbInputZrusen.Value = zrusen;
            dbInputZrusen.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputZrusen);

            SqlParameter dbInputPocetZaku = new SqlParameter();
            dbInputPocetZaku.ParameterName = "@P_POCETZAKU";
            dbInputPocetZaku.DbType = DbType.Int32;
            dbInputPocetZaku.Value = pocetZaku;
            dbInputPocetZaku.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputPocetZaku);

            int ret = db.ExecuteNonQuery(command);


            Console.WriteLine("\tnavratova hodnoda:{0}", ret);
            return ret >= 0;
        }

        public bool salaryUpdate(int idLektor, DateTime platnostOd) {
            /*PROJEKT.PROC_9_4_ZVYSENI_MZDY_LEKTOROVI(@P_IDLEKTOR INTEGER, @P_PLATNOSTOD DATE)*/

            Console.WriteLine("Funkce 9.4. Zvyseni mzdy lektorovi");
            Console.WriteLine("\tidLektor:{0}", idLektor);
            Console.WriteLine("\tplatnostOd:{0}", platnostOd);


            Database.Database db = this.dao.db;

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("PROJEKT.PROC_9_4_ZVYSENI_MZDY_LEKTOROVI");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            //3. create input parameters
            SqlParameter dbInputIdLektor = new SqlParameter();
            dbInputIdLektor.ParameterName = "@P_IDLEKTOR";
            dbInputIdLektor.DbType = DbType.Int32;
            dbInputIdLektor.Value = idLektor;
            dbInputIdLektor.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputIdLektor);


            SqlParameter dbInputPlatnostOd = new SqlParameter();
            dbInputPlatnostOd.ParameterName = "@P_PLATNOSTOD";
            dbInputPlatnostOd.DbType = DbType.Date;
            dbInputPlatnostOd.Value = platnostOd;
            dbInputPlatnostOd.Direction = ParameterDirection.Input;
            command.Parameters.Add(dbInputPlatnostOd);
            int ret = db.ExecuteNonQuery(command);

            Console.WriteLine("\tnavratova hodnoda:{0}", ret);
            return ret >= 0;
        }
    
        public static string InsertString(KonkretniKrouzekPrototype krouzek) {
            bool zrusen = false;
            int pocetZaku = 0;
            //Random random = new Random((int)DateTime.Now.Ticks);
            if (random.Next(0, 50) == 25) {
                zrusen = true;
                pocetZaku = 0;
            }
            else {
                pocetZaku = random.Next(3, 10);
            }

            return
                $"INSERT INTO projekt.KonkretniKrouzek(idKrouzek,datum,zrusen,pocetZaku) VALUES ({krouzek.idKrouzek}, '{krouzek.datum:yyyy-MM-dd}', {Convert.ToInt32(zrusen)}, {pocetZaku});";
        }
    }
}