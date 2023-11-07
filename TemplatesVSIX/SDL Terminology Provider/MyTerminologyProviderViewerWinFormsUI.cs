using Sdl.Terminology.TerminologyProvider.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sdl.Core.Globalization;
using System.Windows.Forms;

namespace $safeprojectname$
{
    [TerminologyProviderViewerWinFormsUI]
    class MyTerminologyProviderViewerWinFormsUI : ITerminologyProviderViewerWinFormsUI
    {
        public Control Control
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Initialized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Entry SelectedTerm
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

        public bool CanAddTerm => throw new NotImplementedException();

        public bool IsEditing => throw new NotImplementedException();

        public event EventHandler<EntryEventArgs> SelectedTermChanged;
        public event EventHandler TermChanged;

        public void AddAndEditTerm(Entry term, string source, string target)
        {
            throw new NotImplementedException();
        }

        public void AddTerm(string source, string target)
        {
            throw new NotImplementedException();
        }

        public void CancelTerm()
        {
            throw new NotImplementedException();
        }

        public void EditTerm(Entry term)
        {
            throw new NotImplementedException();
        }

        public void Initialize(ITerminologyProvider terminologyProvider, CultureCode source, CultureCode target)
        {
            throw new NotImplementedException();
        }

        public void JumpToTerm(Entry entry)
        {
            throw new NotImplementedException();
        }

        public void Release()
        {
            throw new NotImplementedException();
        }

        public void SaveTerm()
        {
            throw new NotImplementedException();
        }

        public bool SupportsTerminologyProviderUri(Uri terminologyProviderUri)
        {
            throw new NotImplementedException();
        }
    }
}
