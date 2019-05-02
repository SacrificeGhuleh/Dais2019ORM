#region UsingRegion

using System;
using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class DenVTydnu {
        //public static Dictionary<int, DenVTydnu> instances;

        public DenVTydnu(int idDenVTydnu, string popis) {
            this.idDenVTydnu = idDenVTydnu;
            this.popis = popis ?? throw new ArgumentNullException(nameof(popis));

            //if (instances == null) instances = new Dictionary<int, DenVTydnu>();

            //instances.Add(idDenVTydnu, this);
        }

        public int idDenVTydnu { get; set; }
        public string popis { get; set; }

        public override string ToString() {
            return "<DenVTydnu> idDenVTydnu: " + this.idDenVTydnu + " |popis: " + this.popis;
        }
    }
}