using API.Models;

namespace API.Repositories.Interface
{
    public interface IUniversityRepository : IGeneralRepository<University, int>
    {
        IEnumerable<University> GetByName(string name);
    }
}
