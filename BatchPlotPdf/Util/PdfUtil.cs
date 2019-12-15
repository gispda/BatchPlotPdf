using System;
using System.Collections.Generic;
using System.Text;
using IniParser;
using IniParser.Model;


namespace HomeDesignCad.Plot.Util
{
    class PdfUtil
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>();
        private static Dictionary<string, string> rdict = new Dictionary<string, string>();

        private static Dictionary<string, string> pdfattribdict = new Dictionary<string, string>();
        private static Dictionary<int, string> pdfdict = new Dictionary<int, string>();

        private static IniData parsedData;
        private static FileIniDataParser fileIniData;
        public static string getCfgPath()
        {
            string sPath = Environment.GetEnvironmentVariable("BATCHPLOTPDF");
            if (sPath == null || sPath == "")
                sPath = "C:\\Program Files (x86)\\BatchPlotPdf";
            return sPath;
        }
        public static void Init()
        {
            if (dict.Count > 0 && rdict.Count >0)
                return;

            dict.Add("A0", "UserDefinedMetric (1189.00 x 841.00毫米)");
            dict.Add("A01", "UserDefinedMetric (1486.00 x 841.00毫米)");
            dict.Add("A02", "UserDefinedMetric (1784.00 x 841.00毫米)");
            dict.Add("A03", "UserDefinedMetric (2081.00 x 841.00毫米)");
            dict.Add("A04", "UserDefinedMetric (2378.00 x 841.00毫米)");

            dict.Add("A1", "ISO_full_bleed_A1_(841.00_x_594.00_MM)");
            dict.Add("A11", "UserDefinedMetric (1051.00 x 594.00毫米)");
            dict.Add("A12", "UserDefinedMetric (1262.00 x 594.00毫米)");
            dict.Add("A13", "UserDefinedMetric (1472.00 x 594.00毫米)");
            dict.Add("A14", "UserDefinedMetric (1682.00 x 594.00毫米)");
            dict.Add("A2", "ISO_full_bleed_A2_(594.00_x_420.00_MM)");
            dict.Add("A21", "UserDefinedMetric (743.00 x 420.00毫米)");
            dict.Add("A22", "UserDefinedMetric (891.00 x 420.00毫米)");
            dict.Add("A23", "UserDefinedMetric (1040.00 x 420.00毫米)");
            dict.Add("A24", "UserDefinedMetric (1188.00 x 420.00毫米)");



            rdict.Add("A0", "ISO_A0_(841.00_x_1189.00_MM)");
            rdict.Add("A01", "UserDefinedMetric (841.00 x 1486.00毫米)");
            rdict.Add("A02", "UserDefinedMetric (841.00 x 1784.00毫米)");
            rdict.Add("A03", "UserDefinedMetric (841.00 x 2081.00毫米)");
            rdict.Add("A04", "UserDefinedMetric (841.00 x 2378.00毫米)");

            rdict.Add("A1", "ISO_full_bleed_A1_(594.00_x_841.00_MM)");
            
            rdict.Add("A11", "UserDefinedMetric (594.00 x 1051.00毫米)");
            rdict.Add("A12", "UserDefinedMetric (594.00 x 1262.00毫米)");
            rdict.Add("A13", "UserDefinedMetric (594.00 x 1472.00毫米)");
            rdict.Add("A14", "UserDefinedMetric (594.00 x 1682.00毫米)");
            rdict.Add("A2", "ISO_full_bleed_A2_(420.00_x_594.00_MM)");   
            rdict.Add("A21", "UserDefinedMetric (420.00 x 743.00毫米)");
            rdict.Add("A22", "UserDefinedMetric (420.00 x 891.00毫米)");
            rdict.Add("A23", "UserDefinedMetric (420.00 x 1040.00毫米)");
            rdict.Add("A24", "UserDefinedMetric (420.00 x 1188.00毫米)");

            fileIniData = new FileIniDataParser();
            fileIniData.Parser.Configuration.CommentString = "#";

            string ddd = System.Environment.CurrentDirectory;
            string eee = System.IO.Directory.GetCurrentDirectory();

            try
            {
                parsedData = fileIniData.ReadFile(getCfgPath()+"\\BatchPlotPdf.ini");
            }
            catch (Exception)
            {
                
                PdfUtil.setXs(50);
                PdfUtil.setYs(100);
                PdfUtil.setRxs(300);
                PdfUtil.setRys(400);
                PdfUtil.setSmaxy(0.1);
                PdfUtil.setSmaxx(0.02);
            }
            
        }

        public static void addPdfAttribDict(string fnode, string drawname)
        {
            if (drawname != null || fnode != null)
                pdfattribdict.Add(fnode, drawname);
            else
                Log4NetHelper.WriteErrorLog("出错了" + fnode + "\n");

        }

        public static void addPdfDict(int idx, string pdfname)
        {
            if(pdfname!=null || idx<0)
            pdfdict.Add(idx, pdfname);
            else
                Log4NetHelper.WriteErrorLog("出错了"+pdfname+"\n");
         
        }
        public static void writeIniData()
        {

            fileIniData.WriteFile("BatchPlotPdf.ini", parsedData, Encoding.UTF8);
        }
        public static void setXs(int xs)
        {
            string sxs = Convert.ToString(xs);

            parsedData["Plot"]["Xs"]=sxs;
        }
        public static void setYs(int ys)
        {
            string sys = Convert.ToString(ys);

            parsedData["Plot"]["Ys"] = sys;
        }
        public static void setRxs(int xs)
        {
            string sxs = Convert.ToString(xs);

            parsedData["Plot"]["Rxs"] = sxs;
        }
        public static void setRys(int ys)
        {
            string sys = Convert.ToString(ys);

            parsedData["Plot"]["Rys"] = sys;
        }
        public static void setSmaxy(double smaxy)
        {
            string sys = Convert.ToString(smaxy);

            parsedData["Plot"]["Smaxy"] = sys;
        }
        public static void setSmaxx(double smaxx)
        {
            string sxs = Convert.ToString(smaxx);

            parsedData["Plot"]["Smaxx"] = sxs;
        }
        public static int getXs()
        {
            string sxs = parsedData["Plot"]["Xs"];
            return Convert.ToInt32(sxs);
        }
        public static int getYs()
        {
            string sys = parsedData["Plot"]["Ys"];
            return Convert.ToInt32(sys);
        }
        public static int getRxs()
        {
            string srxs = parsedData["Plot"]["Rxs"];
            return Convert.ToInt32(srxs);
        }
        public static int getRys()
        {
            string srys = parsedData["Plot"]["Rys"];
            return Convert.ToInt32(srys);
        }
        public static double getSmaxy()
        {
            string sys = parsedData["Plot"]["Smaxy"];
            return Convert.ToDouble(sys);
        }
        public static double getSmaxx()
        {
            string sxs = parsedData["Plot"]["Smaxx"];
            return Convert.ToDouble(sxs);
        }

        public static void setEngineering(string eng)
        {
            parsedData["Data"]["Engineering"]=eng;
        }

        public static void setPlotDir(string pdir)
        {
            parsedData["Data"]["PlotDir"] = pdir;
        }
        public static string getPlotDir()
        {
            return parsedData["Data"]["PlotDir"];
        }

        public static void setProject(string proj)
        {
            parsedData["Data"]["Project"] = proj;
        }
        public static string getEngineering()
        {
            return parsedData["Data"]["Engineering"];
        }


        public static string getProject()
        {
            return parsedData["Data"]["Project"];
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
                if (pdfdict[key].Contains(pdfname))
                {
                    //...... key
                    keyidx = key;
                }
            }
            return keyidx;
        }

        public static string getDrawingname(string pdfname)
        {
            return pdfattribdict[pdfname];
        }


        public static void clearPdfDict()
        {
            pdfdict.Clear();
            pdfattribdict.Clear();
        }

        public static bool getIPaperParams(string bkname,out string paperparams)
        {
            bool isfind = false;
         
                Init();


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
            
                Init();


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
