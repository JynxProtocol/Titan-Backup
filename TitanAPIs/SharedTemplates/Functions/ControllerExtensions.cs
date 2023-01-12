﻿#if NETFRAMEWORK
using System.Web.Mvc;
#else
using Microsoft.AspNetCore.Mvc;
#endif
using System;

public static class ControllerExtensions
{
    public static string ControllerName(this Type controllerType)
    {
        Type baseType = typeof(Controller);
        if (baseType.IsAssignableFrom(controllerType))
        {
            int lastControllerIndex = controllerType.Name.LastIndexOf("Controller");
            if (lastControllerIndex > 0)
            {
                return controllerType.Name.Substring(0, lastControllerIndex);
            }
        }

        return controllerType.Name;
    }
}
