using System;
using System.Collections.Generic;
using System.Linq;

namespace vivatest.models
{
    public static class TypeExtensions
    {
        public static List<string> GetConstValues(this Type type)
        {
            var fields = type.GetFields();
            if (!fields.Any()) return null;
            var names = fields.Select(x => x.GetValue(null).ToString().Trim()).ToList();
            if (!names.Any()) return null;
            return names;
        }
    }
}
