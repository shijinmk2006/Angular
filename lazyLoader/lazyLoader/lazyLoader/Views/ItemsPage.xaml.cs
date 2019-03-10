using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using lazyLoader.Models;
using lazyLoader.Views;
using lazyLoader.ViewModels;
using static lazyLoader.Views.ItemDetailPage;
using Xamarin.Forms.Extended;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace lazyLoader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        InfiniteListViewViewModel viewModel;


        public ItemsPage()
        {
            InitializeComponent();


            BindingContext = viewModel = new InfiniteListViewViewModel();
        }
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public class InfiniteListViewViewModel : INotifyPropertyChanged
        {
            public InfiniteScrollCollection<DataItem> Items { get; }
            int COUNT = 0;
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
            InfiniteScrollCollection<DataItem> homeItems = null;
            public InfiniteListViewViewModel()
            {
                homeItems = new InfiniteScrollCollection<DataItem>(new[]
                 {
                    new DataItem { Id = 0,Price="Rs.1650",NewPrice="Rs.657",Title = "Smart Relaxed Fit Formal Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2338624/2018/2/6/11517900277289-WROGN-Men-Navy-Blue-Slim-Fit-Checked-Casual-Shirt-7881517900277120-1_mini.jpg", },
                    new DataItem { Id = 1, Price="Rs.2650",NewPrice="Rs.657",Title = "Smart Relaxed Fit Casual Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2032614/2017/10/24/11508834373908-WROGN-Men-Blue--White-Slim-Fit-Striped-Casual-Shirt-3881508834373652-1_mini.jpg" },
                    new DataItem { Id = 2, Price="Rs.1650",NewPrice="Rs.647",Title = "Smart Relaxed Fit Casual Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2338627/2018/2/6/11517914484169-WROGN-Men-Navy-Blue--Maroon-Regular-Fit-Checked-Casual-Shirt-3721517914483990-1_mini.jpg"  },
                    new DataItem { Id = 3, Price="Rs.1350",NewPrice="Rs.697",Title = "Smart Relaxed Fit Formal Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2478205/2018/5/25/8408b98c-500f-463c-a7f3-563d7d13bc5f1527242936529-WROGN-Men-Green-Manhattan-Slim-Fit-Checked-Casual-Shirt-7681527242935109-1_mini.jpg"},
                    new DataItem { Id = 4, Price="Rs.1650",NewPrice="Rs.697",Title = "Smart Relaxed Fit Casual Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/1502379/2016/11/9/11478687493658-WROGN-Men-Black--Olive-Green-Checked-Casual-Shirt-391478687493324-1_mini.jpg"},
                    new DataItem { Id = 5, Price="Rs.1950",NewPrice="Rs.617",Title = "Smart Fit Formal Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2478200/2018/3/9/11520581186038-WROGN-Men-Shirts-3461520581185674-1_mini.jpg"},
                    new DataItem { Id = 6, Price="Rs.1250",NewPrice="Rs.857",Title = "Smart Relaxed Fit Casual Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2478248/2018/2/23/11519376947954-WROGN-Men-Black--Red-Slim-Fit-Checked-Casual-Shirt-9121519376947738-1_mini.jpg"},
                    new DataItem { Id = 7, Price="Rs.1640",NewPrice="Rs.757",Title = "Smart Relaxed Fit Casual Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/4699468/2018/5/31/e461d4e5-b408-4fa9-9ae2-f963f59fca911527762018271-WROGN-Men-Purple--Red-Slim-Fit-Striped-Casual-Shirt-85115277-1_mini.jpg", },
                    new DataItem { Id = 8, Price="Rs.1850",NewPrice="Rs.687",Title = "Smart Relaxed Fit Casual Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2338705/2018/2/19/11519038692742-WROGN-Men-Shirts-8911519038692561-1_mini.jpg" },
                    new DataItem { Id = 9, Price="Rs.1620",NewPrice="Rs.957",Title = "Smart Relaxed  Casual Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2338570/2018/5/31/2d3dbb13-0839-4537-a8db-7b26ced505191527769515870-WROGN-Men-Black-Slim-Fit-Printed-Casual-Shirt-39215277695140-1_mini.jpg"  },
                    new DataItem { Id = 10, Price="Rs.1650",NewPrice="Rs.657",Title = "Smart Relaxed Fit Casual Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2338718/2018/1/17/11516166050176-WROGN-Men-Black--Grey-Regular-Fit-Checked-Casual-Shirt-1481516166050070-1_mini.jpg"},
                    new DataItem { Id = 11, Price="Rs.2650",NewPrice="Rs.637",Title = "Smart Relaxed Fit Formal Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2032685/2017/9/5/11504607857814-WROGN-Men-Gold-Toned--Blue-Slim-Fit-Printed-Casual-Shirt-8221504607857537-1_mini.jpg"},
                    new DataItem { Id = 12, Price="Rs.1250",NewPrice="Rs.657",Title = "Smart Relaxed Fit Formal Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2478214/2018/4/12/11523531611545-WROGN-Men-Khaki-Slim-Fit-Solid-Casual-Shirt-3861523531611399-1_mini.jpg"},
                    new DataItem { Id = 13, Price="Rs.1630",NewPrice="Rs.557",Title = "Smart Relaxed Fit Casual Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/1502380/2016/11/9/11478686816093-WROGN-Men-Navy-Checked-Casual-Shirt-3651478686815730-1_mini.jpg"},
                    new DataItem { Id = 14, Price="Rs.1450",NewPrice="Rs.627",Title = "Smart Relaxed Fit Casual Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2032636/2017/8/14/11502713439207-WROGN-Men-Off-White-Slim-Fit-Checked-Casual-Shirt-9481502713438924-1_mini.jpg", },
                    new DataItem { Id = 15, Price="Rs.1610",NewPrice="Rs.658",Title = "Smart Casual Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2032646/2018/4/13/11523597484913-WROGN-Men-Blue-Slim-Fit-Checked-Casual-Shirt-5651523597484734-1_mini.jpg" },
                    new DataItem { Id = 16, Price="Rs.1680",NewPrice="Rs.657",Title = "Smart Fit Casual Shirt",ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2032643/2018/5/21/9a1e941e-c56b-4a5a-9f7a-c744e8da69bf1526902046361-WROGN-Men-Beige--Navy-Slim-Fit-Checked-Casual-Shirt-5441526902044599-1_mini.jpg"  },
                    new DataItem { Id = 17, Price="Rs.1920",NewPrice="Rs.837",Title = "Smart Relaxed Fit Casual Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/2338565/2018/5/31/07152ba4-cb4b-43bc-a993-1b7b186782c21527769630054-WROGN-Men-Olive-Green-Slim-Fit-Printed-Casual-Shirt-34152776-1_mini.jpg"},
                    new DataItem { Id = 18, Price="Rs.1670",NewPrice="Rs.657",Title = "Smart Relaxed Fit Formal Shirt" ,ImageUrl="https://assets.myntassets.com/dpr_1.5/h_240,q_90,w_180/v1/assets/images/1700781/2016/12/26/11482739978885-WROGN-Men-Shirts-7871482739978587-1_mini.jpg"}
                });
                Items = new InfiniteScrollCollection<DataItem>
                {
                    OnLoadMore = async () =>
                    {
                        IsLoadingMore = true;
                        COUNT++;
                        var items = GetItems(false, COUNT);
                        //Call your Web API next items page.
                        await Task.Delay(1200);

                        IsLoadingMore = false;
                        return items;
                    }
                };
                Items.LoadMoreAsync();
            }

            InfiniteScrollCollection<DataItem> GetItems(bool clearList, int COUNT)
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
                int SKIP = COUNT * 10;

               var obj = homeItems.Skip(SKIP).Take(10);

                foreach(DataItem item in obj)
                {
                    items.Add(new DataItem { Title = item.Title.ToString(),ImageUrl=item.ImageUrl });
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