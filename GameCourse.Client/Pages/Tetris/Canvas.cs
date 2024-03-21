﻿using GameCourse.Client.Pages.Tetris.Blocks;

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
            ActiveBlock.PositionRow = 0;
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
            for (int row = 0; row < ActiveBlock.BlockHeight; row++)
            {
                for (int col = 0; col < ActiveBlock.BlockWidth; col++)
                {
                    if (col == 0 || ActiveBlock.Shape[row, col - 1] == 0)
                    {
                        var leftColumnIndexOfCanvas = ActiveBlock.PositionColumn + col - 1;
                        if (ActiveBlock.Shape[row, col] == 1 && (leftColumnIndexOfCanvas < 0 || Points[ActiveBlock.PositionRow + row, leftColumnIndexOfCanvas].IsOccupied))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private bool CanMoveRight()
        {
            for (int row = 0; row < ActiveBlock.BlockHeight; row++)
            {
                for (int col = 0; col < ActiveBlock.BlockWidth; col++)
                {
                    if (col == ActiveBlock.BlockWidth - 1 || ActiveBlock.Shape[row, col + 1] == 0)
                    {
                        var rightColumnIndexOfCanvas = ActiveBlock.PositionColumn + col + 1;
                        if (ActiveBlock.Shape[row, col] == 1 && (rightColumnIndexOfCanvas > Points.GetUpperBound(1) || Points[ActiveBlock.PositionRow + row, rightColumnIndexOfCanvas].IsOccupied))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private bool CanMoveDown()
        {
            for (int row = 0; row < ActiveBlock.BlockHeight; row++)
            {
                for (int col = 0; col < ActiveBlock.BlockWidth; col++)
                {
                    if (ActiveBlock.Shape[row, col] == 1 && (row == ActiveBlock.BlockHeight - 1 || ActiveBlock.Shape[row + 1, col] == 0))
                    {
                        var nextRowIndexOfCanvas = ActiveBlock.PositionRow + row + 1;
                        if (nextRowIndexOfCanvas > Points.GetUpperBound(0) || Points[nextRowIndexOfCanvas, ActiveBlock.PositionColumn + col].IsOccupied)
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
                        var point = Points[ActiveBlock.PositionRow + row, ActiveBlock.PositionColumn + col];
                        point.IsOccupied = true;
                        point.Color = ActiveBlock.Color;
                    }
                }
            }

            PickABlock();
        }

        public void TurnActiveBlock()
        {
            ActiveBlock.TurnRight();
        }
    }
}