using Infrastructure.SqlServer.System;
using Microsoft.AspNetCore.Mvc;

namespace NisolNicole.Controllers
{
    //Here we declare our Database route
    [ApiController]
    [Route("api/database")]
    public class DatabaseController : ControllerBase
    {
        private readonly IHostEnvironment _environment;
        private readonly IDatabaseManager _databaseManager;

        public DatabaseController(IDatabaseManager databaseManager, IHostEnvironment environment)
        {
            _databaseManager = databaseManager;
            _environment = environment;
        }

        [HttpGet]
        [Route("init")]
        /*This method will call the init file that creates the tables in our database*/
        public IActionResult CreateDatabaseAndTables()
        {

            if (_environment.IsProduction())
                return BadRequest("Only in dev");

            _databaseManager.CreateDatabaseAndTables();
            return Ok("Database and tables created successfully");
        }

        [HttpGet]
        [Route("fill")]
        /*This method will call the Data file that fills our tables in our database*/
        public IActionResult FillTables()
        {
            if (_environment.IsProduction())
                return BadRequest("Only in dev");
            _databaseManager.FillTables();
            return Ok("Table have been filled");
        }
    }
}