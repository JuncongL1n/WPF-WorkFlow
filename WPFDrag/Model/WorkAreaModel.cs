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
		/// 需要序列化保存的属性
		/// </summary>
		private List<string> serializeProps = new List<string> { "Height","Width","Left","Top","Content","X1","Y1","X2","Y2","Points"};

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
          //  addWf.DataContext = addWf;
            WorkAreaItems.Add(addWf);
            SelectItem = addWf;
			RegistModelEvent(addWf);
          //  addWf.MouseRightButtonUp += AddWf_MouseRightButtonUp;
          //  addWf.DragStarted += AddWf_DragStarted; ;
        }

		/// <summary>
		/// 注册流程模块的事件和数据上下文
		/// </summary>
		/// <param name="workFlow"></param>
		private void RegistModelEvent(BaseWorkFlow workFlow)
		{
			workFlow.DataContext = workFlow;
			workFlow.MouseRightButtonUp += AddWf_MouseRightButtonUp;
			workFlow.DragStarted += AddWf_DragStarted; ;
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
			XmlNode xmlRoot = xmlDocument.CreateElement(string.Empty, "WrokFlow", string.Empty);
			foreach (var item in WorkAreaItems)  //遍历工作区所有对象
			{				
				Type type = item.GetType();
				var props = type.GetProperties();
				XmlNode itemNode = xmlDocument.CreateElement(string.Empty, type.Name, string.Empty); //新建节点 
				foreach (var prop in props)  //遍历所有属性
				{
					if (serializeProps.Contains(prop.Name))  //如果有需要序列化的属性
					{
						XmlNode xmlNode = xmlDocument.CreateElement(string.Empty, prop.Name, string.Empty); //新建节点 
						xmlNode.InnerText = prop.GetValue(item, null).ToString();  //得到属性的值
						itemNode.AppendChild(xmlNode);  //附加在节点
					}
				}
				xmlRoot.AppendChild(itemNode);  //附加对象节点到根节点
			}			
			xmlDocument.AppendChild(xmlRoot);
			xmlDocument.Save(fileName);
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
			XmlNode xmlRoot = xmlDocument.DocumentElement;
			XmlNodeList xmlNodeList = xmlRoot.ChildNodes;
			foreach (XmlNode li in xmlNodeList)   //遍历模块节点
			{
				Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
				dynamic obj = assembly.CreateInstance("WPFDrag.Themes."+li.Name); //反射新建实例
				Type type = obj.GetType();
				var propNodes = li.ChildNodes;
				foreach(XmlNode propNode in propNodes)   //遍历属性节点
				{
					var prop = type.GetProperty(propNode.Name);
					if (prop.Name == "Points")
					{
						PointCollection pc = PointCollection.Parse(propNode.InnerText);  //设置复杂属性
						prop.SetValue(obj, pc);
					}
					else
					{
						prop.SetValue(obj, Convert.ChangeType(propNode.InnerText, prop.PropertyType));  //根据类型设置属性
					}
				
				}
				WorkAreaItems.Add(obj);
				RegistModelEvent(obj);
			}
		}

    }
}
