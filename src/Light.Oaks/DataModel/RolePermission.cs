namespace Light.Oaks
{
    /// <summary>
    /// Role permission.
    /// </summary>
    public class RolePermission
    {
        /// <summary>
        /// Gets or sets the role code.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the permission code.
        /// </summary>
        /// <value>The permission code.</value>
        public string Permission { get; set; }
    }

    public class Role
    {
        public string Code
        {
            get;
            set;
        }

        public string[] Permissions
        {
            get;
            set;
        }
    }
}