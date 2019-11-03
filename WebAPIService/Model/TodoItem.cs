using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIService.Model
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

    }
}
