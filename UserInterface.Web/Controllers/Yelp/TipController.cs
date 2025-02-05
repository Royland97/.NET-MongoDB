using AutoMapper;
using Core.DataAccess.IRepository.Yelp;
using Core.Domain.Yelp;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Yelp;

namespace UserInterface.Web.Controllers.Yelp
{
    /// <summary>
    /// Tip Api Controller
    /// </summary>
    [ApiController]
    [Route("api/yelp/tip")]
    public class TipController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ITipRepository _tipRepository;

        public TipController(
            IMapper mapper,
            ITipRepository tipRepository)
        {
            _mapper = mapper;
            _tipRepository = tipRepository;
        }

        /// <summary>
        /// Saves a Tip
        /// </summary>
        /// <param name="tipModel">Tip data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] TipModel tipModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var tip = _mapper.Map<Tip>(tipModel);

            await _tipRepository.SaveAsync(tip, cancellationToken);

            return Ok(tipModel);
        }

        /// <summary>
        /// Updates a Tip
        /// </summary>
        /// <param name="tipModel">Tip data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] TipModel tipModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var tip = await _tipRepository.GetByKeyValueAsync(tipModel.Id, cancellationToken);

                if (tip == null)
                    return NotFound(tipModel.Id);

                tip = _mapper.Map(tipModel, tip);
                await _tipRepository.UpdateAsync(tip, cancellationToken);

                return Ok(tip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a tip.
        /// </summary>
        /// 
        /// <param name="id">Tip Id to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var tip = await _tipRepository.GetByIdAsync(id, cancellationToken);

            if (tip == null)
                return NotFound(id);

            try
            {
                await _tipRepository.DeleteAsync(tip, cancellationToken);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all Tips
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _tipRepository.GetAllAsync(cancellationToken);
            var tips = _mapper.Map<ICollection<TipModel>>(result);

            return Ok(result);
        }

        /// <summary>
        /// Gets an Tip by Id.
        /// </summary>
        /// 
        /// <param name="id">Tip Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var tip = await _tipRepository.GetByIdAsync(id, cancellationToken);

            if (tip == null)
                return NotFound(id);

            var tipModel = _mapper.Map<TipModel>(tip);

            return Ok(tipModel);
        }
    }
}
