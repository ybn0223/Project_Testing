namespace E_CommerceApp.Interfaces
{
    public interface IOrderItem
    {
        string Name { get; set; }
        double Price { get; set; }
        int Quantity { get; set; }
    }
}