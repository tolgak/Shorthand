using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PragmaTouchUtils
{
  public interface IConfigContentEditor
  {
    bool Modified { get; }
    bool ContentLoaded { get; }
    string ItemClassName { get; }
    string Caption { get; }

    bool LoadContent();
    bool SaveContent();
    void ShowContent();
    void HideContent();
  }
}
