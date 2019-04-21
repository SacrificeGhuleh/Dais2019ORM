#region UsingRegion

using System;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class KonkretniKrouzekPrototype {
        public KonkretniKrouzekPrototype(int idKrouzek, DateTime datum) {
            this.idKrouzek = idKrouzek;
            this.datum = datum;

            if (Krouzek.instances != null) this.krouzek = Krouzek.instances[this.idKrouzek];
        }

        public Krouzek krouzek { get; set; }
        public int idKrouzek { get; set; }
        public DateTime datum { get; set; }

        public override string ToString() {
            return "<KonkretniKrouzekPrototype> idKrouzek: " + this.idKrouzek + " |datum: " + this.datum;
        }
    }
}