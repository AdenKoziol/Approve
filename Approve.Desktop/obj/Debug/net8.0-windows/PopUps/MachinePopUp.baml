   M S B A M L   `   `   `  ����J  FApprove.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Approve.Desktop.PopUpsApprove.Desktop.PopUps  5   	   )    #Approve.Desktop.PopUps.MachinePopUp   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Approve.Desktop.PopUps     5   	     � Title$  Machine��5   	    � SizeToContent$ Height=�6   $	��800��60    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � ���#252526 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� $��Machine Name��6   $��0��6'   
�������64   �� 5   
   -   5         ��Name$ txtName��5      $�� ��6#   $��1��6+   $��5,0,5,0q�68   6I   �� 5   
   -   5         ��Name$ txtNameError��5      $"��Machine Name cannot be empty��6&   
���  ��$y�	Collapsed=�6^   $��2��6u   �� 5   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��4��6J   $��5q�6W   .+�5      �� -   5         ��Name$ btnSave��5      $
��Save��6&   65   �� 5      -   5        $ btnClose��5      $��Close��6'   67   5      �� 5   
   -   5        $ 	btnDelete��5      $��Delete��6$   $
��Left=�65   $y�	Collapsed=�6h   ���#FF474C$��5,5,0,5q�6�   $��4��6�   5      5      