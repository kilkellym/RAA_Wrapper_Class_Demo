using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Wrapper_Class_Demo
{
    internal class WrapperViewSheet
    {
        public ViewSheet SheetElement { get; set; }
        public string SheetNumber { get; set; }
        public string SheetName { get; set; }
        public string SheetNumberName { get; set; }

        public WrapperViewSheet(ViewSheet _sheetObj)
        {
            SheetElement = _sheetObj;

            SheetNumber = SheetElement.SheetNumber;
            SheetName = SheetElement.Name;
            SheetNumberName = SheetNumber + " - " + SheetName;
        }
    }
}
