using Casgem_MicroServer.Shared.DTOS;
using CasgemMicroService.Catalog.DTOS.CategoryDtos;
using CasgemMicroService.Catalog.DTOS.ProductDtos;

namespace CasgemMicroService.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<Response<List<ResultProductDto>>> GetProductListAsync();
        Task<Response<ResultProductDto>> GetProductByIdAsync(string id);
        Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto);
        Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<Response<NoContent>> DeleteProductAsync(string id);
        Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync();
    }
}
