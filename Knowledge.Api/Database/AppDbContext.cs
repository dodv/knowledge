
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Knowledge.Models.Database;
using Knowledge.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Knowledge.Api.Database
{
    public class AppDbContext : BaseAppDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GuidService _guidService;

        public AppDbContext(
           DbContextOptions<AppDbContext> options,
           IHttpContextAccessor httpContextAccessor,
           GuidService guidService
       ) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _guidService = guidService;
        }
    }
}
