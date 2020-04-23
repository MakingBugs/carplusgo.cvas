using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.BPM
{
    public class BPMFile
    {
        public string AccountID { get; set; }
        public string RequisitionID { get; set; }
        public string FormName { get; set; }
        public string NFileName { get; set; }
        public string OFileName { get; set; }
        public int FileSize { get; set; }
        public string FlowId { get; set; }
    }
}
