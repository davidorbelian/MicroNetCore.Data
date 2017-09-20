using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Humanizer;
using MicroNetCore.Data.Abstractions;
using MicroNetCore.Data.Models.Collections;
using MicroNetCore.Models;
using MicroNetCore.Models.Collections;

namespace MicroNetCore.Data.Models
{
    public sealed class DataModelListProvider : IDataModelListProvider
    {
        private readonly ModuleBuilder _moduleBuilder;

        public DataModelListProvider()
        {
            _moduleBuilder = CreateModuleBuilder();
        }

        #region Constants

        private const string TypeNamePostfix = "DataModel";

        private const TypeAttributes DmTypeAttributes =
            TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed;

        private const MethodAttributes DmMethodAttributes =
            MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

        #endregion

        #region Static

        private static readonly IDictionary<Type, Type> EntityTypesCache;
        private static readonly IDictionary<(Type, Type), Type> RelationTypesCache;

        static DataModelListProvider()
        {
            EntityTypesCache = new Dictionary<Type, Type>();
            RelationTypesCache = new Dictionary<(Type, Type), Type>();
        }

        #endregion
        
        #region IDataModelListProvider

        public IDataModelTypeList Get(IModelTypeList types)
        {
            // Create TypeBuilders
            var entityModelTypeBuilders = CreateEntityModelTypeBuilders(types);

            // Create Types
            var entityModelTypes = entityModelTypeBuilders.ToDictionary(t => t.Key, t => t.Value.CreateType());

            // Add Types to Cache
            foreach (var typeBuilder in entityModelTypes)
                EntityTypesCache.Add(typeBuilder.Key, typeBuilder.Value);

            // Return all DataModelTypes
            return new DataModelTypeList(new List<Type>()
                .Concat(EntityTypesCache.Values)
                .Concat(RelationTypesCache.Values));
        }
        
        #endregion

        #region Helpers

        private IDictionary<Type, TypeBuilder> CreateEntityModelTypeBuilders(IEnumerable<Type> types)
        {
            return types.ToDictionary(t => t, GetEntityModelTypeBuilder);
        }

        private Type GetRelationModelType(Type one, Type two)
        {
            return string.CompareOrdinal(one.Name, two.Name) < 0
                ? Get(one, two)
                : Get(two, one);

            Type Get(Type tOne, Type tTwo)
            {
                if (!RelationTypesCache.ContainsKey((tOne, tTwo)))
                    RelationTypesCache.Add((tOne, tTwo), CreateRelationModelType(tOne, tTwo));

                return RelationTypesCache[(tOne, tTwo)];
            }
        }

        private Type CreateRelationModelType(Type one, Type two)
        {
            var builder = GetRelationModelTypeBuilder(one, two);

            AddRelationModelProperties(builder, one, two);

            return builder.CreateType();
        }

        #region Builders

        private static AssemblyBuilder CreateAssemblyBuilder()
        {
            var assemblyName = new AssemblyName(Guid.NewGuid().ToString());
            return AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        }

        private static ModuleBuilder CreateModuleBuilder()
        {
            var moduleName = Guid.NewGuid().ToString();
            return CreateAssemblyBuilder().DefineDynamicModule(moduleName);
        }

        private TypeBuilder GetEntityModelTypeBuilder(Type type)
        {
            var builder = _moduleBuilder.DefineType($"{type.Name}{TypeNamePostfix}", DmTypeAttributes);
            builder.AddInterfaceImplementation(typeof(IEntityDataModel));

            return builder;
        }

        private TypeBuilder GetRelationModelTypeBuilder(Type one, Type two)
        {
            var builder = _moduleBuilder.DefineType($"{one.Name}To{two.Name}{TypeNamePostfix}", DmTypeAttributes);
            builder.AddInterfaceImplementation(typeof(IRelationDataModel));

            return builder;
        }

        #endregion

        #region Properties

        #region EntityModels

        private static void AddEntityModelProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            if (IsOneTo(property))
            {
                // One-To-One
                if (IsToOne(property.DeclaringType, property.PropertyType))
                    AddEntityModelOneToOneProperty(typeBuilder, property);

                // One-To-Many
                else if (IsToMany(property.DeclaringType, property.PropertyType))
                    AddEntityModelOneToManyProperty(typeBuilder, property);
            }
            else if (IsManyTo(property))
            {
                // Many-To-One
                if (IsToOne(property.DeclaringType, property.PropertyType.GetGenericArguments().First()))
                    AddEntityModelManyToOneProperty(typeBuilder, property);

                // Many-To-Many
                else if (IsToMany(property.DeclaringType, property.PropertyType.GetGenericArguments().First()))
                    AddEntityModelManyToManyProperty(typeBuilder, property);
            }
            else
            {
                // No relation
                AddEntityModelValueProperty(typeBuilder, property);
            }
        }

        private static void AddEntityModelValueProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var fieldBuilder = typeBuilder.DefineField(
                property.Name.Camelize(),
                property.PropertyType,
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                property.Name,
                property.Attributes,
                property.PropertyType,
                new Type[0]);

            propertyBuilder.SetSetMethod(GetSetMethod(typeBuilder, propertyBuilder, fieldBuilder));
            propertyBuilder.SetGetMethod(GetGetMethod(typeBuilder, propertyBuilder, fieldBuilder));
        }

        private static void AddEntityModelOneToOneProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var fieldBuilder = typeBuilder.DefineField(
                property.Name.Camelize(),
                property.PropertyType,
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                property.Name,
                property.Attributes,
                property.PropertyType,
                new Type[0]);

            propertyBuilder.SetSetMethod(GetSetMethod(typeBuilder, propertyBuilder, fieldBuilder));
            propertyBuilder.SetGetMethod(GetGetMethod(typeBuilder, propertyBuilder, fieldBuilder));
        }

        private static void AddEntityModelOneToManyProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var fieldBuilder = typeBuilder.DefineField(
                property.Name.Camelize(),
                property.PropertyType,
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                property.Name,
                property.Attributes,
                property.PropertyType,
                new Type[0]);

            propertyBuilder.SetSetMethod(GetSetMethod(typeBuilder, propertyBuilder, fieldBuilder));
            propertyBuilder.SetGetMethod(GetGetMethod(typeBuilder, propertyBuilder, fieldBuilder));
        }

        private static void AddEntityModelManyToOneProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var fieldBuilder = typeBuilder.DefineField(
                property.Name.Camelize(),
                property.PropertyType,
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                property.Name,
                property.Attributes,
                property.PropertyType,
                new Type[0]);

            propertyBuilder.SetSetMethod(GetSetMethod(typeBuilder, propertyBuilder, fieldBuilder));
            propertyBuilder.SetGetMethod(GetGetMethod(typeBuilder, propertyBuilder, fieldBuilder));
        }

        private static void AddEntityModelManyToManyProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var fieldBuilder = typeBuilder.DefineField(
                property.Name.Camelize(),
                property.PropertyType,
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                property.Name,
                property.Attributes,
                property.PropertyType,
                new Type[0]);

            propertyBuilder.SetSetMethod(GetSetMethod(typeBuilder, propertyBuilder, fieldBuilder));
            propertyBuilder.SetGetMethod(GetGetMethod(typeBuilder, propertyBuilder, fieldBuilder));
        }

        private static bool IsOneTo(PropertyInfo property)
        {
            return typeof(IModel).IsAssignableFrom(property.PropertyType);
        }

        private static bool IsManyTo(PropertyInfo property)
        {
            return typeof(ICollection<IModel>).IsAssignableFrom(property.PropertyType);
        }

        private static bool IsToOne(Type thisType, Type otherType)
        {
            return otherType
                       .GetProperties()
                       .SingleOrDefault(p => p.PropertyType == thisType) != null;
        }

        private static bool IsToMany(Type thisType, Type otherType)
        {
            return otherType
                       .GetProperties()
                       .SingleOrDefault(p => p.PropertyType == typeof(ICollection).MakeGenericType(thisType)) != null;
        }

        #endregion

        #region RelationModels

        private static void AddRelationModelProperties(TypeBuilder typeBuilder, Type one, Type two)
        {
            AddRelationModelIdProperty(typeBuilder, one.Name);
            AddRelationModelNavigationProperty(typeBuilder, one);

            AddRelationModelIdProperty(typeBuilder, two.Name);
            AddRelationModelNavigationProperty(typeBuilder, two);
        }

        private static void AddRelationModelIdProperty(TypeBuilder typeBuilder, string typeName)
        {
            var name = $"{typeName}Id";

            var fieldBuilder = typeBuilder.DefineField(
                name.Camelize(), 
                typeof(long), 
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                name,
                PropertyAttributes.None,
                typeof(long),
                new Type[0]);

            propertyBuilder.SetSetMethod(GetSetMethod(typeBuilder, propertyBuilder, fieldBuilder));
            propertyBuilder.SetGetMethod(GetGetMethod(typeBuilder, propertyBuilder, fieldBuilder));
        }

        private static void AddRelationModelNavigationProperty(TypeBuilder typeBuilder, Type propertyType)
        {
            var name = propertyType.Name;

            var fieldBuilder = typeBuilder.DefineField(
                name.Camelize(),
                propertyType,
                FieldAttributes.Private);

            var propertyBuilder = typeBuilder.DefineProperty(
                name,
                PropertyAttributes.None,
                propertyType,
                new Type[0]);

            propertyBuilder.SetSetMethod(GetSetMethod(typeBuilder, propertyBuilder, fieldBuilder));
            propertyBuilder.SetGetMethod(GetGetMethod(typeBuilder, propertyBuilder, fieldBuilder));
        }
        
        #endregion

        #endregion

        #region Getter and Setter

        private static MethodBuilder GetSetMethod(TypeBuilder typeBuilder, PropertyInfo property, FieldInfo field)
        {
            var setMethod = typeBuilder.DefineMethod(
                $"set_{property.Name}",
                DmMethodAttributes,
                null,
                new[] { property.PropertyType });

            var ilGenerator = setMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Stfld, field);
            ilGenerator.Emit(OpCodes.Ret);

            return setMethod;
        }

        private static MethodBuilder GetGetMethod(TypeBuilder typeBuilder, PropertyInfo property, FieldInfo field)
        {
            var getMethod = typeBuilder.DefineMethod(
                $"get_{property.Name}",
                DmMethodAttributes,
                property.PropertyType,
                Type.EmptyTypes);

            var ilGenerator = getMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, field);
            ilGenerator.Emit(OpCodes.Ret);

            return getMethod;
        }
        
        #endregion

        #endregion
    }
}