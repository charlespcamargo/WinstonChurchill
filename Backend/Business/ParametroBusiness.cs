using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WinstonChurchill.Backend.Model;
using WinstonChurchill.Backend.Repository;
using WinstonChurchill.Backend.Utils;

namespace WinstonChurchill.Backend.Business
{
    public class ParametroBusiness
    {
        Expression<Func<Parametro, bool>> predicate;

        public static ParametroBusiness New
        {
            get
            {
                return new ParametroBusiness();
            }
        }

        public Parametro Salvar(Parametro entidade, Usuario usuario)
        {
            if (entidade == null)
                throw new ArgumentException("Erro: nenhuma informação foi gerada");

            using (UnitOfWork uow = new UnitOfWork())
            {
                if (entidade.ID == 0)
                {
                    entidade.UsuarioID = usuario.ID;
                    uow.ParametroRepository.Inserir(entidade);
                }
                else
                {
                    Parametro objSalvo = uow.ParametroRepository.Carregar(p => p.ID == entidade.ID, ord => ord.OrderBy(p => p.ID));
                    if (objSalvo != null)
                    {
                        objSalvo.DiasCadaRodada = entidade.DiasCadaRodada;
                        objSalvo.LimiteCancelCompra = entidade.LimiteCancelCompra;
                        objSalvo.MargemGarantiaPreco = entidade.MargemGarantiaPreco;
                        objSalvo.PercLucroEmpresa = entidade.PercLucroEmpresa;
                        objSalvo.PercLucroRepComercial = entidade.PercLucroRepComercial;
                        objSalvo.RodasLeilao = entidade.RodasLeilao;
                        objSalvo.SegundaMargemGarantiaPreco = entidade.SegundaMargemGarantiaPreco;
                        objSalvo.UsuarioID = usuario.ID;
                        uow.ParametroRepository.Alterar(objSalvo);
                    }
                }

                uow.Save();
                return entidade;
            }
        }


        public Parametro Carregar(int usuarioID)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Parametro objeto = uow.ParametroRepository.Carregar(p=>p.UsuarioID == usuarioID, ord => ord.OrderBy(p => p.ID));
                return objeto;
            }
        }

        public void Excluir(Parametro filtro)
        {
            if (filtro == null)
                throw new ArgumentException("Informe o Parâmetro para realizar a exclusão");

            using (UnitOfWork uow = new UnitOfWork())
            {
                Parametro ParametroExcluir = uow.ParametroRepository.Carregar(predicate, ord => ord.OrderBy(p => p.ID));
                if (ParametroExcluir == null)
                    throw new ArgumentException("Nenhum Parâmetro encontrado");

                uow.ParametroRepository.Excluir(ParametroExcluir);
                uow.Save();
            }
        }
    }
}
