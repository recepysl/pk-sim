using SimApi.Base;
using SimApi.Schema;

namespace SimApi.Operation;

public interface ITokenService
{
    BaseResponse<TokenResponse> GetToken(TokenRequest request);
}
