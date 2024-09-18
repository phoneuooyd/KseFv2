using KseF.Services;
using KseF.Interfaces;

namespace KseF
{
    public partial class AppShell : Shell
    {
        private readonly ILocalDbService _dbService;

		public AppShell(ILocalDbService dbService)
        {
            InitializeComponent();
			_dbService = dbService;
		}
    }
}
