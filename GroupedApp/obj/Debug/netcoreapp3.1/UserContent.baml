   M S B A M L   `   `   `  ����E  AGroupedApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GroupedApp
GroupedApp  5   	       GroupedApp.UserContent   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:GroupedApp     5   	   $	��310��5      $	��390��6'     � 
ResizeMode$  NoResize=�63    � WindowStartupLocation$ CenterScreen=�6I   .��5      �� 
���   �$��2q�6!   ._�5	   
   � -   5        $��	Popupgrid��5	      $
��Auto��6    $��Stretch=�6.   $��Center=�6J   
������� � RowDefinitions 5
      �� 5      $��35 �6    �� 5      $��30 �6    �� 5      $
��Auto �6    �� 5      $��25 �6    �� 5      $��25 �6    �� 5      $��30 �6     � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $��* �6#   �� 5      $	��100 �6#   .+�5      �� $��0��6   $��0��6!   $��Discipline: ��61   
�������6H   �� 5      $��1��6   $��0��6!   $��Discipline Color: ��61   
�������6N   �� 5      $��2��6   $��0��6!   $��	Content: ��61   
�������6E   �� 5      $��3��6   $��0��6!   $��Start Date: ��61   
�������6H   �� 5      $��4��6   $��0��6!   $��
End Date: ��61   
�������6F   �� 5      -   5         ��Name$ discBoxRectangle��5      $��1��62   $��2��6B   $��0��6V   
���  ��$��2��6t   �� 5      -   5        $��discBox��5      $��0��6&   $��1��63   $��2��6C    �� 
IsEditable$
 True��6W   $��2q�6�   6�       GroupedApp.TextBoxRequired  5      -   5          Name$ ContentBoxEntry��5   n   $��2��6$   $��1��61   $��2��6A   $	��280��6U   $	��125��6a   6n   (  "System.Windows.Controls.DatePicker  5      -   5        $��StartDateCntntBox��5      $��3��63   $��1��6@   $��2��6P      SelectedDateFormat$
 Long=�6d   6~   �� 5       -   5        $ StartDateRectangle��5       $��1��64   $��2��6D   $��3��6X   
���  ��$��2��6v     5!      -   5        $��EndDateCntntBox��5!      $��4��61   $��1��6>   $��2��6N   $
 Long=�6b   �� 5"      -   5        $��PopupCancelBtn��5"      $��5��6+   $��Cancel��68   $��Center=�6I   $	��100��5#      
����  �
�������6J   �� 5$      -	   5        $��PopupConfirmBtn��5$      $��5��6,   $��2��69   $��Confirm��6I   
����  �$��Center=�5%      $	��100��62   
�������6>   �� 5&      -
   5        $��MilestoneCheckbox��5&      $��5��60   $��1��6=   $��Bottom=�6M   $��Center=�6h   $��	Milestone��6�   6�   �� 5'      -   5        $ discColorRectangle��5'      $��1��64   $��2��6D   $��1��6X   
���  ��$��2��6v   �� 5(      -   5        $��	discColor��5(      $��1��6(   $��1��65   $��2��6E   $��2q�6Y   ��5)      �� 5*      .R�5+      ��  �� Orientation$ 
Horizontal=�6%   .+�5,      �� $��20��6?   $��20��6J   $��0,2,5,2q�6V   ��6(   �� *Value+6V   �� 5-      ��6(   �� *Key+5.      5/      51      52      53      54      