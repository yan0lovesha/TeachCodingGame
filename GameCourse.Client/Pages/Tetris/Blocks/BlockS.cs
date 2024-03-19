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

        public override string Color => "Yellow";

        protected override int[,] ShapeUp => shapeVertical;
        protected override int[,] ShapeRight => shapeHorizontal;
        protected override int[,] ShapeDown => shapeVertical;
        protected override int[,] ShapeLeft => shapeHorizontal;
    }
}
