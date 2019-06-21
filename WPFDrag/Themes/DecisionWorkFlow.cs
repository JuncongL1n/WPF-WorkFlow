using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDrag.Themes
{
    /// <summary>
    /// j决策流程菱形
    /// </summary>
    public class DecisionWorkFlow :BaseWorkFlow
    {
        static DecisionWorkFlow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DecisionWorkFlow), new FrameworkPropertyMetadata(typeof(DecisionWorkFlow)));
        }

        public DecisionWorkFlow()
        {
            WorkFlowEnum = WorkFlowEnum.Decision;
        }

		internal override void SetAttributes()
		{
			Attributes = new List<string> { "Height", "Width", "Left", "Top", "Content" };
		}
	}
}
