using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMiddleware.Common
{
    public class SystemConstants
    {
        public class UserClaim
        {
            public class Animail
            {
                private string _name;
                public string Name
                {
                    set
                    {
                        _name = value;
                    }

                    get
                    {
                        return _name;
                    }
                }
                public class Dog
                {
                    public string Color { set; get; }
                }
            }

            public const string Roles = "Roles";
            public const string Id = "Id";
            public const string Permissions = "permissions";
            public const string FullName = "fullName";
        }
    }
}
