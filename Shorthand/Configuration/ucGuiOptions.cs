using System;
using PragmaTouchUtils;

namespace Shorthand
{

  public partial class ucGuiOptions : ucOptionEditorBase, IConfigContentEditor
  {    
    private GuiOptions _options;

    public ucGuiOptions()
    {
      InitializeComponent();

      this.ItemClassName = "GuiOptions";
      this.Caption = "GUI";
    }

    protected override object LoadUnderlyingOption()
    {
      _options = _currentConfig.GetConfigContentItem(this.ItemClassName) as GuiOptions;
      if (_options == null)
        throw new Exception(string.Format("Configuration content does not contain {0} item!", this.ItemClassName));

      txtWidth.DataBindTo(_options, "Width");
      txtHeight.DataBindTo(_options, "Height");

      return _options;
    }




  }

}
