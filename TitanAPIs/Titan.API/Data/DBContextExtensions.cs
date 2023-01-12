using Microsoft.EntityFrameworkCore;
using System;

namespace Titan.API
{
    public static class DBContextExtensions
    {
        public static void PrintChanges<T>(this T context)
            where T : DbContext
        {
            Console.WriteLine(context.ChangeTracker.DebugView.LongView);
        }
    }
}
