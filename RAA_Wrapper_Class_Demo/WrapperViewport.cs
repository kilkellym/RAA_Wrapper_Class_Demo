using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Wrapper_Class_Demo
{
    internal class WrapperViewport
    {
        public string VpViewName { get; set; }
        public string VpViewNum { get; set; }
        public string VpSheetNum { get; set; }
        public string VpSheetView { get; set; }
        public Viewport VpObject { get; set; }
        public XYZ BoxCenter { get; set; }
        public Outline BoxOutline { get; set; }
        public ElementId VpTypeId { get; set; }

        public WrapperViewport(Viewport _vpObj)
        {
            VpObject = _vpObj;

            // get view name
            View vpView = VpObject.Document.GetElement(VpObject.ViewId) as View;
            VpViewName = vpView.Name;

            // get sheet number
            ViewSheet vpSheet = VpObject.Document.GetElement(VpObject.SheetId) as ViewSheet;
            VpSheetNum = vpSheet.SheetNumber;

            // get view number
            VpViewNum = VpObject.get_Parameter(BuiltInParameter.VIEWPORT_DETAIL_NUMBER).AsString();

            // get sheet num : view name
            VpSheetView = VpSheetNum + ", " + VpViewName;

            // get box center
            BoxCenter = VpObject.GetBoxCenter();

            // get box outline 
            BoxOutline = VpObject.GetBoxOutline();

            // get viewport view marker
            VpTypeId = VpObject.GetTypeId();
        }

        public View GetView()
        {
            View returnView = VpObject.Document.GetElement(VpObject.ViewId) as View;
            return returnView;
        }

    }
}
