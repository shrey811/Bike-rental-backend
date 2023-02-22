using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BCP.Core.Enums;

public class BaseEnum
{
    public int Id { get; }
    private readonly string _value;

    protected BaseEnum(int id, string value)
    {
        Id = id;
        _value = value;
    }

    private static IEnumerable<T> GetAll<T>() where T : BaseEnum
    {
        return typeof(T).GetFields(BindingFlags.Public |
                                   BindingFlags.Static |
                                   BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    public static T GetByString<T>(string name) where T : BaseEnum
    {
        return GetAll<T>().SingleOrDefault(x => x.ToString().Equals(name)) ?? throw new InvalidOperationException($"Cannot convert the string into {typeof(T).Name}");
    }

    public int CompareTo(BaseEnum? other)
    {
        return other == null ? 1 : Id.CompareTo(other.Id);
    }
    public override string ToString()
    {
        return _value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseEnum otherValue || obj == null)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }


    public override int GetHashCode()
    {
        return HashCode.Combine(_value, Id);
    }

    public static implicit operator string(BaseEnum eEnum)
    {
        return eEnum.ToString();
    }
}