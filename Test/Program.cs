using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Parcial2 {
    class Program {
        static void Main (string[] args) {
            var sw = System.Diagnostics.Stopwatch.StartNew ();
            List<Record> records = new List<Record> ();
            Dictionary<string, int> LengthOfRoutes = new Dictionary<string, string> ();
            String line;
            try {
                StreamReader sr = new StreamReader ("rutas_nacionales.csv");
                line = sr.ReadLine ();
                while (line != null) {
                    records.Add (new Record (line));
                    line = sr.ReadLine ();
                }
                sr.Close ();
            } catch (Exception e) {
                Console.WriteLine ("Exception: " + e.Message);
            }

            Parallel.ForEach (records,
                () => { return new Dictionary<string, int> (); },

                (r, loopControl, localD) => {
                    int length = Convert.ToInt32 (r.lengthKm);
                    if (!localD.ContainsKey (r.province)) // first review:
                        localD.Add (r.province, length);
                    else // another review by same user:
                        localD[r.province] += length;
                    return localD; // return out so it can be passed back in later:
                },
                //
                // Finalizer: reduce individual local dictionaries into global dictionary:
                //
                (localD) => {
                    lock (LengthOfRoutes) {
                        foreach (string province in localD.Keys) {
                            int numreviews = localD[province];
                            if (!LengthOfRoutes.ContainsKey (province)) // first review:
                                LengthOfRoutes.Add (province, numreviews);
                            else // another review by same user:
                                LengthOfRoutes[province] += numreviews;
                        }
                    }
                }
            );
            long timems = sw.ElapsedMilliseconds;
            //
            // Write out the results:
            //
            Console.WriteLine ();
            Console.WriteLine ("** Resultado:");

            foreach (var record in LengthOfRoutes)
                Console.WriteLine ("{0}: {1}", record.Key, record.Value);

            // 
            // Done:
            //
            double time = timems / 1000.0; // convert milliseconds to secs
            Console.WriteLine ();
            Console.WriteLine ("** Done! Time: {0:0.000} secs", time);
            Console.WriteLine ();
            Console.WriteLine ();
            Console.WriteLine ();

            Console.Write ("Press a key to exit...");
            Console.ReadKey ();
        }
    }

    class Record {
        public string province;
        public string responsible;
        public string route;
        public string type;
        public string lengthKm;

        public Record (string fileLine) {
            string[] tokens = fileLine.Split (",");
            this.province = tokens[0];
            this.responsible = tokens[1];
            this.route = tokens[2];
            this.type = tokens[3];
            this.lengthKm = tokens[4];
        }
    }
}