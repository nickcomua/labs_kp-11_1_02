﻿#pragma checksum "..\..\..\StudyMode.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2FC277328C63599929E98DF7C727311CC6396418"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using prac_1_2;


namespace prac_1_2 {
    
    
    /// <summary>
    /// StudyMode
    /// </summary>
    public partial class StudyMode : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\StudyMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock VerifField;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\StudyMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputField;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\StudyMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseStudyMode;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\StudyMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CountProtection;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\StudyMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SymbolCount;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/prac 1-2;component/studymode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StudyMode.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.VerifField = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.InputField = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\StudyMode.xaml"
            this.InputField.PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.TextBox_PreviewKeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CloseStudyMode = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\StudyMode.xaml"
            this.CloseStudyMode.Click += new System.Windows.RoutedEventHandler(this.CloseStudyMode_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CountProtection = ((System.Windows.Controls.ComboBox)(target));
            
            #line 48 "..\..\..\StudyMode.xaml"
            this.CountProtection.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CountProtection_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SymbolCount = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
