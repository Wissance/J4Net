using Wissance.J4Net.Reflection.Methods;
using Wissance.J4Net.Reflection.Binders;
using Wissance.J4Net.Reflection.Methods;

namespace Wissance.J4Net.Reflection.Fields
{
    public abstract class FieldInfo : MemberInfo
    {
        public Type FieldType
		{
			get { return this.FieldSignature.FieldType; }
		}

		public CustomModifiers GetCustomModifiers()
		{
			return this.FieldSignature.GetCustomModifiers();
		}

		public Type[] GetOptionalCustomModifiers()
		{
			return GetCustomModifiers().GetOptional();
		}

		public Type[] GetRequiredCustomModifiers()
		{
			return GetCustomModifiers().GetRequired();
		}

		public bool IsStatic
		{
			get { return (Attributes & FieldAttributes.Static) != 0; }
		}

		public bool IsLiteral
		{
			get { return (Attributes & FieldAttributes.Literal) != 0; }
		}

		public bool IsInitOnly
		{
			get { return (Attributes & FieldAttributes.InitOnly) != 0; }
		}

		public bool IsNotSerialized
		{
			get { return (Attributes & FieldAttributes.NotSerialized) != 0; }
		}

		public bool IsSpecialName
		{
			get { return (Attributes & FieldAttributes.SpecialName) != 0; }
		}

		public bool IsPublic
		{
			get { return (Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public; }
		}

		public bool IsPrivate
		{
			get { return (Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private; }
		}

		public bool IsFamily
		{
			get { return (Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family; }
		}

		public bool IsFamilyOrAssembly
		{
			get { return (Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem; }
		}

		public bool IsAssembly
		{
			get { return (Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly; }
		}

		public bool IsFamilyAndAssembly
		{
			get { return (Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem; }
		}

		public bool IsPinvokeImpl
		{
			get { return (Attributes & FieldAttributes.PinvokeImpl) != 0; }
		}

		public virtual FieldInfo GetFieldOnTypeDefinition()
		{
			return this;
		}

		public abstract bool TryGetFieldOffset(out int offset);

		public bool TryGetFieldMarshal(out FieldMarshal fieldMarshal)
		{
			return FieldMarshal.ReadFieldMarshal(this.Module, GetCurrentToken(), out fieldMarshal);
		}

		internal abstract int ImportTo(Emit.ModuleBuilder module);

		internal virtual FieldInfo BindTypeParameters(Type type)
		{
			return new GenericFieldInstance(this.DeclaringType.BindTypeParameters(type), this);
		}

		internal sealed override bool BindingFlagsMatch(BindingFlags flags)
		{
			return BindingFlagsMatch(IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic)
				&& BindingFlagsMatch(IsStatic, flags, BindingFlags.Static, BindingFlags.Instance);
		}

		internal sealed override bool BindingFlagsMatchInherited(BindingFlags flags)
		{
			return (Attributes & FieldAttributes.FieldAccessMask) > FieldAttributes.Private
				&& BindingFlagsMatch(IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic)
				&& BindingFlagsMatch(IsStatic, flags, BindingFlags.Static | BindingFlags.FlattenHierarchy, BindingFlags.Instance);
		}

		internal sealed override MemberInfo SetReflectedType(Type type)
		{
			return new FieldInfoWithReflectedType(type, this);
		}

		internal sealed override List<CustomAttributeData> GetPseudoCustomAttributes(Type attributeType)
		{
			Module module = this.Module;
			List<CustomAttributeData> list = new List<CustomAttributeData>();
			if (attributeType == null || attributeType.IsAssignableFrom(module.universe.System_Runtime_InteropServices_MarshalAsAttribute))
			{
				FieldMarshal spec;
				if (TryGetFieldMarshal(out spec))
				{
					list.Add(CustomAttributeData.CreateMarshalAsPseudoCustomAttribute(module, spec));
				}
			}
			if (attributeType == null || attributeType.IsAssignableFrom(module.universe.System_Runtime_InteropServices_FieldOffsetAttribute))
			{
				int offset;
				if (TryGetFieldOffset(out offset))
				{
					list.Add(CustomAttributeData.CreateFieldOffsetPseudoCustomAttribute(module, offset));
				}
			}
			return list;
		}
        
        
        public sealed override MemberTypes MemberType
        {
            get { return MemberTypes.Field; }
        }
        
        public abstract FieldAttributes Attributes { get; }
        public abstract void GetDataFromRva(byte[] data, int offset, int length);
        public abstract int FieldRva { get; }
        public abstract Object GetRawConstantValue();
        internal abstract FieldSignature FieldSignature { get; }
    }
}