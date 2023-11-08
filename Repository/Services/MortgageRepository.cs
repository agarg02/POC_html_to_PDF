using Amazon.DynamoDBv2.DocumentModel;
using iText.Commons.Actions.Contexts;
using iText.Kernel.Geom;
using iText.Layout.Element;
using JSON_To_PDF.Model;
using JSON_To_PDF.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static JSON_To_PDF.Response.Result;

namespace JSON_To_PDF.Repository.Services
{
    public class MortgageRepository : IMortgageRepository
    {
        ////aws dynamo
        //private readonly AmazonDynamoDBClient _client;
        //private readonly string _tableName;

        //public MortgageRepository(AmazonDynamoDBClient client, string tableName)
        //{
        //    _client = client;
        //    _tableName = tableName;
        //}
        ////aws dynamo

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
        public List<Loan> GetLoanData(PaginationModel paginationModel)
        {
            List<Loan> loanlist = new List<Loan>();
            for(int i = 0; i < 3; i++)
            {
                Loan loan = new Loan();
                loan.LoanType = "Mortgage loan" + i;
                loan.LoanAmount = 100000;
                loan.CollateralValue = 500;
                loan.LTV = "Mortgage LTV" + i;
                loan.Terms = "Mortage terms" + i;
                loan.ResidualIncome = 5000;
                loan.Riki = 8;
                loan.CRA = true;
                loan.DPA = false;
                loan.Location = "Indore" + i;
                loan.IsDeleted = false;
                loanlist.Add(loan);
            }


            ////var table = Table.LoadTable(_client, _tableName);
            //var table = loanlist;

            //var scanFilter = new ScanFilter();

            //foreach (var filter in paginationModel.LoanFilterType.FilterType)
            //{
            //    var attributeName = filter.Key;
            //    var attributeValues = filter.Value;

            //    scanFilter.AddCondition(attributeName, ScanOperator.In, attributeValues);
            //}

            //var search = table.Scan(scanFilter);

            //var documentList = await search.GetNextSetAsync();

            //return documentList;


            return loanlist;
        }

        public bool DeleteLoanRecordById(int loanId, int userid)
        {
            Loan loan = new Loan(); 

            if (loan == null || loan == null)
            {
                return false;
            }

            loan.IsDeleted = true;
            return true;
        }
        #endregion

    }
}
