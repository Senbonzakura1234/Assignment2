﻿#pragma checksum "C:\Users\Aspire A315-51\source\repos\T1807EHello\T1807EHello\Pages\MasterLayout.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3E4495ADA743501F0F876C7D00D30C02"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace T1807EHello.Pages
{
    partial class MasterLayout : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\MasterLayout.xaml line 13
                {
                    this.NavView = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.NavView).Loaded += this.NavView_Loaded;
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.NavView).ItemInvoked += this.NavView_ItemInvoked;
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.NavView).BackRequested += this.NavView_BackRequested;
                }
                break;
            case 3: // Pages\MasterLayout.xaml line 21
                {
                    this.MainPagesHeader = (global::Windows.UI.Xaml.Controls.NavigationViewItemHeader)(target);
                }
                break;
            case 4: // Pages\MasterLayout.xaml line 23
                {
                    this.Upload = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 5: // Pages\MasterLayout.xaml line 24
                {
                    this.NewSong = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 6: // Pages\MasterLayout.xaml line 29
                {
                    this.MySong = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 7: // Pages\MasterLayout.xaml line 35
                {
                    this.Profile = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 8: // Pages\MasterLayout.xaml line 40
                {
                    this.Edit = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 9: // Pages\MasterLayout.xaml line 41
                {
                    this.Login = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 10: // Pages\MasterLayout.xaml line 42
                {
                    this.Register = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 11: // Pages\MasterLayout.xaml line 43
                {
                    this.Logout = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                }
                break;
            case 12: // Pages\MasterLayout.xaml line 53
                {
                    this.NavViewSearchBox = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                }
                break;
            case 13: // Pages\MasterLayout.xaml line 56
                {
                    this.ContentFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                    ((global::Windows.UI.Xaml.Controls.Frame)this.ContentFrame).NavigationFailed += this.ContentFrame_NavigationFailed;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

