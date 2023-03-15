using System.ComponentModel.DataAnnotations;

namespace CapitalPlacement.DTOs
{
    public class ApplicationFormDTO
    {
        public string? id { get; set; }
        public string ImageFilePath { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsDeleted { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public Profile? Profile { get; set; }
        public AdditionalQuestion? AdditionalQuestion { get; set; }

        public void UploadImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                var fileName = Path.GetFileName(imagePath);
                var destinationPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);
                File.Copy(imagePath, destinationPath);
                ImageFilePath = destinationPath;
                IsUploaded = true;
            }
            else
            {
                throw new ArgumentException("Image file not found.");
            }
        }

        public void PreviewImage()
        {
            if (IsUploaded && !IsDeleted)
            {
                // Show the preview of the uploaded image
                Console.WriteLine("Showing preview of the uploaded image.");
            }
        }

        public void DeleteImage()
        {
            if (IsUploaded && !IsDeleted)
            {
                if (File.Exists(ImageFilePath))
                {
                    File.Delete(ImageFilePath);
                    IsDeleted = true;
                }
            }
        }

        public void ReuploadImage(string imagePath)
        {
            if (IsUploaded && !IsDeleted)
            {
                DeleteImage();
            }
            UploadImage(imagePath);
        }

    }

    public class Question
    {
        public string Choice { get; set; }
    }

    public class AdditionalQuestion
    {
        [MaxLength(499)]
        public string AboutYourself { get; set; }
        [Range(1900, 2100)]
        public int YearOfGraduation { get; set; }
        public string EverBeenRejected { get; set; }
        public Question? question { get; set; }

    }


    public class Profile
    {
        [MandatoryIf(true, ErrorMessage = "Education is required.")]
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Resume { get; set; }
        public Questions? question { get; set; }
    }

    public class Questions
    {
        public string Type { get; set; }
        public string Question { get; set; }
        public string Choice { get; set; }
    }

    public class PersonalInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string AddrCurentResidence { get; set; }
        public string IdNumber { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public Questions? AddQuestion { get; set; }


    }


    /*
     *  property mandatory only when the toggle button is turned on 
     */
    public class MandatoryIfAttribute : ValidationAttribute
    {
        private readonly bool _condition;

        public MandatoryIfAttribute(bool condition)
        {
            _condition = condition;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_condition && value == null)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
