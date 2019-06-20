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
    public class BaseWorkFlow : Thumb,INotifyPropertyChanged
    {
        public WorkFlowEnum WorkFlowEnum { get; internal set; } = WorkFlowEnum.Base;
        static BaseWorkFlow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseWorkFlow), new FrameworkPropertyMetadata(typeof(BaseWorkFlow)));
        }

       public BaseWorkFlow()
        {
            DragDelta += BaseWorkFlow_DragDelta;
           // MouseRightButtonUp += BaseWorkFlow_MouseRightButtonUp;
        }

        //private void BaseWorkFlow_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var parent = VisualTreeHelper.GetParent(this);

        //    var canvas = parent as Canvas;
        //    if(canvas != null)
        //    {
        //        canvas.Children.Remove(this);
        //    }
        //}

        private void BaseWorkFlow_DragDelta(object sender, DragDeltaEventArgs e)
        {
			//BaseWorkFlow myThumb = (BaseWorkFlow)sender;
			//double nTop = Canvas.GetTop(myThumb) + e.VerticalChange;
			//double nLeft = Canvas.GetLeft(myThumb) + e.HorizontalChange;
			//Canvas.SetTop(myThumb, nTop);
			//Canvas.SetLeft(myThumb, nLeft);
			//Console.WriteLine("top:{0},left:{1}", nTop, nLeft);
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

    public enum WorkFlowEnum
    {
        Base,
        Start,
        Process,
        Decision,
        End,
		Line,
		PolyLine	
    }
}



