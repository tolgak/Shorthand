using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VerifyProjectFile
{
  class Program
  {
    static int  Main(string[] args)
    {
      //var solutionBasePath = @"D:\Development\GitProjects\BilgiCampus\Bilgi.Sis.MobileWeb";
      //var solutionFilePath = Path.Combine(solutionBasePath, "BilgiCampus.sln");

      var sw = new Stopwatch();

      var result = true;
      if (args[0] == "?" || args[0] == "--help")
        Usage();

      sw.Start();
      if (args[0] == "--s")
        result = CheckSolution(args[1]);

      if (args[0] == "--f")
        result = CheckFolder(args[1]);

      sw.Stop();
      Log($"completed in {sw.ElapsedMilliseconds.ToString()} ms.");

      return result ? 0 : 1;
    }

    private static void Usage()
    {
      Console.WriteLine("");
      Console.WriteLine("Checks content references in project files and reports duplicated references and references to files missing in the file system");
      Console.WriteLine("VerifyProjectFile --option path");
      Console.WriteLine("");
      Console.WriteLine("option");
      Console.WriteLine("f : find project files in the specified folder including its subfolders and verify");
      Console.WriteLine("s : verify projects in the specified solution file");
      Console.WriteLine("");
      Console.WriteLine("path : a path to a solution file or a folder");

      Console.WriteLine("");
      Console.WriteLine("return value is 0 if valid, 1 otherwise");
    }

    private static bool CheckSolution(string solutionFilePath)
    {
      if (!File.Exists(solutionFilePath))
      {
        Log("Not a file path or file does not exist!");
        return false;
      }

      var solutionBasePath = Path.GetDirectoryName(solutionFilePath);
      Regex regex = new Regex("Project\\(.*\\) *= *\"(?<projectName>.*)\" *, *\"(?<projectFilePath>.*)\" *, *\"(?<solutionUID>.*)\""
                             , RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

      var inputText = File.ReadAllText(solutionFilePath);
      MatchCollection ms = regex.Matches(inputText);

      bool result = true;
      ms.OfType<Match>()
        .ToList()
        .ForEach(m => {
          var p = m.Groups["projectFilePath"].Value;
          if (p != ".nuget")
          {
            var x = Path.Combine(solutionBasePath, p);
            result &= 0 == CheckContentReferences(x);
          }
        });

      return result;
    }

    private static bool CheckFolder(string path)
    {
      if (!Directory.Exists(path))
      {
        Log("Not a folder or folder does not exist!");
        return false;
      }

      bool result = true;
      var projects = Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories).ToList();
      projects.ForEach(p => { if (p != ".nuget")
                              {
                                var x = Path.Combine(p);
                                result &= 0 == CheckContentReferences(x);
                              }
                            } );
      return result;
    }

    private static int CheckContentReferences(string projectFilePath)
    {
      if (!File.Exists(projectFilePath))
      {
        Log("Specified file does not exist!");
        return 3;
      }

      try
      {
        var projectBasePath = Path.GetDirectoryName(projectFilePath);
        var projectName = Path.GetFileName(projectFilePath);
        var xmlProject = XDocument.Load(projectFilePath);

        //XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
        XNamespace ns = xmlProject.Root.GetDefaultNamespace();
        var result = xmlProject.Element(ns + "Project")
                               .Elements(ns + "ItemGroup")
                               .Elements(ns + "Content")
                               .Select(x => $"{projectBasePath}\\{(string)x.Attribute("Include")}");
//                                       .ToList();

        //var filesExist = result.TrueForAll(x => File.Exists(x));
        //var allUnique  = result.Distinct().Count() == result.Count();
        //var allUnique  = result.GroupBy(x => x).All(g => g.Count() == 1);

        // 1. duplications
        var duplicates = result.GroupBy(x => x)
                              .Where(g => g.Count() > 1)
                              .Select(y => y.Key)
                              .ToList();
        if (duplicates.Count() > 0)
        {
          Log($"{projectName} Duplicates");
          duplicates.ForEach(x => Log(x));
          return 1;
        }

        //2.missing in file system
        var missing = result.AsParallel().Where(x => !File.Exists(x)).ToList();
        if (missing.Count() > 0)
        {
          Log($"{projectName} Missing");
          missing.ForEach(x => Log(x));
          return 2;
        }


      }
      catch (Exception ex)
      {
        Log(ex.Message);
        return 4;
      }

      return 0;
    }

    private static void Log(string line)
    {
      Console.WriteLine($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {line}");
    }



  }
}
