using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PragmaTouchUtils;

namespace Shorthand
{
  public partial class ucOptionEditorBase : UserControl
  {
    public ucOptionEditorBase()
    {
      InitializeComponent();
    }

    protected ConfigContent _currentConfig;

    public bool Modified { get; set; }

    public bool ContentLoaded { get; set; }

    public string ItemClassName { get; set; }

    public string Caption { get; set; }

    public bool LoadContent()
    {
      _currentConfig = ConfigContent.Current;
      if ( _currentConfig == null )
        throw new Exception("Configuration content param is null!");

      LoadInitial();

      this.ContentLoaded = true;
      return true;
    }

    protected virtual void LoadInitial()
    {

    }

    public virtual bool SaveContent()
    {
      _currentConfig.SaveConfiguration();
      return true;
    }

    public void ShowContent()
    {
      this.Show();
    }

    public void HideContent()
    {
      this.Hide();
    }


  }
}
