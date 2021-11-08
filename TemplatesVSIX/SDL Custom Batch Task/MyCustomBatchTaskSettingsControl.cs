using Sdl.Desktop.IntegrationApi;
using Sdl.Desktop.IntegrationApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace $safeprojectname$
{
    class MyCustomBatchTaskSettingsControl : UserControl, IUISettingsControl, ISettingsAware<MyCustomBatchTaskSettings>
    {
        public MyCustomBatchTaskSettings Settings
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
