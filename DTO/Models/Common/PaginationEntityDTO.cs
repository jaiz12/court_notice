namespace DTO.Models.Common
{
    public class PaginationEntityDTO
    {
        public string searchTerm { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public string? sortColumn { get; set; }
        public string? sortOrder { get; set; }
        public long company_id { get; set; }
    }
}
