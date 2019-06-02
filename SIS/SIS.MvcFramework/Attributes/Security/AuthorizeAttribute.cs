namespace SIS.MvcFramework.Attributes.Security
{
    using System;
    using Identity;

    public class AuthorizeAttribute : Attribute
    {
        private readonly string authrority;

        public AuthorizeAttribute(string authority = Principal.Authorized)
        {
            this.authrority = authority;
        }

        public bool IsLoggedIn(Principal principal)
        {
            return principal != null;
        }

        public bool IsInAuthority(Principal principal)
        {
            if (!this.IsLoggedIn(principal))
            {
                return this.authrority == Principal.Anonymous;
            }

            return this.authrority == Principal.Authorized
                   || principal.Roles.Contains(this.authrority.ToLower());
        }
    }
}