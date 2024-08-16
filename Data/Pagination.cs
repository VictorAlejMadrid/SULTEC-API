namespace SULTEC_API.Data;

public class Pagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public bool HasNextPage { get; set; }
}
