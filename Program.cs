using System;
using System.Collections.Generic;
using System.Linq;

class Recursive {
    static public void Move(int n, string start, string middle, string final, Stack<int> startStack, Stack<int> middleStack, Stack<int> finalStack, int totalDisks) {
        if (n == 1) {
            // Console.WriteLine("Move disk 1 from " + start + " to " + final);
            MoveDisk(startStack, finalStack, start, final, startStack, middleStack, finalStack, totalDisks);
        } else {
            Move(n - 1, start, final, middle, startStack, finalStack, middleStack, totalDisks);
            // Console.WriteLine("Move disk " + n + " from " + start + " to " + final);
            MoveDisk(startStack, finalStack, start, final, startStack, middleStack, finalStack, totalDisks);

            Move(n - 1, middle, start, final, middleStack, startStack, finalStack, totalDisks);
        }
    }

    static void MoveDisk(Stack<int> from, Stack<int> to, string fromName, string toName, Stack<int> start, Stack<int> middle, Stack<int> final, int totalDisks) {
        if (from.Count == 0) {
            // pop means to remove the top element of the stack
            int disk = to.Pop();
            from.Push(disk);
            // Console.WriteLine("Move disk " + disk + " from " + toName + " to " + fromName);
        } else if (to.Count == 0) {
            int disk = from.Pop();
            to.Push(disk);
            // Console.WriteLine("Move disk " + disk + " from " + fromName + " to " + toName);
        } else {
            int fromTop = from.Peek();
            int toTop = to.Peek();
            if (fromTop < toTop) {
                from.Pop();
                to.Push(fromTop);
                // Console.WriteLine("Move disk " + fromTop + " from " + fromName + " to " + toName);
            } else {
                to.Pop();
                from.Push(toTop);
                // Console.WriteLine("Move disk " + toTop + " from " + toName + " to " + fromName);
            }
        }
        Animation.DrawRods(start, middle, final, totalDisks);
    }
}

class Program {
    static void Main(string[] args) {
        if (args.Length != 2) {
            Console.WriteLine("Usage: dotnet run -<method> <number_of_disks>");
            Console.WriteLine("method: '-recursive' or '-iterative'");
            return;
        }

        string method = args[0].ToLower();
        if (!method.StartsWith("-") || (!method.Equals("-recursive") && !method.Equals("-iterative"))) {
            Console.WriteLine("Invalid method. Please enter '-recursive' or '-iterative'.");
            return;
        }

        if (!int.TryParse(args[1], out int disks) || disks <= 0) {
            Console.WriteLine("Invalid number of disks. Please enter a positive integer.");
            return;
        }

        Stack<int> start = new();
        Stack<int> middle = new();
        Stack<int> final = new();

        for (int i = disks; i >= 1; i--) {
            start.Push(i);
        }

        switch (method) {
            case "-recursive":
                Console.WriteLine("Recursive solution:");
                Animation.DrawRods(start, middle, final, disks);
                Recursive.Move(disks, "A", "B", "C", start, middle, final, disks);
                break;
            case "-iterative":
                Console.WriteLine("Iterative solution:");
                Iteration.Move(disks, "A", "B", "C");
                break;
        }
    }
}

class Iteration {
    static void MoveDisk(Stack<int> from, Stack<int> to, string fromName, string toName, Stack<int> start, Stack<int> middle, Stack<int> final, int totalDisks) {
        if (from.Count == 0) {
            int disk = to.Pop();
            from.Push(disk);
            // Console.WriteLine("Move disk " + disk + " from " + toName + " to " + fromName);
        } else if (to.Count == 0) {
            int disk = from.Pop();
            to.Push(disk);
            // Console.WriteLine("Move disk " + disk + " from " + fromName + " to " + toName);
        } else {
            // peek means to see the top element of the stack without removing it
            int fromTop = from.Peek();
            int toTop = to.Peek();
            if (fromTop < toTop) {
                from.Pop();
                to.Push(fromTop);
               // Console.WriteLine("Move disk " + fromTop + " from " + fromName + " to " + toName);
            } else {
                to.Pop();
                from.Push(toTop);
                // Console.WriteLine("Move disk " + toTop + " from " + toName + " to " + fromName);
            }
        }
        Animation.DrawRods(start, middle, final, totalDisks);
    }

    // Move the amount 'n' of disks from start to final using middle rod
    public static void Move(int totalDisks, string startName, string middleName, string finalName) {
        Stack<int> start = new();
        Stack<int> middle = new();
        Stack<int> final = new();

        for (int i = totalDisks; i >= 1; i--) {
            start.Push(i);
        }

        if (totalDisks % 2 == 0) {
            (middleName, finalName) = (finalName, middleName);
            (middle, final) = (final, middle);
        }

        int totalMoves = (int)Math.Pow(2, totalDisks) - 1;

        Animation.DrawRods(start, middle, final, totalDisks);

        for (int move = 1; move <= totalMoves; move++) {
            switch (move % 3) {
                case 1:
                    MoveDisk(start, final, startName, finalName, start, middle, final, totalDisks);
                    break;
                case 2:
                    MoveDisk(start, middle, startName, middleName, start, middle, final, totalDisks);
                    break;
                case 0:
                    MoveDisk(middle, final, middleName, finalName, start, middle, final, totalDisks);
                    break;
            }
        }
    }
}

// Terminal Animation to visualize the move of the disks
class Animation {
    public static void DrawRods(Stack<int> start, Stack<int> middle, Stack<int> final, int totalDisks) {
        // clear the console before drawing the new state
        Console.Clear();
        
        List<string[]> rods = new List<string[]> { new string[totalDisks], new string[totalDisks], new string[totalDisks] };

        FillRod(rods[0], start, totalDisks);
        FillRod(rods[1], middle, totalDisks);
        FillRod(rods[2], final, totalDisks);

        for (int i = totalDisks - 1; i >= 0; i--) {
            Console.WriteLine($"|{rods[0][i].PadRight(totalDisks)}| |{rods[1][i].PadRight(totalDisks)}| |{rods[2][i].PadRight(totalDisks)}|");
        }
        Console.WriteLine("  A     B     C  ");
        // time to slow animation to visualize the move
        System.Threading.Thread.Sleep(500);
    }

    static void FillRod(string[] rod, Stack<int> stack, int totalDisks) {
        // reverse the stack to draw the disks from the bottom to the top
        var disks = stack.Reverse().ToArray();
        for (int i = 0; i < totalDisks; i++) {
            if (i < disks.Length) {
                rod[i] = new string('-', disks[i]);
            } else {
                rod[i] = " ";
            }
        }
    }
}