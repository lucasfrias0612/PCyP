using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program2
    {
        static void Main(string[] args)
        {

            Dictionary<int, double> longitudesPorCarta = new Dictionary<int, double>();

            List<string> archivos = new List<string>();
            for (int iNroArchivo = 1; iNroArchivo <= 72; iNroArchivo++)
            {
                archivos.Add(@"data\vial" + iNroArchivo.ToString().PadLeft(2, '0') + ".csv");
            }

            //
            //foreach (string line in File.ReadLines(infile))
            //
            Parallel.ForEach(archivos,

                //
                // Initializer:  create task-local Dictionary:
                //
                () => { return new Dictionary<int, double>(); },


                //
                // Loop-body: work with TLS which represents a local Dictionary,
                // mapping our results into this local dictionary:
                //
                (archivo, loopControl, localD) =>
                {

                    foreach (string line in File.ReadLines(archivo))
                    {
                        char[] separators = { ',' };
                        string[] tokens = line.Split(separators);
                        int carta = Convert.ToInt32(tokens[0]);
                        double longitud;
                        if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            longitud = double.Parse(tokens[4].Replace(".", ","));
                        }
                        else
                        {
                            longitud = double.Parse(tokens[4]);
                        }

                        if (!localD.ContainsKey(carta))  // primera vez que llega esta carta:
                            localD.Add(carta, 1);
                        else  // otra longitud de la misma carta:
                            localD[carta] += longitud;
                    }

                    return localD;  // return out so it can be passed back in later:
                },

                //
                // Finalizer: reduce individual local dictionaries into global dictionary:
                //
                (localD) =>
                {
                    lock (longitudesPorCarta)
                    {
                        //
                        // merge into global data structure:
                        //
                        foreach (int carta in localD.Keys)
                        {
                            double longitud = localD[carta];

                            if (!longitudesPorCarta.ContainsKey(carta))  // first review:
                                longitudesPorCarta.Add(carta, longitud);
                            else  // another review by same user:
                                longitudesPorCarta[carta] += longitud;
                        }
                    }
                }

            );

            //
            // Write out the results:
            //
            Console.WriteLine();
            Console.WriteLine("** Longitudes de caminos por carta");

            double total = 0;
            foreach (int carta in longitudesPorCarta.Keys)
            {
                double longitud = longitudesPorCarta[carta];
                total += longitud;
                Console.WriteLine("Carta {0}: {1} m.", carta, longitud);
            }

            Console.WriteLine();
            Console.WriteLine("** Longitud total del país: {0}", total);
            Console.WriteLine();
            Console.ReadLine();

        }
    }
}
