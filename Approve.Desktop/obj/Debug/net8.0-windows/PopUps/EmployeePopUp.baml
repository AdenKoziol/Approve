   M S B A M L   `   `   `  ����J  FApprove.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Approve.DesktopApprove.Desktop  5   	   #    Approve.Desktop.EmployeePopUp   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Approve.Desktop     5   	     � Title$  Employee��5   	    � SizeToContent$ Height=�6   $	��800��61    � WindowStartupLocation$ CenterScreen=�5	   	   .��5      � ���#252526 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5    
   �� $��Employee Name��6   $��0��6(   
�������65   �� 5!   
   -   5         ��Name$ txtName��5!      $�� ��6#   $��1��6+   $��5,0,5,0q�68   6I   �� 5"   
   -   5         ��Name$ txtNameError��5"      $#��Employee Name cannot be empty��6&   
���  ��$y�	Collapsed=�6_   $��2��6v   �� 5$   
   $��Email��6   $��3��6    
�������6-   �� 5%   
   -   5        $ txtEmail��5%      $�� ��6$   $��4��6,   $��5,0,5,0q�69   6J   �� 5&   
   -   5        $ txtEmailError��5&      $��Email cannot be empty��6'   
���  ��$y�	Collapsed=�6X   $��5��6o   �� 5(   
   $
��Team��6   $��6��6   
�������6,   �� 5)   
   -   5         ��Name$ ddlTeam��5)   ,   $��7��6=   $��5,0,5,0q�6J    �� DisplayMemberPath$
 Name��6}   ��6[   �� *	TeamsList+6�   �� 5*   
   -   5        $ txtTeamError��5*      $��You must select a team��6&   
���  ��$y�	Collapsed=�6X   $��8��6o   �� 5,   
   -   5         ��Name$ 
spUsername��5,      $��9��6)   .+�5-      �� $��Username��6   $��9��6'   
�������64   �� 5.      -   5        $ txtUsername��5.      $��10��6+   $��5,0,5,0q�69   6J   �� 5/      -	   5        $ txtUsernameError��5/      $��Username cannot be empty��6.   
���  ��$y�	Collapsed=�6b   $��11��6y   �� 50      -
   5        $ txtUsernameTakenError��50      $��Username in use��63   
���  ��$y�	Collapsed=�6^   $��11��6u   51      �� 53   
   -   5        $ 
spPassword��53      $��10��6)   .+�54      �� $��Password��6   
�������6'   �� 55      -   5        $ txtPassword��55      $��5,0,5,0q�6+   6<   �� 56      -   5        $ txtPasswordError��56      $��Password cannot be empty��6.   
���  ��$y�	Collapsed=�6b   $��14��6y   57      �� 59   
   	 �� Orientation$	 
Horizontal=�6   $��Right=�6.   $��16��6J   $��5q�6X   .+�5:      �� -   5        
 ��Name$
 btnSave��5:      $
��Save��6&   65   �� 5;      -   5        $
 btnClose��5;      $��Close��6'   67   5<      �� 5=   
   -   5        $
 	btnDelete��5=      $��Delete��6$   $
��Left=�65   $y�	Collapsed=�6P   ���#FF474C$��5,5,0,5q�6�   $��16��6�   5>      5?      