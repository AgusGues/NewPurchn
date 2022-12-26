using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZetroPDFViewer
{
    public class DisplayPdf : WebControl
    {
        private string _filepath;
        public string FilePath
        {
            get
            {
                return _filepath;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _filepath = string.Empty;
                }
                else
                {
                    int tild = -1;
                    //check ~ symbol including in pdf path then remove
                    tild = value.IndexOf('~');
                    if (tild != -1)
                    {
                        _filepath = value.Substring((tild + 2)).Trim();
                    }
                    else
                    {
                        _filepath = value;
                    }
                }
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<iframe src=" + Convert.ToString(FilePath) + " ");
                //fix height and width
                sb.Append("width=800px height=500px");
                sb.Append("<View PDF: <a href=" + Convert.ToString(FilePath) + "</a></p> ");
                sb.Append("</iframe>");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(Convert.ToString(sb));
                writer.RenderEndTag();
            }
            catch
            {
                //If any problem in the PDF at that time display below information
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("PDF Control...");
                writer.RenderEndTag();
            }
        }
    }
}