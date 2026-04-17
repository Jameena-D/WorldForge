using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WorldForge.Models;

namespace WorldForge.ViewModel
{
    public class CreateWorldViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
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