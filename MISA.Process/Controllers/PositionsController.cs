using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interface.Repository;
using MISA.Core.Models;

namespace MISA.Process.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : BasesController<Positions>
    {
        IPositionRepository _repository;
        public PositionsController(IPositionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
