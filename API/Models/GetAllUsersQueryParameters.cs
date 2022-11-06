namespace API.Models
{
    public class GetAllUsersQueryParameters
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public string? SortOrder { get; set; } 
        public string? SearchString { get; set; } 
    }
}
