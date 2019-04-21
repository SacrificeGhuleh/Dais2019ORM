#region UsingRegion

using System;
using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class KontaktniOsoba : Connectable {
        public static Dictionary<int, KontaktniOsoba> instances;

        public KontaktniOsoba(int idKontaktniOsoba, int idOsoba, int idSkola, string popis) {
            this.idKontaktniOsoba = idKontaktniOsoba;
            this.idOsoba = idOsoba;
            this.idSkola = idSkola;
            this.popis = popis ?? throw new ArgumentNullException(nameof(popis));

            if (instances == null) instances = new Dictionary<int, KontaktniOsoba>();

            instances.Add(idKontaktniOsoba, this);
        }

        public int idKontaktniOsoba { get; set; }
        public int idOsoba { get; set; }
        public int idSkola { get; set; }
        public string popis { get; set; }

        public Osoba osoba { get; set; }

        public override void connectObjects() {
            if (Osoba.instances != null) this.osoba = Osoba.instances[this.idOsoba];
        }

        public override string ToString() {
            return "<KontaktniOsoba> idKontaktniOsoba: " + this.idKontaktniOsoba +
                   " |idOsoba: " + this.idOsoba + " |idSkola: " + this.idSkola + " |popis: " +
                   this.popis;
        }
    }
}