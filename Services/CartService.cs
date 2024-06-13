using BlendMaster.Entities;
using BlendMaster.Models;
using System.Linq;

namespace BlendMaster.Services
{
    public class CartService
    {
        private readonly EnDbContext _context;

        public CartService(EnDbContext context)
        {
            _context = context;
        }

        public Order CheckOut(CartViewModel cart)
        {

            // 检查桌号是否存在
            var table = _context.TableList.Find(cart.TableId);
            if (table == null)
            {
                throw new Exception("Table not found.");
            }
            // 存在的话生成Order对象
            var newOrder = new Order
            {
                OrderId = new int(),
                TableID = cart.TableId,
                OrderMoney = cart.TotalPrice,
                OrderTime = DateTime.Now
            };

            //数据库中添加订单
            _context.OrderList.Add(newOrder);

            // 确保OrderId生成并保存
            _context.SaveChanges();  

            // 生成OrderDetail对象
            foreach (var item in cart.Items)
            {
                var orderDetail = new OrderDetail
                {
                    OrderDetailId = new int(),
                    OrderID = newOrder.OrderId,
                    WineID = item.WineId
                };
                // 数据库中添加订单详细信息
                _context.OrderDetailList.Add(orderDetail);
            }

            // 确保调用 SaveChanges 以保存更改
            _context.SaveChanges();  
            return newOrder;
        }
    }
}
