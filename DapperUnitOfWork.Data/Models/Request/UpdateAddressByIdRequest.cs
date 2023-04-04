namespace DapperUnitOfWork.Data.Models.Request;

public record UpdateAddressByIdRequest(
    int AddressId,
    string PostalCode);