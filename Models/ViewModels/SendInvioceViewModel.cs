using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KseF.Interfaces;
using KseF.Models.Invoice_FA_2;

namespace KseF.Models.ViewModels
{
    public class SendInvioceViewModel
    {
        private readonly ILocalDbService _dbService;
        public ObservableCollection<Product> _products;
        public ObservableCollection<ClientEntities> _clients;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ClientEntities> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public SendInvioceViewModel(ILocalDbService dbService)
        {
            _dbService = dbService;
            LoadClients();
        }

        private async void LoadClients()
        {
            var products = await _dbService.GetItemsAsync<Product>();
            Products = new ObservableCollection<Product>(products);

            var clients = await _dbService.GetItemsAsync<ClientEntities>();
            Clients = new ObservableCollection<ClientEntities>(clients);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
