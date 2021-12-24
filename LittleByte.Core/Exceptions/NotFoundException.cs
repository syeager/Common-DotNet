using System;
using System.Net;

namespace LittleByte.Core.Exceptions
{
    public class NotFoundException : HttpException
    {
        public Type EntityType { get; }
        public string KeyName { get; }
        public string EntityKey { get; }

        public NotFoundException(Type entityType, Guid entityId, Exception? innerException = null)
            : this(entityType, entityId.ToString(), "Id", innerException)
        {
        }

        public NotFoundException(Type entityType, string entityKey, string keyName, Exception? innerException = null)
            : base(HttpStatusCode.NotFound, $"Could not find '{entityType.Name}' with '{keyName}' '{entityKey}'", innerException)
        {
            EntityType = entityType;
            KeyName = keyName;
            EntityKey = entityKey;
        }
    }
}