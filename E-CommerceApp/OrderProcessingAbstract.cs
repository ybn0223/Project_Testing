using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;

namespace E_CommerceApp
{
    public class OrderProcessingAbstract
    {
        private readonly Order _order;

        public OrderProcessingAbstract(Order order)
        {
            _order = order;
        }

        public void ApplyDiscount(double discountPercentage)
        {
            if (_order.Items != null)
            {
                _order.Total = _order.Items.Sum(item => item.Price * item.Quantity);
                _order.Total *= 1 - discountPercentage / 100;
                _order.DiscountApplied = true;
            }
        }

        public double CalculateTotal()
        {
            return _order.Items.Sum(item => item.Price * item.Quantity);
        }

        public bool CheckStock(Dictionary<string, int> stock)
        {
            foreach (var item in _order.Items)
            {
                if (stock.ContainsKey(item.Name) && stock[item.Name] < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
