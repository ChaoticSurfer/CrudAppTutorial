using System.ComponentModel.DataAnnotations;

namespace CrudAppTutorial.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10, ErrorMessage = "Cant be longer then 10")]
        public string Name { get; set; }

        public List<Pet> Pets { get; set; }
    }
}
