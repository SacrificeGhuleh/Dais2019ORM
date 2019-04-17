namespace Krouzky.ORM.Database.DAO
{
    #region UsingRegion

    using System;
    using System.Collections.ObjectModel;
    using System.Data.SqlClient;
    using Krouzky.ORM.Database.DTO;

    #endregion

    public class KonkretniKrouzekTable : Table<KonkretniKrouzek>
    {
        public KonkretniKrouzekTable() : base("projekt.KonkretniKrouzek", /*Jmeno tabulky*/
            "SELECT * FROM projekt.KonkretniKrouzek", /*jednoduchy select*/
            "SELECT * FROM projekt.KonkretniKrouzek WHERE idKonkretniKrouzek = @idKonkretniKrouzek", /*select s primarnim klicem*/
            "INSERT INTO projekt.KonkretniKrouzek VALUES (@idKrouzek, @datum, @zrusen, @pocetZaku)" /*Insert*/,
            "UPDATE projekt.KonkretniKrouzek SET idKrouzek = @idKrouzek, datum = @datum, zrusen = @zrusen, pocetZaku = @pocetZaku WHERE idKonkretniKrouzek = @idKonkretniKrouzek" /*Update*/,
            "DELETE FROM projekt.KonkretniKrouzek WHERE idKonkretniKrouzek=@idKonkretniKrouzek" /*Delete*/
        )
        {
        }

        protected override void PrepareCommand(SqlCommand command, KonkretniKrouzek dbObject)
        {
            if ((command == null) || (dbObject == null))
            {
                return;
            }

            command.Parameters.AddWithValue("@idKonkretniKrouzek", dbObject.idKonkretniKrouzek);
            command.Parameters.AddWithValue("@idKrouzek", dbObject.idKrouzek);
            command.Parameters.AddWithValue("@datum", dbObject.datum);
            command.Parameters.AddWithValue("@zrusen", dbObject.zrusen);
            command.Parameters.AddWithValue("@pocetZaku", dbObject.pocetZaku);
        }

        protected override Collection<KonkretniKrouzek> Read(SqlDataReader reader)
        {
            Collection<KonkretniKrouzek> results = new Collection<KonkretniKrouzek>();
            while (reader.Read())
            {
                KonkretniKrouzek result = new KonkretniKrouzek((int) reader["idKonkretniKrouzek"],
                    (int) reader["idKrouzek"], (DateTime) reader["datum"], (bool) reader["zrusen"],
                    (int) reader["pocetZaku"]);

                results.Add(result);
            }

            return results;
        }
    }
}