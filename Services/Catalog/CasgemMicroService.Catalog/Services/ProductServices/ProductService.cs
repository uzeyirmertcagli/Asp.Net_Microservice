using AutoMapper;
using Casgem_MicroServer.Shared.DTOS;
using CasgemMicroService.Catalog.DTOS.CategoryDtos;
using CasgemMicroService.Catalog.DTOS.ProductDtos;
using CasgemMicroService.Catalog.Models;
using CasgemMicroService.Catalog.Settings.Abstract;
using MongoDB.Driver;

namespace CasgemMicroService.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public ProductService(IMapper mapper , IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var values = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(values);
            return Response<CreateProductDto>.Success(_mapper.Map<CreateProductDto>(values),200);
        }

        public async Task<Response<NoContent>> DeleteProductAsync(string id)
        {
          var value = await _productCollection.DeleteOneAsync(id);

            if(value.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Silinecek Ürün Bulunamadı!" , 404);
            }
        }

        public async Task<Response<ResultProductDto>> GetProductByIdAsync(string id)
        {
            var value = await _productCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync();
            if (value == null)
            {
                return Response<ResultProductDto>.Fail("Böyle Bir ID Bulunamadı !", 404);
            }
            else
            {
                return Response<ResultProductDto>.Success(_mapper.Map<ResultProductDto>(value), 200);
            }
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync()
        {
            var values =await _productCollection.Find(x => true).ToListAsync();
            if(values.Any())
            {
                foreach(var item in values)
                {
                    item.Category = await _categoryCollection.Find<Category>(x =>x.CategoryID == item.CategoryID).FirstOrDefaultAsync();
                }

            }
            else
            {
                values = new List<Product>();
            }
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateProductDto.ProductID, value);
            if (result == null)
            {
                return Response<UpdateProductDto>.Fail("Güncellenecek Ürün Bulunamadı!", 404);
            }
            else
            {
                return Response<UpdateProductDto>.Success(204);
            }
        }
        
    }
}
