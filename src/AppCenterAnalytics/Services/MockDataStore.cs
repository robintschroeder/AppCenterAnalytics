using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCenterAnalytics.Models;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppCenterAnalytics.Services.AppState))]
[assembly: Xamarin.Forms.Dependency(typeof(AppCenterAnalytics.Services.Logging))]

namespace AppCenterAnalytics.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        private readonly List<Item> items;
        private IAppState _appState;
        private ILogging _logging;

        //The Xamarin.Forms.DependencyService is a SERVICE LOCATOR and not an IoC CONTAINER
        //if it was an IoC container, we could inject these into the constructor like this:
        //public MockDataStore(IAppState appState, ILogging logging)...
        public MockDataStore()
        {
            _appState = DependencyService.Get<IAppState>();
            _logging = DependencyService.Get<ILogging>();

            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            _logging.LogEvent(AppLogLevel.Debug, "Item Added");

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            _logging.LogEvent(AppLogLevel.Debug, "Item Deleted");

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            _logging.LogEvent(AppLogLevel.Debug, "Item Updated");

            return await Task.FromResult(true);
        }
    }
}