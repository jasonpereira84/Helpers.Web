using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Web
        {
            public static Boolean IsBuildConfiguration(this CompilationProperties compilationProperties, String configurationName, Boolean ignoreCase = false)
                => String.Compare(compilationProperties.BUILD_CONFIGURATION, configurationName, ignoreCase).Equals(0);

            public static Boolean IsReleaseBuild(this CompilationProperties compilationProperties, Boolean ignoreCase = false)
                => IsBuildConfiguration(compilationProperties, $"{BuildConfiguration.Release}", ignoreCase);

            public static Boolean IsDebugBuild(this CompilationProperties compilationProperties, Boolean ignoreCase = false)
                => IsBuildConfiguration(compilationProperties, $"{BuildConfiguration.Debug}", ignoreCase);
        }
    }
}
