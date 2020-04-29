When upgrading from an oldest version of Visual Studio (ex Visual Studio 2017) to a new one (ex: Visual Studio 2019), the next steps should be done:
	1. Create a new source.extension.vsixmanifest file using the new Visual Studio (in case the source.extension.vsixmanifest is copied from the oldest Visual Studio,
the rebuild will crash because of some properties which are not copatible from the old with the new version of Visual Studio)
	2. Update the source.extension.vsixmanifest file with the needed information (by default, it is pre-populated with information related to Visual Studio version)
	3. In case the PluginFramework.Build or PluginFramework versions were changed, then it should be also updated inside each template project
at file SDL Custom Batch Task.vstemplate/SDL Terminology Provider.vstemplate/SDL Trados Studio.vstemplate/SDL Translation Provider.vstemplate,
so the version from TemplatesVSIX\Packages\Sdl.Core.PluginFramework.1.0.0.nupkg and TemplatesVSIX\Packages\Sdl.Core.PluginFramework.Build.16.0.0.nupkg 
should correspond with the ones from template project .vstemplate file