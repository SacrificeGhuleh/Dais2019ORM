#region UsingRegion

using System;
using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class Kalendar /*: Connectable*/ {
        //public static Dictionary<DateTime, Kalendar> instances;

        public Kalendar(DateTime datum, int idDenVTydnu, int den, int mesic, int rok, bool sudy) {
            this.datum = datum;
            this.idDenVTydnu = idDenVTydnu;
            this.den = den;
            this.mesic = mesic;
            this.rok = rok;
            this.sudy = sudy;
            //if (instances == null) instances = new Dictionary<DateTime, Kalendar>();

            //instances.Add(datum, this);
        }

        public DenVTydnu denVTydnu { get; set; }

        public DateTime datum { get; set; }
        public int idDenVTydnu { get; set; }
        public int den { get; set; }
        public int mesic { get; set; }
        public int rok { get; set; }
        public bool sudy { get; set; }

        /*public override void connectObjects() {
            if (DenVTydnu.instances != null) this.denVTydnu = DenVTydnu.instances[this.idDenVTydnu];
        }*/

        public override string ToString() {
            return "<Kalendar> datum: " + this.datum + " |idDenVTydnu: " + this.idDenVTydnu +
                   " |den: " + this.den + " |mesic: " + this.mesic + " |rok: " + this.rok +
                   " |sudy: " + this.sudy;
        }
    }
}