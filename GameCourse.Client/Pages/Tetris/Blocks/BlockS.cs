namespace GameCourse.Client.Pages.Tetris.Blocks
{
    public class BlockS : Block
    {
        private static int[,] shapeVertical = new[,]
        {
            {1,0},
            {1,1},
            {0,1}
        };
        private static int[,] shapeHorizontal = new[,]
        {
            {0,1,1},
            {1,1,0}
        };

        public override int[,] ShapeUp => shapeVertical;
        public override int[,] ShapeRight => shapeHorizontal;
        public override int[,] ShapeDown => shapeVertical;
        public override int[,] ShapeLeft => shapeHorizontal;
    }
}
