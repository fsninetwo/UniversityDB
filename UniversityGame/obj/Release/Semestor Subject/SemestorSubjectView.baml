   M S B A M L   `   `   `  ���� I  EUniversityGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:UniversityGame.Semestor_SubjectUniversityGame.Semestor_Subject  9    3UniversityGame.Semestor_Subject.SemestorSubjectView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?local-clr-namespace:UniversityGame.Semestor_Subject     $	��780��.��� .+��� -     ��Name$  semestorSubjectTable��$��	0,0,280,0q� �� View �� .P��� $��ID�� �� Width$ 50�� �� DisplayMemberBinding �� *id+�� $��Lections��$ 60�� �� *
lections+�� $��	Practical��$ 60�� �� *	practical+�� $��	Labratory��$ 60�� �� *	labratory+�� $��Response��$ 80�� �� *
response+�� $��Semestor��$	 100�� �� *
semestor+�� $��Subject��$ 80�� �� *	subject+� $��535,10,25,190q� � ColumnDefinitions �� $��7* ��� $	��15* � � RowDefinitions �� $	��28* ��� $	��28* ��� $	��28* ��� $	��28* ��� $	��28* ��� $	��28* ��� .+��� -    ��Name$ 	lectField��$��1��$
��Left=� �� TextWrapping$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ 
practField��$��1��$
��Left=�$��1��$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ labField��$��1��$
��Left=�$��2��$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ responseField��$��1��$
��Left=�$��3��$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ semestorField��$��1��$
��Left=�$��4��$
 Wrap=�$	��Top=�$	��150��$��25���� -    ��Name$ subjectChoice��$��1��$
��Left=�$��5��$	��Top=�$	��150��$��25���� -   	 ��Name$	 label1��$��Lections��$��Right=�$��Bottom=�$��70��$��25���� -	   $	 label2��$��	Practical��$��Right=�$��1��$��Bottom=�$��70��$��25���� -
   $	 label3��$��	Labratory��$��Right=�$��2��$��Bottom=�$��70��$��25���� -   $	 label4��$��Response��$��Right=�$��3��$��Bottom=�$��70��$��25���� -   $	 label5��$��Semestor��$��Right=�$��4��$��Bottom=�$��70��$��25���� -   $	 label6��$��Subject��$��Right=�$��5��$��Bottom=�$��70��$��23��$��0,0,0,3q�� $��505,210,5,40q� �� �� �� �� .+��� -   
 ��Name$
 	addButton��$��Add Data���� -   $
 deleteButton��$��1��$��Delete Data���� -   $
 updateButton��$��2��$��Update Data���� -   $
 updateField��$��3��$��Make Report��