using WebGoatCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;

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
            return _context.Orders.Single(o => o.OrderId == orderId);
        }

        public int CreateOrder(Order order)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            // These commented lines cause EF Core to do wierd things.
            // Instead, make the query manually.

            // order = _context.Orders.Add(order).Entity;
            // _context.SaveChanges();
            // return order.OrderId;

            string shippedDate = order.ShippedDate.HasValue ? "'" + string.Format("yyyy-MM-dd", order.ShippedDate.Value) + "'" : "NULL";
            var sql = "INSERT INTO Orders (" +
                "CustomerId, EmployeeId, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, " +
                "ShipCity, ShipRegion, ShipPostalCode, ShipCountry" +
                ") VALUES (" +
                $"'{order.CustomerId}','{order.EmployeeId}','{order.OrderDate:yyyy-MM-dd}','{order.RequiredDate:yyyy-MM-dd}'," +
                $"{shippedDate},'{order.ShipVia}','{order.Freight}','{order.ShipName}','{order.ShipAddress}'," +
                $"'{order.ShipCity}','{order.ShipRegion}','{order.ShipPostalCode}','{order.ShipCountry}')";
            sql += ";\nSELECT OrderID FROM Orders ORDER BY OrderID DESC LIMIT 1;";

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();

                using var dataReader = command.ExecuteReader();
                dataReader.Read();
                order.OrderId = Convert.ToInt32(dataReader[0]);
            }

            sql = ";\nINSERT INTO OrderDetails (" +
                "OrderId, ProductId, UnitPrice, Quantity, Discount" +
                ") VALUES ";
            foreach (var (orderDetails, i) in order.OrderDetails.WithIndex())
            {
                orderDetails.OrderId = order.OrderId;
                sql += (i > 0 ? "," : "") +
                    $"('{orderDetails.OrderId}','{orderDetails.ProductId}','{orderDetails.UnitPrice}','{orderDetails.Quantity}'," +
                    $"'{orderDetails.Discount}')";
            }

            if (order.Shipment != null)
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

        public void CreateOrderPayment(int orderId, decimal amountPaid, string creditCardNumber, DateTime expirationDate, string approvalCode)
        {
            var orderPayment = new OrderPayment()
            {
                AmountPaid = Convert.ToDouble(amountPaid),
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
    }
}
