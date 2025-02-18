namespace Admin.Free
{
	/// <summary>
	/// 
	/// </summary>
	public class ResultObjet
	{
		/// <summary>
		/// ״̬��
		/// </summary>
		public int Code { get; set; }
		/// <summary>
		/// ����
		/// </summary>
		public required string Message { get; set; }
		/// <summary>
		/// ����
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
		/// ����
		/// </summary>
		public new T? Data { get; set; }
	}
}
