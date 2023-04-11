using OneTSQ.Core.Model;
using OneTSQ.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.MediaService
{
    public class MediaServiceUtility
    {
       
        public static AjaxOut UploadMedia(string SecurityCode, string Xml, string XmlSchema, byte[] BytesUpload)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                MediaInfoCls OUploadMediaInfo = MediaInfoParser.ParseFromXml(Xml, XmlSchema);
                string MediaPathRoot = WebConfig.GetMediaPathRoot();
                string MediaHttpRoot = WebConfig.GetMediaHttpRoot();

                if (string.IsNullOrEmpty(OUploadMediaInfo.LoginName))
                {
                    throw new Exception("INVALID LOGIN NAME");
                }
                if (string.IsNullOrEmpty(OUploadMediaInfo.Section))
                {
                    throw new Exception("INVALID SECTION");
                }
                if(string.IsNullOrEmpty(OUploadMediaInfo.UploadFileName))
                {
                    throw new Exception("INVALID UPLOAD FILENAME");
                }
                //if (OUploadMediaInfo.Month==0)
                //{
                //    throw new Exception("INVALID MONTH");
                //}
                //if (OUploadMediaInfo.Year == 0)
                //{
                //    throw new Exception("INVALID YEAR");
                //}


                OUploadMediaInfo.UploadFileName=new System.IO.FileInfo(OUploadMediaInfo.UploadFileName).Name;
                string SaveDir = MediaPathRoot + "\\" + OUploadMediaInfo.Month.ToString().PadLeft(2, '0') + "." + OUploadMediaInfo.Year + "\\" + OUploadMediaInfo.LoginName + "\\" + OUploadMediaInfo.Section;
                System.IO.Directory.CreateDirectory(SaveDir);
                string SaveFile=SaveDir +"\\" + OUploadMediaInfo.UploadFileName;
                if (System.IO.File.Exists(SaveFile))
                {
                    if (OUploadMediaInfo.Overwrite)
                    {
                        try
                        {
                            System.IO.File.Delete(SaveFile);
                        }
                        catch { }
                        FunctionUtility.SaveBytesToFile(SaveFile, BytesUpload);
                    }
                }
                else
                {
                    FunctionUtility.SaveBytesToFile(SaveFile, BytesUpload);
                }

                RetAjaxOut.RetBoolean = true;
                RetAjaxOut.RetUrl = MediaHttpRoot + "/" + OUploadMediaInfo.Month.ToString().PadLeft(2, '0') + "." + OUploadMediaInfo.Year + "/" + OUploadMediaInfo.LoginName + "/" + OUploadMediaInfo.Section + "/" + OUploadMediaInfo.UploadFileName;
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }

            return RetAjaxOut;
        }

        public static AjaxOut DownloadMedia(string SecurityCode, string Xml,string XmlSchema)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                MediaInfoCls OUploadMediaInfo = MediaInfoParser.ParseFromXml(Xml, XmlSchema);
                string MediaPathRoot = WebConfig.GetMediaPathRoot();
                string MediaHttpRoot = WebConfig.GetMediaHttpRoot();

                if (string.IsNullOrEmpty(OUploadMediaInfo.LoginName))
                {
                    throw new Exception("INVALID LOGIN NAME");
                }
                if (string.IsNullOrEmpty(OUploadMediaInfo.Section))
                {
                    throw new Exception("INVALID SECTION");
                }
                if (string.IsNullOrEmpty(OUploadMediaInfo.UploadFileName))
                {
                    throw new Exception("INVALID UPLOAD FILENAME");
                }
                if (OUploadMediaInfo.Month == 0)
                //{
                //    throw new Exception("INVALID MONTH");
                //}
                //if (OUploadMediaInfo.Year == 0)
                //{
                //    throw new Exception("INVALID YEAR");
                //}

                OUploadMediaInfo.UploadFileName = new System.IO.FileInfo(OUploadMediaInfo.UploadFileName).Name;
                string SaveDir = MediaPathRoot + "\\" + OUploadMediaInfo.Month.ToString().PadLeft(2, '0') + "." + OUploadMediaInfo.Year + "\\" + OUploadMediaInfo.LoginName + "\\" + OUploadMediaInfo.Section;
                System.IO.Directory.CreateDirectory(SaveDir);
                string SaveFile = SaveDir + "\\" + OUploadMediaInfo.UploadFileName;
                if (System.IO.File.Exists(SaveFile))
                {
                    RetAjaxOut.RetBytes = FunctionUtility.GetBytesFromFile(SaveFile);
                    RetAjaxOut.RetUrl = MediaHttpRoot + "/" + OUploadMediaInfo.Month.ToString().PadLeft(2, '0') + "." + OUploadMediaInfo.Year + "/" + OUploadMediaInfo.LoginName + "/" + OUploadMediaInfo.Section + "/" + OUploadMediaInfo.UploadFileName;
                    RetAjaxOut.RetBoolean = true;
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }

            return RetAjaxOut;
        }

        public static AjaxOut GetMediaInfo(string SecurityCode, string Xml,string XmlSchema)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string MediaPathRoot = WebConfig.GetMediaPathRoot();
                string MediaHttpRoot = WebConfig.GetMediaHttpRoot();
                MediaInfoCls OGetMediaInfo = MediaInfoParser.ParseFromXml(Xml, XmlSchema);

                if (string.IsNullOrEmpty(OGetMediaInfo.LoginName))
                {
                    throw new Exception("INVALID LOGIN NAME");
                }
                if (string.IsNullOrEmpty(OGetMediaInfo.Section))
                {
                    throw new Exception("INVALID SECTION");
                }
                if (string.IsNullOrEmpty(OGetMediaInfo.UploadFileName))
                {
                    throw new Exception("INVALID UPLOAD FILENAME");
                }
                //if (OGetMediaInfo.Month == 0)
                //{
                //    throw new Exception("INVALID MONTH");
                //}
                //if (OGetMediaInfo.Year == 0)
                //{
                //    throw new Exception("INVALID YEAR");
                //}

                OGetMediaInfo.UploadFileName = new System.IO.FileInfo(OGetMediaInfo.UploadFileName).Name;
                string SaveDir = MediaPathRoot + "\\" + OGetMediaInfo.Month.ToString().PadLeft(2, '0') + "." + OGetMediaInfo.Year + "\\" + OGetMediaInfo.LoginName + "\\" + OGetMediaInfo.Section;
                System.IO.Directory.CreateDirectory(SaveDir);
                string SaveFile = SaveDir + "\\" + OGetMediaInfo.UploadFileName;
                if (System.IO.File.Exists(SaveFile))
                {
                    RetAjaxOut.RetBytes = FunctionUtility.GetBytesFromFile(SaveFile);
                    RetAjaxOut.RetUrl = MediaHttpRoot + "/" + OGetMediaInfo.Month.ToString().PadLeft(2, '0') + "." + OGetMediaInfo.Year + "/" + OGetMediaInfo.LoginName + "/" + OGetMediaInfo.Section + "/" + OGetMediaInfo.UploadFileName;
                    RetAjaxOut.RetBoolean = true;
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }

            return RetAjaxOut;
        }

        public static AjaxOut DeleteMediaInfo(string SecurityCode, string Xml, string XmlSchema)
        {
            AjaxOut RetAjaxOut = new AjaxOut();
            try
            {
                string MediaPathRoot = WebConfig.GetMediaPathRoot();
                string MediaHttpRoot = WebConfig.GetMediaHttpRoot();
                MediaInfoCls OGetMediaInfo = MediaInfoParser.ParseFromXml(Xml, XmlSchema);

                if (string.IsNullOrEmpty(OGetMediaInfo.LoginName))
                {
                    throw new Exception("INVALID LOGIN NAME");
                }
                if (string.IsNullOrEmpty(OGetMediaInfo.Section))
                {
                    throw new Exception("INVALID SECTION");
                }
                if (string.IsNullOrEmpty(OGetMediaInfo.UploadFileName))
                {
                    throw new Exception("INVALID UPLOAD FILENAME");
                }
                if (OGetMediaInfo.Month == 0)
                {
                    throw new Exception("INVALID MONTH");
                }
                if (OGetMediaInfo.Year == 0)
                {
                    throw new Exception("INVALID YEAR");
                }

                OGetMediaInfo.UploadFileName = new System.IO.FileInfo(OGetMediaInfo.UploadFileName).Name;
                string SaveDir = MediaPathRoot + "\\" + OGetMediaInfo.Month.ToString().PadLeft(2, '0') + "." + OGetMediaInfo.Year + "\\" + OGetMediaInfo.LoginName + "\\" + OGetMediaInfo.Section;
                System.IO.Directory.CreateDirectory(SaveDir);
                string SaveFile = SaveDir + "\\" + OGetMediaInfo.UploadFileName;
                if (System.IO.File.Exists(SaveFile))
                {
                    System.IO.File.Delete(SaveFile);
                }
            }
            catch (Exception ex)
            {
                RetAjaxOut.Error = true;
                RetAjaxOut.InfoMessage = ex.Message.ToString();
            }

            return RetAjaxOut;
        }
    }
}
