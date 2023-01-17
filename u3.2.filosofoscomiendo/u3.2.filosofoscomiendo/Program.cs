using System;
using System.Threading;

namespace u3._2.filosofoscomiendo
{
    public class TableController
    {
        int count = 5;
        
        public static int[] Palillos;
        public static Filo[] Filos;
        public static TakePut[] Takes;
        public static object o = new object();

        public TableController()
        {
            Palillos = new int[count];
            Filos = new Filo[count];
            Takes = new TakePut[count];

            for (int i = 0; i < Filos.Length; i++)
            {
                Filo Filotemp = new Filo(i);
                TakePut Take = new TakePut();
                Palillos[i] = 1;
                Take.num = i;
                Filos[i] = Filotemp;
                Takes[i] = Take;
                Filos[i].init();
            }
        }

        public class Filo
        {
            int num;

            public Filo(int num)
            {
                this.num = num;
            }

            public void init()
            {
                Takes[num].num = num;
                Thread t = new Thread(new ThreadStart(Takes[num].init));
                t.Start();
            }
        }

        public class TakePut
        {
            bool stop = false;
            int time = 0;
            int numnext = 0;
            public int num = 0;

            public void init()
            {
                if (num == Palillos.Length - 1) numnext = 0;
                else numnext = num + 1;

                while (stop == false)
                {
                    lock (o)
                    {
                        if (Palillos[num] == 1 && Palillos[numnext] == 1)
                        {
                            time = 1;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Filosofo {0} tiene pallillos : Tiempo {1}", num, time);

                            Palillos[num] = 0;
                            Palillos[numnext] = 0;
                        }
                        else if (Palillos[num] == 0 || Palillos[numnext] == 0)
                        {
                            if (time == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Filosofo {0} esta esperando : Tiempo de espera...{1}", num, time);
                            }
                            else if (time <= 5)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("Filosofo {0} esta cominedo : Tiempo {1}", num, time);
                                time++;
                            }
                            else if (time > 5)
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("Filosofo {0} ya no tiene pallillos", num);

                                Palillos[num] = 1;
                                Palillos[numnext] = 1;

                                time = 0;
                            }
                        }

                        Thread.Sleep(300);
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TableController table = new TableController();
        }
    }
}