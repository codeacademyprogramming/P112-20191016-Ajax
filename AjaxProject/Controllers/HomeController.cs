using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxProject.Models;
using Newtonsoft.Json;

namespace AjaxProject.Controllers
{
    public class HomeController : Controller
    {
        private P112RentACarEntities db = new P112RentACarEntities();
        public ActionResult Index()
        {
            List<Manufacturer> model = db.Manufacturers.ToList();

            return View(model);
        }



        public ActionResult Models(int id)
        {
            string output = "";
            var carmodels = db.CarModels.Where(c => c.ManufacturerId == id).ToList();

            foreach (var carmodel in carmodels)
            {
                output += "<option value=\"" + carmodel.Id + "\">" + carmodel.Name + "</option>";
            }
            return Content(output);
        }

        public JsonResult ModelsJson(int id)
        {
            ResponseModel response = new ResponseModel();

            var carmodels = db.CarModels.Where(c => c.ManufacturerId == id).Select(s => new { s.Id, s.Name  }).ToList();

            if (carmodels == null || carmodels.Count == 0)
            {
                response.Success = false;
                response.ErrorMessage = "Bu istehsalcinin modelleri movcud deyil";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            response.Success = true;
            response.Data = JsonConvert.SerializeObject(carmodels);
            response.ErrorMessage = null;

            return Json(response, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}