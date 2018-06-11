using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MultichoiceCollection.Common.Entities.Base;
using MultichoiceCollection.Common.Entities.Enum;
using MultichoiceCollection.Models.Repositories.Context;
using MultichoiceCollection.Presentation.Areas.Admin.Models;
using MultichoiceCollection.Presentation.Attributes;
using MultichoiceCollection.Presentation.Controllers;
using MultichoiceCollection.Presentation.Models;
using MultichoiceCollection.Presentation.Services;
using MultichoiceCollection.Services.Implementations;

namespace MultichoiceCollection.Presentation.Areas.Admin.Controllers
{
    [AdminAuthorize(Roles = RoleNames.RoleAdmin)]
    public class DashboardController : AdminBaseController
    {
        readonly ApiRequestService request;
        public DashboardController()
        {
             request = new ApiRequestService();
        }
        public ActionResult Index()
        {
            ViewBag.CurrentDashboard = "current";
            return View();
        }

        public ActionResult Transactions(DateTime? startDate = null, DateTime? endDate = null)
        {
            ViewBag.Transactions = "current";
            using (var context = new AppDbContext())
            {
                var query = context.Transactions.Where(t=>t.success);
                if (endDate.HasValue)
                {
                    query = query.Where(q => q.CreatedDate >= startDate && q.CreatedDate <= endDate);
                }
                var transactions = query.OrderByDescending(t => t.CreatedDate).Take(20);//todo do pagination
                var vm = new TransactionViewModel{Transactions = transactions.ToList()};

                return View(vm);
            }

        }

        public ActionResult Reports()
        {
            ViewBag.Reports = "current";
            using (var context = new AppDbContext())
            {
                var transactions = context.Transactions.OrderByDescending(t => t.CreatedDate).Take(50);//todo do pagination
                return View(transactions.ToList());
            }
        }
    }
}