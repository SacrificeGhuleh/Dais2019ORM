namespace Krouzky.ORM.Database.DTO
{
    #region UsingRegion

    using System;
    using System.Collections.Generic;

    #endregion

    public class Osoba
    {
        public static Dictionary<int, Osoba> instances;

        public Osoba(int idOsoba, string jmeno, string prostredniJmeno, string prijmeni, string email,
            string telefonPracovni, string telefonOsobni)
        {
            this.idOsoba = idOsoba;
            this.jmeno = jmeno ?? throw new ArgumentNullException(nameof(jmeno));
            this.prostredniJmeno = prostredniJmeno; //?? throw new ArgumentNullException(nameof(prostredniJmeno));
            this.prijmeni = prijmeni ?? throw new ArgumentNullException(nameof(prijmeni));
            this.email = email ?? throw new ArgumentNullException(nameof(email));
            this.telefonPracovni = telefonPracovni; //?? throw new ArgumentNullException(nameof(telefonPracovni));
            this.telefonOsobni = telefonOsobni ?? throw new ArgumentNullException(nameof(telefonOsobni));

            if (instances == null)
            {
                instances = new Dictionary<int, Osoba>();
            }

            instances.Add(idOsoba, this);
        }

        public int idOsoba { get; set; }
        public string jmeno { get; set; }
        public string prostredniJmeno { get; set; }
        public string prijmeni { get; set; }
        public string email { get; set; }
        public string telefonPracovni { get; set; }
        public string telefonOsobni { get; set; }

        public override string ToString() => "<Osoba> idOsoba: " + this.idOsoba + " |jmeno: " + this.jmeno +
                                             " |prostredniJmeno: " + this.prostredniJmeno + " |prijmeni: " +
                                             this.prijmeni + " |email: " + this.email + " |telefonPracovni: " +
                                             this.telefonPracovni + " |telefonOsobni: " + this.telefonOsobni;
    }
}