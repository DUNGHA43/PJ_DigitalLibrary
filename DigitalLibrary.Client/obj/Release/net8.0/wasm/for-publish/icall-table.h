#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
229,
241,
242,
243,
244,
245,
246,
247,
248,
249,
252,
253,
254,
427,
428,
429,
458,
459,
460,
480,
481,
482,
483,
600,
601,
602,
605,
646,
647,
648,
651,
653,
655,
657,
662,
670,
671,
672,
673,
674,
675,
676,
677,
678,
679,
680,
681,
682,
683,
684,
685,
686,
688,
689,
690,
691,
692,
693,
694,
786,
787,
788,
789,
790,
791,
792,
793,
794,
795,
796,
797,
798,
799,
800,
801,
802,
804,
805,
806,
807,
808,
809,
810,
877,
878,
947,
954,
957,
959,
964,
965,
967,
968,
972,
973,
975,
977,
978,
981,
982,
983,
986,
988,
991,
993,
995,
1004,
1072,
1074,
1076,
1086,
1087,
1088,
1089,
1091,
1098,
1099,
1100,
1101,
1102,
1110,
1111,
1112,
1116,
1117,
1119,
1123,
1124,
1125,
1409,
1601,
1602,
9713,
9714,
9716,
9717,
9718,
9719,
9720,
9722,
9724,
9726,
9727,
9738,
9740,
9747,
9749,
9751,
9753,
9804,
9805,
9807,
9808,
9809,
9810,
9811,
9813,
9815,
10936,
10940,
10942,
10943,
10944,
10945,
11210,
11211,
11212,
11213,
11231,
11232,
11233,
11235,
11279,
11364,
11366,
11368,
11378,
11379,
11380,
11381,
11382,
11841,
11842,
11843,
11848,
11849,
11928,
11929,
11972,
11979,
11986,
11997,
12001,
12026,
12105,
12118,
12120,
12122,
12145,
12147,
12148,
12149,
12150,
12151,
12160,
12175,
12195,
12196,
12204,
12206,
12213,
12214,
12217,
12219,
12224,
12230,
12231,
12238,
12240,
12252,
12255,
12256,
12257,
12268,
12277,
12283,
12284,
12285,
12287,
12288,
12305,
12307,
12321,
12344,
12345,
12346,
12371,
12376,
12406,
12407,
12966,
12980,
13081,
13082,
13307,
13308,
13317,
13318,
13319,
13325,
13422,
13988,
13989,
14569,
14570,
14575,
14585,
15552,
15573,
15575,
15577,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_InitializeInternal_raw (int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
void ves_icall_System_Enum_InternalBoxEnum_raw (int,int,int64_t,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
int ves_icall_System_GC_GetCollectionCount (int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Acos (double);
double ves_icall_System_Math_Acosh (double);
double ves_icall_System_Math_Asin (double);
double ves_icall_System_Math_Asinh (double);
double ves_icall_System_Math_Atan (double);
double ves_icall_System_Math_Atan2 (double,double);
double ves_icall_System_Math_Atanh (double);
double ves_icall_System_Math_Cbrt (double);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Cosh (double);
double ves_icall_System_Math_Exp (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sinh (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_Tanh (double);
double ves_icall_System_Math_FusedMultiplyAdd (double,double,double);
double ves_icall_System_Math_Log2 (double);
double ves_icall_System_Math_ModF (double,int);
float ves_icall_System_MathF_Acos (float);
float ves_icall_System_MathF_Acosh (float);
float ves_icall_System_MathF_Asin (float);
float ves_icall_System_MathF_Asinh (float);
float ves_icall_System_MathF_Atan (float);
float ves_icall_System_MathF_Atan2 (float,float);
float ves_icall_System_MathF_Atanh (float);
float ves_icall_System_MathF_Cbrt (float);
float ves_icall_System_MathF_Ceiling (float);
float ves_icall_System_MathF_Cos (float);
float ves_icall_System_MathF_Cosh (float);
float ves_icall_System_MathF_Exp (float);
float ves_icall_System_MathF_Floor (float);
float ves_icall_System_MathF_Log (float);
float ves_icall_System_MathF_Log10 (float);
float ves_icall_System_MathF_Pow (float,float);
float ves_icall_System_MathF_Sin (float);
float ves_icall_System_MathF_Sinh (float);
float ves_icall_System_MathF_Sqrt (float);
float ves_icall_System_MathF_Tan (float);
float ves_icall_System_MathF_Tanh (float);
float ves_icall_System_MathF_FusedMultiplyAdd (float,float,float);
float ves_icall_System_MathF_Log2 (float);
float ves_icall_System_MathF_ModF (float,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_System_RuntimeType_AllocateValueType_raw (int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsComObject_raw (int,int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_String_InternalIsInterned_raw (int,int);
int ves_icall_System_String_InternalIntern_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw (int,int);
int ves_icall_System_Reflection_Assembly_GetCallingAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
void ves_icall_System_Reflection_AssemblyName_FreeAssemblyName (int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetEntryPoint_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetTopLevelForwardedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInfoInternal_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetReferencedAssemblies_raw (int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw (int,int);
void ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw (int,int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
int ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_CustomAttributeBuilder_GetBlob_raw (int,int,int,int,int,int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_Debugger_IsLogging ();
void ves_icall_System_Diagnostics_Debugger_Log (int,int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 229,
ves_icall_System_Array_InternalCreate,
// token 241,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 242,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 243,
ves_icall_System_Array_CanChangePrimitive,
// token 244,
ves_icall_System_Array_FastCopy,
// token 245,
ves_icall_System_Array_GetLengthInternal_raw,
// token 246,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 247,
ves_icall_System_Array_GetGenericValue_icall,
// token 248,
ves_icall_System_Array_GetValueImpl_raw,
// token 249,
ves_icall_System_Array_SetGenericValue_icall,
// token 252,
ves_icall_System_Array_SetValueImpl_raw,
// token 253,
ves_icall_System_Array_InitializeInternal_raw,
// token 254,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 427,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 428,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 429,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 458,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 459,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 460,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 480,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 481,
ves_icall_System_Enum_InternalBoxEnum_raw,
// token 482,
ves_icall_System_Enum_InternalGetCorElementType,
// token 483,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 600,
ves_icall_System_Environment_get_ProcessorCount,
// token 601,
ves_icall_System_Environment_get_TickCount,
// token 602,
ves_icall_System_Environment_get_TickCount64,
// token 605,
ves_icall_System_Environment_FailFast_raw,
// token 646,
ves_icall_System_GC_GetCollectionCount,
// token 647,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 648,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 651,
ves_icall_System_GC_SuppressFinalize_raw,
// token 653,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 655,
ves_icall_System_GC_GetGCMemoryInfo,
// token 657,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 662,
ves_icall_System_Object_MemberwiseClone_raw,
// token 670,
ves_icall_System_Math_Acos,
// token 671,
ves_icall_System_Math_Acosh,
// token 672,
ves_icall_System_Math_Asin,
// token 673,
ves_icall_System_Math_Asinh,
// token 674,
ves_icall_System_Math_Atan,
// token 675,
ves_icall_System_Math_Atan2,
// token 676,
ves_icall_System_Math_Atanh,
// token 677,
ves_icall_System_Math_Cbrt,
// token 678,
ves_icall_System_Math_Ceiling,
// token 679,
ves_icall_System_Math_Cos,
// token 680,
ves_icall_System_Math_Cosh,
// token 681,
ves_icall_System_Math_Exp,
// token 682,
ves_icall_System_Math_Floor,
// token 683,
ves_icall_System_Math_Log,
// token 684,
ves_icall_System_Math_Log10,
// token 685,
ves_icall_System_Math_Pow,
// token 686,
ves_icall_System_Math_Sin,
// token 688,
ves_icall_System_Math_Sinh,
// token 689,
ves_icall_System_Math_Sqrt,
// token 690,
ves_icall_System_Math_Tan,
// token 691,
ves_icall_System_Math_Tanh,
// token 692,
ves_icall_System_Math_FusedMultiplyAdd,
// token 693,
ves_icall_System_Math_Log2,
// token 694,
ves_icall_System_Math_ModF,
// token 786,
ves_icall_System_MathF_Acos,
// token 787,
ves_icall_System_MathF_Acosh,
// token 788,
ves_icall_System_MathF_Asin,
// token 789,
ves_icall_System_MathF_Asinh,
// token 790,
ves_icall_System_MathF_Atan,
// token 791,
ves_icall_System_MathF_Atan2,
// token 792,
ves_icall_System_MathF_Atanh,
// token 793,
ves_icall_System_MathF_Cbrt,
// token 794,
ves_icall_System_MathF_Ceiling,
// token 795,
ves_icall_System_MathF_Cos,
// token 796,
ves_icall_System_MathF_Cosh,
// token 797,
ves_icall_System_MathF_Exp,
// token 798,
ves_icall_System_MathF_Floor,
// token 799,
ves_icall_System_MathF_Log,
// token 800,
ves_icall_System_MathF_Log10,
// token 801,
ves_icall_System_MathF_Pow,
// token 802,
ves_icall_System_MathF_Sin,
// token 804,
ves_icall_System_MathF_Sinh,
// token 805,
ves_icall_System_MathF_Sqrt,
// token 806,
ves_icall_System_MathF_Tan,
// token 807,
ves_icall_System_MathF_Tanh,
// token 808,
ves_icall_System_MathF_FusedMultiplyAdd,
// token 809,
ves_icall_System_MathF_Log2,
// token 810,
ves_icall_System_MathF_ModF,
// token 877,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 878,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 947,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 954,
ves_icall_RuntimeType_make_array_type_raw,
// token 957,
ves_icall_RuntimeType_make_byref_type_raw,
// token 959,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 964,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 965,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 967,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 968,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 972,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 973,
ves_icall_System_RuntimeType_AllocateValueType_raw,
// token 975,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 977,
ves_icall_System_RuntimeType_getFullName_raw,
// token 978,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 981,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 982,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 983,
ves_icall_RuntimeType_GetFields_native_raw,
// token 986,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 988,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 991,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 993,
ves_icall_RuntimeType_GetName_raw,
// token 995,
ves_icall_RuntimeType_GetNamespace_raw,
// token 1004,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 1072,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 1074,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 1076,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 1086,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 1087,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 1088,
ves_icall_RuntimeTypeHandle_IsComObject_raw,
// token 1089,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 1091,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 1098,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 1099,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 1100,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 1101,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 1102,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 1110,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 1111,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 1112,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 1116,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 1117,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 1119,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 1123,
ves_icall_System_String_FastAllocateString_raw,
// token 1124,
ves_icall_System_String_InternalIsInterned_raw,
// token 1125,
ves_icall_System_String_InternalIntern_raw,
// token 1409,
ves_icall_System_Type_internal_from_handle_raw,
// token 1601,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1602,
ves_icall_System_ValueType_Equals_raw,
// token 9713,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 9714,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 9716,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 9717,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 9718,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 9719,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 9720,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 9722,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 9724,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 9726,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 9727,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 9738,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 9740,
mono_monitor_exit_icall_raw,
// token 9747,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 9749,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 9751,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 9753,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 9804,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 9805,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 9807,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 9808,
ves_icall_System_Threading_Thread_GetState_raw,
// token 9809,
ves_icall_System_Threading_Thread_SetState_raw,
// token 9810,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 9811,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 9813,
ves_icall_System_Threading_Thread_YieldInternal,
// token 9815,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 10936,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 10940,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 10942,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 10943,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 10944,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 10945,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 11210,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 11211,
ves_icall_System_GCHandle_InternalFree_raw,
// token 11212,
ves_icall_System_GCHandle_InternalGet_raw,
// token 11213,
ves_icall_System_GCHandle_InternalSet_raw,
// token 11231,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 11232,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 11233,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 11235,
ves_icall_System_Runtime_InteropServices_Marshal_SizeOfHelper_raw,
// token 11279,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 11364,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 11366,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalTryGetHashCode_raw,
// token 11368,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 11378,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 11379,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 11380,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 11381,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw,
// token 11382,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 11841,
ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw,
// token 11842,
ves_icall_System_Reflection_Assembly_GetCallingAssembly_raw,
// token 11843,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 11848,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 11849,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 11928,
ves_icall_System_Reflection_AssemblyName_FreeAssemblyName,
// token 11929,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 11972,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 11979,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 11986,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 11997,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 12001,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 12026,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 12105,
ves_icall_System_Reflection_RuntimeAssembly_GetEntryPoint_raw,
// token 12118,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 12120,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 12122,
ves_icall_System_Reflection_RuntimeAssembly_GetTopLevelForwardedTypes_raw,
// token 12145,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 12147,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInfoInternal_raw,
// token 12148,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 12149,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 12150,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 12151,
ves_icall_System_Reflection_Assembly_InternalGetReferencedAssemblies_raw,
// token 12160,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 12175,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 12195,
ves_icall_reflection_get_token_raw,
// token 12196,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 12204,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 12206,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 12213,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 12214,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 12217,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 12219,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 12224,
ves_icall_reflection_get_token_raw,
// token 12230,
ves_icall_get_method_info_raw,
// token 12231,
ves_icall_get_method_attributes,
// token 12238,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 12240,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 12252,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 12255,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 12256,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 12257,
ves_icall_reflection_get_token_raw,
// token 12268,
ves_icall_InternalInvoke_raw,
// token 12277,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 12283,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 12284,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 12285,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 12287,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 12288,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 12305,
ves_icall_InvokeClassConstructor_raw,
// token 12307,
ves_icall_InternalInvoke_raw,
// token 12321,
ves_icall_reflection_get_token_raw,
// token 12344,
ves_icall_System_Reflection_RuntimeModule_InternalGetTypes_raw,
// token 12345,
ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw,
// token 12346,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 12371,
ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw,
// token 12376,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 12406,
ves_icall_reflection_get_token_raw,
// token 12407,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 12966,
ves_icall_CustomAttributeBuilder_GetBlob_raw,
// token 12980,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 13081,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 13082,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 13307,
ves_icall_ModuleBuilder_basic_init_raw,
// token 13308,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 13317,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 13318,
ves_icall_ModuleBuilder_getToken_raw,
// token 13319,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 13325,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 13422,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 13988,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 13989,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 14569,
ves_icall_System_Diagnostics_Debugger_IsLogging,
// token 14570,
ves_icall_System_Diagnostics_Debugger_Log,
// token 14575,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 14585,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 15552,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 15573,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 15575,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 15577,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
0,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
};
