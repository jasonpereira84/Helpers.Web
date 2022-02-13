using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Web
        {
            public static Boolean IsBuildConfiguration(this CompilationProperties compilationProperties, String configurationName, Boolean ignoreCase = false)
                => String.Compare(compilationProperties.BUILD_CONFIGURATION, configurationName, ignoreCase).Equals(0);

            public static Boolean IsBuildConfiguration(this CompilationProperties compilationProperties, BuildConfiguration buildConfiguration, Boolean ignoreCase = false)
                => IsBuildConfiguration(compilationProperties, $"{buildConfiguration}", ignoreCase);
        }
    }
}
