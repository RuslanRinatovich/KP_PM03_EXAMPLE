   M S B A M L   `   `   `  ����D  @AfishaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:AfishaApp	AfishaApp  5   	       AfishaApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:AfishaApp     5   	   -   5           base_window#��[  5	   	   $	��450��5
   	   $	��800��6     � WindowStartupLocation$  CenterScreen=�5       � Icon$4 ./AfishaApp;component/Resources/spreadsheet.ico��6D    � Title 5   	   ��  �� ElementName$ 	MainFrame�� �� Path$ Content.Title�.��5      �  � RowDefinitions 5   
   �� 5      $��75 �6   �� 5      $��* �6   �� 5      $��30 �6   .+�5   
   �   
additional#��[ 6   �� 5   
   $4��./AfishaApp;component/Resources/spreadsheet.png��6   $��10q�5      $
��Left=�5      �� 5   
   $��0��6   $��
Афиша��6!   $��Center=�5      $��Center=�5      #��[ 5      $��20(�5      $
��Bold"�5       � 5   
   -   5          �Name$ 	MainFrame��5      $��1��6#    
 akcent#��[ 60     � NavigationUIVisibility$ Hidden=�5      $��1q�5      6$   �� 5    
   $��0��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5!      �� -   5        	 ��Name$	 BtnEditGoods��5!      $��Мероприятия��6,   6C   �� 5#      -   5        $	 BtnBack��5#      $��
Назад��6'   69   5$      � 5%   
   $��2��6   #��[ 6   5&      5'      