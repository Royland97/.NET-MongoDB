using AutoMapper;
using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Yelp;

namespace UserInterface.Web.Controllers.Yelp
{
    /// <summary>
    /// Chekin Api Controller
    /// </summary>
    [ApiController]
    [Route("api/yelp/chekin")]
    public class ChekinController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IChekinRepository _chekinRepository;

        public ChekinController(
            IMapper mapper,
            IChekinRepository chekinRepository)
        {
            _mapper = mapper;
            _chekinRepository = chekinRepository;
        }

        /// <summary>
        /// Saves a Chekin
        /// </summary>
        /// <param name="chekinModel">Chekin data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ChekinModel chekinModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var chekin = _mapper.Map<Chekin>(chekinModel);

            await _chekinRepository.SaveAsync(chekin, cancellationToken);

            return Ok(chekinModel);
        }

        /// <summary>
        /// Updates a Chekin
        /// </summary>
        /// <param name="chekinModel">Chekin data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] ChekinModel chekinModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var chekin = await _chekinRepository.GetByKeyValueAsync(chekinModel.Id, cancellationToken);

                if (chekin == null)
                    return NotFound(chekinModel.Id);

                chekin = _mapper.Map(chekinModel, chekin);
                await _chekinRepository.UpdateAsync(chekin, cancellationToken);

                return Ok(chekin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a chekin.
        /// </summary>
        /// 
        /// <param name="id">Chekin Id to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var chekin = await _chekinRepository.GetByIdAsync(id, cancellationToken);

            if (chekin == null)
                return NotFound(id);

            try
            {
                await _chekinRepository.DeleteAsync(chekin, cancellationToken);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all Chekins
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _chekinRepository.GetAllAsync(cancellationToken);
            var chekins = _mapper.Map<ICollection<ChekinModel>>(result);

            return Ok(result);
        }

        /// <summary>
        /// Gets an Chekin by Id.
        /// </summary>
        /// 
        /// <param name="id">Chekin Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var chekin = await _chekinRepository.GetByIdAsync(id, cancellationToken);

            if (chekin == null)
                return NotFound(id);

            var chekinModel = _mapper.Map<ChekinModel>(chekin);

            return Ok(chekinModel);
        }
    }
}
