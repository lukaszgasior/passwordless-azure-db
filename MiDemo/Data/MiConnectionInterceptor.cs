namespace MiDemo.Data
{
    using System.Data.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    public class MiConnectionInterceptor : DbConnectionInterceptor
    {
        private const string AzureSqlResourceId = "https://database.windows.net/";
        private readonly string _tenantId;
        private readonly AzureServiceTokenProvider _tokenProvider;

        public MiConnectionInterceptor(string tenantId)
        {
            _tenantId = string.IsNullOrEmpty(tenantId) ? null : tenantId;
            _tokenProvider = new AzureServiceTokenProvider();
        }

        public override async Task<InterceptionResult> ConnectionOpeningAsync(
            DbConnection connection,
            ConnectionEventData eventData,
            InterceptionResult result,
            CancellationToken cancellationToken = default)
        {
            var sqlConnection = (SqlConnection)connection;
            sqlConnection.AccessToken = await GetAccessTokenAsync();

            return result;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            return await _tokenProvider.GetAccessTokenAsync(AzureSqlResourceId, _tenantId);
        }
    }
}