using System.Collections.Generic;
using System.Linq;

using WebAPIService;
using WebAPIService.Model;

namespace ToDoApi.Services
{
    public class ToDoRepository : IToDoRepository
    {
        private List<TodoItem> _toDoList;

        public ToDoRepository()
        {
            InitializeData();
        }

        public IEnumerable<TodoItem> All
        {
            get { return _toDoList; }
        }

        public bool DoesItemExist(string id)
        {
            return _toDoList.Any(item => item.Id == id);
        }

        public TodoItem Find(string id)
        {
            return _toDoList.FirstOrDefault(item => item.Id == id);
        }

        public void Insert(TodoItem item)
        {
            _toDoList.Add(item);
        }

        public void Update(TodoItem item)
        {
            var todoItem = this.Find(item.Id);
            var index = _toDoList.IndexOf(todoItem);
            _toDoList.RemoveAt(index);
            _toDoList.Insert(index, item);
        }

        public void Delete(string id)
        {
            _toDoList.Remove(this.Find(id));
        }

        private void InitializeData()
        {
            _toDoList = new List<TodoItem>();

            var todoItem1 = new TodoItem
            {
                Id = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                Name = "Learn app development",
              
            };

            var todoItem2 = new TodoItem
            {
                Id = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                Name = "Develop apps",
              
            };

            var todoItem3 = new TodoItem
            {
                Id = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                Name = "Publish apps",
               
            };

            _toDoList.Add(todoItem1);
            _toDoList.Add(todoItem2);
            _toDoList.Add(todoItem3);
        }
    }
}