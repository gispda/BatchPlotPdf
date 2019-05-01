using System;
using System.Collections.Generic;
using System.Text;

namespace HomeDesignCad.Plot.Util
{
    class SysUtil
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();
        private static Dictionary<string, string> rdict = new Dictionary<string, string>();
        private static Dictionary<int, string> pdfdict = new Dictionary<int, string>();
        public static string getCfgPath()
        {
            string sPath = Environment.GetEnvironmentVariable("BATCHPLOTPDF");
            if (sPath == null || sPath == "")
                sPath = "F:\\project\\BatchPlotPdf\\BatchPlotPdf\\Util\\";
            return sPath;
        }
        public static void buildDict()
        {
            rdict.Add("A0", "ISO_full_bleed_A0_(841.00_x_1189.00_MM)");
            rdict.Add("A01", "UserDefinedMetric (841.00 x 1486.00毫米)");
            rdict.Add("A02", "UserDefinedMetric (841.00 x 1784.00毫米)");
            rdict.Add("A03", "UserDefinedMetric (841.00 x 2081.00毫米)");
            rdict.Add("A04", "UserDefinedMetric (841.00 x 2378.00毫米)");
          

            rdict.Add("A1", "ISO_full_bleed_Al_(841.00_x_594.00_MM)");
            rdict.Add("A11", "UserDefinedMetric (594.00 x 1051.00毫米)");
            rdict.Add("A12", "UserDefinedMetric (594.00 x 1262.00毫米)");
            rdict.Add("A13", "UserDefinedMetric (594.00 x 1472.00毫米)");
            rdict.Add("A14", "UserDefinedMetric (594.00 x 1682.00毫米)");

            rdict.Add("A2", "ISO_full_bleed_A2_(594.00_x_420.00_MM)");
            rdict.Add("A21", "UserDefinedMetric (420.00 x 743.00毫米)");
            rdict.Add("A22", "UserDefinedMetric (420.00 x 891.00毫米)");
            rdict.Add("A23", "UserDefinedMetric (420.00 x 1040.00毫米)");
            rdict.Add("A24", "UserDefinedMetric (420.00 x 1188.00毫米)");

            dict.Add("A0", "ISO_full_bleed_A0_(1189.00_x_841.00_MM)");
            dict.Add("A01", "UserDefinedMetric (1486.00 x 841.00毫米)");
            dict.Add("A02", "UserDefinedMetric (1784.00 x 841.00毫米)");
            dict.Add("A03", "UserDefinedMetric (2081.00 x 841.00毫米)");
            dict.Add("A04", "UserDefinedMetric (2378.00 x 841.00毫米)");

            dict.Add("A1", "ISO_full_bleed_Al_(594.00_x_841.00_MM)");
            dict.Add("A11", "UserDefinedMetric (1051.00 x 594.00毫米)");
            dict.Add("A12", "UserDefinedMetric (1262.00 x 594.00毫米)");
            dict.Add("A13", "UserDefinedMetric (1472.00 x 594.00毫米)");
            dict.Add("A14", "UserDefinedMetric (1682.00 x 594.00毫米)");
            dict.Add("A2", "ISO_full_bleed_A2_(420.00_x_594.00_MM)");
            dict.Add("A21", "UserDefinedMetric (743.00 x 420.00毫米)");
            dict.Add("A22", "UserDefinedMetric (891.00 x 420.00毫米)");
            dict.Add("A23", "UserDefinedMetric (1040.00 x 420.00毫米)");
            dict.Add("A24", "UserDefinedMetric (1188.00 x 420.00毫米)");

        }

        public static void addPdfDict(int idx, string pdfname)
        {
            if(pdfname!=null || idx<0)
            pdfdict.Add(idx, pdfname);
            else
                Log4NetHelper.WriteErrorLog("出错了"+pdfname+"\n");
         
        }
        public static string getpdfbyidx(int idx)
        {
            string pdfname = pdfdict[idx];
            if (pdfname != null)
                return pdfname;
            else
                return null;
        }
        
        public static int getidxbypdf(string pdfname)
        {
            int keyidx = -1;
            foreach (int key in pdfdict.Keys)
            {
                if (pdfdict[key].Equals(pdfname))
                {
                    //...... key
                    keyidx = key;
                }
            }
            return keyidx;
        }

        public static void clearPdfDict()
        {
            pdfdict.Clear();
        }

        public static bool getIPaperParams(string bkname,out string paperparams)
        {
            bool isfind = false;
            if (dict.Count == 0)
                buildDict();


            try
            {
              
                Log4NetHelper.WriteInfoLog("找到key:" + bkname + ":"+dict[bkname]+"\n");
                paperparams = dict[bkname];
                isfind = true;
               
            }
            catch (Exception ex)
            {

                Log4NetHelper.WriteInfoLog("没有找到key:" + bkname+"\n");
                paperparams = "ISO_full_bleed_A2_(594.00_x_420.00_MM)";
               
            }
            return isfind;
        }

        public static bool getIPaperParamsR(string bkname, out string paperparams)
        {
            bool isfind = false;
            if (rdict.Count == 0)
                buildDict();


            try
            {

                Log4NetHelper.WriteInfoLog("找到key:" + bkname + ":" + rdict[bkname] + "\n");
                paperparams = rdict[bkname];
                isfind = true;

            }
            catch (Exception ex)
            {

                Log4NetHelper.WriteInfoLog("没有找到key:" + bkname + "\n");
                paperparams = "ISO_full_bleed_A2_(594.00_x_420.00_MM)";

            }
            return isfind;
        }
    }
}
