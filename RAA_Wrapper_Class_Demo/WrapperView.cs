using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Wrapper_Class_Demo
{
    internal class WrapperView
    {
        public View ViewObject { get; set; }
        public string ViewName { get; set; }
        public string ViewTypeName { get; set; }
        public string ViewType { get; set; }
        public bool IsViewOnSheet { get; set; }
        public bool IsViewOnSheetNoPlot { get; set; }
        public string ViewSheetName { get; set; }
        public string ViewSheetNum { get; set; }
        public string TitleOnSheet { get; set; }

        public WrapperView(View view)
        {
            ViewObject = view;
            ViewName = view.Name;
            ViewType = view.ViewType.ToString();
            ViewTypeName = ViewType + " | " + ViewName;

            TitleOnSheet = view.LookupParameter("Title on Sheet")?.AsString();

            isViewOnSheet();
        }

        private void isViewOnSheet()
        {
            FilteredElementCollector collector = new FilteredElementCollector(ViewObject.Document);
            collector.OfClass(typeof(ViewSheet));
            collector.WhereElementIsNotElementType();
            
            if (collector.Count() > 0)
            {
                var firstSheetId = collector.FirstOrDefault().Id;
                // check is view on sheet
                if (Viewport.CanAddViewToSheet(ViewObject.Document, firstSheetId, ViewObject.Id))
                {
                    IsViewOnSheet = false;
                    IsViewOnSheetNoPlot = false;
                    ViewSheetName = "";
                    ViewSheetNum = "";
                }
                else
                {
                    IsViewOnSheet = true;

                    // get view's sheet
                    FilteredElementCollector vpCollector = new FilteredElementCollector(ViewObject.Document);
                    vpCollector.OfClass(typeof(Viewport));
                    vpCollector.WhereElementIsNotElementType();

                    foreach (Viewport curVP in vpCollector)
                    {
                        if (curVP.ViewId == ViewObject.Id)
                        {
                            // get sheet ID and sheet
                            ViewSheet curSheet = ViewObject.Document.GetElement(curVP.SheetId) as ViewSheet;
                            ViewSheetName = curSheet.Name;
                            ViewSheetNum = curSheet.SheetNumber;

                            break;
                        }
                    }
                }
            }
        }

        public bool SetViewPhase(Phase curPhase)
        {
            try
            {
                Parameter curParam = ViewObject.get_Parameter(BuiltInParameter.VIEW_PHASE);
                curParam.Set(curPhase.Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetViewPhaseFilter(PhaseFilter curPhaseFilter)
        {
            try
            {
                Parameter curParam = ViewObject.get_Parameter(BuiltInParameter.VIEW_PHASE_FILTER);
                curParam.Set(curPhaseFilter.Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
