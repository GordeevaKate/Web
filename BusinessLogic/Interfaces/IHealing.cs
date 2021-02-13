using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
namespace BusinessLogic.Interfaces
{
    public interface IHealing
    {
        List<HealingViewModel> Read(HealingBindingModel model);
        void CreateOrUpdate(HealingBindingModel model);
        void Delete(HealingBindingModel model);
        List<HealingServiseViewModel> ReadService(HealingServiseBindingModel model);
        void CreateOrUpdateService(HealingServiseBindingModel model);

    }
}
