using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using BC = BCrypt.Net.BCrypt;

namespace MyApi.Services;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // 1. Seed Roles
        if (!await context.Roles.AnyAsync())
        {
            var adminRole = new Role { Name = "Admin" };
            var userRole = new Role { Name = "User" };
            await context.Roles.AddRangeAsync(adminRole, userRole);
            await context.SaveChangesAsync();
        }

        // 2. Seed Organization
        if (!await context.Organizations.AnyAsync())
        {
            var peaDistrict1 = new Organization
            {
                Code = "D1",
                Name = "PEA District 1 (Chiang Mai)",
                Type = "กฟจ."
            };
            await context.Organizations.AddAsync(peaDistrict1);
            await context.SaveChangesAsync();

            var branch1 = new Organization
            {
                Code = "B1",
                Name = "PEA Branch A",
                Type = "กฟฟ.",
                ParentId = peaDistrict1.Id
            };
            var branch2 = new Organization
            {
                Code = "B2",
                Name = "PEA Branch B",
                Type = "กฟฟ.",
                ParentId = peaDistrict1.Id
            };
            await context.Organizations.AddRangeAsync(branch1, branch2);
            await context.SaveChangesAsync();

            // Seed Departments for Branch A
            var dept1 = new Department { Name = "Operations", OrganizationId = branch1.Id };
            var dept2 = new Department { Name = "Maintenance", OrganizationId = branch1.Id };
            await context.Departments.AddRangeAsync(dept1, dept2);
            await context.SaveChangesAsync();
        }

        // 3. Seed Positions
        if (!await context.Positions.AnyAsync())
        {
            await context.Positions.AddRangeAsync(
                new Position { Name = "Manager" },
                new Position { Name = "Engineer" },
                new Position { Name = "Technician" },
                new Position { Name = "Staff" }
            );
            await context.SaveChangesAsync();
        }

        // 4. Seed Super Admin User & Employee
        if (!await context.Users.AnyAsync(u => u.Username == "admin"))
        {
            var org = await context.Organizations.FirstAsync();
            var dept = await context.Departments.FirstAsync();
            var pos = await context.Positions.FirstAsync();

            var employee = new Employee
            {
                EmployeeCode = "999999",
                FirstName = "Super",
                LastName = "Admin",
                Email = "admin@pea.co.th",
                OrganizationId = org.Id,
                DepartmentId = dept.Id,
                PositionId = pos.Id,
                EmployeeType = "Full-time"
            };
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            var adminUser = new User
            {
                Username = "admin",
                PasswordHash = BC.HashPassword("Admin@1234"),
                EmployeeId = employee.Id,
                IsActive = true
            };
            await context.Users.AddAsync(adminUser);
            await context.SaveChangesAsync();
        }
    }
}
