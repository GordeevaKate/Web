using BusinessLogic.BindingModel;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class AdminController : Controller
    {
        private string password = "qwerty";
        private readonly IDoctor _client;

        public AdminController(IDoctor client)
        {
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
    }
    }
