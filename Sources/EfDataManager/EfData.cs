using EF_Champions;
using Model;

namespace EfDataManager
{
    public partial class EfData : IDataManager
    {
        public EfData(ChampDbContext dbContext) 
        { 
            DbContext = dbContext;
            ChampionsMgr = new EfChampionsManager(this);
            SkinsMgr = new EfSkinsManager(this);
            RunesMgr = new EfRunesManager(this);
            RunePagesMgr = new EfRunePagesManager(this);
        }

        public IChampionsManager ChampionsMgr { get; }

        public ISkinsManager SkinsMgr { get; }

        public IRunesManager RunesMgr { get; }

        public IRunePagesManager RunePagesMgr { get; }

        protected ChampDbContext DbContext { get; set; }
    }
}