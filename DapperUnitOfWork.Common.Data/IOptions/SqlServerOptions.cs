namespace DapperUnitOfWork.Common.Data.IOptions;

public class SqlServerOptions
{
    public const string SectionName = $"{nameof(SqlServerOptions)}";
    public string DefaultConnection { get; set; } = null!;
}