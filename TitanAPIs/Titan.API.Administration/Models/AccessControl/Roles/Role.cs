namespace Titan.Models.AccessControl.Roles
{
    /// <summary>
    /// A role in Titan that allows users access to features
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string RoleName { get; set; }
    }
}
