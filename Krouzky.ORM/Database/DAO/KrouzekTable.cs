namespace Krouzky.ORM.Database.DAO
{
    #region UsingRegion

    using System;
    using System.Collections.ObjectModel;
    using System.Data.SqlClient;
    using Krouzky.ORM.Database.DTO;

    #endregion

    public class KrouzekTable : Table<Krouzek>
    {
        public KrouzekTable() : base("projekt.Krouzek", /*Jmeno tabulky*/
            "SELECT * FROM projekt.Krouzek", /*jednoduchy select*/
            "SELECT * FROM projekt.Krouzek WHERE idKrouzek = @idKrouzek", /*select s primarnim klicem*/
            "INSERT INTO projekt.Krouzek VALUES (@idSkola, @idPravidelnost, @idDenVTydnu, @casKonaniOd, @casKonaniDo)" /*Insert*/,
            "UPDATE projekt.Krouzek SET idSkola = @idSkola, idPravidelnost = @idPravidelnost, idDenVTydnu = @idDenVTydnu, casKonaniOd = @casKonaniOd, casKonaniDo = @casKonaniDo  WHERE idKrouzek = @idKrouzek" /*Update*/,
            "DELETE FROM projekt.Krouzek WHERE idKrouzek = @idKrouzek" /*Delete*/
        )
        {
        }

        protected override void PrepareCommand(SqlCommand command, Krouzek dbObject)
        {
            if ((command == null) || (dbObject == null))
            {
                return;
            }

            command.Parameters.AddWithValue("@idKrouzek", dbObject.idKrouzek);
            command.Parameters.AddWithValue("@idSkola", dbObject.idSkola);
            command.Parameters.AddWithValue("@idPravidelnost", dbObject.idPravidelnost);
            command.Parameters.AddWithValue("@idDenVTydnu", dbObject.idDenVTydnu);
            command.Parameters.AddWithValue("@casKonaniOd", dbObject.casKonaniOd);
            command.Parameters.AddWithValue("@casKonaniDo", dbObject.casKonaniDo);
        }

        protected override Collection<Krouzek> Read(SqlDataReader reader)
        {
            Collection<Krouzek> results = new Collection<Krouzek>();
            while (reader.Read())
            {
                Krouzek result = new Krouzek((int) reader["idKrouzek"], (int) reader["idSkola"],
                    (int) reader["idPravidelnost"], (int) reader["idDenVTydnu"], (DateTime) reader["casKonaniOd"],
                    (DateTime) reader["casKonaniDo"]);
                results.Add(result);
            }

            return results;
        }
    }
}