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
            b.load ();
            b.show ();

            showMsg ("\nResultado de Multiplicación");
            Matrix r = a.multiply (b);
            if (r != null)
                r.show ();
            else
                showMsg ("Las matrices ingresadas no se pueden multiplicar");
        }
    }

    class Matrix {

        private int width;
        private int heigth;
        private int[, ] m;

        public Matrix () {

        }

        private Matrix (int width, int heigth) {
            this.width = width;
            this.heigth = heigth;
            this.m = new int[heigth, width];
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

        public Matrix multiply (Matrix other) {
            if (this.isMultipliable (other)) {
                Matrix result = new Matrix (this.heigth, other.width);
                for (int i = 0; i < heigth; i++) {
                    for (int j = 0; j < other.width; j++) {
                        for (int k = 0; k < width; k++) {
                            result.m[i,j] = result.m[i,j] + m[i,k] * other.m[k,j];
                        }
                    }
                }
                return result;
            }
            return null;
        }

        public bool isMultipliable (Matrix other) {
            return this.width == other.heigth;
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
                    m[i, j] = getInt ("{" + (i + 1) + " ; " + (j + 1) + "} = ");
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