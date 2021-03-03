using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using BusinessLogic.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HealingController : Controller
    {
    private readonly    IHealing heal;
        private readonly IPacient p;
        private readonly IWard ward;
        private readonly IDiagnosis diagnos;
        private readonly IService service;
        private readonly ReportLogic report;
        public HealingController(ReportLogic report,IService service,IHealing healing,IDiagnosis diagnosis, IPacient pacient , IWard ward)
        {
            this.report = report;
            this.service = service;
            heal = healing;
            p = pacient;
            this.ward = ward;
            diagnos = diagnosis;
        }

        public IActionResult SendPdfReport(int id, int pid,int hid)
        {
            //   var guests = room.ReadGuest(new GuestRoomViewModel { RoomId = id });
            var services = service.Read(new ServiceBindingModel { Id=id})[0];
            var pacient = p.Read(new PacientBindingModel {Id = pid })[0];
            string fileName = "C:\\data\\" + 
            $"Рецепт  на {services.Name}за-{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}" +
            DateTime.Now.Minute + ".pdf";
            report.SaveFile(fileName, services,pacient, Program.User);
            return RedirectToAction("HealingService", new { heallingid = hid, pacientid =pid});
        }

        public IActionResult HealingService(int heallingid, int pacientid)
        {
            ViewBag.Service = service.Read(null);
            ViewBag.HealId = heallingid;
            ViewBag.PacientId = pacientid;
            ViewBag.FIO = p.Read(new PacientBindingModel { Id = pacientid })[0].FIO;
            ViewBag.HS = heal.ReadService(new HealingServiseBindingModel {HealingId=heallingid });
            return View();
        }
        public IActionResult ServiceOut(int id, int heallingid, int pacientid)
        {
            heal.CreateOrUpdateService(new HealingServiseBindingModel
            {
                Id = id,
                Status = BusinessLogic.Enums.OutStatus.Принято
            });
            return RedirectToAction("HealingService", new { heallingid= heallingid, pacientid = pacientid });
        }
        public IActionResult Healing(int id)
        {
            ViewBag.Id = id;
            ViewBag.Diagnos = diagnos.Read(null);
            ViewBag.Pacient = p.Read(null);
            if (Program.User.Status == BusinessLogic.Enums.DoctorStatus.Лечащий_Врач)
            {
                ViewBag.Healing = heal.Read(new HealingBindingModel { DoctorId = (int)Program.User.Id, PacientId = id });
            }
            else
            {
                ViewBag.Healing = heal.Read(new HealingBindingModel { PacientId = id });

            }
            return View();
        }
        public IActionResult AddHealing(int id)
        {
            ViewBag.Person = 37;
            ViewBag.Id = id;
            ViewBag.Diagnos = new SelectList(diagnos.Read(null), "Id", "Name");
            var number = ward.Read(null);
            List<WardViewModel> wards = new List<WardViewModel> { };
            foreach(var n in number)
            {
                if(ward.ReadPacient(new WardPacientBindingModel {WardId= (int)n.Id }).Count < n.Mesto)
                {
                    wards.Add(n);
                }
            }
            ViewBag.Number = new SelectList(wards, "Id", "Number"); 
            return View();
        }
        public IActionResult AddService( int id, decimal Person, string  Diagnos, string Number)
        {
            ViewBag.Id = id;
            ViewBag.DiagnosisId = Convert.ToInt32(Diagnos);
            ViewBag.Number = Convert.ToInt32(Number);
            ViewBag.Temp = Person;
            ViewBag.Diagnosis = diagnos.Read(new DiagnosisBindingModel { Id = Convert.ToInt32(Diagnos) })[0].Name;
            ViewBag.Name = p.Read(new PacientBindingModel { Id = id })[0].FIO;
           var servicediagnos = service.ReadDiagnosis(new DiagnosisServiceBindingModel { DiagnosisId=Convert.ToInt32(Diagnos)});
            List<ServiceViewModel> servis = new List<ServiceViewModel> { };
            foreach(var s in servicediagnos)
            {
                servis.Add(service.Read(new ServiceBindingModel { Id = s.ServiceId})[0]);
            }
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
           var d = new SelectList(diagnos.Read(new DiagnosisBindingModel { Id = Convert.ToInt32(Diagnos) }), "Id", "Name");
            ViewBag.Service=servis.OrderBy(u => u.Cena);
            return View();
        }
        [HttpPost]
        public ActionResult AddService(int id, decimal Temp, string DiagnosisId, string Number,ServiceModel model)
        {

            var visitDoctors = new List<HealingServiseBindingModel>();

            foreach (var doctor in model.VisitDoctors)
            {
                if (doctor.Value > 0)
                {
                    visitDoctors.Add(new HealingServiseBindingModel
                    {
                       ServiseId=doctor.Key
                    });
                }
            }
            if (visitDoctors.Count == 0)
            {
                ModelState.AddModelError("", "Ни один service не выбран");
                var servicediagnos = service.ReadDiagnosis(new DiagnosisServiceBindingModel { DiagnosisId = Convert.ToInt32(DiagnosisId) });
                List<ServiceViewModel> servis = new List<ServiceViewModel> { };
                foreach (var s in servicediagnos)
                {
                    servis.Add(service.Read(new ServiceBindingModel { Id = s.ServiceId })[0]);
                }
                ViewBag.Id = id;
                 ViewBag.DiagnosisId = DiagnosisId;
                      ViewBag.Number = Number;
                      ViewBag.Temp = Temp;
                      ViewBag.Diagnosis = diagnos.Read(new DiagnosisBindingModel { Id = Convert.ToInt32(DiagnosisId) })[0].Name;
                     ViewBag.Name = p.Read(new PacientBindingModel { Id = id })[0].FIO;
                      ViewBag.Service = servis.OrderBy(u => u.Cena);
                return View("AddService", model);
            }
            ward.CreateOrUpdate(new WardPacientBindingModel { PacientId = id, WardId = Convert.ToInt32(Number) });
            heal.CreateOrUpdate(new HealingBindingModel
            {
                WardId = Convert.ToInt32(Number),
                Data = DateTime.Now,
                DiagnosisId = Convert.ToInt32(DiagnosisId),
                DoctorId = (int)Program.User.Id,
                PacientId = id,
                Temperatura = Temp
            });
            var t = heal.Read(new HealingBindingModel
            {
                WardId = Convert.ToInt32(Number),
                DiagnosisId = Convert.ToInt32(DiagnosisId),
                PacientId = id,
                DoctorId = (int)Program.User.Id
            });
            foreach (var s in model.VisitDoctors)
            {
                if (s.Value > 0)
                {
                    for(int i=0; i<s.Value; i++)
                    heal.CreateOrUpdateService(new HealingServiseBindingModel { 
                     HealingId= (int)t[t.Count-1].Id,
                      ServiseId=s.Key,
                       Status = BusinessLogic.Enums.OutStatus.Непринято
                    });
                }
            }
            return RedirectToAction("Healing", new { id=id});
        }
    }
}
