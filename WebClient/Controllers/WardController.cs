using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class WardController : Controller
    {
    private readonly    IWard ward;
        public WardController(IWard ward)
        {
            this.ward = ward;
        }
        public IActionResult Ward(string str)
        {
            if (str != null)
            {
                ModelState.AddModelError("", str);
            }
            ViewBag.Rooms = ward.Read(null);
            return View();
        }
        public IActionResult CreateWard()
        {
            return View();
        }
		[HttpPost]
		public ActionResult CreateWard(CreateWard model)
		{
			if (model.Mesto <= 0 ||
		   model.Mesto > 5)
			{
				ModelState.AddModelError("", $"Вместимость должна быть от {1} до {5} символов");
				return View(model);
			}
			if (model.Number_Room <= 0)
			{
				ModelState.AddModelError("", $"Номер должен быть от {1}");
				return View(model);
			}
			var existClient = ward.Read(new WardBindingModel
			{
				Number = model.Number_Room,
			}).FirstOrDefault();
			if (existClient != null)
			{
				ModelState.AddModelError("", "Уже есть такой номер у сотрудника");
				return View(model);
			}
			
			ward.CreateOrUpdate(new WardBindingModel
			{
				
				Mesto = model.Mesto,
				Number = model.Number_Room
			});
			ModelState.AddModelError("", "Вы успешно добавили номер");
			return View("CreateWard", model);
		}
	}
}
