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
using System.Globalization;
using Wissance.J4Net.Reflection.Methods;
using Wissance.J4Net.Reflection.Fields;


namespace Wissance.J4Net.Reflection.Binders
{
    public abstract class Binder
    {
        /// <summary>
        ///     Was virtual method in original Binder with throw InvalidOperationException
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <param name="match"></param>
        /// <param name="args"></param>
        /// <param name="modifiers"></param>
        /// <param name="culture"></param>
        /// <param name="names"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public abstract MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match,
                                                ref object[] args, ParameterModifier[] modifiers, CultureInfo culture,
                                                string[] names, out object state);

        /// <summary>
        ///     Was virtual method in original Binder with throw InvalidOperationException
        /// </summary>
        /// <param name="bindingAttr"></param>
        /// <param name="match"></param>
        /// <param name="value"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture);

        /// <summary>
        ///     Was virtual method in original Binder with throw InvalidOperationException
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ChangeType(object value, Type type, CultureInfo culture);

        /// <summary>
        ///     Was virtual method in original Binder with throw InvalidOperationException
        /// </summary>
        /// <param name="args"></param>
        /// <param name="state"></param>
        public abstract void ReorderArgumentArray(ref object[] args, object state);
        
        public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers);
        public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers);
    }
}