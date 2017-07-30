using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;

namespace WinstonChurchill.Backend.Business
{
    public class ParceiroNegocioUsuarioBusiness
    {
        public static ParceiroNegocioUsuarioBusiness New
        {
            get
            {
                return new ParceiroNegocioUsuarioBusiness();
            }
        }

        public void Salvar(ParceiroNegocio entidade, Usuario usuario, UnitOfWork uow)
        {
            List<ParceiroNegocioUsuario> lst = entidade.Usuarios;
            int parceiroId = entidade.ID;

            List<ParceiroNegocioUsuario> lstSalva = uow.ParceiroNegocioUsuarioRepository.Listar(p => p.ParceiroNegocioID == parceiroId);
            if (lst == null || lst.Count == 0)
                Excluir(uow, lstSalva);
            else
            {
                List<ParceiroNegocioUsuario> lstExcluir = lstSalva.Where(w => !lst.Any(a => a.UsuarioID == w.UsuarioID)).ToList();
                Excluir(uow, lstExcluir);
                Inserir(uow, lstSalva, lst, parceiroId, usuario.ID);
            }
        }

        private void Inserir(UnitOfWork uow, List<ParceiroNegocioUsuario> lstSalva, List<ParceiroNegocioUsuario> lstSalvar, int parceiroId, int usuarioID)
        {
            if (lstSalvar != null && lstSalvar.Count > 0)
            {
                foreach (var itemSalvar in lstSalvar)
                {
                    ParceiroNegocioUsuario itemSalvo = lstSalva.FirstOrDefault(f => f.UsuarioID == itemSalvar.UsuarioID);
                    if (itemSalvo == null)
                    {
                        itemSalvar.ParceiroNegocioID = parceiroId;
                        itemSalvar.Usuario = null;
                        itemSalvar.CriadorID = usuarioID;
                        itemSalvar.DataCadastro = DateTime.Now;
                        uow.ParceiroNegocioUsuarioRepository.Inserir(itemSalvar);
                    }
                }
            }
        }

        private static void Excluir(UnitOfWork uow, List<ParceiroNegocioUsuario> lstExcluir)
        {
            if (lstExcluir != null && lstExcluir.Count > 0)
            {
                foreach (var itemExcluir in lstExcluir)
                    uow.ParceiroNegocioUsuarioRepository.Excluir(itemExcluir);
            }
        }
    }
}
