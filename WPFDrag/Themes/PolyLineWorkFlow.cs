using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPFDrag.Themes
{
	/// <summary>
	/// 流程图折线
	/// </summary>
    public class PolyLineWorkFlow :BaseWorkFlow
    {
		static PolyLineWorkFlow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PolyLineWorkFlow), new FrameworkPropertyMetadata(typeof(PolyLineWorkFlow)));
		}

		public PolyLineWorkFlow()
		{
			WorkFlowEnum = WorkFlowEnum.PolyLine;
		}


		public PointCollection Points
		{
			get { return (PointCollection)GetValue(PointsProperty); }
			set { SetValue(PointsProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PointsProperty =
			DependencyProperty.Register("Points", typeof(PointCollection), typeof(PolyLineWorkFlow), 
				new PropertyMetadata ( new PointCollection()));

	}
}
