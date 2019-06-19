using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WPFDrag.Model
{
    public class WorkFlowModel :ObservableObject
    {


        private int _height;

        /// <summary>
        /// Sets and gets the Height property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Height
        {
            get
            { return _height; }
            set
            { _height = value; RaisePropertyChanged(() => Height); }
        }



        private int _width;

        /// <summary>
        /// Sets and gets the Width property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Width
        {
            get
            { return _width; }
            set
            { _width = value; RaisePropertyChanged(() => Width); }
        }



        private string _content;

        /// <summary>
        /// Sets and gets the Content property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Content
        {
            get
            { return _content; }
            set
            { _content = value; RaisePropertyChanged(() => Content); }
        }



        private double _top;

        /// <summary>
        /// Sets and gets the Top property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Top
        {
            get
            { return _top; }
            set
            { _top = value; RaisePropertyChanged(() => Top); }
        }



        private double _left;

        /// <summary>
        /// Sets and gets the Left property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Left
        {
            get
            { return _left; }
            set
            { _left = value; RaisePropertyChanged(() => Left); }
        }
    }
}
