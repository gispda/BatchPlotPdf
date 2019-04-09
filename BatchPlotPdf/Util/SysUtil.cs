using System;
using System.Collections.Generic;
using System.Text;

namespace BatchPlotPdf.Util
{
    class SysUtil
    {
        public static string getCfgPath()
        {
            string sPath = Environment.GetEnvironmentVariable("BATCHPLOTPDF");
            if (sPath == null || sPath == "")
                sPath = "F:\\project\\BatchPlotPdf\\BatchPlotPdf\\Util\\";
            return sPath;
        }
    }
}
