   M S B A M L   `   `   `  ����D  @AfishaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AfishaApp.PagesAfishaApp.Pages  5          AfishaApp.Pages.Сatalog   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:AfishaApp.Pages     5      -   5          N� Title$  Каталог��5	         	base_page#��[  5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��30 �6   .+�5   
   � $��0��6   $��Stretch=�5        
additional#��[ 5      .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $:��4Введите название для поиска:��6     base_textblock#��[ 5      �� 5      -   5         ��Name$ 
TBoxSearch��5   &   $	��275��6   5      5      �� 5      $ 
Horizontal=�6   .+�5      �� $)��#Выберите категорию��6   #��[ 5      �� 5      -   5         ��Name$ ComboCategory��5   '   $	��275��6    �� DisplayMemberPath$ CategoryName��5!      5"      �� 5#      $ 
Horizontal=�6   .+�5$      �� $#��Выберите статус��6   #��[ 5%      �� 5&      -   5        $ ComboStatus��5&   '   $	��275��6   $ 
StatusName��5(      5)      5*      �� 5+   
   -   5         ��Name$ 
LViewGoods��5+      $��1��6'   $��Disabled=�5,        	good_item#��[ 5-      ��5.      �� 5/      .R�50      �  � Orientation$ 
Horizontal=�6    $��Center=�69   51      ��53      �� 54      .R�55      � $��20q�6   $	��300��6'    56      �� 57      $	��310 �6,   �� 58      $
��auto �6,   �� 59      $��20 �6,   .+�5;      �� $	��240��6    $��0��6,   $��Uniform=�69   $��Center=�5<       $��5q�6=   ��5=       ��  �� Path$ GetPhoto��� 5>      $��1��6%   $	��100��62   .+�5?      �� 	 �� Padding$	 3q�6)   $
��Auto��65     item_textblock#��[ 5@   $   $��Center=�5A   )    
 akcent#��[ 5B   (   $
��Bold"�5C   (   $��20(�6:   ��5D   )   �� *	EventName+�� 5E      $
��Auto��6D   #��[ 5F   $   ��5E   (   �� *
EventPlace+5F   $   5H      �� 5I      $
��Auto��6?   #��[ 5J   $   $��2��6L   ��5I   $   �� *	GetStatus+5J   L   5K      5L      5N      �� 5O   
   -   5        
 ��Name$
 TextBlockCount��5O   !   $��2��6   6!   5P      5Q      