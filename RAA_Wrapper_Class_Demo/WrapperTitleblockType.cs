using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Wrapper_Class_Demo
{
    public class WrapperTitleblockType
    {
        public FamilySymbol TitleblockType { get; set; }
        public string FamilyAndType { get; set; }

        public WrapperTitleblockType(FamilySymbol titleblocktype) 
        { 
            TitleblockType = titleblocktype;
            FamilyAndType = TitleblockType.FamilyName + " : " + TitleblockType.Name;
        }
    }
}
