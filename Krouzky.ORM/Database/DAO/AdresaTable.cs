#region UsingRegion

using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public class AdresaTable : Table<Adresa> {
        public AdresaTable() : base("projekt.Adresa", /*Jmeno tabulky*/
            "SELECT * FROM projekt.Adresa", /*jednoduchy select*/
            "SELECT * FROM projekt.Adresa WHERE idAdresa = @idAdresa", /*select s primarnim klicem*/
            "INSERT INTO projekt.Adresa VALUES (@ulice, @cisloPopisne, @mesto, @stat, @psc)" /*Insert*/,
            "UPDATE projekt.Adresa SET ulice = @ulice, cisloPopisne = @cisloPopisne, mesto = @mesto, stat = @stat, psc = @psc WHERE idAdresa = @idAdresa" /*Update*/,
            "DELETE FROM projekt.Adresa WHERE idAdresa=@idAdresa" /*Delete*/,
            "@idAdresa" /*select by id nazev parametru*/
        ) {
        }

        protected override void PrepareCommand(SqlCommand command, Adresa dbObject) {
            if (command == null || dbObject == null) return;

            command.Parameters.AddWithValue("@idAdresa", dbObject.idAdresa);
            command.Parameters.AddWithValue("@ulice", dbObject.ulice);
            command.Parameters.AddWithValue("@cisloPopisne", dbObject.cisloPopisne);
            command.Parameters.AddWithValue("@mesto", dbObject.mesto);
            command.Parameters.AddWithValue("@stat", dbObject.stat);
            command.Parameters.AddWithValue("@psc", dbObject.psc.HasValue ? (object) dbObject.psc : DBNull.Value);
        }

        protected override Collection<Adresa> Read(SqlDataReader reader) {
            Collection<Adresa> results = new Collection<Adresa>();
            while (reader.Read()) {
                Adresa result = new Adresa((int) reader["idAdresa"], reader["ulice"] as string,
                    (int) reader["cisloPopisne"], (string) reader["mesto"], reader["stat"] as string,
                    reader["psc"] as int?);

                results.Add(result);
            }

            return results;
        }
    }
}