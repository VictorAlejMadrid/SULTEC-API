namespace SULTEC_API.Data.DataModel;

public class Result
{ 
    public bool Success { get; set; } = true;

    public object? Data { get; set; } = null;

    public List<string> Errors { get; set; } = [];
}
