#region UsingRegion

using System;
using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class Pravidelnost {
        public static Dictionary<int, Pravidelnost> instances;

        public Pravidelnost(int idPravidelnost, string popis) {
            this.idPravidelnost = idPravidelnost;
            this.popis = popis ?? throw new ArgumentNullException(nameof(popis));

            if (instances == null) instances = new Dictionary<int, Pravidelnost>();

            instances.Add(idPravidelnost, this);
        }

        public int idPravidelnost { get; set; }
        public string popis { get; set; }

        public override string ToString() {
            return "<Pravidelnost> idPravidelnost: " + this.idPravidelnost + " |popis: " + this.popis;
        }
    }
}