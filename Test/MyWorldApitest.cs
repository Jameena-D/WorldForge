using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Controllers;
using RestApi.Data;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Test
{
    public class MyWorldApitest
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetMyWorlds_ReturnsOnlyWorldsFromGivenUser()
        {
            // Arrange
            using var context = CreateContext();

            context.Worlds.AddRange(
                new World
                {
                    Id = 1,
                    Name = "World user 1",
                    Description = "Test",
                    UserId = "user-1",
                    IsPublic = false,
                    CreatedAt = DateTime.UtcNow
                },
                new World
                {
                    Id = 2,
                    Name = "Second world user 1",
                    Description = "Test 2",
                    UserId = "user-1",
                    IsPublic = true,
                    CreatedAt = DateTime.UtcNow
                },
                new World
                {
                    Id = 3,
                    Name = "World user 2",
                    Description = "Test 3",
                    UserId = "user-2",
                    IsPublic = false,
                    CreatedAt = DateTime.UtcNow
                }
            );

            await context.SaveChangesAsync();

            var controller = new WorldApiController(context);

            // Act
            var result = await controller.GetMyWorlds("user-1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            var json = JsonSerializer.Serialize(okResult.Value);

            var worlds = JsonSerializer.Deserialize<List<MyWorldTestDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            Assert.NotNull(worlds);
            Assert.Equal(2, worlds!.Count);

            Assert.Contains(worlds, w => w.Name == "World user 1");
            Assert.Contains(worlds, w => w.Name == "Second world user 1");

            Assert.DoesNotContain(worlds, w => w.Name == "World user 2");
        }

        private class MyWorldTestDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public bool IsPublic { get; set; }
        }
    }
}
