using konyvtar.Contracts;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class LoanDTO
{

    public int Id { get; set; }


    public int ReaderId { get; set; }

    public int BookId { get; set; }


    [Required(ErrorMessage = "Kölcsönzés ideje kötelező.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Range(typeof(DateTime), "2023-12-12", "2023-12-31", ErrorMessage = "Nem lehet a jelenlegi napnál korábbi.")]
    public DateTime BorrowDate { get; set; }

    [Required(ErrorMessage = "Visszahozási határidő kötelező.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DateLessThan("BorrowDate", ErrorMessage = "Visszahozás határideje később kell legyen, mint a kölcsönzés ideje.")]
    public DateTime ReturnDeadline { get; set; }

   

}