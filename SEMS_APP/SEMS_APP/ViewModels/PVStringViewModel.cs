using SEMS_APP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SEMS_APP.ViewModels
{
    public class PVStringViewModel : BaseViewModel
    {
        ObservableCollection<PVString> _listPVString;
        public ObservableCollection<PVString> ListPVString
        {
            get { return _listPVString; }
            set
            {
                _listPVString = value;
                OnPropertyChanged("ListPVString");
            }
        }
        public Inverter _selectItem;
        public Inverter SelectItem
        {
            get => _selectItem;
            set => SetProperty(ref _selectItem, value);
        }
        int _animationDuration;
        public int AnimationDuration { get => _animationDuration; set => SetProperty(ref _animationDuration, value); }
        public PVStringViewModel()
        {
            ListPVString = new ObservableCollection<PVString>();
            foreach (PVString temp in clsVaribles._dtPVString)
            {
                ListPVString.Add(temp);
            }
        }
        public void Data2Grid()
        {
            ListPVString.Clear();
            //ListPVString = new ObservableCollection<PVString>();
            //ListPVString = clsVaribles._dtPVString;
            foreach (PVString tempAdd in clsVaribles._dtPVString)
            {
                ListPVString.Add(tempAdd);
            }
        }
    }
}
