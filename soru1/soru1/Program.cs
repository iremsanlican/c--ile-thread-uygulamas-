using System;
using System.Collections;
using System.Threading;


namespace soru1
{
   
    internal class Program
    { 
    static ArrayList numbers = new ArrayList();
    static ArrayList evenNumbers = new ArrayList();
    static ArrayList oddNumbers = new ArrayList();
    static ArrayList primeNumbers = new ArrayList();

    
        static void Main(string[] args)
        {

            for (int i = 1; i <= 1000000; i++)
            {
                numbers.Add(i);
            }


            int size = numbers.Count / 4;
            int[] array = new int[4];
            for (int i = 0; i < 4; i++)
            {
                array[i] = i * size;
            }



            Thread[] threads = new Thread[4];
            for (int i = 0; i < 4; i++)
            {
                int start = array[i];
                int end = (i == 3) ? numbers.Count - 1 : array[i + 1] - 1;
                threads[i] = new Thread(() => Process(start, end));
                threads[i].Start();
            }


            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            Console.WriteLine("Asal Sayılar: " + string.Join(", ", primeNumbers.ToArray()));


            Console.WriteLine("Toplam Çift Sayılar: " + evenNumbers.Count);
            Console.WriteLine("Toplam Tek Sayılar: " + oddNumbers.Count);
            Console.WriteLine("Toplam Asal Sayılar: " + primeNumbers.Count);



        }

        static bool primecontrol(int num)
        {
            if (num < 2)
                return false;

            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }

        static void Process(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                int num = (int)numbers[i];
                if (num % 2 == 0)
                {
                    lock (evenNumbers.SyncRoot)
                    {
                        evenNumbers.Add(num);
                    }
                }

                else
                {
                    lock (oddNumbers.SyncRoot)
                    {
                        oddNumbers.Add(num);
                    }
                }
                if (primecontrol(num))
                {
                    lock (primeNumbers.SyncRoot)
                    {
                        primeNumbers.Add(num);
                    }
                }
            }
        }
    }
    }

