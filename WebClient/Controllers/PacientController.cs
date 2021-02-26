using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class PacientController : Controller
    {
        private readonly IPacient pacient;
        public PacientController(IPacient pacient)
        {
            this.pacient = pacient;
        }

		public IActionResult Pacient()
		{
			ViewBag.Guests = pacient.Read(null);
			return View();
		}
		public IActionResult CreatePacient()
		{
			return View();
		}
		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public ViewResult CreatePacient(CreatePacient Guest)
		{
			if (String.IsNullOrEmpty(Guest.FIO))
			{
				ModelState.AddModelError("", "Введите FIO");
				return View(Guest);
			}

			if (String.IsNullOrEmpty(Guest.Adress))
			{
				ModelState.AddModelError("", "Введите Adress");
				return View(Guest);
			}
			if (!Regex.IsMatch(Guest.Polis, @"^[1-9]{1}[0-9]{9}"))
			{
				ModelState.AddModelError("", "Заполните Polis правильно: 10 цифр");
				return View(Guest);

			}
			if (pacient.Read(new PacientBindingModel
			{
				Polis = Guest.Polis
            }).Count != 0)
            {
				ModelState.AddModelError("", "Такой полис уже есть");
				return View(Guest);
			}
			pacient.CreateOrUpdate(new PacientBindingModel
			{
				FIO=Guest.FIO,
				Adress=Guest.Adress,
				Polis= Guest.Polis
			});
			ModelState.AddModelError("", "Вы успешно зарегистрированы");
			return View("CreatePacient", Guest);
		}

	}
}
