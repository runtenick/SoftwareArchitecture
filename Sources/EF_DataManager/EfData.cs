using EF_Champions;
using EfDataManagerLib;
using Model;

namespace EF_DataManagerLib
{
    public partial class EfData : IDataManager
    {

        public EfData(ChampDbContext champDbContext) 
        {
            ChampDbContext = champDbContext;
            ChampionsMgr = new EfChampionsManager(this);
        }

        public IChampionsManager ChampionsMgr { get; }

        public ISkinsManager SkinsMgr { get; }

        public IRunesManager RunesMgr { get; }

        public IRunePagesManager RunePagesMgr { get; }

        protected ChampDbContext ChampDbContext { get; }
         
    }
}
