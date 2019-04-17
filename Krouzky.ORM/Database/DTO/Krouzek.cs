namespace Krouzky.ORM.Database.DTO
{
    #region UsingRegion

    using System;
    using System.Collections.Generic;

    #endregion

    public class Krouzek : Connectable
    {
        public static Dictionary<int, Krouzek> instances;

        public Krouzek(int idKrouzek, int idSkola, int idPravidelnost, int idDenVTydnu, DateTime casKonaniOd,
            DateTime casKonaniDo)
        {
            this.idKrouzek = idKrouzek;
            this.idSkola = idSkola;
            this.idPravidelnost = idPravidelnost;
            this.idDenVTydnu = idDenVTydnu;
            this.casKonaniOd = casKonaniOd;
            this.casKonaniDo = casKonaniDo;

            if (instances == null)
            {
                instances = new Dictionary<int, Krouzek>();
            }

            instances.Add(idKrouzek, this);
        }

        public int idKrouzek { get; set; }
        public int idSkola { get; set; }
        public int idPravidelnost { get; set; }
        public int idDenVTydnu { get; set; }
        public DateTime casKonaniOd { get; set; }
        public DateTime casKonaniDo { get; set; }
        public DenVTydnu denVTydnu { get; set; }
        public Pravidelnost pravidelnost { get; set; }
        public Skola skola { get; set; }

        public override void connectObjects()
        {
            if (DenVTydnu.instances != null)
            {
                this.denVTydnu = DenVTydnu.instances[this.idDenVTydnu];
            }

            if (Pravidelnost.instances != null)
            {
                this.pravidelnost = Pravidelnost.instances[this.idPravidelnost];
            }

            if (Skola.instances != null)
            {
                this.skola = Skola.instances[this.idSkola];
            }
        }

        public override string ToString() => "<Krouzek> idKrouzek: " + this.idKrouzek + " |idSkola: " + this.idSkola +
                                             " |idPravidelnost: " + this.idPravidelnost + " |idDenVTydnu: " +
                                             this.idDenVTydnu + " |casKonaniOd: " + this.casKonaniOd +
                                             " |casKonaniDo: " + this.casKonaniDo;
    }
}