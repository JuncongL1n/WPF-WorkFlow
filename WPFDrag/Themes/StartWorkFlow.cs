using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDrag.Themes
{
    
 
    /// <summary>
    /// 开始流程圆形
    /// </summary>
    public class StartWorkFlow : BaseWorkFlow
    {
        static StartWorkFlow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StartWorkFlow), new FrameworkPropertyMetadata(typeof(StartWorkFlow)));
        }

        public StartWorkFlow()
        {
            WorkFlowEnum = WorkFlowEnum.Start;
        }

		internal override void SetAttributes()
		{
			Attributes = new List<string> { "Height", "Width", "Left", "Top", "Content"};
		}
    }
}
