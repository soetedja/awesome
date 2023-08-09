using System;
using System.Collections.Generic;
using System.Text;

namespace Awesome.UnitTest.Infrastructures
{
    public static class ExtensionHelper
    {
        public static string ToStringDateTest(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
