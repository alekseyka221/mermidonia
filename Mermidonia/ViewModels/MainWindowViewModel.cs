using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Mermidonia.Models;
using Nito.AsyncEx;
using ReactiveUI;

namespace Mermidonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Init _init;

        public string Greeting => "Welcome to Avalonia!";
        public INotifyTaskCompletion InitializationNotifier { get; private set; }

        public MainWindowViewModel()
        {
            InitializationNotifier = NotifyTaskCompletion.Create(InitializeAsync());
        }

        private async Task InitializeAsync()
        {
            _init = new Init();
            var data = await _init.Hydrate();
            
            MyItems = new ObservableCollection<CommitItem>(data);
        }
        
        private ObservableCollection<CommitItem> _myItems;
        public ObservableCollection<CommitItem> MyItems
        {
            get => _myItems;
            set
            {
                if (value != null)
                {
                    _myItems = value;
                    OnPropertyChanged();
                }
            }
        }
    }

    
}