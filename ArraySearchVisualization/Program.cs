using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ArraySearchVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMethod();
        }

        static int[] GenerateRandomArray(int size, int min, int max)
        {
            Random randNum = new Random();
            int[] array = new int[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = randNum.Next(min, max);
            }
            return array;
        }

        static int[] GenerateIndexArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            return array;
        }

        static void PrintArrayConsole(int[] array)
        {
            Console.Write("[");
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write(" ");
                }
                Console.Write(array[i]);
            }
            Console.WriteLine("]");
        }

        static void TestSortedArraySearch(int[] array, int searchValue, int partitionSize)
        {
            Array.Sort(array);
            Console.WriteLine();
            Console.WriteLine("Search value = {0}", searchValue);
            Console.WriteLine("Array size = {0}", array.Length);
            int index;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            index = BinarySearch(array, searchValue);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("BinarySearch():");
            Console.WriteLine("Elapsed = {0}", sw.Elapsed);
            Console.WriteLine("Index = {0}", index);
            sw = new Stopwatch();
            sw.Start();
            index = SimpleSearch(array, searchValue);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("SimpleSearch():");
            Console.WriteLine("Elapsed = {0}", sw.Elapsed);
            Console.WriteLine("Index = {0}", index);
            sw = new Stopwatch();
            sw.Start();
            index = DumbSearch(array, searchValue, partitionSize);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("DumbSearch():");
            Console.WriteLine("Elapsed = {0}", sw.Elapsed);
            Console.WriteLine("Index = {0}", index);
            Console.WriteLine();
        }

        static void TestUnsortedArraySearch(int[] array, int searchValue, int partitionSize)
        {
            Console.WriteLine();
            Console.WriteLine("Search value = {0}", searchValue);
            Console.WriteLine("Array size = {0}", array.Length);
            int index;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            index = SimpleSearch(array, searchValue);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("SimpleSearch():");
            Console.WriteLine("Elapsed = {0}", sw.Elapsed);
            Console.WriteLine("Index = {0}", index);
            sw = new Stopwatch();
            sw.Start();
            index = DumbSearch(array, searchValue, partitionSize);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("DumbSearch():");
            Console.WriteLine("Elapsed = {0}", sw.Elapsed);
            Console.WriteLine("Index = {0}", index);
            Console.WriteLine();
        }

        static int TestSearchVisualConsole(int[] array, int searchValue)
        {
            Console.WriteLine("Choose method:");
            Console.WriteLine("1 > SimpleSearchVisualConsole()");
            Console.WriteLine("2 > BinarySearchVisualConsole()");
            Console.WriteLine("3 > DumbSearchVisualConsole()");
            bool parentRepeat = true;
            bool childRepeat = true;
            while (parentRepeat)
            {
                string input = Console.ReadLine().Trim();
                if (input.ToUpper() == "R")
                {
                    Console.Clear();
                    return 1;
                }
                if (input.ToUpper() == "Q")
                {
                    return 2;
                }
                if (int.TryParse(input, out int methodNumber))
                {
                    bool success = true;
                    int index;
                    switch (methodNumber)
                    {
                        case 1:
                            index = SimpleSearchVisualConsole(array, searchValue);
                            parentRepeat = false;
                            break;
                        case 2:
                            Array.Sort(array);
                            index = BinarySearchVisualConsole(array, searchValue);
                            parentRepeat = false;
                            break;
                        case 3:
                            Console.WriteLine("Enter partition size:");
                            int partitionSize = 0;
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    return 1;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return 2;
                                }
                                if (int.TryParse(input, out partitionSize))
                                {
                                    if (partitionSize < 0)
                                    {
                                        Console.WriteLine("Invalid partition size");
                                    }
                                    else
                                    {
                                        childRepeat = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid partition size");
                                }
                            }
                            index = DumbSearchVisualConsole(array, searchValue, partitionSize);
                            parentRepeat = false;
                            break;
                        default:
                            index = -1;
                            success = false;
                            Console.WriteLine("Invalid input");
                            break;
                    }
                    if (success)
                    {
                        Console.WriteLine("Search value = {0}", searchValue);
                        Console.WriteLine("Index = {0}", index);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            return 0;
        }

        static int TestSearchVisualFile(int[] array, int searchValue, string file)
        {
            Console.WriteLine("Choose method:");
            Console.WriteLine("1 > SimpleSearchVisualFile()");
            Console.WriteLine("2 > BinarySearchVisualFile()");
            Console.WriteLine("3 > DumbSearchVisualFile()");
            bool parentRepeat = true;
            bool childRepeat = true;
            while (parentRepeat)
            {
                string input = Console.ReadLine().Trim();
                if (input.ToUpper() == "R")
                {
                    Console.Clear();
                    return 1;
                }
                if (input.ToUpper() == "Q")
                {
                    return 2;
                }
                if (int.TryParse(input, out int methodNumber))
                {
                    bool success = true;
                    int index;
                    switch (methodNumber)
                    {
                        case 1:
                            index = SimpleSearchVisualFile(array, searchValue, file);
                            parentRepeat = false;
                            break;
                        case 2:
                            Array.Sort(array);
                            index = BinarySearchVisualFile(array, searchValue, file);
                            parentRepeat = false;
                            break;
                        case 3:
                            Console.WriteLine("Enter partition size:");
                            int partitionSize = 0;
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    return 1;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return 2;
                                }
                                if (int.TryParse(input, out partitionSize))
                                {
                                    if (partitionSize < 0)
                                    {
                                        Console.WriteLine("Invalid partition size");
                                    }
                                    else
                                    {
                                        childRepeat = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid partition size");
                                }
                            }
                            index = DumbSearchVisualFile(array, searchValue, partitionSize, file);
                            parentRepeat = false;
                            break;
                        default:
                            index = -1;
                            success = false;
                            Console.WriteLine("Invalid input");
                            break;
                    }
                    if (success)
                    {
                        Console.WriteLine("Search value = {0}", searchValue);
                        Console.WriteLine("Index = {0}", index);
                        using (StreamWriter sw = new StreamWriter(file, true))
                        {
                            sw.WriteLine("Search value = {0}", searchValue);
                            sw.WriteLine("Index = {0}", index);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            return 0;
        }

        static void MainMethod()
        {
        Restart:
            Console.WriteLine("R > Restart");
            Console.WriteLine("Q > Close application");
            Console.WriteLine("Choose array type:");
            Console.WriteLine("1 > GenerateRandomArray()");
            Console.WriteLine("2 > GenerateIndexArray()");
            Console.WriteLine("3 > Custom array");
            bool parentRepeat = true;
            bool childRepeat = true;
            string input;
            int[] array = new int[1];
            int searchValue = 0;
            int testSearchVisualReturnValue;
            while (parentRepeat)
            {
                input = Console.ReadLine().Trim();
                if (input.ToUpper() == "R")
                {
                    Console.Clear();
                    goto Restart;
                }
                if (input.ToUpper() == "Q")
                {
                    return;
                }
                if (int.TryParse(input, out int arrayType))
                {
                    int size = 10;
                    switch (arrayType)
                    {
                        case 1:
                            Console.WriteLine("Enter array size:");
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    goto Restart;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return;
                                }
                                if (int.TryParse(input, out size))
                                {
                                    if (size <= 0)
                                    {
                                        Console.WriteLine("Invalid array size");
                                    }
                                    else
                                    {
                                        childRepeat = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid array size");
                                }
                            }
                            int min = 0;
                            int max = 10;
                            Console.WriteLine("Enter min value:");
                            childRepeat = true;
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    goto Restart;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return;
                                }
                                if (!int.TryParse(input, out min))
                                {
                                    Console.WriteLine("Invalid min value");
                                }
                                else
                                {
                                    childRepeat = false;
                                }
                            }
                            Console.WriteLine("Enter max value:");
                            childRepeat = true;
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    goto Restart;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return;
                                }
                                if (!int.TryParse(input, out max))
                                {
                                    Console.WriteLine("Invalid max value");
                                }
                                else
                                {
                                    childRepeat = false;
                                }
                            }
                            if (min > max)
                            {
                                int temp = min;
                                min = max;
                                max = temp;
                                Console.WriteLine(string.Format("Swapped min and max values: min = {0} | max = {1}", min, max));
                            }
                            array = GenerateRandomArray(size, min, max);
                            parentRepeat = false;
                            break;
                        case 2:
                            Console.WriteLine("Enter array size:");
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    goto Restart;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return;
                                }
                                if (int.TryParse(input, out size))
                                {
                                    if (size <= 0)
                                    {
                                        Console.WriteLine("Invalid array size");
                                    }
                                    else
                                    {
                                        childRepeat = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid array size");
                                }
                            }
                            array = GenerateIndexArray(size);
                            parentRepeat = false;
                            break;
                        case 3:
                            Console.WriteLine("Enter array values:");
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    goto Restart;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return;
                                }
                                string arrayValuesString = input;
                                List<int> arrayValues = new List<int>();
                                for (int i = 0; i < arrayValuesString.Length; i++)
                                {
                                    string arrayValueString = "";
                                    while (char.IsNumber(arrayValuesString[i]))
                                    {
                                        arrayValueString += arrayValuesString[i++];
                                        if (i >= arrayValuesString.Length)
                                        {
                                            break;
                                        }
                                    }
                                    if (int.TryParse(arrayValueString, out int arrayValue))
                                    {
                                        arrayValues.Add(arrayValue);
                                    }
                                }
                                if (arrayValues.Count > 0)
                                {
                                    array = arrayValues.ToArray();
                                    PrintArrayConsole(array);
                                    childRepeat = false;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid array values");
                                }
                            }
                            parentRepeat = false;
                            break;
                        default:
                            Console.WriteLine("Invalid array type");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid array type");
                }
            }
            Console.WriteLine("Enter search value:");
            parentRepeat = true;
            while (parentRepeat)
            {
                input = Console.ReadLine().Trim();
                if (input.ToUpper() == "R")
                {
                    Console.Clear();
                    goto Restart;
                }
                if (input.ToUpper() == "Q")
                {
                    return;
                }
                if (!int.TryParse(input, out searchValue))
                {
                    Console.WriteLine("Invalid search value");
                }
                else
                {
                    parentRepeat = false;
                }
            }
            Console.WriteLine("Choose method:");
            Console.WriteLine("1 > TestSortedArraySearch()");
            Console.WriteLine("2 > TestUnsortedArraySearch()");
            Console.WriteLine("3 > TestSearchVisualConsole()");
            Console.WriteLine("4 > TestSearchVisualFile()");
            parentRepeat = true;
            while (parentRepeat)
            {
                input = Console.ReadLine().Trim();
                if (input.ToUpper() == "R")
                {
                    Console.Clear();
                    goto Restart;
                }
                if (input.ToUpper() == "Q")
                {
                    return;
                }
                if (int.TryParse(input, out int methodNumber))
                {
                    int partitionSize = 0;
                    switch (methodNumber)
                    {
                        case 1:
                            Console.WriteLine("Enter partition size:");
                            childRepeat = true;
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    goto Restart;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return;
                                }
                                if (int.TryParse(input, out partitionSize))
                                {
                                    if (partitionSize < 0)
                                    {
                                        Console.WriteLine("Invalid partition size");
                                    }
                                    else
                                    {
                                        childRepeat = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid partition size");
                                }
                            }
                            TestSortedArraySearch(array, searchValue, partitionSize);
                            parentRepeat = false;
                            break;
                        case 2:
                            Console.WriteLine("Enter partition size:");
                            childRepeat = true;
                            while (childRepeat)
                            {
                                input = Console.ReadLine().Trim();
                                if (input.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    goto Restart;
                                }
                                if (input.ToUpper() == "Q")
                                {
                                    return;
                                }
                                if (int.TryParse(input, out partitionSize))
                                {
                                    if (partitionSize < 0)
                                    {
                                        Console.WriteLine("Invalid partition size");
                                    }
                                    else
                                    {
                                        childRepeat = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid partition size");
                                }
                            }
                            TestUnsortedArraySearch(array, searchValue, partitionSize);
                            parentRepeat = false;
                            break;
                        case 3:
                            testSearchVisualReturnValue = TestSearchVisualConsole(array, searchValue);
                            if (testSearchVisualReturnValue == 1)
                            {
                                goto Restart;
                            }
                            if (testSearchVisualReturnValue == 2)
                            {
                                return;
                            }
                            parentRepeat = false;
                            break;
                        case 4:
                            Console.WriteLine("Enter file path:");
                            string file = "file.txt";
                            childRepeat = true;
                            while (childRepeat)
                            {
                                bool success = true;
                                file = Console.ReadLine().Trim();
                                if (file.ToUpper() == "R")
                                {
                                    Console.Clear();
                                    goto Restart;
                                }
                                if (file.ToUpper() == "Q")
                                {
                                    return;
                                }
                                try
                                {
                                    File.WriteAllText(file, string.Empty);
                                }
                                catch (Exception e)
                                {
                                    success = false;
                                    Console.WriteLine("Invalid file path: {0}", e.Message);
                                }
                                if (success)
                                {
                                    childRepeat = false;
                                }
                            }
                            testSearchVisualReturnValue = TestSearchVisualFile(array, searchValue, file);
                            if (testSearchVisualReturnValue == 1)
                            {
                                goto Restart;
                            }
                            if (testSearchVisualReturnValue == 2)
                            {
                                return;
                            }
                            parentRepeat = false;
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            Console.WriteLine("Choose action:");
            Console.WriteLine("R > Restart");
            Console.WriteLine("Q > Close application");
            while (true)
            {
                input = Console.ReadLine().Trim().ToUpper();
                switch (input)
                {
                    case "R":
                        Console.Clear();
                        goto Restart;
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        static int BinarySearch(int[] array, int searchValue)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            int startIndex = 0;
            int endIndex = array.Length - 1;
            int distance = endIndex - startIndex + 1;
            int middleIndex = distance / 2;
            while (distance > 0)
            {
                if (array[middleIndex] == searchValue)
                {
                    return middleIndex;
                }
                if (array[middleIndex] > searchValue)
                {
                    endIndex = middleIndex - 1;
                }
                else
                {
                    startIndex = middleIndex + 1;
                }
                distance = endIndex - startIndex + 1;
                middleIndex = distance / 2 + startIndex;
            }
            return -1;
        }

        static int BinarySearchVisualConsole(int[] array, int searchValue)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            int startIndex = 0;
            int endIndex = array.Length - 1;
            int distance = endIndex - startIndex + 1;
            int middleIndex = distance / 2;
            int index = -1;
            while (distance > 0)
            {
                Console.Write("[");
                for (int i = 0; i < startIndex; i++)
                {
                    if (i > 0)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(array[i]);
                }
                if (middleIndex != startIndex)
                {
                    if (startIndex > 0)
                    {
                        Console.Write(" ");
                    }
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(array[startIndex]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    for (int i = startIndex + 1; i < middleIndex; i++)
                    {
                        Console.Write(" " + array[i]);
                    }
                }
                if (array[middleIndex] == searchValue)
                {
                    index = middleIndex;
                }
                if (middleIndex > 0)
                {
                    Console.Write(" ");
                }
                if (index >= 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                }
                Console.Write(array[middleIndex]);
                Console.BackgroundColor = ConsoleColor.Black;
                if (middleIndex != endIndex)
                {
                    for (int i = middleIndex + 1; i < endIndex; i++)
                    {
                        Console.Write(" " + array[i]);
                    }
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(array[endIndex]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                for (int i = endIndex + 1; i < array.Length; i++)
                {
                    Console.Write(" " + array[i]);
                }
                Console.WriteLine("]");
                if (index >= 0)
                {
                    break;
                }
                if (array[middleIndex] > searchValue)
                {
                    endIndex = middleIndex - 1;
                }
                else
                {
                    startIndex = middleIndex + 1;
                }
                distance = endIndex - startIndex + 1;
                middleIndex = distance / 2 + startIndex;
            }
            return index;
        }

        static int BinarySearchVisualFile(int[] array, int searchValue, string file)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            int startIndex = 0;
            int endIndex = array.Length - 1;
            int distance = endIndex - startIndex + 1;
            int middleIndex = distance / 2;
            int index = -1;
            File.WriteAllText(file, string.Empty);
            using (StreamWriter sw = new StreamWriter(file))
            {
                while (distance > 0)
                {
                    for (int i = 0; i < startIndex; i++)
                    {
                        sw.Write(" " + array[i] + " ");
                    }
                    if (middleIndex != startIndex)
                    {
                        sw.Write("[" + array[startIndex] + "]");
                        for (int i = startIndex + 1; i < middleIndex; i++)
                        {
                            sw.Write(" " + array[i] + " ");
                        }
                    }
                    if (array[middleIndex] == searchValue)
                    {
                        index = middleIndex;
                    }
                    if (index >= 0)
                    {
                        sw.Write("{" + array[middleIndex] + "}");
                    }
                    else
                    {
                        sw.Write("(" + array[middleIndex] + ")");
                    }
                    if (middleIndex != endIndex)
                    {
                        for (int i = middleIndex + 1; i < endIndex; i++)
                        {
                            sw.Write(" " + array[i] + " ");
                        }
                        sw.Write("[" + array[endIndex] + "]");
                    }
                    for (int i = endIndex + 1; i < array.Length; i++)
                    {
                        sw.Write(" " + array[i] + " ");
                    }
                    sw.WriteLine();
                    if (index >= 0)
                    {
                        break;
                    }
                    if (array[middleIndex] > searchValue)
                    {
                        endIndex = middleIndex - 1;
                    }
                    else
                    {
                        startIndex = middleIndex + 1;
                    }
                    distance = endIndex - startIndex + 1;
                    middleIndex = distance / 2 + startIndex;
                }
            }
            return index;
        }

        static int SimpleSearch(int[] array, int searchValue)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == searchValue)
                {
                    return i;
                }
            }
            return -1;
        }

        static int SimpleSearchVisualConsole(int[] array, int searchValue)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            int index = -1;
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write("[");
                for (int j = 0; j < i; j++)
                {
                    if (j > 0)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(array[j]);
                }
                if (i > 0)
                {
                    Console.Write(" ");
                }
                if (array[i] == searchValue && index == -1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    index = i;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.Write(array[i]);
                Console.BackgroundColor = ConsoleColor.Black;
                for (int j = i + 1; j < array.Length; j++)
                {
                    Console.Write(" " + array[j]);
                }
                Console.WriteLine("]");
                if (index >= 0)
                {
                    break;
                }
            }
            return index;
        }

        static int SimpleSearchVisualFile(int[] array, int searchValue, string file)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            int index = -1;
            File.WriteAllText(file, string.Empty);
            using (StreamWriter sw = new StreamWriter(file))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        sw.Write(" " + array[j] + " ");
                    }
                    if (array[i] == searchValue && index == -1)
                    {
                        sw.Write("{" + array[i] + "}");
                        index = i;
                    }
                    else
                    {
                        sw.Write("[" + array[i] + "]");
                    }
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        sw.Write(" " + array[j] + " ");
                    }
                    sw.WriteLine();
                    if (index >= 0)
                    {
                        break;
                    }
                }
            }
            return index;
        }

        static int DumbSearch(int[] array, int searchValue, int partitionSize)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            int partitionCount = 0;
            if (partitionSize > 0 && array.Length > 1)
            {
                partitionCount = (array.Length - 2) / partitionSize;
            }
            int[] indexes = new int[partitionCount * 2 + 2];
            indexes[0] = 0;
            indexes[indexes.Length - 1] = array.Length - 1;
            for (int i = 1, partitionIndex = partitionSize; i < indexes.Length - 2; i += 2, partitionIndex += partitionSize)
            {
                indexes[i] = partitionIndex;
                indexes[i + 1] = partitionIndex;
            }
            while (indexes[0] <= indexes[1])
            {
                if (indexes[0] <= indexes[1])
                {
                    if (array[indexes[0]] == searchValue)
                    {
                        return indexes[0];
                    }
                    indexes[0]++;
                }
                for (int i = 1; i < indexes.Length - 2; i += 2)
                {
                    if (indexes[i - 1] <= indexes[i])
                    {
                        if (array[indexes[i]] == searchValue)
                        {
                            return indexes[i];
                        }
                        indexes[i]--;
                    }
                    if (indexes[i + 1] <= indexes[i + 2])
                    {
                        if (array[indexes[i + 1]] == searchValue)
                        {
                            return indexes[i + 1];
                        }
                        indexes[i + 1]++;
                    }
                }
                if (indexes[indexes.Length - 2] <= indexes[indexes.Length - 1])
                {
                    if (array[indexes[indexes.Length - 1]] == searchValue)
                    {
                        return indexes[indexes.Length - 1];
                    }
                    indexes[indexes.Length - 1]--;
                }
            }
            return -1;
        }

        static int DumbSearchVisualConsole(int[] array, int searchValue, int partitionSize)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            int partitionCount = 0;
            if (partitionSize > 0 && array.Length > 1)
            {
                partitionCount = (array.Length - 2) / partitionSize;
            }
            int[] indexes = new int[partitionCount * 2 + 2];
            indexes[0] = 0;
            indexes[indexes.Length - 1] = array.Length - 1;
            for (int i = 1, partitionIndex = partitionSize; i < indexes.Length - 2; i += 2, partitionIndex += partitionSize)
            {
                indexes[i] = partitionIndex;
                indexes[i + 1] = partitionIndex;
            }
            int index = -1;
            while (indexes[0] <= indexes[1])
            {
                Console.Write("[");
                List<int> checkedIndexes = new List<int>();
                if (indexes[0] <= indexes[1])
                {
                    if (array[indexes[0]] == searchValue && index == -1)
                    {
                        index = indexes[0];
                    }
                    checkedIndexes.Add(indexes[0]);
                    indexes[0]++;
                }
                for (int i = 1; i < indexes.Length - 2; i += 2)
                {
                    if (indexes[i - 1] <= indexes[i])
                    {
                        if (array[indexes[i]] == searchValue && index == -1)
                        {
                            index = indexes[i];
                        }
                        checkedIndexes.Add(indexes[i]);
                        indexes[i]--;
                    }
                    if (indexes[i + 1] <= indexes[i + 2])
                    {
                        if (indexes[i] + 1 != indexes[i + 1])
                        {
                            if (array[indexes[i + 1]] == searchValue && index == -1)
                            {
                                index = indexes[i + 1];
                            }
                            checkedIndexes.Add(indexes[i + 1]);
                        }
                        indexes[i + 1]++;
                    }
                }
                if (indexes[indexes.Length - 2] <= indexes[indexes.Length - 1])
                {
                    if (array[indexes[indexes.Length - 1]] == searchValue && index == -1)
                    {
                        index = indexes[indexes.Length - 1];
                    }
                    checkedIndexes.Add(indexes[indexes.Length - 1]);
                    indexes[indexes.Length - 1]--;
                }
                for (int i = 0; i < checkedIndexes[0]; i++)
                {
                    if (i > 0)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(array[i]);
                }
                for (int i = 0; i < checkedIndexes.Count - 1; i++)
                {
                    if (checkedIndexes[i] > 0)
                    {
                        Console.Write(" ");
                    }
                    if (index == checkedIndexes[i])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(array[checkedIndexes[i]]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    for (int j = checkedIndexes[i] + 1; j < checkedIndexes[i + 1]; j++)
                    {
                        Console.Write(" " + array[j]);
                    }
                }
                if (checkedIndexes[checkedIndexes.Count - 1] > 0)
                {
                    Console.Write(" ");
                }
                if (index == checkedIndexes[checkedIndexes.Count - 1])
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.Write(array[checkedIndexes[checkedIndexes.Count - 1]]);
                Console.BackgroundColor = ConsoleColor.Black;
                for (int i = checkedIndexes[checkedIndexes.Count - 1] + 1; i < array.Length; i++)
                {
                    Console.Write(" " + array[i]);
                }
                Console.WriteLine("]");
                if (index >= 0)
                {
                    break;
                }
            }
            return index;
        }

        static int DumbSearchVisualFile(int[] array, int searchValue, int partitionSize, string file)
        {
            if (array == null || array.Length == 0)
            {
                return -2;
            }
            int partitionCount = 0;
            if (partitionSize > 0 && array.Length > 1)
            {
                partitionCount = (array.Length - 2) / partitionSize;
            }
            int[] indexes = new int[partitionCount * 2 + 2];
            indexes[0] = 0;
            indexes[indexes.Length - 1] = array.Length - 1;
            for (int i = 1, partitionIndex = partitionSize; i < indexes.Length - 2; i += 2, partitionIndex += partitionSize)
            {
                indexes[i] = partitionIndex;
                indexes[i + 1] = partitionIndex;
            }
            int index = -1;
            File.WriteAllText(file, string.Empty);
            using (StreamWriter sw = new StreamWriter(file))
            {
                while (indexes[0] <= indexes[1])
                {
                    List<int> checkedIndexes = new List<int>();
                    if (indexes[0] <= indexes[1])
                    {
                        if (array[indexes[0]] == searchValue && index == -1)
                        {
                            index = indexes[0];
                        }
                        checkedIndexes.Add(indexes[0]);
                        indexes[0]++;
                    }
                    for (int i = 1; i < indexes.Length - 2; i += 2)
                    {
                        if (indexes[i - 1] <= indexes[i])
                        {
                            if (array[indexes[i]] == searchValue && index == -1)
                            {
                                index = indexes[i];
                            }
                            checkedIndexes.Add(indexes[i]);
                            indexes[i]--;
                        }
                        if (indexes[i + 1] <= indexes[i + 2])
                        {
                            if (indexes[i] + 1 != indexes[i + 1])
                            {
                                if (array[indexes[i + 1]] == searchValue && index == -1)
                                {
                                    index = indexes[i + 1];
                                }
                                checkedIndexes.Add(indexes[i + 1]);
                            }
                            indexes[i + 1]++;
                        }
                    }
                    if (indexes[indexes.Length - 2] <= indexes[indexes.Length - 1])
                    {
                        if (array[indexes[indexes.Length - 1]] == searchValue && index == -1)
                        {
                            index = indexes[indexes.Length - 1];
                        }
                        checkedIndexes.Add(indexes[indexes.Length - 1]);
                        indexes[indexes.Length - 1]--;
                    }
                    for (int i = 0; i < checkedIndexes[0]; i++)
                    {
                        sw.Write(" " + array[i] + " ");
                    }
                    for (int i = 0; i < checkedIndexes.Count - 1; i++)
                    {
                        if (index == checkedIndexes[i])
                        {
                            sw.Write("{" + array[checkedIndexes[i]] + "}");
                        }
                        else
                        {
                            sw.Write("[" + array[checkedIndexes[i]] + "]");
                        }
                        for (int j = checkedIndexes[i] + 1; j < checkedIndexes[i + 1]; j++)
                        {
                            sw.Write(" " + array[j] + " ");
                        }
                    }
                    if (index == checkedIndexes[checkedIndexes.Count - 1])
                    {
                        sw.Write("{" + array[checkedIndexes[checkedIndexes.Count - 1]] + "}");
                    }
                    else
                    {
                        sw.Write("[" + array[checkedIndexes[checkedIndexes.Count - 1]] + "]");
                    }
                    for (int i = checkedIndexes[checkedIndexes.Count - 1] + 1; i < array.Length; i++)
                    {
                        sw.Write(" " + array[i] + " ");
                    }
                    sw.WriteLine();
                    if (index >= 0)
                    {
                        break;
                    }
                }
            }
            return index;
        }
    }
}
