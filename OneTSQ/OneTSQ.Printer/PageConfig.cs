namespace OneTSQ.Printer
{
    public class PageSize
    {
        /// <summary>
        /// Point unit
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// Point unit
        /// </summary>
        public float Height { get; set; }

        public PageSize(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }

    public class PageConfig
    {
        public bool IsLandscape { get; set; } = false;
        public PageSize PageSize { get; set; }
    }
}