   M S B A M L   `   `   `  ����E  ABeautyCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BeautyCare
BeautyCare  5      !    BeautyCare.adicionarServico   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:BeautyCare     5      V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  adicionarServico��5      $	��680��6    $	��425��6-    N� 
Background
 �����.��5
      � .+�5   
   �� $��Adicionar Serviço��6   $��41,31,11,594q�6,   $��36(�6B   $
��Bold"�6P   �� 5   
   
����  �$��42,86,45,593q�6&    J� RenderTransformOrigin$ 0.5,0.5&�6<   z�5      V� 5      .~�5      ��  �� ScaleY$ 1Q�6%   �� 5      �� 5      T� 5      5      5      �� 5   
   -   5         ��Name$ nomeBox��5      $
��Left=�6'   $	��Top=�6B   $	��181��6Z   $��
45,158,0,0q�6f   �� 5   
   $��Tipo Serviço��6   $��41,120,216,527q�6'   $��20(�6?   $
��Bold"�6M   �� 5   
   -   5        $ precoBox��5      $
��Left=�6)   $	��Top=�6D   $��81��6\   $��
42,272,0,0q�6g   �� 5   
   $��Preço��6   $��39,210,218,434q�6    $��20(�68   $
��Bold"�6F   �� 5   
   -   5         ��Name$ ConfirmButton��5      $��248,582,55,66q�6(   $��16(�6[   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��	Confirmar��5      $��74��6^   '  !MaterialDesignThemes.Wpf.PackIcon  5      ���#FFFFFF   Kind$
 Done=�6?   $��18��6K   $��22��6V   5      5      �� 5    
   -   5        $ descricaoBox��5       $
��Left=�6,   $	��Top=�6G   $	��341��6_   $��
39,380,0,0q�6k   $	��158��6   �� 5!   
   $��Descrição��6   $��39,322,218,322q�6$   $��20(�6<   $
��Bold"�6J   �� 5"   
   -   5        $ CancelButton��5"      $��62,582,241,66q�6'   $��16(�6Y   .��5#      �� $ 
Horizontal=�6   .+�5$      �� -   5        $��Cancelar��5$      $ -1.742,2.197&�6,   $��65��6�     5%      ���#FFFFFF$ Clear=�6?   $��18��6L   $��25��6W   5&      5'      �� 5(   
   -   5        $��236,300,35,348q�5(      .��5)      �� $ 
Horizontal=�6   .+�5*      �� $��Adicionar Foto��6   $ -1.742,2.197&�62   $��97��6W     5+      ���#FFFFFF$ Publish=�6?   $��18��6N   $��21��6Y   5,      5-      � 5.   
   -	   5         �Name$ servicofotogrid��5.      $��236,135,27,399q�6(   	 � ColumnDefinitions	 5/      �� 50      $��3* �6#   �� 51      $	��14* �6#   ��53      �� 54      -
   5        servicofoto��
 �� ImageSource$!
 Images/defaultInstituto.jpg��54   2   6^   56      �� 57   
   $	��€��6   $��123,261,234,383q�6   $��20(�65   $
��Bold"�6C   58      59      