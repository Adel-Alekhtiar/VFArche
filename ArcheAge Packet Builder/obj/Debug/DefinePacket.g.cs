﻿#pragma checksum "..\..\DefinePacket.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5FC92B158D688E1E41B2CCA698DFE2BE"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18046
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
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


namespace ArcheAge_Packet_Builder {
    
    
    /// <summary>
    /// DefinePacket
    /// </summary>
    public partial class DefinePacket : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Segments;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TypeSegment;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ArraySegment;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameSegment;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LengthSegment;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddSegment;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BreakIteration;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox IntoArray;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\DefinePacket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PacketName;
        
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
            System.Uri resourceLocater = new System.Uri("/ArcheAge Packet Builder;component/definepacket.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DefinePacket.xaml"
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
            this.Segments = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            
            #line 24 "..\..\DefinePacket.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TypeSegment = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.ArraySegment = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.NameSegment = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.LengthSegment = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.AddSegment = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\DefinePacket.xaml"
            this.AddSegment.Click += new System.Windows.RoutedEventHandler(this.AddSegment_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BreakIteration = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.IntoArray = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 10:
            this.PacketName = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

