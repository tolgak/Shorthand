using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brotherhood.Configuration
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
