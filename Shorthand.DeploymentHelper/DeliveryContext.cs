namespace Shorthand
{

    public class boIssue
    {
        public string key { get; set; }
        public string status { get; set; }
        public string description { get; set; }
    }



    public class BaseData
    {
        public boIssue InternalIssue { get; set; }
        public boIssue RequestIssue { get; set; }
        public boIssue DeploymentIssue { get; set; }
        public boIssue UatIssue { get; set; }
        public boIssue[] UatIssues { get; set; }

        public string GitProjectName { get; set; }
        public int GitProjectId { get; set; }
        public string GitProjectWebUrl { get; set; }
        public int GitMergeRequestNo { get; set; }
        public string GitMergeRequestState { get; set; }
    }


    public class DeliveryContext
    {
        public const string ToTest = "Test";
        public const string ToProduction = "Production";

        public string InternalIssue { get; set; }
        public string RequestIssue { get; set; }
        public string DeploymentIssue { get; set; }
        public string UatIssue { get; set; }


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
