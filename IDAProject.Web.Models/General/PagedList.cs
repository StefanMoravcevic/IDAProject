namespace IDAProject.Web.Models.General
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalRowCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
