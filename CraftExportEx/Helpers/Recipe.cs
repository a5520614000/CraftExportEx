using CraftExportEx;

namespace CraftExport.Helpers
{
    public class Recipe
    {
        /// <summary>
        /// The Id of the recipe.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ItemId when the recipe is crafted.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The class linked to the recipe.
        /// </summary>
        public ClassJobType ClassJobType { get; set; }

        /// <summary>
        /// The name of the recipe.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The level of the recipe.
        /// </summary>
        public int Level { get; set; }
    }
}
