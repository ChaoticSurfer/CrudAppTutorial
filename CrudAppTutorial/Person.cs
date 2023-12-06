using System.ComponentModel.DataAnnotations;

namespace CrudAppTutorial
{
    public class Person
    {
        public int Id { get; set; }

        [MaxLength(10, ErrorMessage = "Cant be longer then 10" )]
        public string Name { get; set; }
    }
}
