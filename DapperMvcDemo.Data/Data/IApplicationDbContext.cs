namespace DapperMvcDemo.Data.Data
{
    public  interface IApplicationDbContext
    {
        Task<IEnumerable<T>> getData<T, P>(string spName, P parameters, string connectionId = "DefaultConnection");
        Task SaveData<T>(string spName, T parameters, string connectionId = "DefaultConnection");

    }
}