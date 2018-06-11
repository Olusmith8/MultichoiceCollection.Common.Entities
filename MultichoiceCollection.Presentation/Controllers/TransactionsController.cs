using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultichoiceCollection.Common.Entities.Enum;
using MultichoiceCollection.Presentation.Attributes;
using MultichoiceCollection.Presentation.Models;
using MultichoiceCollection.Presentation.Services;
using MultichoiceCollection.Services.Implementations;

namespace MultichoiceCollection.Presentation.Controllers
{
    [CustomAuthorize]
    public class TransactionsController : BaseController
    {
        // GET: Transactions
        private readonly ApiRequestService _request;
        public TransactionsController()
        {
            _request = new ApiRequestService();
        }
        
        public ActionResult TransactionInquiries(int id)
        {
            
            ViewBag.TransactionInquiries = "current";
            var response = new TransactionsResponseModel();
            try
            {
                var url = ApiConstantService.BASE_URL + "transactions/" + id;
                response = _request.MakeRequest<TransactionsResponseModel>(url);
                return View(response);
            }
            catch (Exception exception)
            {
                this.ShowMessage(exception.Message, AlertType.Danger);
                return View(response);
            }
        }
        public ActionResult Confirmation(int id)
        {

            ViewBag.Confirmation = "current";
            var response = new ConfirmationResponseModel();
            try
            {
                var url = ApiConstantService.BASE_URL + "confirmation/" + id;
                response = _request.MakeRequest<ConfirmationResponseModel>(url);
                return View(response);
            }
            catch (Exception exception)
            {
                this.ShowMessage(exception.Message, AlertType.Danger);
                return View(response);
            }
        }


    }
}
