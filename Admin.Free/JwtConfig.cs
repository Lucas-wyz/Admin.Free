using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Admin.Free
{
	/// <summary>
	/// jwt
	/// </summary>
	public class JwtConfig
	{
		/// <summary>
		/// 发行人Issuer
		/// </summary>
		public static readonly string issuer = "free";
		/// <summary>
		/// 订阅人Audience
		/// </summary>
		public static readonly string audience = "free";
		/// <summary>
		/// SecurityKey
		/// </summary>
		public static readonly string secretKey = "egh5t2eh545j2jsj575eisrtgxtrkmhj40wmsh54h21";

		/// <summary>
		/// SecurityKeyBytes
		/// </summary>
		public static readonly byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="claims"></param>
		/// <returns></returns>
		public static string CreateToken(IEnumerable<Claim> claims = null)
		{

			// 1. 定义需要使用到的Claims
			claims = claims ?? new List<Claim>();
			claims = claims.Append(new Claim(JwtRegisteredClaimNames.Jti, "admin"));

			// 2. 读取SecretKey
			var secretKey = new SymmetricSecurityKey(JwtConfig.secretKeyBytes);

			// 3. 选择加密算法
			var algorithm = SecurityAlgorithms.HmacSha256;

			// 4. 生成Credentials
			var signingCredentials = new SigningCredentials(secretKey, algorithm);

			// 5. 根据以上，生成token
			var jwtSecurityToken = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, notBefore: DateTime.Now, expires: DateTime.Now.AddHours(30), signingCredentials: signingCredentials);

			// 6. 将token变为string
			var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

			return token;
		}
	}


}