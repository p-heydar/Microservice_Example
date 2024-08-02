using Discount.Grpc.Data;
using Discount.Grpc.Models;

using Grpc.Core;

using Mapster;

using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var findCoupon = await dbContext
            .Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (findCoupon is null)
            findCoupon = new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Desc" };

        logger.LogInformation($"Discount Retrived For Product: {findCoupon.ProductName} And Amount: {findCoupon.Amount}");

        var couponModel = findCoupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Object."));

        await dbContext.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Discount Created For {coupon.ProductName} And Amount Is {coupon.Amount}");

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var findCoupon = await dbContext
            .Coupons.FirstOrDefaultAsync(x => x.Id == request.Coupon.Id);

        if (findCoupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Invalid Object By Id {request.Coupon.Id}"));

        var coupon = request.Coupon.Adapt<Coupon>();

        dbContext.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Coupon For {request.Coupon.ProductName} Updated");

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var findCouponByProductName = await dbContext
            .Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (findCouponByProductName is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"{request.ProductName} Was Not Found !!!"));

        dbContext.Remove(findCouponByProductName);
        await dbContext.SaveChangesAsync();

        logger.LogInformation($"Coupon For {request.ProductName} Deleted")

        return new DeleteDiscountResponse { Success = true };
    }
}