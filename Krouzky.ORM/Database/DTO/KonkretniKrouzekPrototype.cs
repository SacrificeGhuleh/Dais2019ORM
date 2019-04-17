namespace Krouzky.ORM.Database.DTO
{
    #region UsingRegion

    using System;

    #endregion

    public class KonkretniKrouzekPrototype
    {
        public KonkretniKrouzekPrototype(int idKrouzek, DateTime datum)
        {
            this.idKrouzek = idKrouzek;
            this.datum = datum;
        }

        public int idKrouzek { get; set; }
        public DateTime datum { get; set; }

        public override string ToString() =>
            "<KonkretniKrouzekPrototype> idKrouzek: " + this.idKrouzek + " |datum: " + this.datum;
    }
}