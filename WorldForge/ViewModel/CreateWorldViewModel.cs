using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WorldForge.Models;
using Shared.Enum;

namespace WorldForge.ViewModel
{
    public class CreateWorldViewModel
    {
        //T3 : Maxlength and required validation for the name and description fields, and required validation for the world type field. This ensures that the data submitted by the user meets the necessary criteria before being processed.
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"[\s\S]*\S[\s\S]*", ErrorMessage = "Name cannot be blank.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [RegularExpression(@"[\s\S]*\S[\s\S]*", ErrorMessage = "Description cannot be blank.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "WorldType is required.")]
        public WorldTypeEnum? WorldType { get; set; }

        public bool IsPublic { get; set; }

        public List<WorldSectionInputViewModel> Sections { get; set; } = new List<WorldSectionInputViewModel>();
    }

    public class WorldSectionInputViewModel
    {
        public string Title { get; set; } = string.Empty;

        public List<WorldSectionBlockInputViewModel> Blocks { get; set; } = new List<WorldSectionBlockInputViewModel>();
    }

    public class WorldSectionBlockInputViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}