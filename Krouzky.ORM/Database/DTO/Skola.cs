#region UsingRegion

using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class Skola : Connectable {
        public static Dictionary<int, Skola> instances;

        public Skola(int idSkola, int idAdresa) {
            this.idSkola = idSkola;
            this.idAdresa = idAdresa;

            if (instances == null) instances = new Dictionary<int, Skola>();

            instances.Add(idAdresa, this);
        }

        public int idSkola { get; set; }
        public int idAdresa { get; set; }

        public Adresa adresa { get; set; }

        public override void connectObjects() {
            if (Adresa.instances != null) this.adresa = Adresa.instances[this.idAdresa];
        }

        public override string ToString() {
            return "<Skola> idSkola: " + this.idSkola + " |idAdresa: " + this.idAdresa;
        }
    }
}