   M S B A M L   `   `   `  ����  GestionAprendisaje'    !GestionAprendisaje.UsuariosWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Usuarios��5      $	��700��6   $
��1300��6%   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ UsuariosGrid��5      $��0��6)      AutoGenerateColumns .  $��	8,85,8,24q�6R      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+  5      $ Nombre��6%    65   �� *Nombre+  5      $ Email��6%    64   �� *Email+  5      $ Fecha de Registro��6%    6@   �� *FechaRegistro+  5      $
 Foto��6%    63   �� *Foto+5      � 5   
    � Orientation$ 
Horizontal=�6   $
��Left=�6-   $��
10,0,0,555q�6H   $
��1011��6\   .+�5      �� $��Nombre:��6   $��Center=�6&   �� 5      -   5         ��Name$ 	TxtNombre��5      $	��200��6)   $��Center=�65   $��5q�6P   �� 5      $��Email:��6   $��Center=�6%   �� 5      -   5        $ TxtEmail��5      $	��200��6(   $��Center=�64   $��5q�6O   �� 5      $��Fecha de Registro:��6   $��Center=�61   (  "System.Windows.Controls.DatePicker  5      -   5        	  Name$	 FechaRegistroPicker��5      $	��150��66   $��Center=�6B   $��5q�6]   �� 5      $��Foto:��6   $��Center=�6$   �� 5       -   5        $ TxtFoto��5       $	��200��6'   $��Center=�63   $��5q�6N   �� 5"      -   5        
 ��Name$
 
BtnAgregar��5"      $��Agregar Usuario��6)   $	��150��6\   $��5q�6h   �� 5#      -   5        $
 BtnEliminar��5#      $��Eliminar Usuario��6*   $	��150��6_   $��5q�6k   �� 5$      -   5        $
 BtnActualizar��5$      $��Actualizar Usuario��6,   $	��150��6e   $��5q�6q   5%      5&      5'      