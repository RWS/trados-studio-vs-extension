using System.Threading.Tasks;

namespace TemplatesVSIX.MsBuild
{
    interface ISalvageable
    {
        Task<bool> SaveAsync(string path);
    }
}
