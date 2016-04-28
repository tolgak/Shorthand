using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{
  public class DeliveryContext
  {
    public string InternalIssueKey { get; set; }
    public string RequestIssueKey { get; set; }
    public string DeploymentIssueKey { get; set; }
    public string UatIssueKey { get; set; }

    public string GitProjectName { get; set; }
    public int GitProjectId { get; set; }
    public string GitProjectWebUrl { get; set; }
    public int GitMergeRequestNo { get; set; }
    public string GitMergeRequestState { get; set; }

  }


}
