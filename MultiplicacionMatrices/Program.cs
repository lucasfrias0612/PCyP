using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplicacionMatrices {
    class Program {
        static void Main (string[] args) {

            Matrix m1 = new Matrix (1000, 500);
            m1.loadRandomly ();
            Matrix m2 = new Matrix (500, 1000);
            m2.loadRandomly ();

            if (m1.isMultipliable (m2)) //Si el numero de columnas de la 1 es igual al numero de filas de la 2
            {
                Stopwatch sw = new Stopwatch ();
                sw.Start ();
                Matrix res1 = m1.multiplySequentially (m2);
                sw.Stop ();
                Console.WriteLine ("El programa demoró " + (sw.ElapsedMilliseconds) + " miliseg. Secuencialmente");
                sw = new Stopwatch ();
                sw.Start ();
                Matrix res2 = m1.multiplyInParalell (m2);
                sw.Stop ();
                Console.WriteLine ("El programa demoró " + (sw.ElapsedMilliseconds) + " miliseg. en Parallelo");

                // Console.WriteLine ("LA RESPUESTA ES: ");
                // res.show ();
            } else {
                Console.WriteLine ("No se puede multiplicar estas matrices");
                Console.Read ();
            }
        }
    }

    public class Matrix {
        private int[, ] m;
        private int c;
        private int r;

        public Matrix (int r, int c) {
            this.m = new int[r, c];
            this.r = r;
            this.c = c;
        }

        public void loadRandomly () {
            Random random = new Random ();
            for (int r = 0; r < this.r; r++) {
                for (int c = 0; c < this.c; c++) {
                    this.m[r, c] = random.Next (1, 100);;
                }
            }
        }

        public bool isMultipliable (Matrix other) {
            return this.c == other.r;
        }

        public void show () {
            string igual = "";
            for (int i = 0; i < this.r; i++) {
                for (int j = 0; j < this.c; j++) {
                    igual = igual + (this.m[i, j].ToString ()) + " ";
                }
                Console.WriteLine ("[ " + igual + "]");
                igual = "";
            }
        }
        public Matrix multiplyInParalell (Matrix other) {
            Matrix matrixResponse = new Matrix (this.r, other.c);
            int temp = 0;
            Parallel.For (0, this.r, i => {
                for (int j = 0; j < other.c; j++) {
                    for (int k = 0; k < this.c; k++) {
                        temp += this.m[i, k] * other.m[k, j];
                    }
                    matrixResponse.m[i, j] = temp;
                    temp = 0;
                }
            }); // Parallel.For
            return matrixResponse;
        }

        public Matrix multiplySequentially (Matrix other) {
            Matrix matrixResponse = new Matrix (this.r, other.c);
            int temp = 0;
            for (int i = 0; i < this.r; i++) {
                for (int j = 0; j < other.c; j++) {
                    for (int k = 0; k < this.c; k++) {
                        temp += this.m[i, k] * other.m[k, j];
                    }
                    matrixResponse.m[i, j] = temp;
                    temp = 0;
                }
            }
            return matrixResponse;
        }
    }
}