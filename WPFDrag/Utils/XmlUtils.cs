using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;
using WPFDrag.Model;
using WPFDrag.Themes;

namespace WPFDrag.Utils
{
	public class XmlUtils
	{
		/// <summary>
		/// 保存工作区对象到xml
		/// </summary>
		/// <param name="fileName"></param>
		public static void Save2XmlFile(WorkAreaModel workArea,string fileName)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "yes"));
			XmlElement xmlRoot = xmlDocument.CreateElement(string.Empty, "WrokFlow", string.Empty);
			AddChildXml(workArea, xmlDocument, xmlRoot);
			xmlDocument.AppendChild(xmlRoot);
			xmlDocument.Save(fileName);
		}

		/// <summary>
		/// 递归子节点，添加xml子节点
		/// </summary>
		/// <param name="workFlows"></param>
		/// <param name="xmlDocument"></param>
		/// <param name="xmlElement"></param>
		private static void AddChildXml(WorkAreaModel workArea, XmlDocument xmlDocument, XmlElement xmlElement)
		{
			foreach (IWorkFlowAttributes item in workArea.WorkAreaItems)  //遍历工作区所有对象
			{
				Type type = item.GetType();
				//	var props = type.GetProperties();
				XmlElement itemNode = xmlDocument.CreateElement(string.Empty, type.Name, string.Empty); //新建节点 
				foreach (string propName in item.Attributes)   //遍历需要序列化的属性
				{
					if (propName == "ChildWorkFlow")
					{
						BaseWorkFlow wf = item as BaseWorkFlow;
						if (wf.ChildWorkFlow != null)   //如果存在子流程则递归子流程
						{
							AddChildXml(wf.ChildWorkFlow.WorkArea, xmlDocument, itemNode);
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
		public static void LoadXml(WorkAreaModel workArea,string fileName)
		{
			workArea.WorkAreaItems.Clear();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(fileName);
			XmlElement xmlRoot = xmlDocument.DocumentElement;
			LoadChildNode(workArea, xmlRoot);
		}

		private static void LoadChildNode(WorkAreaModel workArea, XmlElement xmlElement)
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
				workArea.WorkAreaItems.Add(obj);
				workArea.RegistModelEvent(obj);
				if (xe.HasChildNodes)   //如果有子节点，则递归读取
				{
					obj.ChildWorkFlow = new WorkFlowModel();
					LoadChildNode(obj.ChildWorkFlow.WorkArea, xe);
				}
			}
		}
	}
}
