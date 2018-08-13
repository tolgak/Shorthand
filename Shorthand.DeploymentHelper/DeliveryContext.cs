namespace Shorthand
{
    public class DeliveryContext
    {
        public const string ToTest = "Test";
        public const string ToProduction = "Production";

        public string InternalIssueKey { get; set; }
        public string RequestIssueKey { get; set; }
        public string DeploymentIssueKey { get; set; }
        public string UatIssueKey { get; set; }

        public string[] UatIssueKeys { get; set; }

        public string GitProjectName { get; set; }
        public int GitProjectId { get; set; }
        public string GitProjectWebUrl { get; set; }
        public int GitMergeRequestNo { get; set; }
        public string GitMergeRequestState { get; set; }

        public string TestExecutableTargetName { get; set; }

        public string DeliveryTo { get; set; }
        public bool CreateDeploymentIssue { get; set; }
        public bool CreateUatIssue { get; set; }
        public bool CreateMergeRequest { get; set; }
        public bool CopyExecutables { get; set; }

        public bool HasSqlScript { get; set; }
    }


}
