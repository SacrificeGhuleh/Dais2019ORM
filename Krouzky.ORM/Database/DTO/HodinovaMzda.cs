#region UsingRegion

using System;
using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class HodinovaMzda : Connectable {
        public static Dictionary<int, HodinovaMzda> instances;

        public HodinovaMzda(int idHodinovaMzda, int idLektor, int mzda, DateTime platnostOd,
            DateTime? platnostDo = null) {
            this.idHodinovaMzda = idHodinovaMzda;
            this.idLektor = idLektor;
            this.mzda = mzda;
            this.platnostOd = platnostOd;
            this.platnostDo = platnostDo;

            if (instances == null) instances = new Dictionary<int, HodinovaMzda>();

            instances.Add(idHodinovaMzda, this);
        }

        public Lektor lektor { get; set; }

        public int idHodinovaMzda { get; set; }
        public int idLektor { get; set; }
        public int mzda { get; set; }
        public DateTime platnostOd { get; set; }
        public DateTime? platnostDo { get; set; }

        public override void connectObjects() {
            if (Lektor.instances != null) this.lektor = Lektor.instances[this.idLektor];
        }

        public override string ToString() {
            return "<HodinovaMzda> idHodinovaMzda: " + this.idHodinovaMzda + " |idLektor: " +
                   this.idLektor + " | mzda: " + this.mzda + " |platnostOd: " +
                   this.platnostOd.Date + " |platnostDo: " + this.platnostDo;
        }
    }
}