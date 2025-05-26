using Domain.Services;
using Shared.Models.Products;

namespace Domain.Modules.Product;

public interface IProductService : ICrudWrapper<ProductResponseModel,CreateProductRequestModel, UpdateProductRequestModel>
{
    
}