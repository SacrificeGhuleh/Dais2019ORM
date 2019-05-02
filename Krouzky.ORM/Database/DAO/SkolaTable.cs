#region UsingRegion

using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public class SkolaTable : Table<Skola> {
        public SkolaTable() : base("projekt.Skola", /*Jmeno tabulky*/
            "SELECT * FROM projekt.Skola", /*jednoduchy select*/
            "SELECT * FROM projekt.Skola WHERE idSkola = @idSkola", /*select s primarnim klicem*/
            "INSERT INTO projekt.Skola VALUES (@idAdresa)" /*Insert*/,
            "UPDATE projekt.Skola SET idAdresa = @idAdresa WHERE idSkola = @idSkola" /*Update*/,
            "DELETE FROM projekt.Skola WHERE idSkola=@idSkola" /*Delete*/,
            "@idSkola" /*select by id nazev parametru*/
        ) {
        }

        protected override Collection<Skola> Read(SqlDataReader reader) {
            Collection<Skola> results = new Collection<Skola>();
            while (reader.Read()) {
                Skola result = new Skola((int) reader["idSkola"], (int) reader["idAdresa"]);

                results.Add(result);
            }

            return results;
        }

        protected override void PrepareCommand(SqlCommand command, Skola dbObject) {
            if (command == null || dbObject == null) return;

            command.Parameters.AddWithValue("@idAdresa", dbObject.idAdresa);
            command.Parameters.AddWithValue("@idSkola", dbObject.idSkola);
        }
    }
}