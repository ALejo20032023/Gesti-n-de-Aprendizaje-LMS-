﻿#pragma checksum "..\..\UsuariosWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3BED4FE0DE8856B7D44E917D105AC4DB0F3AED41F9367C88B113B3BC6D686F0D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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


namespace GestionAprendisaje {
    
    
    /// <summary>
    /// UsuariosWindow
    /// </summary>
    public partial class UsuariosWindow : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\UsuariosWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid UsuariosGrid;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\UsuariosWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtNombre;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\UsuariosWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtEmail;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\UsuariosWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker FechaRegistroPicker;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\UsuariosWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtFoto;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\UsuariosWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAgregar;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\UsuariosWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnEliminar;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\UsuariosWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnActualizar;
        
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
            System.Uri resourceLocater = new System.Uri("/GestionAprendisaje;component/usuarioswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UsuariosWindow.xaml"
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
            this.UsuariosGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 11 "..\..\UsuariosWindow.xaml"
            this.UsuariosGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.UsuariosGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TxtNombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TxtEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.FechaRegistroPicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.TxtFoto = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.BtnAgregar = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\UsuariosWindow.xaml"
            this.BtnAgregar.Click += new System.Windows.RoutedEventHandler(this.BtnAgregar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnEliminar = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\UsuariosWindow.xaml"
            this.BtnEliminar.Click += new System.Windows.RoutedEventHandler(this.BtnEliminar_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BtnActualizar = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\UsuariosWindow.xaml"
            this.BtnActualizar.Click += new System.Windows.RoutedEventHandler(this.BtnActualizar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

