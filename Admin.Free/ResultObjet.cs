namespace Admin.Free
{
	/// <summary>
	/// 
	/// </summary>
	public class ResultObjet
	{
		/// <summary>
		/// 状态码
		/// </summary>
		public int Code { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public required string Message { get; set; }
		/// <summary>
		/// 数据
		/// </summary>
		public object? Data { get; set; }
	}
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ResultObjet<T> : ResultObjet where T : class
	{
		/// <summary>
		/// 数据
		/// </summary>
		public new T? Data { get; set; }
	}
}
