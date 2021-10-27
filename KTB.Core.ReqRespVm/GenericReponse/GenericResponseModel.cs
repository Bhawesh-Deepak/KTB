using KTB.Core.Entities.Common;
using System.Collections.Generic;

namespace KTB.Core.ReqRespVm.GenericReponse
{
    public class GenericResponseModel<TEntity, T> where TEntity : class
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public TEntity Entity { get; set; }
        public string Message { get; set; }
        public T EntityId { get; set; }
        public StatusCode ResponseStatus { get; set; }

        public GenericResponseModel<TEntity, T> GetGenericResponse(IEnumerable<TEntity> entities,
            TEntity entity, string message, T entityId, StatusCode responseStatus)
        {

            return new GenericResponseModel<TEntity, T>()
            {
                Entities = entities,
                Entity = entity,
                Message = message,
                EntityId = entityId,
                ResponseStatus = responseStatus
            };
        }
    }
}
