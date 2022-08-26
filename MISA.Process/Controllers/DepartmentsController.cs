using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interface.Repository;
using MISA.Core.Models;

namespace MISA.Process.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        IDepartmentRepository _repository;
        public DepartmentsController(IDepartmentRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
