using Casgem_MicroServer.Shared.DTOS;
using CasgemMicroService.Catalog.DTOS.CategoryDtos;

namespace CasgemMicroService.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Response<List<ResultCategoryDto>>> GetCategoryListAsync();
        Task<Response<ResultCategoryDto>> GetCategoryByIdAsync(string id);
        Task<Response<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<Response<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<Response<NoContent>> DeleteCategoryAsync(string id);
    }
}
