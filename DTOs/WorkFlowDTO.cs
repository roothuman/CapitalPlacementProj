using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.DTOs
{
    public class WorkFlowDTO
    {
        public string? id { get; set; }
        public string StageName { get; set; }
        public StageType stageType { get; set; }
    }

    public class StageType
    {
        public string ShortListing { get; set; }

        [MaxLength(3)]
        public VideoInterview videoInterview { get; set; }
        public string Placement { get; }
    }

    public class VideoInterview
    {
        public VidQuestions? vidQuestions { get; set; }
        public string VideoInterviewQuestion { get; set; }
        public string AdditinalInfo { get; set; }
        public VideoMaxDuration videoMaxDuration { get; set; }
        public double VideoSubmissionDeadline { get; set; }
    }

    public class VidQuestions
    {
        public string AboutYourself { get; set; }
        public string WhyYouApplied { get; set; }
    }

    public enum VideoMaxDuration
    {
        Minute,
        Seconds
    }

}
