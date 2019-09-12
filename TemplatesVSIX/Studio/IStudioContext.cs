using System.Collections.Generic;

namespace TemplatesVSIX.Studio
{
    interface IStudioContext
    {
        IEnumerable<string> GetSelectedFileNames();

        void SetStatusBarText(string text);

        void WriteToOutput(string text);
    }
}
