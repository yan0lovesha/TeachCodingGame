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

        public override string Color => "Orange";

        protected override int[,] ShapeUp => shapeVertical;
        protected override int[,] ShapeRight => shapeHorizontal;
        protected override int[,] ShapeDown => shapeVertical;
        protected override int[,] ShapeLeft => shapeHorizontal;
    }
}
