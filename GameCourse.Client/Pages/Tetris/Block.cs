namespace GameCourse.Client.Pages.Tetris
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public abstract class Block
    {
        protected virtual int[,] ShapeUp { get; }
        protected virtual int[,] ShapeRight { get; }
        protected virtual int[,] ShapeDown { get; }
        protected virtual int[,] ShapeLeft { get; }

        public virtual string Color { get; }

        public int PositionRow { get; set; }
        public int PositionColumn { get; set; }

        public int BlockHeight => Shape.GetUpperBound(0) + 1;
        public int BlockWidth => Shape.GetUpperBound(1) + 1;

        public Direction CurrentDirection { get; set; } = Direction.Up;

        public int[,] Shape => CurrentDirection switch
        {
            Direction.Up => ShapeUp,
            Direction.Right => ShapeRight,
            Direction.Down => ShapeDown,
            Direction.Left => ShapeLeft,
            _ => throw new NotImplementedException()
        };

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

        public void MoveLeft()
        {
            PositionColumn -= 1;
        }

        public void MoveRight()
        {
            PositionColumn += 1;
        }

        public void MoveDown()
        {
            PositionRow += 1;
        }
    }
}