   M S B A M L   `   `   `  ����E  ABeautyCare, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:BeautyCare
BeautyCare  5      $    BeautyCare.PersonalInformation   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:BeautyCare     5      V RMaterialDesignThemes.Wpf, Version=3.1.1.1361, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  Personalinformation_Page��5	      $	��680��6(   $	��425��65    N� 
Background
 �����.��5      � .+�5   
   �� -   5         ��Name$ ButtonVoltar��5      $��60��6'   $��60��62   $	��Top=�6k   $
��Left=�6�   ��6>   Z� ��6T   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  ���#000000   Kind$ 	ArrowLeft=�6;   $��25��6L   $��25��6W   5      �� 5   
   $��Perfil��6   $��42,65,10,562q�6!   $��36(�67   $
��Bold"�6E   �� 5   
   
����  �$��42,118,45,561q�6&    J� RenderTransformOrigin$ 0.5,0.5&�6=   z�5      V� 5      .~�5      ��  �� ScaleY$ 1Q�6%   �� 5      �� 5      T� 5      5      5      A� 5   
   -   5         A�Name$ userfotoelipse��5   +   $	��178��6   $	��169��6   $��30,124,226,378q�6C   ��5      �� 5      -   5        userfoto�� �� ImageSource$ Images/userdefault.png��5   /   6V   5      �� 5!   
   -   5         ��Name$ nomeUtilizador��5!      $��
Rui Santos��6(   $��228,194,7,448q�6=   $��22(�6T   $
��Bold"�6b   �� 5"   
   -   5        	 ��Name$	 EmailBox��5"   (   
 �� 
IsReadOnly$

 True��6   $
��Left=�6:   $	��Top=�6U   $	��338��6m   $��
42,413,0,0q�6y   6�   �� 5#   
   $��Email��6   $��38,379,219,272q�6    $��18(�68   $
��Bold"�6F   �� 5$   
   -   5        $	 
Localidade��5$   (   $

 True��6   $
��Left=�6<   $	��Top=�6W   $	��338��6o   $��
42,505,0,0q�6{   6�   �� 5%   
   $��
Localidade��6   $��38,471,219,180q�6%   $��18(�6=   $
��Bold"�6K   �� 5&   
   -   5        $ EditarButton��5&      $��169,586,169,62q�6'   .��5'      ��  �� Orientation$ 
Horizontal=�6   .+�5(        ���#FFFFFF$
 Edit=�6?   $��18��6K   $��18��6V   �� 5)      $��Editar��6   $ -1.742,2.197&�6*   5*      5+      �� 5,   
   -   5        $��42,307,245,341q�5,      .��5-      �� $ 
Horizontal=�6   .+�5.      �� $��Alterar Foto��6   $ -1.742,2.197&�60   $��79��6U     5/      ���#FFFFFF$ Publish=�6?   $��18��6N   $��18��6Y   50      51      �� 52   
   -	   5        $ CancelarButton��52      $��31,587,245,61q�6)   $y�	Collapsed=�6@   .��53      �� $ 
Horizontal=�6   .+�54      �� $��Cancelar��6   $ -1.742,2.197&�6,   $��65��6Q     55      ���#FFFFFF$ Clear=�6?   $��18��6L   $��25��6W   56      57      �� 58   
   -
   5        $ ConfirmarButton��58      $��241,587,35,61q�6*   $y�	Collapsed=�6A   .��59      �� $ 
Horizontal=�6   .+�5:      �� $��	Confirmar��6   $��74��6-     5;      ���#FFFFFF$
 Done=�6?   $��18��6K   $��22��6V   5<      5=      5@      5A      