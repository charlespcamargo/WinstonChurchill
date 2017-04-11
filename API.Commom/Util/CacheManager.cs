using System;
using System.Web;

namespace WinstonChurchill.API.Common.Util
{

    //Classe de gerenciamento de caches
    public class CacheManager<T>
    {
        //public static CachingManager<T> Instance { get { return new CachingManager<T>(); } }

        public static void GravarCache(T obj, string idx, bool replace = false)
        {
            if (replace)
                ExcluirCache(idx);

            HttpRuntime.Cache.Insert(idx, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(1));
        }

        public static T GetCache(string idx, bool limparCache = false)
        {
            T oCache = (T)HttpRuntime.Cache.Get(idx);

            if (limparCache)
                ExcluirCache(idx);

            return oCache;
        }

        public static void ExcluirCache(string idx)
        {
            if (HttpRuntime.Cache.Get(idx) != null)
            {
                HttpRuntime.Cache.Remove(idx.ToString());
            }
        }

    }
}
