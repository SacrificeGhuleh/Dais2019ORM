#region UsingRegion

using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class VyucujiciKrouzek /*: Connectable */ {
        //public static Dictionary<int, VyucujiciKrouzek> instances;

        public VyucujiciKrouzek(int idVyucujiciKrouzek, int idKrouzek, int idLektor, string popis) {
            this.idVyucujiciKrouzek = idVyucujiciKrouzek;
            this.idKrouzek = idKrouzek;
            this.idLektor = idLektor;
            this.popis = popis;

            /*if (instances == null) instances = new Dictionary<int, VyucujiciKrouzek>();

            instances.Add(idVyucujiciKrouzek, this);*/
        }

        public int idVyucujiciKrouzek { get; set; }
        public int idKrouzek { get; set; }
        public int idLektor { get; set; }
        public string popis { get; set; }

        public Lektor lektor_;

        public Lektor lektor {
            get {
                if (lektor_ == null) {
                    ORM.instance.dao.lektorTable.SelectOne(idLektor, out lektor_);
                }

                return lektor_;
            }
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

        /*public override void connectObjects() {
            if (Krouzek.instances != null) this.krouzek = Krouzek.instances[this.idKrouzek];

            if (Lektor.instances != null) this.lektor = Lektor.instances[this.idLektor];
        }*/

        public override string ToString() {
            return "<VyucujiciKrouzek> idVyucujiciKrouzek: " + this.idVyucujiciKrouzek +
                   " |idKrouzek: " + this.idKrouzek + " |idLektor: " + this.idLektor +
                   " |popis: " + this.popis;
        }
    }
}