using System;
using LucasLib;

namespace MultiplicacionMatrices {

    class Program:ConsoleProgram {

        public static void Main () {
            Program program = new Program ();
            program.run ();
            program.pause ();
        }

        override
        public void run () {
        
        }
    }

    class Matrix{

        private int width;
        private int heigth;

        public Matrix(int width, int heigth){
            this.width=width;
            this.heigth=heigth;
        }
    }
}