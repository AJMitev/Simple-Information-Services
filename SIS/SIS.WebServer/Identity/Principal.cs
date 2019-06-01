namespace SIS.MvcFramework.Identity
{
    using System.Collections.Generic;

    public class Principal
    {
        public const string Anonymous = "anonymous";
        public const string Authorized = "authorized";

        public Principal()
        {
            this.Roles = new List<string>();
        }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}