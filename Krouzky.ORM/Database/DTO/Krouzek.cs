#region UsingRegion

using System;
using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class Krouzek /*: Connectable */ {
        //public static Dictionary<int, Krouzek> instances;

        public Krouzek(int idKrouzek, int idSkola, int idPravidelnost, int idDenVTydnu, DateTime casKonaniOd,
            DateTime casKonaniDo) {
            this.idKrouzek = idKrouzek;
            this.idSkola = idSkola;
            this.idPravidelnost = idPravidelnost;
            this.idDenVTydnu = idDenVTydnu;
            this.casKonaniOd = casKonaniOd;
            this.casKonaniDo = casKonaniDo;

            denVTydnu_ = null;
            pravidelnost_ = null;
            skola_ = null;

            /*if (instances == null) instances = new Dictionary<int, Krouzek>();

            instances.Add(idKrouzek, this);*/
        }

        public int idKrouzek { get; set; }
        public int idSkola { get; set; }
        public int idPravidelnost { get; set; }
        public int idDenVTydnu { get; set; }
        public DateTime casKonaniOd { get; set; }
        public DateTime casKonaniDo { get; set; }

        private DenVTydnu denVTydnu_;
        private Pravidelnost pravidelnost_;
        private Skola skola_;

        public DenVTydnu denVTydnu {
            get {
                if (denVTydnu_ == null) {
                    ORM.instance.dao.denVTydnuTable.SelectOne(idDenVTydnu, out denVTydnu_);
                }

                return denVTydnu_;
            }
        }

        public Pravidelnost pravidelnost {
            get {
                if (pravidelnost_ == null) {
                    ORM.instance.dao.pravidelnostTable.SelectOne(idPravidelnost, out pravidelnost_);
                }

                return pravidelnost_;
            }
        }

        public Skola skola {
            get {
                if (skola_ == null) {
                    ORM.instance.dao.skolaTable.SelectOne(idSkola, out skola_);
                }

                return skola_;
            }
        }

        /*public override void connectObjects() {
            if (DenVTydnu.instances != null) this.denVTydnu = DenVTydnu.instances[this.idDenVTydnu];

            if (Pravidelnost.instances != null) this.pravidelnost = Pravidelnost.instances[this.idPravidelnost];

            if (Skola.instances != null) this.skola = Skola.instances[this.idSkola];
        }*/

        public override string ToString() {
            return "<Krouzek> idKrouzek: " + this.idKrouzek + " |idSkola: " + this.idSkola +
                   " |idPravidelnost: " + this.idPravidelnost + " |idDenVTydnu: " +
                   this.idDenVTydnu + " |casKonaniOd: " + this.casKonaniOd +
                   " |casKonaniDo: " + this.casKonaniDo;
        }
    }
}