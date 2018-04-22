namespace OddsFetchingEntities.Mappers
{
    public interface IMapper<Tin, Tout>
    {
        Tout Map(Tin input);
    }
}
