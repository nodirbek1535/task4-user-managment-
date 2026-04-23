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

        [HttpGet]
        public ActionResult<IQueryable<User>> GetAllUsers()
        {
            try
            {
                IQueryable<User> allUsers =
                    this.userService.GetAllUsers();

                return Ok(allUsers);
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

        [HttpPut]
        public async ValueTask<ActionResult<User>> PutUserAsync(User user)
        {
            try
            {
                User modifiedUser = await this.userService.ModifyUserAsync(user);

                return Ok(modifiedUser);
            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
                when (userDependencyValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException.InnerException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException.InnerException);
            }
        }

        [HttpDelete("{userId:guid}")]
        public async ValueTask<ActionResult<User>> DeleteUserByIdAsync(Guid userId)
        {
            try
            {
                User deletedUser =
                    await this.userService.RemoveUserByIdAsync(userId);

                return Ok(deletedUser);

            }
            catch (UserValidationException userValidationException)
            {
                return BadRequest(userValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
                when (userDependencyValidationException.InnerException is NotFoundUserException)
            {
                return Locked(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                return BadRequest(userDependencyValidationException.InnerException);
            }
            catch (UserDependencyException userDependencyException)
            {
                return InternalServerError(userDependencyException.InnerException);
            }
            catch (UserServiceException userServiceException)
            {
                return InternalServerError(userServiceException.InnerException);
            }
        }
    }
}
