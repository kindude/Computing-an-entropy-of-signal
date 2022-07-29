using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТИК_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Signal S1 = new Signal();
            S1.Fm();
            S1.N();
            S1.Delta();
            S1.Ut();
            S1.Xt();
            S1.Display();
            S1.Psignal();
            S1.Pnoise();
            S1.Levels();
            S1.QuantityInformation();
            S1.Entropy();
            Console.ReadKey();
        }
    }
}
