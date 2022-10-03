using APICoreTemplate.Core.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using APICoreTemplate.Core.Data.Models;
using APICoreTemplate.Mapper;
using APICoreTemplate.Shared.Models.UserModel;
using Swashbuckle.AspNetCore.Annotations;

namespace APICoreTemplate.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServerDataFactory _data;
        public UserController(IServerDataFactory data)
        {
            _data = data;
        }

        [HttpGet, Route("GetAllAsync", Order = 1)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(IEnumerable<UsersDetailsModel>))]
        public async Task<IActionResult> GetAllAsync(int? pageNumber = null, int? pageSize = null, CancellationToken cancel = default)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    IEnumerable<User> users;

                    if (pageNumber.HasValue && pageSize.HasValue)
                    {
                        users = await data.Users.GetAllByOffsetAsync(pageNumber.Value, pageSize.Value, cancel);
                    }
                    else
                    {
                        users = await data.Users.GetAllAsync(cancel);
                    }

                    var models = users.Select(UserMapper.MapDetailsModel);
                    return Ok(models);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("GetAllBySearchAsync", Order = 1)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(IEnumerable<UsersDetailsModel>))]
        public async Task<IActionResult> GetAllBySearchAsync(string columnName, string searchValue, int? pageNumber = null, int? pageSize = null, CancellationToken cancel = default)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    IEnumerable<User> users;

                    if (pageNumber.HasValue && pageSize.HasValue)
                    {
                        users = await data.Users.GetAllByOffsetAsync(columnName, searchValue, pageNumber.Value, pageSize.Value, cancel);
                    }
                    else
                    {
                        users = await data.Users.SearchAsync(columnName, searchValue, cancel);
                    }

                    var models = users.Select(UserMapper.MapDetailsModel);

                    return Ok(models);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("GetTotalCountAsync", Order = 1)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(int))]
        public async Task<IActionResult> GetTotalCountAsync(CancellationToken cancel)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    var counts = await data.Users.GetTotalCountAsync(cancel);
                    return Ok(counts);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("GetTotalCountBySearchAsync", Order = 1)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(int))]
        public async Task<IActionResult> GetTotalCountBySearchAsync(string columnName, string searchValue, CancellationToken cancel)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    var counts = await data.Users.GetTotalCountAsync(columnName, searchValue, cancel);
                    return Ok(counts);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("GetAsync", Order = 1)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(UsersDetailsModel))]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancel)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    var user = await data.Users.GetAsync(id, cancel);
                    var models = UserMapper.MapDetailsModel(user);
                    return Ok(models);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("GetUserByEmail", Order = 1)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(UsersDetailsModel))]
        public async Task<IActionResult> GetUserByEmail(string userEmail, CancellationToken cancel)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    var user = await data.Users.GetAsync(m => m.Email, userEmail, cancel);
                    if (user != null)
                    {
                        var models = UserMapper.MapDetailsModel(user);
                        return Ok(models);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost, Route("RegisterUserAsync", Order = 2)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(int))]
        public async Task<IActionResult> RegisterUserAsync(UserCreateModel user, CancellationToken cancel)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User Details Cannot be Null.");
                }

                using (var data = await _data.Create(cancel))
                {
                    var userdetails = await data.Users.GetAsync(m => m.Email, user.Email, cancel);

                    if (userdetails == null)
                    {
                        var userId = await data.Users.CreateUserAsync(user.UserName, user.Email, user.FirstName, user.LastName, user.Roles, default);
                        data.Commit();
                        return Ok(userId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut, Route("UpdateAsync", Order = 3)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(int))]
        public async Task<IActionResult> UpdateAsync(int id, string roles, CancellationToken cancel)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    await data.Users.UpdateUserAsync(id, roles, cancel);
                    data.Commit();

                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("GetHistoryTotalCountAsync", Order = 4)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(int))]
        public async Task<IActionResult> GetHistoryTotalCountAsync(int id, CancellationToken cancel)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    var counts = await data.Users.History.GetTotalCountAsync(id, cancel);
                    return Ok(counts);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet, Route("GetHistoryAsync", Order = 4)]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(IEnumerable<UserHistoryModel>))]
        public async Task<IActionResult> GetHistoryAsync(int id, int pageNumber, int pageSize, CancellationToken cancel = default)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    var users = await data.Users.History.GetAsync(id, pageNumber, pageSize, cancel);
                    var models = users.Select(UserMapper.MapHistoryModel);
                    return Ok(models);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
