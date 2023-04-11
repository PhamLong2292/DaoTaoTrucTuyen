using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace OneTSQ.Printer
{
    public static class XsltUtil
    {
        public static string GenHtml(string xslt, string xml)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var strWriter = new StringWriter();
            var xsltDoc = new XmlDocument();
            xsltDoc.LoadXml(xslt);
            var xsltReader = new XmlNodeReader(xsltDoc);
            var compiledTransform = new XslCompiledTransform();
            compiledTransform.Load(xsltReader);
            compiledTransform.Transform(xmlDoc, null, strWriter);
            var html = strWriter.ToString()
                .Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            return html;
        }
    }
}