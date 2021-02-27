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
    public class WardLogic : IWard
    {
        public void CreateOrUpdate(WardBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Ward elem = model.Id.HasValue ? null : new Ward();
                if (model.Id.HasValue)
                {
                    elem = context.Wards.FirstOrDefault(rec => rec.Id ==
                       model.Id);
                    if (elem == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    elem = new Ward();
                    context.Wards.Add(elem);
                }
                elem.Number = model.Number;
                elem.Mesto = model.Mesto;
                context.SaveChanges();
            }
        }
        public void CreateOrUpdate(WardPacientBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                WardPacient elem = model.Id.HasValue ? null : new WardPacient();
              
                    elem = context.WardPacients.FirstOrDefault(rec => rec.PacientId ==
                       model.PacientId && rec.WardId==model.WardId);
                    if (elem == null)
                    {
                    elem = new WardPacient();
                    context.WardPacients.Add(elem);
                }
                elem.PacientId = model.PacientId;
                elem.WardId = model.WardId;
                context.SaveChanges();
            }
        }


        public void Delete(WardBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<WardViewModel> Read(WardBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Wards
                 .Where(rec => model == null
                || (rec.Id == model.Id) || rec.Number==model.Number)
               .Select(rec => new WardViewModel
               {
                   Id = rec.Id,
                   Mesto = rec.Mesto,
                   Number = rec.Number
               })
                .ToList();
            }
        }


        public List<WardPacientViewModel> ReadPacient(WardPacientBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.WardPacients
                 .Where(rec => model == null
                || (rec.Id == model.Id) || rec.PacientId == model.PacientId ||rec.WardId==model.WardId)
               .Select(rec => new WardPacientViewModel
               {
                   Id = rec.Id,
                   PacientId = rec.PacientId,
                   WardId = rec.WardId
               })
                .ToList();
            }
        }
    }
}
