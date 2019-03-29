using System;
using System.Collections.Generic;
using LucasLib;

namespace Dictionaries {
    class Program : ConsoleProgram{
        private Dictionary<string, string> dic;

        static void Main (string[] args) {
            Program p = new Program ();
            p.run ();
            p.pause ();
        }

        private Program () {
            this.dic = new Dictionary<string, string> ();
            dic.Add ("Juan", "55423412");
            dic.Add ("Ernesto", "56985623");
            dic.Add ("Mariana", "54787451");
        }

        #region runDictionary
        override
        public void run () {
            showMsg ("Ejercicio de Diccionarios en NET\n");

            showMsg ("\nPunto 1.A.Existe la llave 'Juan'?");
            point1A ();
            showMsg ("\nPunto 1.B.Existe la llave 'Pedro'?");
            point1B ();
            showMsg ("\nPunto 1.C.Mostrando todo el diccionario");
            point1C ();
            showMsg ("\nPunto 1.D.d. Alterar el valor cuyo índice es 'Mariana' por '58251425'");
            point1D ();
        }

        void point1A () {
            if (this.dic.ContainsKey ("Juan")) {
                string result;
                dic.TryGetValue ("Juan", out result);
                showMsg ("Valor de la llave 'Juan': " + result);
            }
        }

        void point1B () {
            string result;
            if (dic.TryGetValue ("Pedro", out result))
                showMsg ("Valor de la llave 'Pedro': " + result);
            else
                showMsg ("No contiene la llave");
        }

        void point1C () {
            foreach (KeyValuePair<string, string> pair in dic) {
                showMsg ("Llave {" + pair.Key + "} Valor {" + pair.Value + "}");
            }
        }

        void point1D () {
            if (dic.ContainsKey ("Mariana")) {
                dic.Remove ("Mariana");
                dic.Add ("Mariana", "58251425");
                string result;
                if (dic.TryGetValue ("Mariana", out result))
                    showMsg ("Nuevo valor de la llave 'Mariana': " + result);
            }
        }
        #endregion
    }
}