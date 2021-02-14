using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class SpravkaController : Controller
    {
        private readonly IDiagnosis doctor;

        public SpravkaController(IDiagnosis client)
        {
            doctor = client;
        }
        public IActionResult Diagnosis()
        {
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            ViewBag.Diagnosis = doctor.Read(null);

            return View();
        }
        public ActionResult AddDiagnosis( string _ocenka)
        {
            var diagnosis = doctor.Read(new BusinessLogic.BindingModel.DiagnosisBindingModel { Name = _ocenka });
            if (diagnosis.Count > 0)
            {
                TempData["ErrorLack"] = "Такой диагноз уже есть в базе данных";
                return RedirectToAction("Diagnosis");
            }
            doctor.CreateOrUpdate(new DiagnosisBindingModel
            {

                Name = _ocenka
            });
            return RedirectToAction("Diagnosis");
        }
    }
}
