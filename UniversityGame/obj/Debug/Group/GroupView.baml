   M S B A M L   `   `   `  ����I  EUniversityGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:UniversityGame.GroupUniversityGame.Group  5      $    UniversityGame.Group.GroupView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:UniversityGame.Group     5      $	��780��5   #   .��5	      � .+�5
   
   �� -   5          ��Name$  
groupTable��5
      $��	0,0,280,0q�6'    �� View 5      �� 5      .P�5      �� $��ID��6%    �� Width$ 50��6U    �� DisplayMemberBinding 61   �� *id+6U   �� 5      $
��Name��6%   $	 150��6Y    63   �� *name+6Y   �� 5      $��
Department��6%   $	 200��6e    69   �� *
department+6e   5      5      � 5   
   $��535,10,25,305q�6    � ColumnDefinitions 5      �� 5      $	��37* �6#   �� 5      $	��73* �6#    � RowDefinitions 5      �� 5      $	��28* �6    �� 5      $	��28* �6    �� 5      .+�5      �� -   5         ��Name$ 	nameField��5      $��1��6)   $
��Left=�69    �� TextWrapping$
 Wrap=�6T   $	��Top=�6h   $	��146��6�   $��25��6�   �� 5      -   5         ��Name$ departmentChoice��5      $��1��61   $
��Left=�6A   $��1��6\   $	��Top=�6i   $	��146��6�   $��23��6�   �� 5      -   5        	 ��Name$	 label��5      $
��Name��6#   $��Right=�62   $	��Top=�6N   $��74��6f   $��25��6q   �� 5       -   5        $	 label1��5       $��
Department��6$   $��Right=�69   $��Bottom=�6U   $��75��6p   $��25��6{   $��1��6�   $��-1,0,0,2q�6�   5!      � 5"   
   $��505,210,5,40q�6    5#      �� 5$      �� 5%      �� 5&      �� 5'      .+�5)      �� -   5        
 ��Name$
 	addButton��5)      $��Add Data��6(   6;   �� 5*      -   5        $
 deleteButton��5*   "   $��1��6   $��Delete Data��68   6N   �� 5+      -   5        $
 updateButton��5+   "   $��2��6   $��Update Data��68   6N   �� 5,      -	   5        $
 updateField��5,   "   $��3��6   $��Make Report��67   6M   5-      5.      5/      