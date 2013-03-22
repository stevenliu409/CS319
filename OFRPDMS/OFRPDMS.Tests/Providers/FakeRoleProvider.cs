using System.Configuration.Provider;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Globalization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Security;

// To be removed
namespace OFRPDMS.Tests.Providers
{
    class FakeRoleProvider : RoleProvider
    {
        private string eventSource = "FakeRoleProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please check the Event Log.";

        private ConnectionStringSettings pConnectionStringSettings;
        private string connectionString;


        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog = false;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }



        //
        // System.Configuration.Provider.ProviderBase.Initialize Method
        //

        public override void Initialize(string name, NameValueCollection config)
        {

            //
            // Initialize values from web.config.
            //

            base.Initialize(name, config);
        }



        //
        // System.Web.Security.RoleProvider properties.
        //


        private string pApplicationName;


        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        //
        // System.Web.Security.RoleProvider methods.
        //

        //
        // RoleProvider.AddUsersToRoles
        //

        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {

        }


        //
        // RoleProvider.CreateRole
        //

        public override void CreateRole(string rolename)
        {
            
        }


        //
        // RoleProvider.DeleteRole
        //

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            return true;
        }


        //
        // RoleProvider.GetAllRoles
        //

        public override string[] GetAllRoles()
        {
            string[] roles = new string[] { "Administrators", "Staff" };
            return roles;
        }


        //
        // RoleProvider.GetRolesForUser
        //

        public override string[] GetRolesForUser(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException();
            }

            if (username == "admin")
            {
                string[] roles = new string[] { "Administrators", "Staff" };
                return roles;
            }
            else if (username == "staff")
            {
                string[] roles = new string[] { "Staff" };
                return roles;
            }
            else
            {
                return new string[0];
            }
        }


        //
        // RoleProvider.GetUsersInRole
        //

        public override string[] GetUsersInRole(string rolename)
        {
            if (rolename == "Administrators")
            {
                return new string[1] { "admin" };
            }
            else if (rolename == "Staff")
            {
                return new string[2] { "admin", "staff" };
            }
            else
                return new string[0];
        }


        //
        // RoleProvider.IsUserInRole
        //

        public override bool IsUserInRole(string username, string rolename)
        {
            if (username == "admin" && (rolename == "Staff" || rolename == "Administrators"))
                return true;
            else if (username == "staff" && (rolename == "Staff"))
                return true;
            else
                return false;
        }


        //
        // RoleProvider.RemoveUsersFromRoles
        //

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            
        }


        //
        // RoleProvider.RoleExists
        //

        public override bool RoleExists(string rolename)
        {
            if (rolename == "Administrators" || rolename == "Staff")
                return true;
            else
                return false;
        }

        //
        // RoleProvider.FindUsersInRole
        //

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            if (rolename == "Administrators")
                return new string[1] { "admin" };
            else if (rolename == "Staff")
                return new string[2] { "admin", "staff" };
            else
                return new string[0];
        }

        //
        // WriteToEventLog
        //   A helper function that writes exception detail to the event log. Exceptions
        // are written to the event log as a security measure to avoid private database
        // details from being returned to the browser. If a method does not return a status
        // or boolean indicating the action succeeded or failed, a generic exception is also 
        // thrown by the caller.
        //

        private void WriteToEventLog(OdbcException e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = exceptionMessage + "\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }
    }
}
