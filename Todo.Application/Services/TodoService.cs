using AutoMapper;
using System.Linq.Expressions;
using ToDoList.Application.Interfaces;
using ToDoList.Core.DB;
using ToDoList.Core.Entities;
using ToDoList.Shared.DTOs;

namespace Todo.Application.Services
{
    public class ToDoServices(ITodoRepository<ToDoItem> _todoRepository, ApplicationDbContext _dbContext, IMapper _mapper)
    {
        //private readonly ITodoRepository<ToDoItem> _todoRepository;
        //private readonly ApplicationDbContext _dbContext;
        //private readonly IMapper _mapper;

        //public ToDoServices(ITodoRepository<ToDoItem> todoRepository, ApplicationDbContext dbContext, IMapper mapper)
        //{
        //    _todoRepository = todoRepository;
        //    _dbContext = dbContext;
        //    _mapper = mapper;
        //}

        public async Task<IEnumerable<ToDoItemDTO>> GetAllAsync(Expression<Func<ToDoItem, bool>>? filter = null)
        {
            var items = await _todoRepository.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<ToDoItemDTO>>(items);
        }

        public async Task<ToDoItemDTO> GetByIdAsync(int id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            return _mapper.Map<ToDoItemDTO>(item);
        }

        public async Task AddAsync(ToDoItemDTO itemDto)
        {
            var item = _mapper.Map<ToDoItem>(itemDto);
            await _todoRepository.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDoItemDTO itemDto)
        {
            var item = await _todoRepository.GetByIdAsync(itemDto.Id);
            _mapper.Map(itemDto, item);
            _todoRepository.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            _todoRepository.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
