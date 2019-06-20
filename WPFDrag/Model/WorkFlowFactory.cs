using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFDrag.Themes;

namespace WPFDrag.Model
{
    public class WorkFlowFactory
    {
        public static BaseWorkFlow  GetWorkFlow(WorkFlowEnum workFlowEnum)
        {
			switch (workFlowEnum)
			{
				case WorkFlowEnum.Start:
					return new StartWorkFlow()
					{
						Height = 50,
						Width = 70,
						Left = 10,
						Top = 10,
						Content = "开始"
					};
				case WorkFlowEnum.Process:
					return new ProcessWorkFlow()
					{
						Height = 50,
						Width = 70,
						Left = 10,
						Top = 10,
						Content = "处理"
					};
				case WorkFlowEnum.Decision:
					return new DecisionWorkFlow()
					{
						Height = 50,
						Width = 50,
						Left = 10,
						Top = 10,
						Content = "决策"
					};
				case WorkFlowEnum.End:
					return new EndWorkFlow()
					{
						Height = 50,
						Width = 70,
						Left = 10,
						Top = 10,
						Content = "结束"
					};
				case WorkFlowEnum.Line:
					return new LineWorkFlow()
					{
						X1 = 0,
						Y1 = 0,
						X2 = 0,
						Y2 = 50,
						Left = 10,
						Top = 10,
						Height=50,
						Width=20
					};
				case WorkFlowEnum.PolyLine:
					return new PolyLineWorkFlow()
					{
						Points = new System.Windows.Media.PointCollection(new List<Point>() {
							new Point(){ X=70,Y=70},
							new Point(){X=70,Y=0},
							new Point(){X=0,Y=0}
						}),
						Height = 80,
						Width = 80
				
					};
					
                default: return null;
            }
            
        }
    }
}
