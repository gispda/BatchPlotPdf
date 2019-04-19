using System;
using System.Collections.Generic;
using System.Text;

namespace HomeDesignCad.Plot.Util
{
    class SysUtil
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();
        public static string getCfgPath()
        {
            string sPath = Environment.GetEnvironmentVariable("BATCHPLOTPDF");
            if (sPath == null || sPath == "")
                sPath = "F:\\project\\BatchPlotPdf\\BatchPlotPdf\\Util\\";
            return sPath;
        }
        public static void buildDict()
        {
            dict.Add("", "");
        }

        public static string getIPaperParams(string bkname)
        {
            if (dict.Count == 0)
                buildDict();

            return null;
        }
    }
}
