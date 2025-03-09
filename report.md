
# Assignment1 Report

Course: C# Development SS2025 (4 ECTS, 3 SWS)

Student ID: cc231030

BCC Group: A

Name: Erath Niklas

## Methodology: 
I have a `Program` class which contains the `Main` function and the base functionality of the program. Inside the `Main` function, I created the if statements for the terminal/user inputs to start either the recursive or the iterative method with error messages. Besides that, I created 3 stacks - one for each rod - and added all the disks to the start rod (left = start, middle = middle, right = final).


### Recursive Method: 
The `Main` function calls the `Recursive.Move` method if the user types `dotnet run -recursive 3` (or another number of disks instead of 3). The logic behind the recursive method is that it calls its own function repeatedly until `n == 1`, at which point it calls the `MoveDisk` function to move the disk to a rod. With each move, the `DrawRods` method is called to visualize the current position of the disks in the terminal. Before each move, the terminal gets cleared to provide a better visualization.


### Iterative Method: 
The `Main` function calls the `Iteration.Move` method if the user types `dotnet run -iterative 3` (or another number of disks instead of 3). The iterative method uses a loop to move the disks between the rods according to the rules of the Tower of Hanoi. The `MoveDisk` function is called to handle the actual disk movement, and the `DrawRods` method is called after each move to visualize the current state of the rods and disks in the terminal. The terminal is cleared before each move to ensure a clear visualization.


## Additional Features
I implemented an `Animation` class with draw methods to visualize the Tower of Hanoi.

## Discussion/Conclusion
The first challenge was to get to know the Tower of Hanoi because I had never heard of it before. But after watching some YouTube videos, I got the idea of how it works. I started with the recursion because it was easier for me and began to draw the logic mathematically on paper. The iteration, on the other hand, was more challenging for me to complete. At the beginning, I felt lost and didn't know where to start. At this point, I have to say - thank Stack Overflow and Copilot! Those two helped me a lot to get an understanding of how iterations work and the logic behind them.


I started with simple text lines for the moves of the disks which I commented out in the actual code to see what happens.


## Work with: 
I worked alone during the nights. 

## Reference: 
I used explanation videos on YouTube to get the idea of how the Tower of Hanoi works.