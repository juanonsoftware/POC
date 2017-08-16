using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;
    
namespace CustomerBasedMultiLanguages.Resources {
        public partial class GlobalResources {           

                
        
        public static string WelcomeTitle {
               get {
                   return resourceProvider.GetResource("WelcomeTitle", CultureInfo.CurrentUICulture.Name) as string;
               }
            }

        }        
}
