// (C) Copyright 2019 by  
//
using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.PlottingServices;

// This line is not mandatory, but improves loading                                                                                                                                                                performances
[assembly: CommandClass(typeof(BatchPlotPdf.BatchPlotCommands))]

namespace BatchPlotPdf
{

    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class BatchPlotCommands
    {

        static PromptPointOptions useThisPointOption;
        static PromptPointResult useThisPointResult;
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

                    if (res.Status != PromptStatus.OK)
                        return;
                    SelectionSet sset = res.Value;
                    if (sset.Count == 0)
                        return;

                    foreach (SelectedObject obj in sset)
                    {
                        ed.WriteMessage("\nhas data");
                    }
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage(ex.Message + "\n" + ex.StackTrace);
                }
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
