using System;
using System.Collections.Generic;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterLayout : Page
    {
        private MemberServiceImp _service;
        public MasterLayout()
        {
            InitializeComponent();
            NavItemVisibleStatus();
        }

        // Add "using" for WinUI controls.
        // using muxc = Microsoft.UI.Xaml.Controls;

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("home", typeof(Home)),
            ("login", typeof(Login)),
            ("register", typeof(Register)),
            ("newSong", typeof(NewSong)),
            ("upload", typeof(upload)),
            ("mySong", typeof(mySong)),
            ("profile", typeof(Profile)),
            ("edit", typeof(edit)),
            ("logout", typeof(Logout))
        };

        public static void Refresh()
        {

        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // You can also add items in code.
            //NavView.MenuItems.Add(new NavigationViewItemSeparator());
            //NavView.MenuItems.Add(new NavigationViewItem
            //{
            //    Content = "My content",
            //    Icon = new SymbolIcon((Symbol)0xF1AD),
            //    Tag = "content"
            //});
            //_pages.Add(("content", null));

            // Add handler for ContentFrame navigation.
            ContentFrame.Navigated += On_Navigated;

            // NavView doesn't load any page by default, so load home page.
            NavView.SelectedItem = NavView.MenuItems[0];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            NavView_Navigate("home", new EntranceNavigationTransitionInfo());

            // Add keyboard accelerators for backwards navigation.
            var goBack = new KeyboardAccelerator { Key = VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;
            KeyboardAccelerators.Add(goBack);

            // ALT routes here
            var altLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            altLeft.Invoked += BackInvoked;
            KeyboardAccelerators.Add(altLeft);
        }

        private void NavView_ItemInvoked(NavigationView sender,
                                         NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
            NavItemVisibleStatus();
        }

        // NavView_SelectionChanged is not used in this example, but is shown for completeness.
        // You will typically handle either ItemInvoked or SelectionChanged to perform navigation,
        // but not both.
        private void NavView_SelectionChanged(NavigationView sender,
                                              NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavView_Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer != null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }

            
        }

        public void NavItemVisibleStatus()
        {
            _service = new MemberServiceImp();
            var response = _service.LoginWithToken();
            if (response != null)
            {
                if (response.email != null)
                {
                    Login.IsEnabled = false;
                    Register.IsEnabled = false;

                    Upload.IsEnabled = true;
                    NewSong.IsEnabled = true;
                    MySong.IsEnabled = true;
                    Profile.IsEnabled = true;
                    Edit.IsEnabled = true;
                    Logout.IsEnabled = true;
             
                }
                else
                {
                    Login.IsEnabled = true;
                    Register.IsEnabled = true;

                    Upload.IsEnabled = false;
                    NewSong.IsEnabled = false;
                    MySong.IsEnabled = false;
                    Profile.IsEnabled = false;
                    Edit.IsEnabled = false;
                    Logout.IsEnabled = false;
                }
            }
            else
            {
                Login.IsEnabled = true;
                Register.IsEnabled = true;

                Upload.IsEnabled = false;
                NewSong.IsEnabled = false;
                MySong.IsEnabled = false;
                Profile.IsEnabled = false;
                Edit.IsEnabled = false;
                Logout.IsEnabled = false;
            }
            
        }
        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type page;
            if (navItemTag != "settings")
            {
                _service = new MemberServiceImp();
                var response = _service.LoginWithToken();

                if (navItemTag == "login" || navItemTag == "register")
                {
                    
                    if (response != null)
                    {
                        if (response.email != null)
                        {
                            page = typeof(Home);
                        }
                        else
                        {
                            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                            page = item.Page;
                        }
                    }
                    else
                    {
                        var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                        page = item.Page;
                    }
                    
                }
                else if(navItemTag == "logout")
                {
                    _service.Logout();
                    page = typeof(Logout);
                }
                else
                {
                    if (response != null)
                    {
                        if (response.email != null)
                        {
                            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                            page = item.Page;
                        }
                        else
                        {
                            page = typeof(Home);
                        }
                    }
                    else
                    {
                        page = typeof(Home);
                    }

                }
            }
            else
            {
                page = typeof(setting);
            }

            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            // Only navigate if the selected page isn't currently loaded.
            if (!(page is null) && !Equals(preNavPageType, page))
            {
                ContentFrame.Navigate(page, null, transitionInfo);
            }


        }

        private void NavView_BackRequested(NavigationView sender,
                                           NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private void BackInvoked(KeyboardAccelerator sender,
                                 KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            // Don't go back if the nav pane is overlayed.
            if (NavView.IsPaneOpen &&
                (NavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(setting))
            {
                // SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag.
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
                NavView.Header = "Settings";
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.Tag));

                NavView.Header =
                    ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }
    }
}
