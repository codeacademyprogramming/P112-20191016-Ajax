using AjaxProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AjaxProject.Controllers
{
    public class UserController : Controller
    {
        P112RentACarEntities db = new P112RentACarEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(Account account, HttpPostedFileBase Photo)
        {
            ResponseModel response = new ResponseModel();
        

            if (string.IsNullOrEmpty(account.Fullname) ||
                string.IsNullOrEmpty(account.Password) ||
                Photo == null)
            {
                response.Success = false;
                response.ErrorMessage = "Fullname, Password ve Shekil bosh qoyula bilmez";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            Account accountFromDb = db.Accounts.FirstOrDefault(a => a.Email == account.Email);
            if (accountFromDb != null)
            {
                response.Success = false;
                response.ErrorMessage = "Bu email artiq istifade olunub";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            if (Photo.ContentLength / 1024 > 1024)
            {
                response.Success = false;
                response.ErrorMessage = "Sheklin hecmi max 1 mb ola biler";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            if (Photo.ContentType != "image/png" && Photo.ContentType != "image/jpeg")
            {
                response.Success = false;
                response.ErrorMessage = "Sheklin formati png ve jpeg olmalidir";
                return Json(response, JsonRequestBehavior.AllowGet);
            }

            if (account.Fullname == "Anar")
            {
                response.Success = false;
                response.ErrorMessage = "Anar adli shexsler blok olunub";
                return Json(response, JsonRequestBehavior.AllowGet);

            }

            string photoName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Photo.FileName;
            string photoPath = Path.Combine(Server.MapPath("~/Uploads"), photoName);

            Photo.SaveAs(photoPath);
            account.Photo = photoName;

            string hashedPass = Crypto.HashPassword(account.Password);
            account.Password = hashedPass;
            db.Accounts.Add(account);
            db.SaveChanges();

            response.Success = true;
            response.Data = "Ugurla qeydiyyatdan kecdiniz";

            Thread.Sleep(3000);

            return Json(response);
        }

        public JsonResult CheckEmail(string Email)
        {
            if (db.Accounts.FirstOrDefault(a => a.Email == Email) != null)
            {
                return Json(new {
                    valid = false,
                    message = "Email movcuddur"
                }, JsonRequestBehavior.AllowGet);

            }

            return Json(new
            {
                valid = true,
                message = "Email movcud deyil"
            }, JsonRequestBehavior.AllowGet);

        }
    }
}