using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultichoiceCollection.Presentation.Models
{
    public class MakePaymentViewModel
    {
        public List<BouquetModel> Bouquets { get; set; }
        public string Type { get; set; }
    }
}