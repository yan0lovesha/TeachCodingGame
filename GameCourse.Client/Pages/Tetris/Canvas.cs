using GameCourse.Client.Pages.Tetris.Blocks;
using GameCourse.Common;

namespace GameCourse.Client.Pages.Tetris
{
    public class Canvas
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public CanvasPoint[,] Points { get; set; }

        public bool IsCanvasFull { get; set; }

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
            ActiveBlock.PositionRow = 0 - ActiveBlock.BlockHeight;
            ActiveBlock.PositionColumn = Columns / 2;
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

        private bool CanMoveLeft()
        {
            if (ActiveBlock.PositionColumn > 0 && !AreShapeAndCanvasOverlap(ActiveBlock.PositionRow, ActiveBlock.PositionColumn - 1, ActiveBlock.Shape))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanMoveRight()
        {
            if (ActiveBlock.PositionColumn + ActiveBlock.BlockWidth < Columns && !AreShapeAndCanvasOverlap(ActiveBlock.PositionRow, ActiveBlock.PositionColumn + 1, ActiveBlock.Shape))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanMoveDown()
        {
            if (ActiveBlock.PositionRow + ActiveBlock.BlockHeight < Rows && !AreShapeAndCanvasOverlap(ActiveBlock.PositionRow + 1, ActiveBlock.PositionColumn, ActiveBlock.Shape))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanMoveActiveBlockTo(Direction direction)
        {
            return direction switch
            {
                Direction.Left => CanMoveLeft(),
                Direction.Right => CanMoveRight(),
                Direction.Down => CanMoveDown(),
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
            else
            {
                if (direction == Direction.Down)
                {
                    LockActiveBlock();
                    EliminateFullRows();
                }
            }
        }

        private void LockActiveBlock()
        {
            for (int row = 0; row < ActiveBlock.BlockHeight; row++)
            {
                for (int col = 0; col < ActiveBlock.BlockWidth; col++)
                {
                    if (ActiveBlock.Shape[row, col] == 1)
                    {
                        var rowIndexOfCanvas = ActiveBlock.PositionRow + row;
                        var colIndexOfCanvas = ActiveBlock.PositionColumn + col;

                        // If the top outside of canvas is locked, that means the canvas is full.
                        // The game is loose.
                        if (rowIndexOfCanvas < 0)
                        {
                            IsCanvasFull = true;
                        }
                        else
                        {
                            var point = Points[rowIndexOfCanvas, colIndexOfCanvas];
                            point.IsOccupied = true;
                            var blockColor = ColorUtility.FromColorNameToColor(ActiveBlock.Color);
                            point.Color = blockColor.ChangeGreyScale(0.2).FromColorToHex();
                        }
                    }
                }
            }

            PickABlock();
        }

        private bool AreShapeAndCanvasOverlap(int blockPositionRow, int blockPositionCol, int[,] shape)
        {
            var shapeRowsCount = shape.GetLength(0);
            var shapeColumnsCount = shape.GetLength(1);
            for (int row = 0; row < shapeRowsCount; row++)
            {
                for (int col = 0; col < shapeColumnsCount; col++)
                {
                    var rowIndexOfCanvas = blockPositionRow + row;
                    var colIndexOfCanvas = blockPositionCol + col;
                    if (rowIndexOfCanvas >= 0 && rowIndexOfCanvas < Rows && colIndexOfCanvas >= 0 && colIndexOfCanvas < Columns &&
                        shape[row, col] == 1 && Points[rowIndexOfCanvas, colIndexOfCanvas].IsOccupied)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private List<int> FindFullRows()
        {
            var fullRows = new List<int>();

            // Find from bottom to top.
            for (int row = Rows - 1; row >= 0; row--)
            {
                var rowIsFull = true;
                for (int col = 0; col < Columns; col++)
                {
                    if (!Points[row, col].IsOccupied)
                    {
                        rowIsFull = false;
                    }
                }

                if (rowIsFull)
                {
                    fullRows.Add(row);
                }
            }
            return fullRows;
        }

        private void EliminateFullRows()
        {
            var fullRows = FindFullRows();
            if (fullRows.Count > 0)
            {
                var firstEliminatingRow = fullRows[0];
                var fillingRow = firstEliminatingRow;
                for (int row = fillingRow; row >= 0; row--)
                {
                    if (fullRows.Count > 0)
                    {
                        fullRows.RemoveAt(0);
                    }
                    else
                    {
                        // move the row to filling row
                        for (var col = 0; col < Columns; col++)
                        {
                            Points[fillingRow, col] = Points[row, col];
                        }
                        fillingRow--;
                    }
                }
            }
        }

        public void TurnActiveBlock()
        {
            var shapeAfterTurn = ActiveBlock.GetShapeAfterTurn(Direction.Right);
            var shapeHeight = shapeAfterTurn.GetLength(0);
            var shapeWidth = shapeAfterTurn.GetLength(1);

            var outOfCanvasRight = Math.Max(ActiveBlock.PositionColumn + shapeWidth - Columns, 0);
            var outOfCanvasBottom = Math.Max(ActiveBlock.PositionRow + shapeHeight - Rows, 0);

            if (outOfCanvasRight >= 0 || outOfCanvasBottom >= 0)
            {
                // check if adjusted position will have overlap with canvas.
                if (!AreShapeAndCanvasOverlap(ActiveBlock.PositionRow - outOfCanvasBottom, ActiveBlock.PositionColumn - outOfCanvasRight, shapeAfterTurn))
                {
                    ActiveBlock.PositionRow -= outOfCanvasBottom;
                    ActiveBlock.PositionColumn -= outOfCanvasRight;
                    ActiveBlock.TurnRight();
                }
            }
        }
    }
}