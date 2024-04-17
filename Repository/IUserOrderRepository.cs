using TechLife.Migrations;
using TechLife.Models;
using TechLife.Models.DTOs;

namespace TechLife.Repository
{
    public interface IUserOrderRepository
    {
        Task<IEnumerable<Order>> UserOrders(bool getAll=false);
        Task ChangeOrderStatus(UpdateOrderStatusModel data);
        Task TogglePaymentStatus(int orderId);
        Task<Order?> GetOrderById(int id);
        Task<IEnumerable<OrderStatus>> GetOrderStatuses();
        string GetUserId();


    }
}