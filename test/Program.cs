using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class Program {

        private static string infile = "vial.csv";

        static void Main (string[] args) {
            var sw = System.Diagnostics.Stopwatch.StartNew ();
            if (File.Exists (infile)) {
                string[] lines = File.ReadLines (infile).ToArray ();

                Dictionary<string, Dictionary<string, int>> finalDic = new Dictionary<string, Dictionary<string, int>> ();

                Parallel.ForEach (lines,

                    () => { return new Dictionary<string, Dictionary<string, int>> (); },

                    (line, loopControl, localD) => {
                        if (Record.isValid (line)) {
                            Record r = new Record (line);
                            if (!localD.ContainsKey (r.card)) {
                                Dictionary<string, int> d = new Dictionary<string, int> ();
                                d.Add (r.route, r.routeLength);
                                localD.Add (r.card, d);
                            } else {
                                if (!localD[r.card].ContainsKey (r.route))
                                    localD[r.card].Add (r.route, r.routeLength);
                                else
                                    localD[r.card][r.route] += r.routeLength;
                            }
                        }
                        return localD;
                    },

                    (localD) => {
                        lock (finalDic) {
                            foreach (string card in localD.Keys) {
                                if (!finalDic.ContainsKey (card))
                                    finalDic.Add (card, localD[card]);
                                else {
                                    Dictionary<string, int>[] dics = { finalDic[card], localD[card] };
                                    finalDic[card] = Dictionary<string, int>.Merge (dics);
                                }
                            }
                        }
                    }

                );

                double time = sw.ElapsedMilliseconds / 1000.0; // convert milliseconds to secs

                Console.WriteLine ();
                Console.WriteLine ("** Done! Time: {0:0.000} secs", time);
                Console.WriteLine ();
                Console.Write ("Press a key to exit...");
                Console.ReadKey ();
            } else {
                Console.WriteLine ("** Error: infile '{0}' does not exist.", infile);
                Console.WriteLine ();
                System.Environment.Exit (-1);
            }
        }
    } //class

    class Record {
        static char CSV_DELIMITER = ',';

        public string card;
        public string route;
        public int routeLength;

        public Record (string input) {
            string[] tokens = input.Split (CSV_DELIMITER);
            if (tokens.Length == 5) {
                card = tokens[0];
                route = tokens[3];
                Int32.TryParse (tokens[4], out routeLength);
            }
        }

        public static bool isValid (string l) {
            bool b = false;
            string[] tokens = l.Split (CSV_DELIMITER);
            if (tokens.Length == 5) {
                var converted = 0;
                bool success = Int32.TryParse (tokens[4], out converted);
                b = !string.IsNullOrEmpty (tokens[0]) && !string.IsNullOrEmpty (tokens[3]) && success;
            }
            return b;
        }
    }
}