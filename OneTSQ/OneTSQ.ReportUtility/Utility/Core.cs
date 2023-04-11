using FlexCel.Render;
using FlexCel.Report;
using FlexCel.XlsAdapter;
using OneTSQ.CallTempService;
using OneTSQ.Core.Model;
using OneTSQ.Utility;
using OneTSQ.ReportUtility.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OneTSQ.ReportUtility.Utility
{
    public static class DynamicDelegates
    {
        #region Others
        //public static TDelegate FindBestMatch<TDelegate>(Type type, string function, Type returnType, params Type[] paramTypes)
        //    where TDelegate : class
        //{
        //    var mp = FindBestMatchMethodInfo<TDelegate>(type, function, returnType, paramTypes);
        //    if (mp == null)
        //        return null;
        //    return DynamicDelegates.CreateMethodDelegate<TDelegate>(type, function, false, mp.Value.Select(ps => ps.ParameterType).ToArray());
        //}

        /// <summary>
        /// Generates a DynamicMethodDelegate delegate from a MethodInfo object.
        /// </summary>
        public static TDelegate TryCreateMethodDelegate<TDelegate>(Type targetType, string methodName, bool isStatic, params Type[] methodParameters)
            where TDelegate : class
        {
            try
            {
                return targetType.GetMethod(methodName, isStatic ? DynamicDelegates.ClassBindingFlags : DynamicDelegates.ObjectBindingFlags, null, methodParameters, null) == null ? null :
                    DynamicDelegates.CreateMethodDelegate<TDelegate>(targetType, methodName, isStatic, methodParameters.Length <= 0 ? null : methodParameters);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Generates a DynamicMethodDelegate delegate from a MethodInfo object.
        /// </summary>
        public static TDelegate TryCreateMethodDelegate<TDelegate>(Func<MethodInfo, bool> validateFunc, Type targetType, string methodName, bool isStatic, params Type[] methodParameters)
            where TDelegate : class
        {
            try
            {
                var methodInfo = targetType.GetMethod(methodName, isStatic ? DynamicDelegates.ClassBindingFlags : DynamicDelegates.ObjectBindingFlags, null, methodParameters, null);
                return methodInfo == null || !validateFunc(methodInfo) ? null :
                    DynamicDelegates.CreateMethodDelegate<TDelegate>(targetType, methodName, isStatic, methodParameters.Length <= 0 ? null : methodParameters);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region CreateMethod Instance/Static Helpers

        public static TDelegate CreateObjectMethod<TDelegate>(string methodName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateMethodDelegate<TDelegate>(null, methodName, false, null);
        }

        public static TDelegate CreateObjectMethod<TDelegate>(Type targetType, string methodName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateMethodDelegate<TDelegate>(targetType, methodName, false, null);
        }

        public static TDelegate CreateObjectMethod<TDelegate>(string methodName, Type[] methodParameters)
            where TDelegate : class
        {
            return DynamicDelegates.CreateMethodDelegate<TDelegate>(null, methodName, false, methodParameters);
        }

        public static TDelegate CreateObjectMethod<TDelegate>(Type targetType, string methodName, Type[] methodParameters)
            where TDelegate : class
        {
            return DynamicDelegates.CreateMethodDelegate<TDelegate>(targetType, methodName, false, methodParameters);
        }

        public static TDelegate CreateClassMethod<TDelegate>(Type targetType, string methodName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateMethodDelegate<TDelegate>(targetType, methodName, true, null);
        }

        public static TDelegate CreateClassMethod<TDelegate>(Type targetType, string methodName, Type[] methodParameters)
            where TDelegate : class
        {
            return DynamicDelegates.CreateMethodDelegate<TDelegate>(targetType, methodName, true, methodParameters);
        }

        #endregion CreateMethod Instance/Static Helpers


        #region CreateProperty Instance/Static Helpers

        public static TDelegate CreateObjectPropertyGet<TDelegate>(string propertyName)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(null, propertyName, null, false, true);
        }

        public static TDelegate CreateObjectPropertyGet<TDelegate>(string propertyName, Type propertyType)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(null, propertyName, propertyType, false, true);
        }

        public static TDelegate CreateObjectPropertyGet<TDelegate>(Type targetType, string propertyName)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(targetType, propertyName, null, false, true);
        }

        public static TDelegate CreateObjectPropertyGet<TDelegate>(Type targetType, string propertyName, Type propertyType)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(targetType, propertyName, propertyType, false, true);
        }


        public static TDelegate CreateObjectPropertySet<TDelegate>(string propertyName)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(null, propertyName, null, false, false);
        }

        public static TDelegate CreateObjectPropertySet<TDelegate>(string propertyName, Type propertyType)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(null, propertyName, propertyType, false, false);
        }

        public static TDelegate CreateObjectPropertySet<TDelegate>(Type targetType, string propertyName)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(targetType, propertyName, null, false, false);
        }

        public static TDelegate CreateObjectPropertySet<TDelegate>(Type targetType, string propertyName, Type propertyType)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(targetType, propertyName, propertyType, false, false);
        }



        public static TDelegate CreateClassPropertyGet<TDelegate>(Type targetType, string propertyName)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(targetType, propertyName, null, true, true);
        }

        public static TDelegate CreateClassPropertyGet<TDelegate>(Type targetType, string propertyName, Type propertyType)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(targetType, propertyName, propertyType, true, true);
        }


        public static TDelegate CreateClassPropertySet<TDelegate>(Type targetType, string propertyName)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(targetType, propertyName, null, true, false);
        }

        public static TDelegate CreateClassPropertySet<TDelegate>(Type targetType, string propertyName, Type propertyType)
            where TDelegate : class
        {
            return DynamicDelegates.CreatePropertyDelegate<TDelegate>(targetType, propertyName, propertyType, true, false);
        }

        #endregion CreateProperty Instance/Static Helpers


        #region CreateField Instance/Static Helpers

        public static TDelegate CreateObjectFieldGet<TDelegate>(string fieldName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateFieldDelegate<TDelegate>(null, fieldName, false, true);
        }

        public static TDelegate CreateObjectFieldGet<TDelegate>(Type targetType, string fieldName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateFieldDelegate<TDelegate>(targetType, fieldName, false, true);
        }


        public static TDelegate CreateObjectFieldSet<TDelegate>(string fieldName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateFieldDelegate<TDelegate>(null, fieldName, false, false);
        }

        public static TDelegate CreateObjectFieldSet<TDelegate>(Type targetType, string fieldName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateFieldDelegate<TDelegate>(targetType, fieldName, false, false);
        }



        public static TDelegate CreateClassFieldGet<TDelegate>(Type targetType, string fieldName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateFieldDelegate<TDelegate>(targetType, fieldName, true, true);
        }


        public static TDelegate CreateClassFieldSet<TDelegate>(Type targetType, string fieldName)
            where TDelegate : class
        {
            return DynamicDelegates.CreateFieldDelegate<TDelegate>(targetType, fieldName, true, false);
        }

        #endregion CreateField Instance/Static Helpers


        #region Internal Delegate creations core

        public const BindingFlags ObjectBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public const BindingFlags ClassBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        private static TDelegate CreateFieldDelegate<TDelegate>(Type targetType, string fieldName, bool isStatic, bool isGetMethod)
            where TDelegate : class
        {
            Type delegateTargetType = isStatic ? targetType : null;
            Type[] delegateParameters;
            Type delegateReturnType;

            DynamicDelegates.LoadDelegateInfo<TDelegate>(isStatic, ref delegateTargetType, out delegateParameters, out delegateReturnType);

            if (targetType == null)
                targetType = delegateTargetType;

#if DEBUG
            string delegateName = targetType.FullName + (isStatic ? (isGetMethod ? ".[static$field$get]" : ".[static$field$set]") : (isGetMethod ? ".[object$field$get]" : ".[object$field$set]")) + fieldName;
#else
            string delegateName = String.Empty;
#endif

            FieldInfo field = targetType.GetField(fieldName, isStatic ? DynamicDelegates.ClassBindingFlags : DynamicDelegates.ObjectBindingFlags);
            if (field == null)
                throw new ArgumentException(String.Format("{0} field not found", delegateName));

            DynamicMethod dynam = new DynamicMethod(
                delegateName,
                delegateReturnType == typeof(void) ? null : delegateReturnType,
                isStatic ? delegateParameters : Helper.YieldSingle(delegateTargetType).Concat(delegateParameters).ToArray(),
                targetType);
            ILGenerator il = dynam.GetILGenerator();

            // Load the instance of the object (argument 0) onto the stack
            if (isStatic)
                il.Emit(OpCodes.Nop);
            else
                il.Emit(OpCodes.Ldarg_0);

            if (isGetMethod)
            {
                // Load the value of the object's field onto the stack
                // & return the value on the top of the stack (on Ret statement)
                if (isStatic)
                    il.Emit(OpCodes.Ldsfld, field);
                else
                    il.Emit(OpCodes.Ldfld, field);
                if (field.FieldType.IsValueType && !delegateReturnType.IsValueType)
                    il.Emit(OpCodes.Box, field.FieldType);
                else if (delegateReturnType.IsValueType && !field.FieldType.IsValueType)
                    il.Emit(OpCodes.Unbox_Any, delegateReturnType);
            }
            else
            {
                if (!isStatic)
                    il.Emit(OpCodes.Ldarg_1);
                else
                    il.Emit(OpCodes.Ldarg_0);
                if (delegateParameters[0].IsValueType && !field.FieldType.IsValueType)
                    il.Emit(OpCodes.Box, delegateParameters[0]);
                else if (field.FieldType.IsValueType && !delegateParameters[0].IsValueType)
                    il.Emit(OpCodes.Unbox_Any, field.FieldType);
                if (isStatic)
                    il.Emit(OpCodes.Stsfld, field);
                else
                    il.Emit(OpCodes.Stfld, field);
            }
            il.Emit(OpCodes.Ret);

            return dynam.CreateDelegate(typeof(TDelegate)) as TDelegate;
        }

        private static TDelegate CreatePropertyDelegate<TDelegate>(Type targetType, string propertyName, Type propertyType, bool isStatic, bool isGetMethod)
            where TDelegate : class
        {
            Type delegateTargetType = isStatic ? targetType : null;
            Type[] delegateParameters;
            Type delegateReturnType;

            DynamicDelegates.LoadDelegateInfo<TDelegate>(isStatic, ref delegateTargetType, out delegateParameters, out delegateReturnType);

            if (targetType == null)
                targetType = delegateTargetType;

#if DEBUG
            string delegateName = targetType.FullName + (isStatic ? (isGetMethod ? ".[static$prop$get]" : ".[static$prop$set]") : (isGetMethod ? ".[object$prop$get]" : ".[object$prop$set]")) + propertyName;
#else
            string delegateName = String.Empty;
#endif

            PropertyInfo property = propertyType == null ?
                targetType.GetProperty(propertyName, isStatic ? DynamicDelegates.ClassBindingFlags : DynamicDelegates.ObjectBindingFlags) :
                targetType.GetProperty(propertyName, isStatic ? DynamicDelegates.ClassBindingFlags : DynamicDelegates.ObjectBindingFlags, null, propertyType, Type.EmptyTypes, null);
            if (property == null)
                throw new ArgumentException(String.Format("{0} property not found", delegateName));

            MethodInfo method = isGetMethod ? property.GetGetMethod(true) : property.GetSetMethod(true);

            Type[] methodParameters = isGetMethod ? Type.EmptyTypes : new Type[] { property.PropertyType };

            return DynamicDelegates.CreateMethodDelegate<TDelegate>(targetType, delegateTargetType, delegateName, delegateReturnType, delegateParameters, method, methodParameters);
        }

        /// <summary>
        /// Generates a DynamicMethodDelegate delegate from a MethodInfo object.
        /// </summary>
        public static TDelegate CreateMethodDelegate<TDelegate>(Type targetType, string methodName, bool isStatic, Type[] methodParameters)
            where TDelegate : class
        {
            Type delegateTargetType = isStatic ? targetType : null;
            Type[] delegateParameters;
            Type delegateReturnType;

            DynamicDelegates.LoadDelegateInfo<TDelegate>(isStatic, ref delegateTargetType, out delegateParameters, out delegateReturnType);

            if (targetType == null)
                targetType = delegateTargetType;

            if (methodParameters == null)
                methodParameters = delegateParameters;

#if DEBUG
            string delegateName = targetType.FullName + (isStatic ? ".[static]" : ".[object]") + methodName;
#else
            string delegateName = String.Empty;
#endif

            MethodInfo method;
            try
            {
                method = targetType.GetMethod(methodName, isStatic ? DynamicDelegates.ClassBindingFlags : DynamicDelegates.ObjectBindingFlags, null, methodParameters, null);
            }
            catch (System.Reflection.AmbiguousMatchException)
            {
                method = targetType.GetMethods(isStatic ? DynamicDelegates.ClassBindingFlags : DynamicDelegates.ObjectBindingFlags)
                    .FirstOrDefault(m => !m.IsGenericMethod && m.Name == methodName &&
                        m.GetParameters().Length == methodParameters.Length &&
                        (methodParameters.Length <= 0 || m.GetParameters().Select((pi, i) => Tuple.Create(pi, i)).All(pii => pii.Item1.ParameterType == methodParameters[pii.Item2]))
                    );
                if (method == null)
                    throw;
            }

            return DynamicDelegates.CreateMethodDelegate<TDelegate>(targetType, delegateTargetType, delegateName, delegateReturnType, delegateParameters, method, methodParameters);
        }

        public static TDelegate CreateMethodDelegate<TDelegate>(MethodInfo methodInfo)
            where TDelegate : class
        {
            var delegateTargetType = methodInfo.IsStatic ? methodInfo.DeclaringType : null;
            Type[] delegateParameters;
            Type delegateReturnType;

            DynamicDelegates.LoadDelegateInfo<TDelegate>(methodInfo.IsStatic, ref delegateTargetType, out delegateParameters, out delegateReturnType);
#if DEBUG
            var delegateName = methodInfo.DeclaringType.FullName + (methodInfo.IsStatic ? ".[static]" : ".[object]") + methodInfo.Name;
#else
            var delegateName = String.Empty;
#endif
            return DynamicDelegates.CreateMethodDelegate<TDelegate>(methodInfo.DeclaringType, delegateTargetType, delegateName, delegateReturnType, delegateParameters, methodInfo, methodInfo.GetParameters().Select(pi => pi.ParameterType).ToArray());
        }

        private static void LoadDelegateInfo<TDelegate>(bool isStatic, ref Type targetType, out Type[] delegateParameters, out Type delegateReturnType)
            where TDelegate : class
        {
            MethodInfo delegateInvokeMethod = typeof(TDelegate).GetMethod("Invoke");
            ParameterInfo[] delegateArgsInfo = delegateInvokeMethod.GetParameters();

            if (targetType == null)
                if (isStatic)
                    throw new ArgumentNullException("targetType", "Must provider the targetType for static Method");
                else
                    targetType = delegateArgsInfo[0].ParameterType;

            delegateParameters = (isStatic ? delegateArgsInfo : delegateArgsInfo.Skip(1)).Select(param => param.ParameterType).ToArray();
            delegateReturnType = delegateInvokeMethod.ReturnType;
        }

        /// <summary>
        /// Generates a DynamicMethodDelegate delegate from a MethodInfo object.
        /// </summary>
        private static TDelegate CreateMethodDelegate<TDelegate>(Type targetType, Type delegateTargetType, string delegateName, Type delegateReturnType, Type[] delegateParameters, MethodInfo method, Type[] methodParameters)
            where TDelegate : class
        {
            #region Initialization & Validation

            if (method == null)
                throw new ArgumentException(String.Format("{0} method not found", delegateName));

            bool delegateParamIsArray = delegateParameters.Length == 1 && (delegateParameters[0] == typeof(object[]));
            bool methodParamIsArray = methodParameters.Length == 1 && (methodParameters[0] == typeof(object[]));

            if (!delegateParamIsArray && !methodParamIsArray && methodParameters.Length != delegateParameters.Length)
                throw new ArgumentException("Delegate parameters & method parameters do not match");

            if (delegateReturnType != typeof(void) && method.ReturnType != typeof(void) && !delegateReturnType.IsAssignableFrom(method.ReturnType))
                throw new ArgumentException("Delegate return type & method return type do not match");

            #endregion Initialization & Validation

            // Check that if destination method has same signature
            // so that will not need to inject IL code, bind delegate dirrectly to the method
            if ((method.IsStatic || targetType == delegateTargetType) && method.ReturnType == delegateReturnType && Helper.IsEquals(methodParameters, delegateParameters))
            {
                return Delegate.CreateDelegate(typeof(TDelegate), method) as TDelegate;
            }
            else
            {
                // Create dynamic method and obtain its IL generator to inject code
                DynamicMethod dynam;
                try
                {
                    dynam = new DynamicMethod(
                        delegateName,
                        delegateReturnType == typeof(void) ? null : delegateReturnType,
                        method.IsStatic ? delegateParameters : Helper.YieldSingle(delegateTargetType).Concat(delegateParameters).ToArray(),
                        targetType);
                }
                catch (ArgumentException)
                {
                    dynam = new DynamicMethod(
                        delegateName,
                        delegateReturnType == typeof(void) ? null : delegateReturnType,
                        method.IsStatic ? delegateParameters : Helper.YieldSingle(delegateTargetType).Concat(delegateParameters).ToArray());
                }
                ILGenerator il = dynam.GetILGenerator();

                #region IL generation

                #region Instance push

                // If method isn't static push target instance on top
                // of stack.
                il.Emit(OpCodes.Nop);
                if (!method.IsStatic)
                {
                    // Argument 0 of dynamic method is target instance.
                    il.Emit(OpCodes.Ldarg_0);
                }

                #endregion

                #region Standard argument layout

                // Lay out args array onto stack.
                int argIndex = method.IsStatic ? 0 : 1;
                if (delegateParamIsArray)
                    if (methodParamIsArray)
                        il.LoadParameters(argIndex);
                    else
                        il.LoadMethodParameters(methodParameters, argIndex);
                else
                    if (methodParamIsArray)
                    il.LoadDelegateParameters(delegateParameters, argIndex);
                else
                    il.LoadParameters(delegateParameters, methodParameters, argIndex);

                #endregion

                #region Method call

                // Perform actual call.
                // If method is not final a callvirt is required
                // otherwise a normal call will be emitted.
                il.Emit(method.IsStatic || method.IsFinal ? OpCodes.Call : OpCodes.Callvirt, method);

                if (method.ReturnType != typeof(void) &&
                    method.ReturnType.IsValueType &&
                    !delegateReturnType.IsValueType)
                {
                    // If result is of value type it needs to be boxed
                    il.Emit(OpCodes.Box, method.ReturnType);
                }
                else
                {
                    il.Emit(OpCodes.Nop);
                }

                // Emit return opcode.
                il.Emit(OpCodes.Ret);

                #endregion

                #endregion

                return dynam.CreateDelegate(typeof(TDelegate)) as TDelegate;
            }
        }

        #region Parameters loading utils

        private static void LoadParameters(this ILGenerator il, Type[] delegateParameters, Type[] methodParameters, int argIndex)
        {
            for (int i = 0; i < delegateParameters.Length; i++)
            {
                if (!delegateParameters[i].IsAssignableFrom(methodParameters[i]) && !methodParameters[i].IsAssignableFrom(delegateParameters[i]))
                    throw new ArgumentException(String.Format("Delegate parameter [{0}] & method parameter [{1}] are incompatible", delegateParameters[i], methodParameters[i]), String.Format("Parameter No.{0}", i + 1));

                il.Emit(OpCodes.Ldarg_S, (byte)(argIndex + i));
                if (methodParameters[i].IsValueType && !delegateParameters[i].IsValueType)
                    il.Emit(OpCodes.Unbox_Any, methodParameters[i]);
                else if (delegateParameters[i].IsValueType && !methodParameters[i].IsValueType)
                    il.Emit(OpCodes.Box, delegateParameters[i]);
            }
        }

        private static void LoadParameters(this ILGenerator il, int argIndex)
        {
            il.Emit(OpCodes.Ldarg, argIndex);
        }

        private static void LoadMethodParameters(this ILGenerator il, Type[] methodParameters, int argIndex)
        {
            for (int i = 0; i < methodParameters.Length; i++)
            {
                // Push args array reference onto the stack, followed
                // by the current argument index (i). The Ldelem_Ref opcode
                // will resolve them to args[i].

                // Argument 1 of dynamic method is argument array.
                il.Emit(OpCodes.Ldarg, argIndex);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem_Ref);

                // If parameter [i] is a value type perform an unboxing.
                if (methodParameters[i].IsValueType)
                {
                    il.Emit(OpCodes.Unbox_Any, methodParameters[i]);
                }
            }
        }

        private static void LoadDelegateParameters(this ILGenerator il, Type[] delegateParameters, int argIndex)
        {
            il.DeclareLocal(typeof(object[]));
            il.Emit(OpCodes.Ldc_I4, delegateParameters.Length);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc_0);   // push arr to local stack

            for (int i = 0; i < delegateParameters.Length; i++)
            {
                il.Emit(OpCodes.Ldloc_0);   // load the local arr
                il.Emit(OpCodes.Ldc_I4, i);         // index [i] of item in arr
                il.Emit(OpCodes.Ldarg, (short)(argIndex + i));  // load args at [argIndex + i]
                if (delegateParameters[i].IsValueType)  // Box the loaded params (if needed)
                    il.Emit(OpCodes.Box, delegateParameters[i]);
                il.Emit(OpCodes.Stelem_Ref);        // set arr[i] := args[argIndex + i]
            }
            il.Emit(OpCodes.Ldloc_0);   // load the local arr as input for next method call
        }

        #endregion Parameters loading utils

        #endregion Internal Delegate creations core
    }
}
