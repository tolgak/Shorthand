using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand.JiraEntity
{

    public class Issuetype
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public int avatarId { get; set; }
    }



    public class AvatarUrls
    {
        public string large_48x48 { get; set; }
        public string small_24x24 { get; set; }
        public string tiny_16x16 { get; set; }
        public string medium_32x32 { get; set; }
    }

    public class Project
    {
        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public AvatarUrls avatarUrls { get; set; }
    }

    public class ProjectCategory
    {
        public string self { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }







    public class Watches
    {
        public string self { get; set; }
        public int watchCount { get; set; }
        public bool isWatching { get; set; }
    }

    public class Priority
    {
        public string self { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Type
    {
        public string id { get; set; }
        public string name { get; set; }
        public string inward { get; set; }
        public string outward { get; set; }
        public string self { get; set; }
    }

    public class StatusCategory
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string colorName { get; set; }
        public string name { get; set; }
    }

    public class Status
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public StatusCategory statusCategory { get; set; }
    }

    public class Priority2
    {
        public string self { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Issuetype2
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public int avatarId { get; set; }
    }

    public class Fields2
    {
        public string summary { get; set; }
        public Status status { get; set; }
        public Priority2 priority { get; set; }
        public Issuetype2 issuetype { get; set; }
    }

    public class InwardIssue
    {
        public string id { get; set; }
        public string key { get; set; }
        public string self { get; set; }
        public Fields2 fields { get; set; }
    }

    public class OutwardIssue
    {
        public string id { get; set; }
        public string key { get; set; }
        public string self { get; set; }
        public Fields2 fields { get; set; }
    }

    public class Issuelink
    {
        public string id { get; set; }
        public string self { get; set; }
        public Type type { get; set; }
        public InwardIssue inwardIssue { get; set; }
        public OutwardIssue outwardIssue { get; set; }
    }


    public class Resolution
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }


    public class AvatarUrls2
    {
        public string __invalid_name__48x48 { get; set; }
        public string __invalid_name__24x24 { get; set; }
        public string __invalid_name__16x16 { get; set; }
        public string __invalid_name__32x32 { get; set; }
    }

    public class Assignee
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls2 avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class StatusCategory2
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string colorName { get; set; }
        public string name { get; set; }
    }

    public class Status2
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public StatusCategory2 statusCategory { get; set; }
    }

    public class Customfield11264
    {
        public string self { get; set; }
        public string value { get; set; }
        public string id { get; set; }
    }

    public class Timetracking
    {
    }

    public class Security
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    //public class AvatarUrls3
    //{
    //    public string __invalid_name__48x48 { get; set; }
    //    public string __invalid_name__24x24 { get; set; }
    //    public string __invalid_name__16x16 { get; set; }
    //    public string __invalid_name__32x32 { get; set; }
    //}

    public class Author
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Attachment
    {
        public string self { get; set; }
        public string id { get; set; }
        public string filename { get; set; }
        public Author author { get; set; }
        public DateTime created { get; set; }
        public int size { get; set; }
        public string mimeType { get; set; }
        public string content { get; set; }
        public string thumbnail { get; set; }
    }

    //public class AvatarUrls4
    //{
    //    public string __invalid_name__48x48 { get; set; }
    //    public string __invalid_name__24x24 { get; set; }
    //    public string __invalid_name__16x16 { get; set; }
    //    public string __invalid_name__32x32 { get; set; }
    //}

    public class Creator
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Customfield12066
    {
        public int spentTimeSeconds { get; set; }
        public string spentTime { get; set; }
        public string approveStatus { get; set; }
    }

    public class Customfield12065
    {
        public int spentTimeSeconds { get; set; }
        public string spentTime { get; set; }
        public string approveStatus { get; set; }
    }

    //public class AvatarUrls5
    //{
    //    public string __invalid_name__48x48 { get; set; }
    //    public string __invalid_name__24x24 { get; set; }
    //    public string __invalid_name__16x16 { get; set; }
    //    public string __invalid_name__32x32 { get; set; }
    //}

    public class Reporter
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Customfield12068
    {
        public int spentTimeSeconds { get; set; }
        public string spentTime { get; set; }
        public string approveStatus { get; set; }
    }

    public class Customfield12067
    {
        public int spentTimeSeconds { get; set; }
        public string spentTime { get; set; }
        public string approveStatus { get; set; }
    }

    public class Aggregateprogress
    {
        public int progress { get; set; }
        public int total { get; set; }
    }

    public class Progress
    {
        public int progress { get; set; }
        public int total { get; set; }
    }

    //public class AvatarUrls6
    //{
    //    public string __invalid_name__48x48 { get; set; }
    //    public string __invalid_name__24x24 { get; set; }
    //    public string __invalid_name__16x16 { get; set; }
    //    public string __invalid_name__32x32 { get; set; }
    //}

    public class Author2
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    //public class AvatarUrls7
    //{
    //    public string __invalid_name__48x48 { get; set; }
    //    public string __invalid_name__24x24 { get; set; }
    //    public string __invalid_name__16x16 { get; set; }
    //    public string __invalid_name__32x32 { get; set; }
    //}

    public class UpdateAuthor
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string emailAddress { get; set; }
        public AvatarUrls avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Comment2
    {
        public string self { get; set; }
        public string id { get; set; }
        public Author2 author { get; set; }
        public string body { get; set; }
        public UpdateAuthor updateAuthor { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }

    public class Comment
    {
        public List<Comment2> comments { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public int startAt { get; set; }
    }

    public class Votes
    {
        public string self { get; set; }
        public int votes { get; set; }
        public bool hasVoted { get; set; }
    }

    public class Worklog
    {
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public List<object> worklogs { get; set; }
    }

    public class Fields
    {
        public Issuetype issuetype { get; set; }
        public object customfield_10070 { get; set; }
        public object timespent { get; set; }
        public Project project { get; set; }
        public List<object> fixVersions { get; set; }
        public object customfield_10110 { get; set; }
        public List<string> customfield_11960 { get; set; }
        public object aggregatetimespent { get; set; }
        public object resolution { get; set; }
        public string customfield_11961 { get; set; }
        public object resolutiondate { get; set; }
        public int workratio { get; set; }
        public DateTime lastViewed { get; set; }
        public Watches watches { get; set; }
        public object customfield_11270 { get; set; }
        public DateTime created { get; set; }
        public object customfield_12561 { get; set; }
        public object customfield_12560 { get; set; }
        public object customfield_12563 { get; set; }
        public object customfield_10462 { get; set; }
        public object customfield_12562 { get; set; }
        public Priority priority { get; set; }
        public object customfield_10100 { get; set; }
        public object customfield_12564 { get; set; }
        public object customfield_10102 { get; set; }
        public List<object> labels { get; set; }
        public object timeestimate { get; set; }
        public object aggregatetimeoriginalestimate { get; set; }
        public List<object> versions { get; set; }
        public List<Issuelink> issuelinks { get; set; }
        public Assignee assignee { get; set; }
        public DateTime updated { get; set; }
        public Status2 status { get; set; }
        public List<object> components { get; set; }
        public object timeoriginalestimate { get; set; }
        public string description { get; set; }
        public object customfield_11263 { get; set; }
        public Customfield11264 customfield_11264 { get; set; }
        public Timetracking timetracking { get; set; }
        public object customfield_11269 { get; set; }
        public object customfield_11862 { get; set; }
        public Security security { get; set; }
        public List<Attachment> attachment { get; set; }
        public object aggregatetimeestimate { get; set; }
        public string summary { get; set; }
        public Creator creator { get; set; }
        public string customfield_12260 { get; set; }
        public List<object> subtasks { get; set; }
        public string customfield_10160 { get; set; }
        public object customfield_12462 { get; set; }
        public Customfield12066 customfield_12066 { get; set; }
        public Customfield12065 customfield_12065 { get; set; }
        public Reporter reporter { get; set; }
        public object customfield_10560 { get; set; }
        public object customfield_10120 { get; set; }
        public object customfield_12464 { get; set; }
        public Customfield12068 customfield_12068 { get; set; }
        public object customfield_12463 { get; set; }
        public Customfield12067 customfield_12067 { get; set; }
        public Aggregateprogress aggregateprogress { get; set; }
        public object customfield_12466 { get; set; }
        public object customfield_12465 { get; set; }
        public string customfield_10960 { get; set; }
        public object customfield_12467 { get; set; }
        public string duedate { get; set; }
        public Progress progress { get; set; }
        public Comment comment { get; set; }
        public Votes votes { get; set; }
        public Worklog worklog { get; set; }
    }

    public class Issue
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
    }


    public class IssueTransition
    {
        public string id { get; set; }
        public string name { get; set; }
        public To to { get; set; }
    }

    public class To
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public StatusCategory statusCategory { get; set; }
    }


}
