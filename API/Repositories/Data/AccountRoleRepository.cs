﻿using API.Context;
using API.Models;
using API.Repositories.Interface;
using System.Data;

namespace API.Repositories.Data
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly MyContext _context;
        public AccountRoleRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList();
        }

        public AccountRole GetById(int id)
        {
            return _context.Set<AccountRole>().FirstOrDefault(x => x.Id == id);
        }

        public int Insert(AccountRole accountRole)
        {
            _context.Set<AccountRole>().Add(accountRole);
            return _context.SaveChanges();
        }

        public int Update(AccountRole accountRole)
        {
            _context.Set<AccountRole>().Update(accountRole);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var accountRole = GetById(id);
            if (accountRole != null)
            {
                _context.Set<AccountRole>().Remove(accountRole);
                return _context.SaveChanges();
            }
            return 0;
        }
    }
}