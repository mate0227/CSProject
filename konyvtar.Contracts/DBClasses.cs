using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace konyvtar.Contracts
{

    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required(ErrorMessage = "A könyv címe kötelező.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A könyv szerzője kötelező.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "A kiadó kötelező.")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "A kiadás éve kötelező.")]
        [Range(0, int.MaxValue, ErrorMessage = "Az érték nem lehet negatív.")]
        public int PublicationYear { get; set; }
    }

    public class Reader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Az olvasó nevének megadása kötelező.")]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "A születési dátum megadása kötelező.")]
        [Range(typeof(DateTime), "1900-01-01", "2022-12-31", ErrorMessage = "A születési év nem lehet kisebb mint 1900 és nagyobb mint 2022.")]
        public DateTime BirthDate { get; set; }


    }

    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Reader))]
        public int ReaderId { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }

        [Required(ErrorMessage = "A kölcsönzés idejének megadása kötelező.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1900-01-01", "2023-12-31", ErrorMessage = "Nem lehet a jelenlegi napnál korábbi.")]
        public DateTime BorrowDate { get; set; }

        [Required(ErrorMessage = "A visszahozási határidő megadása kötelező.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateGreaterThan("BorrowDate", ErrorMessage = "A visszahozás ideje később kell legyen, mint a kölcsönzés ideje.")]
        public DateTime ReturnDeadline { get; set; }


        public virtual Reader Reader { get; set; }
        public virtual Book Book { get; set; }
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (propertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = propertyInfo.GetValue(validationContext.ObjectInstance) as IComparable;

            if (comparisonValue == null)
            {
                return new ValidationResult($"The property {_comparisonProperty} is not IComparable");
            }

            return value != null && (value as IComparable).CompareTo(comparisonValue) <= 0
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {_comparisonProperty}.");
        }
    }
}
