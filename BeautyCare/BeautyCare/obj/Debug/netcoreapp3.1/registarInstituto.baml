   M S B A M L   `   `   `  ����E  ABeautyCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BeautyCare
BeautyCare  5      "    BeautyCare.registarInstituto   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:BeautyCare     5      V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  registarInstituto��5      $	��680��6!   $	��425��6.    N� 
Background
 �����.��5      �  J� RenderTransformOrigin$ 
0.36,0.503&�6   .+�5   
   �� $��Registar Instituto��6   $��39,28,13,597q�6-   $��36(�6C   $
��Bold"�6Q   �� 5   
   
����  �$��43,88,44,591q�6&   $ 0.5,0.5&�6<   z�5      V� 5      .~�5      ��  �� ScaleY$ 1Q�6%   �� 5      �� 5      T� 5      5      5      �� 5   
   -   5         ��Name$ nomeBox��5      $
��Left=�6'   $	��Top=�6B   $	��164��6Z   $��
45,139,0,0q�6f   �� 5   
   $��Nome Instituto��6   $��43,110,214,541q�6)   $��20(�6A   $
��Bold"�6O   �� 5   
   -   5        $ localizacaoBox��5      $
��Left=�6.   $	��Top=�6I   $	��170��6a   $��
43,205,0,0q�6m   �� 5   
   $��Localização��6   $��44,171,213,480q�6&   $��20(�6>   $
��Bold"�6L   �� 5   
   -   5         ��Name$ EditarButton��5      $��248,602,55,46q�6'   $��16(�6?   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��	Confirmar��5      $ -1.742,2.197&�6-   $��74��6�   '  !MaterialDesignThemes.Wpf.PackIcon  5      ���#FFFFFF   Kind$
 Done=�6?   $��18��6K   $��22��6V   5       5!      �� 5"   
   -   5        $ contactoBox��5"      $
��Left=�6+   $	��Top=�6F   $	��165��6^   $��
46,278,0,0q�6j   �� 5#   
   $��Contacto��6   $��43,241,215,399q�6#   $��20(�6;   $
��Bold"�6I   �� 5$   
   $��	Serviços��6   $��39,409,218,237q�6#   $��20(�6;   $
��Bold"�6I   �� 5%   
   $
��Auto=�6   $
��Auto��6:   $��43,443,48,147q�6H   .��5&      �� -   5         ��Name$ painel��5&      $ Vertical=�6)   $��0,0,0,0q�6A   $	��316��6R   $	��530��6^   5;      5<      �� 5=   
   -   5        $ adicionarServico��5=   (   ���	#FF60C4E5$��285,538,55,110q�6B   .��5>      �� $ 
Horizontal=�6   .+�5?        ���#FFFFFF$	 Add=�6?   $��18��6J   $��18��6U   �� 5@      -   5        $	��Add��5@      $ -1.742,2.197&�6'   6L   5A      5B      �� 5C   
   -	   5        $ EditarButton_Copy��5C      $��66,602,238,46q�6,   $��16(�6C   .��5D      �� $ 
Horizontal=�6   .+�5E      �� -
   5        $��Cancelar��5E      $ -1.742,2.197&�6,   $��65��6�     5F      ���#FFFFFF$ Clear=�6?   $��18��6L   $��25��6W   5G      5H      �� 5I   
   -   5        $��238,292,44,356q�5I      .��5J      �� $ 
Horizontal=�6   .+�5K      �� $��Adicionar Foto��6   $ -1.742,2.197&�62   $��95��6W     5L      ���#FFFFFF$ Publish=�6?   $��18��6N   $��21��6Y   5M      5N      � 5O   
   -   5        	 �Name$	 institutofotogrid��5O      $��230,129,33,405q�6*   
 � ColumnDefinitions
 5P      �� 5Q      $��3* �6#   �� 5R      $	��14* �6#   ��5T      �� 5U      -   5        institutofoto�� �� ImageSource$! Images/defaultInstituto.jpg��5U   4   5W      �� 5X   
   -   5        $ descricaoBox��5X      $
��Left=�6,   $	��Top=�6G   $	��351��6_   $��
39,363,0,0q�6k   $��41��6   �� 5Y   
   $��Descrição��6   $��39,314,218,330q�6$   $��20(�6<   $
��Bold"�6J   5[      5\      