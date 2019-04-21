#region UsingRegion

using System;
using System.Collections.ObjectModel;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class DTO {
        public DTO(DAO.DAO dao) {
            this.adresa = dao.adresaTable.Select(dao.db);
            this.denVTydnu = dao.denVTydnuTable.Select(dao.db);
            this.hodinovaMzda = dao.hodinovaMzdaTable.Select(dao.db);
            this.kalendar = dao.kalendarTable.Select(dao.db);
            this.konkretniKrouzek = dao.konkretniKrouzekTable.Select(dao.db);
            this.kontaktniOsoba = dao.kontaktniOsobaTable.Select(dao.db);
            this.krouzek = dao.krouzekTable.Select(dao.db);
            this.lektor = dao.lektorTable.Select(dao.db);
            this.osoba = dao.osobaTable.Select(dao.db);
            this.pravidelnost = dao.pravidelnostTable.Select(dao.db);
            this.skola = dao.skolaTable.Select(dao.db);
            this.vyucujiciKrouzek = dao.vyucujiciKrouzekTable.Select(dao.db);

            this.print();

            foreach (Connectable conn in this.lektor) conn.connectObjects();

            foreach (Connectable conn in this.hodinovaMzda) conn.connectObjects();

            foreach (Connectable conn in this.kalendar) conn.connectObjects();

            foreach (Connectable conn in this.konkretniKrouzek) conn.connectObjects();

            foreach (Connectable conn in this.skola) conn.connectObjects();

            foreach (Connectable conn in this.kontaktniOsoba) conn.connectObjects();

            foreach (Connectable conn in this.vyucujiciKrouzek) conn.connectObjects();

            foreach (Connectable conn in this.krouzek) conn.connectObjects();
        }

        public Collection<Adresa> adresa { get; }
        public Collection<DenVTydnu> denVTydnu { get; }
        public Collection<HodinovaMzda> hodinovaMzda { get; }
        public Collection<Kalendar> kalendar { get; }
        public Collection<KonkretniKrouzek> konkretniKrouzek { get; }
        public Collection<KontaktniOsoba> kontaktniOsoba { get; }
        public Collection<Krouzek> krouzek { get; }
        public Collection<Lektor> lektor { get; }
        public Collection<Osoba> osoba { get; }
        public Collection<Pravidelnost> pravidelnost { get; }
        public Collection<Skola> skola { get; }
        public Collection<VyucujiciKrouzek> vyucujiciKrouzek { get; }

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