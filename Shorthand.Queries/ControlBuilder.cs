using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Shorthand.Common;

namespace Shorthand.Queries
{
  public class ControlBuilder
  {
    private SqlDbType[] Numerics = new SqlDbType[] {SqlDbType.BigInt, SqlDbType.Int, SqlDbType.SmallInt, SqlDbType.TinyInt};
    private SqlDbType[] Alphanumerics = new SqlDbType[] { SqlDbType.NChar, SqlDbType.NText, SqlDbType.NVarChar, SqlDbType.Char, SqlDbType.Text, SqlDbType.VarChar };
    private SqlDbType[] Dates = new SqlDbType[] { SqlDbType.Date, SqlDbType.DateTime, SqlDbType.SmallDateTime };
    private SqlDbType[] Booleans = new SqlDbType[] { SqlDbType.Bit };
    private string _connectionString;

    public ControlBuilder()
    {

    }

    public ControlBuilder(string connectionString) : this()
    {
      _connectionString = connectionString;
    }

    public Control BuildControl(QueryParam p)
    {       
      SqlDbType type = (SqlDbType) p.SqlDbType;

      if ( p.HasLookup )
        return BuildComboBox(p);
      else if ( Dates.Contains(type) )
        return BuildDateTimePicker2(p);
      else if ( Booleans.Contains(type) )
        return BuildCheckBox(p);
      else
        return BuildTextBox(p);    
    }

    private Control BuildComboBox(QueryParam p)
    {
      List<LookupItem<int>> lookupItems = Utils.FetchEntity<LookupItem<int>>(_connectionString, p.LookupSqlText, null);
      lookupItems.Insert(0, new LookupItem<int>{ Name = string.Empty, Value = 0} );

      ComboBox c = new ComboBox();
      c.DataSource = lookupItems;
      c.DisplayMember = "Name";
      c.ValueMember = "Value";
      
      if ( p.ControlWidth > 0 )
        c.Width = p.ControlWidth;

      if ( p.ControlHeight > 0 )
        c.Height = p.ControlHeight;
      
      if ( !string.IsNullOrEmpty(p.DefaultValue) )
        p.Value = int.Parse(p.DefaultValue);

      this.AddBinding(c, new Binding("SelectedValue", p, "Value", false));
      return c;
    }

    private Control BuildCheckBox(QueryParam p)
    {
      CheckBox c = new CheckBox();
      if ( p.ControlWidth > 0 )
        c.Width = p.ControlWidth;

      if ( p.ControlHeight > 0 )
        c.Height = p.ControlHeight;

      if ( !string.IsNullOrEmpty(p.DefaultValue) )
        p.Value = bool.Parse(p.DefaultValue);

      this.AddBinding(c, new Binding("Checked", p, "Value"));
      return c;
    }

    private Control BuildDateTimePicker(QueryParam p)
    {
      DateTimePicker c = new DateTimePicker();
      if ( p.ControlWidth > 0 )
        c.Width = p.ControlWidth;

      if ( p.ControlHeight > 0 )
        c.Height = p.ControlHeight;

      if ( !string.IsNullOrEmpty(p.DefaultValue) )
        p.Value = DateTime.Parse(p.DefaultValue);
      else
        p.Value = DateTime.Now;

      this.AddBinding(c, new Binding("Value", p, "Value"));
      return c;
    }

    private Control BuildDateTimePicker2(QueryParam p)
    {
      MaskedTextBox c = new MaskedTextBox();
      if ( p.ControlWidth > 0 )
        c.Width = p.ControlWidth;

      if ( p.ControlHeight > 0 )
        c.Height = p.ControlHeight;

      c.Mask = "00/00/0000";
      if ( !string.IsNullOrEmpty(p.DefaultValue) )
        p.Value = DateTime.Parse(p.DefaultValue);

      this.AddBinding(c, new Binding("Text", p, "Value"));
      return c;
    }

    private Control BuildTextBox(QueryParam p)
    {
      TextBox c = new TextBox();
      if ( p.ControlWidth > 0 )
        c.Width = p.ControlWidth;

      if ( p.ControlHeight > 0 )
        c.Height = p.ControlHeight;

      if ( !string.IsNullOrEmpty(p.DefaultValue) )
        p.Value = p.DefaultValue;

      this.AddBinding(c, new Binding("Text", p, "Value"));
      return c;
    }

    private void AddBinding(Control control, Binding binding)
    {
      control.DataBindings.Clear();
      control.DataBindings.Add(binding);
    }


  }
}
