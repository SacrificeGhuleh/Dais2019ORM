#region UsingRegion

using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class Lektor /*: Connectable*/ {
        //public static Dictionary<int, Lektor> instances;

        public Lektor(int idLektor = -1, int idOsoba = -1, string popis = "") {
            this.idLektor = idLektor;
            this.idOsoba = idOsoba;
            this.popis = popis; //?? throw new ArgumentNullException(nameof(popis));

            osoba_ = null;
            /*if (instances == null) instances = new Dictionary<int, Lektor>();

            instances.Add(idLektor, this);*/
        }

        private Osoba osoba_;

        public Osoba osoba {
            get {

                if (osoba_ == null) {
                    ORM.instance.dao.osobaTable.SelectOne(idOsoba, out osoba_);
                }
                return osoba_;
            }
        }

        public int idLektor { get; set; }
        public int idOsoba { get; set; }
        public string popis { get; set; }

        /*public override void connectObjects() {
            if (Osoba.instances != null) this.osoba = Osoba.instances[this.idOsoba];
        }*/

        public override string ToString() {
            return "<Lektor> idLektor: " + this.idLektor + " |idOsoba: " + this.idOsoba +
                   " |popis: " + this.popis;
        }
    }
}