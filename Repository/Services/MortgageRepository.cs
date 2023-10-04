using iText.Commons.Actions.Contexts;
using iText.Kernel.Geom;
using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Repository.Services
{
    public class MortgageRepository : IMortgageRepository
    {

        #region add mortgage record
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


        #region get mortgage record
        public Mortgage GetMortgageRecord(string employeeid)
        {
            try
            {
                Mortgage mortgage = new Mortgage();
                return mortgage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region
        public EmployeeBankDetail GetBankDetailByEmployeeId(string employerId)
        {
            try
            {
                EmployeeBankDetail detail = new EmployeeBankDetail();
                return detail;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region
        public Loan GetLoanData(int page, int pagesize)
        {
            //var paginatedData = _loanData.Skip((page - 1) * pageSize).Take(pageSize);
            Loan loan = new Loan(); 
            return loan;
        }

        public bool DeleteLoanRecordById(int loanId)
        {
            Loan loan = new Loan(); 

            if (loan == null)
            {
                return false;
            }

            loan.IsDeleted = true;
            return true;
        }
        #endregion

    }
}
