namespace GameCourse.Client.Pages.Tetris.Blocks
{
    public class BlockO : Block
    {
        private static int[,] shape = new[,] 
        {
            {1,1},
            {1,1}
        };

        public override int[,] ShapeUp => shape;
        public override int[,] ShapeRight => shape;
        public override int[,] ShapeDown => shape;
        public override int[,] ShapeLeft => shape;
    }
}
