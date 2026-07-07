namespace TASK_API.Domain
{
    public enum Jobstatus {
        created,
        processing,
        completed,
        failed
    };
    public class Job
    {
        public string filePath { get; set; }
        public int jobStatus { get; set; }

    }
}
