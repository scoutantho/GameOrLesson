   M S B A M L   `   `   `  ����G  CGameOrLesson, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GameOrLessonGameOrLesson  5   	       GameOrLesson.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:GameOrLesson     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ 
tabControl��5
      $
��Left=�6)   $	��301��6D   $��	10,10,0,0q�6Q   $	��Top=�6d   $	��498��6|   .@�5      �� -   5         ��Name$ Debug��5      $��Debug��6%   .��5      � -   5         �Name$ grille��5   .   ���	#FFE5E5E5 � ColumnDefinitions 5      �� 5      $	��200 �6+   �� 5      $	��200 �6+   �� 5      $	��100 �6+    � RowDefinitions 5      �� 5      $��30 �6(   �� 5      $��30 �6(   .+�5!      �� -   5         ��Name$ nomTab��5!   p   $��0��6   $��0��6.   $��Center=�6;   $	��Top=�6X   $��19��6�    �� TextWrapping$
 Wrap=�6�   $��	50,6,50,0q�6�   $	��100��6�   �� 5#      -   5         ��Name$ addTab��5#   :   $��1��6   $��0��6-   $	��Add��6J   $
��Left=�6X   $��20,6,0,0q�6s   $	��Top=�6�   $��60��6�   6�   �� 5%      -   5        $ 	removeTab��5%   :   $��2��6   $��0��6-   $��Remove��6M   $��Center=�6^   $��0,10,0,0q�6{   $	��Top=�6�   $��60��6�   6�   �� 5&      -   5        	 ��Name$	 
listBoxTab��5&      $��2��62   $
��Left=�6F   $	��240��6a   $��0,1.8,0,-210.6q�6n   $��1��6�   $	��Top=�6�   $	��400��6�   �� 5'      -   5        
 ��Name$
 checkBox��5'      $��Only Files ��61   $��1��6G   $
��Left=�6W   $��	101,8,0,0q�6r   $	��Top=�6�   5(      5)      �� 5*      $��option��6   .��5+      � ���	#FFE5E5E56   5,      5.      5/      50      