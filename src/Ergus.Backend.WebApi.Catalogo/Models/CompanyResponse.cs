using Ergus.Backend.Infrastructure.Models;

namespace Ergus.Backend.WebApi.Catalogo.Models
{
    public class CompanyResponse
    {
        public CompanyResponse(Company company)
        {
            if (company == null)
                return;

            this.Id = company.Id;
        }

        public int Id               { get; set; }
    }
}
