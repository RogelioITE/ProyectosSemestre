using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static int sumaTotalConcurrente = 0;
    static object lockObject = new object();

    // Método para verificar si un número es primo
    static bool EsPrimo(int numero)
    {
        if (numero < 2) return false; // Menores a 2 no son primos
        if (numero == 2) return true; // 2 es primo
        if (numero % 2 == 0) return false; // Números pares mayores a 2 no son primos
        
        for (int i = 3; i * i <= numero; i += 2) // Solo verificamos números impares
        {
            if (numero % i == 0) return false; // Si es divisible por otro número, no es primo
        }
        return true; // Si no se encontró divisor, es primo
    }

    // Cálculo secuencial
    static int CalcularSumaPrimosSecuencial(int N)
    {
        int suma = 0;
        for (int i = 2; i <= N; i++) // Recorre desde 2 hasta N
        {
            if (EsPrimo(i)) // Si es primo
            {
                suma += i; // Lo suma al total
            }
        }
        return suma; // Devuelve la suma de todos los primos hasta N
    }

    // Cálculo concurrente con hilos
    static void CalcularPrimosConcurrente(object rango)
    {
        (int inicio, int fin) = ((int, int))rango;
        int suma = 0;

        for (int i = inicio; i <= fin; i++) // Solo revisa los números en su segmento
        {
            if (EsPrimo(i))
            {
                suma += i;
            }
        }

        // Bloqueo para evitar que varios hilos modifiquen la suma total al mismo tiempo
        lock (lockObject)
        {
            sumaTotalConcurrente += suma;
        }
    }

    static void Main()
    {
        Console.Write("Ingrese el número límite N: ");
        int N = int.Parse(Console.ReadLine());

        Console.Write("Ingrese el número de hilos M: ");
        int M = int.Parse(Console.ReadLine());

        // Cálculo secuencial
        Stopwatch stopwatch = Stopwatch.StartNew();
        int sumaSecuencial = CalcularSumaPrimosSecuencial(N);
        stopwatch.Stop();
        Console.WriteLine($"Suma secuencial: {sumaSecuencial}");
        Console.WriteLine($"Tiempo secuencial: {stopwatch.ElapsedMilliseconds} ms\n");

        // Cálculo concurrente con hilos
        sumaTotalConcurrente = 0;
        Thread[] hilos = new Thread[M];
        int rango = N / M;
        stopwatch.Restart();

        for (int i = 0; i < M; i++)
        {
            int inicio = i * rango + 1;
            int fin = (i == M - 1) ? N : inicio + rango - 1;
            hilos[i] = new Thread(CalcularPrimosConcurrente);
            hilos[i].Start((inicio, fin));
        }

        // Esperar a que todos los hilos terminen
        foreach (var hilo in hilos)
        {
            hilo.Join();
        }

        stopwatch.Stop();
        Console.WriteLine($"Suma concurrente: {sumaTotalConcurrente}");
        Console.WriteLine($"Tiempo concurrente: {stopwatch.ElapsedMilliseconds} ms");
    }
}