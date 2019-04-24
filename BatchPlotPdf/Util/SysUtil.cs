using System;
using System.Collections.Generic;
using System.Text;

namespace HomeDesignCad.Plot.Util
{
    class SysUtil
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();
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
            dict.Add("A0", "ISO_full_bleed_A0_(841.00_x_1189.00_MM)");
            dict.Add("A1", "ISO_full_bleed_Al_(841.00_x_594.00_MM)");
            dict.Add("A2", "ISO_full_bleed_A2_(594.00_x_420.00_MM)");

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

        public static string getIPaperParams(string bkname)
        {
            if (dict.Count == 0)
                buildDict();


            try
            {
                Log4NetHelper.WriteInfoLog("找到key:" + bkname + ":"+dict[bkname]+"\n");
                return dict[bkname];
            }
            catch (Exception ex)
            {

                Log4NetHelper.WriteInfoLog("没有找到key:" + bkname+"\n");
                return "ISO_full_bleed_A2_(594.00_x_420.00_MM)";
            }
        }
    }
}
