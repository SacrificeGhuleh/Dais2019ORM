#region UsingRegion

using System;

#endregion

namespace Krouzky.ORM.Database.DAO {
    public class DAO {
        public DAO() {
            this.db = new Database();
            //this.db.Connect();
            if (!this.db.Connect()) throw new Exception("Nelze se pripojit k databazi.");

            this.adresaTable = new AdresaTable();
            this.denVTydnuTable = new DenVTydnuTable();
            this.hodinovaMzdaTable = new HodinovaMzdaTable();
            this.kalendarTable = new KalendarTable();
            this.konkretniKrouzekTable = new KonkretniKrouzekTable();
            this.kontaktniOsobaTable = new KontaktniOsobaTable();
            this.krouzekTable = new KrouzekTable();
            this.lektorTable = new LektorTable();
            this.osobaTable = new OsobaTable();
            this.pravidelnostTable = new PravidelnostTable();
            this.skolaTable = new SkolaTable();
            this.vyucujiciKrouzekTable = new VyucujiciKrouzekTable();
        }

        public Database db { get; }
        public AdresaTable adresaTable { get; }
        public DenVTydnuTable denVTydnuTable { get; }
        public HodinovaMzdaTable hodinovaMzdaTable { get; }
        public KalendarTable kalendarTable { get; }
        public KonkretniKrouzekTable konkretniKrouzekTable { get; }
        public KontaktniOsobaTable kontaktniOsobaTable { get; }
        public KrouzekTable krouzekTable { get; }
        public LektorTable lektorTable { get; }
        public OsobaTable osobaTable { get; }
        public PravidelnostTable pravidelnostTable { get; }
        public SkolaTable skolaTable { get; }
        public VyucujiciKrouzekTable vyucujiciKrouzekTable { get; }
    }
}