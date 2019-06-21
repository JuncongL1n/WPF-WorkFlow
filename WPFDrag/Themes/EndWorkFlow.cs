using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDrag.Themes
{
    /// <summary>
    /// 结束流程圆角矩形
    /// </summary>
    public class EndWorkFlow:BaseWorkFlow
    {
        static EndWorkFlow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EndWorkFlow), new FrameworkPropertyMetadata(typeof(EndWorkFlow)));
        }

        public EndWorkFlow()
        {
            WorkFlowEnum = WorkFlowEnum.End;
        }

		internal override void SetAttributes()
		{
			Attributes = new List<string> { "Height", "Width", "Left", "Top", "Content" };
		}
	}
}
