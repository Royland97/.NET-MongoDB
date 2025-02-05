using AutoMapper;
using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Yelp;

namespace UserInterface.Web.Controllers.Yelp
{
    /// <summary>
    /// Review Api Controller
    /// </summary>
    [ApiController]
    [Route("api/yelp/review")]
    public class ReviewController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(
            IMapper mapper,
            IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Saves a Review
        /// </summary>
        /// <param name="reviewModel">Review data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ReviewModel reviewModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var review = _mapper.Map<Review>(reviewModel);

            await _reviewRepository.SaveAsync(review, cancellationToken);

            return Ok(reviewModel);
        }

        /// <summary>
        /// Updates a Review
        /// </summary>
        /// <param name="reviewModel">Review data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] ReviewModel reviewModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var review = await _reviewRepository.GetByKeyValueAsync(reviewModel.Id, cancellationToken);

                if (review == null)
                    return NotFound(reviewModel.Id);

                review = _mapper.Map(reviewModel, review);
                await _reviewRepository.UpdateAsync(review, cancellationToken);

                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a review.
        /// </summary>
        /// 
        /// <param name="id">Review Id to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);

            if (review == null)
                return NotFound(id);

            try
            {
                await _reviewRepository.DeleteAsync(review, cancellationToken);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all Reviews
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _reviewRepository.GetAllAsync(cancellationToken);
            var reviews = _mapper.Map<ICollection<ReviewModel>>(result);

            return Ok(result);
        }

        /// <summary>
        /// Gets an Review by Id.
        /// </summary>
        /// 
        /// <param name="id">Review Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);

            if (review == null)
                return NotFound(id);

            var reviewModel = _mapper.Map<ReviewModel>(review);

            return Ok(reviewModel);
        }
    }
}
