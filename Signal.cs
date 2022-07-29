using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТИК_2
{
    class Signal
    {

        private int A, B, C, T;
        private int k, n, m;
        private double[] U,t;
        private double[] X;
        private int p,limit,size;
        private double delta, IntegralSignal,IntegralNoise;

        public Signal()
        {
            A = 12;
            B = 5;
            C = 1;
            k = 14;
            n = k + 2;
            m = k + 4;
            T = 2;
            p = 0;
        }

        public double Fm()
        {
            double fm;


            fm = m / (Math.PI*2);
            return fm;
        }

        public int N()
        {
            int n;
            double fm = Fm();
            double tmp;

            tmp = 2 * T * fm + 1; //Деламе расчет количестов точек 
            n = (int)Math.Ceiling(tmp); //округляем до большего
 
            return n;
        }


        public double Delta() //Считаем шаг
        {
            double delta;
            int n;
            n = N();// КОличество интервалов

            delta = (double)T / n; // Делим период на количество интервалов 

            return delta;
        }


        public int Ut() // Рассчитываем уровень сигнала
        {
            delta = Delta(); //Вызываем функцию для расчета шага 
            size = N()+1;
            U = new double[size];
            t = new double[size];

            double f = 0;
            p = 0;
            for (int i = 0; i < size; i++)
            {

                U[i] = (A * Math.Sin(k * f) + B * Math.Cos(n * f) + C * Math.Sin(m * f)) + 15; // КОнтрольные точки сигнала
                t[i] = f; //В массив заносим точки интервалов
                f += delta;
                p++;
            }
            return p;
        }

        public void Xt() // Расччитываем уровень шума
        {
            Random rnd = new Random();
            X = new double[size];
           
            for(int i=0;i<size;i++)
            {
                X[i] = (double)(rnd.Next(-5,5))/10+1;
            }
        }


        public void Display()// Функция вывода на экран
        {
            limit = Ut();
            Console.WriteLine($"t  = {Math.Round(t[0],3)}                  U = {Math.Round(U[0], 3)} \t          X = {Math.Round(X[0], 3)}");
            for (int i = 1; i < limit-1; i++)
            {
                Console.WriteLine($"t = {Math.Round(t[i],3)}               U = {Math.Round(U[i], 3)} \t  X = {Math.Round(X[i],3)}");

            }
            Console.WriteLine($"t  = {Math.Round(t[limit-1], 3)}                  U = {Math.Round(U[limit-1], 3)}        X = {Math.Round(X[limit-1], 3)}");
        }

        public void Psignal() // рассчитываем мощность полезного сигнала
        {
            double tmp=0;
            
            for(int i=0;i<size-1;i++)
            {
                tmp += (((U[i] + U[i + 1]) / 2) * delta)* (((U[i] + U[i + 1]) / 2) * delta);
            }

            IntegralSignal = ((1 /(double) T) * tmp);
            Console.WriteLine($"Мощность полезного сигнала = {Math.Round(IntegralSignal,3)}");
        }


        public void Pnoise() //  рассчитываем мощщность шума
        {
            double tmp = 0;
         
            for (int i = 0; i < size - 1; i++)
            {
                tmp += (((X[i]) + X[i + 1] / 2) * delta)*(((X[i]) + X[i + 1] / 2) * delta);

            }
            IntegralNoise = ((1 / (double)T) * tmp);
            Console.WriteLine($"Мощность Шума = {Math.Round(IntegralNoise,3)}");
        }

        public void Levels() // Делаеам расчет количества уровней в сингнале
        {
            int L;

            L =(int)Math.Floor(Math.Sqrt((IntegralSignal/IntegralNoise)+1));
            Console.WriteLine($"Количество различимых уровней сигнала = {L}");
        }

        public void QuantityInformation() // Рассчитываем количестов информации( все формулы в лекциях были все по ним)
        {
            double I;
            double fm = Fm();
            I = (2 * T * fm + 1) * Math.Log(2, Math.Sqrt((IntegralNoise + IntegralSignal) / IntegralNoise));
            Console.WriteLine($"Количество информации в непрерывном сообщении = {Math.Round(I,3)}");
        }

        public void Entropy() // Расчет энтропии
        {
            double H;
            H = Math.Log(2,Math.Sqrt((IntegralNoise + IntegralSignal) / IntegralNoise));
            Console.WriteLine($"Энтропия непрерывного сообщения = {Math.Round(H,3)}");
        }
    }
}
