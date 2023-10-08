using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Knowledge.Api.Database;
using System.Threading.Tasks;
using Dto = Knowledge.Models.Models.DTOs;
using Knowledge.Api.Utils;

namespace Knowledge.Api.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Dto.PagedResults<Dto.UserEnumerable>> Get(string search, int page, int limit)
        {
            var query = from u in _dbContext.Users
                        where  (string.IsNullOrEmpty(search)
                                || (u.Email.ToUpper().Contains(search.Trim().ToUpper())
                                    || (u.FirstName + " " + u.LastName).ToUpper().Contains(search.Trim().ToUpper())))
                        select new Dto.UserEnumerable
                        {
                            Id = u.Id,
                            Created = u.Created,
                            Email = u.Email,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            //LastActivity = (from s in _dbContext.UserSessions
                            //                where s.UserId == u.Id
                            //                orderby s.Expiration descending
                            //                select s.Created.Add(s.Expiration.Subtract(s.InitialExpiration))).FirstOrDefault()
                        };

            return query.ToPagedUserEnumerable(page, limit);
        }
    }
}

public static class IEqueryableUserExtensions
{
    public static Task<Dto.PagedResults<Dto.User>> ToPagedUsersList(this IQueryable<Dto.User> query, int page, int limit)
    {
        return query.OrderByDescending(u => u.Created).ToPagedResults(page, limit);
    }

    public static Task<Dto.PagedResults<Dto.UserEnumerable>> ToPagedUserEnumerable(this IQueryable<Dto.UserEnumerable> query, int page, int limit)
    {
        return query.OrderByDescending(u => u.Created).ToPagedResults(page, limit);
    }
}
