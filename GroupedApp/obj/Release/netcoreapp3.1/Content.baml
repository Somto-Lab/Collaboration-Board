   M S B A M L   `   `   `  ���� E  AGroupedApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GroupedApp
GroupedApp      GroupedApp.Content   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:GroupedApp     $	��310��$	��390��  � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=�.���� 
���   �$��2q�._�� -   $��	Popupgrid��$
��Auto��$��Stretch=�$��Center=�
������� � RowDefinitions �� $��35 ��� $��30 ��� $
��Auto ��� $��25 ��� $��25 ��� $��30 � � ColumnDefinitions �� $	��100 ��� $��* ��� $	��100 �.+��� $��0��$��0��$��Discipline: ��
��������� $��1��$��0��$��Discipline Color: ��
��������� $��2��$��0��$��	Content: ��
��������� $��3��$��0��$��Start Date: ��
��������� $��4��$��0��$��
End Date: ��
��������� -    ��Name$ discBoxRectangle��$��1��$��2��$��0��
���  ��$��2���� -   $��discBox��$��0��$��1��$��2�� �� 
IsEditable$
 True��$��2q�    GroupedApp.TextBoxRequired  -     Name$ ContentBoxEntry��$��2��$��1��$��2��$	��280��$	��125��(  "System.Windows.Controls.DatePicker  -   $��StartDateCntntBox��$��3��$��1��$��2��   SelectedDateFormat$
 Long=�  -   $��EndDateCntntBox��$��4��$��1��$��2��$
 Long=��� -   $��PopupCancelBtn��$��5��$��Cancel��$��Center=�$	��100��
����  �
��������� -   $��PopupConfirmBtn��$��5��$��2��$��Confirm��
����  �$��Center=�$	��100��
��������� -	   $��MilestoneCheckbox��$��5��$��1��$��Bottom=�$��Center=�$��	Milestone���� -
   $ discColorRectangle��$��1��$��2��$��1��
���  ��$��2���� -   $��	discColor��$��1��$��1��$��2��$��2q����� .R���  �� Orientation$ 
Horizontal=�.+��� $��20��$��20��$��0,2,5,2q����� *Value+�� ���� *Key+