using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace WinstonChurchill.API.Common.Mapeador
{
    using Reflector;
    public class EDIRefletor
    {
        // Fields
        private Type T;

        // Methods
        public EDIRefletor(Type T)
        {
            this.T = T;
        }

        public E Atributo<E>(MemberInfo pi)
        {
            E[] localArray = this.Atributos<E>(pi);
            if (localArray.Length > 0)
            {
                return localArray[0];
            }
            return default(E);
        }

        public E[] Atributos<E>(MemberInfo pi)
        {
            return (pi.GetCustomAttributes(typeof(E), false) as E[]);
        }

        public PropertyInfo[] Propriedades
        {
            get
            {
                List<PropertyInfo> listaPropriedades = new List<PropertyInfo>();
                BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

                listaPropriedades.AddRange(this.T.GetProperties(bindingAttr).ToList());

                return listaPropriedades.Distinct(new ComparePropertyInfo()).ToArray();
            }
        }

        public T MapeamentoEDI<T>(string linhaUnica, T retorno)
        {
            if (retorno == null)
                retorno = Activator.CreateInstance<T>();

            foreach (PropertyInfo info in this.Propriedades)
            {
                EDIMapper atributo = this.Atributo<EDIMapper>(info);
                if (atributo != null)
                {
                    int linhaIndice = (atributo.NumeroLinha > 0 ? atributo.NumeroLinha - 1 : atributo.NumeroLinha);
                    int posicaoInicial = atributo.PosicaoInicial;
                    int posicaoFinal = atributo.PosicaoFinal;
                    int qtdCaracteres = atributo.QtdCaracteres;
                    if (qtdCaracteres == 0 && posicaoFinal > 0)
                        qtdCaracteres = (posicaoFinal - posicaoInicial) + 1;

                    int verificaTamanhoLinha = (posicaoInicial - 1) + qtdCaracteres;
                    //string linhaUnica = linhas[linhaIndice];

                    object valorPropriedade;
                    if (verificaTamanhoLinha <= linhaUnica.Length)
                        valorPropriedade = linhaUnica.Substring(posicaoInicial - 1, qtdCaracteres);
                    else
                    {
                        try
                        {
                            valorPropriedade = linhaUnica.Substring(posicaoInicial - 1);
                        }
                        catch
                        {
                            valorPropriedade = "".PadLeft(qtdCaracteres, '#');
                        }
                    }

                    if (info.PropertyType == typeof(string))
                    {
                        if (valorPropriedade.ToString().Length < qtdCaracteres)
                            valorPropriedade = valorPropriedade.ToString().PadLeft(qtdCaracteres, '#').PadRight(qtdCaracteres, '#');
                    }

                    info.SetValue(retorno, GetConvertedObject(info.PropertyType, valorPropriedade), null);
                }
            }

            return retorno;
        }


        public string EscreveLinhaEDI<T>(T entidade)
        {
            int totalCaracteresLInha = 0;
            foreach (PropertyInfo info in this.Propriedades)
            {
                EDIMapper atributo = this.Atributo<EDIMapper>(info);
                int linhaIndice = (atributo.NumeroLinha > 0 ? atributo.NumeroLinha - 1 : atributo.NumeroLinha);
                int posicaoInicial = atributo.PosicaoInicial;
                int posicaoFinal = atributo.PosicaoFinal;
                int qtdCaracteres = atributo.QtdCaracteres;
                if (qtdCaracteres == 0 && posicaoFinal > 0)
                    qtdCaracteres = (posicaoFinal - posicaoInicial) + 1;
                totalCaracteresLInha = totalCaracteresLInha + qtdCaracteres;
            }

            string linha = "".PadRight(totalCaracteresLInha, ' ');
            foreach (PropertyInfo info in this.Propriedades)
            {
                EDIMapper atributo = this.Atributo<EDIMapper>(info);
                if (atributo != null)
                {
                    int linhaIndice = (atributo.NumeroLinha > 0 ? atributo.NumeroLinha - 1 : atributo.NumeroLinha);
                    int posicaoInicial = atributo.PosicaoInicial;
                    int posicaoFinal = atributo.PosicaoFinal;
                    int qtdCaracteres = atributo.QtdCaracteres;
                    if (qtdCaracteres == 0 && posicaoFinal > 0)
                        qtdCaracteres = (posicaoFinal - posicaoInicial) + 1;

                    int verificaTamanhoLinha = (posicaoInicial - 1) + qtdCaracteres;

                    object valor = info.GetValue(entidade, null);
                    string valorAdd = "";
                    if (valor != null)
                    {
                        if (valor.ToString().Length < qtdCaracteres)
                        {
                            if (info.PropertyType == typeof(string))
                            {
                                valorAdd = valor.ToString().PadRight(qtdCaracteres, ' ');
                            }
                            else
                            {
                                valorAdd = valor.ToString().PadLeft(qtdCaracteres, '0');
                            }
                        }
                        else if (valor.ToString().Length == qtdCaracteres)
                        {
                            valorAdd = valor.ToString();
                        }
                        else if (valor.ToString().Length > qtdCaracteres)
                        {
                            valorAdd = valor.ToString().Substring(0, qtdCaracteres); ;
                        }
                    }
                    else
                    {
                        if (info.PropertyType == typeof(string))
                        {
                            valorAdd = "".ToString().PadRight(qtdCaracteres, ' ');
                        }
                        else
                        {
                            valorAdd = "".ToString().PadLeft(qtdCaracteres, '0');
                        }
                    }

                    linha = ReplaceAt(linha, valorAdd, posicaoInicial - 1, qtdCaracteres);
                }

            }

            if (linha.Replace(" ", "").Replace("0", "").Length == 0)
                return "";
            else
                return linha;
        }

        public string ReplaceAt(string linha, string valorAdd, int posicaoInicial, int qtdCaracteres)
        {
            StringBuilder sb = new StringBuilder(linha);
            for (int i = posicaoInicial; i < (posicaoInicial + valorAdd.Length); i++)
            {
                sb[i] = valorAdd[i - posicaoInicial];
            }

            return sb.ToString();

        }


        public object GetConvertedObject(Type type, object o)
        {
            return GetConvertedObject(type, o, new CultureInfo("pt-BR"));
        }

        public object GetConvertedObject(Type type, object o, CultureInfo Cinfo)
        {
            // Nullable Enum
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>))
            {
                Type[] typeCol = type.GetGenericArguments();
                Type nullableType;
                if (typeCol.Length > 0)
                {
                    nullableType = typeCol[0];
                    if (nullableType.BaseType == typeof(Enum))
                    {
                        return Enum.Parse(nullableType, o.ToString(), true);
                    }
                }
            }
            if (type.IsEnum) return Convert.ToInt32(Enum.Parse(type, o.ToString(), true));
            if (type == typeof(byte[])) return o.Equals(DBNull.Value) ? null : o;
            if (type == typeof(DateTime)) return Convert.ToDateTime(o, Cinfo);
            if (type == typeof(TimeSpan)) return (TimeSpan)o;
            if (type == typeof(string)) return Convert.ToString(o);
            if (type == typeof(int)) return Convert.ToInt32(o);
            if (type == typeof(uint)) return Convert.ToInt32(o);
            if (type == typeof(long)) return Convert.ToInt64(o);
            if (type == typeof(float)) return float.Parse(o.ToString());
            if (type == typeof(double)) return Convert.ToDouble(o);
            if (type == typeof(decimal)) return string.IsNullOrEmpty(o.ToString()) ? 0 : Convert.ToDecimal(o, Cinfo);
            if (type == typeof(char)) return Convert.ToChar(o);
            if (type == typeof(byte)) return Convert.ToByte(o);
            if (type == typeof(bool)) return Convert.ToBoolean(o);

            if (type == typeof(long?)) return Convert.ToInt64(o);
            if (type == typeof(DateTime?)) return (o == null ? default(DateTime?) : Convert.ToDateTime(o, Cinfo));
            if (type == typeof(bool?)) return (o == null ? default(bool?) : Convert.ToBoolean(o));
            if (type == typeof(decimal?)) return (string.IsNullOrEmpty(o.ToString().Trim()) ? default(decimal?) :   Convert.ToDecimal(o, Cinfo));
            if (type == typeof(int?)) return (o == null ? default(int?) : (int)(o));

            return o;
        }
    }
    public class ComparePropertyInfo : IEqualityComparer<PropertyInfo>
    {
        public bool Equals(PropertyInfo x, PropertyInfo y)
        {
            RefletorDinamico refletorX = new RefletorDinamico(x.GetType());
            EDIMapper atributoX = refletorX.Atributo<EDIMapper>(x);

            RefletorDinamico refletorY = new RefletorDinamico(y.GetType());
            EDIMapper atributoY = refletorY.Atributo<EDIMapper>(y);

            //return (
            //    (atributoX.PosicaoInicial >= atributoY.PosicaoInicial && atributoX.PosicaoFinal <= atributoY.PosicaoFinal)
            //    || (atributoY.PosicaoInicial >= atributoX.PosicaoInicial && atributoY.PosicaoFinal <= atributoX.PosicaoFinal));
            return (x.Name.Contains(y.Name));
        }

        public int GetHashCode(PropertyInfo codeh)
        {
            return codeh.Name.GetHashCode();
        }

    }

}
