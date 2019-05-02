#region UsingRegion

using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public class KontaktniOsobaTable : Table<KontaktniOsoba> {
        public KontaktniOsobaTable() : base("projekt.KontaktniOsoba", /*Jmeno tabulky*/
            "SELECT * FROM projekt.KontaktniOsoba", /*jednoduchy select*/
            "SELECT * FROM projekt.KontaktniOsoba WHERE idKontaktniOsoba = @idKontaktniOsoba", /*select s primarnim klicem*/
            "INSERT INTO projekt.KontaktniOsoba VALUES (@idOsoba, @idSkola, @popis)" /*Insert*/,
            "UPDATE projekt.KontaktniOsoba SET idOsoba = @idOsoba, idSkola = @idSkola, popis = @popis WHERE idKontaktniOsoba = @idKontaktniOsoba" /*Update*/,
            "DELETE FROM projekt.KontaktniOsoba WHERE idKontaktniOsoba = @idKontaktniOsoba" /*Delete*/,
            "@idKontaktniOsoba" /*select by id nazev parametru*/
        ) {
        }

        protected override void PrepareCommand(SqlCommand command, KontaktniOsoba dbObject) {
            if (command == null || dbObject == null) return;

            command.Parameters.AddWithValue("@idKontaktniOsoba", dbObject.idKontaktniOsoba);
            command.Parameters.AddWithValue("@idOsoba", dbObject.idOsoba);
            command.Parameters.AddWithValue("@idSkola", dbObject.idSkola);
            command.Parameters.AddWithValue("@popis", dbObject.popis);
        }

        protected override Collection<KontaktniOsoba> Read(SqlDataReader reader) {
            Collection<KontaktniOsoba> results = new Collection<KontaktniOsoba>();
            while (reader.Read()) {
                KontaktniOsoba result = new KontaktniOsoba((int) reader["idKontaktniOsoba"], (int) reader["idOsoba"],
                    (int) reader["idSkola"], reader["popis"] as string);
                results.Add(result);
            }

            return results;
        }
    }
}