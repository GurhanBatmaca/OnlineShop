namespace Shared.Models;

public class PageInfoModel
{
    public int TotalItems { get; set; }
    public int ItemPerPage { get; set; }
    public int CurrentPage { get; set; }
    public string? CurrentCategory { get; set; }
    public string? CurrentOrderState { get; set; }
    public string? SearchQuery { get; set; }
    public string? PaginationType { get; set; }

    public int TotalPages()
    {
        return (int)Math.Ceiling((decimal)TotalItems/ItemPerPage);
    }
}
