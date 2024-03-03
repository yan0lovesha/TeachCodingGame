# TeachCodingGame
## Lesson 1: Git
1. What is git?
2. Why do we use git?
3. Register on github
4. Create repostiory on git hub

## Lesson 2: Setup environment
1. Download VSCode
2. Clone your repository to local
3. Install git client through vscode
4. Install dotnet SDK
5. Install necesasry extensions: C# Dev Kit
6. Install debuging tool:
    > dotnet workload install wasm-tools

## Lesson 3: Create a project
1. Run command
    > dotnet new blazor -int Auto -n GameCourse -o .\
2. Try run the project
    > dotnet run
3. Create launch settings

## Lesson 4: Analyze the project
1. List the least acceptable rules
   1. A playground with boundaries.
      1. Is a square with a hight and width.
      2. It is composed of hight * width cells.
   2. A snake
      1. Can move to a direction of Up, Left, Down and Right.
      2. Moves 1 step a time.
      3. It cannot turn back.
      4. Grow one node when eats one food.
      5. Dead when hit boundary/itself.
   3. Food
      1. Disappeared when eaten by snake.
      2. Appears at a random cell in the ground.
      3. Cannot apppear on a cell that already has food or snake body.
      4. Add one food to the playground every 10s.

3. Turn the real world requirements into programming concepts
   1. Playground
      1. Width
      2. Hight
   2. Snake
      1. Snake has a size n.
      2. An array of n cells
      4. first node in the array is the snake head. nodes[0] is head.
      5. last node in the array is the snake tail. nodes[n] is tail.
      6. When the snake moves one step
         1. a new node added to the array at the first position. insert node to nodes at 0.
         2. the new node cannot be it's body
         3. the last node of the array is removed. remove nodes[n]
         4. When a food is eaten, do not remove the last node.
         5. Whne the new node is at boundry or itself, DEAD.
   3. Food
      1. An array of cells
      2. A food has the location of the cell in playground.
      3. When a new food is showing up, the new food is added to the array.
      4. When a food is eaten, the food is removed from the array.

## Lesson 5: Implement the Ground

## Lesson 6: Implement the Snake

## Lesson 7: Implement the Timer

## Lesson 8: Implement the snake operation interactions

## Lesson 9: Implement the Food
