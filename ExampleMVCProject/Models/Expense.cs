using System.ComponentModel.DataAnnotations;

namespace ExampleMVCProject.Models
{
    public class Expense
    {
        [Key, DataType("int")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!"),
            DataType("varchar"),
            MaxLength(150, ErrorMessage = "Name must be lower than 150 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Amount is required!"), 
            DataType("double"), 
            Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0!")]
        public double Amount { get; set; }

        [DataType("datetime")]
        public DateTime? PaidDate { get; set; }
    }
}
