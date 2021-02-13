using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.BindingModel;
using BusinessLogic.ViewModel;
namespace BusinessLogic.Interfaces
{
    public interface IService
    {
        List<ServiceViewModel> Read(ServiceBindingModel model);
        void CreateOrUpdate(ServiceBindingModel model);
        void Delete(ServiceBindingModel model);
        List<DiagnosisServiceViewModel> ReadDiagnosis(DiagnosisServiceBindingModel model);
        void CreateOrUpdateDiagnosis(DiagnosisServiceBindingModel model);
    }
}
