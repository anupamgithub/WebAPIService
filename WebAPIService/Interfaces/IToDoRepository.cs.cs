using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIService.Model;

namespace WebAPIService
{
    public interface IToDoRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<TodoItem> All { get; }
        TodoItem Find(string id);
        void Insert(TodoItem item);
        void Update(TodoItem item);
        void Delete(string id);
    }
}
