   M S B A M L   `   `   `  ����J  FApprove.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Approve.DesktopApprove.Desktop  5   	       Approve.Desktop.Login   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Approve.Desktop     5   	     � Title$  Login��5   	   $	��450��6   $	��400��6$    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � ���#252526 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Approve��6   
�������$��0��65   $��60(�6B   $��Center=�6P   $��SemiBold"�6m   $��	0,40,0,40q�6�   �� 5   
   $��Username��6   $��1��6#   
�������60   �� 5   
   -   5         ��Name$ txtUsername��5      $��2��6'   $��5,0,5,0q�64   �� 5   
   $��Password��6   $��3��6#   
�������60   G� 5   
   -   5         G�Name$ txtPassword��5      $��4��6+   $��5,0,5,0q�68   ���#3B3B3B���#FFFFFF6^   �� 5   
   -   5         ��Name$ lblError��5      $,��&Please enter a valid username/password��6"   $��5��6S   
���  ��$y�	Collapsed=�6q   �� 5   
   -   5         ��Name$ btnLogin��5      $��6��6#   $��Login��60   $��5,20,5,5q�6W    �� 	IsDefault$
 True��6i   5       5!      