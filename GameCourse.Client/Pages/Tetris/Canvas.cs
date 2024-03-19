using GameCourse.Client.Pages.Tetris.Blocks;

namespace GameCourse.Client.Pages.Tetris
{
    public class Canvas
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public CanvasPoint[,] Points { get; set; }

        /// <summary>
        /// The block is current falling
        /// </summary>
        public Block ActiveBlock { get; set; }

        /// <summary>
        /// The candidate blocks that will falls next in sequence.
        /// </summary>
        public Queue<Block> BlockCandidatesQueue { get; set; } = new Queue<Block>();

        private Block GetRandomBlock()
        {
            Random ran = new Random();
            var ranNumber = ran.Next(1, 7);
            Block randomBlock = ranNumber switch
            {
                1 => new BlockI(),
                2 => new BlockJ(),
                3 => new BlockL(),
                4 => new BlockO(),
                5 => new BlockS(),
                6 => new BlockT(),
                7 => new BlockZ(),
                _ => throw new InvalidOperationException()
            };
            return randomBlock;
        }

        private void PickABlock()
        {
            ActiveBlock = BlockCandidatesQueue.Dequeue();
            ActiveBlock.PositionRow = Columns / 2;
            BlockCandidatesQueue.Enqueue(GetRandomBlock());
        }

        public Canvas(GameOptions options)
        {
            Rows = options.CanvasRowsCount;
            Columns = options.CanvasColumnsCount;
            Points = new CanvasPoint[Rows, Columns];
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Points[row, col] = new CanvasPoint();
                }
            }

            for (int i = 0; i < 3; i++)
            {
                BlockCandidatesQueue.Enqueue(GetRandomBlock());
            }
            PickABlock();
        }

        private bool CanMoveLeft(int activeBlockHeight, int activeBlockWidth)
        {
            for (int row = 0; row < activeBlockHeight; row++)
            {
                for (int col = 0; col < activeBlockWidth; col++)
                {
                    if (col == 0 || ActiveBlock.Shape[row, col - 1] == 0)
                    {
                        var leftPointInCanvas = Points[ActiveBlock.PositionRow + row, ActiveBlock.PositionColumn + col - 1];
                        if (leftPointInCanvas.IsOccupied)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private bool CanMoveRight(int activeBlockHeight, int activeBlockWidth)
        {
            for (int row = 0; row < activeBlockHeight; row++)
            {
                for (int col = 0; col < activeBlockWidth; col++)
                {
                    if (col == activeBlockWidth - 1 || ActiveBlock.Shape[row, col + 1] == 0)
                    {
                        var leftPointInCanvas = Points[ActiveBlock.PositionRow + row, ActiveBlock.PositionColumn + col + 1];
                        if (leftPointInCanvas.IsOccupied)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private bool CanMoveDown(int activeBlockHeight, int activeBlockWidth)
        {
            for (int row = 0; row < activeBlockHeight; row++)
            {
                for (int col = 0; col < activeBlockWidth; col++)
                {
                    if (row == activeBlockHeight - 1 || ActiveBlock.Shape[row + 1, col] == 0)
                    {
                        var leftPointInCanvas = Points[ActiveBlock.PositionRow + row + 1, ActiveBlock.PositionColumn + col];
                        if (leftPointInCanvas.IsOccupied)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool CanMoveActiveBlockTo(Direction direction)
        {
            return direction switch
            {
                Direction.Left => CanMoveLeft(ActiveBlock.BlockHeight, ActiveBlock.BlockWidth),
                Direction.Right => CanMoveRight(ActiveBlock.BlockHeight, ActiveBlock.BlockWidth),
                Direction.Down => CanMoveDown(ActiveBlock.BlockHeight, ActiveBlock.BlockWidth),
                _ => false
            };
        }

        public void MoveActiveBlockTo(Direction direction)
        {
            if (CanMoveActiveBlockTo(direction))
            {
                switch (direction)
                {
                    case Direction.Left:
                        ActiveBlock.MoveLeft();
                        break;
                    case Direction.Right:
                        ActiveBlock.MoveRight();
                        break;
                    case Direction.Down:
                        ActiveBlock.MoveDown();
                        break;
                    default:
                        break;
                };
            }
        }

        public void TurnActiveBlock()
        {
            ActiveBlock.TurnRight();
        }
    }
}