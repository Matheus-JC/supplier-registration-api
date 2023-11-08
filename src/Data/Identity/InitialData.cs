using Microsoft.AspNetCore.Identity;

namespace SupplierRegServer.Data.Identity;

public class InitialData
{
    public static List<IdentityUser> GetUsers()
    {
        return Enumerable.Concat(GetAdminUsers(), GetNormalUsers()).ToList();
    }

    public static List<IdentityUser> GetAdminUsers()
    {
        var hasher = new PasswordHasher<IdentityUser>();

        var users = new List<IdentityUser>();

        var admin = new IdentityUser
        {
            Id = "c032759e-f7a7-4079-9c29-225bc9211be1",
            UserName = "admin@admin.com",
            NormalizedUserName = "ADMIN@ADMIN.COM",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            LockoutEnabled = false,
            SecurityStamp = new Guid().ToString(),
        };

        admin.PasswordHash = hasher.HashPassword(admin, "Admin@123");

        users.Add(admin);

        return users;
    }

    public static List<IdentityUser> GetNormalUsers()
    {
        var users = new List<IdentityUser>();

        var hasher = new PasswordHasher<IdentityUser>();

        var user = new IdentityUser
        {
            Id = "7693cb32-775f-466f-a24d-4acf77b18398",
            UserName = "testuser@testuser.com",
            NormalizedUserName = "TESTUSER@TESTUSER.COM",
            Email = "testuser@testuser.com",
            NormalizedEmail = "TESTUSER@TESTUSER.COM",
            EmailConfirmed = true,
            LockoutEnabled = false,
            SecurityStamp = new Guid().ToString(),
        };

        user.PasswordHash = hasher.HashPassword(user, "TestUser@123");

        users.Add(user);

        return users;
    }

    public static List<IdentityRole> GetRoles()
    {
        return new List<IdentityRole>
        {
            new() {
                Id = "f0ef553e-63bf-465d-b5f2-8abbe9768692",
                Name = "Admin"
            },
            new() {
                Id = "08ae5d44-fb25-4ec7-b4e2-2aa62a8dfefc",
                Name = "User"
            },
        };
    }

    public static List<IdentityUserClaim<string>> GetUserTestClaims()
    {
        var usersTestClaims = new List<IdentityUserClaim<string>>();

        var userTest = GetUsers().SingleOrDefault(u => u.UserName == "testuser@testuser.com");

        if (userTest != null)
        {
            usersTestClaims.Add(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    UserId = userTest.Id,
                    ClaimType = "Supplier",
                    ClaimValue = "Create,Update"
                }
            );

            usersTestClaims.Add(
                new IdentityUserClaim<string>
                {
                    Id = 2,
                    UserId = userTest.Id,
                    ClaimType = "Product",
                    ClaimValue = "Create,Delete"
                }
            );
        }

        return usersTestClaims;
    }

    public static List<IdentityUserRole<string>> GetUsersRoles()
    {
        var adminsUsersInRole = AssignInRole(GetAdminUsers(), "Admin");
        var normalUsersInRole = AssignInRole(GetNormalUsers(), "User");
        return Enumerable.Concat(adminsUsersInRole, normalUsersInRole).ToList();
    }

    private static List<IdentityUserRole<string>> AssignInRole(List<IdentityUser> users, string roleName)
    {
        var usersInRoles = new List<IdentityUserRole<string>>();

        var role = GetRoles().SingleOrDefault(r => r.Name == roleName);

        if (role != null)
        {
            foreach (var user in users)
            {
                usersInRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            }
        }

        return usersInRoles;
    }
}
