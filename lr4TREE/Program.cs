using System;

public class TreeNode
{
    public int Value;
    public TreeNode Left;
    public TreeNode Right;
    public int Height;

    public TreeNode(int value)
    {
        Value = value;
        Left = null;
        Right = null;
        Height = 1;
    }
}

public class AVLTree
{
    public TreeNode Root;

    public AVLTree()
    {
        Root = null;
    }

    public void Insert(int value)
    {
        Root = InsertRec(Root, value);
    }

    private TreeNode InsertRec(TreeNode node, int value)
    {
        if (node == null)
        {
            return new TreeNode(value);
        }

        if (value < node.Value)
        {
            node.Left = InsertRec(node.Left, value);
        }
        else if (value > node.Value)
        {
            node.Right = InsertRec(node.Right, value);
        }
        else
        {
            return node; // Дубликаты не разрешены
        }

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        int balance = GetBalance(node);

        // Левый левый случай
        if (balance > 1 && value < node.Left.Value)
        {
            return RotateRight(node);
        }

        // Правый правый случай
        if (balance < -1 && value > node.Right.Value)
        {
            return RotateLeft(node);
        }

        // Левый правый случай
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        // Правый левый случай
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    private TreeNode RotateRight(TreeNode y)
    {
        TreeNode x = y.Left;
        TreeNode T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

        return x;
    }

    private TreeNode RotateLeft(TreeNode x)
    {
        TreeNode y = x.Right;
        TreeNode T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

        return y;
    }

    private int Height(TreeNode node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.Height;
    }

    private int GetBalance(TreeNode node)
    {
        if (node == null)
        {
            return 0;
        }
        return Height(node.Left) - Height(node.Right);
    }

    public void Delete(int value)
    {
        Root = DeleteRec(Root, value);
    }

    private TreeNode DeleteRec(TreeNode root, int value)
    {
        if (root == null)
        {
            return root;
        }

        if (value < root.Value)
        {
            root.Left = DeleteRec(root.Left, value);
        }
        else if (value > root.Value)
        {
            root.Right = DeleteRec(root.Right, value);
        }
        else
        {
            if ((root.Left == null) || (root.Right == null))
            {
                TreeNode temp = root.Left ?? root.Right;

                if (temp == null)
                {
                    temp = root;
                    root = null;
                }
                else
                {
                    root = temp;
                }
            }
            else
            {
                TreeNode temp = MinValueNode(root.Right);
                root.Value = temp.Value;
                root.Right = DeleteRec(root.Right, temp.Value);
            }
        }

        if (root == null)
        {
            return root;
        }

        root.Height = 1 + Math.Max(Height(root.Left), Height(root.Right));

        int balance = GetBalance(root);

        if (balance > 1 && GetBalance(root.Left) >= 0)
        {
            return RotateRight(root);
        }

        if (balance > 1 && GetBalance(root.Left) < 0)
        {
            root.Left = RotateLeft(root.Left);
            return RotateRight(root);
        }

        if (balance < -1 && GetBalance(root.Right) <= 0)
        {
            return RotateLeft(root);
        }

        if (balance < -1 && GetBalance(root.Right) > 0)
        {
            root.Right = RotateRight(root.Right);
            return RotateLeft(root);
        }

        return root;
    }

    private TreeNode MinValueNode(TreeNode node)
    {
        TreeNode current = node;

        while (current.Left != null)
        {
            current = current.Left;
        }

        return current;
    }

    public bool Search(int value)
    {
        return SearchRec(Root, value);
    }

    private bool SearchRec(TreeNode root, int value)
    {
        if (root == null)
        {
            return false;
        }

        if (value == root.Value)
        {
            return true;
        }

        if (value < root.Value)
        {
            return SearchRec(root.Left, value);
        }
        else
        {
            return SearchRec(root.Right, value);
        }
    }

    public bool SearchInSubtree(int rootValue, int searchValue)
    {
        TreeNode rootNode = FindNode(Root, rootValue);
        if (rootNode == null)
        {
            return false;
        }
        return SearchRec(rootNode, searchValue);
    }

    private TreeNode FindNode(TreeNode root, int value)
    {
        if (root == null)
        {
            return null;
        }

        if (value == root.Value)
        {
            return root;
        }

        if (value < root.Value)
        {
            return FindNode(root.Left, value);
        }
        else
        {
            return FindNode(root.Right, value);
        }
    }

    public void PrintTree()
    {
        if (Root == null)
        {
            Console.WriteLine("Дерево пусто");
            return;
        }
        PrintTreeRec(Root, "", true);
    }

    private void PrintTreeRec(TreeNode node, string indent, bool last)
    {
        if (node != null)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("+- ");
                indent += "   ";
            }
            else
            {
                Console.Write("|- ");
                indent += "|  ";
            }
            Console.WriteLine(node.Value);

            PrintTreeRec(node.Left, indent, false);
            PrintTreeRec(node.Right, indent, true);
        }
    }
}

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
