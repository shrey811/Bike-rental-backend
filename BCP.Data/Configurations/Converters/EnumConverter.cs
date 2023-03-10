using BCP.Core.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BCP.Infrastructure.Configurations.Converters;

public class EnumConverter<T> : ValueConverter<T,string> where T : BaseEnum
{
    public EnumConverter():base(e=>e.ToString(),s=>BaseEnum.GetByString<T>(s))
    {
        
    }
}