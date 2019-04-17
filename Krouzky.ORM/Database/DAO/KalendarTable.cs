namespace Krouzky.ORM.Database.DAO
{
    #region UsingRegion

    using System;
    using System.Collections.ObjectModel;
    using System.Data.SqlClient;
    using Krouzky.ORM.Database.DTO;

    #endregion

    public class KalendarTable : Table<Kalendar>
    {
        public KalendarTable() : base("projekt.Kalendar", /*Jmeno tabulky*/
            "SELECT * FROM projekt.Kalendar", /*jednoduchy select*/
            "SELECT * FROM projekt.Kalendar WHERE datum = @datum", /*select s primarnim klicem*/
            "" /*Insert*/, "" /*Update*/, "" /*Delete*/
        )
        {
        }

        protected override void PrepareCommand(SqlCommand command, Kalendar dbObject)
        {
            if ((command == null) || (dbObject == null))
            {
                return;
            }

            command.Parameters.AddWithValue("@datum", dbObject.datum);
            command.Parameters.AddWithValue("@idDenVTydnu", dbObject.idDenVTydnu);
            command.Parameters.AddWithValue("@den", dbObject.den);
            command.Parameters.AddWithValue("@mesic", dbObject.mesic);
            command.Parameters.AddWithValue("@rok", dbObject.rok);
            command.Parameters.AddWithValue("@sudy", dbObject.sudy);
        }

        protected override Collection<Kalendar> Read(SqlDataReader reader)
        {
            Collection<Kalendar> results = new Collection<Kalendar>();
            while (reader.Read())
            {
                Kalendar result = new Kalendar((DateTime) reader["datum"], (int) reader["idDenVTydnu"],
                    (int) reader["den"], (int) reader["mesic"], (int) reader["rok"], (bool) reader["sudy"]);

                results.Add(result);
            }

            return results;
        }
    }
}