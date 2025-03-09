using System;
using System.Collections.Generic;
using System.Linq;

// recursive solution to move the disks from start to final rod
class Recursive
{
    static public void Move(int n, string start, string middle, string final, Stack<int> startStack, Stack<int> middleStack, Stack<int> finalStack, int totalDisks)
    {
        if (n == 1)
        {
            // Console.WriteLine("Move disk 1 from " + start + " to " + final);
            Disk.MoveDisk(startStack, finalStack, start, final, startStack, middleStack, finalStack, totalDisks);
        }
        else
        {
            Move(n - 1, start, final, middle, startStack, finalStack, middleStack, totalDisks);
            // Console.WriteLine("Move disk " + n + " from " + start + " to " + final);
            Disk.MoveDisk(startStack, finalStack, start, final, startStack, middleStack, finalStack, totalDisks);

            Move(n - 1, middle, start, final, middleStack, startStack, finalStack, totalDisks);
        }
    }
}

// main program to run the tower of hanoi by typing the method and the number of disks
class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: dotnet run -<method> <number_of_disks>");
            Console.WriteLine("method: '-recursive' or '-iterative'");
            return;
        }

        string method = args[0].ToLower();
        if (!method.StartsWith("-") || (!method.Equals("-recursive") && !method.Equals("-iterative")))
        {
            Console.WriteLine("Invalid method. Please enter '-recursive' or '-iterative'.");
            return;
        }

        // TryParse tries to convert the string of the number of disks to an integer
        if (!int.TryParse(args[1], out int disks) || disks <= 1)
        {
            Console.WriteLine("Invalid number of disks. Please enter a positive integer.");
            return;
        }

        Stack<int> start = new();
        Stack<int> middle = new();
        Stack<int> final = new();

        switch (method)
        {
            case "-recursive":
                for (int i = disks; i >= 1; i--)
                {
                    start.Push(i);
                }
                Console.WriteLine("Recursive solution:");
                Animation.DrawRods(start, middle, final, disks);
                Recursive.Move(disks, "L", "M", "R", start, middle, final, disks);
                break;
            case "-iterative":
                Console.WriteLine("Iterative solution:");
                Iteration.Move(disks, "L", "M", "R", start, middle, final);
                break;
        }
    }
}

// iterative solution to move the disks from start to final rod
class Iteration
{
    // move the amount 'n' to the destination rod which depends on the total number of disks
    public static void Move(int totalDisks, string startName, string middleName, string finalName, Stack<int> startStack, Stack<int> middleStack, Stack<int> finalStack)
    {

        for (int i = totalDisks; i >= 1; i--)
        {
            startStack.Push(i);
        }

        Animation.DrawRods(startStack, middleStack, finalStack, totalDisks);

        if (totalDisks % 2 == 0)
        {
            (middleName, finalName) = (finalName, middleName);
            (middleStack, finalStack) = (finalStack, middleStack);
        }

        int totalMoves = (int)Math.Pow(2, totalDisks) - 1;

        for (int move = 1; move <= totalMoves; move++)
        {
            switch (move % 3)
            {
                case 1:
                    Disk.MoveDisk(startStack, finalStack, startName, finalName, startStack, middleStack, finalStack, totalDisks);
                    break;
                case 2:
                    Disk.MoveDisk(startStack, middleStack, startName, middleName, startStack, middleStack, finalStack, totalDisks);
                    break;
                case 0:
                    Disk.MoveDisk(middleStack, finalStack, middleName, finalName, startStack, middleStack, finalStack, totalDisks);
                    break;
            }
        }
    }
}


// class to move the disks
class Disk
{
    public static void MoveDisk(Stack<int> from, Stack<int> to, string fromName, string toName, Stack<int> start, Stack<int> middle, Stack<int> final, int totalDisks)
    {
        if (from.Count == 0)
        {
            int disk = to.Pop();
            from.Push(disk);
            // Console.WriteLine("Move disk " + disk + " from " + toName + " to " + fromName);
        }
        else if (to.Count == 0)
        {
            int disk = from.Pop();
            to.Push(disk);
            // Console.WriteLine("Move disk " + disk + " from " + fromName + " to " + toName);
        }
        else
        {
            int fromTop = from.Peek();
            int toTop = to.Peek();
            if (fromTop < toTop)
            {
                from.Pop();
                to.Push(fromTop);
                // Console.WriteLine("Move disk " + fromTop + " from " + fromName + " to " + toName);
            }
            else
            {
                to.Pop();
                from.Push(toTop);
                // Console.WriteLine("Move disk " + toTop + " from " + toName + " to " + fromName);
            }
        }
        Animation.DrawRods(start, middle, final, totalDisks);
    }
}


// animation to visualize the move of the disks
class Animation
{
    public static void DrawRods(Stack<int> start, Stack<int> middle, Stack<int> final, int totalDisks)
    {
        Console.Clear();

        List<string[]> rods = new List<string[]> { new string[totalDisks], new string[totalDisks], new string[totalDisks] };

        DrawDisks(rods[0], start, totalDisks);
        DrawDisks(rods[1], middle, totalDisks);
        DrawDisks(rods[2], final, totalDisks);

        for (int i = totalDisks - 1; i >= 0; i--)
        {
            Console.WriteLine($"|{rods[0][i].PadRight(totalDisks)}| |{rods[1][i].PadRight(totalDisks)}| |{rods[2][i].PadRight(totalDisks)}|");
        }
        Console.WriteLine("  L     M     R  ");
        // time to slow animation to visualize the move - change if you want to speed up
        System.Threading.Thread.Sleep(500);
    }

    // draw the Disks inside the rods - reverse order to get the smallest disk at the top
    static void DrawDisks(string[] rod, Stack<int> stack, int totalDisks)
    {
        var disks = stack.Reverse().ToArray();
        for (int i = 0; i < totalDisks; i++)
        {
            if (i < disks.Length)
            {
                rod[i] = new string('-', disks[i]);
            }
            else
            {
                rod[i] = " ";
            }
        }
    }
}