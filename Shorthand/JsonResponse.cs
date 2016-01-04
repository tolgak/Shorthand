using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{
  public class JsonResponse
  {
    public string Result { get; set; }
    public bool Success { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Description { get; set; }
  }
}
