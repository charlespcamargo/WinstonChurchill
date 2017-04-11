using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonChurchill.API.Common.Zip
{
    public class CompressionUtility
    {
        public static MemoryStream Compress(List<FileZip> lista)
        {
            MemoryStream retorno = new MemoryStream();

            using (ZipFile zip = new ZipFile())
            {
                foreach (var item in lista)
                {
                    zip.AddEntry(item.Nome, item.Arquivo);
                }

                zip.Save(retorno);
                retorno.Seek(0, 0);
            }
            return retorno;
        }

        public static void ExtrairTudo(string path, string fileName)
        {
            string fullPath = Path.Combine(path, fileName);

            ZipFile zip = ZipFile.Read(fullPath);
            foreach (ZipEntry e in zip)
            {
                e.Extract(path, ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }

    public class FileZip
    {
        public decimal Chave { get; set; }
        public string Nome { get; set; }
        public byte[] Arquivo { get; set; }
    }

}
