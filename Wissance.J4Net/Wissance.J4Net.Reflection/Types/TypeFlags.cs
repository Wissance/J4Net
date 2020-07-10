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

namespace Wissance.J4Net.Reflection.Types
{
    [Flags]
    internal enum TypeFlags
    {
        // for use by TypeBuilder or TypeDefImpl
        IsGenericTypeDefinition = 1,

        // for use by TypeBuilder
        HasNestedTypes = 2,
        Baked = 4,

        // for use by IsValueType to cache result of IsValueTypeImpl
        ValueType = 8,
        NotValueType = 16,

        // for use by TypeDefImpl, TypeBuilder or MissingType
        PotentialEnumOrValueType = 32,
        EnumOrValueType = 64,

        // for use by TypeDefImpl
        NotGenericTypeDefinition = 128,

        // used to cache __ContainsMissingType
        ContainsMissingTypeUnknown = 0,
        ContainsMissingTypePending = 256,
        ContainsMissingTypeYes = 512,
        ContainsMissingTypeNo = 256 | 512,
        ContainsMissingTypeMask = 256 | 512,

        // built-in type support
        PotentialBuiltIn = 1024,
        BuiltIn = 2048,
    }
}