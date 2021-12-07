using SEMS_APP.Interface;
using SEMS_APP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SEMS_APP.ViewModels
{
    public class InvertersViewModel : BaseViewModel
    {
        ObservableCollection<Inverter> _listInverters;
        public ObservableCollection<Inverter> ListInverters 
        {
            get { return _listInverters; }
            set
            {
                _listInverters = value;
                OnPropertyChanged("ListInverters");

            }
        }
        int _animationDuration;
        public int AnimationDuration { get => _animationDuration; set => SetProperty(ref _animationDuration, value); }

        //ObservableCollection<Rectangle> _listBarBounds;
        //public ObservableCollection<Rectangle> BarBounds
        //{
        //    get => _listBarBounds;
        //    set => SetProperty(ref _listBarBounds, value);
        //}
        public Rectangle BarBounds { get; set; }
        public Inverter _selectItem;
        public Inverter SelectItem 
        { 
            get => _selectItem; 
            set => SetProperty(ref _selectItem, value); 
        }
        public InvertersViewModel()
        {
            ListInverters = clsVaribles._dtInverter;
            //ListInverters = new ObservableCollection<Inverter>();
            //foreach (Inverter temp in clsVaribles._dtInverter)
            //{
            //    ListInverters.Add(temp);
            //    //BarBounds = new Rectangle(0, 0, (double)temp.PERFORMANCE, 1);
            //}
        }
        public void Data2Grid()
        {
            ListInverters = clsVaribles._dtInverter;
            //ListInverters.Clear();
            //foreach (Inverter tempAdd in clsVaribles._dtInverter)
            //{
            //    ListInverters.Add(tempAdd);
            ////    //BarBounds = new Rectangle(0, 0, (double)tempAdd.PERFORMANCE, 1);
            ////    //BarBounds.Add(new Rectangle(0, 0, (double) tempAdd.PERFORMANCE, 1));
            //}
        }
    }
}
