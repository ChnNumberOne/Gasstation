﻿#pragma checksum "..\..\..\Fenster\AlterItemsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B77D419F6264B85F24FD4D29AB4683FD78D45F128D3EA51DA601FCA2DBC286BD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WPF_OOP_Testing.Fenster;
using WPF_OOP_Testing.Models;
using WPF_OOP_Testing.Viewmodels;
using WPF_OOP_Testing.Views;


namespace WPF_OOP_Testing.Fenster {
    
    
    /// <summary>
    /// AlterItemsPage
    /// </summary>
    public partial class AlterItemsPage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ButtonsPanel;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TankstellenNameBox;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StelleAddenButton;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StelleLoeschenButton;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AnzZapfsäulenBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SelectZapfseule;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FuelOverview;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ZapfseulenOverview;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\Fenster\AlterItemsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl LastColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/WPF OOP Testing;component/fenster/alteritemspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Fenster\AlterItemsPage.xaml"
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
            this.ButtonsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.TankstellenNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.StelleAddenButton = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\Fenster\AlterItemsPage.xaml"
            this.StelleAddenButton.Click += new System.Windows.RoutedEventHandler(this.StelleAddenButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.StelleLoeschenButton = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\Fenster\AlterItemsPage.xaml"
            this.StelleLoeschenButton.Click += new System.Windows.RoutedEventHandler(this.StelleLoeschenButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AnzZapfsäulenBox = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.SelectZapfseule = ((System.Windows.Controls.ComboBox)(target));
            
            #line 70 "..\..\..\Fenster\AlterItemsPage.xaml"
            this.SelectZapfseule.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SelectZapfseule_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.FuelOverview = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\..\Fenster\AlterItemsPage.xaml"
            this.FuelOverview.Click += new System.Windows.RoutedEventHandler(this.FuelOverview_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ZapfseulenOverview = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\Fenster\AlterItemsPage.xaml"
            this.ZapfseulenOverview.Click += new System.Windows.RoutedEventHandler(this.ZapfseulenOverview_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.LastColumn = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

