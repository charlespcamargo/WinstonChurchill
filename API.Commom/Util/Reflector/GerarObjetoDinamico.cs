using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace WinstonChurchill.API.Common.Reflector
{
    public class GerarObjetoDinamico
    {
        public static dynamic CriarObjeto<T>(T objeto, List<string> listaCampos)
        {
            List<T> lista = new List<T>();
            lista.Add(objeto);

            List<object> listaObjetos = CriarObjetoDinamico<T>(lista, listaCampos);
            
            return listaObjetos.FirstOrDefault();
        }

        public static dynamic CriarObjeto<T>(List<T> listaValores, List<string> listaCampos)
        {
            return CriarObjetoDinamico<T>(listaValores, listaCampos);
        }

        private static List<object> CriarObjetoDinamico<T>(List<T> listaValores, List<string> listaCampos)
        {
            RefletorDinamico refletor = new RefletorDinamico(typeof(T));

            List<object> listaObjetos = new List<object>();

            foreach (T item in listaValores)
            {
                Dictionary<string, object> objeto = new Dictionary<string, object>();
                Dictionary<string, object> objetoRico = new Dictionary<string, object>();

                if (listaCampos != null && listaCampos.Count > 0)
                {
                    PopulaObjetoComCampos<T>(listaCampos, refletor, item, objeto, objetoRico);
                }
                else
                {
                    PopulaObjeto<T>(refletor, item, objeto);
                }

                listaObjetos.Add(objeto);
            }

            return listaObjetos;

        }

        private static void PopulaObjeto<T>(RefletorDinamico refletor, T item, Dictionary<string, object> objeto)
        {
            foreach (var prop in refletor.Propriedades)
            {
                string namespaceModelo = typeof(T).FullName.Replace("." + typeof(T).Name, "");
                string namespaceProperty = prop.PropertyType.FullName.Replace("." + prop.PropertyType.Name, "");

                if (!prop.ToString().Contains("EntityReference") && prop.Name != "EntityKey" && namespaceModelo != namespaceProperty
                    && !prop.PropertyType.Name.Contains("EntityCollection"))
                {
                    object valor = prop.GetValue((T)item, null);
                    objeto.Add(prop.Name, valor);
                }
                else if (!prop.ToString().Contains("EntityReference"))
                {
                    objeto.Add(prop.Name, null);
                }
            }
        }

        private static void PopulaObjetoComCampos<T>(List<string> listaCampos, RefletorDinamico refletor, T item, Dictionary<string, object> objeto, Dictionary<string, object> objetoRico)
        {
            for (int i = 0; i < listaCampos.Count; i++)
            {
                object valor;
                string propriedade = listaCampos[i];
                bool ehRico = false;
                if (propriedade.Contains("|")) ehRico = true;

                try
                {
                    PropertyInfo prop = refletor.Propriedade(propriedade, ehRico);
                    if (!ehRico)
                    {
                        valor = prop.GetValue((T)item, null);
                        if (valor != null)
                        {
                            if (valor.GetType() == typeof(decimal))
                                objeto.Add(prop.Name.Replace("|", "."), valor.ToString());
                            else
                                objeto.Add(prop.Name.Replace("|", "."), valor);
                        }
                        else
                            objeto.Add(prop.Name.Replace("|", "."), valor);
                    }
                    else
                    {
                        valor = refletor.GetPropValueObjRico(propriedade.Replace("|", "."), (T)item);
                        objetoRico.Add(propriedade.Split('|').Last(), valor);

                        string nomeObjeto = propriedade.Split('|')[0];
                        if (objeto.ContainsKey(nomeObjeto))//Já Existe objeto
                        {
                            objeto[nomeObjeto] = objetoRico;
                        }
                        else//Novo objeto Rico
                        {
                            objeto.Add(nomeObjeto, objetoRico);
                        }
                    }
                }
                catch
                {
                    valor = "PROPRIEDADE NÃO EXISTE";
                    objeto.Add(propriedade.Replace("|", "."), valor);
                }
            }
        }



        #region Gerar objeto dinamico não tipado

        public static TypeBuilder CreateTypeBuilder(
          string assemblyName, string moduleName, string typeName)
        {
            TypeBuilder typeBuilder = AppDomain
                .CurrentDomain
                .DefineDynamicAssembly(new AssemblyName(assemblyName),
                                       AssemblyBuilderAccess.Run)
                .DefineDynamicModule(moduleName)
                .DefineType(typeName, TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            return typeBuilder;
        }


        public static void CreateAutoImplementedProperty(
         TypeBuilder builder, string propertyName, Type propertyType)
        {
            const string PrivateFieldPrefix = "m_";
            const string GetterPrefix = "get_";
            const string SetterPrefix = "set_";

            // Generate the field.
            FieldBuilder fieldBuilder = builder.DefineField(
                string.Concat(PrivateFieldPrefix, propertyName),
                              propertyType, FieldAttributes.Private);

            // Generate the property
            PropertyBuilder propertyBuilder = builder.DefineProperty(
                propertyName, System.Reflection.PropertyAttributes.HasDefault, propertyType, null);

            // Property getter and setter attributes.
            MethodAttributes propertyMethodAttributes =
                MethodAttributes.Public | MethodAttributes.SpecialName |
                MethodAttributes.HideBySig;

            // Define the getter method.
            MethodBuilder getterMethod = builder.DefineMethod(
                string.Concat(GetterPrefix, propertyName),
                propertyMethodAttributes, propertyType, Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            MethodBuilder setterMethod = builder.DefineMethod(
                string.Concat(SetterPrefix, propertyName),
                propertyMethodAttributes, null, new Type[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }

        #endregion
    }
}
