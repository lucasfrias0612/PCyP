using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using LucasLib;

namespace WaitAllOneByOne {
    class Program : ConsoleProgram {
        static void Main (string[] args) {
            Program program = new Program ();
            program.run ();
            program.pause ();
        }

        override
        public void run () {
            List<Task<string>> tasks = new List<Task<string>> ();
            for (int i=0;i<10;i++) {
                tasks.Add(new Task<string>((index) => {
                    Thread.SpinWait (10000000);
                    return "Tarea "+index+" finalizada.";
                }, i));
            }
            foreach(Task t in tasks){
                t.Start();
            }
            while(tasks.Count>0){
                int index = Task.WaitAny(tasks.ToArray());
                showMsg("Result: "+tasks.ToArray()[index].Result);
                tasks.RemoveAt(index);
            }
            showMsg ("Hilo principal esperó a las tareas una por una");
        }
    }

}