
namespace CapitalPlacement.DTOs
{
    public class ProgramDTO
    {
        public ProgramDTO()
        {
            KeySkills = new List<string>() { "UI", "UX", "Content Writing", "SEO", "Graphics Design", "Social Media" };
        }

        public string id { get; set; }
        public string ProgramTitle { get; set; }
        public string Summary { get; set; }
        public string? ProgramDescription { get; set; }
        public List<string> KeySkills { get; set; }
        public string ProgramBenefits { get; set; }
        public string ApplicationCriteria { get; set; }
        public ProgramInfoDto ProgramInfo { get; set; }

    }

    public class ProgramInfoDto
    {
        public ProgramType ProgramType { get; set; }
        public DateTime ProgramOpenDate { get; set; }
        public DateTime ApplicationOpenDate { get; set; }
        public DateTime ApplicationCloseDate { get; set; }
        public int DurationInWeeks { get; set; }
        public MinQualification MinQualification { get; set; }
        public int MaxNumberOfApplications { get; set; }

        private string programLocation;

        public string ProgramLocation
        {
            get
            {
                return programLocation;
            }
            set
            {
                if (value.Equals("Remote", StringComparison.OrdinalIgnoreCase))
                {
                    programLocation = "Remote";
                }
                else
                {
                    programLocation = value;
                }
            }
        }

    }

    public enum ProgramType
    {
        Internship,
        Job,
        Course,
        Webinar,
        MasterClass,
        Training,
        LiveSeminar,
        Volunteering,
        Other
    }

    public enum MinQualification
    {
        HighSchool,
        Graduate,
        College,
        Masters,
        University,
        PhD,
        Any
    }
}
