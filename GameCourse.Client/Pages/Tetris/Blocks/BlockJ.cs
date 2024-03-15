namespace GameCourse.Client.Pages.Tetris.Blocks
{
    public class BlockJ : Block
    {
        private static int[,] shapeUp = new[,]
        {
            {0,1},
            {0,1},
            {1,1}
        };
        private static int[,] shapeRight = new[,]
        {
            {1,0,0},
            {1,1,1}
        };
        private static int[,] shapeDown = new[,]
        {
            {1,1},
            {1,0},
            {1,0}
        };
        private static int[,] shapeLeft = new[,]
        {
            {1,1,1},
            {0,0,1}
        };

        public override int[,] ShapeUp => shapeUp;
        public override int[,] ShapeRight => shapeRight;
        public override int[,] ShapeDown => shapeDown;
        public override int[,] ShapeLeft => shapeLeft;
    }
}
