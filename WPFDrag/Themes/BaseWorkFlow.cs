using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WPFDrag.Themes
{
    public abstract class BaseWorkFlow : Thumb,INotifyPropertyChanged,IWorkFlowAttributes
    {
        public WorkFlowEnum WorkFlowEnum { get; internal set; } = WorkFlowEnum.Base;

		public List<string> Attributes { get; internal set; }

		static BaseWorkFlow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseWorkFlow), new FrameworkPropertyMetadata(typeof(BaseWorkFlow)));
		
        }

       public BaseWorkFlow()
        {
			SetAttributes();
            DragDelta += BaseWorkFlow_DragDelta;
        }

		/// <summary>
		/// 初始化设置需要序列化的属性
		/// </summary>
		internal abstract void SetAttributes();

        private void BaseWorkFlow_DragDelta(object sender, DragDeltaEventArgs e)
        {
			Top += e.VerticalChange;
			Left += e.HorizontalChange;
		}

		public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(BaseWorkFlow), new PropertyMetadata(""));

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private double _left;

        public double Left
        {
            get { return _left; }
            set { _left = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Left"));
            }
        }


        private double _top;

        public double Top
        {
            get { return _top; }
            set { _top = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Top"));
            }
        }

		
	} 
}



