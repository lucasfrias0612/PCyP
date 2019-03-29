using System;
using System.Collections.Generic;
using LucasLib;

namespace Lists {
    class Program : ConsoleProgram {

        private ArraysGroup ag;

        static void Main (string[] args) {
            Program program = new Program ();
            program.run ();
            program.pause ();
        }

        private Program () {
            this.ag = new ArraysGroup ();
        }

        #region runList

        override
        public void run () {
            showMsg ("Ejercicio de Listas en NET\n");
            ag.showColorsArray ();
            ag.showToRemoveColorsArray ();

            showMsg ("\nPunto 2.AyB.ColorsList");
            showList (ag.colorsArrayToList ());
            showMsg ("\nPunto 2.AyB.toReomveColorsList");
            showList (ag.toRemoveColorsArrayToList ());

            showMsg ("\nPunto 2.CyD.ResultColorList");
            showList (point2C ());
        }

        private List<string> point2C () {
            List<string> colorsList = new List<string> ();
            foreach (var item in ag.getColorsArray ()) {
                if (!hasString (ag.getToRemoveColorsArray (), item))
                    colorsList.Add (item);
            }
            return colorsList;
        }

        private bool hasString (string[] s, string c) {
            for (int i = 0; i < s.Length; i++) {
                if (s[i].Equals (c))
                    return true;
            }
            return false;
        }

        private void showList (List<string> a) {
            foreach (string item in a) {
                if (item != null) {
                    Console.WriteLine ("Colors {0}", item);
                }
            }
            Console.WriteLine ();
        }
        #endregion
    }
    class ArraysGroup {
        private readonly string[] colors = { "MAGENTA", "RED", "WHITE", "BLUE", "CYAN" };
        private readonly string[] toRemoveColors = { "RED", "WHITE", "BLUE" };

        public void showColorsArray () {
            showArray (this.colors, "Original Colors Array");
        }

        public void showToRemoveColorsArray () {
            showArray (this.toRemoveColors, "Original ToRemoveColors Array");
        }

        private void showArray (string[] a, string arrayName) {
            Console.WriteLine (arrayName + ":");
            foreach (string item in a) {
                if (item != null)
                    Console.WriteLine ("Colors {0}", item);
            }
            Console.WriteLine ();
        }

        public List<string> toRemoveColorsArrayToList () {
            return new List<string> (this.toRemoveColors);
        }

        public List<string> colorsArrayToList () {
            List<string> colorsList = new List<string> ();
            foreach (var item in this.colors) {
                colorsList.Add (item);
            }
            return colorsList;
        }

        public string[] getColorsArray () {
            return this.colors;
        }

        public string[] getToRemoveColorsArray () {
            return this.toRemoveColors;
        }
    }
}