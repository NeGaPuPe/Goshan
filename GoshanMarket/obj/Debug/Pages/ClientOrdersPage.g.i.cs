﻿#pragma checksum "..\..\..\Pages\ClientOrdersPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DFF4FC95892BE1845FFA0C97F6382CD7EA0540888F34E82233777D79273E441E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using GoshanMarket.Pages;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GoshanMarket.Pages {
    
    
    /// <summary>
    /// ClientOrdersPage
    /// </summary>
    public partial class ClientOrdersPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 21 "..\..\..\Pages\ClientOrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border MyProfileButton;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\ClientOrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border FavouritesButton;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Pages\ClientOrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border MyOrdersButton;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Pages\ClientOrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MyOrdersTextBlock;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Pages\ClientOrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView OrdersClientListView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GoshanMarket;component/pages/clientorderspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\ClientOrdersPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\Pages\ClientOrdersPage.xaml"
            ((GoshanMarket.Pages.ClientOrdersPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MyProfileButton = ((System.Windows.Controls.Border)(target));
            
            #line 21 "..\..\..\Pages\ClientOrdersPage.xaml"
            this.MyProfileButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.MyProfileButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\Pages\ClientOrdersPage.xaml"
            this.MyProfileButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.MyProfileButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\Pages\ClientOrdersPage.xaml"
            this.MyProfileButton.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.MyProfileButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FavouritesButton = ((System.Windows.Controls.Border)(target));
            
            #line 29 "..\..\..\Pages\ClientOrdersPage.xaml"
            this.FavouritesButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.FavouritesButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\Pages\ClientOrdersPage.xaml"
            this.FavouritesButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.FavouritesButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\Pages\ClientOrdersPage.xaml"
            this.FavouritesButton.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.FavouritesButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MyOrdersButton = ((System.Windows.Controls.Border)(target));
            
            #line 37 "..\..\..\Pages\ClientOrdersPage.xaml"
            this.MyOrdersButton.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.MyOrdersButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 44 "..\..\..\Pages\ClientOrdersPage.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MyOrdersTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.OrdersClientListView = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 8:
            
            #line 72 "..\..\..\Pages\ClientOrdersPage.xaml"
            ((System.Windows.Controls.Border)(target)).PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

