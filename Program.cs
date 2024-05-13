using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG6221
{
    // Creating enum for food groups
    public enum FoodGroup
    {
        
        Protein,Grain,Vegetable,Fruit,Fat,Sugar,Dairy,
        Other
    }

    // Creating class for storing the ingredients
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public FoodGroup Group { get; set; }
        public int Calories { get; set; }

        public Ingredient(string name, double quantity, FoodGroup group, int calories)
        {
            Name = name;
            Quantity = quantity;
            Group = group;
            Calories = calories;
        }

        public override string ToString()
        {
            return $"{Quantity} of {Name}";
        }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
        }

        public void AddIngredient(string name, double quantity, FoodGroup group, int calories)
        {
            Ingredients.Add(new Ingredient(name, quantity, group, calories));
        }

        public int TotalCalories()
        {
            return Ingredients.Sum(ingredient => ingredient.Calories);
        }

        public override string ToString()
        {
            string recipeString = $"Recipe: {Name}\nIngredients:\n";

            foreach (Ingredient ingredient in Ingredients)
            {
                recipeString += $"{ingredient}\n";
            }

            return recipeString;
        }
    }

    class Program
    {
        static List<Recipe> recipes = new List<Recipe>(); // Store recipes in a list

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("Welcome to RecipeWorld");
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. View recipes");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                }

                switch (choice)
                {
                    case 1:
                        AddNewRecipe();
                        break;
                    case 2:
                        ViewRecipes();
                        break;
                    case 3:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please choose a number between 1 and 3.");
                        break;
                }
            }
        }

        static void AddNewRecipe()
        {
            Console.WriteLine("\nEnter the name of the recipe:");
            string recipeName = Console.ReadLine();

            Recipe myRecipe = new Recipe(recipeName);

            bool addingIngredients = true;

            while (addingIngredients)
            {
                Console.WriteLine("Enter the name of the ingredient (or type 'done' to finish adding ingredients):");
                string ingredientName = Console.ReadLine();

                if (ingredientName.ToLower() == "done")
                {
                    addingIngredients = false;
                    break;
                }

                double quantity;
                Console.WriteLine("Enter the quantity:");
                while (!double.TryParse(Console.ReadLine(), out quantity))
                {
                    Console.WriteLine("Invalid input! Please enter a valid quantity:");
                }

                Console.WriteLine("Enter the food group of the ingredient:");
                Console.WriteLine("Choose from: Dairy, Protein, Grain, Vegetable, Fruit, Fat, Sugar, Other");
                FoodGroup group;
                while (!Enum.TryParse(Console.ReadLine(), true, out group))
                {
                    Console.WriteLine("Invalid input! Please enter a valid food group:");
                }

                Console.WriteLine("Enter the number of calories:");
                int calories;
                while (!int.TryParse(Console.ReadLine(), out calories))
                {
                    Console.WriteLine("Invalid input! Please enter a valid number of calories:");
                }

                myRecipe.AddIngredient(ingredientName, quantity, group, calories);
            }

            recipes.Add(myRecipe);
            Console.WriteLine("\nRecipe added successfully!");
        }

        static void ViewRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("\nNo recipes available.");
                return;
            }

            recipes = recipes.OrderBy(recipe => recipe.Name).ToList(); // Sort recipes by name

            Console.WriteLine("\nAvailable recipes:");
            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.WriteLine("\nEnter the number of the recipe you want to view:");
            int recipeNumber;
            while (!int.TryParse(Console.ReadLine(), out recipeNumber) || recipeNumber < 1 || recipeNumber > recipes.Count)
            {
                Console.WriteLine("Invalid input! Please enter a valid recipe number:");
            }

            Recipe selectedRecipe = recipes[recipeNumber - 1];

            Console.WriteLine($"\nRecipe: {selectedRecipe.Name}");
            Console.WriteLine("Ingredients:");
            foreach (Ingredient ingredient in selectedRecipe.Ingredients)
            {
                Console.WriteLine($"- {ingredient}");
            }

            int totalCalories = selectedRecipe.TotalCalories();
            Console.WriteLine($"Total Calories: {totalCalories}");

            if (totalCalories > 300)
            {
                Console.WriteLine("Warning: Total calories exceed 300!");
            }
        }
    }
}
