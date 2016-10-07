using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{
  public interface IDelivery
  {
    void Deliver(DeliveryContext ctx);

    //void Prepare(Dictionary<string, string> references);
    //string BuilComment(Dictionary<string, string> references);

  }
}
