using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using WPFDrag.Themes;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Windows.Media;
using GalaSoft.MvvmLight.Messaging;
using WPFDrag.Token;

namespace WPFDrag.Model
{
    public class WorkAreaModel:ObservableObject
    {
        private ObservableCollection<BaseWorkFlow> _workAreaItems;

        /// <summary>
        /// Sets and gets the WorkAreaItems property.  工作区添加的流程图项目
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<BaseWorkFlow> WorkAreaItems
        {
            get
            { return _workAreaItems; }
            set
            { _workAreaItems = value; RaisePropertyChanged(() => WorkAreaItems); }
        }



        private BaseWorkFlow _selectItem;

        /// <summary>
        /// Sets and gets the SelectItem property. 鼠标选定的流程
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BaseWorkFlow SelectItem
        {
            get
            { return _selectItem; }
            set
            { _selectItem = value; RaisePropertyChanged(() => SelectItem); }
        }

		/// <summary>
		/// 双击添加子流程事件
		/// </summary>
		public event Action<WorkFlowModel> AddChildWorkFlowEventHandler;

        public WorkAreaModel()
        {
            WorkAreaItems = new ObservableCollection<BaseWorkFlow>();
        }
	

		/// <summary>
		/// 从工作区移除流程
		/// </summary>
		/// <param name="workFlow"></param>
		/// <param name="workArea"></param>
		public void RemoveModelFromWorkArea(BaseWorkFlow workFlow)
        {
            WorkAreaItems.Remove(workFlow);
        }

        /// <summary>
        /// 工作区添加流程
        /// </summary>
        /// <param name="workFlowEnum">流程类型</param>
        public void AddModelToWorkArea(WorkFlowEnum workFlowEnum)
        {
            BaseWorkFlow addWf = WorkFlowFactory.GetWorkFlow(workFlowEnum);
            WorkAreaItems.Add(addWf);
            SelectItem = addWf;
			RegistModelEvent(addWf);
        }

		/// <summary>
		/// 注册流程模块的事件和数据上下文
		/// </summary>
		/// <param name="workFlow"></param>
		public void RegistModelEvent(BaseWorkFlow workFlow)
		{
			workFlow.DataContext = workFlow;   //设定数据上下文
			workFlow.MouseRightButtonUp += AddWf_MouseRightButtonUp;   //注册右键删除事件
			workFlow.DragStarted += AddWf_DragStarted;      //注册鼠标选定事件 
			workFlow.MouseDoubleClick += WorkFlow_MouseDoubleClick;   //注册双击打开子流程事件
		}

		private void WorkFlow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			BaseWorkFlow wf = sender as BaseWorkFlow;
			if (wf.ChildWorkFlow == null)   
			{
				wf.ChildWorkFlow = new WorkFlowModel();    //新建子流程
			}

			if (AddChildWorkFlowEventHandler != null)
			{
				AddChildWorkFlowEventHandler(wf.ChildWorkFlow);    //调用事件处理委托通知
				if(wf.ChildWorkFlow.WorkArea.AddChildWorkFlowEventHandler == null)    //注册子流程双击事件 递归添加
				{
					wf.ChildWorkFlow.WorkArea.AddChildWorkFlowEventHandler += (obj) => Messenger.Default.Send<object>(obj, MessageToken.ChildWindowMessageToken);
				}
				
			}
		}

		private void AddWf_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            SelectItem = sender as BaseWorkFlow;
        }

        private void AddWf_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
			if (sender is BaseWorkFlow wf)
			{
				RemoveModelFromWorkArea(wf);
			}
		}

	
		
    }
}
