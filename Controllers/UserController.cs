//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using task4_user_managment_.Models.Exceptions;
using task4_user_managment_.Services.Foundations.Users;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : RESTFulController
    {
        private readonly IUserService userService;
        public UserController(IUserService userService) =>
            this.userService = userService;

        [HttpPost]
        public async ValueTask<ActionResult> PostUserAsync(User user)
        {
            try
            {
                User addedUser =
                    await this.userService.AddUserAsync(user);

                return Created(addedUser);
            }
            catch (UserValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (UserDependencyValidationException dependencyValidationException)
                when (dependencyValidationException.InnerException is AlreadyExistsUserException)
            {
                return Conflict(dependencyValidationException.InnerException);
            }
            catch (UserDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (UserServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }

        [HttpGet("{userId:guid}")]
        public async ValueTask<ActionResult> GetUserByIdAsync(Guid userId)
        {
            try
            {
                User retrievedUser =
                    await this.userService.RetrieveUserByAsync(userId);

                return Ok(retrievedUser);
            }
            catch (UserValidationException validationException)
            {
                return BadRequest(validationException.InnerException);
            }
            catch (UserDependencyValidationException dependencyValidationException)
                when (dependencyValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(dependencyValidationException.InnerException);
            }
            catch (UserDependencyException dependencyException)
            {
                return InternalServerError(dependencyException.InnerException);
            }
            catch (UserServiceException serviceException)
            {
                return InternalServerError(serviceException.InnerException);
            }
        }
    }
}
