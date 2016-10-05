//---------------------------------------------------------------------
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace Shorthand
{
  public class SortComparer<T> : IComparer<T>
  {
    private PropertyDescriptor _propDesc = null;
    private ListSortDirection _sortDirection = ListSortDirection.Ascending;
    private Comparer _defComparer;

    public SortComparer(PropertyDescriptor propDesc, ListSortDirection direction)
    {
      _defComparer = Comparer.Default;
      _propDesc = propDesc;
      _sortDirection = direction;
    }

    public int Compare(T x, T y)
    {
      // make sure there's a comparable field
      if ( (object) _propDesc == null )
        return 0;

      // both objects must exist
      if ( (object) x == null || (object) y == null )
        return 0;

      object xValue = _propDesc.GetValue(x);
      object yValue = _propDesc.GetValue(y);

      if ( _sortDirection == ListSortDirection.Ascending )
        return _defComparer.Compare(xValue, yValue);
      else
        return _defComparer.Compare(yValue, xValue);

      //return CompareValues(xValue, yValue, _direction);
    }

    //private int CompareValues(object x, object y, ListSortDirection direction)
    //{
    //  //int retValue = 0;

    //  //if (xValue == null && yValue == null)
    //  //  return 0;
    //  //else if (xValue == null)
    //  //  return -1;
    //  //else if (yValue == null)
    //  //  return 1;
    //  ////else if (xValue is IComparable) //can ask the x value      
    //  ////  retValue = ((IComparable)xValue).CompareTo(yValue);
    //  ////else if (yValue is IComparable) //can ask the y value      
    //  ////  retValue = ((IComparable)yValue).CompareTo(xValue);
    //  //else if (!xValue.Equals(yValue)) //not comparable, compare string representations      
    //  //  retValue = xValue.ToString().CompareTo(yValue.ToString());
      
      
    //  ////if (direction == ListSortDirection.Ascending)
    //  //  return retValue;
    //  ////else
    //  ////  return retValue * -1;
    //}

  }



  public class SortableBindingList<T> : BindingList<T>
  {
    protected override bool SupportsSortingCore
    {
      get { return true; }
    }
    
    private bool _sorted = false;
    protected override bool IsSortedCore
    {
      get { return _sorted; }
    }

    private ListSortDirection _sortDirection = ListSortDirection.Ascending;
    protected override ListSortDirection SortDirectionCore
    {
      get { return _sortDirection; }
    }

    private PropertyDescriptor _sortProperty = null;
    protected override PropertyDescriptor SortPropertyCore
    {
      get { return _sortProperty;}
    }


    public SortableBindingList(IList<T> list) : base(list) { }
    
    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
      _sortDirection = direction;
      _sortProperty = prop;
      var listRef = this.Items as List<T>;
      if (listRef == null)
        return;

      var comparer = new SortComparer<T>(prop, direction);
      listRef.Sort(comparer);

      //OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }
  }
    
}
