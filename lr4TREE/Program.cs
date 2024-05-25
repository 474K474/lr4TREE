using lr4TREE;
using System;




class Program
{
    static void Main(string[] args)
    {
        AVLTree tree = new AVLTree();
        GenerateRandomSequence(tree);

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Добавить узел");
            Console.WriteLine("2. Удалить узел");
            Console.WriteLine("3. Найти узел");
            Console.WriteLine("4. Найти узел в поддереве");
            Console.WriteLine("5. Показать дерево");
            Console.WriteLine("6. Выйти");

            Console.Write("\nВведите номер действия: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите значение для добавления: ");
                    if (int.TryParse(Console.ReadLine(), out int insertValue))
                    {
                        tree.Insert(insertValue);
                        Console.WriteLine("Узел добавлен.");
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение.");
                    }
                    break;

                case "2":
                    Console.Write("Введите значение для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteValue))
                    {
                        tree.Delete(deleteValue);
                        Console.WriteLine("Узел удален.");
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение.");
                    }
                    break;

                case "3":
                    Console.Write("Введите значение для поиска: ");
                    if (int.TryParse(Console.ReadLine(), out int searchValue))
                    {
                        bool found = tree.Search(searchValue);
                        Console.WriteLine(found ? "Узел найден." : "Узел не найден.");
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение.");
                    }
                    break;

                case "4":
                    Console.Write("Введите значение корня поддерева: ");
                    if (int.TryParse(Console.ReadLine(), out int subtreeRootValue))
                    {
                        Console.Write("Введите значение для поиска в поддереве: ");
                        if (int.TryParse(Console.ReadLine(), out int subtreeSearchValue))
                        {
                            bool foundInSubtree = tree.SearchInSubtree(subtreeRootValue, subtreeSearchValue);
                            Console.WriteLine(foundInSubtree ? "Узел найден в поддереве." : "Узел не найден в поддереве.");
                        }
                        else
                        {
                            Console.WriteLine("Некорректное значение для поиска в поддереве.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение корня поддерева.");
                    }
                    break;

                case "5":
                    Console.WriteLine("\nБинарное дерево:");
                    tree.PrintTree();
                    break;

                case "6":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите действие от 1 до 6.");
                    break;
            }
        }
    }

    static void GenerateRandomSequence(AVLTree tree)
    {
        Random rand = new Random();
        int size = rand.Next(10, 21); // Размер случайной последовательности от 10 до 20

        Console.WriteLine("Случайная последовательность:");
        for (int i = 0; i < size; i++)
        {
            int value = rand.Next(1, 101); // Случайные значения от 1 до 100
            Console.Write(value + " ");
            tree.Insert(value);
        }
        Console.WriteLine();
    }
}
