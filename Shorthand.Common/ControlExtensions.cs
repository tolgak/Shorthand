using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Shorthand
{
  public static class ControlExtensions
  {

    //public static void Dump(this TextBox box, string line)
    //{      
    //  box.InvokeIfRequired( (x) => { x.AppendText($"{line}\r\n"); });
    //}

    public static void Log(this TextBox box, string line)
    {
      box.InvokeIfRequired( (x) => { x.AppendText($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {line}\r\n"); } );
    }

    public static void DataBindTo(this TextBox control, object instance, string propertyName)
    {
      DataBindTo(control, instance, propertyName, null);
    }

    public static void DataBindTo(this TextBox control, object instance, string propertyName, EventHandler valueChangedHandler)
    {
      ControlExtensions.AddBinding(control, new Binding("Text", instance, propertyName, false, DataSourceUpdateMode.OnPropertyChanged));
      if ( valueChangedHandler != null )
        control.TextChanged += new EventHandler(valueChangedHandler);
    }

    public static void DataBindTo(this MaskedTextBox control, object instance, string propertyName)
    {
      DataBindTo(control, instance, propertyName, null);
    }

    public static void DataBindTo(this MaskedTextBox control, object instance, string propertyName, EventHandler valueChangedHandler)
    {
      ControlExtensions.AddBinding(control, new Binding("Text", instance, propertyName, false, DataSourceUpdateMode.OnPropertyChanged));
      if ( valueChangedHandler != null )
        control.TextChanged += new EventHandler(valueChangedHandler);
    }

    public static void DataBindTo(this ComboBox control, object instance, string propertyName)
    {
      DataBindTo(control, instance, propertyName, null);
    }

    public static void DataBindTo(this ComboBox control, object instance, string propertyName, EventHandler valueChangedHandler)
    {
      if (string.IsNullOrEmpty(control.ValueMember))
        ControlExtensions.AddBinding(control, new Binding("Text", instance, propertyName, false, DataSourceUpdateMode.OnPropertyChanged));        
      else
        ControlExtensions.AddBinding(control, new Binding("SelectedValue", instance, propertyName, false, DataSourceUpdateMode.OnPropertyChanged));
        
      if ( valueChangedHandler != null )
        control.SelectedValueChanged += new EventHandler(valueChangedHandler);
    }

    public static void DataBindTo(this DateTimePicker control, object instance, string propertyName)
    {
      DataBindTo(control, instance, propertyName, null);
    }

    public static void DataBindTo(this DateTimePicker control, object instance, string propertyName, EventHandler valueChangedHandler)
    {
      ControlExtensions.AddBinding(control, new Binding("Value", instance, propertyName, false, DataSourceUpdateMode.OnPropertyChanged));
      if ( valueChangedHandler != null )
        control.ValueChanged += new EventHandler(valueChangedHandler);
    }

    public static void DataBindTo(this CheckBox control, object instance, string propertyName)
    {
      DataBindTo(control, instance, propertyName, null);
    }

    public static void DataBindTo(this CheckBox control, object instance, string propertyName, EventHandler valueChangedHandler)
    {
      ControlExtensions.AddBinding(control, new Binding("Checked", instance, propertyName, false, DataSourceUpdateMode.OnPropertyChanged));
      if ( valueChangedHandler != null )
        control.CheckedChanged += new EventHandler(valueChangedHandler);
    }

    private static void AddBinding(Control control, Binding binding)
    {
      control.DataBindings.Clear();
      control.DataBindings.Add(binding);    
    }

    public static int GetSelectedValue(this ComboBox control)
    {
      var item = control.Items[control.SelectedIndex];
      return (int) item.GetType().GetProperty(control.ValueMember).GetValue(item, null) ;
    }

  }


}
