namespace Tasks.Application.Repositories
{
    public interface IDeleteTask
    {
        Task<bool> ExecuteAsync(Guid id);
    }

}