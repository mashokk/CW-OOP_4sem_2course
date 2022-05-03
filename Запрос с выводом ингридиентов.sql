use CW_Recipes;

--Объявляем переменную для строки данных
DECLARE @Ingredients NVARCHAR(MAX);
--Формируем строку
SELECT @Ingredients = ISNULL(@Ingredients + ', ','') + QUOTENAME(Ingredients.Ingredient_name) 
FROM     Dishes INNER JOIN
                  Dish_Composition ON Dishes.ID = Dish_Composition.ID_Dish INNER JOIN
                  Ingredients ON Dish_Composition.ID_Ingredient = Ingredients.ID
WHERE (Dishes.Dish_name = 'Кропкакор');
--Смотрим результат
SELECT @Ingredients AS TextProduct