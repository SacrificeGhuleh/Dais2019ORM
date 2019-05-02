namespace Krouzky.ORM.Tester {
    #region UsingRegion

    using System;

    #endregion

    public class Program {
        private static void Main(string[] args) {
            /*
             *
             * PRO SPRAVNE OTESTOVANI VSECH FUNKCI JE TREBA RESETOVAT DATABAZI
             *
             */

            var orm = ORM.instance;

            /*
            //Vypis databaze
            orm.dto.print();
            */
            try {
                orm.calculateHoursInPeriod(DateTime.Parse("2019-01-01"), DateTime.Parse("2019-01-31"), 0);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            for (int i = 0; i < 10; i++) {
                try {
                    orm.calculateHoursTotal(i);
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            }

/*
            orm.calculateHoursTotal(0);
            orm.calculateHoursTotal(1);
            orm.calculateHoursTotal(2);
            orm.calculateHoursTotal(3);
            orm.calculateHoursTotal(4);
            orm.calculateHoursTotal(5);
            orm.calculateHoursTotal(6);
*/
            try {
                orm.calculateSalary(DateTime.Parse("2019-01-01"), DateTime.Parse("2019-01-31"), 0);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            try {
                orm.getPlannedAndPassed(DateTime.Parse("2019-04-15"), DateTime.Parse("2019-04-22"),
                    DateTime.Today);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            try {
                orm.promotePlannedToPassed(3, DateTime.Parse("2019-04-19"), false, 5);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            try {
                orm.getPlannedAndPassed(DateTime.Parse("2019-04-15"), DateTime.Parse("2019-04-22"),
                    DateTime.Today);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
//            /*Generovani konkretnich krouzku*/
//            try {
//                var krouzky =
//                    orm.getPlannedAndPassed(DateTime.Parse("2018-01-01"), DateTime.Parse("2019-04-20"), DateTime.Now);
//                foreach (var planned in krouzky.Item1) {
//                    Console.WriteLine(ORM.InsertString(planned));
//                }
//            }
//            catch (Exception e) {
//                Console.WriteLine(e);
//            }

            try {
                Console.WriteLine("Prvni pokus o zvyseni mzdy");
                Console.WriteLine("Zvyseni mzdy: {0}", orm.salaryUpdate(0, DateTime.Today));
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            Console.WriteLine();

            try {
                Console.WriteLine("Druhy pokus o zvyseni mzdy");
                Console.WriteLine("Zvyseni mzdy: {0}", orm.salaryUpdate(0, DateTime.Today));
            }
            catch (Exception e) {
                Console.WriteLine(e);
//                throw;
            }
        }
    }
}