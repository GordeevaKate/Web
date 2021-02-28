using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Report
{
 public   class PdfInfo
    {
        public List<ReportLogic.counts> counts { get; set; }
        public List<PacientViewModel> pacients { get; set; }

        public List<string> Colon { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public ServiceViewModel service { get; set; }
        public PacientViewModel pacient { get; set; }
        public List<ServiceViewModel> services { get; set; }
                 public List<DiagnosisServiceViewModel> ds { get; set; }
        public List<DiagnosisViewModel> diagnosis { get; set; }
        public List<HealingViewModel> heals { get; set; }

    }
}
