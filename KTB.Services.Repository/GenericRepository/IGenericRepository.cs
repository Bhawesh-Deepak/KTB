using KTB.Core.ReqRespVm.GenericReponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.Services.Repository.GenericRepository
{
    public interface IGenericRepository<TEntity,T> where TEntity:class
    {
        Task<GenericResponseModel<TEntity, T>> GetAllEntities(Func<TEntity, bool> where);
        Task<GenericResponseModel<TEntity, T>> GetAllEntityById(T Id);
        Task<GenericResponseModel<TEntity, T>> CreateEntities(TEntity[] model);
        Task<GenericResponseModel<TEntity, T>> CreateEntity(TEntity model);
        Task<GenericResponseModel<TEntity, T>> UpdateEntity(TEntity model);
        Task<GenericResponseModel<TEntity, T>> DeleteEntity(params TEntity[] items);
        Task<GenericResponseModel<TEntity, T>> CheckIsExists(Func<TEntity, bool> where);
    }
}
