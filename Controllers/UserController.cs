using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin.Auth;
using System.Threading.Tasks;

namespace BackendUsuario.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly FirebaseAuth _firebaseAuth;

        public AuthController(FirebaseAuth firebaseAuth)
        {
            _firebaseAuth = firebaseAuth;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(string email, string password)
        {
            try
            {
                var userRecord = await _firebaseAuth.CreateUserAsync(new UserRecordArgs
                {
                    Email = email,
                    Password = password
                });

                return Ok(userRecord);
            }
            catch (FirebaseAuthException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync(string idToken)
        {
            try
            {
                var token = await _firebaseAuth.VerifyIdTokenAsync(idToken);
                return Ok(token);
            }
            catch (FirebaseAuthException e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("signout")]
        public async Task<IActionResult> SignOutAsync()
        {
            // Implementa la lógica para cerrar sesión del usuario
            return Ok("Usuario desconectado correctamente.");
        }

        [HttpPost("googlesignin")]
        public async Task<IActionResult> GoogleSignInAsync(string idToken)
        {
            try
            {
                var signInResult = await _firebaseAuth.VerifyIdTokenAsync(idToken);
                return Ok(signInResult);
            }
            catch (FirebaseAuthException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("githubsignin")]
        public async Task<IActionResult> GitHubSignInAsync(string idToken)
        {
            try
            {
                var token = await _firebaseAuth.VerifyIdTokenAsync(idToken);
                return Ok(token);
            }
            catch (FirebaseAuthException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
