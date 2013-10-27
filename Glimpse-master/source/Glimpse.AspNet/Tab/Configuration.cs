﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Web.Configuration;
using Glimpse.AspNet.Extensibility;
using Glimpse.AspNet.Model;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;

namespace Glimpse.AspNet.Tab
{
    /// <summary>
    /// Configuration tab
    /// </summary>
    public class Configuration : AspNetTab, IDocumentation, ILayoutControl, IKey
    {
        private const string PasswordHash = "########";
        private readonly IEnumerable<string> keysToAnnomalizePassword = new[] { "Password", "Pwd" };

        /// <summary>
        /// Gets the name that will show in the tab.
        /// </summary>
        /// <value>The name.</value>
        public override string Name
        {
            get { return "Configuration"; }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key. Only valid JavaScript identifiers should be used for future compatibility.</value>
        public string Key 
        {
            get { return "glimpse_configuration"; }
        }

        /// <summary>
        /// Gets the documentation URI.
        /// </summary>
        /// <value>The documentation URI.</value>
        public string DocumentationUri
        {
            get { return "http://getglimpse.com/Help/Configuration-Tab"; }
        }

        public bool KeysHeadings
        { 
            get { return true; } 
        }
        
        /// <summary>
        /// Gets the data that should be shown in the UI.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Object that will be shown.</returns>
        public override object GetData(ITabContext context)
        { 
            var result = new ConfigurationModel();
            result.AppSettings = ConfigurationManager.AppSettings.ToDictionary();
            result.Authentication = ProcessAuthenticationSection(ConfigurationManager.GetSection("system.web/authentication") as AuthenticationSection);
            result.ConnectionStrings = ProcessConnectionString(ConfigurationManager.ConnectionStrings);
            result.CustomErrors = ProcessCustomErrors(ConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection);
            result.HttpModules = ProcessHttpModules(ConfigurationManager.GetSection("system.web/httpModules") as HttpModulesSection);
            result.HttpHandlers = ProcessHttpHandler(ConfigurationManager.GetSection("system.web/httpHandlers") as HttpHandlersSection);
            result.RoleManager = ProcessRoleManager(ConfigurationManager.GetSection("system.web/roleManager") as RoleManagerSection);
             
            return result;
        }

        private ConfigurationAuthenticationModel ProcessAuthenticationSection(AuthenticationSection authenticationSection)
        {
            if (authenticationSection == null)
            {
                return null;
            }

            var formsSection = authenticationSection.Forms;

            var result = new ConfigurationAuthenticationModel();
            result.Mode = authenticationSection.Mode.ToString();

            if (result.Forms != null)
            {
                result.Forms = new ConfigurationAuthenticationFormsModel();
                result.Forms.Cookieless = formsSection.Cookieless.ToString();
                result.Forms.DefaultUrl = formsSection.DefaultUrl;
                result.Forms.Domain = formsSection.Domain;
                result.Forms.EnableCrossAppRedirects = formsSection.EnableCrossAppRedirects;
                result.Forms.Name = formsSection.Name;
                result.Forms.Path = formsSection.Path;
                result.Forms.Protection = formsSection.Protection.ToString();
                result.Forms.RequireSSL = formsSection.RequireSSL;
                result.Forms.SlidingExpiration = formsSection.SlidingExpiration;
                result.Forms.TicketCompatibilityMode = TicketCompatibilityMode(formsSection);
                result.Forms.Timeout = formsSection.Timeout;

                var credentialsSection = formsSection.Credentials;
                if (credentialsSection != null)
                {
                    result.Forms.Credentials = new ConfigurationAuthenticationFormsCredentialsModel();
                    result.Forms.Credentials.PasswordFormat = credentialsSection.PasswordFormat.ToString();
                }
            }

            return result;
        }

        private IEnumerable<ConfigurationConnectionStringModel> ProcessConnectionString(ConnectionStringSettingsCollection connectionStrings)
        {
            if (connectionStrings == null)
            {
                return null;
            }

            var result = new List<ConfigurationConnectionStringModel>();

            foreach (ConnectionStringSettings connectionString in connectionStrings)
            {
                var resultItem = new ConfigurationConnectionStringModel();
                resultItem.Key = connectionString.Name;
                resultItem.Raw = connectionString.ConnectionString;
                resultItem.ProviderName = connectionString.ProviderName;

                try
                {
                    var providerName = string.IsNullOrEmpty(connectionString.ProviderName) ? "System.Data.SqlClient" : connectionString.ProviderName;
                    var connectionFactory = DbProviderFactories.GetFactory(providerName);
                    var connectionStringBuilder = connectionFactory.CreateConnectionStringBuilder();
                    if (connectionStringBuilder != null)
                    {
                        connectionStringBuilder.ConnectionString = connectionString.ConnectionString;

                        var connectionDetails = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
                        var keys = connectionStringBuilder.Keys;
                        if (keys != null)
                        {
                            foreach (string key in keys)
                            {
                                connectionDetails.Add(key, connectionStringBuilder[key]);
                            }

                            resultItem.Details = connectionDetails;

                            AnnomalizeConnectionStringPassword(connectionDetails, resultItem);
                        }
                    }
                }
                catch (Exception e)
                {
                    resultItem.Details = new Dictionary<string, object> { { "Error", e.Message } };
                }

                result.Add(resultItem);
            }

            return result.Count > 0 ? result : null;
        } 

        private void AnnomalizeConnectionStringPassword(IDictionary<string, object> connectionDetails, ConfigurationConnectionStringModel model)
        {
            foreach (var key in keysToAnnomalizePassword)
            {
                if (connectionDetails.ContainsKey(key))
                {
                    var password = connectionDetails[key].ToString();
                    if (!string.IsNullOrEmpty(password))
                    {
                        connectionDetails[key] = PasswordHash; 
                        model.Raw = model.Raw.Replace(password, PasswordHash);
                    }

                    return;
                }
            }
        }

        private ConfigurationCustomErrorsModel ProcessCustomErrors(CustomErrorsSection customErrorsSection)
        {
            if (customErrorsSection == null)
            {
                return null;
            }

            var result = new ConfigurationCustomErrorsModel();
            result.DefaultRedirect = customErrorsSection.DefaultRedirect;
            result.RedirectMode = customErrorsSection.RedirectMode.ToString();
            result.Mode = customErrorsSection.Mode.ToString(); 
            
            var errorsSection = customErrorsSection.Errors;
            if (errorsSection != null)
            {
                var resultErrors = new List<ConfigurationCustomErrorsErrorModel>();
                foreach (CustomError error in errorsSection)
                {
                    var resultError = new ConfigurationCustomErrorsErrorModel();
                    resultError.Redirect = error.Redirect;
                    resultError.StatusCode = error.StatusCode;

                    resultErrors.Add(resultError);
                }

                result.Errors = resultErrors;
            }

            return result;
        }

        private IEnumerable<ConfigurationHttpModulesModel> ProcessHttpModules(HttpModulesSection httpModulesSection)
        {
            if (httpModulesSection == null)
            {
                return null;
            }

            var result = new List<ConfigurationHttpModulesModel>();
            foreach (HttpModuleAction httpModule in httpModulesSection.Modules)
            {
                var resultItem = new ConfigurationHttpModulesModel();
                resultItem.Name = httpModule.Name;
                resultItem.Type = httpModule.Type;

                result.Add(resultItem);
            }

            return result;
        }

        private IEnumerable<ConfigurationHttpHandlersModel> ProcessHttpHandler(HttpHandlersSection httpHandlersSection)
        {
            if (httpHandlersSection == null)
            {
                return null;
            }

            var result = new List<ConfigurationHttpHandlersModel>();
            foreach (HttpHandlerAction httpModule in httpHandlersSection.Handlers)
            {
                var resultItem = new ConfigurationHttpHandlersModel();
                resultItem.Path = httpModule.Path;
                resultItem.Verb = httpModule.Verb;
                resultItem.Validate = httpModule.Validate;
                resultItem.Type = httpModule.Type;

                result.Add(resultItem);
            }

            return result;
        }

        private ConfigurationRoleManagerModel ProcessRoleManager(RoleManagerSection roleManagerSection)
        {
            if (roleManagerSection == null)
            {
                return null;
            }

            var result = new ConfigurationRoleManagerModel();
            result.CacheRolesInCookie = roleManagerSection.CacheRolesInCookie;
            result.CookieName = roleManagerSection.CookieName;
            result.CookiePath = roleManagerSection.CookiePath;
            result.CookieProtection = roleManagerSection.CookieProtection.ToString();
            result.CookieRequireSSL = roleManagerSection.CookieRequireSSL;
            result.CookieSlidingExpiration = roleManagerSection.CookieSlidingExpiration;
            result.CookieTimeout = roleManagerSection.CookieTimeout;
            result.CreatePersistentCookie = roleManagerSection.CreatePersistentCookie;
            result.DefaultProvider = roleManagerSection.DefaultProvider;
            result.Domain = roleManagerSection.Domain;
            result.Enabled = roleManagerSection.Enabled;
            result.MaxCachedResults = roleManagerSection.MaxCachedResults;

            var providerSection = roleManagerSection.Providers;
            if (providerSection != null)
            {
                var resultProviders = new List<ConfigurationRoleManagerProviderSettingsModel>();
                foreach (ProviderSettings provider in providerSection)
                {
                    var resultProvider = new ConfigurationRoleManagerProviderSettingsModel();
                    resultProvider.Name = provider.Name;
                    resultProvider.Type = provider.Type;
                    resultProvider.Parameters = provider.Parameters.ToDictionary();

                    resultProviders.Add(resultProvider);
                }

                result.Providers = resultProviders;
            }

            return result;
        }

        private string TicketCompatibilityMode(FormsAuthenticationConfiguration formsSection)
        {
#if NET35
            return "n/a";      
#else
            return formsSection.TicketCompatibilityMode.ToString();
#endif
        }
    }
}
