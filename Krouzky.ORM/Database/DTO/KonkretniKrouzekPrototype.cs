#region UsingRegion

using System;
using Krouzky.ORM.Database.DAO;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class KonkretniKrouzekPrototype {
        public KonkretniKrouzekPrototype(int idKrouzek, DateTime datum) {
            this.idKrouzek = idKrouzek;
            this.datum = datum;
            this.krouzek_ = null;
            //if (Krouzek.instances != null) this.krouzek = Krouzek.instances[this.idKrouzek];
        }

        private Krouzek krouzek_;

        public Krouzek krouzek {
            get {
                if (krouzek_ == null) {
                    ORM.instance.dao.krouzekTable.SelectOne(idKrouzek, out krouzek_);
                }
                return krouzek_;
            }
        }

        public int idKrouzek { get; set; }
        public DateTime datum { get; set; }

        public override string ToString() {
            return "<KonkretniKrouzekPrototype> idKrouzek: " + this.idKrouzek + " |datum: " + this.datum;
        }
    }
}