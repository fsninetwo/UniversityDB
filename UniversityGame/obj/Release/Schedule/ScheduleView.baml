   M S B A M L   `   `   `  ���� I  EUniversityGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:UniversityGame.ScheduleUniversityGame.Schedule  *    $UniversityGame.Schedule.ScheduleView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:UniversityGame.Schedule     $	��780��.��� .+��� -     ��Name$  scheduleTable��$��	0,0,280,0q� �� View �� .P��� $��ID�� �� Width$ 30�� �� DisplayMemberBinding �� *id+�� $	��Day��$ 50�� �� *day+�� $��Start��$ 80�� �� *start+�� $��Finish��$ 80�� �� *finish+�� $��Cabinet��$ 60�� �� *	cabinet+�� $��Group��$ 80�� �� *group+�� $��Subject��$	 110�� �� *	subject+� $��537,10,23,190q� � ColumnDefinitions �� $��7* ��� $	��15* � � RowDefinitions �� $	��28* ��� $	��28* ��� $	��28* ��� $	��28* ��� $	��28* ��� $	��28* ��� .+��� -    ��Name$ dayField��$��1��$
��Left=� �� TextWrapping$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ 
startField��$��1��$
��Left=�$��1��$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ finishField��$��1��$
��Left=�$��2��$
 Wrap=�$	��Top=�$	��150��$��25���� -   $ cabinetField��$��1��$
��Left=�$��3��$
 Wrap=�$	��Top=�$	��150��$��25���� -    ��Name$ groupChoice��$��1��$
��Left=�$��4��$	��Top=�$	��150��$��25���� -   $ subjectChoice��$��1��$
��Left=�$��5��$	��Top=�$	��150��$��25���� -   	 ��Name$	 label��$	��Day��$��Right=�$	��Top=�$��70��$��25���� -	   $	 label1��$��Start��$��Right=�$��1��$��Bottom=�$��70��$��25���� -
   $	 label2��$��Finish��$��Right=�$��2��$��Bottom=�$��70��$��25���� -   $	 label3��$��Cabinet��$��Right=�$��3��$��Bottom=�$��70��$��25���� -   $	 label4��$��Group��$��Right=�$��4��$��Bottom=�$��70��$��25���� -   $	 label5��$��Subject��$��Right=�$��5��$��Bottom=�$��70��$��25��� $��505,210,5,40q� �� �� �� �� .+��� -   
 ��Name$
 	addButton��$��Add Data���� -   $
 deleteButton��$��1��$��Delete Data���� -   $
 updateButton��$��2��$��Update Data���� -   $
 updateField��$��3��$��Make Report��