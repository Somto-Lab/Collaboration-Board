   M S B A M L   `   `   `  ���� E  AGroupedApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GroupedApp
GroupedApp      GroupedApp.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:GroupedApp       � 	Resources     OverlapStyle �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value  	UndoStyle  ExpanderControlTemplate1 �� 
TargetType �� CornerRadius	 J� SnapsToDevicePixels
 d�Name d� 	IsChecked �� Mode �� RelativeSource � FocusVisualStyle �� StrokeDashArray �� Padding � ColumnDefinitions A�Name F�Name �� ContentStringFormat �� ContentStringFormat �� Triggers �� 
TargetName ��Name � RowDefinitions � LayoutTransform �� Angle��%z  &	        &	 �     &	 2    �� " � P�  �@J�IsMouseOver$ False��. ���  �@J�Opacity$ 0Q�P�  �@J�IsMouseOver$
 True��. ���  �@J�Opacity$	 0.5Q��� " � P�  �@J�IsMouseOver$ False��. ���  �@J�Opacity$	 0.5Q�P�  �@J�IsMouseOver$
 True��. ���  �@J�Opacity$ 1Q��� " 9�.R��� #��z��#��z��#��z��$ 3��$
	 True��._�]� .+�d� $
 
HeaderSite��#��z��#��z��$	��Top=�#��z��#��z��#��z��#��z��#��z��#��z��#��z��$��1q�$��0��$��0��#��z��#��z�� �� *
IsExpanded+$ TwoWay=� �� *TemplatedParent+ �� .���  � �� �� .R��� ._��� $��0q�$
	 True��
���   �$��1��$	 1 2R����� " d�.���  � �� �� " d�.R��� # z��._�� 
������ $	 False�� �� $��19 ��� $��* �.+�A� $ circle��
�������$��Center=�$��19�����	#FF333333$��Center=�$��19��F� $ arrow��"����@B `� � �D @KL � z `� 	$��Center=�$	 False�����	#FF333333$��2��$��Center=��� #��z��#��z��$��1��# z $
��Left=�$��4,0,0,0q�$
��True��$
	 True��$��Center=� P�  �@d�	IsChecked$
 TrueY�. ��� $ arrow�� � ��" ��@B  �D � �D @B � z  �D 	P�  �@J�IsMouseOver$
 True��. ��� $ circle�� � �� �	#FF5593FF�� $ circle�� � �� �	#FFF3F9FF�� $ arrow�� � ��
 �   �P�  � ��$
 True��. ��� $ circle�� � �� �	#FF3C77DD�� $ circle�� � ��$	 1.5���� $ circle�� � �� �	#FFD9ECFF�� $ arrow�� � ��
 �   �P�  � {�$ False��. ��� $ circle�� � �� �	#FFBCBCBC�� $ circle�� � �� �	#FFE6E6E6�� $ arrow�� � �� �	#FF707070�� $ 
ExpandSite��#��z��#��z��# z $��Bottom=�$|�False��#��z��#��z��$y�	Collapsed=�#��z�� P�  �@9�
IsExpanded$
 True��. ��� $ 
ExpandSite�� � y�$ Visible=�P�  �@9�ExpandDirection$
 Left=�. ��� $ 
ExpandSite�� � ��$
 Left=��� $ 
HeaderSite�� � ��$ Right=��� $ 
HeaderSite�� � �� �� " d�.���  � �� �� " d�.R��� # z��._�� 
������ $	 False�� �� $��* ��� $��* ��� $��* �.+��  V� .~��� $ 90Q�.+�A� $ circle��
�������$��Center=�$��19�����	#FF333333$��Center=�$��19��F� $ arrow��"����@B `� � �D @KL � z `� 	$��Center=�$	 False�����	#FF333333$��2��$��Center=��� #��z��#��z��# z $��Center=�$��0,4,0,0q�$��1��$
��True��$
	 True��$	��Top=� P�  �@d�	IsChecked$
 TrueY�. ��� $ arrow�� � ��" ��@B  �D � �D @B � z  �D 	P�  �@J�IsMouseOver$
 True��. ��� $ circle�� � �� �	#FF5593FF�� $ circle�� � �� �	#FFF3F9FF�� $ arrow�� � ��
 �   �P�  � ��$
 True��. ��� $ circle�� � �� �	#FF3C77DD�� $ circle�� � ��$	 1.5���� $ circle�� � �� �	#FFD9ECFF�� $ arrow�� � ��
 �   �P�  � {�$ False��. ��� $ circle�� � �� �	#FFBCBCBC�� $ circle�� � �� �	#FFE6E6E6�� $ arrow�� � �� �	#FF707070P�  � {�$ False��. ��� # � �� � ��.��� .+��� $
��Auto=�$
��Auto=�$��
0,0,0,-0.5q�.���� -    ��Name$ 
GridsStack�� ��  �� ScaleX ��  �� ElementName$ Slide�� �� Path$ Value�  �� ScaleY  �� $ Slide��$ Value�.+�� -   $��DateGrid��$	��Top=�$��70 0 0 0q� �� $��60 � �� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 ��� $��85 �� -   $��	Groupgrid��$��50��$��Center=�$��Bottom=�$��0 0 0 20q�  �� $��30 �� -   $��Addgrid��$��50��$
��Left=�$	��Top=�$��	10 10 0 0q� ��  �� $��50 �.+��� -   $��	AddConBtn��
�������
�������$��2q�$��+��! � ToolTip$! Add Content��� -   $��TBCgrid��$��Center=�$��Right=�$��0 0 20 0q� �� $��50 � � -   $��StateUpdateGrid��$��Bottom=�$
��Left=�$��	10 0 0 60q� �� $��64 ��� $��35 � �� $	��100 ��� $��25 ��� $��25 ��� $��25 �.+��� 
�������$��1��$��3��$��2���� $��Activities:��$��1��$��0��$
��Bold"�$��10(�$��Bottom=�$��Center=��� $��
Completed:��$��2��$��0��$
��Bold"�$��10(�$��Bottom=�$��Center=��� $��Missed:��$��3��$��0��$
��Bold"�$��10(�$��Bottom=�$��Center=��� -   $��
TotalBlock��$��1��$��1��$��0��$
��Left=�$��Center=��� -	   $��CompleteBlock��$��2��$��1��$��0%��$
��Left=�$��Center=��� -
   $��MissedBlock��$��3��$��1��$��0%��$
��Left=�$��Center=��� -   $��45��$��45��$	��PDF��$
��Bold"�$��10(�
�������
�������$��2q�$! Open PDF��9� " 9� ExpandDirection$
" Left=�$
��Left=�$��Center=�
������ $
��Bold"�$��10(�#���  $! 
Milestones������ $��	Milestone�� �� $ 90Q�.��� -   $��MilestoneGrid�� �� $��5 ��� $	��100 ��� $��5 ��� $	��100 ��� $��5 � �� $	��100 �� $��Right=�$��Bottom=�$��	0 0 10 60q� �� $��50 � �� $��50 �.+��� -   # ��Name$# Slide��$ �� Orientation$$ Vertical=�% � Minimum$% 1Q�& � Maximum$& 8Q�' � LargeChange$	' 0.2Q�( � SmallChange$
( 0.01Q�) � Value$) 1Q�