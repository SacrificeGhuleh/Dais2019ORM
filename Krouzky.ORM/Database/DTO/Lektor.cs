namespace Krouzky.ORM.Database.DTO
{
    #region UsingRegion

    using System.Collections.Generic;

    #endregion

    public class Lektor : Connectable
    {
        public static Dictionary<int, Lektor> instances;

        public Lektor(int idLektor, int idOsoba, string popis)
        {
            this.idLektor = idLektor;
            this.idOsoba = idOsoba;
            this.popis = popis; //?? throw new ArgumentNullException(nameof(popis));

            if (instances == null)
            {
                instances = new Dictionary<int, Lektor>();
            }

            instances.Add(idLektor, this);
        }

        public Osoba osoba { get; set; }

        public int idLektor { get; set; }
        public int idOsoba { get; set; }
        public string popis { get; set; }

        public override void connectObjects()
        {
            if (Osoba.instances != null)
            {
                this.osoba = Osoba.instances[this.idOsoba];
            }
        }

        public override string ToString() => "<Lektor> idLektor: " + this.idLektor + " |idOsoba: " + this.idOsoba +
                                             " |popis: " + this.popis;
    }
}