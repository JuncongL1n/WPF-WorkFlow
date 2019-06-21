using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPFDrag.Model;
using WPFDrag.Themes;
using WPFDrag.ViewModel;

namespace WPFDrag
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

	

		public Thumb selectItem;

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb myThumb = (Thumb)sender;
            double nTop = Canvas.GetTop(myThumb) + e.VerticalChange;
            double nLeft = Canvas.GetLeft(myThumb) + e.HorizontalChange;
            Canvas.SetTop(myThumb, nTop);
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
          
          
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            
            selectItem = null;
        }



        private void ProcessWorkFlow_DragStarted(object sender, DragStartedEventArgs e)
        {
            BaseWorkFlow wf = sender as BaseWorkFlow;
            BaseWorkFlow addWf = WorkFlowFactory.GetWorkFlow(wf.WorkFlowEnum);
            selectItem = addWf;
            Canvas.SetTop(addWf, 10);
            Canvas.SetLeft(addWf, 10);
            
            
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
           
        }

        private void ProcessWorkFlow_DragOver(object sender, DragEventArgs e)
        {
            selectItem = null;
        }


    }
}