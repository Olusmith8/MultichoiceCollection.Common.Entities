using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultichoiceCollection.Common.Entities.Enum;
using MultichoiceCollection.Models.Repositories.Context;
using MultichoiceCollection.Presentation.Attributes;
using MultichoiceCollection.Presentation.Models;
using MultichoiceCollection.Presentation.Services;
using MultichoiceCollection.Services.Implementations;

namespace MultichoiceCollection.Presentation.Controllers
{
    [CustomAuthorize]
    public class HomeController : BaseController
    {
        readonly ApiRequestService request;
        private AppDbContext _appDbContext;
        public HomeController()
        {
             request = new ApiRequestService();
            this._appDbContext = new AppDbContext();

        }
        public ActionResult Index()
        {
            // ViewBag.TotalSales = _appDbContext.Transactions.Sum(tr => tr.amount).ToString("N");
            if (_appDbContext.Transactions.Any())
            {
                ViewBag.TotalSales = _appDbContext.Transactions.Sum(tr => tr.amount);
                ViewBag.PrintedReceipts = _appDbContext.Transactions.Count(tr => tr.PrintedReceipt == true);
                ViewBag.LatestTransactions = _appDbContext.Transactions.OrderByDescending(tr => tr.id).Take(10)
                    .ToList();
            }

            ViewBag.Customers = UserManager.Users.Count();
            ViewBag.CurrentDashboard = "current";
            return View();
        }
        public ActionResult BouquetTypes()
        {
            ViewBag.Types = "current";
            var response = new List<BouquetTypeModel>();
            try
            {
                
                 response = request.MakeRequest<List<BouquetTypeModel>>(ApiConstantService.TYPES);
                return View(response);
            }
            catch (Exception exception)
            {
                this.ShowMessage(exception.Message, AlertType.Danger);
                return View(response);
            }

        }
        public ActionResult Bouquets(string type)
        {
            ViewBag.Bouquets = "current";
            if (string.IsNullOrEmpty(type))
            {
                this.ShowMessage("Type of bouquet must be selected", AlertType.Danger);
                return RedirectToAction("Index");
            }
            if (!type.Equals("gotv") && !type.Equals("dstv"))
            {
                this.ShowMessage("Bouquet type can only be gotv or dstv", AlertType.Danger);
                return RedirectToAction("Index");
            }
                var response = new List<BouquetModel>();
            try
            {
                var url = ApiConstantService.BASE_URL + type + "/bouquet";
                 response = request.MakeRequest<List<BouquetModel>>(url);
                return View(response);
            }
            catch (Exception exception)
            {
                this.ShowMessage(exception.Message, AlertType.Danger);
                return View(response);
            }

        }

        public ActionResult Accounts(string type, string cardNumber)
        {
            ViewBag.Accounts = "current";
            if (string.IsNullOrEmpty(type))
            {
                this.ShowMessage("Type of bouquet must be selected", AlertType.Danger);
                return RedirectToAction("Index");
            }
            if (!type.Equals("gotv") && !type.Equals("dstv"))
            {
                this.ShowMessage("Bouquet type can only be gotv or dstv", AlertType.Danger);
                return RedirectToAction("Index");
            }
            if (string.IsNullOrEmpty(cardNumber))
            {
                this.ShowMessage("Smart card number is required.", AlertType.Danger);
                return RedirectToAction("Index");
            }
            var response = new CustomerAccountModel();
            try
            {
                var url = ApiConstantService.BASE_URL + type + "/accounts/" + cardNumber;
                response = request.MakeRequest<CustomerAccountModel>(url);
                return View(response);
            }
            catch (Exception exception)
            {
                this.ShowMessage(exception.Message, AlertType.Danger);
                return View(response);
            }
        }
        public ActionResult BouquetDetails(string type, string cardNumber)
        {
            ViewBag.BouquetDetails = "current";
            if (string.IsNullOrEmpty(type))
            {
                this.ShowMessage("Type of bouquet must be selected", AlertType.Danger);
                return RedirectToAction("Index");
            }
            if (!type.Equals("gotv") && !type.Equals("dstv"))
            {
                this.ShowMessage("Bouquet type can only be gotv or dstv", AlertType.Danger);
                return RedirectToAction("Index");
            }
            if (string.IsNullOrEmpty(cardNumber))
            {
                this.ShowMessage("Smart card number is required.", AlertType.Danger);
                return RedirectToAction("Index");
            }
            var response = new CustomerDetailsModel();
            try
            {
                var url = ApiConstantService.BASE_URL + type + "/details/" + cardNumber;
                response = request.MakeRequest<CustomerDetailsModel>(url);
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