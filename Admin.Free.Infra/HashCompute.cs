using System.Security.Cryptography;
using System.Text;

namespace Admin.Free.Infra
{
	public class HashCompute
	{

		public string GetMd5(string str)
		{ 
			var byrearr = Encoding.UTF8.GetBytes(str);

			var md5 = MD5.Create();

			var hash = md5.ComputeHash(byrearr);
			 
			var hashstr = string.Join("", hash.Select(x => x.ToString("x2")));

			return hashstr;
		}


		public string GetSHA1(string str)
		{

			var byrearr = Encoding.UTF8.GetBytes(str);

			var sha1 = SHA1.Create();

			var hash = sha1.ComputeHash(byrearr);

			var hashstr = string.Join("", hash.Select(x => x.ToString("x2")));

			return hashstr;
		}

	}
}