using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDrag.Themes
{
   /// <summary>
   /// 过程流程矩形
   /// </summary>
    public class ProcessWorkFlow : BaseWorkFlow
    {
        static ProcessWorkFlow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProcessWorkFlow), new FrameworkPropertyMetadata(typeof(ProcessWorkFlow)));
        }


        public ProcessWorkFlow()
        {
            WorkFlowEnum = WorkFlowEnum.Process;
        }
    

    }
}
