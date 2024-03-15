namespace GameCourse.Client.Pages.Tetris.Blocks
{
    public class BlockZ : Block
    {
        private static int[,] shapeVertical = new[,]
        {
            {0,1},
            {1,1},
            {1,0}
        };
        private static int[,] shapeHorizontal = new[,]
        {
            {1,1,0},
            {0,1,1}
        };

        public override int[,] ShapeUp => shapeVertical;
        public override int[,] ShapeRight => shapeHorizontal;
        public override int[,] ShapeDown => shapeVertical;
        public override int[,] ShapeLeft => shapeHorizontal;
    }
}
