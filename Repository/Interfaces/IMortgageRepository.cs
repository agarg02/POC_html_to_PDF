using JSON_To_PDF.Model;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Repository.Interfaces
{
    public interface IMortgageRepository
    {
        public Task<Mortgage> AddMortgageRecord(Mortgage mortgage);

        public Mortgage GetMortgageRecord(string employeeid);

        public EmployeeBankDetail GetBankDetailByEmployeeId(string employerId);

    }
}
