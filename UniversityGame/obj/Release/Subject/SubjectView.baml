   M S B A M L   `   `   `  ���� I  EUniversityGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:UniversityGame.SubjectUniversityGame.Subject  (    "UniversityGame.Subject.SubjectView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:UniversityGame.Subject     $	��780��.��� .+��� -     ��Name$  subjectTable��$��	0,0,280,0q� �� View �� .P��� $��ID�� �� Width$ 50�� �� DisplayMemberBinding �� *id+�� $
��Name��$	 100�� �� *name+�� $��Lections��$ 80�� �� *
lections+�� $��	Practical��$ 80�� �� *	practical+�� $��	Labratory��$ 80�� �� *	labratory+�� $��	Semestors��$	 100�� �� *	semestors+� $��535,10,25,224q� � ColumnDefinitions �� $��7* ��� $	��15* � � RowDefinitions �� $	��28* ��� $	��28* ��� $	��28* ��� $	��28* ��� $	��28* ��� .+��� -    ��Name$ 	nameField��$��1��$
��Left=� �� TextWrapping$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ 	lectField��$��1��$
��Left=�$��1��$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ 
practField��$��1��$
��Left=�$��2��$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ labField��$��1��$
��Left=�$��3��$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ semField��$��1��$
��Left=�$��4��$
 Wrap=�$	��Top=�$	��150��$��22���� -    ��Name$ label��$
��Name��$��Right=�$	��Top=�$��70��$��25���� -   $ label1��$��Lections��$��Right=�$��Bottom=�$��70��$��25��$��1��$��0,0,0,4q��� -	   $ label2��$��	Practical��$��Right=�$��2��$��Bottom=�$��70��$��25��$��0,0,0,3q��� -
   $ label3��$��	Labratory��$��Right=�$��3��$��Bottom=�$��70��$��25��$��0,0,0,3q��� -   $ label4��$��	Semestors��$��Right=�$��4��$��Bottom=�$��70��$��23��$��0,0,0,5q�� $��505,210,5,40q� �� �� �� �� .+��� -   	 ��Name$	 	addButton��$��Add Data���� -   $	 deleteButton��$��1��$��Delete Data���� -   $	 updateButton��$��2��$��Update Data���� -   $	 updateField��$��3��$��Make Report��