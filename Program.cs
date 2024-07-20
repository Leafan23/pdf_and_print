﻿using Kompas6API5;
using KompasAPI7;
using Kompas6Constants;
using Kompas6Constants3D;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Versioning;
using System.Security;
using System.Xml.Linq;
using System.Linq;
using System.IO;


namespace Kompas
{
    class KompasAPI
    {

        static void Main(string[] args)
        {
            IApplication application = (IApplication)Marshal2.GetActiveObject("Kompas.Application.7");
            IKompasDocument kompasDocument = (IKompasDocument)application.ActiveDocument;

            if ((int)kompasDocument.DocumentType == 1 || (int)kompasDocument.DocumentType == 3)
            {
                string path_name = kompasDocument.PathName;
                path_name = path_name.Remove(path_name.Length - 4);
                Dictionary<int, string> doc_type = new Dictionary<int, string>()
                {
                    {3, " СП" },
                    {1, "" }
                };
                string lib_path = (string)Directory.GetCurrentDirectory();
                IConverter convert = application.Converter[@"..\Bin\Pdf2d.dll"];

                //string path = Path.GetFullPath(@"..\Bin\Pdf2d.dll");
                //Console.WriteLine(path);

                path_name = path_name + doc_type[(int)kompasDocument.DocumentType] + ".pdf";
                convert.Convert(null, path_name, 0, false);
                application.ExecuteKompasCommand(57607, true);
            }

        }
    }
}


public static class Marshal2
{
    internal const String OLEAUT32 = "oleaut32.dll";
    internal const String OLE32 = "ole32.dll";

    [System.Security.SecurityCritical]  // auto-generated_required
    public static Object GetActiveObject(String progID)
    {
        Object obj = null;
        Guid clsid;

        // Call CLSIDFromProgIDEx first then fall back on CLSIDFromProgID if
        // CLSIDFromProgIDEx doesn't exist.
        try
        {
            CLSIDFromProgIDEx(progID, out clsid);
        }
        //            catch
        catch (Exception)
        {
            CLSIDFromProgID(progID, out clsid);
        }

        GetActiveObject(ref clsid, IntPtr.Zero, out obj);
        return obj;
    }

    //[DllImport(Microsoft.Win32.Win32Native.OLE32, PreserveSig = false)]
    [DllImport(OLE32, PreserveSig = false)]
    [ResourceExposure(ResourceScope.None)]
    [SuppressUnmanagedCodeSecurity]
    [System.Security.SecurityCritical]  // auto-generated
    private static extern void CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] String progId, out Guid clsid);

    //[DllImport(Microsoft.Win32.Win32Native.OLE32, PreserveSig = false)]
    [DllImport(OLE32, PreserveSig = false)]
    [ResourceExposure(ResourceScope.None)]
    [SuppressUnmanagedCodeSecurity]
    [System.Security.SecurityCritical]  // auto-generated
    private static extern void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] String progId, out Guid clsid);

    //[DllImport(Microsoft.Win32.Win32Native.OLEAUT32, PreserveSig = false)]
    [DllImport(OLEAUT32, PreserveSig = false)]
    [ResourceExposure(ResourceScope.None)]
    [SuppressUnmanagedCodeSecurity]
    [System.Security.SecurityCritical]  // auto-generated
    private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out Object ppunk);

}