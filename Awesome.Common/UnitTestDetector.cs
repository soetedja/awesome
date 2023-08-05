using System.Reflection;

namespace Awesome.Common
{
    public static class UnitTestDetector
    {
        private static bool _isRunningFromXUnit = false;

        public static bool IsRunningFromXUnit()
        {
            if (!_isRunningFromXUnit)
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!string.IsNullOrEmpty(assembly.FullName) && assembly.FullName.ToLowerInvariant().StartsWith("xunit."))
                    {
                        _isRunningFromXUnit = true;
                        break;
                    }
                }
            }

            return _isRunningFromXUnit;
        }
    }
}
