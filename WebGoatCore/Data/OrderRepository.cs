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
            // These commented lines cause EF Core to do wierd things.
            // Instead, make the query manually.

            //order = _context.Orders.Add(order).Entity;
            //_context.SaveChanges();
            //return order.OrderId;

            var sql = "INSERT INTO Orders (" +
                "CustomerId, EmployeeId, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, " +
                "ShipCity, ShipRegion, ShipPostalCode, ShipCountry" +
                ") VALUES (" +
                $"'{order.CustomerId}','{order.EmployeeId}','{order.OrderDate:yyyy-MM-dd}','{order.RequiredDate:yyyy-MM-dd}'," +
                $"'{order.ShippedDate:yyyy-MM-dd}','{order.ShipVia}','{order.Freight}','{order.ShipName}','{order.ShipAddress}'," +
                $"'{order.ShipCity}','{order.ShipRegion}','{order.ShipPostalCode}','{order.ShipCountry}')";
            sql += ";\nSELECT TOP 1 OrderID FROM Orders ORDER BY OrderID DESC;";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();

                using var dataReader = command.ExecuteReader();
                dataReader.Read();
                order.OrderId = (int)dataReader[0];
            }

            foreach (var orderDetails in order.OrderDetails)
            {
                orderDetails.OrderId = order.OrderId;
                sql = ";\nINSERT INTO OrderDetails (" +
                    "OrderId, ProductId, UnitPrice, Quantity, Discount" +
                    ") VALUES (" +
                    $"'{orderDetails.OrderId}','{orderDetails.ProductId}','{orderDetails.UnitPrice}','{orderDetails.Quantity}'," +
                    $"'{orderDetails.Discount}')";
            }

            if(order.Shipment != null)
            {
                var shipment = order.Shipment;
                shipment.OrderId = order.OrderId;
                sql += ";\nINSERT INTO Shipments (" +
                    "OrderId, ShipperId, ShipmentDate, TrackingNumber" +
                    ") VALUES (" +
                    $"'{shipment.OrderId}','{shipment.ShipperId}','{shipment.ShipmentDate:yyyy-MM-dd}','{shipment.TrackingNumber}')";
            }

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
            }

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
            return _context.Orders
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ThenByDescending(o => o.OrderId)
                .ToList();
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
