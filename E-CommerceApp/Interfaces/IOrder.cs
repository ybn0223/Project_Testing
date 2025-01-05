using E_CommerceApp.Models;

namespace E_CommerceApp.Interfaces
{
    public interface IOrder
    {
        bool DiscountApplied { get; set; }
        List<IOrderItem>? Items { get; set; }
        double Total { get; set; }
    }
}