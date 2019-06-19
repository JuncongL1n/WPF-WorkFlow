using GalaSoft.MvvmLight;
using System.Windows.Controls;
using WPFDrag.Model;
using WPFDrag.Themes;

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



        private WorkAreaModel _workArea;

        /// <summary>
        /// Sets and gets the WorkArea property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public WorkAreaModel WorkArea
        {
            get
            { return _workArea; }
            set
            { _workArea = value; RaisePropertyChanged(() => WorkArea); }
        }



        private WorkAreaModel _toolBox;

        /// <summary>
        /// Sets and gets the ToolBox property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public WorkAreaModel ToolBox
        {
            get
            { return _toolBox; }
            set
            { _toolBox = value; RaisePropertyChanged(() => ToolBox); }
        }

        public MainViewModel()
        {
            WorkArea = new WorkAreaModel();
            ToolBox = new WorkAreaModel();
            InitToolBox();
            
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

        /// <summary>
        /// 初始化工具箱
        /// </summary>
        public void InitToolBox()
        {
            #region 测试代码
            //BaseWorkFlow startWF = new StartWorkFlow()
            //{
            //    Width = 70,
            //    Height = 50,
            //    Content="开始"
            //};
            //startWF.DragStarted += StartWF_DragStarted;
            //BaseWorkFlow processWF = new ProcessWorkFlow()
            //{
            //    Width = 70,
            //    Height = 50,
            //    Content="处理"
            //};
            //BaseWorkFlow decisionWF = new DecisionWorkFlow()
            //{
            //    Width = 50,
            //    Height = 50,
            //    Content="决策"
            //};
            //BaseWorkFlow endWF = new EndWorkFlow()
            //{
            //    Width = 70,
            //    Height = 50,
            //    Content = "结束"
            //};
            //ToolBox.WorkAreaItems.Add(startWF);
            //ToolBox.WorkAreaItems.Add(processWF);
            //ToolBox.WorkAreaItems.Add(decisionWF);
            //ToolBox.WorkAreaItems.Add(endWF);
            #endregion
            BaseWorkFlow startWF = WorkFlowFactory.GetWorkFlow(WorkFlowEnum.Start);
            startWF.DragStarted += AddModel;
            ToolBox.WorkAreaItems.Add(startWF);

            BaseWorkFlow processWF = WorkFlowFactory.GetWorkFlow(WorkFlowEnum.Process);
            ToolBox.WorkAreaItems.Add(processWF);
            processWF.DragStarted += AddModel;

            BaseWorkFlow decisionWF = WorkFlowFactory.GetWorkFlow(WorkFlowEnum.Decision);
            ToolBox.WorkAreaItems.Add(decisionWF);
            decisionWF.DragStarted += AddModel;

            BaseWorkFlow endWF = WorkFlowFactory.GetWorkFlow(WorkFlowEnum.End);
            endWF.DragStarted += AddModel;
            ToolBox.WorkAreaItems.Add(endWF);
        }


        /// <summary>
        /// 添加流程到工作区中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddModel(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            BaseWorkFlow wf = sender as BaseWorkFlow;
            WorkArea.AddModelToWorkArea(wf.WorkFlowEnum);
            //BaseWorkFlow addWf = WorkFlowFactory.GetWorkFlow(wf.WorkFlowEnum);
            //addWf.DataContext = addWf;
            //WorkArea.WorkAreaItems.Add(addWf);
            //addWf.MouseRightButtonUp += AddWf_MouseRightButtonUp;
            //addWf.MouseLeftButtonUp += AddWf_MouseLeftButtonUp;
        }

      


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}