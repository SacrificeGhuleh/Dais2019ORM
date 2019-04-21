#region UsingRegion

using System;
using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class Adresa {
        public static Dictionary<int, Adresa> instances;

        public Adresa(int idAdresa, string ulice, int cisloPopisne, string mesto, string stat, int? psc) {
            this.idAdresa = idAdresa;
            this.ulice = ulice ?? throw new ArgumentNullException(nameof(ulice));
            this.cisloPopisne = cisloPopisne;
            this.mesto = mesto ?? throw new ArgumentNullException(nameof(mesto));
            this.stat = stat ?? throw new ArgumentNullException(nameof(stat));
            this.psc = psc;

            if (instances == null) instances = new Dictionary<int, Adresa>();

            instances.Add(idAdresa, this);
        }

        public int idAdresa { get; set; }
        public string ulice { get; set; }
        public int cisloPopisne { get; set; }
        public string mesto { get; set; }
        public string stat { get; set; }
        public int? psc { get; set; } //int with null value. Call .HasValue to determine if a int value was set.

        public override string ToString() {
            return "<Adresa>idAdresa: " + this.idAdresa + " |ulice: " + this.ulice +
                   " |cisloPopisne: " + this.cisloPopisne + " |mesto: " + this.mesto +
                   " |stat: " + this.stat + " |psc: " + this.psc;
        }
    }
}