using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Web.Compilation;
using System.Resources;
using System.Collections;
using System.Collections.Specialized;

namespace YourNameSpace
{

	public sealed class SqlResourceProviderFactory : ResourceProviderFactory
	{

		public SqlResourceProviderFactory()
		{
		}

		public override IResourceProvider CreateGlobalResourceProvider(string classKey)
		{
                    return new SqlResourceProvider(null, classKey);
		}

		public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
		{
                    virtualPath = System.IO.Path.GetFileName(virtualPath);
		    return new SqlResourceProvider(virtualPath, null);
		}

		 
		//
		// inner class
		//
		private sealed class SqlResourceProvider : IResourceProvider
		{
			private string _virtualPath;
			private string _className;
			private IDictionary _resourceCache;

			private static object CultureNeutralKey = new object();

			public SqlResourceProvider(string virtualPath, string className)
			{
				_virtualPath = virtualPath;
				_className = className;
			}

			private IDictionary GetResourceCache(string cultureName)
			{
				object cultureKey;

				if (cultureName != null)
				{
					cultureKey = cultureName;
				}
				else
				{
					cultureKey = CultureNeutralKey;
				}
				

				if (_resourceCache == null)
				{
					_resourceCache = new ListDictionary();
				}

				IDictionary resourceDict = _resourceCache[cultureKey] as IDictionary;

				if (resourceDict == null)
				{
					resourceDict = SqlResourceHelper.GetResources(_virtualPath, _className, cultureName, false, null);
					_resourceCache[cultureKey] = resourceDict;
				}
				
				return resourceDict;
			}

			object IResourceProvider.GetObject(string resourceKey, CultureInfo culture)
			{
				string cultureName = null;
				if (culture != null)
				{
					cultureName = culture.Name;
				}
				else
				{
					cultureName = CultureInfo.CurrentUICulture.Name;
				}
				
				object value = GetResourceCache(cultureName)[resourceKey];
				if (value == null)
				{
					// resource is missing for current culture
					SqlResourceHelper.AddResource(resourceKey, _virtualPath, _className, cultureName);
					value = GetResourceCache(null)[resourceKey];
				}

				if (value == null)
				{ 
					// the resource is really missing, no default exists
					SqlResourceHelper.AddResource(resourceKey, _virtualPath, _className, string.Empty);
				}

				return value;
			}

			IResourceReader IResourceProvider.ResourceReader
			{
				get
				{
					return new SqlResourceReader(GetResourceCache(null));

				}
			}

		}
		
		//
		// inner class
		//
		private sealed class SqlResourceReader : IResourceReader {
                private IDictionary _resources;

                public SqlResourceReader(IDictionary resources) {
					_resources = resources;
                }

                IDictionaryEnumerator IResourceReader.GetEnumerator() {
                    return _resources.GetEnumerator();
                }

                void IResourceReader.Close() {
                }

                IEnumerator IEnumerable.GetEnumerator() {
                    return _resources.GetEnumerator();
                }

                void IDisposable.Dispose() {
                }
            }


	}


	 
	internal static class SqlResourceHelper
	{
		private const string DatabaseLocationKey = "LocalizationDatabasePath";

		public static IDictionary GetResources(string virtualPath, string className, string cultureName, bool designMode, IServiceProvider serviceProvider)
		{
			SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["your_connection_string"].ToString());
			SqlCommand com = new SqlCommand();

			//
			// Build correct select statement to get resource values
			//
			if (!String.IsNullOrEmpty(virtualPath))
			{
				//
				// Get Local resources
				//
				if (string.IsNullOrEmpty(cultureName))
				{ 
					// default resource values (no culture defined)
					com.CommandType = CommandType.Text;
					com.CommandText = "select resource_name, resource_value from ASPNET_GLOBALIZATION_RESOURCES where resource_object = @virtual_path and culture_name is null";
					com.Parameters.AddWithValue("@virtual_path",virtualPath);
				}
				else
				{
					com.CommandType = CommandType.Text;
					com.CommandText = "select resource_name, resource_value from ASPNET_GLOBALIZATION_RESOURCES where resource_object = @virtual_path and culture_name = @culture_name ";
					com.Parameters.AddWithValue("@virtual_path", virtualPath);
					com.Parameters.AddWithValue("@culture_name", cultureName);
				}
			}
			else if (!String.IsNullOrEmpty(className))
			{ 
				//
				// Get Global resources
				// 
				if (string.IsNullOrEmpty(cultureName))
				{
					// default resource values (no culture defined)
					com.CommandType = CommandType.Text;
					com.CommandText = "select resource_name, resource_value from ASPNET_GLOBALIZATION_RESOURCES where resource_object = @class_name and culture_name is null";
					com.Parameters.AddWithValue("@class_name", className);
				}
				else
				{
					com.CommandType = CommandType.Text;
					com.CommandText = "select resource_name, resource_value from ASPNET_GLOBALIZATION_RESOURCES where resource_object = @class_name and culture_name = @culture_name ";
					com.Parameters.AddWithValue("@class_name", className);
					com.Parameters.AddWithValue("@culture_name", cultureName);
				}
			}
			else 
			{
				//
				// Neither virtualPath or className provided, unknown if Local or Global resource
				//
				throw new Exception("SqlResourceHelper.GetResources() - virtualPath or className missing from parameters."); 
			}


			ListDictionary resources = new ListDictionary();
			try
			{
				com.Connection = con;
				con.Open();
				SqlDataReader sdr = com.ExecuteReader(CommandBehavior.CloseConnection);
				
				while (sdr.Read())
				{
					string rn = sdr.GetString(sdr.GetOrdinal("resource_name"));
					string rv = sdr.GetString(sdr.GetOrdinal("resource_value"));
					resources.Add(rn, rv);
				}
			}
			catch (Exception e)
			{
                            throw new Exception(e.Message, e); 
			}
			finally
			{
				if (con.State == ConnectionState.Open)
				{
					con.Close();
				}
			}

			return resources;
		}


		public static void AddResource(string resource_name, string virtualPath, string className, string cultureName)
		{

			SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["your_connection_string"].ToString());
			SqlCommand com = new SqlCommand(); 

			StringBuilder sb = new StringBuilder();
			sb.Append("insert into ASPNET_GLOBALIZATION_RESOURCES (resource_name ,resource_value,resource_object,culture_name ) ");
			sb.Append(" values (@resource_name ,@resource_value,@resource_object,@culture_name) ");
			com.CommandText = sb.ToString();
			com.Parameters.AddWithValue("@resource_name",resource_name);
			com.Parameters.AddWithValue("@resource_value", resource_name + " * DEFAULT * " + (String.IsNullOrEmpty( cultureName) ? string.Empty : cultureName ));
			com.Parameters.AddWithValue("@culture_name", (String.IsNullOrEmpty(cultureName) ? SqlString.Null : cultureName));			
			
			string resource_object = "UNKNOWN **ERROR**";
			if (!String.IsNullOrEmpty(virtualPath))
			{
				resource_object = virtualPath;
			}
			else if (!String.IsNullOrEmpty(className))
			{
				resource_object = className;
			}
			com.Parameters.AddWithValue("@resource_object", resource_object);
			

			try 
			{
				com.Connection = con;
				con.Open();
				com.ExecuteNonQuery();
			}
			catch (Exception e)
			{
			    throw new Exception(e.ToString())
			}
			finally 
			{
				if (con.State == ConnectionState.Open)
					con.Close();
			}

		}
	}


}
