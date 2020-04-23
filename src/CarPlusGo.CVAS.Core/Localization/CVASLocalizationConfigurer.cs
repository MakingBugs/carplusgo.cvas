using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace CarPlusGo.CVAS.Localization
{
    public static class CVASLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CVASConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CVASLocalizationConfigurer).GetAssembly(),
                        "CarPlusGo.CVAS.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
