   M S B A M L   `   `   `  ����E  ABeautyCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BeautyCare
BeautyCare  5          BeautyCare.servicePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:BeautyCare     5      V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  servicePage��5	      $	��680��6   $	��425��6)    N� 
Background
 �����.��5      � 
�������.+�5   
   �� -   5         ��Name$ ButtonVoltar��5      $��60��6'   $��60��62   $	��Top=�6k   $
��Left=�6�   ��6>   Z� ��6T   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  ���#000000   Kind$ 	ArrowLeft=�6<   $��25��6M   $��25��6X   5      �� 5   
   -   5        $ ButtonAddFav��5      $��60��6'   $��60��62   $	��Top=�6k   $��Right=�6�   $��	0,10,10,0q�6�   ��6>   Z� ��6T   Z� .��5        -   5          Name$ 	favoritos��5   &   ���#000000$ 
StarBorder=�6N   $��40��6`   $��50��6k   $��Center=�6w   5      � 5   
   -   5         �Name$ userfotogrid��5      $��2,75,217,384q�6%   ��5      �� 5      -   5        userfoto�� �� ImageSource$ Images/servicedefault.png��5   /   6Y   5      �� 5   
   -   5         ��Name$ nomeservico��5   I   $	��Top=�6   $
��Bold"�6(   $��20(�6;   $��222,89,10,0q�6_   $��54��6t   5      �� 5   
   $��	Instituto��6   $
��Bold"�6$   $��24(�66   
���   �$��50��6W   $��222,191,84,439q�6c   �� 5   
   -   5         ��Name$ nomeinstituto��5   N   	 �� 
IsReadOnly$
	 True��6   $��20(�6(   $	��Top=�66   $��222,241,53,0q�6f   $��41��6|   
 � 	Resources
 5        �� 
TargetType �� Property �� Value��%X   'J�       �� 5!      " J�6   .�5"      ��  �@��Margin$ 0q�6/   5#      5$      5%      �� 5&   
   -   5        $ descricaoServico��5&   @   $
	 True��6   $	��Top=�6(   $��71��6Z   $��21,318,40,0q�6f   
 5'      ��%X   'J�       �� 5(      " J�6   .�5)      ��  �@��Margin$ 0q�6/   5*      5+      5,      �� 5-   
   -	   5        $ 
precoLabel��5-      $��Preço��6$   $
��Bold"�64   $��310,424,46,207q�6F   $��22(�6^    J� RenderTransformOrigin$ 0.294,0.384&�6l   �� 5.   
   -
   5        $
 Icon��5.      $	��€��6   $��40��6*   $
��Bold"�66   $��22(�6H   $ 0.294,0.384&�6V   $��385,457,10,183q�6z   �� 5/   
   -   5        $ preco��5/   @   $
	 True��6   $	��Top=�6(   $��32��6O   $��310,461,40,0q�6[   
 50      ��%X   'J�       �� 51      " J�6   .�52      ��  �@��Margin$ 0q�6/   53      54      55      �� 56   
   -   5         ��Name$
 data��56      $��18(�6!   $
��Left=�6/   $��
58,461,0,0q�6J   $	��Top=�6^   $	��170��6v   �� 57   
   -   5        $
 hora��57      $��18(�6!   $��Center=�6/   $	��120��6L   $��83,518,222,0q�6X   $	��Top=�6n   �� 58   
   -   5        $��Agendar��58      $��136,603,137,45q�6#   6;     59   
   -   5        $ 
ArrowRight=�59   "   $��25��64   $��25��6?   $��Right=�6K   $��
0,249,23,0q�6g   $ 0.684,1.668&�6{   6�   5;      5<      