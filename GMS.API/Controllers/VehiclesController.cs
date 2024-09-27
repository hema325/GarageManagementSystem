using GMS.Shared.Dtos.Requests.Vehicles;
using GMS.Shared.Dtos.Responses.Vehicles;

namespace GMS.API.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;

        public VehiclesController(IUnitOfWork ufw, IMapper mapper)
        {
            _ufw = ufw;
            _mapper = mapper;
        }


        [HttpPost(Router.Vehicles.Create)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> CreateAsync(CreateOrUpdateVehicleDto dto)
        {
            var vehicle = _mapper.Map<Vehicle>(dto);

            _ufw.Vehicles.Create(vehicle);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok(vehicle.Id));
        }

        [HttpPut(Router.Vehicles.Update)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> UpdateAsync(int id, CreateOrUpdateVehicleDto dto)
        {
            var vehicle = await _ufw.Vehicles.GetByIdAsync(id);

            if (vehicle == null)
                return NotFound(ResponseFactory.NotFound());

            _mapper.Map(dto, vehicle);

            _ufw.Vehicles.Update(vehicle);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }
        
        [HttpPut(Router.Vehicles.UpdateStatus)]
        [HaveRoles(Roles.Admin, Roles.Employee)]
        public async Task<IActionResult> UpdateStatusAsync(int id, VehicleStatus status)
        {
            var vehicle = await _ufw.Vehicles.GetByIdAsync(id);

            if (vehicle == null)
                return NotFound(ResponseFactory.NotFound());

            vehicle.Status = status;

            _ufw.Vehicles.Update(vehicle);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpDelete(Router.Vehicles.Delete)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var vehicle = await _ufw.Vehicles.GetByIdAsync(id);

            if (vehicle == null)
                return NotFound(ResponseFactory.NotFound());

            _ufw.Vehicles.Delete(vehicle);
            await _ufw.SaveChangesAsync();

            return Ok(ResponseFactory.Ok());
        }

        [HttpGet(Router.Vehicles.GetById)]
        [HaveRoles(Roles.Admin, Roles.Employee)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var vehicle = await _ufw.Vehicles.GetByIdIncludeRelatedAsync(id);

            if (vehicle == null)
                return NotFound(ResponseFactory.NotFound());

            return Ok(ResponseFactory.Ok(_mapper.Map<VehicleDto>(vehicle)));
        }
        
        [HttpGet(Router.Vehicles.GetAll)]
        [HaveRoles(Roles.Admin, Roles.Employee)]
        public async Task<IActionResult> GetAllAsync([FromQuery] FilterVehicleDto filter)
        {
            var vehicle = await _ufw.Vehicles.FilterIncludeRelatedAsync(filter);

            return Ok(ResponseFactory.Ok(_mapper.Map<List<VehicleDto>>(vehicle)));
        }
    }
}
