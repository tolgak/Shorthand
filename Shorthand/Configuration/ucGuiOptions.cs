using System;
using PragmaTouchUtils;

namespace Shorthand
{

  public partial class ucGuiOptions : ucOptionEditorBase, IConfigContentEditor
  {    
    private GuiOptions _options;
    private string _cleanHash;

    public ucGuiOptions()
    {
      InitializeComponent();

      this.ItemClassName = "GuiOptions";
      this.Caption = "GUI";
    }

    protected override void LoadInitial()
    {
      _options = _currentConfig.GetConfigContentItem(this.ItemClassName) as GuiOptions;
      if (_options == null)
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      _cleanHash = _options.GetMd5Hash();

      txtWidth.DataBindTo(_options, "Width");
      txtHeight.DataBindTo(_options, "Height");
    }

    public override bool Modified { get { return string.IsNullOrEmpty(_cleanHash) ? false : _cleanHash != _options.GetMd5Hash(); } }


  }

}
