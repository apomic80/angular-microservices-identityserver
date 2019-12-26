// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer4.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "1", Username = "michele", Password = "michele", 
                Claims = 
                {
                    new Claim("microservice1", "admin"),
                    new Claim("microservice2", "admin"),
                }
            },
            new TestUser{SubjectId = "2", Username = "antonio", Password = "antonio", 
                Claims = 
                {
                    new Claim("microservice1", "user"),
                }
            }
        };
    }
}