using System;
using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repository.Orders
{
    partial class OrderRepository
    {
        // Nom de la table
        public const string TableName = "orders";

        // Colonnes
        public const string ColOrderId = "OrderId";
        public const string ColUserId = "UserId";
        public const string ColAmount = "Amount";
        public const string ColPaymentStatus = "PaymentStatus";
        public const string ColCreatedAt = "CreatedAt";

        // Nom de la table pour les relations
        public const string TableOrderBooks = "order_books";
        public const string ColOrderBookOrderId = "OrderId";
        public const string ColOrderBookBookId = "BookId";

        // Requête : Insérer une commande
        public static readonly string ReqCreateOrder = $@"
            INSERT INTO {TableName} ({ColUserId}, {ColAmount}, {ColPaymentStatus}, {ColCreatedAt})
            OUTPUT INSERTED.{ColOrderId}
            VALUES (@{ColUserId}, @{ColAmount}, @{ColPaymentStatus}, @{ColCreatedAt})";

        // Requête : Associer des livres à une commande
        public static readonly string ReqCreateOrderBooks = $@"
            INSERT INTO {TableOrderBooks} ({ColOrderBookOrderId}, {ColOrderBookBookId})
            VALUES (@{ColOrderBookOrderId}, @{ColOrderBookBookId})";

        // Requête : Obtenir une commande par ID
        public static readonly string ReqGetOrderById = $@"
            SELECT * FROM {TableName}
            WHERE {ColOrderId} = @{ColOrderId}";

        // Requête : Obtenir toutes les commandes pour un utilisateur
        public static readonly string ReqGetOrdersByUserId = $@"
            SELECT * FROM {TableName}
            WHERE {ColUserId} = @{ColUserId}";

        // Requête : Obtenir les livres associés à une commande
        public static readonly string ReqGetOrderBooks = $@"
            SELECT * FROM {TableOrderBooks}
            WHERE {ColOrderBookOrderId} = @{ColOrderBookOrderId}";

        // Requête : Supprimer une commande
        public static readonly string ReqDeleteOrder = $@"
            DELETE FROM {TableName}
            WHERE {ColOrderId} = @{ColOrderId}";

        // Requête : Supprimer les livres associés à une commande
        public static readonly string ReqDeleteOrderBooks = $@"
            DELETE FROM {TableOrderBooks}
            WHERE {ColOrderBookOrderId} = @{ColOrderBookOrderId}";

        // Requête : Mettre à jour le statut de paiement d'une commande
        public static readonly string ReqUpdatePaymentStatus = $@"
            UPDATE {TableName}
            SET {ColPaymentStatus} = @{ColPaymentStatus}
            WHERE {ColOrderId} = @{ColOrderId}";
    }
}
