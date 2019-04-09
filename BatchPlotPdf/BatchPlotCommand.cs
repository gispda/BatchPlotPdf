// (C) Copyright 2019 by  
//
using System;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;

using cad = Autodesk.AutoCAD.ApplicationServices.Application;
using Ap = Autodesk.AutoCAD.ApplicationServices;
using Db = Autodesk.AutoCAD.DatabaseServices;
using Ed = Autodesk.AutoCAD.EditorInput;
using Rt = Autodesk.AutoCAD.Runtime;
using Gm = Autodesk.AutoCAD.Geometry;
using Wn = Autodesk.AutoCAD.Windows;
using Hs = Autodesk.AutoCAD.DatabaseServices.HostApplicationServices;
using Us = Autodesk.AutoCAD.DatabaseServices.SymbolUtilityServices;
using Br = Autodesk.AutoCAD.BoundaryRepresentation;
using Pt = Autodesk.AutoCAD.PlottingServices;

using Autodesk.AutoCAD.PlottingServices;
using BatchPlotPdf.Util;

// This line is not mandatory, but improves loading                                                                                                                                                                performances
[assembly: CommandClass(typeof(BatchPlotPdf.BatchPlotCommands))]

namespace BatchPlotPdf
{
    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class BatchPlotCommands:IExtensionApplication
    {

        static PromptPointOptions useThisPointOption;
        static PromptPointResult useThisPointResult;


#if AUTOCAD_NEWER_THAN_2012
    const String acedTransOwner = "accore.dll";
#else
        const String acedTransOwner = "accore.dll";
#endif

#if AUTOCAD_NEWER_THAN_2014
    const String acedTrans_x86_Prefix = "_";
#else
        const String acedTrans_x86_Prefix = "";
#endif

        const String acedTransName = "acedTrans";

        [DllImport(acedTransOwner, CallingConvention = CallingConvention.Cdecl,
                EntryPoint = acedTrans_x86_Prefix + acedTransName)]
        static extern Int32 acedTrans_x86(Double[] point, IntPtr fromRb,
          IntPtr toRb, Int32 disp, Double[] result);

        [DllImport(acedTransOwner, CallingConvention = CallingConvention.Cdecl,
                EntryPoint = acedTransName)]
        static extern Int32 acedTrans_x64(Double[] point, IntPtr fromRb,
          IntPtr toRb, Int32 disp, Double[] result);

        public static Int32 acedTrans(Double[] point, IntPtr fromRb, IntPtr toRb,
          Int32 disp, Double[] result)
        {
            if (IntPtr.Size == 4)
                return acedTrans_x86(point, fromRb, toRb, disp, result);
            else
                return acedTrans_x64(point, fromRb, toRb, disp, result);
        }
        // Ф初始化函数（在加载插件时执行）.
        public void Initialize()
        {
            Log4NetHelper.InitLog4Net(SysUtil.getCfgPath() + "log4net.config");
        }

        // Ф加载插件时执行的函数.
        public void Terminate()
        {

        }
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an intance member then the enclosing class is 
        // intantiated for each document. If the member is a static member then
        // the enclosing class is NOT intantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.

        // Modal Command with localized name
        [CommandMethod("BPlotGroup", "Baplot", "BpCommandLocal", CommandFlags.Modal| CommandFlags.UsePickSet)]
        public void RunBatchPlot() // This method can have any name
        {
            //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            //PromptPointOptions ptopts = new PromptPointOptions("选择打印窗体区域左上角点");
            //ptopts.BasePoint = new Point3d(1, 1, 1);
            //ptopts.UseDashedLine = true;
            //ptopts.Message = "选择打印窗体区域左上角点";

            //ed.PromptingForPoint += new PromptPointOptionsEventHandler(handle_promptPointOptions);
            //ed.PromptedForPoint += new PromptPointResultEventHandler(handle_promptPointResult);
            //PromptPointResult ptRes = ed.GetPoint(ptopts);
            //ed.PromptingForPoint -= new PromptPointOptionsEventHandler(handle_promptPointOptions);
            //ed.PromptedForPoint -= new PromptPointResultEventHandler(handle_promptPointResult);


            //Point3d start = ptRes.Value;
            //if (ptRes.Status == PromptStatus.Cancel)
            //{
            //    ed.WriteMessage("将 (0,0,0) 作为打印窗体区域左上角点");
            //}

            //ptopts.Message = "选择打印窗体区域右下角";
            //ptRes = ed.GetPoint(ptopts);
            //Point3d end = ptRes.Value;
            //if (ptRes.Status == PromptStatus.Cancel)
            //{
            //    ed.WriteMessage("将 (0,0,0) 作为打印窗体区域右下角");
            //}



            plottoPdfUseWindows();
        }


        public void plottoPdfUseWindows()
        {
            Document doc =

           Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;

            Database db = doc.Database;


            Transaction tr =

              db.TransactionManager.StartTransaction();
            string blockName = "k1";
            TypedValue[] tvs = new TypedValue[] {new TypedValue(0, "INSERT"),new TypedValue(2, blockName)};
            SelectionFilter sf = new SelectionFilter(tvs);

            using (tr)
            {
                try
                {
                    PromptPointOptions ppo = new PromptPointOptions("\n\tSpecify a first corner: ");
                    PromptPointResult ppr = ed.GetPoint(ppo);
                    if (ppr.Status != PromptStatus.OK) return;
                    PromptCornerOptions pco = new PromptCornerOptions("\n\tOther corner: ", ppr.Value);
                    PromptPointResult pcr = ed.GetCorner(pco);
                    if (pcr.Status != PromptStatus.OK) return;
                    Point3d pt1 = ppr.Value;
                    Point3d pt2 = pcr.Value;
                    if (pt1.X == pt2.X || pt1.Y == pt2.Y)
                    {
                        ed.WriteMessage("\nInvalid point specification");
                        return;
                    }

                    PromptSelectionResult res;
                    res = ed.SelectWindow(pt1, pt2, sf);
                    Log4NetHelper.WriteInfoLog("pt1 pt2位置\n");
                    Log4NetHelper.WriteInfoLog(pt1 + "\n");
                    Log4NetHelper.WriteInfoLog(pt2 + "\n");
                    if (res.Status != PromptStatus.OK)
                        return;
                    SelectionSet sset = res.Value;
                    if (sset.Count == 0)
                        return;
                    BlockReference br = null;
                    foreach (SelectedObject obj in sset)
                    {
                       // ed.WriteMessage("\nhas data");
                        br = GetBlockReference(obj, tr);
                        //Log4NetHelper.WriteInfoLog(br.BlockName + "\n");
                        //Log4NetHelper.WriteInfoLog(br.Name+"\n");
                        //Log4NetHelper.WriteInfoLog(br.Bounds + "\n");
                        //Log4NetHelper.WriteInfoLog(br.Position + "\n");
                        //Log4NetHelper.WriteInfoLog(br.BlockTransform + "\n");
                        //Log4NetHelper.WriteInfoLog(br.BlockUnit + "\n");
                        //Log4NetHelper.WriteInfoLog("***********************\n");
                        //Log4NetHelper.WriteInfoLog(br.GeometricExtents.MinPoint + "\n");
                        //Log4NetHelper.WriteInfoLog("66666666666666666666666666\n");
                        //Log4NetHelper.WriteInfoLog(br.GeometricExtents.MaxPoint + "\n");
                        //Log4NetHelper.WriteInfoLog("***********************\n");
                        //Log4NetHelper.WriteInfoLog("-----------------------------\n");
                        PlotOnePaper(db, doc, br, "DWG To PDF.pc3",
                  "ISO_A4_(210.00_x_297.00_MM)", "111.pdf");


                    }
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage(ex.Message + "\n" + ex.StackTrace);
                }
            }
            
        }
        private BlockReference GetBlockReference(SelectedObject obj, Transaction tr)
        {
            BlockReference br=null;
            br = (BlockReference)tr.GetObject(obj.ObjectId, OpenMode.ForRead);

            return br;
        }

        static public int sortByX(Point3d a, Point3d b)
        {
            if (a.X == b.X)
                return a.Y.CompareTo(b.Y);
            return a.X.CompareTo(b.X);
        }
        // This code based on Kean Walmsley's article:
        // http://through-the-interface.typepad.com/through_the_interface/2007/10/plotting-a-wind.html
        public static void PlotOnePaper(Database db,Document doc,BlockReference br, String pcsFileName,
      String mediaName, String outputFileName)
        {

      ;

            if (pcsFileName == null)
                throw new ArgumentNullException("pcsFileName");
            if (pcsFileName.Trim() == String.Empty)
                throw new ArgumentException("pcsFileName.Trim() == String.Empty");

            if (mediaName == null)
                throw new ArgumentNullException("mediaName");
            if (mediaName.Trim() == String.Empty)
                throw new ArgumentException("mediaName.Trim() == String.Empty");

            if (outputFileName == null)
                throw new ArgumentNullException("outputFileName");
            if (outputFileName.Trim() == String.Empty)
                throw new ArgumentException("outputFileName.Trim() == String.Empty");

           

            Ed.Editor ed = doc.Editor;
            
            try
            {
               

                using (doc.LockDocument())
                {
                    using (Db.Transaction tr = db.TransactionManager.StartTransaction())
                    {


                       
                        Db.Extents3d extends = br.GeometricExtents;
                        Point3d pmin = extends.MinPoint;
                        Point3d pmax = extends.MaxPoint;




                        Db.ObjectId modelId = Us.GetBlockModelSpaceId(db);
                        Db.BlockTableRecord model = tr.GetObject(modelId,
                        Db.OpenMode.ForRead) as Db.BlockTableRecord;

                        Db.Layout layout = tr.GetObject(model.LayoutId,
                        Db.OpenMode.ForRead) as Db.Layout;

                        using (Pt.PlotInfo pi = new Pt.PlotInfo())
                        {
                            pi.Layout = model.LayoutId;

                            using (Db.PlotSettings ps = new Db.PlotSettings(layout.ModelType)
                              )
                            {

                                ps.CopyFrom(layout);

                                Db.PlotSettingsValidator psv = Db.PlotSettingsValidator
                                  .Current;

                             
                                Db.Extents2d extents = new Db.Extents2d(
                                    pmin.X*(1-0.0001),
                                    pmin.Y*(1+0.001),
                                    pmax.X,
                                    pmax.Y
                                  );

                                Log4NetHelper.WriteInfoLog("左上角坐标:" + pmin.X + "," + pmin.Y+"\n");
                                Log4NetHelper.WriteInfoLog("右下角角坐标:" + pmax.X + "," + pmax.Y + "\n");



                                psv.SetZoomToPaperOnUpdate(ps, true);

                                psv.SetPlotWindowArea(ps, extents);
                                psv.SetPlotType(ps, Db.PlotType.Window);
                                psv.SetUseStandardScale(ps, true);
                                psv.SetStdScaleType(ps, Db.StdScaleType.ScaleToFit);
                                psv.SetPlotCentered(ps, true);
                                psv.SetPlotRotation(ps, Db.PlotRotation.Degrees000);

                                // We'll use the standard DWF PC3, as
                                // for today we're just plotting to file
                                psv.SetPlotConfigurationName(ps, pcsFileName, mediaName);

                                // We need to link the PlotInfo to the
                                // PlotSettings and then validate it
                                pi.OverrideSettings = ps;
                                Pt.PlotInfoValidator piv = new Pt.PlotInfoValidator();
                                piv.MediaMatchingPolicy = Pt.MatchingPolicy.MatchEnabled;
                                piv.Validate(pi);

                                // A PlotEngine does the actual plotting
                                // (can also create one for Preview)
                                if (Pt.PlotFactory.ProcessPlotState == Pt.ProcessPlotState
                                  .NotPlotting)
                                {
                                    using (Pt.PlotEngine pe = Pt.PlotFactory.CreatePublishEngine()
                                      )
                                    {
                                        // Create a Progress Dialog to provide info
                                        // and allow thej user to cancel

                                        using (Pt.PlotProgressDialog ppd =
                                          new Pt.PlotProgressDialog(false, 1, true))
                                        {
                                            ppd.set_PlotMsgString(
                                            Pt.PlotMessageIndex.DialogTitle, "Custom Plot Progress");

                                            ppd.set_PlotMsgString(
                                              Pt.PlotMessageIndex.CancelJobButtonMessage,
                                              "Cancel Job");

                                            ppd.set_PlotMsgString(
                                            Pt.PlotMessageIndex.CancelSheetButtonMessage,
                                            "Cancel Sheet");

                                            ppd.set_PlotMsgString(
                                            Pt.PlotMessageIndex.SheetSetProgressCaption,
                                            "Sheet Set Progress");

                                            ppd.set_PlotMsgString(
                                              Pt.PlotMessageIndex.SheetProgressCaption,
                                             "Sheet Progress");

                                            ppd.LowerPlotProgressRange = 0;
                                            ppd.UpperPlotProgressRange = 100;
                                            ppd.PlotProgressPos = 0;

                                            // Let's start the plot, at last
                                            ppd.OnBeginPlot();
                                            ppd.IsVisible = true;
                                            pe.BeginPlot(ppd, null);

                                            // We'll be plotting a single document
                                            pe.BeginDocument(pi, doc.Name, null, 1, true,
                                                // Let's plot to file
                                             outputFileName);
                                            // Which contains a single sheet
                                            ppd.OnBeginSheet();
                                            ppd.LowerSheetProgressRange = 0;
                                            ppd.UpperSheetProgressRange = 100;
                                            ppd.SheetProgressPos = 0;
                                            Pt.PlotPageInfo ppi = new Pt.PlotPageInfo();
                                            pe.BeginPage(ppi, pi, true, null);
                                            pe.BeginGenerateGraphics(null);
                                            pe.EndGenerateGraphics(null);

                                            // Finish the sheet
                                            pe.EndPage(null);
                                            ppd.SheetProgressPos = 100;
                                            ppd.OnEndSheet();

                                            // Finish the document
                                            pe.EndDocument(null);

                                            // And finish the plot
                                            ppd.PlotProgressPos = 100;
                                            ppd.OnEndPlot();
                                            pe.EndPlot(null);
                                        }
                                    }
                                }
                                else
                                {
                                    ed.WriteMessage("\nAnother plot is in progress.");
                                }
                            }
                        }
                        tr.Commit();
                    }
                }
            }
            finally
            {
              //  Hs.WorkingDatabase = previewDb;
            }
        }
        private static void handle_promptPointOptions(object sender, PromptPointOptionsEventArgs e)
        {
            useThisPointOption = e.Options;

        }
        private static void handle_promptPointResult(object sender, PromptPointResultEventArgs e)
        {
            useThisPointResult = e.Result;

        }  

        public void plottoPdf()
        {
            Document doc =

          Application.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;

            Database db = doc.Database;


            Transaction tr =

              db.TransactionManager.StartTransaction();

            using (tr)
            {

                // We'll be plotting the current layout


                BlockTableRecord btr =

                  (BlockTableRecord)tr.GetObject(

                    db.CurrentSpaceId,

                    OpenMode.ForRead

                  );

                Layout lo =

                  (Layout)tr.GetObject(

                    btr.LayoutId,

                    OpenMode.ForRead

                  );


                // We need a PlotInfo object

                // linked to the layout


                PlotInfo pi = new PlotInfo();

                pi.Layout = btr.LayoutId;


                // We need a PlotSettings object

                // based on the layout settings

                // which we then customize


                PlotSettings ps =

                  new PlotSettings(lo.ModelType);

                ps.CopyFrom(lo);


                // The PlotSettingsValidator helps

                // create a valid PlotSettings object


                PlotSettingsValidator psv =

                  PlotSettingsValidator.Current;


                // We'll plot the extents, centered and

                // scaled to fit


                psv.SetPlotType(

                  ps,

                  Autodesk.AutoCAD.DatabaseServices.PlotType.Extents

                );

                psv.SetUseStandardScale(ps, true);

                psv.SetStdScaleType(ps, StdScaleType.ScaleToFit);

                psv.SetPlotCentered(ps, true);


                // We'll use the standard DWF PC3, as

                // for today we're just plotting to file


                psv.SetPlotConfigurationName(

                  ps,

                  "DWG To PDF.pc3",

                  "ANSI_A_(8.50_x_11.00_Inches)"

                );


                // We need to link the PlotInfo to the

                // PlotSettings and then validate it


                pi.OverrideSettings = ps;

                PlotInfoValidator piv =

                  new PlotInfoValidator();

                piv.MediaMatchingPolicy =

                  MatchingPolicy.MatchEnabled;

                piv.Validate(pi);


                // A PlotEngine does the actual plotting

                // (can also create one for Preview)


                if (PlotFactory.ProcessPlotState ==

                    ProcessPlotState.NotPlotting)
                {

                    PlotEngine pe =

                      PlotFactory.CreatePublishEngine();

                    using (pe)
                    {

                        // Create a Progress Dialog to provide info

                        // and allow thej user to cancel


                        PlotProgressDialog ppd =

                          new PlotProgressDialog(false, 1, true);

                        using (ppd)
                        {

                            ppd.set_PlotMsgString(

                              PlotMessageIndex.DialogTitle,

                              "Custom Plot Progress"

                            );

                            ppd.set_PlotMsgString(

                              PlotMessageIndex.CancelJobButtonMessage,

                              "Cancel Job"

                            );

                            ppd.set_PlotMsgString(

                              PlotMessageIndex.CancelSheetButtonMessage,

                              "Cancel Sheet"

                            );

                            ppd.set_PlotMsgString(

                              PlotMessageIndex.SheetSetProgressCaption,

                              "Sheet Set Progress"

                            );

                            ppd.set_PlotMsgString(

                              PlotMessageIndex.SheetProgressCaption,

                              "Sheet Progress"

                            );

                            ppd.LowerPlotProgressRange = 0;

                            ppd.UpperPlotProgressRange = 100;

                            ppd.PlotProgressPos = 0;


                            // Let's start the plot, at last


                            ppd.OnBeginPlot();

                            ppd.IsVisible = true;

                            pe.BeginPlot(ppd, null);


                            // We'll be plotting a single document


                            pe.BeginDocument(

                              pi,

                              doc.Name,

                              null,

                              1,

                              true, // Let's plot to file

                              "c:\\test-output"

                            );


                            // Which contains a single sheet


                            ppd.OnBeginSheet();


                            ppd.LowerSheetProgressRange = 0;

                            ppd.UpperSheetProgressRange = 100;

                            ppd.SheetProgressPos = 0;


                            PlotPageInfo ppi = new PlotPageInfo();

                            pe.BeginPage(

                              ppi,

                              pi,

                              true,

                              null

                            );

                            pe.BeginGenerateGraphics(null);

                            pe.EndGenerateGraphics(null);


                            // Finish the sheet

                            pe.EndPage(null);

                            ppd.SheetProgressPos = 100;

                            ppd.OnEndSheet();


                            // Finish the document


                            pe.EndDocument(null);


                            // And finish the plot


                            ppd.PlotProgressPos = 100;

                            ppd.OnEndPlot();

                            pe.EndPlot(null);

                        }

                    }

                }

                else
                {

                    ed.WriteMessage(

                      "\nAnother plot is in progress."

                    );

                }

            }
        }
        

        // Modal Command with pickfirst selection
        [CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal | CommandFlags.UsePickSet)]
        public void MyPickFirst() // This method can have any name
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            PromptPointOptions ptopts = new PromptPointOptions("选择打印窗体区域左上角点");
            ptopts.BasePoint = new Point3d(1, 1, 1);
            ptopts.UseDashedLine = true;
            ptopts.Message = "选择打印窗体区域左上角点";

            ed.PromptingForPoint += new PromptPointOptionsEventHandler(handle_promptPointOptions);
            ed.PromptedForPoint += new PromptPointResultEventHandler(handle_promptPointResult);
             
            PromptPointResult ptRes = ed.GetPoint(ptopts);
            ed.PromptingForPoint -= new PromptPointOptionsEventHandler(handle_promptPointOptions);
            ed.PromptedForPoint -= new PromptPointResultEventHandler(handle_promptPointResult);


            Point3d start = ptRes.Value;
            if (ptRes.Status == PromptStatus.Cancel)
            {
                ed.WriteMessage("将 (0,0,0) 作为打印窗体区域左上角点");
            }

            ptopts.Message = "选择打印窗体区域右下角";
            ptRes = ed.GetPoint(ptopts);
            Point3d end = ptRes.Value;
            if (ptRes.Status == PromptStatus.Cancel)
            {
                ed.WriteMessage("将 (0,0,0) 作为打印窗体区域右下角");
            }

        }

        // Application Session Command with localized name
        [CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal | CommandFlags.Session)]
        public void MySessionCmd() // This method can have any name
        {
            // Put your command code here
        }

        // LispFunction is similar to CommandMethod but it creates a lisp 
        // callable function. Many return types are supported not just string
        // or integer.
        [LispFunction("MyLispFunction", "MyLispFunctionLocal")]
        public int MyLispFunction(ResultBuffer args) // This method can have any name
        {
            // Put your command code here

            // Return a value to the AutoCAD Lisp Interpreter
            return 1;
        }

    }

}
