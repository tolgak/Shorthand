using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PragmaTouchUtils;

namespace Shorthand
{
  [ConfigContentItem]
  [Serializable]
  public class DeploymentOptions
  {
    public string LocalBinPath { get; set; }
    public string RemoteBinPath { get; set; }
    public string ArchiveToolPath { get; set; }
    public string ArchiveToolSwitches { get; set; }
    public string DestinationFolder { get; set; }

    public string TestDeliveryFolder { get; set; }
    public string ProductionDeliveryFolder { get; set; }

  }
}
