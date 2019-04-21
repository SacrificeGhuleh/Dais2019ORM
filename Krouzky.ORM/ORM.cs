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
        private DAO dao { get; }


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
                    if (!KonkretniKrouzek.instances.ContainsKey(idKonkretniKrouzek))
                        this.dao.konkretniKrouzekTable.SelectOne(idKonkretniKrouzek, "@idKonkretniKrouzek", out k, db);
                    else
                        k = KonkretniKrouzek.instances[idKonkretniKrouzek];

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
            return ret == 0;
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
            return ret == 0;
        }

        /**************************************************************************************************/
//        public int calculateHoursInPeriod(DateTime obdobiOd, DateTime obdobiDo, int idLektor,
//            bool callFromMethod = false)
//        {
//            if (!callFromMethod)
//            {
//                Console.WriteLine("Funkce 3.5. Vypocet oducenych hodin v zadanem obdobi.");
//                Console.WriteLine("\tOd:{0}", obdobiOd);
//                Console.WriteLine("\tDo:{0}", obdobiDo);
//                Console.WriteLine("\tIdLektor:{0}", idLektor);
//            }
//
//            double pocetHodin = 0.0;
//
//            string selectStr =
//                "SELECT casKonaniOd, casKonaniDo, krouzek.idKrouzek, idKonkretniKrouzek, datum, zrusen, mesto FROM projekt.KonkretniKrouzek LEFT JOIN projekt.Krouzek ON KonkretniKrouzek.idKrouzek = Krouzek.idKrouzek LEFT JOIN projekt.VyucujiciKrouzek ON Krouzek.idKrouzek = VyucujiciKrouzek.idKrouzek LEFT JOIN projekt.Skola ON Krouzek.idSkola = Skola.idSkola LEFT JOIN projekt.Adresa ON Skola.idAdresa = Adresa.idAdresa WHERE VyucujiciKrouzek.idLektor = @p_idOsoba AND KonkretniKrouzek.datum >= @p_obdobiOd AND KonkretniKrouzek.datum <= @p_obdobiDo";
//
//            SqlCommand command = this.dao.db.CreateCommand(selectStr);
//            command.Parameters.AddWithValue("@p_idOsoba", idLektor);
//            command.Parameters.AddWithValue("@p_obdobiOd", obdobiOd);
//            command.Parameters.AddWithValue("@p_obdobiDo", obdobiDo);
//
//            SqlDataReader reader = this.dao.db.Select(command);
//            if (reader == null)
//            {
//                Console.WriteLine("Chyba v selectu");
//                return -1;
//            }
//
//            DateTime casKonaniOd;
//            DateTime casKonaniDo;
//            bool zrusen;
//            string mesto;
//            double dur;
//
//            while (reader.Read())
//            {
//                try
//                {
//                    casKonaniOd = (DateTime) reader["casKonaniOd"];
//                    casKonaniDo = (DateTime) reader["casKonaniDo"];
//                    zrusen = (bool) reader["zrusen"];
//                    mesto = reader["mesto"] as string;
//
//                    if (!mesto.ToLower().Contains("ostrava"))
//                    {
//                        pocetHodin++;
//                    }
//
//                    dur = Math.Ceiling((casKonaniDo - casKonaniOd).TotalMinutes / 60.0);
//                    if (zrusen)
//                    {
//                        pocetHodin += dur / 2.0;
//                    }
//                    else
//                    {
//                        pocetHodin += dur;
//                    }
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e);
//                }
//            }
//
//            reader.Close();
//
//            Console.WriteLine("\tVysledek: {0}", (int) Math.Ceiling(pocetHodin));
//            return (int) Math.Ceiling(pocetHodin);
//        }
//
//        public int calculateHoursTotal(int idLektor, bool callFromMethod = false)
//        {
//            if (!callFromMethod)
//            {
//                Console.WriteLine("Funkce 3.6. Vypocet oducenych hodin celkem.");
//                Console.WriteLine("\tIdLektor:{0}", idLektor);
//            }
//
//            /*if (Lektor.instances == null) {
//                return -1;
//            }
//
//            if (!Lektor.instances.ContainsKey(idLektor)) {
//                return -1;
//            }*/
//
//            double pocetHodin = 0.0;
//
//            string selectStr =
//                "SELECT casKonaniOd, casKonaniDo, krouzek.idKrouzek, idKonkretniKrouzek, datum, zrusen, mesto FROM projekt.KonkretniKrouzek LEFT JOIN projekt.Krouzek ON KonkretniKrouzek.idKrouzek = Krouzek.idKrouzek LEFT JOIN projekt.VyucujiciKrouzek ON Krouzek.idKrouzek = VyucujiciKrouzek.idKrouzek LEFT JOIN projekt.Skola ON Krouzek.idSkola = Skola.idSkola LEFT JOIN projekt.Adresa ON Skola.idAdresa = Adresa.idAdresa WHERE VyucujiciKrouzek.idLektor = @p_idOsoba";
//
//            SqlCommand command = this.dao.db.CreateCommand(selectStr);
//            command.Parameters.AddWithValue("@p_idOsoba", idLektor);
//
//            SqlDataReader reader = this.dao.db.Select(command);
//            if (reader == null)
//            {
//                Console.WriteLine("Chyba v selectu");
//                return -1;
//            }
//
//            DateTime casKonaniOd;
//            DateTime casKonaniDo;
//            bool zrusen;
//            string mesto;
//            double dur;
//
//            while (reader.Read())
//            {
//                try
//                {
//                    casKonaniOd = (DateTime) reader["casKonaniOd"];
//                    casKonaniDo = (DateTime) reader["casKonaniDo"];
//                    zrusen = (bool) reader["zrusen"];
//                    mesto = reader["mesto"] as string;
//
//                    if (!mesto.ToLower().Contains("ostrava"))
//                    {
//                        pocetHodin++;
//                    }
//
//                    dur = Math.Ceiling((casKonaniDo - casKonaniOd).TotalMinutes / 60.0);
//                    if (zrusen)
//                    {
//                        pocetHodin += dur / 2.0;
//                    }
//                    else
//                    {
//                        pocetHodin += dur;
//                    }
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e);
//                }
//            }
//
//            reader.Close();
//
//            Console.WriteLine("\tVysledek: {0}", (int) Math.Ceiling(pocetHodin));
//            return (int) Math.Ceiling(pocetHodin);
//        }
//
//        public int calculateSalary(DateTime obdobiOd, DateTime obdobiDo, int idLektor, bool callFromMethod = false)
//        {
//            if (!callFromMethod)
//            {
//                Console.WriteLine("Funkce 3.7. Vypocet mesicniho platu.");
//                Console.WriteLine("\tOd:{0}", obdobiOd);
//                Console.WriteLine("\tDo:{0}", obdobiDo);
//                Console.WriteLine("\tIdLektor:{0}", idLektor);
//            }
//
//            double plat = 0;
//
//            string selectStr =
//                "SELECT mzda, platnostOd, platnostDo FROM projekt.HodinovaMzda WHERE HodinovaMzda.idLektor = @p_idOsoba";
//
//            SqlCommand command = this.dao.db.CreateCommand(selectStr);
//            command.Parameters.AddWithValue("@p_idOsoba", idLektor);
//
//            SqlDataReader reader = this.dao.db.Select(command);
//            if (reader == null)
//            {
//                Console.WriteLine("Chyba v selectu");
//                return -1;
//            }
//
//            int mzda;
//            DateTime platnostOd;
//            DateTime platnostDo;
//            int hours;
//
//            while (reader.Read())
//            {
//                try
//                {
//                    mzda = (int) reader["mzda"];
//                    platnostOd = (DateTime) reader["platnostOd"];
//
//                    try
//                    {
//                        platnostDo = (DateTime) reader["platnostDo"];
//                    }
//                    catch (Exception)
//                    {
//                        platnostDo = DateTime.MaxValue;
//                    }
//
//                    if (platnostDo < obdobiOd)
//                    {
//                        continue;
//                    }
//
//                    if (platnostOd > obdobiOd)
//                    {
//                        platnostOd = obdobiOd;
//                    }
//
//                    if (platnostDo > obdobiDo)
//                    {
//                        platnostDo = obdobiDo;
//                    }
//
//                    hours = this.calculateHoursInPeriod(platnostOd, platnostDo, idLektor, true);
//                    plat += hours * mzda;
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e);
//                }
//            }
//
//            reader.Close();
//
//            Console.WriteLine("\tVysledek: {0}", (int) Math.Ceiling(plat));
//            return (int) Math.Ceiling(plat);
//        }
//
//        public Tuple<Collection<KonkretniKrouzekPrototype>, Collection<KonkretniKrouzek>> getPlannedAndPassed(
//            DateTime datumOd, DateTime datumDo, DateTime aktualniDatum, bool callFromMethod = false)
//        {
//            if (!callFromMethod)
//            {
//                Console.WriteLine("Funkce 6.5. Zobrazeni naplanovanych a probehlych krouzku.");
//                Console.WriteLine("\tOd:{0}", datumOd);
//                Console.WriteLine("\tDo:{0}", datumDo);
//                Console.WriteLine("\tAktualniDatum:{0}", aktualniDatum);
//            }
//
//            Collection<KonkretniKrouzekPrototype> naplanovaneKrouzky = new Collection<KonkretniKrouzekPrototype>();
//            Collection<KonkretniKrouzek> probehleKrouzky = new Collection<KonkretniKrouzek>();
//
//            Tuple<Collection<KonkretniKrouzekPrototype>, Collection<KonkretniKrouzek>> krouzkyTuple =
//                new Tuple<Collection<KonkretniKrouzekPrototype>, Collection<KonkretniKrouzek>>(naplanovaneKrouzky,
//                    probehleKrouzky);
//
//            string selectDateStr =
//                "SELECT * FROM projekt.Kalendar WHERE Kalendar.datum >= @p_datumOd AND Kalendar.datum <= @p_datumDo";
//
//            SqlCommand selectDateCommand = this.dao.db.CreateCommand(selectDateStr);
//            selectDateCommand.Parameters.AddWithValue("@p_datumOd", datumOd);
//            selectDateCommand.Parameters.AddWithValue("@p_datumDo", datumDo);
//
//            SqlDataReader selectDateReader = this.dao.db.Select(selectDateCommand);
//            if (selectDateReader == null)
//            {
//                Console.WriteLine("Chyba v selectu");
//                return krouzkyTuple;
//            }
//
//            string selectKrouzekStr = "SELECT * FROM projekt.Krouzek WHERE Krouzek.idDenVTydnu = @v_idDenVTydnu";
//            string selectKonkretniKrouzekStr =
//                "SELECT * FROM projekt.KonkretniKrouzek WHERE KonkretniKrouzek.idKrouzek = @v_idAktualnihoKrouzku AND KonkretniKrouzek.datum = @v_datum";
//
//            Kalendar kalendar;
//            while (selectDateReader.Read())
//            {
//                try
//                {
//                    //kalendar = new Kalendar((DateTime) selectDateReader["datum"], (int) selectDateReader["idDenVTydnu"], (int) selectDateReader["den"], (int) selectDateReader["mesic"], (int) selectDateReader["rok"], (bool) selectDateReader["sudy"]);
//                    kalendar = Kalendar.instances[(DateTime) selectDateReader["datum"]];
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e);
//                    continue;
//                }
//
//                SqlCommand selectKrouzekCommand = this.dao.db.CreateCommand(selectKrouzekStr);
//                selectKrouzekCommand.Parameters.AddWithValue("@v_idDenVTydnu", kalendar.idDenVTydnu);
//
//                SqlDataReader selectKrouzekReader = this.dao.db.Select(selectKrouzekCommand);
//                if (selectKrouzekReader == null)
//                {
//                    Console.WriteLine("Chyba v selectu");
//                    continue;
//                }
//
//                Krouzek krouzek;
//                while (selectKrouzekReader.Read())
//                {
//                    try
//                    {
//                        //krouzek = new Krouzek((int) selectKrouzekReader["idKrouzek"], (int) selectKrouzekReader["idSkola"], (int) selectKrouzekReader["idPravidelnost"], (int) selectKrouzekReader["idDenVTydnu"], (DateTime) selectKrouzekReader["casKonaniOd"], (DateTime) selectKrouzekReader["casKonaniDo"]);
//                        krouzek = Krouzek.instances[(int) selectKrouzekReader["idKrouzek"]];
//                    }
//                    catch (Exception e)
//                    {
//                        Console.WriteLine(e);
//                        continue;
//                    }
//
//                    switch (krouzek.idPravidelnost)
//                    {
//                        case 0:
//                            if (!kalendar.sudy)
//                            {
//                                continue;
//                            }
//
//                            break;
//                        case 1:
//                            if (kalendar.sudy)
//                            {
//                                continue;
//                            }
//
//                            break;
//                        case 2:
//                            break;
//                        default:
//                            continue;
//                    }
//
//                    if (kalendar.datum > aktualniDatum)
//                    {
//                        naplanovaneKrouzky.Add(new KonkretniKrouzekPrototype(krouzek.idKrouzek, kalendar.datum));
//                    }
//                    else
//                    {
//                        SqlCommand selectKonkretniKrouzekCommand = this.dao.db.CreateCommand(selectKonkretniKrouzekStr);
//
//                        selectKonkretniKrouzekCommand.Parameters.AddWithValue("@v_idAktualnihoKrouzku",
//                            krouzek.idKrouzek);
//                        selectKonkretniKrouzekCommand.Parameters.AddWithValue("@v_datum", kalendar.datum);
//
//                        SqlDataReader selectKonkretniKrouzekReader = this.dao.db.Select(selectKonkretniKrouzekCommand);
//                        if (selectKonkretniKrouzekReader == null)
//                        {
//                            Console.WriteLine("Chyba selectu");
//                        }
//                        else
//                        {
//                            KonkretniKrouzek konkretniKrouzek;
//                            bool found = false;
//                            while (selectKonkretniKrouzekReader.Read())
//                            {
//                                try
//                                {
//                                    //konkretniKrouzek = new KonkretniKrouzek((int) selectKonkretniKrouzekReader["idKonkretniKrouzek"], (int) selectKonkretniKrouzekReader["idKrouzek"], (DateTime) selectKonkretniKrouzekReader["datum"], (bool) selectKonkretniKrouzekReader["zrusen"], (int) selectKonkretniKrouzekReader["pocetZaku"]);
//                                    konkretniKrouzek =
//                                        KonkretniKrouzek.instances[
//                                            (int) selectKonkretniKrouzekReader["idKonkretniKrouzek"]];
//                                }
//                                catch (Exception e)
//                                {
//                                    Console.WriteLine(e);
//                                    continue;
//                                }
//
//                                found = true;
//                                probehleKrouzky.Add(konkretniKrouzek);
//                            }
//
//                            if (!found)
//                            {
//                                naplanovaneKrouzky.Add(new KonkretniKrouzekPrototype(krouzek.idKrouzek,
//                                    kalendar.datum));
//                            }
//                        }
//                    }
//                }
//            }
//
//            Console.WriteLine("Naplanovane:");
//            foreach (var naplanovany in naplanovaneKrouzky)
//            {
//                Console.WriteLine("\t{0}", naplanovany);
//            }
//
//            Console.WriteLine("Probehle:");
//
//            foreach (var probehly in probehleKrouzky)
//            {
//                Console.WriteLine("\t{0}", probehly);
//            }
//
//            /* using (System.IO.StreamWriter file = new System.IO.StreamWriter("Krouzky.sql")) {
// 
//                 foreach (var naplanovany in naplanovaneKrouzky)
//                 {
//                     if (naplanovany.datum < aktualniDatum) {
//                         file.WriteLine(InsertString(naplanovany));
//                     }
//                 }
//             }*/
//            return krouzkyTuple;
//        }
//
//        public bool promotePlannedToPassed(int idKrouzek, DateTime datum, bool zrusen, int pocetZaku,
//            bool callFromMethod = false)
//        {
//            if (!callFromMethod)
//            {
//                Console.WriteLine("Funkce 7.1. Povyseni naplanovaneho krouzku na probehly.");
//                Console.WriteLine("\tidKrouzek:{0}", idKrouzek);
//                Console.WriteLine("\tdatumo:{0}", datum);
//                Console.WriteLine("\tzrusen:{0}", zrusen);
//                Console.WriteLine("\tpocetZaku:{0}", pocetZaku);
//            }
//
//            string selectCountKrouzekStr =
//                "SELECT COUNT (*) FROM projekt.Krouzek WHERE Krouzek.idDenVTydnu = (SELECT idDenVTydnu FROM projekt.Kalendar WHERE Kalendar.datum = @p_datum ) AND Krouzek.idkrouzek = @p_idKrouzek";
//            SqlCommand selectCountKrouzekCommand = this.dao.db.CreateCommand(selectCountKrouzekStr);
//            selectCountKrouzekCommand.Parameters.AddWithValue("@p_idKrouzek", idKrouzek);
//            selectCountKrouzekCommand.Parameters.AddWithValue("@p_datum", datum);
//
//            SqlDataReader selectCountKrouzekReader = this.dao.db.Select(selectCountKrouzekCommand);
//            if (selectCountKrouzekReader == null)
//            {
//                Console.WriteLine("Chyba v selectu");
//                return false;
//            }
//
//            int count = 0;
//            if (selectCountKrouzekReader.Read())
//            {
//                try
//                {
//                    count = (int) selectCountKrouzekReader[0];
//                }
//                catch (Exception)
//                {
//                    return false;
//                }
//            }
//
//            if (count < 1)
//            {
//                return false;
//            }
//
//            Krouzek krouzek = Krouzek.instances[idKrouzek];
//            Kalendar kalendar = Kalendar.instances[datum];
//
//            switch (krouzek.idPravidelnost)
//            {
//                case 0:
//                    if (!kalendar.sudy)
//                    {
//                        return false;
//                    }
//
//                    break;
//                case 1:
//                    if (kalendar.sudy)
//                    {
//                        return false;
//                    }
//
//                    break;
//                case 2:
//                    break;
//                default:
//                    return false;
//            }
//
//            string selectCountKonkretniKrouzekStr =
//                "SELECT COUNT (*) FROM projekt.KonkretniKrouzek WHERE KonkretniKrouzek.idKrouzek = @p_idKrouzek AND KonkretniKrouzek.datum = @p_datum";
//            SqlCommand selectCountKonkretniKrouzekCommand = this.dao.db.CreateCommand(selectCountKonkretniKrouzekStr);
//            selectCountKonkretniKrouzekCommand.Parameters.AddWithValue("@p_idKrouzek", idKrouzek);
//            selectCountKonkretniKrouzekCommand.Parameters.AddWithValue("@p_datum", datum);
//
//            SqlDataReader selectCountKonkretniKrouzekReader = this.dao.db.Select(selectCountKonkretniKrouzekCommand);
//            if (selectCountKonkretniKrouzekReader == null)
//            {
//                Console.WriteLine("Chyba v selectu");
//                return false;
//            }
//
//            count = 1;
//            if (selectCountKonkretniKrouzekReader.Read())
//            {
//                try
//                {
//                    count = (int) selectCountKonkretniKrouzekReader[0];
//                }
//                catch (Exception)
//                {
//                    return false;
//                }
//            }
//
//            if (count != 0)
//            {
//                return false;
//            }
//
//            this.dao.konkretniKrouzekTable.Insert(new KonkretniKrouzek(KonkretniKrouzek.instances.Count, idKrouzek,
//                datum, zrusen, pocetZaku));
//            return true;
//        }
//
//        public bool salaryUpdate(int idLektor, DateTime platnostOd, bool callFromMethod = false)
//        {
//            if (!callFromMethod)
//            {
//                Console.WriteLine("Funkce 9.4. Zvyseni mzdy lektorovi");
//                Console.WriteLine("\tidLektor:{0}", idLektor);
//                Console.WriteLine("\tplatnostOd:{0}", platnostOd);
//            }
//
//            string selectStr = "SELECT COUNT (*) FROM projekt.HodinovaMzda WHERE HodinovaMzda.idLektor = @p_idLektor";
//            SqlCommand command = this.dao.db.CreateCommand(selectStr);
//            command.Parameters.AddWithValue("@p_idLektor", idLektor);
//
//            SqlDataReader reader = this.dao.db.Select(command);
//            if (reader == null)
//            {
//                Console.WriteLine("Chyba v selectu");
//                return false;
//            }
//
//            int pocetZaznamu = 0;
//            if (reader.Read())
//            {
//                try
//                {
//                    pocetZaznamu = (int) reader[0];
//                }
//                catch (Exception)
//                {
//                    return false;
//                }
//            }
//            else
//            {
//                return false;
//            }
//
//            int hodinCelkem = this.calculateHoursTotal(idLektor, true);
//
//            selectStr =
//                "SELECT HodinovaMzda.idHodinovaMzda FROM projekt.HodinovaMzda WHERE HodinovaMzda.idLektor = @p_idLektor AND HodinovaMzda.platnostDo IS NULL";
//            command = this.dao.db.CreateCommand(selectStr);
//            command.Parameters.AddWithValue("@p_idLektor", idLektor);
//
//            reader = this.dao.db.Select(command);
//            if (reader == null)
//            {
//                Console.WriteLine("Chyba v selectu");
//                return false;
//            }
//
//            int idAktualniMzda = 0;
//            if (reader.Read())
//            {
//                try
//                {
//                    idAktualniMzda = (int) reader["idHodinovaMzda"];
//                }
//                catch (Exception)
//                {
//                    return false;
//                }
//            }
//            else
//            {
//                return false;
//            }
//
//            bool narokNaZvyseni = false;
//            int hodnotaZvyseni = 0;
//
//            if ((hodinCelkem > 100) && (pocetZaznamu < 2))
//            {
//                narokNaZvyseni = true;
//                hodnotaZvyseni = 10;
//            }
//
//            if ((hodinCelkem > 500) && (pocetZaznamu < 3))
//            {
//                narokNaZvyseni = true;
//                hodnotaZvyseni = 10;
//            }
//
//            if (!narokNaZvyseni)
//            {
//                return false;
//            }
//
//            HodinovaMzda staraMzda = HodinovaMzda.instances[idAktualniMzda];
//            staraMzda.platnostDo = platnostOd;
//            this.dao.hodinovaMzdaTable.Update(staraMzda);
//
//            HodinovaMzda novaMzda = new HodinovaMzda(HodinovaMzda.instances.Count, idLektor,
//                staraMzda.mzda + hodnotaZvyseni, platnostOd.AddDays(1.0));
//            this.dao.hodinovaMzdaTable.Insert(novaMzda);
//
//            Console.WriteLine("\tLektorovi {0} byla zvysena mzda o {1} na {2}", idLektor, hodnotaZvyseni,
//                novaMzda.mzda);
//            return true;
//        }
//
//        
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