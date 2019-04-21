#region UsingRegion

using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public class PravidelnostTable : Table<Pravidelnost> {
        public PravidelnostTable() : base("projekt.Pravidelnost", /*Jmeno tabulky*/
            "SELECT * FROM projekt.Pravidelnost", /*jednoduchy select*/
            "SELECT * FROM projekt.Pravidelnost WHERE idPravidelnost = @idPravidelnost", /*select s primarnim klicem*/
            "" /*Insert*/, "" /*Update*/, "" /*Delete*/
        ) {
        }

        protected override void PrepareCommand(SqlCommand command, Pravidelnost dbObject) {
            if (command == null || dbObject == null) return;

            command.Parameters.AddWithValue("@idPravidelnost", dbObject.idPravidelnost);
            command.Parameters.AddWithValue("@popis", dbObject.popis);
        }

        protected override Collection<Pravidelnost> Read(SqlDataReader reader) {
            Collection<Pravidelnost> results = new Collection<Pravidelnost>();
            while (reader.Read()) {
                Pravidelnost result = new Pravidelnost((int) reader["idPravidelnost"], reader["popis"] as string);
                results.Add(result);
            }

            return results;
        }
    }
}