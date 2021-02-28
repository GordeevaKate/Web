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
    public class HealingLogic : IHealing
    {
        public void CreateOrUpdate(HealingBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                Healing element = model.Id.HasValue ? null : new Healing();
                if (model.Id.HasValue)
                {
                    element = context.Healings.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Healing();
                    context.Healings.Add(element);
                }
                element.Data = model.Data;
                element.DiagnosisId = model.DiagnosisId;
                element.DoctorId = model.DoctorId;
                element.PacientId = model.PacientId;
                element.Temperatura = model.Temperatura;
                element.WardId = model.WardId;
                context.SaveChanges();
            }
        }

        public void CreateOrUpdateService(HealingServiseBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                HealingServise elem = model.Id.HasValue ? null : new HealingServise();
                if (model.Id.HasValue)
                {
                    elem = context.HealingServises.FirstOrDefault(rec => (
                    (rec.HealingId ==model.HealingId && rec.ServiseId == model.ServiseId) ||( rec.Id == model.Id)) );
                    if (elem == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    elem = new HealingServise();
                    context.HealingServises.Add(elem);
                    elem.ServiseId = model.ServiseId;
                    elem.HealingId = model.HealingId;
                }
                elem.Status = model.Status;
                context.SaveChanges();
            }
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
                || (rec.Id == model.Id)|| (model.WardId == rec.WardId && 0 == model.DiagnosisId)
                || ((model.Data!=null) && (rec.Data>=model.Data && rec.Data<=model.Data.AddYears(1).AddDays(-1)) )
                ||(model.DoctorId == rec.DoctorId && model.PacientId == model.PacientId&& model.WardId==0)||
                (model.DoctorId == rec.DoctorId && model.PacientId == model.PacientId &&
                model.WardId == rec.WardId && rec.DiagnosisId==model.DiagnosisId)
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
            using (var context = new KursachDatabase())
            {
                return context.HealingServises
                 .Where(rec => model == null
                || (rec.Id == model.Id) || model.HealingId==rec.HealingId
             )
               .Select(rec => new HealingServiseViewModel
               {
                   Id = rec.Id,
                   HealingId = rec.HealingId,
                   ServiseId = rec.ServiseId,
                   Status = rec.Status
               })
                .ToList();
            }
        }
    }
}
