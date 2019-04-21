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

            orm.calculateHoursInPeriod(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-31"), 0);

            orm.calculateHoursTotal(0);
            orm.calculateHoursTotal(1);
            orm.calculateHoursTotal(2);
            orm.calculateHoursTotal(3);
            orm.calculateHoursTotal(4);
            orm.calculateHoursTotal(5);
            orm.calculateHoursTotal(6);

            orm.calculateSalary(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-31"), 0);

            orm.getPlannedAndPassed(DateTime.Parse("2018-05-01"), DateTime.Parse("2018-05-20"),
                DateTime.Parse("2018-05-15"));
            orm.promotePlannedToPassed(4, DateTime.Parse("2018-05-08"), false, 5);
            orm.getPlannedAndPassed(DateTime.Parse("2018-05-01"), DateTime.Parse("2018-05-20"),
                DateTime.Parse("2018-05-15"));


            var krouzky =
                orm.getPlannedAndPassed(DateTime.Parse("2018-01-01"), DateTime.Parse("2019-04-20"), DateTime.Now);

            /*foreach (var planned in krouzky.Item1) {
                Console.WriteLine(ORM.InsertString(planned));
            }*/

            Console.WriteLine("Prvni pokus o zvyseni mzdy");
            Console.WriteLine("Zvyseni mzdy: {0}", orm.salaryUpdate(4, DateTime.Parse("2018-05-05")));
            Console.WriteLine();
            Console.WriteLine("Druhy pokus o zvyseni mzdy");
            Console.WriteLine("Zvyseni mzdy: {0}", orm.salaryUpdate(4, DateTime.Parse("2018-05-05")));
        }
    }
}