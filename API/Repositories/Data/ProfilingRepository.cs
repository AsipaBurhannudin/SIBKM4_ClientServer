using API.Context;
using API.Models;
using API.Repositories.Interface;
using System.Data;

namespace API.Repositories.Data
{
    public class ProfilingRepository : IProfilingRepository
    {
        private readonly MyContext _context;
        public ProfilingRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Profiling> GetAll()
        {
            return _context.Set<Profiling>().ToList();
        }

        public Profiling? GetById(string employeeNIK)
        {
            return _context.Set<Profiling>().Find(employeeNIK);
        }

        public int Insert(Profiling profiling)
        {
            _context.Set<Profiling>().Add(profiling);
            return _context.SaveChanges();
        }

        public int Update(Profiling profiling)
        {
            _context.Set<Profiling>().Update(profiling);
            return _context.SaveChanges();
        }

        public int Delete(string employeeNIK)
        {
            var profiling = _context.Set<Profiling>().Find(employeeNIK);
            if (profiling != null)
            {
                _context.Set<Profiling>().Remove(profiling);
                return _context.SaveChanges();
            }
            return 0;
        }
    }
}
