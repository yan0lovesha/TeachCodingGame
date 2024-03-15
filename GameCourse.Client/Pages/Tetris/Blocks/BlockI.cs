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

        public override int[,] ShapeUp => shapeVertical;
        public override int[,] ShapeRight => shapeHonrizental;
        public override int[,] ShapeDown => shapeVertical;
        public override int[,] ShapeLeft => shapeHonrizental;
    }
}
