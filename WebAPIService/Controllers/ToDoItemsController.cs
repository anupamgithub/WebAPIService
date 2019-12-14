using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIService;
using WebAPIService.Model;


namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : Controller
    {
        public  IToDoRepository _toDoRepository;
        private readonly TodoContext _context;
       

        //public ToDoItemsController(IToDoRepository toDoRepository)
        //{
        //    _toDoRepository = toDoRepository;
        //}

        public ToDoItemsController(TodoContext context, IToDoRepository toDoRepository)
        {
            _context = context;
            _toDoRepository = toDoRepository;
        }


       [HttpGet]
  
        public IActionResult List()
        {
            return Ok(_toDoRepository.All);
        }

        //[HttpPost]
        //public IActionResult Create([FromBody] TodoItem item)
        //{
        //    //try
        //    //{
        //    //    if (item == null || !ModelState.IsValid)
        //    //    {
        //    //        return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
        //    //    }
        //    //    bool itemExists = _toDoRepository.DoesItemExist(item.Id);
        //    //    if (itemExists)
        //    //    {
        //    //        return StatusCode(StatusCodes.Status409Conflict, ErrorCode.TodoItemIDInUse.ToString());
        //    //    }
        //    //    _toDoRepository.Insert(item);
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
        //    //}
        //    //return Ok(item);

        //    return View(Create);
        //}

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: Test
       // [HttpGet]
        public async Task<IActionResult> Index()
        {
         
            return View(await _context.TodoItems.ToListAsync());
        }

        [HttpPut]
        public IActionResult Edit([FromBody] TodoItem item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
                }
                var existingItem = _toDoRepository.Find(item.Id);
                if (existingItem == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _toDoRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var item = _toDoRepository.Find(id);
                if (item == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _toDoRepository.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
            }
            return NoContent();
        }
    }

    public enum ErrorCode
    {
        TodoItemNameAndNotesRequired,
        TodoItemIDInUse,
        RecordNotFound,
        CouldNotCreateItem,
        CouldNotUpdateItem,
        CouldNotDeleteItem
    }

}