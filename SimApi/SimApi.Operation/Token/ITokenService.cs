using SimApi.Base;
using SimApi.Schema;

namespace SimApi.Operation;

public interface ITokenService
{
    ApiResponse<TokenResponse> GetToken(TokenRequest request);
}
