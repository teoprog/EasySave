﻿#pragma checksum "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F8A89168F0A9183CB80C62D2DB76D66D5E09AA82"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using EasySaveApp.Properties.Langs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using View.MVVM.View;
using View.MVVM.ViewModel;


namespace View.MVVM.View {
    
    
    /// <summary>
    /// BlackListSoftware
    /// </summary>
    public partial class BlackListSoftware : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Settingstitle;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SoftwareBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorTargetLabel;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SuccesLabel;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid JobsGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EasySaveApp;component/mvvm/view/blacklistsoftwareview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Settingstitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.SoftwareBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 58 "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml"
            this.SoftwareBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SoftwareBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 60 "..\..\..\..\..\MVVM\View\BlackListSoftwareView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ErrorTargetLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.SuccesLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.JobsGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
