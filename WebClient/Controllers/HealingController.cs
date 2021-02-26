using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class HealingController : Controller
    {
    private readonly    IHealing heal;
        private readonly IPacient p;
        private readonly IDiagnosis diagnos;
        public HealingController(IHealing healing,IDiagnosis diagnosis, IPacient pacient )
        {
            heal = healing;
            p = pacient;
            diagnos = diagnosis;
        }
        public IActionResult Healing(int id)
        {
            ViewBag.Id = id;
            ViewBag.Diagnos = diagnos.Read(null);
            ViewBag.Pacient = p.Read(null);
            ViewBag.Healing = heal.Read(new HealingBindingModel {DoctorId= (int)Program.User.Id, PacientId=id});
            return View();
        }
        public IActionResult AddHealing(int id)
        {
            ViewBag.Person = 37;
            ViewBag.Id = id;
              ViewBag.Diagnos = new SelectList(diagnos.Read(null), "Id", "Name");
             return View();
        }
        public IActionResult AddService( int id, decimal Person, string  Diagnos)
        {
            ViewBag.Id = id;
            if (TempData["ErrorLack"] != null)
            {
                ModelState.AddModelError("", TempData["ErrorLack"].ToString());
            }
           var d = new SelectList(diagnos.Read(new DiagnosisBindingModel { Id = Convert.ToInt32(Diagnos) }), "Id", "Name");
            return View("AddHealing", new { id = id, Diagnos=d, Dynamic = ViewBag.Person = Person });
        }
    }
}
