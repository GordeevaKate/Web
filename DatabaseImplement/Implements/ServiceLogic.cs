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
    public class ServiceLogic : IService
    {
        public void CreateOrUpdate(ServiceBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Service element = model.Id.HasValue ? null : new Service();
                if (model.Id.HasValue)
                {
                    element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Service();
                    context.Services.Add(element);
                }
                element.Name = model.Name;
                element.Status = model.Status;
                element.Cena = model.Cena;
                context.SaveChanges();
            }
        }

        public void CreateOrUpdateDiagnosis(DiagnosisServiceBindingModel model)
        {
                using (var context = new KursachDatabase())
                {
                    var elem = context.DiagnosisServices.FirstOrDefault(rec =>
                     rec.ServiceId == model.ServiceId && rec.DiagnosisId == model.DiagnosisId);
                    if (elem == null)
                    {
                        context.DiagnosisServices.Add(new DiagnosisService
                        {
                            DiagnosisId = model.DiagnosisId,
                            ServiceId = model.ServiceId

                        });
                    }
                    else
                    {
                        elem.DiagnosisId = model.DiagnosisId;
                        elem.ServiceId = model.ServiceId;
                    }
                    Service room = context.Services.FirstOrDefault(rec =>
                    rec.Id == model.ServiceId);
                Diagnosis d = context.Diagnosiss.FirstOrDefault(rec =>
           rec.Id == model.DiagnosisId);

                context.SaveChanges();
                }
            }

        public void Delete(ServiceBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<ServiceViewModel> Read(ServiceBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Services
                 .Where(rec => model == null
                || (rec.Id == model.Id) || (model.Name == rec.Name && model.Status == rec.Status)||
                 (model.Name == null && model.Id==null &&model.Status == rec.Status))
               .Select(rec => new ServiceViewModel
               {
                   Id = (int)rec.Id,
                   Status = rec.Status,
                   Cena = rec.Cena,
                   Name = rec.Name
               })
                .ToList();
            }
        }

        public List<DiagnosisServiceViewModel> ReadDiagnosis(DiagnosisServiceBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.DiagnosisServices
                 .Where(rec => model == null
                || (rec.Id == model.Id) || (model.DiagnosisId == rec.DiagnosisId && model.ServiceId == 0)||(model.DiagnosisId == rec.DiagnosisId && model.ServiceId==rec.ServiceId ))
               .Select(rec => new DiagnosisServiceViewModel
               {
                   Id = rec.Id,
                   DiagnosisId=rec.DiagnosisId,
                   ServiceId=rec.ServiceId
               })
                .ToList();
            }
        }
    }
}
