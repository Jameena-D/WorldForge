using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Controllers;
using RestApi.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class PublicWorldApi
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetPublicWorldDetail_ReturnsNotFound_WhenWorldDoesNotExist()
        {
            // Arrange
            using var context = CreateContext();

            var world = new RestApi.Models.World
            {
                Id = 1,
                Name = "Private world",
                Description = "Deze wereld is privé",
                IsPublic = false,
                CreatedAt = DateTime.UtcNow,
                UserId = "user-1"
            };

            context.Worlds.Add(world);
            await context.SaveChangesAsync();

            var controller = new PublicWorldsApiController(context);

            // Act
            var result = await controller.GetPublicWorldDetail(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
