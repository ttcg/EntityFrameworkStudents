namespace Students.Web.Services.Students.Dtos
{
    public class GetStudentsFilter
    {
        public GetStudentsFilter(int pageNumber, int pageSize, string? searchText = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchText = searchText;
        }
        public int PageNumber { get; }
        public int PageSize { get; }
        public string? SearchText { get; }
    }
}
