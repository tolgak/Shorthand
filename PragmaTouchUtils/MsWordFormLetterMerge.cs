using System;
using System.Reflection;
using System.IO;
using System.Xml;
//using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace PragmaTouchUtils
{
  public class MsWordFormLetterMerge
  {
    private int nextFormLetterVersion = 1;

    private Dictionary<string, string> _bookmarks = new Dictionary<string,string>();
    public Dictionary<string, string> Bookmarks
    {
      get { return _bookmarks; }
      set { _bookmarks = value; }
    }

    public string TemplateFullPath { get; set; }
    public string OutputFolder { get; set; }

    public void Merge() 
    {
      if ( !File.Exists(this.TemplateFullPath) )
        throw new Exception("Template file does not exist.");

      if ( !Directory.Exists(this.OutputFolder) )      
        Directory.CreateDirectory(this.OutputFolder);
              
      string wordmlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
      
      // Make a copy of the template file.
      string targetFileName = string.Format("{0}\\Document{1}.docx", this.OutputFolder, nextFormLetterVersion);
      File.Copy(this.TemplateFullPath, targetFileName, true);      

      //Open the document as an Open XML package and extract the main document part.
      using ( WordprocessingDocument wordPackage = WordprocessingDocument.Open(targetFileName, true) )
      {
        MainDocumentPart part = wordPackage.MainDocumentPart;

        //Setup the namespace manager so you can perform XPath queries to search for bookmarks in the part.
        NameTable nt = new NameTable();
        XmlNamespaceManager nsManager = new XmlNamespaceManager(nt);
        nsManager.AddNamespace("w", wordmlNamespace);

        //Load the part's XML into an XmlDocument instance.
        XmlDocument xmlDoc = new XmlDocument(nt);
        xmlDoc.Load(part.GetStream());

        foreach ( var item in _bookmarks )
        {
          bool firstTextNodeFound = false;
          XmlElement bookmarkStartNode = ( XmlElement ) xmlDoc.DocumentElement.SelectSingleNode("//w:bookmarkStart[@w:name='" + item.Key + "']", nsManager);
          string id = bookmarkStartNode.Attributes["w:id"].Value;

          //Get the beginning and end bookmark nodes as well as the text node for that ID.
          XmlNodeList followingNodesList = bookmarkStartNode.SelectNodes(".//following::w:t | .//following::w:bookmarkEnd[@w:id='" + id + "']", nsManager);

          foreach ( XmlElement el in followingNodesList )
          {
            //Update the value of first text node and remove all other text nodes falling between bookmark start and bookmark end nodes.
            if ( el.Name == "w:t" )
            {
              if ( firstTextNodeFound )
              {
                el.ParentNode.RemoveChild(el);
              }
              else
              {
                el.InnerText = item.Value;
                firstTextNodeFound = true;
              }
            }
            else
            {
              //It is a different bookmark node so move to the next one.
              break;
            }
          }
        }

        //Write the changes back to the document part.
        xmlDoc.Save(wordPackage.MainDocumentPart.GetStream(FileMode.Create));
        xmlDoc = null;
        wordPackage.Close();
      }

      //Increment the form letter version.
      nextFormLetterVersion++;
    }

 

  }


  
}
