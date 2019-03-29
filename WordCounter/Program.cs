using System;
using System.Collections.Generic;
using LucasLib;

namespace WordCounter {
    class Program : ConsoleProgram {
        private Dictionary<string, int> dic;

        static void Main (string[] args) {
            Program p = new Program ();
            p.run ();
            p.pause ();
        }

        private Program () {
            this.dic = new Dictionary<string, int> ();
        }

        #region runWordCounter
        override
        public void run () {
            showMsg ("Contador de palabras\n");
            string s = getString ("Ingrese un texto:");
            foreach (var word in s.Split (" ")) {
                addWordToDic (word);
            }
            showMsg ("Diccionario Resultante:\n");
            showDictionary ();
        }

        void addWordToDic (string s) {
            if (dic.ContainsKey (s))
                dic[s]++; //Los Diccionarios se pueden indexar
            else
                dic.Add (s, 1);
        }

        void showDictionary () {
            foreach (var item in dic) {
                showMsg ("Key {" + item.Key + "} Value {" + item.Value + "}");
            }
        }
        #endregion
    }
}