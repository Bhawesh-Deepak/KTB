using KTB.Core.Entities.Common;
using KTB.Core.ReqRespVm.GenericReponse;
using KTB.Services.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.Services.Implementation.GenericService
{
    public class GenericImplementation<TEntity, T> : IGenericRepository<TEntity, T> where TEntity : class
    {
        private readonly KTBContext context;
        private readonly DbSet<TEntity> TEntities;

        /// <summary>
        /// Constructore to configure the Data base connection string
        /// </summary>
        /// <param name="configuration"></param>
        public GenericImplementation(IConfiguration configuration)
        {
            context = new KTBContext(configuration);
            TEntities = context.Set<TEntity>();
        }

        /// <summary>
        /// Check the Item present on to the data base or not
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<GenericResponseModel<TEntity, T>> CheckIsExists(Func<TEntity, bool> where)
        {
            try
            {
                TEntity item = null;
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();
                item = dbQuery.AsNoTracking().FirstOrDefault(where);

                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                    .GetGenericResponse(null, item, "success", default,
                    StatusCode.success));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                   .GetGenericResponse(null, null, ex.Message, default,
                   StatusCode.ExceptionOccured));
            }
        }

        /// <summary>
        /// Create multitple entity to the database object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<GenericResponseModel<TEntity, T>> CreateEntities(TEntity[] model)
        {
            try
            {
                await TEntities.AddRangeAsync(model);
                await context.SaveChangesAsync();

                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                    .GetGenericResponse(null, null, "success", default,
                    StatusCode.success));
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The duplicate key "))
                {
                    return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                        .GetGenericResponse(null, null, ex.Message, default,
                        StatusCode.AlreadyExists));
                }

                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                 .GetGenericResponse(null, null, ex.Message, default,
                 StatusCode.ExceptionOccured));
            }
        }

        /// <summary>
        /// Create single entity to the data base
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<GenericResponseModel<TEntity, T>> CreateEntity(TEntity model)
        {
            try
            {
                await TEntities.AddAsync(model);
                await context.SaveChangesAsync();

                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                    .GetGenericResponse(null, null, "success", default,
                    StatusCode.success));
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("The duplicate key "))
                {
                    return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                        .GetGenericResponse(null, null, ex.Message, default,
                        StatusCode.AlreadyExists));
                }

                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                 .GetGenericResponse(null, null, ex.Message, default,
                 StatusCode.ExceptionOccured));
            }
        }

        /// <summary>
        /// Remove entity from Data base object
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<GenericResponseModel<TEntity, T>> DeleteEntity(params TEntity[] items)
        {
            try
            {
                using (context)
                {
                    context.UpdateRange();
                    await context.SaveChangesAsync();
                }

                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                 .GetGenericResponse(null, null, "success", default,
                 StatusCode.Deleted));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                 .GetGenericResponse(null, null, ex.Message, default,
                 StatusCode.ExceptionOccured));
            }
        }

        /// <summary>
        /// Get All active entity from Data base
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<GenericResponseModel<TEntity, T>> GetAllEntities(Func<TEntity, bool> where)
        {
            try
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();
                var tList = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();

                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                 .GetGenericResponse(tList, null, "success", default,
                 StatusCode.success));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                   .GetGenericResponse(null, null, ex.Message, default,
                   StatusCode.ExceptionOccured));

            }
        }

        /// <summary>
        /// Get Specific entity from the Data base
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<GenericResponseModel<TEntity, T>> GetAllEntityById(T Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the entity to the data base
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<GenericResponseModel<TEntity, T>> UpdateEntity(TEntity model)
        {
            try
            {
                context.Update(model);
                await context.SaveChangesAsync();

                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                 .GetGenericResponse(null, null, "success", default,
                 StatusCode.Updated));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new GenericResponseModel<TEntity, T>()
                 .GetGenericResponse(null, null, ex.Message, default,
                 StatusCode.ExceptionOccured));
            }
        }
    }
}
