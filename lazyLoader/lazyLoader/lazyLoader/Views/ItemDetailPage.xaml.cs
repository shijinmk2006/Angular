using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using lazyLoader.Models;
using lazyLoader.ViewModels;
using Xamarin.Forms.Extended;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace lazyLoader.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        InfiniteListViewViewModel viewModel;

        public ItemDetailPage(InfiniteListViewViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new InfiniteListViewViewModel();
            BindingContext = viewModel;
        }
        public class InfiniteListViewViewModel : INotifyPropertyChanged
        {
            public InfiniteScrollCollection<DataItem> Items { get; }

            bool _isLoadingMore;
            bool IsLoadingMore
            {
                get
                {
                    return _isLoadingMore;
                }
                set
                {
                    _isLoadingMore = value;
                    OnPropertyChanged(nameof(IsLoadingMore));
                }
            }

            public InfiniteListViewViewModel()
            {
                Items = new InfiniteScrollCollection<DataItem>
                {
                    OnLoadMore = async () =>
                    {
                        IsLoadingMore = true;

                        var items = GetItems(false);
                        //Call your Web API next items page.
                        await Task.Delay(1200);

                        IsLoadingMore = false;
                        return items;
                    }
                };
                Items.LoadMoreAsync();
            }

            InfiniteScrollCollection<DataItem> GetItems(bool clearList)
            {
                InfiniteScrollCollection<DataItem> items;
                if (clearList || Items == null)
                {
                    items = new InfiniteScrollCollection<DataItem>();
                }
                else
                {
                    items = new InfiniteScrollCollection<DataItem>(Items);
                }

                for (int i = 0; i < 20; i++)
                {
                    items.Add(new DataItem { Title = i.ToString() });
                }

                return items;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public class DataItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ImageUrl { get; set; }
            public string Price { get; set; }
            public string NewPrice { get; set; }
        }
    }
}