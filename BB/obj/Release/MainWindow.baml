   M S B A M L   `   `   `  ����   BB    DailyPlanner.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Ежедневник��$	��480��$
��1250��.���  � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�$��10q�.+��� $��	Дата:��$��Center=�$��0,0,5,0q�(  "System.Windows.Controls.DatePicker  -     Name$ 
datePicker��$��0,0,10,0q��� $��Время:��$��Center=�$��0,0,5,0q��� -    ��Name$ timeTextBox��$��60��$��0,0,10,0q��� $��Описание:��$��Center=�$��0,0,5,0q��� -   $ descriptionTextBox��$	��400��$��0,0,10,0q��� -    ��Name$ typeComboBox��$	��100��$��25�� �� SelectedIndex$ 0��.@��� $��Работа���� $��
Учеба���� $��Другое���� -   $��Добавить��$��4��$��1��$��5q��� -   $��
Поиск��$��4��$��1��$��5q��� -   $ searchTextBox��$��84���� -   $��Фильтр��$��5q�$��19���� -	   $��Сбросить��$��4��$��1��$��5q��� -
   $��Обновить��$��4��$��2��$��5q��� -    ��Name$ eventListView��$��1��$��10,10,10,10q����� *Events+ �� View �� .P��� $��Дата��	 �� DisplayMemberBinding	 �� *Date+
 �� StringFormat$
 d���� $��
Время��	 �� *Time+�� $��Описание��	 �� *Description+�� $��Тип��	 �� *Type+�� $�� �� �� Width$
 Auto�� �� CellTemplate �� .R��� -   $��Изменить���� $�� ��$
 Auto�� �� .R��� -   $��Удалить��