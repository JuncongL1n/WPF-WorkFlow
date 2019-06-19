using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using WPFDrag.Themes;

namespace WPFDrag.Model
{
    public class WorkAreaModel:ObservableObject
    {
        private ObservableCollection<BaseWorkFlow> _workAreaItems;

        /// <summary>
        /// Sets and gets the WorkAreaItems property.
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
        /// Sets and gets the SelectItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public BaseWorkFlow SelectItem
        {
            get
            { return _selectItem; }
            set
            { _selectItem = value; RaisePropertyChanged(() => SelectItem); }
        }

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
            addWf.DataContext = addWf;
            WorkAreaItems.Add(addWf);
            SelectItem = addWf;
            addWf.MouseRightButtonUp += AddWf_MouseRightButtonUp;
            addWf.DragStarted += AddWf_DragStarted; ;
        }


        private void AddWf_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            SelectItem = sender as BaseWorkFlow;
        }

        private void AddWf_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var wf = sender as BaseWorkFlow;
            if (wf != null)
            {
               RemoveModelFromWorkArea(wf);
            }
        }


    }
}
