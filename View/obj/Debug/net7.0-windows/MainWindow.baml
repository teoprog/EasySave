   M S B A M L   `   `   `  ????F  BEasySaveApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5   	   9!clr-namespace:View.MVVM.ViewModelView.MVVM.ViewModel  5   	   K*clr-namespace:EasySaveApp.Properties.LangsEasySaveApp.Properties.Langs  5   	       View.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:View     5   	   7	viewModel!clr-namespace:View.MVVM.ViewModel     5   	   ?language*clr-namespace:EasySaveApp.Properties.Langs     5   	   -   5        $	??700??5
   	   $	??920??6     ? WindowStyle$
  None=?5   	    ? 
ResizeMode$ NoResize=?5   	   
??????  ? AllowsTransparency$
 True??5   	    ? DataContext 5      '  !View.MVVM.ViewModel.MainViewModel  5   
   .??5      ?? ???#272537 ?? CornerRadius$ 20??5      ._?5   
   ?  ? ColumnDefinitions 5      ?? 5      $	??200 ?6#   ?? 5       ? RowDefinitions 5      ?? 5      $??75 ?6    ?? 5       .+?5#      ?? $??EasySave™??6   $??Center=?5$      $
??left=?5%      
???????$??22(?5'      $??20,0,0,0q?5(      ?? 5*      $??1??6   .+?5,      	? '   !EasySaveApp.Properties.Langs.Lang   MainHomeButton#??Z 6   $??70??5-      
???????$??14(?5/         MenuButtonTheme#??[  50       d? 	IsChecked$
 TrueY?51      ??52      ?? *HomeViewCommand+	? 54      	   MainCompleteBackUpButton#??Z	 6   $??70??55      
???????$??14(?57      #??[  58      ??59      ?? *SecondViewCommand+	? 5;      
   MainDifferentialButton#??Z
 6   $??70??5<      
???????$??14(?5>      #??[  5?      ??5@      ?? *ThirdViewCommand+	? 5B      !   MainBusinessSoftwareButton#??Z 6   $??70??5C      
???????$??14(?5E      #??[  5F      ??5G      ?? *BlackListViewCommand+	? 5I         MainSettingsButton#??Z 6   $??70??5J      
???????$??14(?5L      #??[  5M      ??5N      ?? *SettingsViewCommand+	? 5P         MainAboutUsButton#??Z 6   $??70??5Q      
???????$??14(?5S      #??[  5T      ??5U      ?? *AboutUsViewCommand+5V      ?? 5Y      $??1??6   $??1??5Z      $??10q?5[      ??5\      ?? *CurrentView+A? 5^      -   5        ???#1F1C2E$??0??5^   &   $??1??63   $??19??6C   $??19??6O   $??Right=?6Z   $	??Top=?6v   $??	0,20,20,0q?6?    ? ToolTip$ Close??6?    ? Cursor$
 Hand??6?   6?   5a      5c      5d      