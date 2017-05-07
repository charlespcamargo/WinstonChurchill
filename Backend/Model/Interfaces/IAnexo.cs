using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.Backend.Model.Interfaces
{
    public interface IAnexo
    {
        string NomeArquivo { get; set; }

        string DiretorioFisico { get; set; }

        long TamanhoBytes { get; set; }


        DateTime DataCadastro { get; set; }


        string Tipo { get; set; }
    }
}
