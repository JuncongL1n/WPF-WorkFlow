using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDrag.Themes
{
	/// <summary>
	/// 流程图直线
	/// </summary>
    public class LineWorkFlow:BaseWorkFlow
    {
		static LineWorkFlow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(LineWorkFlow), new FrameworkPropertyMetadata(typeof(LineWorkFlow)));
		}

		public LineWorkFlow()
		{		
			WorkFlowEnum = WorkFlowEnum.Line;			
		}


		public double X1
		{
			get { return (double)GetValue(X1Property); }
			set { SetValue(X1Property, value); }
		}

		// Using a DependencyProperty as the backing store for X1.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty X1Property =
			DependencyProperty.Register("X1", typeof(double), typeof(LineWorkFlow), new PropertyMetadata(0.0d));



		public double X2
		{
			get { return (double)GetValue(X2Property); }
			set { SetValue(X2Property, value); }
		}

		// Using a DependencyProperty as the backing store for X2.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty X2Property =
			DependencyProperty.Register("X2", typeof(double), typeof(LineWorkFlow), new PropertyMetadata(0.0d));



		public double Y1
		{
			get { return (double)GetValue(Y1Property); }
			set { SetValue(Y1Property, value); }
		}

		// Using a DependencyProperty as the backing store for Y1.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty Y1Property =
			DependencyProperty.Register("Y1", typeof(double), typeof(LineWorkFlow), new PropertyMetadata(0.0d));




		public double Y2
		{
			get { return (double)GetValue(Y2Property); }
			set { SetValue(Y2Property, value); }
		}

		// Using a DependencyProperty as the backing store for Y2.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty Y2Property =
			DependencyProperty.Register("Y2", typeof(double), typeof(LineWorkFlow), new PropertyMetadata(0.0d));




	}
}
