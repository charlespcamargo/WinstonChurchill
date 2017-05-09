using WinstonChurchill.Backend.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Web;
using MySql.Data.MySqlClient;

namespace WinstonChurchill.Backend.Repository
{
    public class ConfigModel
    {

        public static DbConnection Conexao
        {
            get
            {
                string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["contexto"].ConnectionString;

                DbConnection conn;

                if (System.Configuration.ConfigurationManager.ConnectionStrings["contexto"].ProviderName == "MySql.Data.MySqlClient") 
                    conn = new MySqlConnection(strConnection); 
                else                  
                    conn = new SqlConnection(strConnection); 

                return conn;
            }
        }

        public static DbCompiledModel CompileModel
        {
            get
            {
                DbCompiledModel compilado = HttpRuntime.Cache.Get("CacheModelCompiler") as DbCompiledModel;
                if (compilado == null)
                {
                    var builder = CompilaModel();

                    DbModel model = builder.Build(Conexao);
                    DbCompiledModel compliedModel = model.Compile();

                    HttpRuntime.Cache.Insert("CacheModelCompiler", compliedModel);

                    return compliedModel;
                }
                else
                {
                    return compilado;
                }
            }
        }

        private static System.Data.Entity.DbModelBuilder CompilaModel()
        {
            var builder = new System.Data.Entity.DbModelBuilder();

            builder.Configurations.Add(new EntityTypeConfiguration<Usuario>());
            builder.Configurations.Add(new EntityTypeConfiguration<GrupoUsuario>());
            builder.Configurations.Add(new EntityTypeConfiguration<UsuarioXGrupoUsuario>());

            builder.Configurations.Add(new EntityTypeConfiguration<Produto>());
            builder.Configurations.Add(new EntityTypeConfiguration<Categoria>());
            builder.Configurations.Add(new EntityTypeConfiguration<CategoriaProduto>());
            builder.Configurations.Add(new EntityTypeConfiguration<CategoriaImagem>());
            builder.Configurations.Add(new EntityTypeConfiguration<ProdutoImagem>());
            builder.Configurations.Add(new EntityTypeConfiguration<Imagem>());
            builder.Configurations.Add(new EntityTypeConfiguration<CaracteristicaProduto>());
            builder.Configurations.Add(new EntityTypeConfiguration<Parametro>());

            return builder;
        }


    }
}
