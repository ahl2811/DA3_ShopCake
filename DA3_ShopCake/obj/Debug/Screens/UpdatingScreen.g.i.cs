﻿#pragma checksum "..\..\..\Screens\UpdatingScreen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "001F1BC6B5720334E16633EEBB6C052A66BD3C4B641D07C5AD8A25AD7B015D2C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DA3_ShopCake.Screens;
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


namespace DA3_ShopCake.Screens {
    
    
    /// <summary>
    /// UpdatingScreen
    /// </summary>
    public partial class UpdatingScreen : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 116 "..\..\..\Screens\UpdatingScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock keyWordCakeName;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Screens\UpdatingScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCakeName;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\Screens\UpdatingScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock keyWordPrice;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\Screens\UpdatingScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPrice;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\Screens\UpdatingScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock keyWordDescription;
        
        #line default
        #line hidden
        
        
        #line 184 "..\..\..\Screens\UpdatingScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDescription;
        
        #line default
        #line hidden
        
        
        #line 209 "..\..\..\Screens\UpdatingScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFetch;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\..\Screens\UpdatingScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbImages;
        
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
            System.Uri resourceLocater = new System.Uri("/DA3_ShopCake;component/screens/updatingscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Screens\UpdatingScreen.xaml"
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
            
            #line 8 "..\..\..\Screens\UpdatingScreen.xaml"
            ((DA3_ShopCake.Screens.UpdatingScreen)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 34 "..\..\..\Screens\UpdatingScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.exitUpdateButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 64 "..\..\..\Screens\UpdatingScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.submitButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.keyWordCakeName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txtCakeName = ((System.Windows.Controls.TextBox)(target));
            
            #line 121 "..\..\..\Screens\UpdatingScreen.xaml"
            this.txtCakeName.GotFocus += new System.Windows.RoutedEventHandler(this.TextBoxCakeName_GotFocus);
            
            #line default
            #line hidden
            
            #line 122 "..\..\..\Screens\UpdatingScreen.xaml"
            this.txtCakeName.LostFocus += new System.Windows.RoutedEventHandler(this.TextBoxCakeName_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.keyWordPrice = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.txtPrice = ((System.Windows.Controls.TextBox)(target));
            
            #line 152 "..\..\..\Screens\UpdatingScreen.xaml"
            this.txtPrice.GotFocus += new System.Windows.RoutedEventHandler(this.TextBoxPrice_GotFocus);
            
            #line default
            #line hidden
            
            #line 153 "..\..\..\Screens\UpdatingScreen.xaml"
            this.txtPrice.LostFocus += new System.Windows.RoutedEventHandler(this.TextBoxPrice_LostFocus);
            
            #line default
            #line hidden
            
            #line 161 "..\..\..\Screens\UpdatingScreen.xaml"
            this.txtPrice.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 8:
            this.keyWordDescription = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.txtDescription = ((System.Windows.Controls.TextBox)(target));
            
            #line 185 "..\..\..\Screens\UpdatingScreen.xaml"
            this.txtDescription.GotFocus += new System.Windows.RoutedEventHandler(this.TextBoxDescription_GotFocus);
            
            #line default
            #line hidden
            
            #line 186 "..\..\..\Screens\UpdatingScreen.xaml"
            this.txtDescription.LostFocus += new System.Windows.RoutedEventHandler(this.TextBoxDescription_LostFocus);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnFetch = ((System.Windows.Controls.Button)(target));
            
            #line 209 "..\..\..\Screens\UpdatingScreen.xaml"
            this.btnFetch.Click += new System.Windows.RoutedEventHandler(this.onAddNewImage);
            
            #line default
            #line hidden
            return;
            case 11:
            this.lbImages = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

