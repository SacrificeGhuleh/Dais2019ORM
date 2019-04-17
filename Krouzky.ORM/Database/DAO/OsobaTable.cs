namespace Krouzky.ORM.Database.DAO
{
    #region UsingRegion

    using System.Collections.ObjectModel;
    using System.Data.SqlClient;
    using Krouzky.ORM.Database.DTO;

    #endregion

    public class OsobaTable : Table<Osoba>
    {
        public OsobaTable() : base("projekt.Osoba", /*Jmeno tabulky*/
            "SELECT * FROM projekt.Osoba", /*jednoduchy select*/
            "SELECT * FROM projekt.Osoba WHERE idOsoba = @idOsoba", /*select s primarnim klicem*/
            "INSERT INTO projekt.Osoba VALUES (@jmeno, @prostredniJmeno, @prijmeni, @email, @telefonPracovni, @telefonOsobni)" /*Insert*/,
            "UPDATE projekt.Osoba SET jmeno = @jmeno, prostredniJmeno = @prostredniJmeno, prijmeni = @prijmeni, email = @email, telefonPracovni = @telefonPracovni, telefonOsobni = @telefonOsobni WHERE idOsoba = @idOsoba" /*Update*/,
            "DELETE FROM projekt.Osoba WHERE idOsoba=@idOsoba" /*Delete*/
        )
        {
        }

        protected override Collection<Osoba> Read(SqlDataReader reader)
        {
            Collection<Osoba> results = new Collection<Osoba>();
            while (reader.Read())
            {
                Osoba result = new Osoba((int) reader["idOsoba"], reader["jmeno"] as string,
                    reader["prostredniJmeno"] as string, reader["prijmeni"] as string, reader["email"] as string,
                    reader["telefonOsobni"] as string, reader["telefonPracovni"] as string);

                results.Add(result);
            }

            return results;
        }

        protected override void PrepareCommand(SqlCommand command, Osoba dbObject)
        {
            if ((command == null) || (dbObject == null))
            {
                return;
            }

            command.Parameters.AddWithValue("@idOsoba", dbObject.idOsoba);
            command.Parameters.AddWithValue("@jmeno", dbObject.jmeno);
            command.Parameters.AddWithValue("@prostredniJmeno", dbObject.prostredniJmeno);
            command.Parameters.AddWithValue("@prijmeni", dbObject.prijmeni);
            command.Parameters.AddWithValue("@email", dbObject.email);
            command.Parameters.AddWithValue("@telefonOsobni", dbObject.telefonOsobni);
            command.Parameters.AddWithValue("@telefonPracovni", dbObject.telefonPracovni);
        }
    }
}