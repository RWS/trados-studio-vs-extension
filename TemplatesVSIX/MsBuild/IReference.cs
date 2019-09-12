namespace TemplatesVSIX.MsBuild
{
    internal interface IReference
    {
        string HintPath { get; set; }

        Include Include { get; set; }
    }
}
