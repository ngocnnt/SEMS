using SEMS_APP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SEMS_APP.ViewModels
{
    class AlarmsViewModel : BaseViewModel
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

        public Inverter _selectItem;
        public Inverter SelectItem
        {
            get => _selectItem;
            set => SetProperty(ref _selectItem, value);
        }
        public AlarmsViewModel()
        {
            ListInverters = new ObservableCollection<Inverter>();
            foreach (Inverter temp in clsVaribles._dtInverter)
            {
                ListInverters.Add(temp);
            }
        }
        public void Data2Grid()
        {
            ListInverters.Clear();
            foreach (Inverter tempAdd in clsVaribles._dtInverter)
            {
                ListInverters.Add(tempAdd);
            }
        }
    }
}
