using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
namespace BusinessLogic.Interfaces
{
    public interface IPacient
    {
        List<PacientViewModel> Read(PacientBindingModel model);
        void CreateOrUpdate(PacientBindingModel model);
        void Delete(PacientBindingModel model);

    }
}
