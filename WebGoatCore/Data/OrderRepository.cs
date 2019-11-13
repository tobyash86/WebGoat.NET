using WebGoatCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebGoatCore.Data
{
    public class OrderRepository
    {
        private readonly NorthwindContext _context;
        private readonly CustomerRepository _customerRepository;

        public OrderRepository(NorthwindContext context, CustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }

        public Order GetOrderById(int orderId)
        {
            var order = _context.Orders.Single(o => o.OrderId == orderId);
            if (order.CustomerId.Length > 0)
            {
                order.Customer = _customerRepository.GetCustomerByCustomerId(order.CustomerId);
            }

            return order;
        }

        public int CreateOrder(Order order)
        {
            order = _context.Orders.Add(order).Entity;

            // XXX: Hack to get EF Core workin
            foreach (var od in order.OrderDetails)
            {
                _context.Entry(_context.Products.Find(od.ProductId)).State = EntityState.Unchanged;
            }

            _context.SaveChanges();
            return order.OrderId;
        }

        public int CreateOrder(List<OrderDetail> orderDetails, string customerId, decimal freight, int shipVia, string shipName, string shipAddress, string shipCity,
            string shipRegion, string shipPostalCode, string shipCountry)
        {
            var order = new Order()
            {
                CustomerId = customerId,
                Freight = freight,
                OrderDate = DateTime.Today,
                RequiredDate = DateTime.Today.AddDays(7.0),
                ShipAddress = shipAddress,
                ShipCity = shipCity,
                ShipCountry = shipCountry,
                ShipPostalCode = shipPostalCode,
                ShipRegion = shipRegion,
                ShipName = shipName,
                ShipVia = shipVia,
                OrderDetails = orderDetails
            };
            order = _context.Orders.Add(order).Entity;
            return order.OrderId;
        }

        public void CreateOrderPayment(int orderId, Decimal amountPaid, string creditCardNumber, DateTime expirationDate, string approvalCode)
        {
            var orderPayment = new OrderPayment()
            {
                AmountPaid = amountPaid,
                CreditCardNumber = creditCardNumber,
                ApprovalCode = approvalCode,
                ExpirationDate = expirationDate,
                OrderId = orderId,
                PaymentDate = DateTime.Now
            };
            _context.OrderPayments.Add(orderPayment);
            _context.SaveChanges();
        }

        public ICollection<Order> GetAllOrdersByCustomerId(string customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).OrderByDescending(o => o.OrderDate).ToList();
        }

        public Shipment GetShipmentByOrderId(int orderId)
        {
            return _context.Shipments.Single(s => s.OrderId == orderId);
        }

        public void UpdateOrder(Order order)
        {
            //TODO: _context.Update() ?
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

}
