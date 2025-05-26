using Persistence.DataModels.Voucher;
using Shared.Constants;
using Shared.Enums;
using Shared.Models.WholeSale;

namespace Domain.Modules.WholeSale;

public class WholeSaleService(AppDbContext context) : IWholeSaleService
{
    public async Task<ResponseModel<PaginationResponse<WholeSaleResponseModel>>> GetWholeSaleVouchersAsync(
        PaginationRequest request)
    {
        var query = context.MerchantVouchers
            .AsNoTracking()
            .Where(m => !m.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(m =>
                EF.Functions.ILike(m.VoucherCode, $"%{request.Search}%") ||
                EF.Functions.ILike(m.MerchantCode, $"%{request.Search}%"));
        }

        query = query.ApplySorting(request.SortBy, request.IsAscending);

        var projectedQuery = from mv in query
                             join m in context.Merchants.AsNoTracking() on mv.MerchantCode equals m.MerchantCode
                             select new WholeSaleResponseModel
                             {
                                 VoucherCode = mv.VoucherCode,
                                 MerchantName = m.Name,
                                 VoucherOpenedDate = mv.VoucherOpenedDate,
                                 TotalAmount = mv.TotalPrice,
                                 LeftAmount = mv.LeftPrice,
                                 DepositAmount = mv.TotalPrice - mv.LeftPrice,
                                 PaidStatus = mv.PaidStatus
                             };
        var paginated = await projectedQuery.ToPagedListAsync(request);
        return ResponseModel<PaginationResponse<WholeSaleResponseModel>>.Success(paginated);
    }

    public async Task<ResponseModel<PaginationResponse<WholeSaleStatusResponseModel>>> GetWholeSaleVouchersStatusAsync(
        PaginationRequest request, PaidStage status)
    {
        var baseQuery = context.MerchantVouchers
            .AsNoTracking()
            .Where(mv => mv.PaidStatus == status.ToString());

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            baseQuery = baseQuery.Where(m =>
                EF.Functions.ILike(m.VoucherCode, $"%{request.Search}%") ||
                EF.Functions.ILike(m.MerchantCode, $"%{request.Search}%"));
        }

        baseQuery = baseQuery.ApplySorting(request.SortBy, request.IsAscending);

        var joinedQuery = from mv in baseQuery
                          join ph in context.MerchantVoucherPaidHistories.AsNoTracking() on mv.VoucherCode equals ph.VoucherCode
                          join pm in context.MasterCodeItems.AsNoTracking() on ph.PaymentMethodId equals pm.Id into pmGroup
                          from pm in pmGroup.DefaultIfEmpty()
                          where pm == null || !pm.IsDeleted
                          select new WholeSaleStatusResponseModel
                          {
                              VoucherCode = mv.VoucherCode,
                              PaymentMethod = pm != null ? pm.Value : null,
                              Amount = ph.PaidAmount,
                              IssueDate = ph.PaidDate
                          };

        var paginated = await joinedQuery.ToPagedListAsync(request);
        return ResponseModel<PaginationResponse<WholeSaleStatusResponseModel>>.Success(paginated);
    }

    public async Task<ResponseModel<NoResponseModel>> DeleteWholeSaleVoucherAsync(string voucherCode)
    {
        NoResponseModel response = new();
        var merchantVoucher =
            await context.MerchantVouchers.FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && !x.IsDeleted);
        if (merchantVoucher is null)
        {
            return ResponseModel<NoResponseModel>.NotFound($"{voucherCode} is not found");
        }

        merchantVoucher.IsDeleted = true;
        context.MerchantVouchers.Update(merchantVoucher);
        await context.SaveChangesAsync();
        return ResponseModel<NoResponseModel>.Success(response);
    }

    public async Task<ResponseModel<PayableWholeSaleVoucherResponseModel>> PayableWholeSaleVoucherAsync(
        PayableWholeSaleVoucherRequestModel request)
    {
        PayableWholeSaleVoucherResponseModel response = new();
        var isExist = await
            context.MerchantVouchers.AnyAsync(x => x.MerchantCode == request.MerchantCode && !x.IsDeleted);

        if (!isExist)
        {
            return ResponseModel<PayableWholeSaleVoucherResponseModel>.NotFound("Not found");
        }

        var payableDataList = context.MerchantVouchers
            .Where(x => x.MerchantCode == request.MerchantCode && x.PaidStatus != PaidStage.PAID.ToString() &&
                        !x.IsDeleted)
            .Select(x => new PayableWholeSaleVouchers
            {
                VoucherCode = x.VoucherCode,
                TotalAmount = x.TotalPrice,
                LeftAmount = x.LeftPrice
            })
            .OrderBy(x => x.LeftAmount)
            .ToList();

        var payableList = payableDataList.GetNearestVoucher(request.PayableAmount);
        response.PayableWholeSaleVouchers = payableList
            .Select(x => new PayableWholeSaleVouchers
            {
                VoucherCode = x.VoucherCode,
                TotalAmount = x.TotalAmount,
                LeftAmount = x.LeftAmount
            }).ToList();
        response.PayableAmount = request.PayableAmount;
        return ResponseModel<PayableWholeSaleVoucherResponseModel>.Success(response);
    }

    public async Task<ResponseModel<PaginationResponse<WholeSaleDetailResponseModel>>> GetWholeSaleVoucherDetailsAsync(
        PaginationRequest request, string voucherCode)
    {
        var isExist = await context.MerchantVouchers.AnyAsync(x => x.VoucherCode == voucherCode && !x.IsDeleted);

        if (!isExist)
        {
            return ResponseModel<PaginationResponse<WholeSaleDetailResponseModel>>.NotFound("Not found");
        }

        var baseQuery = context.MerchantVoucherProducts
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            baseQuery = baseQuery.Where(m =>
                EF.Functions.ILike(m.VoucherCode, $"%{request.Search}%") ||
                EF.Functions.ILike(m.ProductCode, $"%{request.Search}%"));
        }

        var joinedQuery =
            from mv in baseQuery
            join p in context.Products on mv.ProductCode equals p.ProductCode
            where mv.VoucherCode == voucherCode
            select new WholeSaleDetailResponseModel
            {
                VoucherCode = mv.VoucherCode,
                ProductCode = mv.ProductCode,
                ProductName = p.Name,
                ProductQuantity = mv.ProductQuantity,
                ProductPrice = mv.ProductPrice,
                ProductTotalPrice = mv.ProductQuantity * mv.ProductPrice
            };

        var paginated = await joinedQuery.ToPagedListAsync(request);
        return ResponseModel<PaginationResponse<WholeSaleDetailResponseModel>>.Success(paginated);
    }

    public async Task<ResponseModel<GenerateWholeSaleVoucherResponseModel>> GenerateWholeSaleVoucherAsync(
        string voucherCode)
    {
        GenerateWholeSaleVoucherResponseModel response = new();
        var merchantVoucher =
            await context.MerchantVouchers.FirstOrDefaultAsync(x => x.VoucherCode == voucherCode && !x.IsDeleted);
        if (merchantVoucher is null)
        {
            return ResponseModel<GenerateWholeSaleVoucherResponseModel>.NotFound($"{voucherCode} is not found");
        }

        var merchantProducts = await (from v in context.MerchantVoucherProducts
                                      join p in context.Products on v.ProductCode equals p.ProductCode
                                      where v.VoucherCode == voucherCode
                                      select new WholeSaleVoucherList
                                      {
                                          No = 0,
                                          ProductName = p.Name,
                                          Color = context.MasterCodeItems
                                              .Where(x => x.Id == v.ColorId)
                                              .Select(x => x.Value)
                                              .FirstOrDefault(),
                                          Price = v.ProductPrice,
                                          Quantity = v.ProductQuantity,
                                          Amount = v.ProductPrice * v.ProductQuantity
                                      }).ToListAsync();


        for (var i = 0; i < merchantProducts.Count; i++)
        {
            merchantProducts[i].No = i + 1;
        }

        var merchant = await context.Merchants
            .FirstOrDefaultAsync(x => x.MerchantCode == merchantVoucher.MerchantCode);
        response.GenerateWholeSaleVoucher.MerchantName = merchant!.Name;
        response.GenerateWholeSaleVoucher.PhoneNumber = merchant!.PhoneNumber;
        response.GenerateWholeSaleVoucher.SaleDate = merchantVoucher.VoucherOpenedDate.ToString("yy-MMM-dd ddd");
        response.GenerateWholeSaleVoucher.PaymentStatus = merchantVoucher.PaidStatus;
        response.GenerateWholeSaleVoucher.MerchantVoucherLists = merchantProducts;
        response.GenerateWholeSaleVoucher.TotalAmount = merchantVoucher.TotalPrice;
        response.GenerateWholeSaleVoucher.LeftAmount = merchantVoucher.LeftPrice;
        response.GenerateWholeSaleVoucher.PaidAmount = merchantVoucher.TotalPrice - merchantVoucher.LeftPrice;
        return ResponseModel<GenerateWholeSaleVoucherResponseModel>.Success(response);
    }

    public async Task<ResponseModel<NoResponseModel>> OpenWholeSaleVoucherAsync(
        OpenWholeSaleVoucherRequestModel request)
    {
        NoResponseModel response = new();
        if (request.IsOldVoucher)
        {
            if (request.VoucherOpenedDate == DateTime.Now.ToMyanmarTime())
            {
                return ResponseModel<NoResponseModel>.BadRequest("Voucher opened date can't be in the future");
            }
        }

        await using (await context.Database.BeginTransactionAsync())
        {
            var count = await context.MerchantVouchers.CountAsync();
            MerchantVoucher merchantVoucher = new()
            {
                Id = Guid.NewGuid().ToString(),
                VoucherCode =
                    (CodeConstants.MERCHANT + CodeConstants.VOUCHER_CODE).GetCode(count,
                        CodeConstants.VOUCHER_CODE_DIGIT),
                MerchantCode = request.MerchantCode,
                VoucherOpenedDate = request.IsOldVoucher
                    ? DateTime.SpecifyKind(request.VoucherOpenedDate, DateTimeKind.Unspecified)
                    : DateTime.Now.ToMyanmarTime(),
                TotalPrice = request.ProductInfos.Sum(item => item.Price * item.Quantity),
                LeftPrice = request.ProductInfos.Sum(item => item.Price * item.Quantity),
                PaidStatus = PaidStage.UNPAID.ToString(),
                CreatedDate = DateTime.Now
            };
            await context.MerchantVouchers.AddAsync(merchantVoucher);
            await context.SaveChangesAsync();
            var mvpList = request.ProductInfos.Select(x => new MerchantVoucherProduct
            {
                Id = Guid.NewGuid().ToString(),
                VoucherCode = merchantVoucher.VoucherCode,
                ProductCode = x.ProductCode,
                ColorId = x.ProductColor,
                ProductQuantity = x.Quantity,
                ProductPrice = x.Price
            }).ToList();
            await context.MerchantVoucherProducts.AddRangeAsync(mvpList);
            await context.SaveChangesAsync();
            await context.Database.CommitTransactionAsync();
            return ResponseModel<NoResponseModel>.Success(response);
        }
    }
}