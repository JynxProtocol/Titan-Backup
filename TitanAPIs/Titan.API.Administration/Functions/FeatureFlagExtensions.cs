using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Titan.API.Administration;
using Titan.Models;

namespace Titan.Functions
{
    public static class FeatureFlagExtensions
    {
        /// <summary>
        /// This function is used to determine whether to run a feature, 
        /// according to a feature flag. 
        /// </summary>
        /// <param name="Features"></param>
        /// <param name="name">The name of the feature we want to run</param>
        public static bool ShouldRunFeature(this DbSet<FeatureFlag> Features, string name)
        {
            var Status = Features.AsNoTracking().Single(f => f.FeatureName == name).Status;

            return Status switch
            {
                0 => false,
                1 => true,
                2 => Startup.HttpContextAccessor.HttpContext?.User?.Identity?.Name == "Admin",
                _ => throw new NotImplementedException(),
            };
        }
    }
}