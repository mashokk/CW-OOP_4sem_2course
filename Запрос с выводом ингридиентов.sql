use CW_Recipes;

--��������� ���������� ��� ������ ������
DECLARE @Ingredients NVARCHAR(MAX);
--��������� ������
SELECT @Ingredients = ISNULL(@Ingredients + ', ','') + QUOTENAME(Ingredients.Ingredient_name) 
FROM     Dishes INNER JOIN
                  Dish_Composition ON Dishes.ID = Dish_Composition.ID_Dish INNER JOIN
                  Ingredients ON Dish_Composition.ID_Ingredient = Ingredients.ID
WHERE (Dishes.Dish_name = '���������');
--������� ���������
SELECT @Ingredients AS TextProduct