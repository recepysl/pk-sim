using SimApi.Schema;

namespace SimApi.Operation.Token;

public interface ITokenService
{
    TokenResponse GetToken(TokenRequest request);
}
