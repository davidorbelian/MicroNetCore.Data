namespace MicroNetCore.Data.EfCore.Extensions
{
    public static class IntExtensions
    {
        public static int UpdatePageIndex(this int pageIndex, int pageCount)
        {
            return pageIndex < 0 ? 0 : pageIndex > pageCount ? pageCount : pageIndex;
        }
    }
}