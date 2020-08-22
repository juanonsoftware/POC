using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;
    
namespace Resources {
        public class Resources {
            private static IResourceProvider resourceProvider = new XmlResourceProvider(@"D:\Wip\Practices\Github\POC\aspnet-mvc-internationalization-store-strings-in-database-or-xml\Resources\Resources.xml");

                
        
        public static string AddPerson {
               get {
                   return resourceProvider.GetResource("AddPerson", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string Age {
               get {
                   return resourceProvider.GetResource("Age", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string AgeRange {
               get {
                   return resourceProvider.GetResource("AgeRange", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string AgeRequired {
               get {
                   return resourceProvider.GetResource("AgeRequired", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string Biography {
               get {
                   return resourceProvider.GetResource("Biography", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string ChooseYourLanguage {
               get {
                   return resourceProvider.GetResource("ChooseYourLanguage", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string Create {
               get {
                   return resourceProvider.GetResource("Create", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string Email {
               get {
                   return resourceProvider.GetResource("Email", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string EmailInvalid {
               get {
                   return resourceProvider.GetResource("EmailInvalid", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string EmailRequired {
               get {
                   return resourceProvider.GetResource("EmailRequired", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string FirstName {
               get {
                   return resourceProvider.GetResource("FirstName", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string FirstNameLong {
               get {
                   return resourceProvider.GetResource("FirstNameLong", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string FirstNameRequired {
               get {
                   return resourceProvider.GetResource("FirstNameRequired", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string LastName {
               get {
                   return resourceProvider.GetResource("LastName", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string LastNameLong {
               get {
                   return resourceProvider.GetResource("LastNameLong", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string LastNameRequired {
               get {
                   return resourceProvider.GetResource("LastNameRequired", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string LogOff {
               get {
                   return resourceProvider.GetResource("LogOff", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string LogOn {
               get {
                   return resourceProvider.GetResource("LogOn", CultureInfo.CurrentUICulture.Name) as string;
               }
            }
            
        
        public static string Register {
               get {
                   return resourceProvider.GetResource("Register", CultureInfo.CurrentUICulture.Name) as string;
               }
            }

        }        
}
