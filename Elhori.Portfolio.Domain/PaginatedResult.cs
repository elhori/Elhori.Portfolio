namespace Elhori.Portfolio.Domain;

public class PaginatedResult<TItem> where TItem : class
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
    public List<TItem> Results { get; set; } = [];
}