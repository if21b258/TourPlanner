﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlannerUI.View;
using TourPlannerModel;
using System.ComponentModel;
using TourPlannerBL;
using System.Windows;

namespace TourPlannerUI.ViewModel
{
    public class TourListViewModel : BaseViewModel
    {
        public ICommand AddTourCommand { get; set; }
        public ICommand DeleteTourCommand { get; set; }
        public ICommand EditTourCommand { get; set; }

        public event Action<TourModel> SelectedTourChanged;
        private TourService _tourService;
        private ObservableCollection<TourModel> _tourList;
        private TourModel _selectedTour;

        //public event EventHandler<TourModel> GetMapByRequest;

        //private TourService _tourServiceOfficer; vielleicht brauchen wir die beiden 

        //private MapQuest _mapQuest;

        public TourListViewModel(/*ITourManager tourManager, IMapQuest mapquest*/TourService tourService)
        {
            /*this.tourManager = tourManager;
            this.mapquest = mapquest;*/
            AddTourCommand = new RelayCommand<object>(AddTour);
            DeleteTourCommand = new RelayCommand<object>(DeleteTour);
            EditTourCommand = new RelayCommand<object>(EditTour);
            TourList = new ObservableCollection<TourModel>();
            _tourService = tourService;
        }

        public ObservableCollection<TourModel> TourList
        {
            get { return _tourList; }
            set
            {
                if (_tourList != value)
                {
                    _tourList = value;
                    RaisePropertyChangedEvent(nameof(TourList));
                }
            }
        }

        public TourModel SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                if(_selectedTour != value)
                {
                    _selectedTour = value;
                    OnSelectedTourChanged();
                }
            }
        }

        private void AddTour(object obj)
        {
            AddTourWindow addtour = new AddTourWindow();
            addtour.ShowDialog();
        }

        private void DeleteTour(object obj)
        {
            if (MessageBox.Show("Do you want to delete this Tour?",
                "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _tourService.DeleteTour(_selectedTour);
                _tourList.Remove(_selectedTour);
            }

            //DeleteTourWindow deleteTour = new DeleteTourWindow();
            //deleteTour.ShowDialog();
        }

        private void EditTour(object obj)
        {
            EditTourWindow editTour = new EditTourWindow();
            editTour.ShowDialog();
        }

        private void OnSelectedTourChanged()
        {
            SelectedTourChanged?.Invoke(SelectedTour);
        }
    }
}