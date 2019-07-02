using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Shorthand.Common.Sql
{
  /// <summary>
  /// Use for an alternative param name other than the propery name
  /// </summary>
  [System.AttributeUsage(AttributeTargets.Property)]
  public class QueryParamNameAttribute : Attribute
  {
    public string Name { get; set; }

    public ParameterDirection ParamDirection { get; set; }
    public QueryParamNameAttribute(string name) : this(name, ParameterDirection.Input)
    {
    }

    public QueryParamNameAttribute(string name, ParameterDirection paramDirection)
    {
      Name = name;
      ParamDirection = paramDirection;
    }
  }

  /// <summary>
  /// Ignore this property
  /// </summary>
  [System.AttributeUsage(AttributeTargets.Property)]
  public class QueryParamIgnoreAttribute : Attribute
  {
  }

  public static class SqlParameterExtensions
  {
    private class QueryParamInfo
    {
      public string Name { get; set; }
      public object Value { get; set; }
    }



    //public static object[] ToSqlParamsArray(this object obj, SqlParameter[] additionalParams = null)
    //{
    //  var result = ToSqlParamsList(obj, additionalParams);
    //  return result.ToArray<object>();

    //}


    public static SqlParameter[] ToSqlParamsArray(this object obj, SqlParameter[] additionalParams = null)
    {
      var result = ToSqlParamsList(obj, additionalParams);
      return result.ToArray<SqlParameter>();

    }

    public static List<SqlParameter> ToSqlParamsList(this object obj, SqlParameter[] additionalParams = null)
    {
      var props = (
          from p in obj.GetType().GetProperties()
          let nameAttr = p.GetCustomAttributes(typeof(QueryParamNameAttribute), true)
          let ignoreAttr = p.GetCustomAttributes(typeof(QueryParamIgnoreAttribute), true)
          select new { Property = p, Names = nameAttr, Ignores = ignoreAttr }).ToList();

      var result = new List<SqlParameter>();

      props.ForEach(p =>
      {
        if (p.Ignores != null && p.Ignores.Length > 0)
          return;

        var name = p.Names.FirstOrDefault() as QueryParamNameAttribute;
        var pinfo = new QueryParamInfo();
        ParameterDirection paramDir = ParameterDirection.Input;

        if (name != null)
        {
          if (!String.IsNullOrWhiteSpace(name.Name))
            pinfo.Name = name.Name.Replace("@", "");

          paramDir = name.ParamDirection;

        }
        else
          pinfo.Name = p.Property.Name.Replace("@", "");

        pinfo.Value = p.Property.GetValue(obj) ?? DBNull.Value;
        var sqlParam = new SqlParameter(pinfo.Name, TypeConvertor.ToSqlDbType(p.Property.PropertyType))
        {
          Value = pinfo.Value,
        };

        sqlParam.Direction = paramDir;

        result.Add(sqlParam);
      });

      if (additionalParams != null && additionalParams.Length > 0)
        result.AddRange(additionalParams);

      return result;

    }

  }
}
