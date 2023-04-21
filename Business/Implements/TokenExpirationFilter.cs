using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Client.Business
{
    public class TokenExpirationFilter : IAsyncActionFilter
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TokenExpirationFilter(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            
            // lấy thông tin hết hạn của token hiện tại
            var expiresAt = await httpContext.GetTokenAsync("expires_at");
            var accessTokenExpiration = DateTime.Parse(expiresAt, CultureInfo.InvariantCulture);
            var now = DateTime.Now;

            // Kiểm tra xem token có hết hạn không (trong ví dụ này, kiểm tra trong vòng 1 phút trước khi token hết hạn)
            if (accessTokenExpiration <= now)
            {
                // lấy thông tin refresh token
                var serverClient = _httpClientFactory.CreateClient();
                var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44367/");
                var idToken = await httpContext.GetTokenAsync("id_token");
                var refreshToken = await httpContext.GetTokenAsync("refresh_token");
                var refreshTokenClient = _httpClientFactory.CreateClient();
                var handler = new JwtSecurityTokenHandler();
                var accessToken = await httpContext.GetTokenAsync("access_token");
                var readAccessToken = handler.ReadJwtToken(accessToken);
                string clientID = readAccessToken.Claims.First(c => c.Type == "client_id").Value;
                string clientSecret = clientID == "client_id_mvc_1" ? "client_xuanhuongvanhungnguoiban_1" : "client_xuanhuongvanhungnguoiban_2";
                var tokenResponse = await refreshTokenClient.RequestRefreshTokenAsync(
                    new RefreshTokenRequest
                    {
                        Address = discoveryDocument.TokenEndpoint,
                        RefreshToken = refreshToken, 
                        ClientId = clientID,
                        ClientSecret = clientSecret
                    });
                var authInfo = await httpContext.AuthenticateAsync("Cookies");
                var expiresAtUpdate = DateTime.Now + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);
                authInfo.Properties.UpdateTokenValue("access_token", tokenResponse.AccessToken);
                authInfo.Properties.UpdateTokenValue("id_token", tokenResponse.IdentityToken);
                authInfo.Properties.UpdateTokenValue("refresh_token", tokenResponse.RefreshToken);
                authInfo.Properties.UpdateTokenValue("expires_at", expiresAtUpdate.ToString("o", CultureInfo.InvariantCulture));
                await httpContext.SignInAsync("Cookies", authInfo.Principal, authInfo.Properties);
            }

            // Tiếp tục thực thi action tiếp theo
            await next();
        }
    }
}
