using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmeterLabelPrintService
{
    public class Op_MeterPrint
    {
        public string RowPointer { get; set; }
        public string MeterNo { get; set; }
        public string MeterName { get; set; }
        public decimal MeterRate { get; set; }
        public int MeterDigit { get; set; }
        public string Remark { get; set; }
        public string PrintFormat { get; set; }
        public bool Status { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
