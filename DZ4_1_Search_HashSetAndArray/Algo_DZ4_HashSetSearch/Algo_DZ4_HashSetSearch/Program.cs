using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Algo_DZ4_HashSetSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            * Alerxander Fakhrudinov = Александр Фахрудинов
            * asbuka@gmail.com
            * 
            * 1. Протестируйте поиск строки в HashSet и в массиве
            * Заполните массив и HashSet случайными строками, не менее 10 000 строк. 
            * Строки можно сгенерировать. 
            * Потом выполните замер производительности проверки наличия строки в массиве и HashSet. 
            * Выложите код и результат замеров.
            */

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
