   M S B A M L   `   `   `  ����E  ABeautyCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BeautyCare
BeautyCare  5          BeautyCare.meusInstitutos   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:BeautyCare     5      V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  meusInstitutos��5      $	��680��6   $	��425��6,    � 	Resources 5         MenuOpen �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  	MenuClose��%�  &	        &	 �     �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $ 60Q�63     5      $ 0:0:0.5��6'   $	 200Q�69   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $	 200Q�63     5      $ 0:0:0.5��6'   $ 60Q�69   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ ButtonBase.Click��6    :� 
SourceName$ ButtonOpenMenu��66   .[�5      �� #��[  6   5      :� 5   
   $ ButtonBase.Click��6   $ ButtonCloseMenu��66   .[�5      �� #��[ 6   5       :� 5!   
   $ MouseLeftButtonDown��6   $ fecharmenu2��6:   .[�5"      �� #��[ 6   5#      .��5'      � .+�5(   
   � -   5        	 �Name$	 homepage��5(   '   
�������.+�5)      � -   5        $	 grid1��5)      
 � ColumnDefinitions
 5*      �� 5+      $��50 �6'   �� 5,      $	��330 �6'   �� 5-      $��50 �6'    � RowDefinitions 5/      �� 50      $��70 �6$   �� 51      $��48 �6$   �� 52      $	��512 �6$   �� 53      $��50 �6$   .+�56      �� $
��Auto=�6   $��2��6B   $
��Auto��6O   $��2��6]   $��0,2,10,-440q�6q   $��1��6�   $��2��6�   .��57      �� -   5         ��Name$ painel��57   !    �� Orientation$ Vertical=�61   $��0,0,0,0q�6I   $	��370��6Z   $
��1000��6f   5O      5P      �� 5Q      $��1��6   $
��Left=�6%   $��Center=�6@   $��1��6[   $��Meus Institutos��6k   $��24(�6�   $
��Bold"�6�   $��88,0,0,0q�6�   $��Georgia Pro Cond Black)�6�   �� 5R      
���   � J� RenderTransformOrigin$ 0.5,0.5&�6+   $
��Left=�6K   $��1��6f   $��184,-46,0,350q�6v   $��3��6�   $��1��6�   z�5S      V� 5T      .~�5U      ��  �� ScaleY$ 1Q�6-   �� 5V      �� 5W       �� Angle$	 -90Q�6.   T� 5X      5Y      5[      �� 5\      -   5         ��Name$ addInstituto��5\      $��2��6/   $��1��6<   $��75,480,68,0q�6L   .��6|   Adicionar Instituto5^      5_      �� 5`      $��Images/logo2.png.png��6   $��0��62   $
��Left=�6?   $	��261��6Z   $��146,-17,0,591q�6f   5c      � 5e   
   -   5        $	 fecharmenu2��5e      $y�	Collapsed=�6$   
������ 6�   � 5f   
   -   5        $	 GridMenu��5f   &   ���	#FFA7BACB$��55��68   $
��Left=�6C   $��2��6^   .+�5g      �� .+�5h      � $	��150��6   .+�5i      �� -   5        $ ButtonCloseMenu��5i      $��60��66   $��60��6A   $	��Top=�6z   $��Right=�6�   $y�	Collapsed=�6�   ��6M   Z� ��6c   Z� .��5j      '  !MaterialDesignThemes.Wpf.PackIcon  ���#000000   Kind$ 	ArrowLeft=�6G   $��25��6X   $��25��6c   5k      �� 5l      -   5        $ ButtonOpenMenu��5l      $��60��65   $��60��6@   $	��Top=�6y   $��Right=�6�   ��6L   Z� ��6b   Z� .��5m        ���	#FF0E2B5D$
 Menu=�6I   $��25��6U   $��25��6`   5n      5o      �� 5p      -	   5         ��Name$ ListMenu��5p      $y�	Collapsed=�6-   ���#000000$��Disabled=�6Y   .@�5q      �� -
   5        $��60��5q   #   .��5r      �� $ 
Horizontal=�6%   .+�5s        $ Account=�66   $��25��6E   $��25��6P   $��Center=�6\   �� 5t      $��Informação Pessoal��6(   $��Center=�6B   $��20 10q�6]   5u      5v      �� 5w      -   5        $��60��5w   #   .��5x      �� $ 
Horizontal=�6%   .+�5y        $ Calendar=�66   $��25��6F   $��25��6Q   $��Center=�6]   �� 5z      $��Marcações��6(   $��Center=�69   $��20 10q�6T   5{      5|      �� 5}      -   5        $��60��5}   #   .��5~      �� $ 
Horizontal=�6%   .+�5        $ SwitchAccount=�66   $��25��6K   $��25��6V   $��Center=�6b   �� 5�      $��Mudar de Conta��6(   $��Center=�6>   $��20 10q�6Y   5�      5�      �� 5�      -   5        $��60��5�   #   $��Bottom=�6/   .��5�      �� $ 
Horizontal=�6%   .+�5�        $ 	ExitToApp=�66   $��25��6G   $��25��6R   $��Center=�6^   �� 5�      $
��Sair��6(   $��Center=�64   $��20 10q�6O   5�      5�      5�      5�      5�      5�      5�      