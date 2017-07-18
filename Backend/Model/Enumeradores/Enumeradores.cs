using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.Backend.Model.Enumeradores
{
    public enum eTipoParceiro
    {
        Comprador = 1,
        Fornecedor = 2,
        Comprador_Fornecedor = 3
    }

    public enum eTipoGrupoUsuario
    {
        SuperUsuario = 1000,
        Administrador = 1001,
        Fornecedor = 1,
        Comprador = 2,
        RepresentanteComercial = 3
    }
}
