﻿namespace Application.UseCases.Shipping.dtos
{
    public class OutputDtoShippingInfos
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string? ShippingMethod { get; set; } // Standard, Express, etc.
    }
}
