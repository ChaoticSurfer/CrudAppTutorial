using Riok.Mapperly.Abstractions;

namespace CrudAppTutorial
{
    [Mapper]
    public partial class PersonMapper
    {
        public partial Person DtoToPerson(PersonDto car);
    }
}
