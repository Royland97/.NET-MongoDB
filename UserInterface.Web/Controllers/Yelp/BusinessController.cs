using AutoMapper;
using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserInterface.Web.ViewModels.Yelp;
using X.PagedList.Extensions;

namespace UserInterface.Web.Controllers.Yelp
{
    /// <summary>
    /// Business Yelp Controller
    /// </summary>
    [ApiController]
    [Route("yelp/business")]
    public class BusinessController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IBusinessRepository _businessRepository;
        private readonly IReviewRepository _reviewRepository;

        public BusinessController(
            IMapper mapper,
            IBusinessRepository businessRepository,
            IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _businessRepository = businessRepository;
            _reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Gets all Businesss
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(
            int? page)
        {
            var result = _businessRepository.GetBetterBusiness();
            var businesss = _mapper.Map<ICollection<BusinessModel>>(result);

            int pageSize = 10;
            int pageNumber = page ?? 1;

            return View(businesss.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Gets an Business by Id for Details View
        /// </summary>
        /// 
        /// <param name="id">Business Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("/details/{id}")]
        public async Task<IActionResult> Details(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var business = await _businessRepository.GetBusinessByBusinessId(id);

            if (business == null)
                return NotFound(id);

            var businessModel = _mapper.Map<BusinessModelDetails>(business);

            return View(businessModel);
        }

        /// <summary>
        /// Gets an Business by Id for Details View
        /// </summary>
        /// 
        /// <param name="id">Business Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("/reviews/{id}")]
        public async Task<IActionResult> Reviews(
            [FromRoute] string id,
            int? page,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var reviews = _reviewRepository.GetReviewByBusinessId(id);
            //var reviewsModel = _mapper.Map<ICollection<ReviewModel>>(reviews);

            int pageSize = 10;
            int pageNumber = page ?? 1;

            return View(reviews.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Saves a Business
        /// </summary>
        /// <param name="businessModel">Business data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] BusinessModel businessModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var business = _mapper.Map<Business>(businessModel);

            await _businessRepository.SaveAsync(business, cancellationToken);

            return Ok(businessModel);
        }

        /// <summary>
        /// Updates a Business
        /// </summary>
        /// <param name="businessModel">Business data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody]BusinessModel businessModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return View(businessModel);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var business = await _businessRepository.GetByKeyValueAsync(businessModel.Id, cancellationToken);

                if (business == null)
                    return NotFound(businessModel.Id);

                business = _mapper.Map(businessModel, business);
                await _businessRepository.UpdateAsync(business, cancellationToken);

                return Ok(businessModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a business.
        /// </summary>
        /// 
        /// <param name="id">Business Id to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var business = await _businessRepository.GetByIdAsync(id, cancellationToken);

            if (business == null)
                return NotFound(id);

            try
            {
                await _businessRepository.DeleteAsync(business, cancellationToken);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets an Business by Id for Edit View
        /// </summary>
        /// 
        /// <param name="id">Business Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var business = await _businessRepository.GetByKeyValueAsync(id, cancellationToken);

            if (business == null)
                return NotFound(id);

            var businessModel = _mapper.Map<BusinessModel>(business);

            return View(businessModel);
        }

    }
}
