using System;

namespace LucasLib {
    public abstract class ConsoleProgram {
        public abstract void run ();

        public void showMsg (string msg) {
            Console.WriteLine (msg);
        }

        public string getString (string msg) {
            showMsg (msg);
            return Console.ReadLine ();
        }

        public void pause () {
            Console.WriteLine ("Presione cualquier tecla para continuar");
            Console.ReadKey ();
        }
    }
}