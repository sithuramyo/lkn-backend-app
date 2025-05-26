using Shared.Models.WholeSale;

namespace Shared.Extensions;

public static class MethodExtensions
{
    public static List<PayableWholeSaleVouchers> GetNearestVoucher(this List<PayableWholeSaleVouchers> list, decimal inputValue)
    {
        List<PayableWholeSaleVouchers> matchedVouchers = [];

        var sortedList = list.OrderBy(v => v.LeftAmount).ToList();

        foreach (var voucher in sortedList.TakeWhile(voucher => inputValue > 0))
        {
            if (voucher.LeftAmount <= inputValue)
            {
                matchedVouchers.Add(voucher);
                inputValue -= voucher.LeftAmount;
            }
            else
            {
                matchedVouchers.Add(voucher);
                inputValue -= voucher.LeftAmount;
            }
        }

        return matchedVouchers;
    }


}