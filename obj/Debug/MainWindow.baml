   M S B A M L   `   `   `  ����J  FSitekTestingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:SitekTestingAppSitekTestingApp  5   	        SitekTestingApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:SitekTestingApp     5   	     � Title$  
MainWindow��5   	   $	��908��6   $	��800��6)   $
��1200��5	   	   $	��950��6   .��5
      � .+�5   
   �� $
��Left=�6   $	��Top=�60   $��	49,30,0,0q�6H   $��90��6[   $	��155��6h   .+�5      �� -   5         ��Name$ OpenFileRKKButton��5      $$��Выбрать файл РКК��60   $	��Top=�6K   $��26��6c   6p   �� 5      -   5        $ OpenFileAppealsButton��5      $0��*Выбрать файл обращений��64   $��0,5,0,0q�6U   $	��Top=�6f   $��25��6   6�   �� 5      -   5        $ 
ButtonLoad��5      $$��Записать в форму��6)   $��0,5,0,0q�6D   $	��Top=�6V   $��25��6n   6z   5      �� 5   
   $
��Left=�6   $	��Top=�60   $��
229,30,0,0q�6H   $��50��6\   .+�5      �� -   5         ��Name$ TextBlockRKK��5      $
��Left=�6.   $
��Wrap=�6J   $�� ��6^   $	��Top=�6f   $	��181��6~   $��25��6�   �� 5      -   5        $ TextBlockAppeals��5      $��0,5,0,0q�62   $
��Left=�6C   $
��Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��181��6�   $��25��6�   5      �� 5   
   $
��Left=�6   $	��Top=�60   $��
453,30,0,0q�6H   $��70��6\   $	��355��6h   .+�5      �� -   5        $ Total��5      �� 5      -   5        $ TotalRKK��5      $��0,5,0,0q�6*   �� 5      -   5        $ TotalAppeals��5      $��0,5,0,0q�6.   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��67,164,-57,693q�6.   $��35��6F   .+�5      �� -	   5        $ TextBlockSort��5      $*��$Выбрать сортировку:��6/   $��18(�6K   �� 5      -
   5        $ ButtonNameSort��5      $��По ФИО��6-   $��5q�6[   $	��110��6f   �� 5       -   5        $ ButtonRKKSort��5       $��По РКК��6,   $��5q�6Y   $	��110��6d   �� 5!      -   5        $ ButtonAppealsSort��5!      $��По обращениям��60   $��5q�6h   $	��110��6s   �� 5"      -   5        $ ButtonGeneralSort��5"      $#��По общему кол-ву��60   $��5q�6k   $	��110��6v   5#      �� 5%   
   $��67,205,67,93q�6   $��Center=�6+   $	��600��6H   $	��600��6T   .+�5&      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGrid��5&      $	��600��6K      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6W   $	��600��6g   5'      �� 5)   
   $ 
Horizontal=�6   $��0,818,10,24q�6.   $��30��6C   .+�5*      �� -   5        $ TextBlockTodayDate��5*      $
��Left=�64   $	��265��6P   $��15(�6\   $��	70,10,0,0q�6j   �� 5+      -   5        $ 	TextBlock��5+      $
��Left=�6+   $
��Wrap=�6G   $1��+Время работы программы:��6[   $	��Top=�6z   $��15(�6�   $��	150,7,0,0q�6�   �� 5,      -   5        $ TextBlockTime��5,      $
��Left=�6/   $
��Wrap=�6K   $�� ��6_   $	��Top=�6g   $	��120��6   $��15(�6�   $��10,7,0,0q�6�   �� 5-      -   5        $ 
ButtonSave��5-      $-��'Вывести данные в файл��6)   $��30��6I   $	��146��6n   $��	189,0,0,0q�6z   5.      5/      50      