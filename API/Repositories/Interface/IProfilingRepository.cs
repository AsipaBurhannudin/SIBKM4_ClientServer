﻿using API.Models;

namespace API.Repositories.Interface
{
    public interface IProfilingRepository
    {
        IEnumerable<Profiling> GetAll();
        Profiling? GetById(string employeeNIK);
        int Insert(Profiling profiling);
        int Update(Profiling profiling);
        int Delete(string employeeNIK);
    }
}
