using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
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
		private readonly IWard ward;
        public PacientController(IWard ward,IPacient pacient)
        {
            this.pacient = pacient;
			this.ward = ward;
        }

		public IActionResult Pacient()
		{
			ViewBag.Guests = pacient.Read(null);
			return View();
		}
		public IActionResult WardPacient(int id)
		{
			ViewBag.Id = id;
			ViewBag.Number = ward.Read(new WardBindingModel { Id = id })[0].Number ;
			var guest =new List<PacientViewModel> { };
			var pacients = ward.ReadPacient(new WardPacientBindingModel { WardId=id});
			foreach(var p in pacients)
            {
				guest.Add(pacient.Read(new PacientBindingModel {Id=p.PacientId })[0]);
            }
			ViewBag.Guests = guest;
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
