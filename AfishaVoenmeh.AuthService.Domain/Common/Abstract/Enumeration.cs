using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Domain.Common.Abstract;

public abstract class Enumeration : IComparable
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    protected Enumeration() { }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if(obj is not Enumeration other)
            return false;

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMathes = Id.Equals(other.Id);

        return valueMathes && typeMatches;
    }

    public override int GetHashCode() => HashCode.Combine(Id, Name);

    public int CompareTo(object? obj) => Id.CompareTo(((Enumeration)obj!).Id);
}