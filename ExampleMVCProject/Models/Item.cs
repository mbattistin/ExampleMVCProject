using System.ComponentModel.DataAnnotations;

namespace ExampleMVCProject.Models
{
	public class Item
	{
		[Key, DataType("int")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!"), 
            DataType("varchar"), 
            MaxLength(150, ErrorMessage = "Name must be lower than 150 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Borrower is required!"), 
            DataType("varchar"), 
            MaxLength(300, ErrorMessage = "Borrower must be lower than 300 characters!")]
        public string Borrower { get; set; }

        [Required(ErrorMessage = "Lender is required!"), 
            DataType("varchar"), 
            MaxLength(300, ErrorMessage = "Lender must be lower than 300 characters!")]
        public string Lender { get; set; }

        [DataType("datetime")]
        public DateTime? ReturnedDate { get; set; }
    }
}
