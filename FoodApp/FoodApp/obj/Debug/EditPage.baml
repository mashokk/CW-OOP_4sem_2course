   M S B A M L   `   `   `  ����B  >FoodApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:FoodAppFoodApp  5          FoodApp.EditPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:FoodApp     5        N� Title$  EditPage��5	       � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  '��       '�� n    '�� �    '�� &    '�� p    '�� �    '  .    �� 5   
   " ��6   .�5      ��  � ��$ 35��6'   �� 5       � �� �#ad6a3c6+   �� 5       � ��
 �����6+   �� 5       � ��$ 10q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ 20��6!   #��z��5   !   ._�5      �� #��z��6/   #��z��5   /   5      5      5      5      �� 5   
   " ��6   .�5      ��  � ��$ 30��6'   �� 5        � ��
 �����6+   5!      �� 5"   
   " ��6   .�5#      ��  � ��
 �����6+   5$      �� 5%   
   " ��6   .�5&      ��  � ��
 �����6+   5'      �� 5(   
   " ��6   .�5)      ��  � ��
 �����6+   5*      �� 5+   
   " ��6   .�5,      ��  � �� �#ad6a3c6+   �� 5-       � ��
 �����6+   5.      �� 5/   
   "  6   .�50      ��  � ��$ Center=�69   �� 51       � ��$ 35��6'   �� 52       �@ SeparatorBrush
 �  ��6/   �� 53       � �� �#ad6a3c6+   �� 54       � ��
 �����6+   �� 55       � ��
 �����6,   �� 56       � ��$	 0.5q�60   57      58      .��5:      � ���#2E1201.+�5;   
   ]� .+�5<      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��0,10,10.4,10q�6N   $��40��6d   $	��Top=�6p   $	��730��6�   .+�5=      �� -   5        $��На главную��5=      $��0, 0, 350, 0q�6.   $	��140��6D   6P   �� 5>      -   5        $+��%Сохранить изменения��5>      $	��3 0q�67   $	��150��6D   6P   �� 5?      -   5        $��Удалить��5?      $	��3 0q�6+   $��70��68   6C   5@      &   System.Windows.Controls.DataGrid  5B      -   5          Name$ compositionTable��5B      	   CanUserResizeRows	 .  
   AutoGenerateColumns
 .  $��
10 0 10 10q�6g   ���#2E1201   Columns 5C      4  .System.Windows.Controls.DataGridComboBoxColumn  5D      -   5        colDish��,  &System.Windows.Controls.DataGridColumn   Header$    Блюдо��5D   >      DisplayMemberPath$ 	Dish_name��5E   %      SelectedValuePath$ ID��5F   %      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5H   %      SelectedValueBinding 5G   %   ��  �� Path$ ID_Dish�5H   %     5I      -   5        	colIngred��$    Ингридиент��5I   @   $ Ingredient_name��5J   %   $ ID��5K   %   $ * 5M   %    5L   %   �� $ ID_Ingredient�5M   %   5O      5P      5Q      5R      