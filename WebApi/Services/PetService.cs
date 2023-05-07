namespace WebApi.Services
{
    public class PetService : IPetService
    {
        private readonly PetstoreContext _context;
        private readonly IMapper _mapper;

        public PetService(PetstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<Pet>>> GetPets()
        {
            var response = new ServiceResponse<List<Pet>>();
            var pets = await _context.Pets.Include(p => p.Category).ToListAsync();
            if (pets is null || !pets.Any())
            {
                response.Success = false;
                response.Message = "Petstore empty";
            }
            else
                response.Data = pets;
            return response;
        }

        public async Task<ServiceResponse<Pet>> GetPet(int id)
        {
            var response = new ServiceResponse<Pet>();
            var pet = await _context.Pets.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (pet is null)
            {
                response.Success = false;
                response.Message = "Pet not found";
            }
            else
                response.Data = pet;
            return response;
        }

        public async Task<ServiceResponse<bool>> AddPet(AddPetDto request)
        {
            var response = new ServiceResponse<bool>();
            await _context.Pets.AddAsync(_mapper.Map<Pet>(request));

            response.Data = await _context.SaveChangesAsync() > 0;
            if (response.Data is false)
            {
                response.Success = false;
                response.Message = "No pet added";
            }
            else
                response.Message = "Add pet success";
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdatePet(UpdatePetDto request)
        {
            var response = new ServiceResponse<bool>();
            var pet = await _context.Pets.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == request.Id);
            if (pet is null)
            {
                response.Success = false;
                response.Message = "Pet not found";
            }
            else
            {
                if (request.Name is not null)
                    pet.Name = request.Name;
                if (request.Status is not null)
                    pet.Status = request.Status;
                if (request.CategoryId is not null)
                    pet.CategoryId = request.CategoryId;

                response.Data = await _context.SaveChangesAsync() > 0;
                if (response.Data is false)
                {
                    response.Success = false;
                    response.Message = "No pet updated";
                }
                else
                    response.Message = "Update pet success";
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeletePet(int id)
        {
            var response = new ServiceResponse<bool>();
            var pet = await _context.Pets.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (pet is null)
            {
                response.Success = false;
                response.Message = "Pet not found";
            }
            else
            {
                _context.Remove(pet);

                response.Data = await _context.SaveChangesAsync() > 0;
                if (response.Data is false)
                {
                    response.Success = false;
                    response.Message = "No pet deleted";
                }
                else
                    response.Message = "Delete pet success";
            }
            return response;
        }
    }
}
