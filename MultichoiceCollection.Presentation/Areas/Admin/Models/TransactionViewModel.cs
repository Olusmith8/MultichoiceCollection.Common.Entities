using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MultichoiceCollection.Common.Entities;
using MultichoiceCollection.Presentation.Models;

namespace MultichoiceCollection.Presentation.Areas.Admin.Models
{
    public class TransactionViewModel
    {
        public List<BouquetModel> Bouquets { get; set; }
        public List<Transaction> Transactions { get; set; }

        public string Bouquet { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}