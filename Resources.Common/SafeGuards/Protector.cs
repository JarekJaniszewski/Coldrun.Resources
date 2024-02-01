namespace Resources.Common.SafeGuards
{
    public class Protector: ISafeGuard
    {
        public static ISafeGuard Against { get; } = new Protector();
        private Protector() { }
    }
}
