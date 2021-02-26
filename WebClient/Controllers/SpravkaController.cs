﻿using BusinessLogic.BindingModel;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public SpravkaController(IDiagnosis client, IService se)
        {
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
        public IActionResult Service(int id,AddServiceModel model)
        {
            ViewBag.Id = id;
       if(id==1)
            ViewBag.Diagnosis = service.Read(new ServiceBindingModel { Status=ServiceStatus.Лекарство});
       else
                ViewBag.Diagnosis = service.Read(new ServiceBindingModel { Status = ServiceStatus.Процедура });
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
            ViewBag.Id = id;
            ViewBag.Name = diagnos.Read(new DiagnosisBindingModel { Id = id })[0].Name;
            var ServiceYes = service.ReadDiagnosis(null);
            var ServiceAll = service.Read(null);
            var yesservice = new Dictionary<int, int> { };
            var Service = new List<ServiceViewModel> { };

            foreach (var gr in ServiceYes)
            {
                if ((!yesservice.ContainsKey(gr.ServiceId)))
                {
                    yesservice.Add(gr.ServiceId, 3);
                }
            }
            foreach (var g in ServiceAll)
            {
                if (yesservice.ContainsKey((int)g.Id))
                {
                    Service.Add(g);
                }
            }
            ViewBag.DiaSer = Service;
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
                    yesservice.Add(gr.ServiceId, 3);
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
                string str = "Все постояльцы уже заселены в этот номер";
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
            { Status = ServiceStatus.Лекарство })
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