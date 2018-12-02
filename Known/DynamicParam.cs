﻿using System.Collections.Generic;
using System.Dynamic;

namespace Known
{
    internal sealed class DynamicParam : DynamicObject
    {
        private Dictionary<string, object> datas = new Dictionary<string, object>();

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return datas.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = datas[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            datas[binder.Name] = value;
            return true;
        }
    }
}
