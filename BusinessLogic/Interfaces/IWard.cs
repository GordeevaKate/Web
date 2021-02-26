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
        void CreateOrUpdate(WardBindingModel model);
        void Delete(WardBindingModel model);
    }
}
