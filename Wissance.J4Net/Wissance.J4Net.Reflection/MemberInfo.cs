/*
  Copyright (C) 2009-2015 Jeroen Frijters
  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.

  Jeroen Frijters
  jeroen@frijters.net
  ----------------------------------------------------------------------------------
  Copyright (C) 2020- Ushakov Michael, Wissance LLC
  This software is provided 'as-is', without any express or implied
  warranty. 
  
  Permission is granted to use without restrictions in non profit, 
  educational (school, high school, university e.t.c.) and scientific projects
  If your are planning to use it in other or commercial projects please CONTACT us 
  we will provide you with INDIVIDUAL license (info@wissance.com).
  In case of usage in nonprofit, educational and scientific projects 
  you must place copyright on J4Net from Wissance LLC.
  This notice may not be removed or altered from any source distribution.
  
  Wissance LLC
  info@wissance.com
*/


using System;
using System.Collections.Generic;
using System.Reflection;
using Wissance.J4Net.Reflection.Attributes;
using Wissance.J4Net.Reflection.Binders;

namespace Wissance.J4Net.Reflection
{
    public abstract class MemberInfo : ICustomAttributeProvider
    {
        public bool IsDefined(Type attributeType, bool inherit)
        {
            //return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit).Count != 0;
            throw new System.NotImplementedException();
        }

        public IList<CustomAttributeData> Provide(Type attributeType, bool inherit)
        {
            // return CustomAttributeData.__GetCustomAttributes(this, attributeType, inherit);
            throw new System.NotImplementedException();
        }
        
        public IList<CustomAttributeData> GetCustomAttributesData()
        {
            throw new System.NotImplementedException();
            //return CustomAttributeData.GetCustomAttributes(this);
        }

        public IEnumerable<CustomAttributeData> CustomAttributes
        {
            get { return GetCustomAttributesData(); }
        }

        internal abstract bool BindingFlagsMatch(BindingFlags flags);

        internal abstract bool BindingFlagsMatchInherited(BindingFlags flags);
        
        protected static bool BindingFlagsMatch(bool state, BindingFlags flags, BindingFlags trueFlag, BindingFlags falseFlag)
        {
            return (state && (flags & trueFlag) == trueFlag)
                   || (!state && (flags & falseFlag) == falseFlag);
        }

        protected static T SetReflectedType<T>(T member, Type type)
            where T : MemberInfo
        {
            return member == null ? null : (T)member.SetReflectedType(type);
        }

        protected static T[] SetReflectedType<T>(T[] members, Type type)
            where T : MemberInfo
        {
            for (int i = 0; i < members.Length; i++)
            {
                members[i] = SetReflectedType(members[i], type);
            }
            return members;
        }
        
        public static bool operator ==(MemberInfo m1, MemberInfo m2)
        {
            return ReferenceEquals(m1, m2) || (!ReferenceEquals(m1, null) && m1.Equals(m2));
        }

        public static bool operator !=(MemberInfo m1, MemberInfo m2)
        {
            return !(m1 == m2);
        }
        
        internal abstract MemberInfo SetReflectedType(Type type);
        
        public string Name { get; internal set; }
        public Type DeclaringType { get; internal set; }
        //public abstract MemberTypes MemberType { get; }
        public int MetadataToken { get; private set; }
        
        /// <summary>
        ///    TODO: UMV: not transferred yet, should be another module
        /// </summary>
        public Module Module { get; private set; }
        /// <summary>
        ///    Was __IsMissing
        /// </summary>
        public bool IsMissing { get; private set; }
        internal bool IsBaked { get; private set; }

        public virtual Type ReflectedType
        {
            get { return DeclaringType; }
        }
    }
}