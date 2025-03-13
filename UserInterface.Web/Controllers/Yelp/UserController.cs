using System.Drawing.Printing;
using AutoMapper;
using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Yelp;
using X.PagedList.Extensions;

namespace UserInterface.Web.Controllers.Yelp
{
    /// <summary>
    /// User Yelp Controller
    /// </summary>
    [ApiController]
    [Route("yelp/user")]
    public class UserController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ITipRepository _tipRepository;

        public UserController(
            IMapper mapper,
            IUserRepository userRepository,
            IReviewRepository reviewRepository,
            ITipRepository tipRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _tipRepository = tipRepository;
        }

        /// <summary>
        /// Gets all Users
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(
            int? page,
            string? search)
        {
            var result = _userRepository.GetAllUsers(search);
            var users = _mapper.Map<ICollection<UserModel>>(result);

            int pageSize = 10;
            int pageNumber = page ?? 1;

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Gets an User by Id.
        /// </summary>
        /// 
        /// <param name="id">User Id</param>
        [HttpGet("/userDetails/{id}")]
        public async Task<IActionResult> Details(
            [FromRoute] string id)
        {
            var user = _userRepository.GetUserByUserId(id);

            if (user == null)
                return NotFound(id);

            var userModel = _mapper.Map<UserModel>(user);

            return View(userModel);
        }

        /// <summary>
        /// Gets all Reviews and Business related from an User Id.
        /// </summary>
        /// 
        /// <param name="id">User Id</param>
        [HttpGet("/reviewsBusiness/{id}")]
        public async Task<IActionResult> ReviewBusiness(
            int? page,
            [FromRoute] string id)
        {
            var reviewBusinesses = _reviewRepository.GetReviewBusinessByUserId(id);

            if (reviewBusinesses == null)
                return NotFound(id);

            int pageSize = 10;
            int pageNumber = page ?? 1;

            return View(reviewBusinesses.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Gets all Tips and Business related from an User Id
        /// </summary>
        /// 
        /// <param name="id">User Id</param>
        [HttpGet("/tipsBusiness/{id}")]
        public async Task<IActionResult> TipsBusiness(
            int? page,
            [FromRoute] string id)
        {
            var tipsBusiness = _tipRepository.GetTipsBusinessByUserId(id);

            if (tipsBusiness == null)
                return NotFound(id);

            int pageSize = 10;
            int pageNumber = page ?? 1;

            return View(tipsBusiness.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Saves a User
        /// </summary>
        /// <param name="userModel">User data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] UserModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var user = _mapper.Map<User>(userModel);

            await _userRepository.SaveAsync(user, cancellationToken);

            return Ok(userModel);
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="userModel">User data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UserModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var user = await _userRepository.GetByKeyValueAsync(userModel.Id, cancellationToken);

                if (user == null)
                    return NotFound(userModel.Id);

                user = _mapper.Map(userModel, user);
                await _userRepository.UpdateAsync(user, cancellationToken);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// 
        /// <param name="id">User Id to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return NotFound(id);

            try
            {
                await _userRepository.DeleteAsync(user, cancellationToken);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets an User by Id.
        /// </summary>
        /// 
        /// <param name="id">User Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return NotFound(id);

            var userModel = _mapper.Map<UserModel>(user);

            return Ok(userModel);
        }
    }
}
