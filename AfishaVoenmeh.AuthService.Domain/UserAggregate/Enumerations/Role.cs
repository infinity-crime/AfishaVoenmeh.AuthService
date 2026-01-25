using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;

public sealed class Role : Enumeration
{
    public static readonly Role Student = new(1, "Student");

    public static readonly Role Lecturer = new(2, "Lecturer");

    public static readonly Role Inspector = new(3, "Inspector");

    public static readonly Role Administrator = new(4, "Administrator");

    public Role(int id, string name) : base(id, name) { }

    protected Role() : base() { } // EF Core

    public static Role CreateFromId(int id)
    {
        var role = GetAll<Role>()
            .Where(r => r.Id == id)
            .FirstOrDefault();

        // TODO: ErrorOr pattern implementation
        if (role is null)
            throw new ArgumentException($"Role with Id {id} not found.", nameof(id));

        return role;
    }
}