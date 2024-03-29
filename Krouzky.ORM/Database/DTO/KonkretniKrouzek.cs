﻿#region UsingRegion

using System;
using System.Collections.Generic;

#endregion

namespace Krouzky.ORM.Database.DTO {
    #region UsingRegion

    #endregion

    public class KonkretniKrouzek /*: Connectable */{
        //public static Dictionary<int, KonkretniKrouzek> instances;

        public KonkretniKrouzek(int idKonkretniKrouzek, int idKrouzek, DateTime datum, bool zrusen, int pocetZaku,
            bool insert = true) {
            this.idKonkretniKrouzek = idKonkretniKrouzek;
            this.idKrouzek = idKrouzek;
            this.datum = datum;
            this.zrusen = zrusen;
            this.pocetZaku = pocetZaku;

            kalendar_ = null;
            krouzek_ = null;
            
            if (!insert) return;

            /*if (instances == null) instances = new Dictionary<int, KonkretniKrouzek>();

            instances.Add(idKonkretniKrouzek, this);*/
        }

        private Kalendar kalendar_;
        private Krouzek krouzek_;

        public Kalendar kalendar {
            get {
                if (kalendar_ == null) {
                    ORM.instance.dao.kalendarTable.SelectOne(datum, out kalendar_);
                }
                return kalendar_;
            }
        }

        public Krouzek krouzek { get {
            if (krouzek_ == null) {
                ORM.instance.dao.krouzekTable.SelectOne(idKrouzek, out krouzek_);
            }
            return krouzek_;
        } }

        public int idKonkretniKrouzek { get; set; }
        public int idKrouzek { get; set; }
        public DateTime datum { get; set; }
        public bool zrusen { get; set; }
        public int pocetZaku { get; set; }

        /*public override void connectObjects() {
            if (Krouzek.instances != null) this.krouzek = Krouzek.instances[this.idKrouzek];

            if (Kalendar.instances != null) this.kalendar = Kalendar.instances[this.datum];
        }*/

        public override string ToString() {
            return "<KonkretniKrouzek> idKonkretniKrouzek: " + this.idKonkretniKrouzek +
                   " |idKrouzek: " + this.idKrouzek + " |datum: " + this.datum +
                   " |zrusen: " + this.zrusen + " |pocetZaku: " + this.pocetZaku;
        }
    }
}