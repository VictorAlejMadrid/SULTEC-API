using System.Net;

namespace SULTEC_API.Data.DataModel;

public class Result<T>
{
    public bool Success { get; set; } = true;
    public T? Data { get; set; }
    public int Code { get; set; } = HttpStatusCode.OK.GetHashCode();
    public List<string> Errors { get; set; } = [];
}
