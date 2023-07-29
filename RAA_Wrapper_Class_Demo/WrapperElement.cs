using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_Wrapper_Class_Demo
{
    internal class WrapperElement
    {
        public Element curElem { get; set; }
        public FamilySymbol curFamilySymbol { get; set; }
        public string familyAndTypeName { get; private set; }
        public string familyName { get; private set; }
        public string typeName { get; private set; }
        public string designOption { get; private set; }

        public WrapperElement(Element _elem)
        {
            curElem = _elem;
            curFamilySymbol = GetFamilySymbolFromElement(curElem);

            GetFamilyAndTypeName();
            GetDesignOption();
        }

        private void GetFamilyAndTypeName()
        {
            familyName = curFamilySymbol.FamilyName;
            typeName = curFamilySymbol.Name;

            familyAndTypeName = "Family Type: " + typeName + ", Family: " + familyName;
        }

        private void GetDesignOption()
        {
            if (curElem.DesignOption != null)
            {
                DesignOption curOption = (DesignOption)curElem.DesignOption;

                if (curOption != null)
                    designOption = curOption.Name;
                else
                    designOption = string.Empty;
            }
            else
                designOption = string.Empty;
        }
        public bool SetParamValue(string paramName, string value)
        {
            // check family symbol first
            Parameter curParam = GetParameterByName(curFamilySymbol, paramName);

            // if no parameter found, check family instance
            if (curParam == null)
                curParam = GetParameterByName(curElem, paramName);

            if (curParam != null)
            {
                curParam.Set(value);
                return true;
            }

            return false;
        }

        public Parameter GetParameterByName(Element curElem, string paramName)
        {
            foreach (Parameter curParam in curElem.Parameters)
            {
                if (curParam.Definition.Name == paramName)
                    return curParam;
            }

            return null;
        }
        public string GetParameterValueByNameTypeOrInstance(Element curElem, string paramName)
        {
            string returnValue = string.Empty;

            Parameter instParam = GetParameterByName(curElem, paramName);

            if (instParam != null)
                returnValue = instParam.AsString();
            else
            {
                // check type parameter
                if (curFamilySymbol != null)
                {
                    Parameter typeParam = GetParameterByName(curFamilySymbol, paramName);

                    if (typeParam != null)
                        returnValue = typeParam.AsString();
                }
            }

            return returnValue;
        }
        private FamilySymbol GetFamilySymbolFromElement(Element curElem)
        {
            FamilySymbol curFS = null;

            try
            {
                curFS = (FamilySymbol)curElem.Document.GetElement(curElem.GetTypeId());
            }
            catch (Exception)
            {
                Debug.Print("Could not get family symbol from element");
            }

            return curFS;
        }
    }
}

