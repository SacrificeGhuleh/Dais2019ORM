#region UsingRegion

using System;
using System.Collections.ObjectModel;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class DTO {
        private DAO.DAO dao_;

        public DTO(DAO.DAO dao) {
            dao_ = dao;
        }

        public Collection<Adresa> adresa {
            get { return dao_.adresaTable.Select(); }
        }

        public Collection<DenVTydnu> denVTydnu {
            get { return dao_.denVTydnuTable.Select(); }
        }

        public Collection<HodinovaMzda> hodinovaMzda {
            get { return dao_.hodinovaMzdaTable.Select(); }
        }

        public Collection<Kalendar> kalendar {
            get { return dao_.kalendarTable.Select(); }
        }

        public Collection<KonkretniKrouzek> konkretniKrouzek {
            get { return dao_.konkretniKrouzekTable.Select(); }
        }

        public Collection<KontaktniOsoba> kontaktniOsoba {
            get { return dao_.kontaktniOsobaTable.Select(); }
        }

        public Collection<Krouzek> krouzek {
            get { return dao_.krouzekTable.Select(); }
        }

        public Collection<Lektor> lektor {
            get { return dao_.lektorTable.Select(); }
        }

        public Collection<Osoba> osoba {
            get { return dao_.osobaTable.Select(); }
        }

        public Collection<Pravidelnost> pravidelnost {
            get { return dao_.pravidelnostTable.Select(); }
        }

        public Collection<Skola> skola {
            get { return dao_.skolaTable.Select(); }
        }

        public Collection<VyucujiciKrouzek> vyucujiciKrouzek {
            get { return dao_.vyucujiciKrouzekTable.Select(); }
        }

        public void updateDTO() {
        }

        public void print() {
            this.print(this.adresa);
            this.print(this.denVTydnu);
            this.print(this.hodinovaMzda);
            //print(kalendar);
            this.print(this.konkretniKrouzek);
            this.print(this.kontaktniOsoba);
            this.print(this.krouzek);
            this.print(this.lektor);
            this.print(this.osoba);
            this.print(this.pravidelnost);
            this.print(this.skola);
            this.print(this.vyucujiciKrouzek);
        }

        public void print<T>(Collection<T> col) {
            Console.WriteLine("----------{0}----------", typeof(T));
            foreach (var v in col) Console.WriteLine(v.ToString());
        }
    }
}