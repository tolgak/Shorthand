using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shorthand.Queries
{
  public class QueryParam
  {
    public int Id { get; set; }
    public int QueryId { get; set; }
    public string Caption { get; set; }
    public string Name { get; set; }
    public int SqlDbType { get; set; }
    public string LookupSqlText { get; set; }
    public string DefaultValue { get; set; }
    public int ControlWidth { get; set; }
    public int ControlHeight { get; set; }
    public int CaptionWidth { get; set; }
    public int RenderOrder { get; set; }

    public bool HasLookup { get { return !string.IsNullOrEmpty(this.LookupSqlText); } }
    public dynamic Value { get; set; }

    public QueryParam()
    {

    }

  }
}
