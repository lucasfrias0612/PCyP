using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LucasLib;

namespace AbecedarioTareas {
    class Program : ConsoleProgram {
        static void Main (string[] args) {
            Program p = new Program ();
            p.run ();
        }
        override
        public void run () {
            Dictionary<char, string> alphabet = new Dictionary<char, string> ();
            int index = 65;
            const int indexLimit = 91;
            Task t1 = new Task (() => {
                while (index < indexLimit) {
                    if (isPar (index)) {
                        alphabet.Add ((char) (index), "Tarea 1");
                        index++;
                    }
                }
            });

            Task t2 = new Task (() => {
                while (index < indexLimit) {
                    if (!isPar (index)) {
                        alphabet.Add ((char) (index), "Tarea 2");
                        index++;
                    }
                }
            });

            Task t3 = new Task (() => {
                foreach (KeyValuePair<char, string> pair in alphabet) {
                    showMsg ("Letra {" + pair.Key + "} Escrita por {" + pair.Value + "}");
                }
                pause ();
            });

            t1.Start ();
            t2.Start ();

            Task.WaitAny (new Task[] { t1, t2 });

            t3.Start ();
            t3.Wait ();

        }

        static bool isPar (int n) {
            return n % 2 == 0;
        }
    }
}