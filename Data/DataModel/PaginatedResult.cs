namespace SULTEC_API.Data.DataModel;

public class PaginatedResult<T> : Result<T>
{
    public Pagination Pagination { get; set; }

    public PaginatedResult(int pageNumber, int pageSize, int totalItems)
    {
        this.Pagination = new()
        {
            PageSize = pageSize,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            HasNextPage = true
        };
    }
}
public class Pagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public bool HasNextPage { get; set; }
}

