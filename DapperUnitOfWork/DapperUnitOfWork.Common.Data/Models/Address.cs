namespace DapperUnitOfWork.Common.Data.Models;

public class Address
{
    public long AddressID { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public long? StateProvinceID { get; set; }
    public string? PostalCode { get; set; }
    public DateTime? ModifiedDate { get; set; }
}