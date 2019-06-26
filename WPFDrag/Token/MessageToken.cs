using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDrag.Token
{
	public static class MessageToken
	{
		/// <summary>
		/// 打开子流程窗口消息
		/// </summary>
		public static readonly string ChildWindowMessageToken;


		public static readonly string SendMessageToken;

		static MessageToken()
		{
			ChildWindowMessageToken = nameof(ChildWindowMessageToken);

		}
	}
}
