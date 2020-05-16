using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace identityserver.Storage
{
    public class RepositoryPersistedGrantStore : IPersistedGrantStore
    {
        protected IRepository _repository;

        public RepositoryPersistedGrantStore(IRepository repository)
            => _repository = repository;

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
            => Task.FromResult(_repository.Where<PersistedGrant>(i => i.SubjectId == subjectId).AsEnumerable());

        public Task<PersistedGrant> GetAsync(string key)
            => Task.FromResult(_repository.Single<PersistedGrant>(i => i.Key == key));

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            _repository.Delete<PersistedGrant>(i => i.SubjectId == subjectId && i.ClientId == clientId);
            return Task.CompletedTask;
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            _repository.Delete<PersistedGrant>(i => i.SubjectId == subjectId && i.ClientId == clientId && i.Type == type);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _repository.Delete<PersistedGrant>(i => i.Key == key);
            return Task.CompletedTask;
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            _repository.Add<PersistedGrant>(grant);
            return Task.CompletedTask;
        }
    }
}