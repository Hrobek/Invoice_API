using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Data.Interfaces;
using Invoices.Data.Models;
using Invoices.Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Invoices.Api.Managers
{
    public class BaseManager<TDto, TEntity> : IBaseManager<TDto, TEntity> where TDto : class where TEntity : class, IEntity
    {
        private readonly IBaseRepository<TEntity> repository;
        private readonly IMapper mapper;


        public IList<TDto> GetAll()
        {
            var entities = repository.GetAll();
            return mapper.Map<IList<TDto>>(entities);
        }

        public virtual TDto? Get(ulong id)
        {
            var entity = repository.FindById(id);
            if (entity == null) return null;

            return mapper.Map<TDto>(entity);
        }

        public virtual TDto? Add(TDto dto)
        {
            TEntity entity = mapper.Map<TEntity>(dto);
            entity.Id = default;

            TEntity added = repository.Insert(entity);

            TEntity? found = repository.FindById(added.Id);

            return mapper.Map<TDto>(found);
        }

        public virtual TDto? Update(ulong id, TDto dto)
        {
            if (!repository.ExistsWithId(id))
                return null;

            var entity = mapper.Map<TEntity>(dto);
            var updatedEntity = repository.Update(entity);
            return mapper.Map<TDto>(updatedEntity);
        }

        public virtual TDto? Delete(ulong id)
        {
            if (!repository.ExistsWithId(id))
                return null;

            var entity = repository.FindById(id);
            var dto = mapper.Map<TDto>(entity);

            repository.Delete(id);
            return dto;
        }
    }
}
