   M S B A M L   `   `   `  ����D  @AfishaApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AfishaApp.PagesAfishaApp.Pages  5           AfishaApp.Pages.EventsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:AfishaApp.Pages     5      -   5          N� Title$  Мероприятия��5	         	base_page#��[  6D   .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ DataGridGood��5      $��0��6)      AutoGenerateColumns .     
IsReadOnly .    	RowHeight$ 30Q�5         SelectionMode$ Single=�5        	grid_item#��[ 5         Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6)   	   Header$	 Изображение��65   
   CellTemplate
 5      �� 5      .R�5      �� ��6$   �� *
GetPhoto+5      5      0  *System.Windows.Controls.DataGridTextColumn  5)      $	 Название��6C   $ 2* 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *	EventName+6U     5*      $	 Информация��6>   $ * 6R    6%   �� *Info+6R     5+      $#	 Дата проведения��6U   $ * 6n    6%   �� *	EventDate+ �� StringFormat$ d��6n     5,      $%	 Место проведения��6D   $ * 6^    6%   �� *
EventPlace+6^     5-      $	 Длительность��6B   $ * 6X    6%   �� *
Duration+6X     5.      $	 Категория��6O   $ * 6b    6%   �� *Category.CategoryName+6b     5/      $	 Статус��6K   $ * 6[    6%   �� *Status.StatusName+6[     50      $
 auto 6)   $ 	 Редактировать��66   
 51      �� 52      .R�53      ��  ��Name$ BtnEdit��6%   -   5        $	��...��53   6   $��0q�6X   54      56      58      �� 59   
   -   5        $ BtnAdd��59      $��1��6!   $	��100��6.   $��Right=�6:   $��	3 0 120 0q�6V   $��Добавить��5:      6$   �� 5;   
   -   5        $ 	BtnDelete��5;      $��1��6$   $	��100��61   $��Right=�6=   $��3q�6Y   $��Удалить��5<      6#   �� 5=   
   -   5        $ BtnEditStatus��5=      $��1��6(   $	��100��65   $
��Left=�6A   $��Статусы��6\   6o   �� 5>   
   -   5        $ BtnEditCategory��5>      $��1��6+   $	��100��68   $
��Left=�6D   $��	120 3 0 0q�6`   $��Категории��6s   6�   5?      5@      