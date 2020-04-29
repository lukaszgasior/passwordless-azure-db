namespace MiDemo.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EnsureThat;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MiDemo.Data;
    using MiDemo.Model;

    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly MiDbContext _dbContext;

        public SampleController(MiDbContext dbContext)
        {
            Ensure.Any.IsNotNull(dbContext);

            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<SampleTable>> Get()
        {
            return await ReadFromDatabase();
        }

        private async Task<List<SampleTable>> ReadFromDatabase()
        {
            return await _dbContext
                .SampleTable
                .ToListAsync();
        }
    }
}
