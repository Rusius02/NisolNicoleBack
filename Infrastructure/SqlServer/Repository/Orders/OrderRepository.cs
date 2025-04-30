using Domain;
using Infrastructure.SqlServer.Repository.Books;
using Infrastructure.SqlServer.Utils;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.Orders
{
    public partial class OrderRepository : IOrderRepository
    {
        private readonly IDomainFactory<Order> _factory = new OrderFactory();

        // Créer une commande
        public Order Create(Order order)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            // Transaction pour garantir la cohérence des données
            using var transaction = connection.BeginTransaction();

            try
            {
                // Insérer la commande dans la table `orders`
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = ReqCreateOrder,
                    Transaction = transaction
                };
                command.Parameters.AddWithValue("@" + ColUserId, order.UserId);
                command.Parameters.AddWithValue("@" + ColAmount, order.Amount);
                command.Parameters.AddWithValue("@" + ColPaymentStatus, order.PaymentStatus);
                command.Parameters.AddWithValue("@" + ColCreatedAt, order.CreatedAt);

                order.OrderId = (int)command.ExecuteScalar();

                // Associer les livres à la commande dans la table `order_books`
                foreach (var orderBook in order.OrderBooks)
                {
                    var orderBooksCommand = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = ReqCreateOrderBooks,
                        Transaction = transaction
                    };
                    orderBooksCommand.Parameters.AddWithValue("@" + ColOrderBookOrderId, order.OrderId);
                    orderBooksCommand.Parameters.AddWithValue("@" + ColOrderBookBookId, orderBook.BookId);
                    orderBooksCommand.ExecuteNonQuery();
                }
                transaction.Commit();
                return order;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // Supprimer une commande
        public bool Delete(Order order)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqDeleteOrder
            };
            command.Parameters.AddWithValue("@" + ColOrderId, order.OrderId);

            return command.ExecuteNonQuery() > 0;
        }

        // Obtenir toutes les commandes
        public List<Order> GetAll()
        {
            var orders = new List<Order>();
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetOrdersByUserId
            };

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                orders.Add(_factory.CreateFromSqlReader(reader));
            }

            return orders;
        }

        // Obtenir une commande par ID
        public Order GetOrderById(int orderId)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetOrderById
            };

            command.Parameters.AddWithValue("@" + ColOrderId, orderId);

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _factory.CreateFromSqlReader(reader) : null;
        }

        // Mettre à jour le statut de paiement d'une commande
        public bool UpdatePaymentStatus(int orderId, string paymentStatus)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqUpdatePaymentStatus
            };
            command.Parameters.AddWithValue("@" + ColOrderId, orderId);
            command.Parameters.AddWithValue("@" + ColPaymentStatus, paymentStatus);

            return command.ExecuteNonQuery() > 0;
        }
        public bool Update(Order order)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = $@"
            UPDATE {TableName}
            SET 
                {ColUserId} = @{ColUserId}, 
                {ColAmount} = @{ColAmount}, 
                {ColPaymentStatus} = @{ColPaymentStatus}, 
                {ColStripePaymentIntentId} = @{ColStripePaymentIntentId}, 
                {ColCreatedAt} = @{ColCreatedAt}
            WHERE {ColOrderId} = @{ColOrderId}"
            };

            command.Parameters.AddWithValue("@" + ColOrderId, order.OrderId);
            command.Parameters.AddWithValue("@" + ColUserId, order.UserId);
            command.Parameters.AddWithValue("@" + ColAmount, order.Amount);
            command.Parameters.AddWithValue("@" + ColPaymentStatus, order.PaymentStatus);
            command.Parameters.AddWithValue("@" + ColStripePaymentIntentId, order.StripePaymentIntentId ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@" + ColCreatedAt, order.CreatedAt);

            return command.ExecuteNonQuery() > 0;
        }


        // Obtenir les livres associés à une commande
        public List<Book> GetBooksByOrderId(int orderId)
        {
            var books = new List<Book>();
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetOrderBooks
            };
            command.Parameters.AddWithValue("@" + ColOrderBookOrderId, orderId);

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                // Créez une méthode dans BookFactory pour transformer un SqlDataReader en Book
                books.Add(new BookFactory().CreateFromSqlReader(reader));
            }

            return books;
        }
    }
}
