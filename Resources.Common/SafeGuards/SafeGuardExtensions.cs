namespace Resources.Common.SafeGuards
{
    public static class SafeGuardExtensions
    {
        public static void Null(this ISafeGuard safeGuardClause, object input, string parameterName)
        {
            if (null == input)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
