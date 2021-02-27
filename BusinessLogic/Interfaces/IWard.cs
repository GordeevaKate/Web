using System;
using System.Collections.Generic;
using System.Text;

using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
namespace BusinessLogic.Interfaces
{
    public interface IWard
    {
        List<WardViewModel> Read(WardBindingModel model);
               List<WardPacientViewModel> ReadPacient(WardPacientBindingModel model);
        void CreateOrUpdate(WardBindingModel model);
        void CreateOrUpdate(WardPacientBindingModel model);
        void Delete(WardBindingModel model);
    }
}
