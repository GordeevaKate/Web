using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
namespace BusinessLogic.Interfaces
{
    public interface IDoctor
    {
        List<DoctorViewModel> Read(DoctorBindingModel model);
        void CreateOrUpdate(DoctorBindingModel model);

    }
}
