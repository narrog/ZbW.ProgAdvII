using System;
using System.Collections;
using System.Collections.Generic;

namespace _2_CityCollection {
    public class CityCollection : IEnumerable<string> { 
        private string[] cities = {"Bern", "Basel", "Zürich", "Rapperswil", "Genf"};

        public IEnumerable<string> Reverse {
            get
            {
                for (int i = cities.Length - 1; i >= 0; i--)
                {
                    yield return cities[i];
                }
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < cities.Length; i++)
            {
                yield return cities[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // TODO: Members für Test-Code in Main()-Methode erstellen

    }

    class Program {
        static void Main(string[] args) {
            CityCollection myColl = new CityCollection();

            //Ausgabe
            foreach (string s in myColl)
            {
                Console.WriteLine(s);
            }

            //Ausgabe in umgekehrter Reihenfolge
            foreach (string s in myColl.Reverse)
            {
                Console.WriteLine(s);
            }

            Console.ReadKey();
        }
    }
}