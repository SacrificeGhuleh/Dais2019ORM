#region UsingRegion

using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public class VyucujiciKrouzekTable : Table<VyucujiciKrouzek> {
        public VyucujiciKrouzekTable() : base("projekt.VyucujiciKrouzek", /*Jmeno tabulky*/
            "SELECT * FROM projekt.VyucujiciKrouzek", /*jednoduchy select*/
            "SELECT * FROM projekt.VyucujiciKrouzek WHERE idVyucujiciKrouzek = @idVyucujiciKrouzek", /*select s primarnim klicem*/
            "INSERT INTO projekt.VyucujiciKrouzek VALUES (@idKrouzek, @idLektor, @popis)" /*Insert*/,
            "UPDATE projekt.VyucujiciKrouzek SET idKrouzek = @idKrouzek, idLektor = @idLektor, popis = @popis WHERE idVyucujiciKrouzek = @idVyucujiciKrouzek" /*Update*/,
            "DELETE FROM projekt.VyucujiciKrouzek WHERE idVyucujiciKrouzek = @idVyucujiciKrouzek" /*Delete*/,
            "@idVyucujiciKrouzek" /*select by id nazev parametru*/
        ) {
        }

        protected override void PrepareCommand(SqlCommand command, VyucujiciKrouzek dbObject) {
            if (command == null || dbObject == null) return;

            command.Parameters.AddWithValue("@idVyucujiciKrouzek", dbObject.idVyucujiciKrouzek);
            command.Parameters.AddWithValue("@idLektor", dbObject.idLektor);
            command.Parameters.AddWithValue("@idKrouzek", dbObject.idKrouzek);
            command.Parameters.AddWithValue("@popis", dbObject.popis);
        }

        protected override Collection<VyucujiciKrouzek> Read(SqlDataReader reader) {
            Collection<VyucujiciKrouzek> results = new Collection<VyucujiciKrouzek>();
            while (reader.Read()) {
                VyucujiciKrouzek result = new VyucujiciKrouzek((int) reader["idVyucujiciKrouzek"],
                    (int) reader["idKrouzek"], (int) reader["idLektor"], reader["popis"] as string);
                results.Add(result);
            }

            return results;
        }
    }
}