#region UsingRegion

using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public class DenVTydnuTable : Table<DenVTydnu> {
        public DenVTydnuTable() : base("projekt.DenVTydnu", /*Jmeno tabulky*/
            "SELECT * FROM projekt.DenVTydnu", /*jednoduchy select*/
            "SELECT * FROM projekt.DenVTydnu WHERE idDenVTydnu = @idDenVTydnu", /*select s primarnim klicem*/
            "" /*Insert*/, "" /*Update*/, "" /*Delete*/,
            "@idDenVTydnu" /*select by id nazev parametru*/
        ) {
        }

        protected override void PrepareCommand(SqlCommand command, DenVTydnu dbObject) {
            if (command == null || dbObject == null) return;

            command.Parameters.AddWithValue("@idDenVTydnu", dbObject.idDenVTydnu);
            command.Parameters.AddWithValue("@popis", dbObject.popis);
        }

        protected override Collection<DenVTydnu> Read(SqlDataReader reader) {
            Collection<DenVTydnu> results = new Collection<DenVTydnu>();
            while (reader.Read()) {
                DenVTydnu result = new DenVTydnu((int) reader["idDenVTydnu"], reader["popis"] as string);
                results.Add(result);
            }

            return results;
        }
    }
}