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
    public class DiagnosisLogic : IDiagnosis
    {
        public void CreateOrUpdate(DiagnosisBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Diagnosis element = model.Id.HasValue ? null : new Diagnosis();
                if (model.Id.HasValue)
                {
                    element = context.Diagnosiss.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Diagnosis();
                    context.Diagnosiss.Add(element);
                }
                element.Name = model.Name;
                context.SaveChanges();
            }
        }

        public void Delete(DiagnosisBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<DiagnosisViewModel> Read(DiagnosisBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Diagnosiss
                 .Where(rec => model == null
                || (rec.Id == model.Id) || model.Name == rec.Name )
               .Select(rec => new DiagnosisViewModel
               {
                   Id = rec.Id,
                   Name = rec.Name
               })
                .ToList();
            }
        }
    }
}
