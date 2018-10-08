using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Assignment2.BLL.Contracts;
using Assignment2.BLL.Services;

namespace Assignment2.API.Authentication
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private IUserService userService;
        public AuthenticationHandler()
        {
            this.userService =new UserService();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Headers.Contains("Authorization"))
                {
                    HttpContext httpContext = HttpContext.Current;
                    string authHeader = httpContext.Request.Headers["Authorization"];

                    string encodedUsernamePassword = authHeader.Substring("Authorization ".Length).Trim();

                    //Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    //tokens = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    string tokens = encodedUsernamePassword;
                    if (tokens != null)
                    {
                        string[] tokensValues = tokens.Split(':');

                        var ObjUser = userService.Login(tokensValues[0], tokensValues[1]);
                        if (ObjUser != null)
                        {
                            IPrincipal principal = new GenericPrincipal(new GenericIdentity(ObjUser.Email), ObjUser.Type.Split(','));
                            Thread.CurrentPrincipal = principal;
                            HttpContext.Current.User = principal;
                        }
                        else
                        {
                            //The user is unauthorize and return 401 status  
                            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                            var tsc = new TaskCompletionSource<HttpResponseMessage>();
                            tsc.SetResult(response);
                            return tsc.Task;
                        }
                    }
                    else
                    {
                        //Bad Request request because Authentication header is set but value is null  
                        var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                //User did not set Authentication header  
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }

    }
}