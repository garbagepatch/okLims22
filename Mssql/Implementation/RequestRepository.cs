using Business.DataAccess;
using Microsoft.EntityFrameworkCore;
using Mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Mssql.Implementation
{
    public class RequestRepository : IRequestRepository
    {
        private readonly SampleContext _sampleContext;

        public RequestRepository(SampleContext sampleContext)
        {
            _sampleContext = sampleContext;
        }

        public void ChangeState(Guid id, string nextState,  string nextStateName)
        {
            var Request = GetRequest(id);
            if (Request == null)
                return;

            Request.State = nextState;
            Request.StateName = nextStateName;
            
            _sampleContext.SaveChanges();
        }

        public void Delete(Guid[] ids)
        {
            var objs = _sampleContext.Requests.Where(x => ids.Contains(x.Id));
             
            _sampleContext.Requests.RemoveRange(objs);

            _sampleContext.SaveChanges();
        }

        public void DeleteEmptyPreHistory(Guid processId)
        {
            var existingNotUsedItems =
                   _sampleContext.RequestTransitionHistories.Where(
                       dth =>
                       dth.RequestId == processId && !dth.TransitionTime.HasValue);

            _sampleContext.RequestTransitionHistories.RemoveRange(existingNotUsedItems);

            _sampleContext.SaveChanges();
        }

        public List<Business.Model.Request> Get(out int count, int page = 0, int pageSize = 128)
        {
            int actual = page * pageSize;
            var query = _sampleContext.Requests.OrderByDescending(c => c.Number);
            count = query.Count();
            return query.Include(x => x.Author)
                        .Include(x => x.Manager)
                        .Skip(actual)
                        .Take(pageSize)
                        .ToList()
                        .Select(d => Mappings.Mapper.Map<Business.Model.Request>(d)).ToList();
        }

        public IEnumerable<string> GetAuthorsBoss(Guid RequestId)
        {
            var Request = _sampleContext.Requests.Find(RequestId);
            if (Request == null)
                return new List<string> { };

            return
                _sampleContext.VHeads.Where(h => h.Id == Request.AuthorId)
                    .Select(h => h.HeadId)
                    .ToList()
                    .Select(c => c.ToString());
        }

        public List<Business.Model.RequestTransitionHistory> GetHistory(Guid id)
        {
            DateTime orderTime = new DateTime(9999, 12, 31);

            return _sampleContext.RequestTransitionHistories
                 .Include(x => x.Employee)
                 .Where(h => h.RequestId == id)
                 .OrderBy(h => h.TransitionTime == null ? orderTime : h.TransitionTime.Value)
                 .ThenBy(h => h.Order)
                 .ToList()
                 .Select(x => Mappings.Mapper.Map<Business.Model.RequestTransitionHistory>(x)).ToList();
        }

        public List<Business.Model.Request> GetInbox(Guid identityId, out int count, int page = 0, int pageSize = 128)
        {
            var strGuid = identityId.ToString();
            int actual = page * pageSize;
            var subQuery = _sampleContext.WorkflowInboxes.Where(c => c.IdentityId == strGuid);

            var query = _sampleContext.Requests.Include(x => x.Author)
                                                .Include(x => x.Manager)
                                                .Where(c => subQuery.Any(i => i.ProcessId == c.Id));
            count = query.Count();
            return query.OrderByDescending(c => c.Number).Skip(actual).Take(pageSize)
                        .ToList()
                        .Select(d => Mappings.Mapper.Map<Business.Model.Request>(d)).ToList();
        }

        public List<Business.Model.Request> GetOutbox(Guid identityId, out int count, int page = 0, int pageSize = 128)
        {
            int actual = page * pageSize;
            var subQuery = _sampleContext.RequestTransitionHistories.Where(c => c.EmployeeId == identityId);
            var query = _sampleContext.Requests.Include(x => x.Author)
                                                .Include(x => x.Manager)
                                                .Where(c => subQuery.Any(i => i.RequestId == c.Id));
            count = query.Count();
            return query.OrderByDescending(c => c.Number).Skip(actual).Take(pageSize)
                .ToList()
                .Select(d => Mappings.Mapper.Map<Business.Model.Request>(d)).ToList();
        }

        public Business.Model.Request InsertOrUpdate(Business.Model.Request doc)
        {
            Request target = null;
            if (doc.Id != Guid.Empty)
            {
                target = _sampleContext.Requests.Find(doc.Id);
                if (target == null)
                {
                    return null;
                }
            }
            else
            {
                target = new Request
                {
                    Id = Guid.NewGuid(),
                    AuthorId = doc.AuthorId,
                    StateName = doc.StateName
                };
                _sampleContext.Requests.Add(target);
            }

            target.Name = doc.Name;
            target.ManagerId = doc.ManagerId;
            target.Comment = doc.Comment;
            target.Sum = doc.Sum;

            _sampleContext.SaveChanges();

            doc.Id = target.Id;
            doc.Number = target.Number;

            return doc;
        }

        public bool IsAuthorsBoss(Guid RequestId, Guid identityId)
        {
            var Request = _sampleContext.Requests.Find(RequestId);
            if (Request == null)
                return false;
            return _sampleContext.VHeads.Count(h => h.Id == Request.AuthorId && h.HeadId == identityId) > 0;
        }

        public void UpdateTransitionHistory(Guid id, string currentState, string nextState, string command, Guid? employeeId)
        {
            var historyItem =
              _sampleContext.RequestTransitionHistories.FirstOrDefault(
                  h => h.RequestId == id && !h.TransitionTime.HasValue &&
                  h.InitialState == currentState && h.DestinationState == nextState);

            if (historyItem == null)
            {
                historyItem = new RequestTransitionHistory
                {
                    Id = Guid.NewGuid(),
                    AllowedToEmployeeNames = string.Empty,
                    DestinationState = nextState,
                    RequestId = id,
                    InitialState = currentState
                };

                _sampleContext.RequestTransitionHistories.Add(historyItem);

            }

            historyItem.Command = command;
            historyItem.TransitionTime = DateTime.Now;
            historyItem.EmployeeId = employeeId;

            _sampleContext.SaveChanges();
        }

        public void WriteTransitionHistory(Guid id, string currentState, string nextState, string command, IEnumerable<string> identities)
        {
            var historyItem = new RequestTransitionHistory
            {
                Id = Guid.NewGuid(),
                AllowedToEmployeeNames = GetEmployeesString(identities),
                DestinationState = nextState,
                RequestId = id,
                InitialState = currentState,
                Command = command
            };

            _sampleContext.RequestTransitionHistories.Add(historyItem);
            _sampleContext.SaveChanges();
        }

        public Business.Model.Request Get(Guid id, bool loadChildEntities = true)
        {
            Request Request = GetRequest(id, loadChildEntities);
            if (Request == null) return null;
            return Mappings.Mapper.Map<Business.Model.Request>(Request);
        }

        private Request GetRequest(Guid id, bool loadChildEntities = true)
        {
            Request Request = null;

            if (!loadChildEntities)
            {
                Request = _sampleContext.Requests.Find(id);
            }
            else
            {
                Request = _sampleContext.Requests
                                         .Include(x => x.Author)
                                         .Include(x => x.Manager).FirstOrDefault(x => x.Id == id);
            }

            return Request;

        }

        private string GetEmployeesString(IEnumerable<string> identities)
        {
            var identitiesGuid = identities.Select(c => new Guid(c));

            var employees = _sampleContext.Employees.Where(e => identitiesGuid.Contains(e.Id)).ToList();

            var sb = new StringBuilder();
            bool isFirst = true;
            foreach (var employee in employees)
            {
                if (!isFirst)
                    sb.Append(",");
                isFirst = false;

                sb.Append(employee.Name);
            }

            return sb.ToString();
        }
    }
}
