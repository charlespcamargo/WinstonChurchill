using System.Collections.Generic;
using System.IO;
using WinstonChurchill.API.Common.Util.Tools;
using WinstonChurchill.Backend.Model.Interfaces;
using WinstonChurchill.Backend.Utils;

namespace WinstonChurchill.Backend.Business
{
    public class AnexoBusiness
    {
        public static AnexoBusiness New { get { return new AnexoBusiness(); } }

        public void GravarArquivoFisico<T>(T anexo, byte[] bytes) where T : IAnexo
        {
            string fullPath = ObterDiretorio<T>(anexo);
            File.WriteAllBytes(fullPath, bytes);
        }

        public void GravarArquivoFisico<T>(List<T> lista, byte[] bytes) where T : IAnexo
        {
            if (lista != null)
            {
                foreach (var anexo in lista)
                {
                    string fullPath = ObterDiretorio<T>(anexo);
                    File.WriteAllBytes(fullPath, bytes);
                }
            }
        }

        public void ExcluirArquivoFisico<T>(T anexo) where T : IAnexo
        {
            string fullPath = ObterDiretorio<T>(anexo);
            File.Delete(fullPath);
        }

        public void ExcluirArquivoFisico<T>(List<T> lista) where T : IAnexo
        {
            if (lista != null)
            {
                foreach (var anexo in lista)
                {
                    string fullPath = ObterDiretorio<T>(anexo);
                    File.Delete(fullPath);
                }
            }
        }

        public string ObterDiretorio<T>(T anexo) where T : IAnexo
        {
            return Path.Combine(ConfigurarDiretorio(anexo.DiretorioFisico), anexo.NomeArquivo);
        }

        private string ConfigurarDiretorio(string diretorio)
        {
            string path = Path.Combine(Configuracoes.PathAnexos, Formatar.RetirarCaracteresEspeciaisNada(diretorio));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
