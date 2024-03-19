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

        public override string Color => "Red";

        protected override int[,] ShapeUp => shapeUp;
        protected override int[,] ShapeRight => shapeRight;
        protected override int[,] ShapeDown => shapeDown;
        protected override int[,] ShapeLeft => shapeLeft;
    }
}
