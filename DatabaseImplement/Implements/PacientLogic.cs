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
    public class PacientLogic : IPacient
    {
        public void CreateOrUpdate(PacientBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Pacient element = model.Id.HasValue ? null : new Pacient();
                if (model.Id.HasValue)
                {
                    element = context.Pacients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Pacient();
                    context.Pacients.Add(element);
                }
                element.FIO = model.FIO;
                element.Adress = model.Adress;
                element.Polis = model.Polis;
                context.SaveChanges();
            }
        }

        public void Delete(PacientBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<PacientViewModel> Read(PacientBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Pacients
                 .Where(rec => model == null
                || (rec.Id == model.Id)||
                rec.Polis==model.Polis)
               .Select(rec => new PacientViewModel
               {
                   Id = rec.Id,
                   FIO = rec.FIO,
                   Adress = rec.Adress,
                   Polis=rec.Polis
               })
                .ToList();
            }
        }
    }
}
