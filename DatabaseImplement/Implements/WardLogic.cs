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

    }
}
