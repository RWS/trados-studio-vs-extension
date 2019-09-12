using System.IO;
using System.Threading.Tasks;

namespace TemplatesVSIX.MsBuild
{
    internal class Backup : ISalvageable
    {
        private readonly ISalvageable _salvageable;

        public Backup(ISalvageable salvageable)
        {
            _salvageable = salvageable;
        }

        public async Task<bool> SaveAsync(string path)
        {
            await BackupOriginal(path);
            return await _salvageable?.SaveAsync(path);
        }

        private async Task BackupOriginal(string path)
        {
            var destinationPath = path + ".bkp";
            using (var source = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }
    }
}