using CrudAppTutorial.Dtos;
using CrudAppTutorial.Models;
using Riok.Mapperly.Abstractions;

namespace CrudAppTutorial
{
    [Mapper]
    public partial class Mapper
    {
        public partial Person DtoToPerson(PersonDto car);
        public partial Pet DtoToPet(CreatePetDto car);
    }
}
