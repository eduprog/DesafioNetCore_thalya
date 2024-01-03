﻿using System.ComponentModel;
using System.Reflection;

namespace Desafio.Domain;

public static class ExtensionHelper
{
    public static string GetEnumDescription<T>(this T value)
    {
        var field = value.GetType().GetField(value.ToString());

        if (field != null)
        {
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            if (attribute != null)
            {
                return attribute.Description;
            }
        }

        return value.ToString();
    }
}
