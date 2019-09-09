﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace Bash.Session.AspNetCore.Example.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public RedirectResult Get()
        {
            var session = HttpContext.Features.Get<ISession>()
                ?? throw new NullReferenceException("Session was not found in context");

            const string isLoggedIn = "isLoggedIn";
            session.SetInt32(isLoggedIn, 1);
            session.RenewId();

            return new RedirectResult("/");
        }
    }
}
