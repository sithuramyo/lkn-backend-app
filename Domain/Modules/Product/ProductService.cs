using Persistence.DataModels.Setup;
using Shared.Constants;
using Shared.Models.Products;

namespace Domain.Modules.Product;

public class ProductService(AppDbContext context) : IProductService
{
    public async Task<ResponseModel<PaginationResponse<ProductResponseModel>>> ListAsync(PaginationRequest request)
    {
        var baseQuery = context.Products
            .AsNoTracking()
            .Where(m => !m.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            baseQuery = baseQuery.Where(m =>
                EF.Functions.ILike(m.Name, $"%{request.Search}%") ||
                EF.Functions.ILike(m.ProductCode, $"%{request.Search}%"));
        }

        baseQuery = baseQuery.ApplySorting(request.SortBy, request.IsAscending);

        var projectedQuery = from p in baseQuery
                             join size in context.MasterCodeItems on p.SizeId equals size.Id into sizeGroup
                             from size in sizeGroup.DefaultIfEmpty()
                             join pkg in context.MasterCodeItems on p.PackagingId equals pkg.Id into pkgGroup
                             from pkg in pkgGroup.DefaultIfEmpty()
                             join madeIn in context.MasterCodeItems on p.MadeInId equals madeIn.Id into madeInGroup
                             from madeIn in madeInGroup.DefaultIfEmpty()
                             join pc in context.ProductColors on p.Id equals pc.ProductId into pcGroup
                             from pc in pcGroup.DefaultIfEmpty()
                             join color in context.MasterCodeItems on pc.ColorId equals color.Id into colorGroup
                             from color in colorGroup.DefaultIfEmpty()
                             select new
                             {
                                 p.Id,
                                 p.ProductCode,
                                 p.Name,
                                 p.Price,
                                 Size = size.Value,
                                 PackageType = pkg.Value,
                                 MadeIn = madeIn.Value,
                                 Color = color.Value
                             };

        var grouped = projectedQuery
            .GroupBy(p => new
            {
                p.Id,
                p.ProductCode,
                p.Name,
                p.Price,
                p.Size,
                p.PackageType,
                p.MadeIn
            })
            .Select(g => new ProductResponseModel
            {
                Id = g.Key.Id,
                ProductCode = g.Key.ProductCode,
                Name = g.Key.Name,
                Price = g.Key.Price,
                Size = g.Key.Size,
                Packaging = g.Key.PackageType,
                MadeIn = g.Key.MadeIn,
                Color = g.Where(x => !string.IsNullOrEmpty(x.Color)).Select(x => x.Color).Distinct().ToArray()
            });

        var paginated = await grouped.ToPagedListAsync(request);
        return ResponseModel<PaginationResponse<ProductResponseModel>>.Success(paginated);
    }

    public async Task<ResponseModel<NoResponseModel>> CreateAsync(CreateProductRequestModel request)
    {
        NoResponseModel response = new();

        var isExist = await context.Products.AnyAsync(x =>
            x.Name == request.Name && x.Price == request.Price && !x.IsDeleted);

        if (isExist)
        {
            return ResponseModel<NoResponseModel>.BadRequest("Name already exist");
        }

        var count = await context.Products.CountAsync();
        Persistence.DataModels.Setup.Product data = new()
        {
            Id = Guid.NewGuid().ToString(),
            ProductCode = CodeConstants.PRODUCT_CODE.GetCode(count, CodeConstants.OTHER_CODE_DIGIT),
            Name = request.Name,
            Price = request.Price,
            SizeId = request.SizeId,
            PackagingId = request.PackagingId,
            MadeInId = request.MadeInId,
        };
        await context.Products.AddAsync(data);

        if (request.ColorIds is { Length: > 0 })
        {
            var productColors = request.ColorIds
                .Select(colorId => new ProductColor
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = data.Id,
                    ColorId = colorId
                }).ToList();

            await context.ProductColors.AddRangeAsync(productColors);
        }

        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<ProductResponseModel>> GetByIdAsync(string id)
    {
        ProductResponseModel response = new();
        var data = await context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<ProductResponseModel>.NotFound("Not found");
        }

        response.ProductCode = data.ProductCode;
        response.Name = data.Name;
        response.Price = data.Price;
        response.SizeId = data.SizeId;
        var colorIds = await context.ProductColors
            .Where(x => x.ProductId == data.Id)
            .Select(x => x.ColorId)
            .ToArrayAsync();
        response.ColorId = colorIds;
        response.PackagingId = data.PackagingId;
        response.MadeInId = data.MadeInId;
        return ResponseModel<ProductResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> UpdateAsync(string id, UpdateProductRequestModel request)
    {
        NoResponseModel response = new();
        var data = await context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }

        var isExist = await context.Products.AnyAsync(x =>
            x.Name == request.Name && x.Price == request.Price && x.Id != id && !x.IsDeleted);

        if (isExist)
        {
            return ResponseModel<NoResponseModel>.BadRequest("Name already exist");
        }

        data.Name = request.Name;
        data.Price = request.Price;
        data.SizeId = request.SizeId;
        data.PackagingId = request.PackagingId;
        data.MadeInId = request.MadeInId;
        context.Products.Update(data);

        var existingProductColors = await context.ProductColors.Where(x => x.ProductId == id).ToListAsync();
        if (existingProductColors.Count > 0)
        {
            context.ProductColors.RemoveRange(existingProductColors);
        }

        if (request.ColorIds is { Length: > 0 })
        {
            var newProductColors = request.ColorIds.Select(colorId => new ProductColor
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = data.Id,
                ColorId = colorId
            }).ToList();

            await context.ProductColors.AddRangeAsync(newProductColors);
        }

        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> DeleteAsync(string id)
    {
        NoResponseModel response = new();
        var data = await context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (data is null)
        {
            return ResponseModel<NoResponseModel>.NotFound("Not found");
        }

        data.IsDeleted = true;
        context.Products.Update(data);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }
}