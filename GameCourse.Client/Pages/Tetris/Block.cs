namespace GameCourse.Client.Pages.Tetris
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public abstract class Block
    {
        public virtual int[,] ShapeUp { get; }
        public virtual int[,] ShapeRight { get; }
        public virtual int[,] ShapeDown { get; }
        public virtual int[,] ShapeLeft { get; }

        public Direction CurrentDirection { get; set; } = Direction.Up;

        public void TurnRight()
        {
            CurrentDirection = CurrentDirection switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new InvalidOperationException()
            };
        }

        public void TurnLeft()
        {
            CurrentDirection = CurrentDirection switch
            {
                Direction.Up => Direction.Left,
                Direction.Left => Direction.Down,
                Direction.Down => Direction.Right,
                Direction.Right => Direction.Up,
                _ => throw new InvalidOperationException()
            };
        }
    }
}