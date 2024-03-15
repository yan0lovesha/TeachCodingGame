namespace GameCourse.Client.Pages.Tetris.Blocks
{
    public class BlockL : Block
    {
        private static int[,] shapeUp = new[,]
        {
            {1,0},
            {1,0},
            {1,1}
        };
        private static int[,] shapeRight = new[,]
        {
            {1,1,1},
            {1,0,0}
        };
        private static int[,] shapeDown = new[,]
        {
            {1,1},
            {0,1},
            {0,1}
        };
        private static int[,] shapeLeft = new[,]
        {
            {0,0,1},
            {1,1,1}
        };

        public override int[,] ShapeUp => shapeUp;
        public override int[,] ShapeRight => shapeUp;
        public override int[,] ShapeDown => shapeDown;
        public override int[,] ShapeLeft => shapeLeft;
    }
}
