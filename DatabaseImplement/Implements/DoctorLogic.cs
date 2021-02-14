using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using DatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseImplement.Implements
{
    public class DoctorLogic : IDoctor
    {
        public void CreateOrUpdate(DoctorBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Doctor element = model.Id.HasValue ? null : new Doctor();
                if (model.Id.HasValue)
                {
                    element = context.Doctors.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Doctor();
                    context.Doctors.Add(element);
                }
                element.Login = model.Login;
                element.Status = model.Status;
                element.Password = model.Password;
                context.SaveChanges();
            }
        }

        public List<DoctorViewModel> Read(DoctorBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Doctors
                 .Where(rec => model == null
                || (rec.Id == model.Id) ||model.Login==rec.Login||(model.Login == rec.Login && model.Password==model.Password)
                   || (model.Status == rec.Status))
               .Select(rec => new DoctorViewModel
               {
                   Id = rec.Id,
                   Status = rec.Status,
                   Login = rec.Login,
                   Password = rec.Password
               })
                .ToList();
            }
        }
    }
}
