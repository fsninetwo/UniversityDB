   M S B A M L   `   `   `  ����I  EUniversityGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:UniversityGame.DepartmentUniversityGame.Department  5      .    (UniversityGame.Department.DepartmentView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:UniversityGame.Department     5      $	��780��5   #   .��5	      � .+�5
   
   �� -   5          ��Name$  subjectTable��5
      $��	0,0,280,0q�6)    �� View 5      �� 5      .P�5      �� $��ID��6%    �� Width$ 50��6U    �� DisplayMemberBinding 61   �� *id+6U   �� 5      $
��Name��6%   $	 100��6Y    63   �� *name+6Y   �� 5      $��Faculty��6%   $	 150��6_    66   �� *	faculty+6_   �� 5      $��Description��6%   $	 150��6g    6:   �� *description+6g   5      5      � 5   
   $��535,10,25,305q�6    � ColumnDefinitions 5      �� 5      $��7* �6#   �� 5      $	��15* �6#    � RowDefinitions 5      �� 5      $	��28* �6    �� 5      $	��28* �6    .+�5      �� -   5         ��Name$ 	nameField��5      $��1��6)   $
��Left=�69    �� TextWrapping$
 Wrap=�6T   $	��Top=�6h   $	��150��6�   $��25��6�   �� 5      -   5         ��Name$ label��5      $
��Name��6#   $��Right=�62   $	��Top=�6N   $��70��6f   $��25��6q   �� 5      -   5        $ labe2��5      $��Faculty��6#   $��Right=�65   $	��Top=�6Q   $��70��6i   $��25��6t   $��1��6�   �� 5       -   5        	 ��Name$	 facultyChoice��5       $��1��6.   $
��Left=�6>   $��1��6Y   $	��Top=�6f   $	��150��6~   $��23��6�   5!      � 5"   
   $��505,240,5,10q�6    5#      �� 5$      �� 5%      �� 5&      �� 5'      .+�5)      �� -   5        
 ��Name$
 	addButton��5)      $��Add Data��6(   6;   �� 5*      -   5        $
 deleteButton��5*   "   $��1��6   $��Delete Data��68   6N   �� 5+      -   5        $
 updateButton��5+   "   $��2��6   $��Update Data��68   6N   �� 5,      -	   5        $
 updateField��5,   "   $��3��6   $��Make Report��67   6M   5-      � 5.   
   $
��Left=�6   $	��150��6*   $��
535,85,0,0q�67   $	��Top=�6K   $	��220��6c    5/      �� 50      $	��13* �6    �� 51      $	��69* �6    .+�53      �� -
   5        $ label1��53      $��Description��6$   $
��Left=�6:   $	��Top=�6U   $��26��6m   $	��220��6y   �� 54      -   5         ��Name$ descriptionField��54      $	��220��64   $��1��6@   $��0,0,0,0q�6M   55      56      57      