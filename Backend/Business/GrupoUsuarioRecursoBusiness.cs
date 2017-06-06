using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.API.Common.Util;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
   public class GrupoUsuarioRecursoBusiness
    {
        public static GrupoUsuarioRecursoBusiness New { get { return new GrupoUsuarioRecursoBusiness(); } }

        public List<GrupoUsuarioRecurso> Carregar() {
            string cacheKey = "Recurso.Cache";
            List<GrupoUsuarioRecurso> recursos = CacheManager<List<GrupoUsuarioRecurso>>.GetCache(cacheKey);

            if (recursos != null && recursos.Any())
                return recursos;

            using (UnitOfWork uow = new UnitOfWork())
            {
                recursos = uow.GrupoUsuarioRecursoRepository.Listar();
                if (recursos != null && recursos.Any())
                    CacheManager<List<GrupoUsuarioRecurso>>.GravarCache(recursos, cacheKey);
            }

            return recursos;
        }
    }
}
