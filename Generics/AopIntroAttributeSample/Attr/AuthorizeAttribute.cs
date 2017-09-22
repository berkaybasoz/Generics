using AopIntroAttributeSample.Interception;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AopIntroAttributeSample.Attr
{ 
    public class AuthorizeAttribute : InterceptAttribute, IPreVoidInterception
    {  
        public string[] Roles { get; set; }

        public void OnPre(PreInterceptArgs e)
        {
            //Çalıştırılan metodu e.MethodName ile Roles arasında bağ kurup yetki kontrolü yapıp, yetkisi olmayan metodlar için hata fırlatırmalıyız
            if (Roles != null)
            {
                bool autrized = false;
                foreach (var role in Roles)
                {
                    autrized= Thread.CurrentPrincipal.IsInRole(role);
                    if (autrized)
                        break;
                }

                if (!autrized)
                {
                    throw new InvalidCredentialException($"{e.MethodName} metodunu çalıştırmak için yetkiniz yoktur");
                }
            }
        }
    }
}
