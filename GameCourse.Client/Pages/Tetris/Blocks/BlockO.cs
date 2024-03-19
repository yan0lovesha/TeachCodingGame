namespace GameCourse.Client.Pages.Tetris.Blocks
{
    public class BlockO : Block
    {
        private static int[,] shape = new[,] 
        {
            {1,1},
            {1,1}
        };

        public override string Color => "Black";

        protected override int[,] ShapeUp => shape;
        protected override int[,] ShapeRight => shape;
        protected override int[,] ShapeDown => shape;
        protected override int[,] ShapeLeft => shape;
    }
}
