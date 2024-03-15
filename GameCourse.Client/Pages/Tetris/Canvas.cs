using GameCourse.Client.Pages.Tetris.Blocks;
using System.Dynamic;

namespace GameCourse.Client.Pages.Tetris
{
    public class Canvas
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public int[,] Points { get; set; }

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
            BlockCandidatesQueue.Enqueue(GetRandomBlock());
        }

        public Canvas()
        {
            Points = new int[Rows, Columns];
            for (int i = 0; i < 3; i++)
            {
                BlockCandidatesQueue.Enqueue(GetRandomBlock());
            }
            PickABlock();
        }
    }
}