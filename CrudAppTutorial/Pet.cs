using System.ComponentModel.DataAnnotations;

namespace CrudAppTutorial
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public Person Owner { get; set; }
    }
}
