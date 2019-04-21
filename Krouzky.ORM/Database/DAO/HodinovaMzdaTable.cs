#region UsingRegion

using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Krouzky.ORM.Database.DTO;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public class HodinovaMzdaTable : Table<HodinovaMzda> {
        public HodinovaMzdaTable() : base("projekt.HodinovaMzda", /*Jmeno tabulky*/
            "SELECT * FROM projekt.HodinovaMzda", /*jednoduchy select*/
            "SELECT * FROM projekt.HodinovaMzda WHERE idHodinovaMzda = @idHodinovaMzda", /*select s primarnim klicem*/
            "INSERT INTO projekt.HodinovaMzda VALUES (@idLektor, @mzda, @platnostOd, @platnostDo)" /*Insert*/,
            "UPDATE projekt.HodinovaMzda SET idLektor = @idLektor, mzda = @mzda, platnostOd = @platnostOd, platnostDo = @platnostDo WHERE idHodinovaMzda = @idHodinovaMzda" /*Update*/,
            "DELETE FROM projekt.HodinovaMzda WHERE idHodinovaMzda=@idHodinovaMzda" /*Delete*/
        ) {
        }

        protected override Collection<HodinovaMzda> Read(SqlDataReader reader) {
            Collection<HodinovaMzda> results = new Collection<HodinovaMzda>();
            while (reader.Read()) {
                HodinovaMzda result = new HodinovaMzda((int) reader["idHodinovaMzda"], (int) reader["idLektor"],
                    (int) reader["mzda"], (DateTime) reader["platnostOd"]);

                try {
                    result.platnostDo = (DateTime?) reader["platnostDo"];
                }
                catch (Exception) {
                    // ignored
                }

                results.Add(result);
            }

            return results;
        }

        protected override void PrepareCommand(SqlCommand command, HodinovaMzda dbObject) {
            if (command == null || dbObject == null) return;

            command.Parameters.AddWithValue("@idHodinovaMzda", dbObject.idHodinovaMzda);
            command.Parameters.AddWithValue("@idLektor", dbObject.idLektor);
            command.Parameters.AddWithValue("@mzda", dbObject.mzda);
            command.Parameters.AddWithValue("@platnostOd", dbObject.platnostOd);
            command.Parameters.AddWithValue("@platnostDo",
                dbObject.platnostDo == null ? DBNull.Value : (object) dbObject.platnostDo);
        }
    }
}