using System.Collections.Generic;

namespace MultichoiceCollection.Presentation.Models
{
    public class UserModel
    {
        public string accessToken { get; set; }
        public string expirationDate { get; set; }
        public string clientAppName { get; set; }
    }
    public class BouquetTypeModel
    {
        public string name { get; set; }
        public string bouquetUrl { get; set; }
    }
    public class BouquetModel
    {
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string currency { get; set; }
        public string subscription { get; set; }
        public string nameAndPrice { get; set; }
        public string transFee { get; set; }
        public string paymentUrl { get; set; }
    
    }
    public class AccountModel
    {
        public bool isPrimary { get; set; }
        public string accountType { get; set; }
        public string currency { get; set; }
        public decimal amount { get; set; }
        public string methodOfPayment { get; set; }
        public long customerNumber { get; set; }
        public string status { get; set; }
        public string paymentDueDate { get; set; }
        public string lastInvoiceDate { get; set; }
        public int invoicePeriod { get; set; }
        public bool hasBoxOffice { get; set; }
        public bool isCommercial { get; set; }
    
    }
    public class CustomerModel
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string initial { get; set; }
        public string accountStatus { get; set; }
        public long customerNumber { get; set; }
        public long accountNo { get; set; }
        public string title { get; set; }
        public string mobileNo { get; set; }
        public string emailAddress { get; set; }
        public bool hasBoxOffice { get; set; }
        public int invoicePeriod { get; set; }
        public decimal totalBalance { get; set; }
    
    }
    public class CustomerAccountModel
    {
        public string auditReferenceNo { get; set; }
        public CustomerModel customer { get; set; }
        public List<AccountModel> accounts { get; set; }
        
    
    }
    public class ProductModel
    {
        public string currency { get; set; }
        public decimal subscription { get; set; }
        public string productKey { get; set; }
        public string productName { get; set; }
        public string nameAndPrice { get; set; }
        public bool activeOnAccount { get; set; }
        public decimal transFee { get; set; }
        public string paymentUrl { get; set; }
    }
    public class CustomerDetailsModel
    {
        public CustomerModel customer { get; set; }
        public List<ProductModel> Products { get; set; }


    }
    public class MakePaymentCustomerNumberResponseModel
    {
        public string auditReferenceNo { get; set; }
        public string transactionNumber { get; set; }
        public string receiptNumber { get; set; }
        public string date { get; set; }
        public string message { get; set; }
        public bool success { get; set; }


    }
    public class MakePaymentSmartCardNumberResponseModel
    {
        public string auditReferenceNo { get; set; }
        public string transactionNumber { get; set; }
        public string receiptNumber { get; set; }
        public string date { get; set; }
        public string message { get; set; }
        public bool success { get; set; }


    }
    public class TransactionsResponseModel
    {
        public int id { get; set; }
        public string accountNumber { get; set; }
        public string transactionDate { get; set; }
        public string apiClientId { get; set; }
        public string customerNumber { get; set; }
        public string deviceNumber { get; set; }
        public string packageCode { get; set; }
        public string businessType { get; set; }
        public string emailAddress { get; set; }
        public string mobileNumber { get; set; }
        public decimal amount { get; set; }
        public decimal transFees { get; set; }
        public bool posted { get; set; }
        public string auditReferenceNo { get; set; }
        public bool success { get; set; }
        public string url { get; set; }
    }
    public class ConfirmationResponseModel
    {
        public int transactionNumber { get; set; }
        public int receiptNumber { get; set; }
        public string date { get; set; }
        public string message { get; set; }
        public bool sucess { get; set; }
        public string auditReferenceNo { get; set; }
    }

}