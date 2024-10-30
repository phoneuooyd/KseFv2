using KseF.Interfaces;
using KseF.Models.Invoice_FA_2;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;

namespace KseF.Models.ViewModels
{
    public class InvoicesSentViewModel : INotifyPropertyChanged
    {
        private readonly ILocalDbService _dbService;
        public ObservableCollection<BaseFaktura> _invoices;

        public ObservableCollection<BaseFaktura> Invoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; }

        public InvoicesSentViewModel(ILocalDbService dbService)
        {
            _dbService = dbService;
            DeleteCommand = new Command<BaseFaktura>(async (entity) => await DeleteEntity(entity));
            LoadInvoices();
        }

        private async void LoadInvoices()
        {
            var invoices = await _dbService.GetItemsAsync<BaseFaktura>();
            Invoices = new ObservableCollection<BaseFaktura>(invoices);
        }

        private async Task DeleteEntity(BaseFaktura entity)
        {
            if (entity == null) return;

            Invoices.Remove(entity);
            await _dbService.DeleteItemAsync(entity);
            WeakReferenceMessenger.Default.Send(new EntityDeletedMessage<BaseFaktura>(entity));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
