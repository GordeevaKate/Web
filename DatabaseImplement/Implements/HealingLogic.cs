using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseImplement.Implements
{
    public class HealingLogic : IHealing
    {
        public void CreateOrUpdate(HealingBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void CreateOrUpdateService(HealingServiseBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(HealingBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<HealingViewModel> Read(HealingBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Healings
                 .Where(rec => model == null
                || (rec.Id == model.Id) || (model.DoctorId == rec.DoctorId && model.PacientId == model.PacientId)
                 )
               .Select(rec => new HealingViewModel
               {
                   Id = rec.Id,
                   DoctorId = rec.DoctorId,
                   Data = rec.Data,
                 DiagnosisId = rec.DiagnosisId,
                 PacientId=rec.PacientId,
                 Temperatura=rec.Temperatura

               })
                .ToList();
            }
        }

        public List<HealingServiseViewModel> ReadService(HealingServiseBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
