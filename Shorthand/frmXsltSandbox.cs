using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace Shorthand
{
  public partial class frmXsltSandbox : Form
  {
    public frmXsltSandbox()
    {
      InitializeComponent();
    }

    private void btnRun_Click(object sender, EventArgs e)
    {
      this.go();

      //var myXslTrans = new XslCompiledTransform();
      //myXslTrans.Load("stylesheet.xsl");
      //myXslTrans.Transform("source.xml", "result.html"); 

      //using (var xslSourceStream = new MemoryStream())
      //using (var xmlSourceStream = new MemoryStream())
      //using (var resultStream = new FileStream("sample_schedule.htm", FileMode.OpenOrCreate))
      //{
      //  txtSourceXSL.SaveFile(xslSourceStream, RichTextBoxStreamType.UnicodePlainText);        
      //  xslSourceStream.Seek(0, SeekOrigin.Begin);

      //  txtSourceXML.SaveFile(xmlSourceStream, RichTextBoxStreamType.UnicodePlainText);
      //  xmlSourceStream.Seek(0, SeekOrigin.Begin);

      //  var xslReader = XmlReader.Create(xslSourceStream);
      //  var myXslTrans = new XslCompiledTransform();      
      //  myXslTrans.Load(xslReader);

      //  var xmlReader = XmlReader.Create(xmlSourceStream);

      //  var writer = XmlWriter.Create(resultStream);
      //  myXslTrans.Transform(xmlReader, writer);       
        
      //  resultStream.Flush();
      //}


    }


    private void go()
    {
      var html = string.Empty;

      // read xml
      var orgDoc = new XmlDocument();
      orgDoc.LoadXml(txtSourceXML.Text);

      // read xsl
      using ( var stream = new StringReader(txtSourceXSL.Text))
      using ( var reader = XmlReader.Create(stream))
      {
        var trans = new XslCompiledTransform();
        trans.Load(reader);

        // MUST SELECT THE ROOT NODE
        XmlNode transNode = orgDoc.SelectSingleNode("/");
        var sb = new StringBuilder();
        var writer = XmlWriter.Create(sb);
        trans.Transform(transNode, writer);
        html = sb.ToString();
      }


      var css = "<style type=\"text/css\" >" + txtSourceCSS.Text + "</style>";
      html = html.Replace("<style type=\"text/css\" />", css);
      
      browser.Navigate("about:blank");
      if (browser.Document != null)      
        browser.Document.Write(string.Empty);
      
      browser.DocumentText = html;

      

      Application.DoEvents();
    }



}
}
