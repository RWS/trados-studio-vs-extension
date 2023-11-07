using Sdl.Terminology.TerminologyProvider.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    class MyTerminologyProvider : ITerminologyProvider
    {
        public Definition Definition
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Uri Uri
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Id => throw new NotImplementedException();

        public TerminologyProviderType Type => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool SearchEnabled => throw new NotImplementedException();

        public FilterDefinition ActiveFilter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsInitialized => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Entry GetEntry(int id)
        {
            throw new NotImplementedException();
        }

        public Entry GetEntry(int id, IEnumerable<ILanguage> languages)
        {
            throw new NotImplementedException();
        }

        public IList<FilterDefinition> GetFilters()
        {
            throw new NotImplementedException();
        }

        public IList<ILanguage> GetLanguages()
        {
            throw new NotImplementedException();
        }

        public bool Initialize()
        {
            throw new NotImplementedException();
        }

        public bool Initialize(TerminologyProviderCredential credential)
        {
            throw new NotImplementedException();
        }

        public bool IsProviderUpToDate()
        {
            throw new NotImplementedException();
        }

        public IList<SearchResult> Search(string text, ILanguage source, ILanguage destination, int maxResultsCount, SearchMode mode, bool targetRequired)
        {
            throw new NotImplementedException();
        }

        public void SetDefault(bool value)
        {
            throw new NotImplementedException();
        }

        public bool Uninitialize()
        {
            throw new NotImplementedException();
        }
    }
}
