using SULTEC_API.Data;

namespace SULTEC_API.Services;

public class ServiceBase
{
    public Result Result { get; set; }
}

public class Result
{
    public bool Success { get; set; }

    public object? Data { get; set; }

    public List<string> Errors { get; set; } = [];
}

public class PaginatedResult : Result
{
    public Pagination Pagination { get; set; }
}