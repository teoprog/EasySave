   M S B A M L   `   `   `  ????F  BEasySaveApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:View.MVVM.ViewView.MVVM.View  5      9!clr-namespace:View.MVVM.ViewModelView.MVVM.ViewModel  5      K*clr-namespace:EasySaveApp.Properties.LangsEasySaveApp.Properties.Langs  5      &     View.MVVM.View.BlackListSoftware   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -viewclr-namespace:View.MVVM.View     5      7	viewModel!clr-namespace:View.MVVM.ViewModel     5      ?language*clr-namespace:EasySaveApp.Properties.Langs     5      
?????? .??5      ?   ? RowDefinitions  5   
   ?? 5      $??50 ?6   ?? 5      $??70 ?6   ?? 5      $
??auto ?6   ?? 5      $	??300 ?6   .+?5   
   ? $??0??6   .+?5      ?? -   5         ??Name$ Settingstitle??5      '   !EasySaveApp.Properties.Langs.Lang   BlackListTitle#??Z 5      $??20(?5      
???????$
??Left=?5      $	??Top=?5      5       ?? 5"   
   $??1??6      BlackListExtension#??Z 5#      $??14(?5$      
???????$??	55 0 0 20q?5&      $??Bottom=?5'      ? 5+   
   $??2??6   $??Center=?6    ? ColumnDefinitions 5,      ?? 5-      $??* ?6#   ?? 5.      $	??100 ?6#   ?? 5/      $	??100 ?6#     52      ?? 53      $
??auto ?6    ?? 54      $??40 ?6    .+?57      ?? -   5        $??SoftwareBox??57   '   $??0??6   $??Center=?58         TBStyle#??[  59      $	??400??5:      5;      ?? 5=      -   5        $??2??5=         	AddButton#??Z 6%   $??Center=?6R   $??Center=?6m   $??0 0 0 1q?5>        
BrushStyle#??[ 5?      $??	 10 3 0 0q?5@      $??10q?5A      
???????$	??100??5C      ?? 5E      -   5        $??1??5E         DeleteButton#??Z 6%   $??Center=?6U   $??Center=?6p   $??0 0 0 1q?5F      #??[ 5G      $??	 10 3 0 0q?5H      $??10q?5I      
???????$	??100??5K      ?? 5O      -   5         ??Name$ ErrorTargetLabel??5O      $??1??6.   
???  ??$??Center=?6L   $y?Hidden=?6g   ?? 5P      -   5        $ SuccesLabel??5P      $??1??6)   
??? ? ?$??Center=?6I   $y?Hidden=?6d   5R      ? 5T   
   $??3??6   .+?5V      &   System.Windows.Controls.DataGrid  -   5          Name$ BusinessGrid??5V      	   AutoGenerateColumns$	 False??6/   
   CanUserSortColumns$
 False??6K      CanUserResizeColumns$ False??6f      CanUserResizeRows$ False??6?    ? Cursor$
 Hand??6?   ???#1F1C2E$??	50 0 50 0q?5W   =   $??0 0 0 0q?6P      Columns 5X      0  *System.Windows.Controls.DataGridTextColumn  5Y         BlackListTable,  &System.Windows.Controls.DataGridColumn   Header# Z 6)      
IsReadOnly$
 True??6|      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6?   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6Z   ?? *SoftwareList+6?    ? 	Resources 5]      =  7System.Windows.Controls.Primitives.DataGridColumnHeader ?? BasedOn ?? 
TargetType ?? Property ?? Value*  $System.Windows.Controls.DataGridCell??%?  '        7 '  ?     7 ?? 5_      8   "  6U   .?5`      ??  ? ?? ?#1F1C2E67   ?? 5a       ? ??$ 0 0 1 1q?6<   ?? 5b       ? ??$ Center=?6G   ?? 5c       ? ??$ Center=?6E   ?? 5d       ? ??
 ?????67   5f      ?? 5g      8   "  6M   .?5h      ??  ?@??TextAlignment$ Center=?6D   ?? 5i       ? ??
 ???? 67   5j      5k      5l      5n      5p      5q      