   M S B A M L   `   `   `  ����E  ABeautyCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BeautyCare
BeautyCare  5          BeautyCare.institutoPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:BeautyCare     5      V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  institutoPage��5	      $	��680��6   $	��425��6*    N� 
Background
 �����.��5      � 
�������.+�5   
   �� -   5         ��Name$ ButtonVoltar��5      $��60��6'   $��60��62   $	��Top=�6k   $
��Left=�6�   ��6>   Z� ��6T   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  ���#000000   Kind$ 	ArrowLeft=�6<   $��25��6M   $��25��6X   5      � 5   
   -   5         �Name$ ButtonAddFav��5      $	��129��6%   $��60��61   $	��Top=�6=   $��Right=�6U   $��	0,10,10,0q�6q   .+�5      �� -   5         ��Name$ rating��5      $��24(�6$   $��
10,5,10,15q�62   .��6F   ?/56K     5      
��� ���$
 Star=�68   $��28��6D   $��49��6P   $��	65,13,0,0q�6[   �� 5      -   5        $ nomeInstituto��5      $��24(�6+   $
��Bold"�69   $��-266,51,10,-31q�6K   $��Nome Instituto��6c   5      � 5   
   -   5        $ userfotogrid��5      $��224,121,10,340q�6%    � ColumnDefinitions 5      �� 5      $��3* �6#   �� 5      $	��14* �6#   ��5      �� 5      -   5        userfoto�� �� ImageSource$! Images/defaultInstituto.jpg��5   /   6[   5      �� 5   
   -   5         ��Name$ descricaoServico��5   @   	 �� 
IsReadOnly$
	 True��6   $	��Top=�6(   $	��219��6Z   $��10,121,213,0q�6g   
 � 	Resources
 5       �� 
TargetType �� Property �� Value��%X   'J�       �� 5       " J�6   .�5!      ��  �@��Margin$ 0q�6/   5"      5#      5$      � 5'   
    5(      �� 5)      $��15 �6#   �� 5*      $	��400 �6#   �� 5+      $��10 �6#    � RowDefinitions 5-      �� 5.      $	��370 �6    �� 5/      $��50 �6    �� 50      $	��212 �6    �� 51      $��20 �6    .+�53      �� 
���   � J� RenderTransformOrigin$ 0.5,0.5&�6'   $
��Left=�6G   $��1��6b   $��197,228,0,32q�6r   $��3��6�   $��1��6�   z�54      V� 55      .~�56      �� �� 57      �� 58       �� Angle$	 -90Q�6*   T� 59      5:      5<      �� 5=      $��1��6   $��1��6!   $��20(�61   $
��Bold"�6?   $��Serviço��6Q   $��35��6c   $	��100��6o   $��
50,8,250,7q�6{   6�   �� 5>      $��1��6   $��1��6!   $��20(�61   $
��Bold"�6?   $��Preço��6Q   $��35��6b   $	��100��6n   $��
236,8,64,7q�6z   �� 5?      $
��Auto=�6   $��2��6>   $
��Auto��6K   $��3��6Y   .��5@      �� -   5         ��Name$ painel��5@       �� Orientation$ Vertical=�6-   $��0,0,0,0q�6E   5B      5C      5D      5E      5F      