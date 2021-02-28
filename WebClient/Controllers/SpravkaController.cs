using BusinessLogic.BindingModel;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class SpravkaController : Controller
    {
        private readonly IDiagnosis diagnos;
        private readonly IService service;
        private readonly ReportLogic report;
        public SpravkaController(IDiagnosis client, ReportLogic report, IService se)
        {
            this.report = report;
            diagnos = client;
            service = se;
        }
        public IActionResult Diagnosis()
        {
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
            ViewBag.Diagnosis = diagnos.Read(null);

            return View();
        }
        public ActionResult AddDiagnosis(string _ocenka)
        {
            if (_ocenka == null)
            {
                TempData["ErrorLack"] = "Вы не ввели название";
                return RedirectToAction("Diagnosis");
            }
            var diagnosis = diagnos.Read(new DiagnosisBindingModel { Name = _ocenka });
            if (diagnosis.Count > 0)
            {
                TempData["ErrorLack"] = "Такой диагноз уже есть в базе данных";
                return RedirectToAction("Diagnosis");
            }
            diagnos.CreateOrUpdate(new DiagnosisBindingModel
            {

                Name = _ocenka
            });
            return RedirectToAction("Diagnosis");
        }
        public IActionResult Service(string Diagnos, int id,AddServiceModel model)
        {
            var diagnoss = diagnos.Read(null);
            diagnoss.Add(new DiagnosisViewModel { Id = 0 , Name="Все"});
            ViewBag.Diagnos = new SelectList(diagnoss,"Id", "Name");
            ViewBag.Id = id;
            if (id == 1)
                if (Diagnos == null || Diagnos == "0")
                    ViewBag.Diagnosis = service.Read(new ServiceBindingModel { Status = ServiceStatus.Лекарство });
                else
                {
                    var servicediagnos = service.ReadDiagnosis(new DiagnosisServiceBindingModel { DiagnosisId = Convert.ToInt32(Diagnos) });
                    List<ServiceViewModel> servis = new List<ServiceViewModel> { };
                    foreach (var s in servicediagnos)
                    {
                        if(s.DiagnosisId== Convert.ToInt32(Diagnos))
                            if(service.Read(new ServiceBindingModel { Id = s.ServiceId })[0].Status==ServiceStatus.Лекарство)
                           servis.Add(service.Read(new ServiceBindingModel { Id = s.ServiceId })[0]);
                    }
                    ViewBag.Diagnosis = servis.OrderBy(u => u.Cena);
                }

            else
            {
                if (Diagnos == null || Diagnos == "0")
                    ViewBag.Diagnosis = service.Read(new ServiceBindingModel { Status = ServiceStatus.Процедура });
                else
                {
                    var servicediagnos = service.ReadDiagnosis(new DiagnosisServiceBindingModel { DiagnosisId = Convert.ToInt32(Diagnos) });
                    List<ServiceViewModel> servis = new List<ServiceViewModel> { };
                    foreach (var s in servicediagnos)
                    {
                        if (s.DiagnosisId == Convert.ToInt32(Diagnos))
                            if (service.Read(new ServiceBindingModel { Id = s.ServiceId })[0].Status == ServiceStatus.Процедура)
                                servis.Add(service.Read(new ServiceBindingModel { Id = s.ServiceId })[0]);
                    }
                    ViewBag.Diagnosis = servis.OrderBy(u => u.Cena);
                }
            }
            return View();
        }


        public IActionResult AddService(int id)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }
            ViewBag.Id = id;
            return View();
        }
        public IActionResult DiaSer(int id)
        {
            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }
            ViewBag.Id = id;
            ViewBag.Name = diagnos.Read(new DiagnosisBindingModel { Id = id })[0].Name;
            var ServiceYes = service.ReadDiagnosis(new DiagnosisServiceBindingModel { DiagnosisId=id});
            List<ServiceViewModel> services = new List<ServiceViewModel> { };
          foreach(var serv in ServiceYes)
            {
                services.Add(service.Read(new ServiceBindingModel { Id=serv.ServiceId})[0]);
            }
            ViewBag.DiaSer = services;
            return View();
        }
        public IActionResult AddDS(int id)
        {
            ViewBag.Id =id;
            var ServiceYes = service.ReadDiagnosis(new DiagnosisServiceBindingModel {DiagnosisId  = id });

            var ServiceAll = service.Read(null);
            var yesservice = new Dictionary<int, int> { };
            var Service = new List<ServiceViewModel> { };
            
            foreach (var gr in ServiceYes)
            {
                if ((!yesservice.ContainsKey(gr.ServiceId)))
                {
                    yesservice.Add(gr.ServiceId, gr.ServiceId);
                }
            }
            foreach (var g in ServiceAll)
            {
                if (!yesservice.ContainsKey((int)g.Id))
                {
                    Service.Add(g);
                }
            }
            if (Service.Count == 0)
            {
                TempData["CustomError"] = "Все постояльцы уже заселены в этот номер";
                return RedirectToAction("DiaSer", new { id =id});

            }
            ViewBag.Guests = Service;
            return View();
        }
        public IActionResult AddSD(int id, int sid)
        {
       
            service.CreateOrUpdateDiagnosis(new DiagnosisServiceBindingModel {  DiagnosisId=id, ServiceId=sid});
            return RedirectToAction("DiaSer", new { id = id });
        }
        public IActionResult SendPdfSpravka(int id)
        {
            var services = service.Read(null);
            string fileName = "C:\\data\\" +
            $"Прейскурант на услуги за-{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}" +
            DateTime.Now.Minute + ".pdf";

            report.SaveFileService(fileName, services, service.ReadDiagnosis(null), diagnos.Read(null));
            return RedirectToAction("Service", new { id= id});
        }
        [HttpPost]
        public ActionResult AddLec(AddServiceModel model)
        {
            int id = 1;
            if (model.Name == null)
            {
                TempData["CustomError"] = "Вы не ввели название";
                return RedirectToAction("AddService", new { id = id, model = model });
            }
            var Service = service.Read(new ServiceBindingModel {Name=model.Name, Status=ServiceStatus.Лекарство});
            if (Service.Count!=0)
            {
                TempData["CustomError"] = "Такое лекарство уже есть";
                return RedirectToAction("AddService", new { id = id, model = model });
            }
        
            if (model.Count < 0)
            {
                TempData["CustomError"] = "Цена должна быть от 0";
                return RedirectToAction("AddService", new { id = id, model = model });
            }
           
            service.CreateOrUpdate(new ServiceBindingModel
            {
           Name=model.Name,
           Status=ServiceStatus.Лекарство,
           Cena=model.Count
            });
            return View("Service", new { Dynamic = ViewBag.Diagnosis= service.Read(new ServiceBindingModel
            { Status = ServiceStatus.Лекарство }),
                id = 1
            });
        }
        [HttpPost]
        public ActionResult AddPro(AddServiceModel model)
        {
            if (model.Name == null)
            {
                TempData["CustomError"] = "Вы не ввели название";
                return RedirectToAction("AddService", new { id = 2, model = model });
            }
            var Service = service.Read(new ServiceBindingModel { Name = model.Name, Status = ServiceStatus.Процедура });
            if (Service.Count != 0)
            {
                TempData["CustomError"] = "Такое лекарство уже есть";
                ModelState.AddModelError("", $"Такое лекарство уже есть");
                return RedirectToAction("AddService", new { id = 2, model = model });
            }

            if (model.Count < 0)
            {
                TempData["CustomError"] = "Цена должна быть от 0";
                return RedirectToAction("AddService", new { id = 2, model = model });
            }

            service.CreateOrUpdate(new ServiceBindingModel
            {
                Name = model.Name,
                Status = ServiceStatus.Процедура,
                Cena = model.Count
            });
            return View("Service", new
            {
                Dynamic = ViewBag.Diagnosis = service.Read(new ServiceBindingModel
                { Status = ServiceStatus.Процедура })
            });
        }

    }









    
}
