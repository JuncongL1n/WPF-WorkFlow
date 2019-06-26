using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using WPFDrag.Themes;

namespace WPFDrag.Model
{
    public class WorkFlowModel :ObservableObject
    {

		private WorkAreaModel _workArea;

		/// <summary>
		/// Sets and gets the WorkArea property. 工作区内容
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
		/// Sets and gets the ToolBox property. 工具箱内容
		/// Changes to that property's value raise the PropertyChanged event. 
		/// </summary>
		public WorkAreaModel ToolBox
		{
			get
			{ return _toolBox; }
			set
			{ _toolBox = value; RaisePropertyChanged(() => ToolBox); }
		}

		public WorkFlowModel()
		{
			WorkArea = new WorkAreaModel();
			ToolBox = new WorkAreaModel();
			InitToolBox();
		}

		/// <summary>
		/// 初始化工具箱
		/// </summary>
		public void InitToolBox()
		{
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

			BaseWorkFlow lineWF = WorkFlowFactory.GetWorkFlow(WorkFlowEnum.Line);
			lineWF.DragStarted += AddModel;
			ToolBox.WorkAreaItems.Add(lineWF);

			BaseWorkFlow polyLineWF = WorkFlowFactory.GetWorkFlow(WorkFlowEnum.PolyLine);
			polyLineWF.DragStarted += AddModel;
			ToolBox.WorkAreaItems.Add(polyLineWF);

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
		}

	}
}
