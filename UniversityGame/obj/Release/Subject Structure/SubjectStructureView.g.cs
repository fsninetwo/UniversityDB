﻿#pragma checksum "..\..\..\Subject Structure\SubjectStructureView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "105296A737EDC2D1520EE6E49A027C7EDC961227"
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
using UniversityGame.Subject_Structure;


namespace UniversityGame.Subject_Structure {
    
    
    /// <summary>
    /// SubjectStructureView
    /// </summary>
    public partial class SubjectStructureView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView perfomanceTable;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox countField;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox classformChoice;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox subjectChoice;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label2;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addButton;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteButton;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button updateButton;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Subject Structure\SubjectStructureView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button updateField;
        
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
            System.Uri resourceLocater = new System.Uri("/UniversityGame;component/subject%20structure/subjectstructureview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Subject Structure\SubjectStructureView.xaml"
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
            this.perfomanceTable = ((System.Windows.Controls.ListView)(target));
            
            #line 10 "..\..\..\Subject Structure\SubjectStructureView.xaml"
            this.perfomanceTable.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.subjectTable_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\Subject Structure\SubjectStructureView.xaml"
            this.perfomanceTable.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.subjectTable_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.countField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.classformChoice = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.subjectChoice = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.label2 = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.addButton = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Subject Structure\SubjectStructureView.xaml"
            this.addButton.Click += new System.Windows.RoutedEventHandler(this.addButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.deleteButton = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Subject Structure\SubjectStructureView.xaml"
            this.deleteButton.Click += new System.Windows.RoutedEventHandler(this.deleteButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.updateButton = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\Subject Structure\SubjectStructureView.xaml"
            this.updateButton.Click += new System.Windows.RoutedEventHandler(this.updateButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.updateField = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Subject Structure\SubjectStructureView.xaml"
            this.updateField.Click += new System.Windows.RoutedEventHandler(this.updateField_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
