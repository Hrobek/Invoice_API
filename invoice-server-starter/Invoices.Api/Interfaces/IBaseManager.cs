using Microsoft.AspNetCore.Mvc;

namespace Invoices.Api.Interfaces
{
    public interface IBaseManager<TDto, TEntity> where TDto : class where TEntity : class
    {
        IList<TDto> GetAll();
        TDto? Get(ulong id);
        TDto Add(TDto dto);
        TDto? Update(ulong id, TDto dto);
        TDto? Delete(ulong id);
    }
}
