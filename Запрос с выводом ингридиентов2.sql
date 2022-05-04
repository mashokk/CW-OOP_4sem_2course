use CW_Recipes;

SELECT DISTINCT d.Dish_name,
(SELECT distinct Ingredient_name + CHAR(10) FROM  Ingredients p
WHERE  p.ID in (select n.ID_Ingredient from Dish_Composition n where n.ID_Dish = d.ID) 
FOR xml path('')
) 'Ингридиенты'
from Dishes d
inner join Dish_Composition n on n.ID_Dish = d.ID