using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Repository.Services
{
    public class MortgageRepository : IMortgageRepository
    {

        #region main calling unit
        public async Task<Mortgage> AddMortgageRecord(Mortgage mortgage)
        {
            try
            {
                if(mortgage == null) {
                    throw new ArgumentNullException(nameof(mortgage), "Mortgage cannot be null.");
                }
                return mortgage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

        }
        #endregion

    }
}
