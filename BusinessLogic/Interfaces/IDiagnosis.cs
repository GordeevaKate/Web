using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
namespace BusinessLogic.Interfaces
{
    public interface IDiagnosis
    {
        List<DiagnosisViewModel> Read(DiagnosisBindingModel model);
        void CreateOrUpdate(DiagnosisBindingModel model);
        void Delete(DiagnosisBindingModel model);

    }
}
