﻿#pragma checksum "..\..\..\..\..\MVVM\View\ThirdView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "064471EAA323F71C855AB27340B4A0D4EF59084B"
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


namespace View.MVVM.View {
    
    
    /// <summary>
    /// ThirdView
    /// </summary>
    public partial class ThirdView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BackUpNameBox;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorNameLabel;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SoucePathBox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button source_Folder_Button;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorSourceLabel;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TargetPathBox;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button target_Folder_Button;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorTargetLabel;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SuccesLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/EasySaveApp;component/mvvm/view/thirdview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
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
            this.BackUpNameBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 45 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
            this.BackUpNameBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.BackUpNameBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ErrorNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.SoucePathBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 73 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
            this.SoucePathBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SoucePathBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.source_Folder_Button = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
            this.source_Folder_Button.Click += new System.Windows.RoutedEventHandler(this.source_Folder_Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ErrorSourceLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.TargetPathBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 113 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
            this.TargetPathBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TargetPathBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.target_Folder_Button = ((System.Windows.Controls.Button)(target));
            
            #line 120 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
            this.target_Folder_Button.Click += new System.Windows.RoutedEventHandler(this.target_Folder_Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ErrorTargetLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.SuccesLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            
            #line 153 "..\..\..\..\..\MVVM\View\ThirdView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DifferentialButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

