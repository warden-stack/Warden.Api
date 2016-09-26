using System;

namespace Warden.Common.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid target) => target == Guid.Empty;
    }
}