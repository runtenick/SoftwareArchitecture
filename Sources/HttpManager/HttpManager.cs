using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpManager
{
    public partial class HttpManager : IDataManager
    {
        public HttpManager(HttpClient httpClient)
        {
            HttpClient = httpClient;
            ChampionsMgr = new ChampionMgr(this);
        }

        public IChampionsManager ChampionsMgr { get; }

        public ISkinsManager SkinsMgr { get; }

        public IRunesManager RunesMgr { get; }

        public IRunePagesManager RunePagesMgr { get; }

        protected HttpClient HttpClient { get; set; }
    }
}

