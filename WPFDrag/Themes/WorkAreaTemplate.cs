using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFDrag.Themes
{
	public class WorkAreaTemplate : Control
	{
		static WorkAreaTemplate()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkAreaTemplate), new FrameworkPropertyMetadata(typeof(WorkAreaTemplate)));
		}

		public WorkAreaTemplate()
		{

		}





		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register("Title", typeof(string), typeof(WorkAreaTemplate), new PropertyMetadata("Title"));





	}
}
