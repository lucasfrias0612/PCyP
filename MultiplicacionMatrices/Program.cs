using System;
using LucasLib;

namespace MultiplicacionMatrices {

    class Program : ConsoleProgram {

        public static void Main () {
            Program program = new Program ();
            program.run ();
            program.pause ();
        }

        override
        public void run () {
            showMsg ("\tMultiplicador de Matrices\n");

            showMsg ("Matriz 1");
            Matrix a = new Matrix ();
            a.initialize ();
            a.load ();
            a.show ();

            showMsg ("\nMatriz 2");
            Matrix b = new Matrix ();
            b.initialize ();
            b.load();
            b.show ();
        }
    }

    class Matrix {

        private int width;
        private int heigth;
        private int[, ] m;

        public Matrix () {

        }

        public void initialize () {

            Console.WriteLine ("Indique el tamaño de la matriz");
            int width = getInt ("Ancho: ");
            int heigth = getInt ("Alto: ");
            if (width != 0 && heigth != 0) {
                this.width = width;
                this.heigth = heigth;
                this.m = new int[heigth, width];
            }
        }

        public bool isMultipliable () {
            return false;
        }

        public void show () {
            for (int i = 0; i < heigth; i++) {
                for (int j = 0; j < width; j++) {
                    Console.Write (m[i, j] + "\t");
                }
                Console.WriteLine ();
            }
        }

        public void load () {
            Console.WriteLine ("Cargando Matriz:\n");
            for (int i = 0; i < heigth; i++) {
                for (int j = 0; j < width; j++) {
                    m[i, j] = getInt ("{" + (i+1) + " ; " + (j+1) + "} = ");
                }
                Console.WriteLine ();
            }
        }

        private int getInt (string msg) {
            Console.Write (msg);
            int result;
            int.TryParse (Console.ReadLine (), out result);
            return result;
        }
    }
}