using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        Height =50,
                        Width =70,
                        Left = 10,
                        Top = 10,
                        Content = "处理"
                    };
                case WorkFlowEnum.Decision:
                    return new DecisionWorkFlow()
                    {
                        Height = 50,
                        Width = 50 ,
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
                default:return null;
            }
            
        }
    }
}
