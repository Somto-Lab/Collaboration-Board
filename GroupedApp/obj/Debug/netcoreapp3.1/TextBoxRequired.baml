   M S B A M L   `   `   `  ����E  AGroupedApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GroupedApp
GroupedApp  5           GroupedApp.TextBoxRequired   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:GroupedApp     5      (cGroupedApp.TextBoxValidate     5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��ControlledTextBox��5   O   $��1��6    �� 	IsEnabled$
 True��6   $��2��6;    �� TextWrapping$ WrapWithOverflow=�6�    �� 	MaxLength 6h   ��  �� Path$ TextBoxMaximumLength�.��6�   ��5       �� 5!      $ TextBoxEntry�6    �� Mode$ TwoWay=�5"       �� UpdateSourceTrigger$ PropertyChanged=�5#       �� ValidationRules 5$          GroupedApp.TextBoxValidate  5%      ,  &System.Windows.Controls.ValidationRule	   ValidatesOnTargetUpdated$
	 True��60   5'      .��5(      5)      �� 5*   
   ��6   �� *%#(Validation.Errors)[0].ErrorContent+
 �� ElementName$
 ControlledTextBox��5+      5,      