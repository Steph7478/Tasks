using Tasks.Application.DTOs;
using Tasks.Application.Mappers;
using Tasks.Domain.Repositories;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Application.Usecases
{
    public class GetAllTasksUseCase(ITaskRepository taskRepository)
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<IEnumerable<TaskResponseDTO>> ExecuteAsync()
        {
            List<DomainTask> tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(TaskMapper.ToDTO);
        }
    }
}

// IMPORTANT!! - Notes to not forget:

// IEnumerable<T>
// It’s a generic interface used to iterate through collections.
// Execution is lazy, meaning the code only runs when you actually iterate over the elements (like in a foreach).
// You can’t modify the data (you can’t add or remove items).
// Filters (Where, Select, etc.) are performed in memory, not in the database.
// It’s ideal for iterating over already-fetched results — for example, when data has already been retrieved from the database and you just need to loop through it in code.

// IQueryable<T>
// It’s a LINQ interface that inherits from IEnumerable, but it’s specifically designed to work with databases.
// It’s also lazy, but the difference is that execution happens in the database, not in memory.
// That means filters (Where, Select, OrderBy, etc.) are translated into SQL by Entity Framework before execution.
// You can’t directly modify the data — it’s for querying only.
// It’s ideal for EF Core queries, because it prevents loading unnecessary data into memory.

// List<T>
// It’s a concrete class (not an interface) that stores data entirely in memory.
// Execution is immediate, meaning all data is available as soon as the list is created.
// You can freely modify it — you can add (Add), remove (Remove), and access items by index (list[0]).
// Filters and operations (Where, Select) are done in memory.
// It’s ideal for manipulating collections of already-loaded objects, such as temporary lists used in your code.

// T[] (Array)
// It’s a fixed structure in .NET — the most basic type of collection.
// Execution is also immediate, but the size is fixed, meaning you can’t add or remove elements after it’s created.
// Filters and operations are performed in memory.
// It’s extremely fast and lightweight, ideal when you know exactly how many elements you’ll have and want maximum performance.
// It’s great for fixed-size data, such as a constant set of values.