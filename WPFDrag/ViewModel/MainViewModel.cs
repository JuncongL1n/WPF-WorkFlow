using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using System.Windows.Forms;
using WPFDrag.Model;
using WPFDrag.Themes;
using WPFDrag.Token;
using WPFDrag.Utils;

namespace WPFDrag.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "可视化建模工具";

        private string _welcomeTitle = "可视化建模工具";

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

		public WorkFlowModel WorkFlowModel { get; set; }

      
        public MainViewModel()
        {
			WorkFlowModel = new WorkFlowModel();
			WorkFlowModel.WorkArea.AddChildWorkFlowEventHandler += WorkArea_AddChildWorkFlowEventHandler;
            
            //BaseWorkFlow wf = new ProcessWorkFlow()
            //{
            //    Height = 80,
            //    Left=40,
            //    Top=30,
            //    Width = 80
            //};
            //wf.DataContext = wf;
            //WorkArea.WorkAreaItems.Add(wf);
        }

		private void WorkArea_AddChildWorkFlowEventHandler(WorkFlowModel obj)
		{
			Messenger.Default.Send<object>(obj,MessageToken.ChildWindowMessageToken);
		}

		private RelayCommand _cmdSave;

		/// <summary>
		/// Gets and Sets the CmdSave.
		/// </summary>
		public RelayCommand CmdSave
		{
			get
			{
				if (_cmdSave == null)
					return new RelayCommand(() => CmdSaveExecute() );
				return _cmdSave;
			}
			set
			{
				_cmdSave = value;
			}
		}

		private void CmdSaveExecute()
		{
			if (WorkFlowModel.WorkArea.WorkAreaItems.Count < 1)
			{
				MessageBox.Show("工作区没有内容");
				return;
			}
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.InitialDirectory = @"D:\";
			sfd.Filter = "xml file|*.xml";
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				//WorkFlowModel.WorkArea.Save2XmlFile(sfd.FileName);
				XmlUtils.Save2XmlFile(WorkFlowModel.WorkArea, sfd.FileName);
			}
		}


		private RelayCommand _cmdLoad;

		/// <summary>
		/// Gets and Sets the CmdLoad.
		/// </summary>
		public RelayCommand CmdLoad
		{
			get
			{
				if (_cmdLoad == null)
					return new RelayCommand(() => CmdLoadExecute());
				return _cmdLoad;
			}
			set
			{
				_cmdLoad = value;
			}
		}

		private void CmdLoadExecute()
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "xml file(*.xml)|*.xml";
			ofd.ValidateNames = true;
			ofd.CheckPathExists = true;
			ofd.CheckFileExists = true;
			if (ofd.ShowDialog() == DialogResult.OK)
			{

				//WorkFlowModel.WorkArea.LoadXml(ofd.FileName);
				XmlUtils.LoadXml(WorkFlowModel.WorkArea, ofd.FileName);
			}
		}

     
      


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}