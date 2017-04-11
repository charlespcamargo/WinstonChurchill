using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Core;

namespace WinstonChurchill.Backend.Repository
{
    public class BaseRepository<T> where T : class
    {
        internal DbContext context;
        internal DbSet<T> dbSet;

        public BaseRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();

        }

        public virtual List<T> Listar(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", string includePropertiesLeft = "", int pagina = -1, int qtdRegistros = -1)
        {
            try
            {

                pagina = CalculaPaginacao(pagina, qtdRegistros);

                List<T> retorno = null;
                IQueryable<T> query = dbSet;

                query = AplicaFiltro(filter, query);
                query = IncluiPropriedades(includeProperties, includePropertiesLeft, query);

                //if (includeProperties != null)
                //Expression<Func<T, object>>[] includeProperties = null//parametro
                //query = includeProperties.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(query, (current, expression) => current.Include(expression));//include

                if (orderBy != null)
                {
                    if (pagina > -1 && qtdRegistros > -1)
                        retorno = orderBy(query).Take(qtdRegistros).AsNoTracking().ToList();
                    else
                        retorno = orderBy(query).AsNoTracking().ToList();
                }
                else
                {
                    if (pagina > -1 && qtdRegistros > -1)
                    {
                        if (orderBy == null)
                            throw new EntityException("Ao informar uma paginação a opção OrderBy é obrigatória");
                        else
                            retorno = query.Skip(pagina).Take(qtdRegistros).AsNoTracking().ToList();
                    }
                    else
                        retorno = query.AsNoTracking().ToList();
                }
                 

                return retorno;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new Exception(ex.InnerException.Message);
                }
                throw ex;
            }

        }

        private IQueryable<T> IncluiPropriedades(string includeProperties, string includePropertiesLeft, IQueryable<T> query)
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(','))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (!string.IsNullOrEmpty(includePropertiesLeft))
            {
                foreach (var includeProperty in includePropertiesLeft.Split(','))
                {
                    query = query.Include(includeProperty).DefaultIfEmpty();
                }
            }

            return query;
        }

        private IQueryable<T> AplicaFiltro(Expression<Func<T, bool>> filter, IQueryable<T> query)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query;
        }

        private int CalculaPaginacao(int pagina, int qtdRegistros)
        {
            if (pagina > -1 && qtdRegistros > -1)//redefine a página
            {
                pagina = (pagina - 1) * qtdRegistros;
            }
            return pagina;
        }

        public virtual T Carregar(Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            return this.Listar(filter, orderBy, includeProperties, "", 1, 1).FirstOrDefault();
        }

        public int Contar(Expression<Func<T, bool>> predicate)
        {
            int contar = dbSet.Count(predicate);
            return contar;
        }
        


        public virtual void Inserir(T entity)
        {
            if (entity != null)
                entity = dbSet.Add(entity);
        }

        public virtual void Inserir(List<T> entities)
        {
            //context.BulkInsert(entities);
        }

        public virtual void Excluir(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Excluir(entityToDelete);
        }

        public virtual void Excluir(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);
        }

        public virtual void Alterar(T entityToUpdate)
        {
            if (entityToUpdate != null)
            {
                var entry = context.Entry<T>(entityToUpdate);
                if (entry.State == EntityState.Detached)
                {
                    dbSet.Attach(entityToUpdate);
                    context.Entry(entityToUpdate).State = EntityState.Modified;
                }
            }
        }

        public virtual void Alterar(T entity, string nomeChave)
        {
            if (entity != null)
            {
                var entry = context.Entry<T>(entity);
                if (entry != null)
                {
                    object pkId = dbSet.Create().GetType().GetProperty(nomeChave).GetValue(entity);
                    if (pkId != null)
                    {
                        if (entry.State == EntityState.Detached)
                        {
                            var set = context.Set<T>();
                            T attachedEntity = set.Find(pkId);  // access the key
                            if (attachedEntity != null)
                            {
                                var attachedEntry = context.Entry(attachedEntity);
                                attachedEntry.CurrentValues.SetValues(entity);
                            }
                            else
                            {
                                entry.State = EntityState.Modified; // attach the entity
                            }
                        }
                    }
                }
            }
        }


    }
}
