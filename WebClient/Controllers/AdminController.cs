using BusinessLogic.BindingModel;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using BusinessLogic.Report;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using static BusinessLogic.Report.ReportLogic;
using BusinessLogic.ViewModel;
using System.IO.Compression;

namespace WebClient.Controllers
{
    public class AdminController : Controller
    {
        private string password = "qwerty";
        private readonly IDoctor _client;
        private readonly IHealing heal;
        private readonly IDiagnosis diagnosis;
        private readonly IPacient pacient;
        private readonly IWard ward;
        private readonly IService service;
        private readonly ReportLogic report;
        public AdminController(IService service, ReportLogic report,IWard ward, IHealing healing,IPacient pacient, IDiagnosis diagnosis, IDoctor client)
        {
            this.service = service;
            this.report = report;
            this.ward = ward;
            this.diagnosis = diagnosis;
            this.pacient = pacient;
            heal = healing;
            _client = client;
        }
        public IActionResult Index(AdminModel model)
        {
            if (model.Password == password)
            {
                Program.AdminMode = !Program.AdminMode;
                return RedirectToAction("Doctors");
            }
            else if (model.Password != password && model.Password != null)
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль");
                return View();
            }
            return View();
        }
        public IActionResult Doctors()
        {
            ViewBag.Doctors = _client.Read(new DoctorBindingModel
            {
              Status= BusinessLogic.Enums.DoctorStatus.Лечащий_Врач
            });
            return View();
        }
        public IActionResult AddDoctor( )
        {
            return View();
        }
        [HttpPost]
        public ViewResult AddDoctor(RegistrationModel client)
        {
            if (String.IsNullOrEmpty(client.Login))
            {
                ModelState.AddModelError("", "Введите логин");
                return View(client);
            }
            if (client.Login.Length < 0 ||
           client.Login.Length > 51)
            {
                ModelState.AddModelError("", $"Длина логина должна быть от {1} до {50} символов");
                return View(client);
            }
            var existClient = _client.Read(new DoctorBindingModel
            {
                Login = client.Login
            }).FirstOrDefault();
            if (existClient != null)
            {
                ModelState.AddModelError("", "Данный логин уже занят");
                return View(client);
            }
          
            if (client.Password.Length < 6 ||
            client.Password.Length > 13)
            {
                ModelState.AddModelError("", $"Длина пароля должна быть от {6} до {12} символов");
                return View(client);
            }
         
            if (String.IsNullOrEmpty(client.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(client);
            }
            _client.CreateOrUpdate(new DoctorBindingModel
            {
              
                Login = client.Login,
                Password = client.Password,
                Status = BusinessLogic.Enums.DoctorStatus.Лечащий_Врач
            });
            ModelState.AddModelError("", "Вы успешно зарегистрированы");
            return View("AddDoctor", client);
        }
        public IActionResult AddMedics()
        {
            return View();
        }
        [HttpPost]
        public ViewResult AddMedics(RegistrationModel client)
        {
            if (String.IsNullOrEmpty(client.Login))
            {
                ModelState.AddModelError("", "Введите логин");
                return View(client);
            }
            if (client.Login.Length < 0 ||
           client.Login.Length > 51)
            {
                ModelState.AddModelError("", $"Длина логина должна быть от {1} до {50} символов");
                return View(client);
            }
            var existClient = _client.Read(new DoctorBindingModel
            {
                Login = client.Login
            }).FirstOrDefault();
            if (existClient != null)
            {
                ModelState.AddModelError("", "Данный логин уже занят");
                return View(client);
            }

            if (client.Password.Length < 6 ||
            client.Password.Length > 13)
            {
                ModelState.AddModelError("", $"Длина пароля должна быть от {6} до {12} символов");
                return View(client);
            }

            if (String.IsNullOrEmpty(client.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(client);
            }
            _client.CreateOrUpdate(new DoctorBindingModel
            {

                Login = client.Login,
                Password = client.Password,
                Status = BusinessLogic.Enums.DoctorStatus.Медперсонал
            });
            ModelState.AddModelError("", "Вы успешно зарегистрированы");
            return View("AddMedics", client);
        }
        public IActionResult Medics()
        {
            ViewBag.Doctors = _client.Read(new DoctorBindingModel
            {
                Status = BusinessLogic.Enums.DoctorStatus.Медперсонал
            });
            return View();
        }

        public IActionResult Logout()
        {
            Program.AdminMode = false;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public JsonResult Metod()
        {
            List<DiagrammaModel> testDataFirst = new List<DiagrammaModel>();
            var palats = ward.Read(null);
            foreach (var palat in palats)
            {
                decimal count = 0;
                var temps = heal.Read(new HealingBindingModel { WardId= (int)palat.Id});
                foreach (var temp in temps)
                {

                        count=(count+temp.Temperatura);

                }
                if (temps.Count != 0)
                    count = count / temps.Count;
                testDataFirst.Add(new DiagrammaModel() { cityName = Convert.ToString(palat.Number), PopulationYear2020 = count });
            }

            var populationList = testDataFirst;
            return Json(populationList);
        }
            public IActionResult AnaliticReport(string Person)
        {
            var heals = heal.Read(new HealingBindingModel { Data= new DateTime(Convert.ToInt32(Person), 1, 1) });
            string fileName = "C:\\data\\" +
            $"Аналитический отчет  за-{Person}.pdf";

            report.SaveFileAnalitic(fileName, heals, Person);
            var Diagnosis = diagnosis.Read(null);
            List<counts> people = new List<counts> { };
            var Pacient = pacient.Read(null);
            var Counts = heal.Read(new HealingBindingModel { Data = new DateTime(Convert.ToInt32(Person), 1, 1) });
            foreach (var x in Diagnosis)
            {

                foreach (var y in Pacient)
                {
                    people.Add(new counts
                    {
                        PacientId = (int)y.Id,
                        DiagnosisId = (int)x.Id,
                        Count = Counts.Where(c => c.DiagnosisId == x.Id && c.PacientId == y.Id).ToList().Count
                    });
                }
            }
            ViewBag.Diagnosis = diagnosis.Read(null);
            ViewBag.Pacient = pacient.Read(null);
            ViewBag.Count = Counts.Count;
            ViewBag.list = people;
            ViewBag.Person = Person;
            return View("Report", new {Person=Person });
        }

              public IActionResult ArchiveService(string id)
        {

            var services = service.Read(new ServiceBindingModel { Id = Convert.ToInt32(id) });//все договоры
            string fileName =  $"C:\\data\\ArchiveOf{services[0].Name}";
            Directory.CreateDirectory(fileName);
            if (Directory.Exists(fileName))
            {
               
                DataContractJsonSerializer jsonFormatter = new
               DataContractJsonSerializer(typeof(List<ServiceViewModel>));
                using (FileStream fs = new FileStream(string.Format("{0}/{1}.json",
               fileName, "Services"), FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, services);
                }
                service.Delete(new ServiceBindingModel {Id=Convert.ToInt32(id) });
                ZipFile.CreateFromDirectory(fileName, $"{fileName}.zip");
                Directory.Delete(fileName, true);
                return RedirectToAction("Archiv");
            }
            ViewBag.Service = service.Read(null);
            return View("Archiv");
        }

    public IActionResult Archiv()
       {
            ViewBag.Service = service.Read(null);
             return View();
        }
public IActionResult ReportPere(string id)
        {
            var heals = heal.Read(new HealingBindingModel { Data = new DateTime(Convert.ToInt32(id), 1, 1) });
            string fileName = "C:\\data\\" +
            $"Перекрестный отчет  за-{id}.pdf";
            var Diagnosis = diagnosis.Read(null);
            List<counts> people = new List<counts> { };
            var Pacient = pacient.Read(null);
            var Counts = heal.Read(new HealingBindingModel { Data = new DateTime(Convert.ToInt32(id), 1, 1) });
            foreach (var x in Diagnosis)
            {

                foreach (var y in Pacient)
                {
                    people.Add(new counts
                    {
                        PacientId = (int)y.Id,
                        DiagnosisId = (int)x.Id,
                        Count = Counts.Where(c => c.DiagnosisId == x.Id && c.PacientId == y.Id).ToList().Count
                    });
                }
            }
            ViewBag.Diagnosis = diagnosis.Read(null);
            ViewBag.Pacient = pacient.Read(null);
            ViewBag.Count = Counts.Count;
            ViewBag.list = people;
            ViewBag.Person = id;
            report.SaveFilePere(fileName, diagnosis.Read(null), pacient.Read(null), people, id);
            return View("Report", new { Person = id });
        }

        


        public IActionResult Report(string Person)
        {if (Person == null)
            {
                Person = $"{DateTime.Now.Year}";
               
            }
               
            var Diagnosis = diagnosis.Read(null);
          List<counts> people = new List<counts> { };
            var Pacient = pacient.Read(null);
            var Counts = heal.Read(new HealingBindingModel { Data = new DateTime(Convert.ToInt32(Person), 1, 1) });
            foreach (var x in Diagnosis)
            {

                foreach (var y in Pacient)
                {
                    people.Add(new counts { 
                        PacientId= (int)y.Id,
                        DiagnosisId = (int)x.Id,
                        Count = Counts.Where(c => c.DiagnosisId == x.Id && c.PacientId == y.Id).ToList().Count});
                }
            }
            ViewBag.Person = Person;
            ViewBag.Diagnosis = diagnosis.Read(null);
            ViewBag.Pacient = pacient.Read(null);
            ViewBag.Count = Counts.Count;
            ViewBag.list = people;
            return View();
        }
    }
    }
