#region UsingRegion

using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public class LektorTable : Table<Lektor> {
        public LektorTable() : base("projekt.Lektor", /*Jmeno tabulky*/
            "SELECT * FROM projekt.Lektor", /*jednoduchy select*/
            "SELECT * FROM projekt.Lektor WHERE idLektor = @idLektor", /*select s primarnim klicem*/
            "INSERT INTO projekt.Lektor VALUES (@idOsoba, @popis)" /*Insert*/,
            "UPDATE projekt.Lektor SET idOsoba = @idOsoba, popis = @popis WHERE idLektor = @idLektor" /*Update*/,
            "DELETE FROM projekt.Lektor WHERE idLektor=@idLektor" /*Delete*/,
            "@idLektor" /*select by id nazev parametru*/
        ) {
        }

        protected override Collection<Lektor> Read(SqlDataReader reader) {
            Collection<Lektor> results = new Collection<Lektor>();
            while (reader.Read()) {
                Lektor result = new Lektor((int) reader["idLektor"], (int) reader["idOsoba"],
                    reader["popis"] as string);
                results.Add(result);
            }

            return results;
        }

        protected override void PrepareCommand(SqlCommand command, Lektor dbObject) {
            if (command == null || dbObject == null) return;

            command.Parameters.AddWithValue("@idLektor", dbObject.idLektor);
            command.Parameters.AddWithValue("@idOsoba", dbObject.idOsoba);
            command.Parameters.AddWithValue("@popis", dbObject.popis);
        }
    }
}