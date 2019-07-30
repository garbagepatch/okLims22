using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataAccess
{
    public interface IRequestRepository
    {
        Model.Request InsertOrUpdate(Model.Request doc);
        void DeleteEmptyPreHistory(Guid processId);
        List<Model.Request> Get(out int count, int page = 0, int pageSize = 128);
        List<Model.Request> GetInbox(Guid identityId, out int count, int page = 0, int pageSize = 128);
        List<Model.Request> GetOutbox(Guid identityId, out int count, int page = 0, int pageSize = 128);
        List<Model.RequestTransitionHistory> GetHistory(Guid id);
        Model.Request Get(Guid id, bool loadChildEntities = true);
        void Delete(Guid[] ids);
        void ChangeState(Guid id, string nextState, string nextStateName);
        bool IsAuthorsBoss(Guid RequestId, Guid identityId);
        IEnumerable<string> GetAuthorsBoss(Guid RequestId);
        void WriteTransitionHistory(Guid id, string currentState, string nextState, string command, IEnumerable<string> identities);
        void UpdateTransitionHistory(Guid id, string currentState, string nextState, string command, Guid? employeeId);
    }
}
