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
		private void RegistModelEvent(BaseWorkFlow workFlow)
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

	
		/// <summary>
		/// 保存工作区对象到xml
		/// </summary>
		/// <param name="fileName"></param>
		public void Save2XmlFile(string fileName)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes"));
			XmlElement xmlRoot = xmlDocument.CreateElement(string.Empty, "WrokFlow", string.Empty);
			AddChildXml(WorkAreaItems, xmlDocument,xmlRoot);		
			xmlDocument.AppendChild(xmlRoot);
			xmlDocument.Save(fileName);
		}

		/// <summary>
		/// 递归子节点，添加xml子节点
		/// </summary>
		/// <param name="workFlows"></param>
		/// <param name="xmlDocument"></param>
		/// <param name="xmlElement"></param>
		private void AddChildXml(ObservableCollection<BaseWorkFlow> workFlows, XmlDocument xmlDocument,XmlElement xmlElement)
		{
			foreach (IWorkFlowAttributes item in workFlows)  //遍历工作区所有对象
			{			
				Type type = item.GetType();
				//	var props = type.GetProperties();
				XmlElement itemNode = xmlDocument.CreateElement(string.Empty, type.Name, string.Empty); //新建节点 
				foreach (string propName in item.Attributes)   //遍历需要序列化的属性
				{
					if (propName == "ChildWorkFlow")
					{
						BaseWorkFlow wf = item as BaseWorkFlow;
						if(wf.ChildWorkFlow!=null)   //如果存在子流程则递归子流程
						{
							AddChildXml(wf.ChildWorkFlow.WorkArea.WorkAreaItems,xmlDocument, itemNode);
						}
					}
					else
					{      //不存在则设置xml节点属性
						itemNode.SetAttribute(propName, type.GetProperty(propName).GetValue(item, null).ToString());   //设置节点属性
					}				
				}
			    xmlElement.AppendChild(itemNode);  //附加对象节点到根节点
			}
		}

		/// <summary>
		/// 从xml文件加载对象
		/// </summary>
		/// <param name="fileName"></param>
		public void LoadXml(string fileName)
		{
			WorkAreaItems.Clear();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(fileName);
			XmlElement xmlRoot = xmlDocument.DocumentElement;
			LoadChildNode(WorkAreaItems, xmlRoot);
		}

		private void LoadChildNode(ObservableCollection<BaseWorkFlow> workFlows,XmlElement xmlElement)
		{
			XmlNodeList xmlNodeList = xmlElement.ChildNodes;			
			foreach (XmlElement xe in xmlNodeList)   //遍历模块节点
			{				
				Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
				BaseWorkFlow obj = assembly.CreateInstance("WPFDrag.Themes." + xe.Name) as BaseWorkFlow; //反射新建实例
				Type type = obj.GetType();
				var props = type.GetProperties();
				foreach (var prop in props)
				{
					if (xe.HasAttribute(prop.Name))  //判断是否有该属性
					{
						if (prop.Name == "Points")
						{
							PointCollection pc = PointCollection.Parse(xe.GetAttribute("Points"));  //设置复杂属性
							prop.SetValue(obj, pc);
						}					
						else
						{
							prop.SetValue(obj, Convert.ChangeType(xe.GetAttribute(prop.Name), prop.PropertyType));  //根据类型设置属性
						}
					}				
				}
				workFlows.Add(obj);
				RegistModelEvent(obj);
				if (xe.HasChildNodes)   //如果有子节点，则递归读取
				{				
					obj.ChildWorkFlow = new WorkFlowModel();
					LoadChildNode(obj.ChildWorkFlow.WorkArea.WorkAreaItems, xe);					
				}
			}
		}

    }
}
