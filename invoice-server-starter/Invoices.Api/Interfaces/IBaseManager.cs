namespace Invoices.Api.Interfaces
{
    public interface IBaseManager<TDto>
    {
        IList<TDto> GetAll();
        TDto? Get(uint id);
        TDto Add(TDto dto);
        TDto? Update(uint id, TDto dto);
    }
}
