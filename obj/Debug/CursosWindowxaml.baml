   M S B A M L   `   `   `  ����  GestionAprendisaje%    GestionAprendisaje.CursosWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Cursos��5      $	��700��6   $
��1300��6#   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 
CursosGrid��5      $��0��6'      AutoGenerateColumns .  $��	8,85,8,24q�6P      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+  5      $ Título��6%    65   �� *Titulo+  5      $ Descripción��6%    6:   �� *Descripcion+  5      $ Fecha de Inicio��6%    6>   �� *FechaInicio+  5      $ Fecha de Fin��6%    6;   �� *
FechaFin+5      � 5   
    � Orientation$ 
Horizontal=�6   $
��Left=�6-   $��
10,0,0,555q�6H   $
��1011��6\   .+�5      �� $��Título:��6   $��Center=�6&   �� 5      -   5         ��Name$ 	TxtTitulo��5      $	��200��6)   $��Center=�65   $��5q�6P   �� 5      $��Descripción:��6   $��Center=�6+   �� 5      -   5        $ TxtDescripcion��5      $	��200��6.   $��Center=�6:   $��5q�6U   �� 5      $��Fecha de Inicio:��6   $��Center=�6/   (  "System.Windows.Controls.DatePicker  5      -   5        	  Name$	 FechaInicioPicker��5      $	��150��64   $��Center=�6@   $��5q�6[   �� 5      $��Fecha de Fin:��6   $��Center=�6,     5       -   5        $	 FechaFinPicker��5       $	��150��61   $��Center=�6=   $��5q�6X   �� 5"      -   5        
 ��Name$
 
BtnAgregar��5"      $��Agregar Curso��6)   $	��150��6Z   $��5q�6f   �� 5#      -   5        $
 BtnEliminar��5#      $��Eliminar Curso��6*   $	��150��6]   $��5q�6i   �� 5$      -   5        $
 BtnActualizar��5$      $��Actualizar Curso��6,   $	��150��6c   $��5q�6o   5%      5&      5'      