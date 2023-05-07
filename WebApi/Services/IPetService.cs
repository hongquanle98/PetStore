namespace WebApi.Services
{
    public interface IPetService
    {
        Task<ServiceResponse<List<Pet>>> GetPets();
        Task<ServiceResponse<Pet>> GetPet(int id);
        Task<ServiceResponse<bool>> AddPet(AddPetDto request);
        Task<ServiceResponse<bool>> UpdatePet(UpdatePetDto request);
        Task<ServiceResponse<bool>> DeletePet(int id);
    }
}
