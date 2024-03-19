namespace GameCourse.Client.Pages.Tetris.Blocks
{
    public class BlockI : Block
    {
        private static int[,] shapeVertical = new[,] 
        {
            {1},
            {1},
            {1},
            {1}
        };
        private static int[,] shapeHonrizental = new[,] 
        {
            {1,1,1,1}
        };

        public override string Color => "Green";

        protected override int[,] ShapeUp => shapeVertical;
        protected override int[,] ShapeRight => shapeHonrizental;
        protected override int[,] ShapeDown => shapeVertical;
        protected override int[,] ShapeLeft => shapeHonrizental;
    }
}
