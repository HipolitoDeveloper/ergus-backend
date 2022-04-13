namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class PaginationRequest
    {
        public PaginationRequest() { }

        public PaginationRequest(int? page, int? pageSize, bool disablePagination = false)
        {
            this.Page = page ?? 1;
            this.PageSize = pageSize ?? 10;
            this.DisablePagination = disablePagination;
        }

        public int Page                 { get; set; }
        public int PageSize             { get; set; }
        public bool DisablePagination   { get; set; }
    }
}
