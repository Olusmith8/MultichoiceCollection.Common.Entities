using System;
using System.ComponentModel.DataAnnotations;
using MultichoiceCollection.Common.Entities.Base;

namespace MultichoiceCollection.Common.Entities
{
    public class Transaction : BaseEntity
    {
        [Key]
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
        public DateTime CreatedDate { get; set; }
        public bool PrintedReceipt { get; set; }
    }
}