using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WPFDrag.Model;
using WPFDrag.Themes;
using WPFDrag.Token;
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
			Messenger.Default.Register<object>(this, MessageToken.ChildWindowMessageToken, (o) =>
			{
				WorkFlowWinTemplate wft = new WorkFlowWinTemplate();
				wft.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				wft.DataContext = o;
				wft.Show();
			});
        }				
    }
}