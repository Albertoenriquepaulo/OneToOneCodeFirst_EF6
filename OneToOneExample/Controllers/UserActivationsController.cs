using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToOneExample.Data;
using OneToOneExample.Models;

namespace OneToOneExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivationsController : ControllerBase
    {
        private readonly OneToOneDbContext _context;

        public UserActivationsController(OneToOneDbContext context)
        {
            _context = context;
        }

        // GET: api/UserActivations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivation>>> GetUserActivation()
        {
            return await _context.UserActivation.ToListAsync();
        }

        // GET: api/UserActivations/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserActivation>> GetUserActivation(Guid userId)
        {
            //var userActivation = await _context.UserActivation.FindAsync(ua=> ua.us);
            var userActivation = await _context.UserActivation.FirstOrDefaultAsync(ua => ua.UserId == userId);

            if (userActivation == null)
            {
                return NotFound();
            }

            return userActivation;
        }

        // PUT: api/UserActivations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut()]
        public async Task<IActionResult> PutUserActivation(UserActivation userActivation)
        {

            if (!UserActivationExists(userActivation.UserId))
            {
                return NotFound();
            }

            // Con el include me traigo el UserActivations que es lo que realmente encesito actualizar
            var user = await _context.User.Include(ua => ua.UserActivation).FirstOrDefaultAsync(u => u.Id == userActivation.UserId);

            // Ahora debo actualziar el campo active del UserActivation que está en la base de datos
            // que es el que queremos actualizar según el valor recibido como parametro en la variable
            // 'userActivation'
            user.UserActivation.Active = userActivation.Active;


            _context.Entry(userActivation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/UserActivations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserActivation>> PostUserActivation(UserActivation userActivation)
        {
            var user = await _context.User.FindAsync(userActivation.UserId);
            if (user == null)
            {
                return NotFound("There is not User under that Id in our database");
            }

            userActivation.Id = Guid.NewGuid();
            //userActivation.User = user;

            _context.UserActivation.Add(userActivation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserActivationExists(userActivation.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var result = new
            {
                id = userActivation.Id,
                userId = userActivation.UserId,
                FullName = $"{userActivation.User.Name} {userActivation.User.Lastname} ({userActivation.User.UserName})",
                Active = userActivation.Active
            };

            return CreatedAtAction("GetUserActivation", result);
            //return CreatedAtAction("GetUserActivation", new { id = userActivation.Id }, userActivation);
        }

        private bool UserActivationExists(Guid userId)
        {
            return _context.UserActivation.Any(e => e.UserId == userId);
        }
    }
}
